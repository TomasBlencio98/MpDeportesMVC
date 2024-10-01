using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MpDeportesMVC.WEB.ViewModels.Brands
{
    public class BrandEditImageVm
    {
        public int BrandId { get; set; }
        [Required(ErrorMessage = "{0} este campo es requerido")]
        [StringLength(200, ErrorMessage = "{0} debe tener entre {2} y {1} caracteres", MinimumLength = 3)]
        [DisplayName("Marca")]
        public string? BrandName { get; set; }

        [Display(Name = "Image")]
        public string? ImageUrl { get; set; }

        public IFormFile? ImageFile { get; set; }  // Propiedad para la imagen

        [Display(Name = "Remove Image")]
        public bool RemoveImage { get; set; }  // Propiedad para borrar imagen cargada
    }
}
