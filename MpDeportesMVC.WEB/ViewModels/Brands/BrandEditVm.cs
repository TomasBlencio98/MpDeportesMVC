using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MpDeportesMVC.WEB.ViewModels.Brands
{
    public class BrandEditVm
    {
        public int BrandId { get; set; }
        [Required(ErrorMessage = "{0} este campo es requerido")]
        [StringLength(200, ErrorMessage = "{0} debe tener entre {2} y {1} caracteres", MinimumLength = 3)]
        [DisplayName("Marcas")]
        public string? BrandName { get; set; }
    }
}
