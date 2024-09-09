namespace MpDeportesMVC.WEB.ViewModels.Shoes
{
    public class ShoeListVm
    {
        public int ShoeId { get; set; }
        public string Brand { get; set; } = null!;
        public string Sport { get; set; } = null!;
        public string Genre { get; set; } = null!;
        public string Colour { get; set; } = null!;
        //public decimal Size { get; set; } = 0!;
        public string Model { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public int Cantidad { get; set; } = 0!;
    }
}
