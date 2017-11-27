using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CarsSale.DataAccess.DTO;

namespace CarsSale.WebUi.Models.Advertisement
{
    public class SearchAdvertismentViewModel
    {
        public class EngineViewModel
        {
            [Range(1, 10000)]
            public int Volume { get; set; }
        }

        public EngineViewModel EngineFrom { get; set; }
        public EngineViewModel EngineTo { get; set; }
        public IEnumerable<Fuel> Fuels { get; set; }
        public Region Region { get; set; }
        public Brand Brand { get; set; }
        public VehiclType VehiclType { get; set; }
        public TransmissionType TransmissionType { get; set; }

        public IEnumerable<Fuel> FuelOptions { get; set; }
        public IEnumerable<Region> RegionOptions { get; set; }
        public IEnumerable<Brand> BrandOptions { get; set; }
        public IEnumerable<VehiclType> VehiclTypeOptions { get; set; }
        public IEnumerable<TransmissionType> TransmissionTypeOptions { get; set; }
    }
}