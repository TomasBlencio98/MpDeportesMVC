using MpDeportesMVC.Entidades;
using System.Linq.Expressions;

namespace MpDeportesMVC.Servicios.Interfaces
{
    public interface IServicioShoe
    {
        IEnumerable<Shoe>? GetAll(Expression<Func<Shoe, bool>>? filter = null,
            Func<IQueryable<Shoe>, IOrderedQueryable<Shoe>>? orderBy = null,
            string? propertiesNames = null);
        void Save(Shoe Shoe);
        void Delete(Shoe Shoe);
        Shoe? Get(Expression<Func<Shoe, bool>>? filter = null,
            string? propertiesNames = null,
            bool tracked = true);
        bool Existe(Shoe Shoe);
        bool EstaRelacionado(Shoe Shoe);
    }
}
