using MpDeportesMVC.Datos;
using MpDeportesMVC.Datos.Interfaces;
using MpDeportesMVC.Entidades;
using MpDeportesMVC.Servicios.Interfaces;
using System.Linq.Expressions;

namespace MpDeportesMVC.Servicios.Servicios
{
    public class ServicioSize : IServicioSize
    {
        private readonly IRepositorioSize? _repository;
        private readonly IUnitOfWork? _unitOfWork;

        public ServicioSize(IRepositorioSize? repository,
            IUnitOfWork? unitOfWork)
        {
            _repository = repository ?? throw new ArgumentException("Dependencies not set");
            _unitOfWork = unitOfWork ?? throw new ArgumentException("Dependencies not set");
        }

        public void Delete(Size Size)
        {
            try
            {
                _unitOfWork!.BeginTransaction();
                _repository!.Delete(Size);
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


        public bool Existe(Size Size)
        {
            return _repository!.Existe(Size);
        }

        public Size? Get(Expression<Func<Size, bool>>? filter = null,
            string? propertiesNames = null, bool tracked = true)
        {
            return _repository!.Get(filter, propertiesNames, tracked);
        }


        public IEnumerable<Size> GetAll(Expression<Func<Size, bool>>? filter = null,
            Func<IQueryable<Size>, IOrderedQueryable<Size>>? orderBy = null,
            string? propertiesNames = null)
        {
            return _repository!.GetAll(filter, orderBy, propertiesNames);
        }


        public void Save(Size Size)
        {
            try
            {
                _unitOfWork?.BeginTransaction();
                if (Size.SizeId == 0)
                {
                    _repository?.Add(Size);
                }
                else
                {
                    _repository?.Update(Size);
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
