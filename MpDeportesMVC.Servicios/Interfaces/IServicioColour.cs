using MpDeportesMVC.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MpDeportesMVC.Servicios.Interfaces
{
    public interface IServicioColour
    {
        IEnumerable<Colour>? GetAll(Expression<Func<Colour, bool>>? filter = null,
            Func<IQueryable<Colour>, IOrderedQueryable<Colour>>? orderBy = null,
            string? propertiesNames = null);
        void Save(Colour Colour);
        void Delete(Colour Colour);
        Colour? Get(Expression<Func<Colour, bool>>? filter = null,
            string? propertiesNames = null,
            bool tracked = true);
        bool Existe(Colour Colour);
        bool EstaRelacionado(int id);
    }
}
