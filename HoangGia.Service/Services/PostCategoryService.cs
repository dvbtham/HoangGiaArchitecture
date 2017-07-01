using HoangGia.Data.Repositories;
using HoangGia.Model.Entities;
using HoangGia.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using HoangGia.Data.Infrastructure;

namespace HoangGia.Service.Services
{
    public interface IPostCategoryService : ICrudService<PostCategory>, IGetDataService<PostCategory>
    {
        IEnumerable<PostCategory> GetAllByParentId(int parentId);
        void IsDeleted(int id);
    }

    public class PostCategoryService : IPostCategoryService
    {
        private readonly IPostCategoryRepository _postCategoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PostCategoryService(IPostCategoryRepository postCategoryRepository, IUnitOfWork unitOfWork)
        {
            _postCategoryRepository = postCategoryRepository;
            _unitOfWork = unitOfWork;
        }

        public PostCategory Add(PostCategory postCategory)
        {
            return _postCategoryRepository.Add(postCategory);
        }

        public void Update(PostCategory postCategory)
        {
            _postCategoryRepository.Update(postCategory);
        }

        public void Delete(int id)
        {
            _postCategoryRepository.Delete(id);
        }


        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public IEnumerable<PostCategory> GetAllByParentId(int parentId)
        {
            return _postCategoryRepository.GetMulti(x => x.ParentId == parentId);
        }


        public void IsDeleted(int id)
        {
            var postCategory = FindById(id);
            postCategory.IsDeleted = true;
            Update(postCategory);
        }

        public PostCategory FindById(int id)
        {
            return _postCategoryRepository.GetSingleById(id);
        }

        public IEnumerable<PostCategory> GetAll(string keyword = null)
        {
            var query = _postCategoryRepository.GetMulti(x => !x.IsDeleted);
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(x => x.Name.Contains(keyword) || x.Description.Contains(keyword));
            }
            return query;
        }
    }
}
