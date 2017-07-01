using System.Collections.Generic;
using System.Linq;
using HoangGia.Data.Infrastructure;
using HoangGia.Data.Repositories;
using HoangGia.Model.Entities;
using HoangGia.Services.Interfaces;

namespace HoangGia.Service.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PostService(IPostRepository postRepository, IUnitOfWork unitOfWork)
        {
            _postRepository = postRepository;
            _unitOfWork = unitOfWork;
        }
        public Post Add(Post entity)
        {
            return _postRepository.Add(entity);
        }

        public void Delete(int id)
        {
             _postRepository.Delete(id);
        }

        public Post FindById(int id)
        {
            return _postRepository.GetSingleById(id);
        }

        public IEnumerable<Post> GetAll(string keyword = null)
        {
            var query = _postRepository.GetAll();
            if (!string.IsNullOrEmpty(keyword))
                query = query.Where(x => x.Name.Contains(keyword));
            return query;
        }

        public void SaveChanges()
        {
           _unitOfWork.Commit();
        }

        public void MoveToTrash(int id)
        {
            var post = FindById(id);
            post.IsDeleted = true;
            Update(post);
        }

        public void Update(Post entity)
        {
            _postRepository.Update(entity);
        }
    }

    public interface IPostService : IGetDataService<Post>, ICrudService<Post>
    {
        void MoveToTrash(int id);
    }
}
