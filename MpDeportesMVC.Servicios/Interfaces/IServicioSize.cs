using MpDeportesMVC.Entidades;
using System.Linq.Expressions;

namespace MpDeportesMVC.Servicios.Interfaces
{
    public interface IServicioSize
    {
        IEnumerable<Size>? GetAll(Expression<Func<Size, bool>>? filter = null,
            Func<IQueryable<Size>, IOrderedQueryable<Size>>? orderBy = null,
            string? propertiesNames = null);
        void Save(Size Size);
        void Delete(Size Size);
        Size? Get(Expression<Func<Size, bool>>? filter = null,
            string? propertiesNames = null,
            bool tracked = true);
        bool Existe(Size Size);
        bool EstaRelacionado(int id);
        int ContarZapatillasPorTalle(int sizeId);
        List<Shoe> ObtenerZapatillasPorTalle(int sizeId);
    }
}
