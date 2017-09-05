using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Permissions;
using CarsSale.DataAccess;
using CarsSale.WebUi.Models.Vehicl;

namespace CarsSale.WebUi.Models
{
    public class Advertisement
    {
        public Region Region { get; set; }
        public Brand Brand { get; set; }
        public Type Type { get; set; }
        public Transmission Transmission { get; set; }
        public BodyType BodyType { get; set; }
        public Engine Engine { get; set; }
        public IEnumerable<Fuel> SelectedFuels { get; set; }

        public IEnumerable<Brand> Brands { get; set; }
        public IEnumerable<Type> Types { get; set; }
        public IEnumerable<Transmission> Transmissions { get; set; }
        public IEnumerable<BodyType> BodyTypes { get; set; }
        public IEnumerable<Engine> Engines { get; set; }
        public IEnumerable<Fuel> Fuels { get; set; }
    }
}