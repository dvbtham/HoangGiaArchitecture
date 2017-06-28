using HoangGia.Data.Infrastructure;
using HoangGia.Data.Repositories;
using HoangGia.Entities;

namespace HoangGia.Service.Services
{
    public interface IErrorService
    {
        Error LogError(Error error);
        void SaveChange();
    }
    public class ErrorService : IErrorService
    {
        private readonly IErrorRepository _errorRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ErrorService(IErrorRepository errorRepository, IUnitOfWork unitOfWork)
        {
            _errorRepository = errorRepository;
            _unitOfWork = unitOfWork;
        }

        public Error LogError(Error error)
        {
            return _errorRepository.Add(error);
        }

        public void SaveChange()
        {
            _unitOfWork.Commit();
        }
    }
}
