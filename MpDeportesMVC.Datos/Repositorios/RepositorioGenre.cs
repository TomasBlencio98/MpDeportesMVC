using MpDeportesMVC.Datos.Interfaces;
using MpDeportesMVC.Entidades;

namespace MpDeportesMVC.Datos.Repositorios
{
    public class RepositorioGenre : RepositorioGenerico<Genre>, IRepositorioGenre
    {
        private readonly MpDeportesDbContext _db;

        public RepositorioGenre(MpDeportesDbContext? db) : base(db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public bool EstaRelacionado(int id)
        {
            return _db.Shoes.Any(p => p.GenreId == id);
        }

        public bool Existe(Genre genre)
        {
            if (genre.GenreId == 0)
            {
                return _db.Genres.Any(s => s.GenreName == genre.GenreName);
            }
            return _db.Genres.Any(s => s.GenreName == genre.GenreName &&
                    s.GenreId != genre.GenreId);
        }

        public void Update(Genre genre)
        {
            _db.Genres.Update(genre);
        }
    }
}
