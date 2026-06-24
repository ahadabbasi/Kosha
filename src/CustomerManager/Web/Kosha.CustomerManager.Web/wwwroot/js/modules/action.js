"use strict";
(function (react, renderer, axios, taskValue, global) {

    const useState = react.useState;

    const useEffect = react.useEffect;

    function clientRequest() {

        const request = axios.create('/api/obligation/action');

        function add(task, comment) {
            return request.client().post(`/${task}`, { data: { comment: comment } });
        }

        return {
            fetch: request.fetch,
            add: add,
            isStatusCompleted: request.isStatusCompleted
        }
    }

    function action() {

        const task = taskValue();

        const request = clientRequest();

        const [messages, setMessages] = useState([]);

        const [comment, setComment] = useState('');

        useEffect(function () {
            request.fetch(task)
                .then(function (response) {
                    if (request.isStatusCompleted(response)) {
                        setMessages(response.data);
                    }
                });
        }, []);

        function save() {
            if(commentIsValid())
                request.add(task, comment)
                    .then(function (response) {
                        if (request.isStatusCompleted(response)) {
                            setMessages(messages.concat([response.data]));
                            setComment('');
                        }
                    });
        }

        function buttonClasses() {
            var result = ["kt-btn", "kt-btn-icon", "size-9", "rounded-xl", "transition-all"];

            if (commentIsValid())  {
                result.push('kt-btn-primary');
                result.push('opacity-100');
            } else {
                result.push('kt-btn-secondary');
                result.push('opacity-50');
            }

            return result.join(' ');
        }

        function commentIsValid() {
            return comment.trim() !== '';
        }

        function buttonDisable() {
            return commentIsValid() ? '' : 'disabled=""';
        }

        return renderer`<div className="container-fluid flex flex-col">
                            <div className="flex-1 flex flex-col rounded-lg bg-background overflow-hidden">
                                <div id="chat-messages" className="flex flex-col h-full overflow-y-auto space-y-3.5 flex-1 overflow-y-auto px-6 py-4">
                                    ${messages.map(function (item, index) { return renderer`<${userAction} message="${item}" key="${index}" />` })}
                                </div>
                                <div id="chat-starter" className="p-4 pb-6">
                                    <div className="max-w-3xl mx-auto w-full">
                                        <div className="flex flex-col w-full p-0">
                                            <div className="w-full">
                                                <div className="relative">
                                                    <div className="relative flex flex-col gap-2 bg-background transition-all rounded-2xl border border-border shadow-lg p-4">
                                                        <input type="text" className="flex-1 border-0 bg-transparent shadow-none focus-visible:ring-0 focus-visible:ring-ring/30 focus-visible:border-ring focus-visible:outline-none placeholder:text-muted-foreground h-auto px-0 text-sm py-2" placeholder="پیام خود را بنویسید..." onKeyDown=${function (e) { setComment(e.target.value); }} />
                                                        <div className="flex items-center justify-between mt-2">
                                                            <div className="ms-auto flex items-center gap-2">
                                                                <button className="${buttonClasses()}" onClick=${save} ${buttonDisable()}>
                                                                     <i className="ki-outline ki-plus-squared">
                                                                     </i>
                                                                </button>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>`;
    }

    function userAction({ message }) {
        return renderer`<div className="flex flex-col gap-1 flex-1">
                            <div className="rounded-2xl px-5 py-3.5 text-sm shadow-sm relative group bg-muted/50 text-foreground max-w-[90%] rounded-bl-sm">
                                <div className="text-sm">${message.comment}</div>
                                <span className="text-xs text-muted-foreground px-1">${message.registered}</span>
                            </div>
                        </div>`;
    }

    if (typeof global.action === 'undefined') {
        global.action = action;
    }

})(React, renderer, request, task, window)