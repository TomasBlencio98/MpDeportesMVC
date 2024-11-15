using MpDeportesMVC.Datos;
using MpDeportesMVC.Datos.Interfaces;
using MpDeportesMVC.Entidades;
using MpDeportesMVC.Servicios.Interfaces;
using System.Linq.Expressions;

namespace MpDeportesMVC.Servicios.Servicios
{
    public class ServicioShoeSize : IServicioShoeSize
    {
        private readonly IRepositorioShoeSize? _repository;
        private readonly IUnitOfWork? _unitOfWork;

        public ServicioShoeSize(IRepositorioShoeSize? repository,
            IUnitOfWork? unitOfWork)
        {
            _repository = repository ?? throw new ArgumentException("Dependencies not set");
            _unitOfWork = unitOfWork ?? throw new ArgumentException("Dependencies not set");
        }


        public void Delete(ShoeSize ShoeSize)
        {
            try
            {
                _unitOfWork!.BeginTransaction();
                _repository!.Delete(ShoeSize);
                _unitOfWork!.Commit();

            }
            catch (Exception)
            {
                _unitOfWork!.Rollback();
                throw;
            }
        }

        public ShoeSize? Get(Expression<Func<ShoeSize, bool>>? filter = null,
            string? propertiesNames = null, bool tracked = true)
        {
            return _repository!.Get(filter, propertiesNames, tracked);
        }


        public IEnumerable<ShoeSize> GetAll(Expression<Func<ShoeSize, bool>>? filter = null,
            Func<IQueryable<ShoeSize>, IOrderedQueryable<ShoeSize>>? orderBy = null,
            string? propertiesNames = null)
        {
            return _repository!.GetAll(filter, orderBy, propertiesNames);
        }

        public void Save(ShoeSize ShoeSize)
        {
            try
            {
                _unitOfWork?.BeginTransaction();
                if (ShoeSize.ShoeSizeId == 0)
                {
                    _repository?.Add(ShoeSize);
                }
                else
                {
                    _repository?.Update(ShoeSize);
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
