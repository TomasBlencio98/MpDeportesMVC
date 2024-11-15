using MpDeportesMVC.Entidades;

namespace MpDeportesMVC.Datos.Interfaces
{
    public interface IRepositorioShoeSize : IRepositorioGenerico<ShoeSize>
    {
        void Update(ShoeSize ShoeSize);
    }
}
