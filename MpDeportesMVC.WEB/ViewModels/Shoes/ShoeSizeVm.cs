using Microsoft.AspNetCore.Mvc.Rendering;
using MpDeportesMVC.Entidades;

namespace MpDeportesMVC.WEB.ViewModels.Shoes
{
    public class ShoeSizeVm
    {
        public int ShoeId { get; set; } 
        public Brand Brand { get; set; }
        public MpDeportesMVC.Entidades.Colour Colour { get; set; }
        public Genre Genre { get; set; }
        public Sport Sport { get; set; }
        public int SizeId { get; set; } 
        public int Stock { get; set; }

        public List<SelectListItem> Sizes { get; set; } = new List<SelectListItem>();
    }
}
