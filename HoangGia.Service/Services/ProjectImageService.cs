using System.Collections.Generic;
using HoangGia.Data.Infrastructure;
using HoangGia.Model.Entities;
using HoangGia.Repository.Repositories;
using HoangGia.Services.Interfaces;

namespace HoangGia.Service.Services
{
    public interface IProjectImageService : ICrudService<ProjectImage>, IGetDataService<ProjectImage> { }
    public class ProjectImageService: IProjectImageService
    {
        private readonly IProjectImageRepository _projectImageRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProjectImageService(IProjectImageRepository projectImageRepository, IUnitOfWork unitOfWork)
        {
            _projectImageRepository = projectImageRepository;
            _unitOfWork = unitOfWork;
        }

        public ProjectImage Add(ProjectImage entity)
        {
           return _projectImageRepository.Add(entity);
        }

        public void Update(ProjectImage entity)
        {
            _projectImageRepository.Update(entity);
        }

        public void Delete(int id)
        {
            _projectImageRepository.Delete(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public ProjectImage FindById(int id)
        {
            return _projectImageRepository.GetSingleById(id);
        }

        public IEnumerable<ProjectImage> GetAll(string keyword = null)
        {
            var query = _projectImageRepository.GetAll();
            return query;
        }
    }
}
