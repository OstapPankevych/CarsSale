$(() => {
    var error = $("#error-placement");
    var searching = $("#searching");
    var searchResult = $("#search-result");

    var onSearchSuccess = (data) => {
        error.hide();
        searching.hide();
        searchResult.html(data);
        searchResult.show();
    };

    var onSearchError = (error) => {
        error.text("Search Error: " + error);
        searching.hide();
        error.show();
    };

    var onValidationError = (error) => {
        error.text("Search Form: " + error.text());
    };

    var onValidationSuccess = () => { error.hide(); };

    var onStartSearch = () => {
        error.hide();
        searchResult.hide();
        searching.show();
    };

    carsSale.modules.searcher.init("search-form", onStartSearch,
        onSearchSuccess, onSearchError,
        onValidationError, onValidationSuccess);
});