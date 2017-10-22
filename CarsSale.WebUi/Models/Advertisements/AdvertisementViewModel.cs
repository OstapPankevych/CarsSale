using CarsSale.DataAccess.DTO;
using CarsSale.DataAccess.Identity.Entities;

namespace CarsSale.WebUi.Models.Advertisements
{
    public class AdvertisementViewModel
    {
        public CarsSaleUser User { get; set; }
        public Region Region { get; set; }
        public Advertisement Advertisement { get; set; }
    }
}