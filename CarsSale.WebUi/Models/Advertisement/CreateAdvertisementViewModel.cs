using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Web;
using CarsSale.DataAccess.DTO;
using CarsSale.WebUi.Attributes;

namespace CarsSale.WebUi.Models.Advertisement
{
    public class CreateAdvertisementViewModel
    {
        [Range(1, 10000, ErrorMessage = "The engine volume must be between 1 and 10'000")]
        [Required]
        public int EngineVolume { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "The price must be bigger than 0")]
        [Required]
        public int Price { get; set; }

        [AtLeastCollectionCount(Count = 1, ErrorMessage = "Please select at least one fuel type")]
        public int[] Fuels { get; set; }

        [Required(ErrorMessage = "Photo is required!")]
        [ValidateHttpPostedFileBaseAsImage(ErrorMessage = "The photo must be an image!")]
        public HttpPostedFileBase Image { get; set; }

        [Required(ErrorMessage = "Please select region!")]
        public int RegionId { get; set; }

        [Required(ErrorMessage = "Please select brand!")]
        public int BrandId { get; set; }

        [Required(ErrorMessage = "Please select vehicl type!")]
        public int VehiclTypeId { get; set; }

        [Required(ErrorMessage = "Please select transmission type!")]
        public int TransmissionTypeId { get; set; }

        [Required(ErrorMessage = "Please select currency!")]
        public int CurrencyId { get; set; }

        public IEnumerable<Fuel> FuelOptions { get; set; }
        public IEnumerable<Region> RegionOptions { get; set; }
        public IEnumerable<Brand> BrandOptions { get; set; }
        public IEnumerable<VehiclType> VehiclTypeOptions { get; set; }
        public IEnumerable<TransmissionType> TransmissionTypeOptions { get; set; }
        public IEnumerable<Currency> CurrencyOptions { get; set; }
    }
}