using System.Web.Mvc;
using CarsSale.DataAccess.Repositories.Interfaces;

namespace CarsSale.WebUi.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAdvertisementRepository _advertisementRepository;

        public HomeController(IAdvertisementRepository advertisementRepository)
        {
            _advertisementRepository = advertisementRepository;
        }

        public ActionResult Index()
        {
            var advertisements = _advertisementRepository.GetAdvertisements();
            return View("Index", advertisements);
        }
    }
}