using System.Collections.Generic;
using System.Linq;
using HoangGia.Data.Infrastructure;
using HoangGia.Data.Repositories;
using HoangGia.Entities;
using HoangGia.Services.Interfaces;

namespace HoangGia.Service.Services
{
    public interface IMenuService : ICrudService<Menu>, IGetDataService<Menu>
    {
    }
    public class MenuService : IMenuService
    {
        private readonly IMenuRepository _menuRepository;
        private readonly IUnitOfWork _unitOfWork;
        public MenuService(IMenuRepository menuRepository, IUnitOfWork unitOfWork)
        {
            _menuRepository = menuRepository;
            _unitOfWork = unitOfWork;
        }
        public Menu Add(Menu entity)
        {
           return _menuRepository.Add(entity);
        }

        public void Update(Menu entity)
        {
            _menuRepository.Update(entity);
        }
        
        public void Delete(int id)
        {
            _menuRepository.Delete(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public Menu FindById(int id)
        {
            return _menuRepository.GetSingleById(id);
        }

        public IEnumerable<Menu> GetAll(string keyword = null)
        {
            var query = _menuRepository.GetAll();
            if (!string.IsNullOrEmpty(keyword))
                query = query.Where(x => x.Name.Contains(keyword));
            return query;
        }
    }
}
