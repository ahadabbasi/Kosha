"use strict";
(function (react, html, axios, modal, global) {

    const useState = react.useState;

    const useEffect = react.useEffect;

    function clientRequest() {

        const request = axios.create('/api/obligation/contact');

        function attach(task, id) {
            return request.client().post(`/${task}`, { data: { id: id } });
        }

        return {
            fetch: request.fetch,
            search: request.search,
            attach: attach,
            isStatusCompleted: request.isStatusCompleted
        }
    }

    function contact({ task }) {

        const unkownTitle = "نا مشخص";

        const [user, setUser] = useState(undefined);

        const request = clientRequest();

        const [showModal, setShowModal] = useState(false);

        useEffect(function () {
            request.fetch(task)
                .then(function (response) {
                    if (request.isStatusCompleted(response)) {
                        setUser(response.data);
                    }
                })
                .catch(function (error) { });
        }, []);

        function add(item) {
            return new Promise(function (resolve, reject) {
                request.attach(task, item.id)
                    .then(function (response) {
                        if (request.isStatusCompleted(response)) {
                            resolve(true);
                            setUser(item);
                        }
                    }).catch(function (error) { resolve(false); });
            });
        }

        return html`<div className="flex items-center gap-2 px-4">
                        <div className="shrink-0 flex items-center justify-center border rounded-full size-[30px] bg-background">
                        </div>
                        <div className="flex flex-col space-y-0.5">
                            <p className="text-xs text-muted-foreground font-normal">
                                از
                            </p> 
                            <div className="flex items-center gap-2">
                                <p className="font-medium text-2sm text-foreground hover:text-primary" onClick=${function () { setShowModal(true); }} style=${{ cursor: "pointer" }}>${user === undefined ? unkownTitle : user.name}</p>
                            </div>
                        </div>
                        <${modal} status="${showModal}" setStatus="${setShowModal}" request="${request}" add="${add}" />
                    </div>`;
    }

    if (typeof global.contact === 'undefined') {
        global.contact = contact;
    }

})(React, renderer, request, modal, window)