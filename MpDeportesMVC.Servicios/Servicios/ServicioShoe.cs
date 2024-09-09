using MpDeportesMVC.Datos.Interfaces;
using MpDeportesMVC.Datos;
using MpDeportesMVC.Entidades;
using System.Linq.Expressions;
using MpDeportesMVC.Servicios.Interfaces;

namespace MpDeportesMVC.Servicios.Servicios
{
    public class ServicioShoe:IServicioShoe
    {
        private readonly IRepositorioShoe? _repository;
        private readonly IUnitOfWork? _unitOfWork;

        public ServicioShoe(IRepositorioShoe? repository,
            IUnitOfWork? unitOfWork)
        {
            _repository = repository ?? throw new ArgumentException("Dependencies not set");
            _unitOfWork = unitOfWork ?? throw new ArgumentException("Dependencies not set");
        }

        public void Delete(Shoe Shoe)
        {
            try
            {
                _unitOfWork!.BeginTransaction();
                _repository!.Delete(Shoe);
                _unitOfWork!.Commit();

            }
            catch (Exception)
            {
                _unitOfWork!.Rollback();
                throw;
            }
        }

        public bool EstaRelacionado(Shoe Shoe, Size Size)
        {
            return _repository!.EstaRelacionado(Shoe,Size);
        }


        public bool Existe(Shoe Shoe)
        {
            return _repository!.Existe(Shoe);
        }

        public Shoe? Get(Expression<Func<Shoe, bool>>? filter = null,
            string? propertiesNames = null, bool tracked = true)
        {
            return _repository!.Get(filter, propertiesNames, tracked);
        }


        public IEnumerable<Shoe> GetAll(Expression<Func<Shoe, bool>>? filter = null,
            Func<IQueryable<Shoe>, IOrderedQueryable<Shoe>>? orderBy = null,
            string? propertiesNames = null)
        {
            return _repository!.GetAll(filter, orderBy, propertiesNames);
        }


        public void Save(Shoe Shoe)
        {
            try
            {
                _unitOfWork?.BeginTransaction();
                if (Shoe.ShoeId == 0)
                {
                    _repository?.Add(Shoe);
                }
                else
                {
                    _repository?.Update(Shoe);
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
