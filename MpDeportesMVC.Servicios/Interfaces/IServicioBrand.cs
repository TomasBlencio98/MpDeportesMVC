using MpDeportesMVC.Entidades;
using System.Linq.Expressions;

namespace MpDeportesMVC.Servicios.Interfaces
{
    public interface IServicioBrand
    {
        IEnumerable<Brand>? GetAll(Expression<Func<Brand, bool>>? filter = null,
            Func<IQueryable<Brand>, IOrderedQueryable<Brand>>? orderBy = null,
            string? propertiesNames = null);
        void Save(Brand Brand);
        void Delete(Brand Brand);
        Brand? Get(Expression<Func<Brand, bool>>? filter = null,
            string? propertiesNames = null,
            bool tracked = true);
        bool Existe(Brand Brand);
        bool EstaRelacionado(int id);
    }
}
