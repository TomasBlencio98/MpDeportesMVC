using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MpDeportesMVC.Entidades
{
    public class Shoe
    {
        public int ShoeId { get; set; }
        public int BrandId { get; set; }
        public int SportId { get; set; }
        public int GenreId { get; set; }
        public int ColourId { get; set; }
        public string Model { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public bool Active { get; set; } = true;
        public Brand? Brand { get; set; }
        public Sport? Sport { get; set; }
        public Genre? Genre { get; set; }
        public Colour? Colour { get; set; }
        public ICollection<ShoeSize> ShoesSizes { get; set; } = new List<ShoeSize>();
    }
}
