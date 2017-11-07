carsSale.modules = {};

carsSale.modules.searcher = {
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
};