"use strict";
(function (axios, global) {

    function create(baseUrl) {

        var client = axios.create({ baseURL: baseUrl });

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

})(axios, window);