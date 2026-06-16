"use strict";
(function (react, html, global) {
    const taskContext = react.createContext();

    function taskProvider({ children }) {

        var task = undefined;

        var taskElement = document.getElementById('Task');

        if (taskElement !== undefined && taskElement !== null) {
            task = taskElement.getAttribute('value');
        }

        const [state, dispatch] = react.useReducer(function () { }, task);

        return html`<${taskContext.Provider} value=${{ state, dispatch }}>${children}</${taskContext.Provider}>`
    }

    function task() {
        const { state } = react.useContext(taskContext);

        return state;
    }

    if (typeof global.taskProvider === 'undefined') {
        global.taskProvider = taskProvider;
    }

    if (typeof global.task === 'undefined') {
        global.task = task;
    }
})(React, renderer, window)