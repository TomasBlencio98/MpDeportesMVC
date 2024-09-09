using MpDeportesMVC.Entidades;

namespace MpDeportesMVC.Datos.Interfaces
{
    public interface IRepositorioSport : IRepositorioGenerico<Sport>
    {
        void Update(Sport Sport);
        bool Existe(Sport Sport);
        bool EstaRelacionado(int id);
    }
}
