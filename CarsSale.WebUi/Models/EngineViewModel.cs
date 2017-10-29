using System.ComponentModel.DataAnnotations;

namespace CarsSale.WebUi.Models
{
    public class EngineViewModel
    {
        [Range(1, 10000)]
        public int Volume { get; set; }
    }
}