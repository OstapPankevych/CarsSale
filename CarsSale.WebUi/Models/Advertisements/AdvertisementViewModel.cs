using System.Collections.Generic;
using CarsSale.WebUi.Models.Vehicl;

namespace CarsSale.WebUi.Models.Advertisements
{
    public class AdvertisementViewModel
    {
        public AdvertisementViewModel() { }

        public UserViewModel User { get; set; }
        public RegionViewModel Region { get; set; }
        public BrandViewModel Brand { get; set; }
        public VehiclTypeViewModel VehiclType { get; set; }
        public TransmissionTypeViewModel TransmissionType { get; set; }
        public int EngineVolume { get; set; }
        public FuelViewModel[] FuelOptions { get; set; }
  
        public IEnumerable<RegionViewModel> RegionOptions { get; set; }
        public IEnumerable<BrandViewModel> BrandOptions { get; set; }
        public IEnumerable<VehiclTypeViewModel> VehiclTypeOptions { get; set; }
        public IEnumerable<TransmissionTypeViewModel> TransmissionTypeOptions { get; set; }      
    }
}