using System.Collections.Generic;
using CarsSale.WebUi.Models.Vehicl;

namespace CarsSale.WebUi.Models.Advertisements
{
    public class Advertisement
    {
        public Region Region { get; set; }
        public Brand Brand { get; set; }
        public VehiclType VehiclType { get; set; }
        public TransmissionType TransmissionType { get; set; }
        public int EngineVolume { get; set; }

        public IEnumerable<Region> RegionOptions { get; set; }
        public IEnumerable<Brand> BrandOptions { get; set; }
        public IEnumerable<VehiclType> VehiclTypeOptions { get; set; }
        public IEnumerable<TransmissionType> TransmissionTypeOptions { get; set; }
        public Fuel[] FuelOptions { get; set; }
    }
}