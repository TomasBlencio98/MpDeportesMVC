using MpDeportesMVC.Entidades;
using System.Linq.Expressions;

namespace MpDeportesMVC.Servicios.Interfaces
{
    public interface IServicioShoeSize
    {
        IEnumerable<ShoeSize>? GetAll(Expression<Func<ShoeSize, bool>>? filter = null,
            Func<IQueryable<ShoeSize>, IOrderedQueryable<ShoeSize>>? orderBy = null,
            string? propertiesNames = null);
        void Save(ShoeSize ShoeSize);
        void Delete(ShoeSize ShoeSize);
        ShoeSize? Get(Expression<Func<ShoeSize, bool>>? filter = null,
            string? propertiesNames = null,
            bool tracked = true);
    }
}
