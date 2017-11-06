var carsSale = (() => {
    var urls = {
        search: "/Advertisement/Search"
    };

    return { urls };
})();

carsSale.modules = (() => {
    var Searcher = function() {
        var formId;
        var resultId;
        var errorPlacement;

        var getFormElement = (elementId) => {
            return elementId ? $("#" + formId + " " + elementId) : $("#" + formId);
        };

        var getCheckedValues = (checkBoxContainerId) => {
            var fuels = $("#" + formId + " " + checkBoxContainerId + " input:checked")
                .map(() => $(this).val());
            if (!fuels.length) return [];
            return fuels.get();
        };

        var validationOptions = {
            rules: {
                searchVolumeFrom: {
                    required: false,
                    number: true,
                    range: () => [getFormElement("#engine-volume-search-form").attr("min"), getFormElement("#search-volume-to").val() || getFormElement("#engine-volume-search-form").attr("max")]
                },
                searchVolumeTo: {
                    required: false,
                    number: true,
                    range: () => [getFormElement("#search-volume-from").val() || getFormElement("#engine-volume-search-form").attr("min"), getFormElement("#engine-volume-search-form").attr("max")]
                }
            },
            errorPlacement: (error) => {
                var errorPopup = $("#" + errorPlacement);
                $("#" + errorPlacement).text("Search Form: " + error.text());
                errorPopup.show();
            },
            success: () => {
                $("#" + errorPlacement).hide();
            }
        };

        var isFormValid = () => {
            var validator = getFormElement("#engine-volume-search-form").validate();
            return validator.valid();
        };

        var getSearchOptions = () => {
            var regionId = getFormElement("#region").val();
            var brandId = getFormElement("#brand").val();
            var vehiclTypeId = getFormElement("#vehicl-type").val();
            var transmissionId = getFormElement("#transmission").val();
            var fuels = getCheckedValues("#fuels").map(function (id) {
                return { Id: id };
            });
            var engineFromVolume = getFormElement("#search-volume-from").val();
            var engineToVolume = getFormElement("#search-volume-to").val();
            return {
                Region: regionId ? { Id: regionId } : null,
                Brand: brandId ? { Id: brandId } : null,
                VehiclType: vehiclTypeId ? { Id: vehiclTypeId } : null,
                TransmissionType: transmissionId ? { Id: transmissionId } : null,
                Fuels: fuels,
                EngineVolumeFrom: engineFromVolume,
                EngineVolumeTo: engineToVolume
            };
        };

        var onSearchVolumeBlur = () => getFormElement("#engine-volume-search-form").validate();

        var onSearchButtonClick = () => {
            if (!isFormValid()) return;
            var options = {
                url: carsSale.urls.search,
                data: { searchViewModel: getSearchOptions() },
                success: (data) => $("#" + resultId).html(data)
            };
            $.post(options);
        };

        this.init = (searchFormId, resultContainerId, errorPlacementId) => {
            formId = searchFormId;
            resultId = resultContainerId;
            errorPlacement = errorPlacementId;

            getFormElement("#engine-volume-search-form").validate(validationOptions);

            getFormElement("#search-volume-from").blur(onSearchVolumeBlur);
            getFormElement("#search-volume-to").blur(onSearchVolumeBlur);
            
            getFormElement("button#search").click(onSearchButtonClick);
        };
    };

    return { searcher: new Searcher() }
})();