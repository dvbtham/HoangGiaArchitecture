using System.Collections.Generic;
using System.Linq;
using HoangGia.Data.Infrastructure;
using HoangGia.Model.Entities;
using HoangGia.Repository.Repositories;
using HoangGia.Services.Interfaces;

namespace HoangGia.Service.Services
{
    public interface IProjectCategoryService : ICrudService<ProjectCategory>, IGetDataService<ProjectCategory>
    {
        void Trash(int id);
        IEnumerable<ProjectCategory> GetWorkingProjectCategories();
    }
    public class ProjectCategoryService : IProjectCategoryService
    {
        private readonly IProjectCategoryRepository _projectCategoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProjectCategoryService(IUnitOfWork unitOfWork, IProjectCategoryRepository projectCategoryRepository)
        {
            _unitOfWork = unitOfWork;
            _projectCategoryRepository = projectCategoryRepository;
        }

        public ProjectCategory Add(ProjectCategory entity)
        {
            return _projectCategoryRepository.Add(entity);
        }

        public void Update(ProjectCategory entity)
        {
            _projectCategoryRepository.Update(entity);
        }

        public void Delete(int id)
        {
            _projectCategoryRepository.Delete(id);
        }

        public void Trash(int id)
        {
            var category = FindById(id);
            category.IsDeleted = true;
        }

        public IEnumerable<ProjectCategory> GetWorkingProjectCategories()
        {
            var query = _projectCategoryRepository.GetMulti(x => !x.IsDeleted);
            return query;
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public ProjectCategory FindById(int id)
        {
            return _projectCategoryRepository.GetSingleById(id);
        }

        public IEnumerable<ProjectCategory> GetAll(string keyword = null)
        {
            var query = _projectCategoryRepository.GetAll();
            if (!string.IsNullOrEmpty(keyword))
                query = query.Where(x => x.Name.ToLower().Contains(keyword.ToLower()));
            return query;
        }
    }
}
