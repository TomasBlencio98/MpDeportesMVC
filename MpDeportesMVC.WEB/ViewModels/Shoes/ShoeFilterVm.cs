using Microsoft.AspNetCore.Mvc.Rendering;
using X.PagedList;

namespace MpDeportesMVC.WEB.ViewModels.Shoes
{
    public class ShoeFilterVm
    {
        public IPagedList<ShoeListVm>? Shoes { get; set; }
        public List<SelectListItem>? Brands { get; set; }
    }
}
