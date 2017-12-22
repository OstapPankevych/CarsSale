using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CarsSale.DataAccess.DTO;

namespace CarsSale.WebUi.Models.Advertisement
{
    public class SearchAdvertismentViewModel
    {
        [Range(1, 10000)]
        public int? EngineFrom { get; set; }

        [Range(1, 10000)]
        public int? EngineTo { get; set; }

        public int[] FuelIds { get; set; }
        public int? RegionId { get; set; }
        public int? BrandId { get; set; }
        public int? VehiclTypeId { get; set; }
        public int? TransmissionTypeId { get; set; }

        public IEnumerable<Fuel> FuelOptions { get; set; }
        public IEnumerable<Region> RegionOptions { get; set; }
        public IEnumerable<Brand> BrandOptions { get; set; }
        public IEnumerable<VehiclType> VehiclTypeOptions { get; set; }
        public IEnumerable<TransmissionType> TransmissionTypeOptions { get; set; }
    }
}