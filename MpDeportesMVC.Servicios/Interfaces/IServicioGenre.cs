using MpDeportesMVC.Entidades;
using System.Linq.Expressions;

namespace MpDeportesMVC.Servicios.Interfaces
{
    public interface IServicioGenre
    {
        IEnumerable<Genre>? GetAll(Expression<Func<Genre, bool>>? filter = null,
            Func<IQueryable<Genre>, IOrderedQueryable<Genre>>? orderBy = null,
            string? propertiesNames = null);
        void Save(Genre Genre);
        void Delete(Genre Genre);
        Genre? Get(Expression<Func<Genre, bool>>? filter = null,
            string? propertiesNames = null,
            bool tracked = true);
        bool Existe(Genre Genre);
        bool EstaRelacionado(int id);
    }
}
