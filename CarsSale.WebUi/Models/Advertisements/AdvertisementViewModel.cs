using CarsSale.DataAccess;
using CarsSale.DataAccess.DTO;
using CarsSale.DataAccess.Entities;

namespace CarsSale.WebUi.Models.Advertisements
{
    public class AdvertisementViewModel
    {
        public ApplicationUser User { get; set; }
        public Region Region { get; set; }
        public Advertisement Advertisement { get; set; }
    }
}