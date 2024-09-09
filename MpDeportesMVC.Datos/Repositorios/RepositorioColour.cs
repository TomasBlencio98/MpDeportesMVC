using MpDeportesMVC.Datos.Interfaces;
using MpDeportesMVC.Entidades;

namespace MpDeportesMVC.Datos.Repositorios
{
    public class RepositorioColour : RepositorioGenerico<Colour>, IRepositorioColour
    {
        private readonly MpDeportesDbContext _db;

        public RepositorioColour(MpDeportesDbContext? db) : base(db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public bool EstaRelacionado(int id)
        {
            return _db.Shoes.Any(p => p.ColourId == id);
        }

        public bool Existe(Colour Colour)
        {
            if (Colour.ColourId == 0)
            {
                return _db.Colours.Any(s => s.ColourName == Colour.ColourName);
            }
            return _db.Colours.Any(s => s.ColourName == Colour.ColourName &&
                    s.ColourId != Colour.ColourId);
        }

        public void Update(Colour Colour)
        {
            _db.Colours.Update(Colour);
        }
    }
}
