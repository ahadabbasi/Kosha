"use strict";
(function (react, htm, global) {

    const html = htm.bind(react.createElement);

    if (typeof global.renderer === 'undefined') {
        global.renderer = html;
    }

})(React, htm, window)