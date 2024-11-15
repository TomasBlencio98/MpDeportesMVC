using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MpDeportesMVC.Datos;
using MpDeportesMVC.Datos.Interfaces;
using Microsoft.EntityFrameworkCore;
using MpDeportesMVC.Datos.Repositorios;
using MpDeportesMVC.Servicios.Interfaces;
using MpDeportesMVC.Servicios.Servicios;


namespace MpDeportesMVC.Ioc
{
    public class DI
    {
        public static void ConfigurarServicios(IServiceCollection servicios,
            IConfiguration configuration)
        {

            servicios.AddScoped<IRepositorioGenre, RepositorioGenre>();
            servicios.AddScoped<IServicioGenre, ServicioGenre>();
            servicios.AddScoped<IRepositorioBrand, RepositorioBrand>();
            servicios.AddScoped<IServicioBrand, ServicioBrand>();
            servicios.AddScoped<IRepositorioColour, RepositorioColour>();
            servicios.AddScoped<IServicioColour, ServicioColour>();
            servicios.AddScoped<IRepositorioSport, RepositorioSport>();
            servicios.AddScoped<IServicioSport, ServicioSport>();
            servicios.AddScoped<IRepositorioSize, RepositorioSize>();
            servicios.AddScoped<IServicioSize, ServicioSize>();
            servicios.AddScoped<IRepositorioShoe, RepositorioShoe>();
            servicios.AddScoped<IServicioShoe, ServicioShoe>();
            servicios.AddScoped<IRepositorioShoeSize, RepositorioShoeSize>();
            servicios.AddScoped<IServicioShoeSize, ServicioShoeSize>();

            servicios.AddScoped<IUnitOfWork, UnitOfWork>();

            servicios.AddDbContext<MpDeportesDbContext>(options =>
            {
                options.UseSqlServer
                (configuration.GetConnectionString("MyConnection"));
            });


        }
    }
}
