using System.Collections.Generic;
using System.Linq;
using HoangGia.Data.Infrastructure;
using HoangGia.Repository.Repositories;
using HoangGia.Services.Interfaces;

namespace HoangGia.Service.Services
{
    public interface IServService : ICrudService<Model.Entities.Service>, IGetDataService<Model.Entities.Service>
    {
        IEnumerable<Model.Entities.Service> GetActivedServices(string keyword = null);
        IEnumerable<Model.Entities.Service> ShowOnHomeServices();
    }
    public class ServService : IServService
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ServService(IServiceRepository serviceRepository, IUnitOfWork unitOfWork)
        {
            _serviceRepository = serviceRepository;
            _unitOfWork = unitOfWork;
        }

        public Model.Entities.Service Add(Model.Entities.Service entity)
        {
            return _serviceRepository.Add(entity);
        }

        public void Update(Model.Entities.Service entity)
        {
            _serviceRepository.Update(entity);
        }

        public void Delete(int id)
        {
            _serviceRepository.Delete(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public Model.Entities.Service FindById(int id)
        {
            return _serviceRepository.GetSingleById(id);
        }

        public IEnumerable<Model.Entities.Service> GetAll(string keyword = null)
        {
            var services = _serviceRepository.GetAll();
            if (!string.IsNullOrEmpty(keyword))
                services = services.Where(x => x.Name.ToLower().Contains(keyword.ToLower()));
            return services;
        }
        public IEnumerable<Model.Entities.Service> GetActivedServices(string keyword = null)
        {
            var services = _serviceRepository.GetMulti(x => !x.IsDeleted && x.IsActived);
            if (!string.IsNullOrEmpty(keyword))
                services = services.Where(x => x.Name.ToLower().Contains(keyword.ToLower()));
            return services;
        }

        public IEnumerable<Model.Entities.Service> ShowOnHomeServices()
        {
            var services = _serviceRepository.GetMulti(x => !x.IsDeleted && x.IsActived && x.ShowOnHome);
            return services;
        }
    }
}
