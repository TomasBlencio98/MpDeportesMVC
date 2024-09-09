using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MpDeportesMVC.WEB.ViewModels.Genres
{
    public class GenreEditVm
    {
        public int GenreId { get; set; }
        [Required(ErrorMessage = "{0} este campo es requerido")]
        [StringLength(200, ErrorMessage = "{0} debe tener entre {2} y {1} caracteres", MinimumLength = 3)]
        [DisplayName("Generos")]
        public string? GenreName { get; set; }
    }
}
