"use strict";
(function (react, html, keenModal, global) {

    const useState = react.useState;

    const useEffect = react.useEffect;

    function modal({ status, setStatus, request, add }) {

        const [modal, setModal] = useState(undefined);

        const unique = randomString(32, '0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ');

        const [showDropdown, setShowDropdown] = useState(false);

        const [searchValue, setSearchValue] = useState('');

        const [items, setItems] = useState([]);

        useEffect(function () {
            if (modal === undefined) {
                const modalElement = document.querySelector(`#${modalIdentifier()}`);
                if (modalElement !== undefined) {
                    const instance = getKeenElement(keenModal, modalElement);
                    setModal(instance);
                    instance.on('hidden', function () { setStatus(false); });
                }
            }

            if (
                modal !== undefined &&
                status
            ) {
                setShowDropdown(false);
                setSearchValue('');
                setItems([]);
                modal.show();
            }
        }, [status, modal]);

        function getKeenElement(keen, element) {
            keen.init();
            keen.createInstances();
            return keen.getOrCreateInstance(element);
        }

        useEffect(function () { setShowDropdown(items.length !== 0); }, [items]);

        useEffect(function () {
            setItems([]);
            if (status && searchValue !== '') {
                if (!items.some(function (item) { return item.name === searchValue })) {
                    request.search(searchValue)
                        .then(function (response) {
                            if (request.isStatusCompleted(response)) {
                                setItems(response.data);
                            }
                        });
                }
            }
        }, [searchValue]);

        function randomString(length, chars) {
            var result = '';
            for (var i = length; i > 0; --i) result += chars[Math.floor(Math.random() * chars.length)];
            return result;
        }

        function modalIdentifier() {
            return `modal-${unique}`;
        }

        function selected(id) {
            var item = items.filter(function (item) { return item.id === id });
            if (item.length === 1) {
                item = item[0];
                setSearchValue(item.name);
                add(item)
                    .then(function (response) {
                        if (response) {
                            modal.hide();
                        }
                    });

            }
        }

        function dropdownClasses() {
            var result = ["kt-menu-dropdown", "kt-menu-default", "flex", "flex-col", "mt-1", "w-full", "max-w-[600px]"];

            if (!showDropdown)
                result.push("hidden");

            return result.join(" ");
        }

        return html`<div className="kt-modal kt-modal-center" data-kt-modal="true" id="${modalIdentifier()}">
                        <div className="kt-modal-content max-w-[600px]" style=${{ backgroundColor: "transparent", border: "none", boxShadow: "none" }}>
                            <div className="kt-modal-body">
                                <input type="text" className="kt-input" onChange=${function (e) { setSearchValue(e.target.value); }} value="${searchValue}" />
                                <div className="${dropdownClasses()}">
                                    ${items.map(function (item, index) { return html`<${dropdownItem} id="${item.id}" title="${item.name}" selected="${selected}" key="${index}" />` })}
                                </div>
                            </div>
                        </div>
                    </div>`
    }


    function dropdownItem({ id, title, selected }) {
        return html`<div className="kt-menu-item">
                        <p className="kt-menu-link" onClick=${function () { selected(id); }}>
                            <span className="kt-menu-title">
                                ${title}
                            </span>
                        </p>
                    </div>`;
    }

    if (typeof global.modal === 'undefined') {
        global.modal = modal;
    }

})(React, renderer, KTModal, window);