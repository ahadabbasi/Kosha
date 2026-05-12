(function (react, html, modal, global) {

    const useState = react.useState;

    const useEffect = react.useEffect;

    function suggest({ status, setStatus, request, add }) {

        const [showDropdown, setShowDropdown] = useState(false);

        const [searchValue, setSearchValue] = useState('');

        const [items, setItems] = useState([]);

        const classes = ['bg-transparent', 'border-none', 'shadow-none'];

        useEffect(function () {
            if (status) {
                setShowDropdown(false);
                setSearchValue('');
                setItems([]);
            }
        }, [status]);

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

        function selected(id) {
            var item = items.filter(function (item) { return item.id === id });
            if (item.length === 1) {
                item = item[0];
                setSearchValue(item.name);
                add(item)
                    .then(function (response) {
                        if (response) {
                            setStatus(false);
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

        return html`<${modal} status="${status}" setStatus="${setStatus}" classes="${classes}">
                        <input type="text" className="kt-input" onChange=${function (e) { setSearchValue(e.target.value); }} value="${searchValue}" />
                        <div className="${dropdownClasses()}">
                            ${items.map(function (item, index) { return html`<${dropdownItem} id="${item.id}" title="${item.name}" selected="${selected}" key="${index}" />` })}
                        </div>
                    </${modal}>`
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

    if (typeof global.suggest === 'undefined') {
        global.suggest = suggest;
    }

})(React, renderer, modal, window);