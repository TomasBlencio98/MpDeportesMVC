using Microsoft.EntityFrameworkCore;
using MpDeportesMVC.Entidades;
using Size = MpDeportesMVC.Entidades.Size;

namespace MpDeportesMVC.Datos
{
    public class MpDeportesDbContext : DbContext
    {
        public MpDeportesDbContext(DbContextOptions<MpDeportesDbContext>
            options) : base(options)
        {

        }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Sport> Sports { get; set; }
        public DbSet<Colour> Colours { get; set; }
        public DbSet<Shoe> Shoes { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<ShoeSize> ShoesSizes { get; set; }
    }
}
