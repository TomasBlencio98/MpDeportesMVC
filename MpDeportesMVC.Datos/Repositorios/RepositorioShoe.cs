using MpDeportesMVC.Datos.Interfaces;
using MpDeportesMVC.Entidades;

namespace MpDeportesMVC.Datos.Repositorios
{
    public class RepositorioShoe : RepositorioGenerico<Shoe>, IRepositorioShoe
    {
        private readonly MpDeportesDbContext? _db;

        public RepositorioShoe(MpDeportesDbContext db) : base(db)
        {
            _db = db ?? throw new ArgumentException("Dependencies not set");
        }

        public bool Existe(Shoe Shoe)
        {
            if (Shoe.ShoeId == 0)
            {
                return _db!.Shoes.Any(s => s.Model == Shoe.Model &&
                    s.Description == Shoe.Description && s.Price == Shoe.Price
                    && s.BrandId == Shoe.BrandId && s.SportId == Shoe.SportId
                    && s.ColourId == Shoe.ColourId && s.GenreId == Shoe.GenreId);


            }
            return _db!.Shoes.Any(s => s.Model == Shoe.Model &&
                    s.Description == Shoe.Description && s.Price == Shoe.Price
                    && s.BrandId == Shoe.BrandId && s.SportId == Shoe.SportId
                    && s.ColourId == Shoe.ColourId && s.GenreId == Shoe.GenreId
                    && s.ShoeId == Shoe.ShoeId);
        }

        public bool EstaRelacionado(Shoe shoe)
        {
            if (shoe == null) return false;

            // Verifica si el zapato tiene alguna relación con algún tamaño
            return _db!.ShoesSizes.Any(ss => ss.ShoeId == shoe.ShoeId);
        }

        public void Update(Shoe Shoe)
        {
            _db!.Shoes.Update(Shoe);
        }

    }
}
