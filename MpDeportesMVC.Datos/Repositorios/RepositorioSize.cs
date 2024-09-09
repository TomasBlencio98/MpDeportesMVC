using MpDeportesMVC.Datos.Interfaces;
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

        public void Update(Size Size)
        {
            _db.Sizes.Update(Size);
        }
    }
}
