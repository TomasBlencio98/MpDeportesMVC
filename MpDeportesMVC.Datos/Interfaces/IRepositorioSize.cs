﻿using MpDeportesMVC.Entidades;

namespace MpDeportesMVC.Datos.Interfaces
{
    public interface IRepositorioSize : IRepositorioGenerico<Size>
    {
        void Update(Size Size);
        bool Existe(Size Size);
        bool EstaRelacionado(int id);
    }
}