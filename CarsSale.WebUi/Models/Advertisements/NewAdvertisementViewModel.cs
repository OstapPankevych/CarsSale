using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using CarsSale.DataAccess.DTO;

namespace CarsSale.WebUi.Models.Advertisements
{
    public class NewAdvertisementViewModel
    {
        [Range(0, 10000)]
        [Required]
        public int EngineVolume { get; set; }

        [Required(ErrorMessage = "Please select at least one fuel type")]
        public int[] Fuels { get; set; }

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