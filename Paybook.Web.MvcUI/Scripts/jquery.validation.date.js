$(function () {
    $.validator.methods.date = function (value, element) {
        if ($.browser.webkit) {
            var d = new Date();
            console.log(this.optional(element));
            console.log(/Invalid|NaN/.test(new Date(d.toLocaleDateString(value))));
            return this.optional(element) || !/Invalid|NaN/.test(new Date(d.toLocaleDateString(value)));
        }
        else {
            console.log(this.optional(element));
            console.log(/Invalid|NaN/.test(new Date(value)));
            return this.optional(element) || !/Invalid|NaN/.test(new Date(value));
        }
    };
});