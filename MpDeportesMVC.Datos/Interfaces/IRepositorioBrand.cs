﻿using MpDeportesMVC.Entidades;

namespace MpDeportesMVC.Datos.Interfaces
{
    public interface IRepositorioBrand : IRepositorioGenerico<Brand>
    {
        void Update(Brand Brand);
        bool Existe(Brand Brand);
        bool EstaRelacionado(int id);
        int ContarZapatillasPorMarca(int brandId);
        List<Shoe> ObtenerZapatillasPorMarca(int brandId);
    }
}
