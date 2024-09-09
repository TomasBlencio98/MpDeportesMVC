using MpDeportesMVC.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MpDeportesMVC.Datos.Interfaces
{
    public interface IRepositorioColour:IRepositorioGenerico<Colour>
    {
        void Update(Colour Colour);
        bool Existe(Colour Colour);
        bool EstaRelacionado(int id);
    }
}
