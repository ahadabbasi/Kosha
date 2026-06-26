"use strict";
(function (react, html, axios, taskValue, tag, contact, action, category, global) {

    const useState = react.useState;

    const useEffect = react.useEffect;

    const fragment = react.Fragment;

    function clientRequest() {

        const request = axios.create('/api/obligation/task');

        return {
            fetch: request.fetch,
            isStatusCompleted: request.isStatusCompleted
        }
    }

    function details() {

        const task = taskValue();

        const request = clientRequest();

        const [message, setMessage] = useState(undefined);

        const unknownTitle = "عنوان";

        useEffect(function () {
            request.fetch(task)
                .then(function (response) {
                    if (request.isStatusCompleted(response)) {
                        setMessage(response.data);
                    }
                });
        }, []);

        function information() {
            return message === undefined ?
                html`<${fragment}></${fragment}>` : 
                html`<div className="bg-muted/50 p-4 rounded-lg mt-4">
                        ${message.information.map(function (value, index) { return html`<${informationItem} item="${value}" key="${index}" index="${index}" />` })}
                    </div>`
        }

        function informationItem({ item, index }) {
            return html`<${fragment}>
                            ${index !== 0 ? html`<div className="border-t border-input border-dashed mb-3 mt-3"></div>` : html`<${fragment}></${fragment}>`}
                            <div className="flex items-center justify-between flex-wrap gap-2">
                                <span className="text-secondary-foreground">
                                    ${item.type}
                                </span>
                                <span className="text-secondary-foreground">
                                    ${item.value}
                                </span>
                            </div>
                        </${fragment}>`
        }

        return html`<div className="flex flex-col grow items-stretch">
                        <div className="px-4">
                            <div className="flex items-center flex-wrap justify-between pt-3">
                                <${category} />
                            </div>
                            <div className="py-3">
                                <span className="text-lg text-foreground text-semibold">
                                    ${message === undefined ? unknownTitle : message.title}    
                                </span>
                            </div>
                            <${tag} />
                        </div>
                        <div className="shrink-0 bg-border h-px w-full mt-4 mb-3"></div>
                        <div className="px-4 grow flex flex-col">
                            <${contact} />
                            <${information} />
                            <${action} />
                        </div>
                    </div>`;
    }

    if (typeof global.details === 'undefined') {
        global.details = details;
    }

})(React, renderer, request, task, tag, contact, action, category, window)