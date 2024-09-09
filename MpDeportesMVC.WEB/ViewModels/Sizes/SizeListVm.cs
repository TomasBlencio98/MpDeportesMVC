using System.ComponentModel;

namespace MpDeportesMVC.WEB.ViewModels.Sizes
{
    public class SizeListVm
    {
        public int SizeId { get; set; }
        [DisplayName("Talles")]
        public string? SizeNumber { get; set; }
    }
}
