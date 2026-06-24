"use strict";
(function (react, reactDom, html, details, tokenProvider, taskProvider) {

    document.addEventListener("DOMContentLoaded", function () {

        const strictMode = react.StrictMode;

        function App() {
            return html`<${tokenProvider}>
                            <${taskProvider}>
                                <${details} />
                            </${taskProvider}>
                        </${tokenProvider}>`;
        }

        const root = reactDom.createRoot(
            document.querySelector('#react-element')
        );

        root.render(html`<${strictMode}><${App}/></${strictMode}>`);

    });

})(React, ReactDOM, renderer, details, tokenProvider, taskProvider)