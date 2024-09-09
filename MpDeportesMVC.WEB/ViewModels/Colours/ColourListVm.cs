using System.ComponentModel;

namespace MpDeportesMVC.WEB.ViewModels.Colour
{
    public class ColourListVm
    {
        public int ColourId { get; set; }
        [DisplayName("Colores")]
        public string? ColourName { get; set; }
    }
}
