using Microsoft.EntityFrameworkCore;
using MpDeportesMVC.Datos.Interfaces;
using System.Linq.Expressions;

namespace MpDeportesMVC.Datos.Repositorios
{
    public class RepositorioGenerico<T> : IRepositorioGenerico<T> where T : class
    {
        private readonly MpDeportesDbContext? _db;
        internal DbSet<T> dbSet { get; set; }
        public RepositorioGenerico(MpDeportesDbContext? db)
        {
            _db = db ?? throw new ArgumentException("Depedencies not set");
            dbSet = _db.Set<T>();
        }

        public void Add(T entity)
        {
            try
            {
                dbSet.Add(entity);
            }
            catch (Exception)
            {

                throw new Exception("Error al agregar una entidad");
            }
        }

        public void Delete(T entity)
        {
            try
            {
                dbSet.Remove(entity);
            }
            catch (Exception)
            {

                throw new Exception("Error al eliminar una entidad");
            }
        }

        public T? Get(Expression<Func<T, bool>>? filter = null,
            string? propertiesNames = null, bool tracked = true)
        {
            IQueryable<T> query = dbSet.AsQueryable();
            if (!string.IsNullOrWhiteSpace(propertiesNames))
            {
                foreach (var property in propertiesNames
                    .Split(',', StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }
            }
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return tracked ? query.FirstOrDefault()
                : query.AsNoTracking().FirstOrDefault();
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            string? propertiesNames = null)
        {
            IQueryable<T> query = dbSet.AsNoTracking();
            if (!string.IsNullOrWhiteSpace(propertiesNames))
            {
                foreach (var property in propertiesNames
                    .Split(',', StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }
            }
            if (orderBy != null)
            {
                query = orderBy(query);
            }
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return query.ToList();
        }
    }
}
