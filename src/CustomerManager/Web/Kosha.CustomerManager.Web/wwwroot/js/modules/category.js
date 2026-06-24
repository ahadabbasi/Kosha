"use strict";
(function (react, html, axios, modal, taskValue, global) {

    const useState = react.useState;

    const useEffect = react.useEffect;

    const fragment = react.Fragment;

    function clientRequest() {

        const request = axios.create('/api/obligation/category');

        function change(task, id) {
            return request.client().post(`/${task}`, { data: { id: id } });
        }

        function list() {
            return request.client().get();
        }

        return {
            fetch: request.fetch,
            list: list,
            change: change,
            isStatusCompleted: request.isStatusCompleted
        }
    }

    function category() {

        const task = taskValue();

        const request = clientRequest();

        const [current, setCurrent] = useState(undefined);

        const [showModal, setShowModal] = useState(false);

        const [list, setList] = useState([]);

        useEffect(function () {
            request.fetch(task)
                .then(function (response) {
                    if (request.isStatusCompleted(response)) {
                        setCurrent(response.data);
                    }
                });

            request.list()
                .then(function (response) {
                    if (request.isStatusCompleted(response)) {
                        setList(response.data);
                    }
                });
        }, []);

        function categoryChange(value) {
            setCurrent(value);
            setShowModal(false);
            request.change(task, value.id);
        }

        function categoryItem({ value }) {
            return html`<div className="flex items-center gap-2.5">
                            <input className="kt-radio" type="radio" name="category" checked=${value.id === current.id} onChange=${function (e) { categoryChange(value); }} />
                            <label>${value.name}</label>
                        </div>`
        }

        return current === undefined ?
            html`<${fragment}></${fragment}>` :
            html`<${fragment}>
                    <button className="kt-btn kt-btn-secondary" onClick=${function () { setShowModal(true); }}>${current.name}</button>
                    <${modal} status="${showModal}" setStatus="${setShowModal}">
                        <div className="grid gap-2.5">
                            ${list.map(function (value, index) { return html`<${categoryItem} value="${value}" key="${index}" />`;  })}
                        </div>
                    </${modal}>
                </${fragment}>`
    }

    if (typeof global.category === 'undefined') {
        global.category = category;
    }

})(React, renderer, request, modal, task, window)