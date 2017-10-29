$(function () {
    var getSelectedValue = function (elementId) {
        return $('select#' + elementId + ' option:selected').val();
    };

    var getCheckedValues = function (elementName) {
        var fuels = $('input[name=' + elementName + ']:checked').map(
            function () {
                return $(this).val();
            });
        if (!fuels.length) return [];
        return fuels.get();
    };

    var getValue = function (elementId) {
        return $("#" + elementId).val();
    };

    var validateSearchVolume = function () {
        var form = $("#engine-volume-search-form");
        var rule = {
            searchVolumeFrom: {
                required: false,
                number: true,
                range: [form.attr("min"), getValue("search-volume-to") || form.attr("max")]
            },
            searchVolumeTo: {
                required: false,
                number: true,
                range: [getValue("search-volume-from") || form.attr("min"), form.attr("max")]
            }
        };
        var validator = form.validate({ rules: rule });
        return validator.valid();
    };

    var validateSearchForm = function() {
        return validateSearchVolume();
    };

    $("#search-volume-from").blur(function () {
        validateSearchVolume();
    });

    $("#search-volume-to").blur(function () {
        validateSearchVolume();
    });

    var getSearchOptions = function () {
        var regionId = getSelectedValue('region');
        var brandId = getSelectedValue('brand');
        var vehiclTypeId = getSelectedValue('vehicl-type');
        var transmissionId = getSelectedValue('transmission');
        var fuels = getCheckedValues("fuel").map(function (id) {
            return { Id: id };
        });
        var engineFromVolume = getValue("search-volume-from");
        var engineToVolume = getValue("search-volume-to");
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

    var success = function (data) {
        $("div#search-result").html(data);
    };

    $("button#search").click(function () {
        if (!validateSearchForm()) return;
        var options = {
            url: carsSale.urls.search,
            data: { searchViewModel: getSearchOptions() },
            success
        };
        $.post(options);
    });
});