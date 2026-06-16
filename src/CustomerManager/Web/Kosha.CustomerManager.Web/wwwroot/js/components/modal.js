"use strict";
(function (react, html, keenModal, global) {

    const useState = react.useState;

    const useEffect = react.useEffect;

    function modal({ status, setStatus, classes = [], children }) {

        const [modal, setModal] = useState(undefined);

        const unique = randomString(32, '0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ');

        useEffect(function () {
            if (modal === undefined) {
                const modalElement = document.getElementById(modalIdentifier());
                if (modalElement !== undefined) {
                    const instance = getKeenElement(keenModal, modalElement);
                    setModal(instance);
                    instance.on('hidden', function () { setStatus(false); });
                }
            }

            if (modal !== undefined)
                if (status)
                    modal.show();
                else
                    modal.hide();
        }, [status, modal]);

        function getKeenElement(keen, element) {
            keen.init();
            keen.createInstances();
            return keen.getOrCreateInstance(element);
        }

        function randomString(length, chars) {
            var result = '';
            for (var i = length; i > 0; --i)
                result += chars[Math.floor(Math.random() * chars.length)];
            return result;
        }

        function modalClasses() {
            var result = ['kt-modal-content', 'max-w-[600px]'];

            if (classes !== undefined && classes !== null && Array.isArray(classes))
                classes.forEach(function (item) { result.push(item); });

            return result.join(' ');

        }

        function modalIdentifier() {
            return `modal-${unique}`;
        }

        return html`<div className="kt-modal kt-modal-center" data-kt-modal="true" id="${modalIdentifier()}">
                        <div className="${modalClasses()}">
                            <div className="kt-modal-body">
                                ${children}
                            </div>
                        </div>
                    </div>`
    }

    if (typeof global.modal === 'undefined') {
        global.modal = modal;
    }

})(React, renderer, KTModal, window);