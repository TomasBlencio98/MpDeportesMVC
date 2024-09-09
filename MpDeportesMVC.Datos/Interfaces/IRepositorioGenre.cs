using MpDeportesMVC.Entidades;

namespace MpDeportesMVC.Datos.Interfaces
{
    public interface IRepositorioGenre : IRepositorioGenerico<Genre>
    {
        void Update(Genre genre);
        bool Existe(Genre genre);
        bool EstaRelacionado(int id);
    }
}
