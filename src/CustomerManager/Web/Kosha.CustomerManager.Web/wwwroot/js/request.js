"use strict";
(function (react, axios, tokenContext, global) {

    function create(baseUrl) {

        var config = { baseURL: baseUrl };      

        var { state } = react.useContext(tokenContext);

        if (state !== undefined) {
            config = {
                ...config,
                headers: { Authorization: `Bearer ${state}` }
            };
        }

        var client = axios.create(config);

        function getClient() {
            if (client === undefined)
                throw Error("");
            return client;
        }

        function isStatusCompleted(response) {
            return response.status === 200;
        };

        function search(name) {
            return getClient().get('', { params: { name: name } });
        }

        function fetch(task) {
            return getClient().get(`/${task}`);
        }

        return {
            client: getClient,
            search: search,
            isStatusCompleted: isStatusCompleted,
            fetch: fetch
        }
    }

    if (typeof global.request === 'undefined') {
        global.request = {
            create: create
        };
    }

})(React, axios, tokenContext, window);