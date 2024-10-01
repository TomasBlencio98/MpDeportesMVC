using System.ComponentModel.DataAnnotations;

namespace MpDeportesMVC.Entidades
{
    public class Brand
    {
        public int BrandId { get; set; }
        public string BrandName { get; set; } = null!;
        public bool Active { get; set; } = true;
        public string? ImageUrl { get; set; }
        public ICollection<Shoe> Shoes { get; set; } = new List<Shoe>();
    }
}
