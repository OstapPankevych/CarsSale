const carsSale = (() => {
    function Urls() {
        this.searchAdvertisements = "/Home/Search";
    }

    function ContentLoader() {
        let contentUrl;
        let opts;

        this.init = (url, options = {
            method,
            contentType,
            dataType,
            success,
            error,
            complete,
            beforeSend
        }) => {
            contentUrl = url;
            opts = options;
        };

        this.loadContent = (data) => {
            const options = Object.assign({
                    url: contentUrl,
                    data: data
                },
                opts);
            $.ajax(options);
        };
    };

    return {
        contentLoader: new ContentLoader(),
        urls: new Urls()
    }
})();