using MpDeportesMVC.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MpDeportesMVC.Datos.Interfaces
{
    public interface IRepositorioShoe: IRepositorioGenerico<Shoe>
    {
        void Update(Shoe Shoe);
        bool Existe(Shoe Shoe);
        bool EstaRelacionado(Shoe shoe);
    }
}
