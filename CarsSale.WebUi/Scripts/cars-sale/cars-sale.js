var carsSale = (() => {
    var urls = {
        search: "/Advertisement/Search"
    };

    var loader = {
        show: () => { $(".modal").show(); },
        hide: () => { $(".modal").hide(); }
    };

    var errorPlacement = {
        show: (text) => {
            $("#error-placement .message").text(text);
            $("#error-placement").show();
        },
        hide: () => { $("#error-placement").hide(); }
    };

    return { urls, loader, errorPlacement };
})();

$(document).ajaxError(() => {
    carsSale.loader.hide();
    carsSale.errorPlacement.show("Ajax request error. Please contact to support.");
});

$.validator.setDefaults({
    onkeyup: (element, e) => {
        if ($(element).attr('data-val-remote-url')) return false;
        $(element).validate();
        return $(element).valid();
    }
});