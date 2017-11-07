$(() => {
    var searching = $(".modal");
    var searchResult = $("#search-result");

    var getCheckedValues = (checkBoxContainerId) => {
        var fuels = $(checkBoxContainerId + " input:checked")
            .map(() => $(this).val());
        if (!fuels.length) return [];
        return fuels.get();
    };

    var getSearchOptions = () => {
        var regionId = $("#region").val();
        var brandId = $("#brand").val();
        var vehiclTypeId = $("#vehicl-type").val();
        var transmissionId = $("#transmission").val();
        var fuelIds = getCheckedValues("#fuels");
        var volumeFrom = $("#search-volume-from").val();
        var volumeTo = $("#search-volume-to").val();

        return {
            regionId,
            brandId,
            vehiclTypeId,
            transmissionId,
            fuelIds,
            volumeFrom,
            volumeTo
        }
    };

    var validationOptions = {
        rules: {
            searchVolumeFrom: {
                required: false,
                number: true,
                range: () => [$("#engine-volume-search-form").attr("min"), $("#search-volume-to").val() || $("#engine-volume-search-form").attr("max")]
            },
            searchVolumeTo: {
                required: false,
                number: true,
                range: () => [$("#search-volume-from").val() || $("#engine-volume-search-form").attr("min"), $("#engine-volume-search-form").attr("max")]
            }
        },
        errorPlacement: error => { $("#error-placement .message").text("Search Form: " + error.text()); },
        success: () => { $("#error-placement").hide(); }
    };

    $("#engine-volume-search-form").validate(validationOptions);

    $("#search-volume-from").blur(() => $("#engine-volume-search-form").validate());
    $("#search-volume-to").blur(() => $("#engine-volume-search-form").validate());

    var isFormValid = () => {
        var validator = $("#engine-volume-search-form").validate();
        return validator.valid();
    };

    var onSearchSuccess = (data) => {
        $("#error-placement").hide();
        searching.hide();
        searchResult.html(data);
        searchResult.show();
    };

    $("button#search").click(() => {
        if (!isFormValid()) return;
        $("#error-placement").hide();
        searchResult.hide();
        searching.show();
        carsSale.modules.searcher.search(getSearchOptions(), onSearchSuccess);
    });
});