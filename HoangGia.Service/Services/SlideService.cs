using System.Collections.Generic;
using HoangGia.Data.Infrastructure;
using HoangGia.Data.Repositories;
using HoangGia.Model.Entities;
using HoangGia.Services.Interfaces;

namespace HoangGia.Service.Services
{
    public interface ISlideService:  IGetDataService<Slide>, ICrudService<Slide> { }
    public class SlideService : ISlideService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISlideRepository _slideRepository;

        public SlideService(IUnitOfWork unitOfWork, ISlideRepository slideRepository)
        {
            _unitOfWork = unitOfWork;
            _slideRepository = slideRepository;
        }
        public Slide Add(Slide entity)
        {
            return _slideRepository.Add(entity);
        }

        public void Delete(int id)
        {
            _slideRepository.Delete(id);
        }

        public Slide FindById(int id)
        {
            return _slideRepository.GetSingleById(id);
        }

        public IEnumerable<Slide> GetAll(string keyword = null)
        {
            var query = _slideRepository.GetAll();
            return query;
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(Slide entity)
        {
            _slideRepository.Update(entity);
        }
    }
}
