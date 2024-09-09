using MpDeportesMVC.Datos;
using MpDeportesMVC.Datos.Interfaces;
using MpDeportesMVC.Entidades;
using MpDeportesMVC.Servicios.Interfaces;
using System.Linq.Expressions;

namespace MpDeportesMVC.Servicios.Servicios
{
    public class ServicioSport : IServicioSport
    {
        private readonly IRepositorioSport? _repository;
        private readonly IUnitOfWork? _unitOfWork;

        public ServicioSport(IRepositorioSport? repository,
            IUnitOfWork? unitOfWork)
        {
            _repository = repository ?? throw new ArgumentException("Dependencies not set");
            _unitOfWork = unitOfWork ?? throw new ArgumentException("Dependencies not set");
        }

        public void Delete(Sport Sport)
        {
            try
            {
                _unitOfWork!.BeginTransaction();
                _repository!.Delete(Sport);
                _unitOfWork!.Commit();

            }
            catch (Exception)
            {
                _unitOfWork!.Rollback();
                throw;
            }
        }

        public bool EstaRelacionado(int id)
        {
            return _repository!.EstaRelacionado(id);
        }


        public bool Existe(Sport Sport)
        {
            return _repository!.Existe(Sport);
        }

        public Sport? Get(Expression<Func<Sport, bool>>? filter = null,
            string? propertiesNames = null, bool tracked = true)
        {
            return _repository!.Get(filter, propertiesNames, tracked);
        }


        public IEnumerable<Sport> GetAll(Expression<Func<Sport, bool>>? filter = null,
            Func<IQueryable<Sport>, IOrderedQueryable<Sport>>? orderBy = null,
            string? propertiesNames = null)
        {
            return _repository!.GetAll(filter, orderBy, propertiesNames);
        }


        public void Save(Sport Sport)
        {
            try
            {
                _unitOfWork?.BeginTransaction();
                if (Sport.SportId == 0)
                {
                    _repository?.Add(Sport);
                }
                else
                {
                    _repository?.Update(Sport);
                }
                _unitOfWork?.Commit();

            }
            catch (Exception)
            {
                _unitOfWork?.Rollback();
                throw;
            }
        }

    }
}
