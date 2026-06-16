"use strict";
(function (react, html, axios, modal, taskValue, global) {

    const useState = react.useState;

    const useEffect = react.useEffect;

    function clientRequest() {

        const request = axios.create('/api/obligation/tag');

        function remove(task, id) {
            return request.client().delete(`/${task}`, { data: { id: id } })
        }

        function add(task, id) {
            return request.client().post(`/${task}`, { data: { id: id } });
        }

        return {
            fetch: request.fetch,
            remove: remove,
            search: request.search,
            add: add,
            isStatusCompleted: request.isStatusCompleted
        }
    }

    function tag() {

        const task = taskValue();

        const [tags, setTags] = useState([]);

        const [showModal, setShowModal] = useState(false);

        const request = clientRequest();

        useEffect(function () {
            request.fetch(task)
                .then(function (response) {
                    if (request.isStatusCompleted(response)) {
                        var result = [...response.data];
                        setTags(result)
                    }
                });

        }, []);

        function remove(id) {
            request.remove(task, id)
                .then(function (response) {
                    if (request.isStatusCompleted(response)) {
                        var result = [...tags.filter(function (item) { return item.id !== id })];
                        setTags(result);
                    }
                });
        }

        function add(tag) {
            return new Promise(function (resolve, reject) {
                if (!tags.some(function (item) { return item.id === tag.id }))
                    request.add(task, tag.id)
                        .then(function (response) {
                            if (request.isStatusCompleted(response)) {
                                resolve(true);
                                var result = [...tags];
                                result.push(tag);
                                setTags(result);
                            }
                        }).catch(function (error) { resolve(false); });
                else
                    resolve(false);


            });
        }

        return html`<div className="flex flex-col gap-2">
                        <div className="flex flex-row items-center">
                            <span className="text-sm text-foreground font-medium">
                                برچسب ها <span className="text-muted-foreground">[${tags.length}]</span>
                            </span>
                            <button type="button" className="kt-btn kt-btn-icon kt-btn-outline kt-btn-sm ms-2.5" onClick=${function () { setShowModal(true); }}>
                                <i className="ki-filled ki-plus"></i>
                            </button>
                        </div>
                        <div className="flex items-center gap-3 flex-wrap">
                            ${tags.map(function (item, index) { return html`<${tagItem} key="${index}" id="${item.id}" name="${item.name}" remove="${remove}" />` })}
                        </div>
                        <${modal} status="${showModal}" setStatus="${setShowModal}" request="${request}" add="${add}"  />
                    </div>`;
    }

    function tagItem({ id, name, remove }) {
        return html`<div className="flex items-center gap-2 rounded-lg border border-border ps-2 p-0.5">
                        <span className="text-xs max-w-[160px] truncate">${name}</span>
                        <div className="flex items-center pl-1 border-r border-border">
                            <button type="button" className="kt-btn kt-btn-icon kt-btn-dim" onClick=${function () { remove(id) }}>
                                <i className="ki-filled ki-cross"></i>
                            </button>
                        </div>
                    </div>`;
    }

    if (typeof global.tag === 'undefined') {
        global.tag = tag;
    }

})(React, renderer, request, suggest, task, window)