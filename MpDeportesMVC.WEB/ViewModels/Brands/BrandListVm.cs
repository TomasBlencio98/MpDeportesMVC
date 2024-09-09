using System.ComponentModel;

namespace MpDeportesMVC.WEB.ViewModels.Brands
{
    public class BrandListVm
    {
        public int BrandId { get; set; }
        [DisplayName("Marcas")]
        public string? BrandName { get; set; }
    }
}
