"use strict";
(function (react, reactDom, html, tag, contact, action) {

    const strictMode = react.StrictMode;

    function App() {

        return html`<div className="flex flex-col items-stretch min-h-[calc(100vh-6rem)]">
                        <div className="px-4">
                            <div className="py-3">
                                <span className="text-lg text-foreground text-semibold">
                                    عنوان
                                </span>
                            </div>
                            <${tag} task="b57a2994-f061-4861-84da-28eba819bdeb" />
                        </div>
                        <div className="shrink-0 bg-border h-px w-full mt-4 mb-3"></div>
                        <div className="px-4">
                            <${contact} task="b57a2994-f061-4861-84da-28eba819bdeb" />
                            <${action} task="b57a2994-f061-4861-84da-28eba819bdeb" />
                        </div>
                    </div>`;
    }

    const root = reactDom.createRoot(
        document.querySelector('#react-element')
    );

    root.render(html`<${strictMode}><${App}/></${strictMode}>`);

})(React, ReactDOM, renderer, tag, contact, action)