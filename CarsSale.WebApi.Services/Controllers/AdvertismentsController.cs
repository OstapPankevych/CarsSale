using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarsSale.DataAccess.DTO;
using CarsSale.DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarsSale.WebApi.Services.Controllers
{
    [Route("api/[controller]")]
    public class AdvertismentsController : Controller
    {
        private readonly IAdvertisementRepository _advertisementRepository;
        public AdvertismentsController(IAdvertisementRepository advertisementRepository)
        {
            _advertisementRepository = advertisementRepository;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Advertisement Get(int id)
        {
            return _advertisementRepository.GetAdvertisement(id);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
