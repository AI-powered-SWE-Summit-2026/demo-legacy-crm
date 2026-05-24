window.jQuery = window.$ = function (callback) {
    if (typeof callback === 'function') {
        callback();
    }

    return {
        on: function () { return this; },
        ready: function (fn) { if (typeof fn === 'function') { fn(); } return this; }
    };
};
