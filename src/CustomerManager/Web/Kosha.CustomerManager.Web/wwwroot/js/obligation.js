"use strict";
(function (react, reactDom, html, tag, contact, action, category, tokenProvider, taskProvider) {

    document.addEventListener("DOMContentLoaded", function () {

        const strictMode = react.StrictMode;

        function App() {
            return html`<${tokenProvider}>
                            <${taskProvider}>
                                <div className="flex flex-col items-stretch min-h-[calc(100vh-6rem)]">
                                    <div className="px-4">
                                        <div className="flex items-center flex-wrap justify-between pt-3">
                                            <${category} />
                                        </div>
                                        <div className="py-3">
                                            <span className="text-lg text-foreground text-semibold">
                                                عنوان
                                            </span>
                                        </div>
                                        <${tag} />
                                    </div>
                                    <div className="shrink-0 bg-border h-px w-full mt-4 mb-3"></div>
                                    <div className="px-4">
                                        <${contact} />
                                        <${action} />
                                    </div>
                                </div>
                            </${taskProvider}>
                        </${tokenProvider}>`;
        }

        const root = reactDom.createRoot(
            document.querySelector('#react-element')
        );

        root.render(html`<${strictMode}><${App}/></${strictMode}>`);

    });

})(React, ReactDOM, renderer, tag, contact, action, category, tokenProvider, taskProvider)