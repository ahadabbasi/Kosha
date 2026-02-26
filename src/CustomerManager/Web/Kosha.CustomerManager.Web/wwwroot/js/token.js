"use strict";
(function (react, html, global) {
    const tokenContext = react.createContext()

    function tokenProvider({ children }) {

        var token = undefined;

        var tokenElement = document.getElementById('Token');

        if (tokenElement !== undefined && tokenElement !== null) {
            token = tokenElement.getAttribute('value');
        }

        const [state, dispatch] = react.useReducer(function () { }, token);

        return html`<${tokenContext.Provider} value=${{ state, dispatch }}>${children}</${tokenContext.Provider}>`
    }

    if (typeof global.tokenProvider === 'undefined') {
        global.tokenProvider = tokenProvider
    }

    if (typeof global.tokenContext === 'undefined') {
        global.tokenContext = tokenContext
    }
})(React, renderer, window)