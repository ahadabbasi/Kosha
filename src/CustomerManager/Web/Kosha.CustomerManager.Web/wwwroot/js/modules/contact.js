"use strict";
(function (react, html, axios, suggest, modal, taskValue, global) {

    const useState = react.useState;

    const useEffect = react.useEffect;

    const fragment = react.Fragment;

    function clientRequest() {

        const request = axios.create('/api/obligation/contact');

        function attach(task, id) {
            return request.client().post(`/${task}`, { id: id });
        }

        function information(task) {
            return request.client().get(`/information/${task}`);
        }

        return {
            fetch: request.fetch,
            search: request.search,
            attach: attach,
            information: information,
            isStatusCompleted: request.isStatusCompleted
        }
    }

    function contact() {

        const [task] = useState(taskValue());

        const unkownTitle = "نا مشخص";

        const [user, setUser] = useState(undefined);

        const request = clientRequest();

        const [showSuggest, setShowSuggest] = useState(false);

        const [showInformation, setShowInformation] = useState(false);

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
        
        function informationClick() {

            if (user !== undefined) {
                setShowInformation(true);
            }

        }

        return html`<div className="flex items-center gap-2 px-4">
                        <div className="shrink-0 flex items-center justify-center border rounded-full size-[30px] bg-background" onClick="${function () { informationClick(); } }">
                            <${customerInformation} task="${task}" status="${showInformation}" setStatus="${setShowInformation}" request="${request}" />
                        </div>
                        <div className="flex flex-col space-y-0.5">
                            <p className="text-xs text-muted-foreground font-normal">
                                از
                            </p> 
                            <div className="flex items-center gap-2">
                                <p className="font-medium text-2sm text-foreground hover:text-primary" onClick=${function () { setShowSuggest(true); }} style=${{ cursor: "pointer" }}>
                                    ${user === undefined ? unkownTitle : user.name}
                                </p>
                            </div>
                        </div>
                        <${suggest} status="${showSuggest}" setStatus="${setShowSuggest}" request="${request}" add="${add}" />
                    </div>`;
    }

    function customerInformation({ task, status, setStatus, request }) {

        const [customer, setCustomer] = useState(undefined);

        useEffect(function () {
            if (status) {
                request.information(task)
                    .then(function (response) {
                        if (request.isStatusCompleted(response)) {
                            setCustomer(response.data);
                        }
                    })
            }
        }, [status]);

        function contact({ item }) {
            return html`<${fragment}>
                            <div className="flex items-center justify-between flex-wrap mb-3.5 gap-2">
                                <span className="text-xs text-secondary-foreground">
                                    ${item.type}
                                </span>
                                <span className="text-xs text-secondary-foreground">
                                    ${item.value}
                                </span>
                            </div>
                            <div className="border-t border-input border-dashed"></div>
                        </${fragment}>`
        }

        function information() {
            return customer.information != null ?
                customer.information.map(
                    function (value, index) {
                        return html`<${contact} item="${value}" key="${index}" />`
                    }
                ) :
                html`<${fragment} />`;
        }

        function body() {
            return customer !== undefined ?
                html`<div className="kt-card border-0 shadow-none">
                        <div className="kt-card-content grid gap-7 py-7.5">
                            <div className="grid place-items-center gap-4">
                                <div className="flex justify-center items-center size-14 rounded-full ring-1 ring-input bg-accent/60">
                                
                                </div>
                                <div className="grid place-items-center">
                                    <a
                                        className="text-base font-medium text-mono hover:text-primary mb-px"
                                        href="#"
                                    >
                                        ${customer.name} ${customer.family}
                                    </a>
                                </div>
                            </div>
                            <div className="grid">
                                <${information} />
                            </div>
                        </div>
                    </div>` :
                html`<${fragment} />`;
        }

        return html`<${modal} status="${status}" setStatus="${setStatus}" classes="${[]}" >
                        <${body} />
                    </${modal}>`
    }


    if (typeof global.contact === 'undefined') {
        global.contact = contact;
    }

})(React, renderer, request, suggest, modal, task, window)