carsSale.advertisments = (() => ({
    search: ({ regionId, brandId, vehiclTypeId, transmissionId, fuelIds, volumeFrom, volumeTo }, success, error) => {
        var data = {
            Region: regionId ? { Id: regionId } : null,
            Brand: brandId ? { Id: brandId } : null,
            VehiclType: vehiclTypeId ? { Id: vehiclTypeId } : null,
            TransmissionType: transmissionId ? { Id: transmissionId } : null,
            Fuels: fuelIds.map((id) => ({ Id: id })),
            EngineVolumeFrom: volumeFrom,
            EngineVolumeTo: volumeTo
        };

        var options = {
            url: carsSale.urls.search,
            data: { searchViewModel: data },
            success: success,
            error: error
        };
        $.post(options);
    }
}))();

$(() => {
    var searchResult = $("#search-result");
    var emptySearchResult = $("#search-empty");
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
        carsSale.loader.hide();
        if (data) {
            searchResult.html(data);
            emptySearchResult.hide();
            searchResult.show();
        } else {
            searchResult.hide();
            emptySearchResult.show();
        }
    };

    $("button#search").click(() => {
        if (!isFormValid()) return;
        carsSale.errorPlacement.hide();
        carsSale.loader.show();
        carsSale.advertisments.search(getSearchOptions(), onSearchSuccess);
    });
});