using CarsSale.DataAccess.DTO;

namespace CarsSale.WebUi.Models.Advertisements
{
    public class AdvertisementViewModel
    {
        public User User { get; set; }
        public Region Region { get; set; }
        public Advertisement Advertisement { get; set; }
    }
}