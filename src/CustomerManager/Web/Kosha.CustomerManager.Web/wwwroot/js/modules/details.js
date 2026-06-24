"use strict";
(function (react, html, axios, taskValue, tag, contact, action, category, global) {

    const useState = react.useState;

    const useEffect = react.useEffect;

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

        const [messages, setMessages] = useState(undefined);

        const unknownTitle = "عنوان";

        useEffect(function () {
            request.fetch(task)
                .then(function (response) {
                    if (request.isStatusCompleted(response)) {
                        setMessages(response.data);
                    }
                });
        }, []);

        return html`<div className="flex flex-col grow items-stretch">
                        <div className="px-4">
                            <div className="flex items-center flex-wrap justify-between pt-3">
                                <${category} />
                            </div>
                            <div className="py-3">
                                <span className="text-lg text-foreground text-semibold">
                                    ${messages === undefined ? unknownTitle : messages.title}    
                                </span>
                            </div>
                            <${tag} />
                        </div>
                        <div className="shrink-0 bg-border h-px w-full mt-4 mb-3"></div>
                        <div className="px-4 grow flex flex-col">
                            <${contact} />
                            <${action} />
                        </div>
                    </div>`;
    }

    if (typeof global.details === 'undefined') {
        global.details = details;
    }

})(React, renderer, request, task, tag, contact, action, category, window)