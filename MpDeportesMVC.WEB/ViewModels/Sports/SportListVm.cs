using System.ComponentModel;

namespace MpDeportesMVC.WEB.ViewModels.Sports
{
    public class SportListVm
    {
        public int SportId { get; set; }
        [DisplayName("Deportes")]
        public string? SportName { get; set; }
    }
}
