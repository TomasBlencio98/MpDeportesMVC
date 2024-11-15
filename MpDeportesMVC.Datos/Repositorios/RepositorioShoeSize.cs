using Microsoft.EntityFrameworkCore;
using MpDeportesMVC.Datos.Interfaces;
using MpDeportesMVC.Entidades;

namespace MpDeportesMVC.Datos.Repositorios
{
    public class RepositorioShoeSize : RepositorioGenerico<ShoeSize>, IRepositorioShoeSize
    {
        private readonly MpDeportesDbContext _db;
        public RepositorioShoeSize(MpDeportesDbContext? db) : base(db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public void Update(ShoeSize ShoeSize)
        {
            _db!.ShoesSizes.Update(ShoeSize);
        }
    }
}
