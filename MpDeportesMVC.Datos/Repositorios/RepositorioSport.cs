using MpDeportesMVC.Datos.Interfaces;
using MpDeportesMVC.Entidades;

namespace MpDeportesMVC.Datos.Repositorios
{
    public class RepositorioSport : RepositorioGenerico<Sport>, IRepositorioSport
    {
        private readonly MpDeportesDbContext _db;

        public RepositorioSport(MpDeportesDbContext? db) : base(db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public bool EstaRelacionado(int id)
        {
            return _db.Shoes.Any(p => p.SportId == id);
        }

        public bool Existe(Sport Sport)
        {
            if (Sport.SportId == 0)
            {
                return _db.Sports.Any(s => s.SportName == Sport.SportName);
            }
            return _db.Sports.Any(s => s.SportName == Sport.SportName &&
                    s.SportId != Sport.SportId);
        }

        public void Update(Sport Sport)
        {
            _db.Sports.Update(Sport);
        }
    }
}
