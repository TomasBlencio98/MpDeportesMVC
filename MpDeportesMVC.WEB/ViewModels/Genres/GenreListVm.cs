using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MpDeportesMVC.WEB.ViewModels.Genres
{
    public class GenreListVm
    {
        public int GenreId { get; set; }
        [DisplayName("Generos")]
        public string? GenreName { get; set; }
    }
}
