using Microsoft.EntityFrameworkCore;
using MpDeportesMVC.Datos.Interfaces;
using MpDeportesMVC.Entidades;
using Size = MpDeportesMVC.Entidades.Size;

namespace MpDeportesMVC.Datos.Repositorios
{
    public class RepositorioSize : RepositorioGenerico<Size>, IRepositorioSize
    {
        private readonly MpDeportesDbContext _db;

        public RepositorioSize(MpDeportesDbContext? db) : base(db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public int ContarZapatillasPorTalle(int sizeId)
        {
            return _db.ShoesSizes
                   .Where(ss => ss.SizeId == sizeId)
                   .Select(ss => ss.ShoeId)
                   .Distinct()
                   .Count();
        }

        public bool EstaRelacionado(int id)
        {
            return _db.ShoesSizes.Any(ss => ss.SizeId == id);
        }

        public bool Existe(Size Size)
        {
            if (Size.SizeId == 0)
            {
                return _db.Sizes.Any(s => s.SizeNumber == Size.SizeNumber);
            }
            return _db.Sizes.Any(s => s.SizeNumber == Size.SizeNumber &&
                    s.SizeId != Size.SizeId);
        }

        public List<Shoe> ObtenerZapatillasPorTalle(int sizeId)
        {
            return _db.ShoesSizes
               .Where(ss => ss.SizeId == sizeId)
               .Include(ss => ss.Shoe) .ThenInclude(s => s.Brand) 
               .Include(ss => ss.Shoe).ThenInclude(s => s.Colour) 
               .Include(ss => ss.Shoe).ThenInclude(s => s.Genre) 
               .Include(ss => ss.Shoe).ThenInclude(s => s.Sport) 
               .Select(ss => ss.Shoe) 
               .ToList();
        }

        public void Update(Size Size)
        {
            _db.Sizes.Update(Size);
        }
    }
}
