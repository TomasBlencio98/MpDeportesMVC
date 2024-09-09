using MpDeportesMVC.Datos.Interfaces;
using MpDeportesMVC.Entidades;

namespace MpDeportesMVC.Datos.Repositorios
{
    public class RepositorioBrand : RepositorioGenerico<Brand>, IRepositorioBrand
    {
        private readonly MpDeportesDbContext _db;
        public RepositorioBrand(MpDeportesDbContext? db) : base(db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public bool EstaRelacionado(int id)
        {
            return _db.Shoes.Any(p => p.BrandId == id);
        }

        public bool Existe(Brand Brand)
        {
            if (Brand.BrandId == 0)
            {
                return _db.Brands.Any(s => s.BrandName == Brand.BrandName);
            }
            return _db.Brands.Any(s => s.BrandName == Brand.BrandName &&
                    s.BrandId != Brand.BrandId);
        }

        public void Update(Brand Brand)
        {
            _db.Brands.Update(Brand);
        }
    }
}
