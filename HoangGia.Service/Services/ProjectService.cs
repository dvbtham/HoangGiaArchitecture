using System.Collections.Generic;
using System.Linq;
using HoangGia.Data.Infrastructure;
using HoangGia.Model.Entities;
using HoangGia.Repository.Repositories;
using HoangGia.Services.Interfaces;

namespace HoangGia.Service.Services
{
    public interface IProjectService : ICrudService<Project>, IGetDataService<Project>
    {
        IEnumerable<Project> GetInclude(string keyword = null, string[] includes = null);
        IEnumerable<Project> ActivingProjects(string[] includes = null);
    }
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProjectService(IProjectRepository projectRepository, IUnitOfWork unitOfWork)
        {
            _projectRepository = projectRepository;
            _unitOfWork = unitOfWork;
        }

        public Project Add(Project entity)
        {
            return _projectRepository.Add(entity);
        }

        public void Update(Project entity)
        {
            _projectRepository.Update(entity);
        }

        public void Delete(int id)
        {
            _projectRepository.Delete(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public Project FindById(int id)
        {
            return _projectRepository.GetSingleById(id);
        }

        public IEnumerable<Project> GetAll(string keyword = null)
        {
            var query = _projectRepository.GetAll();
            if (!string.IsNullOrEmpty(keyword))
                query = query.Where(x => x.Name.ToLower().Contains(keyword.ToLower()));
            return query;
        }
        public IEnumerable<Project> GetInclude(string keyword = null, string[] includes = null)
        {
            var query = _projectRepository.GetAll(includes);
            if (!string.IsNullOrEmpty(keyword))
                query = query.Where(x => x.Name.ToLower().Contains(keyword.ToLower()));
            return query;
        }

        public IEnumerable<Project> ActivingProjects(string[] includes)
        {
            var query = _projectRepository.GetAll(includes);
            return query;
        }
    }
}
