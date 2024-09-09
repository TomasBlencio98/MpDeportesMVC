using MpDeportesMVC.Datos;
using MpDeportesMVC.Datos.Interfaces;
using MpDeportesMVC.Entidades;
using MpDeportesMVC.Servicios.Interfaces;
using System.Linq.Expressions;

namespace MpDeportesMVC.Servicios.Servicios
{
    public class ServicioBrand : IServicioBrand
    {
        private readonly IRepositorioBrand? _repository;
        private readonly IUnitOfWork? _unitOfWork;

        public ServicioBrand(IRepositorioBrand? repository,
            IUnitOfWork? unitOfWork)
        {
            _repository = repository ?? throw new ArgumentException("Dependencies not set");
            _unitOfWork = unitOfWork ?? throw new ArgumentException("Dependencies not set");
        }

        public void Delete(Brand Brand)
        {
            try
            {
                _unitOfWork!.BeginTransaction();
                _repository!.Delete(Brand);
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


        public bool Existe(Brand Brand)
        {
            return _repository!.Existe(Brand);
        }

        public Brand? Get(Expression<Func<Brand, bool>>? filter = null,
            string? propertiesNames = null, bool tracked = true)
        {
            return _repository!.Get(filter, propertiesNames, tracked);
        }


        public IEnumerable<Brand> GetAll(Expression<Func<Brand, bool>>? filter = null,
            Func<IQueryable<Brand>, IOrderedQueryable<Brand>>? orderBy = null,
            string? propertiesNames = null)
        {
            return _repository!.GetAll(filter, orderBy, propertiesNames);
        }


        public void Save(Brand Brand)
        {
            try
            {
                _unitOfWork?.BeginTransaction();
                if (Brand.BrandId == 0)
                {
                    _repository?.Add(Brand);
                }
                else
                {
                    _repository?.Update(Brand);
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
