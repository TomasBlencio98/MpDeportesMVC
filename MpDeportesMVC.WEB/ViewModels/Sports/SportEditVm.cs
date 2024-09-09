using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MpDeportesMVC.WEB.ViewModels.Sports
{
    public class SportEditVm
    {
        public int SportId { get; set; }
        [Required(ErrorMessage = "{0} este campo es requerido")]
        [StringLength(200, ErrorMessage = "{0} debe tener entre {2} y {1} caracteres", MinimumLength = 3)]
        [DisplayName("Deportes")]
        public string? SportName { get; set; }
    }
}
