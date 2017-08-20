using System.Collections.Generic;
using HoangGia.Data.Infrastructure;
using HoangGia.Data.Repositories;
using HoangGia.Model.Entities;
using HoangGia.Services.Interfaces;

namespace HoangGia.Service.Services
{
    public interface ISettingService : IGetDataService<Setting>, ICrudService<Setting>
    {

    }
    public class SettingService : ISettingService
    {
        private readonly ISettingRepository _settingRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SettingService(ISettingRepository settingRepository, IUnitOfWork unitOfWork)
        {
            _settingRepository = settingRepository;
            _unitOfWork = unitOfWork;
        }
        public Setting FindById(int id)
        {
            var setting = _settingRepository.GetSingleById(id);
            return setting;
        }

        public IEnumerable<Setting> GetAll(string keyword = null)
        {
            var settings = _settingRepository.GetAll();
            return settings;
        }

        public Setting Add(Setting entity)
        {
            return _settingRepository.Add(entity);
        }

        public void Update(Setting entity)
        {
            _settingRepository.Update(entity);
        }

        public void Delete(int id)
        {
            
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }
    }
}
