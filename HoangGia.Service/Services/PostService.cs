using System.Collections.Generic;
using System.Linq;
using HoangGia.Data.Infrastructure;
using HoangGia.Data.Repositories;
using HoangGia.Model.Entities;
using HoangGia.Services.Interfaces;
using HoangGia.Utilities.Helpers;

namespace HoangGia.Service.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IPostTagRepository _postTagRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PostService(IPostRepository postRepository, IPostTagRepository postTagRepository, ITagRepository tagRepository, IUnitOfWork unitOfWork)
        {
            _postRepository = postRepository;
            _postTagRepository = postTagRepository;
            _tagRepository = tagRepository;
            _unitOfWork = unitOfWork;
        }
        public Post Add(Post entity)
        {
            var post = _postRepository.Add(entity);
            SaveChanges();
            if (!string.IsNullOrEmpty(post.Tags))
            {
                var tags = post.Tags.Split(',');
                foreach (var t in tags)
                {
                    var tagId = AliasHelper.Alias(t);

                    if (_tagRepository.Count(x => x.Id == tagId) == 0)
                    {
                        var tag = new Tag
                        {
                            Id = tagId,
                            Name = t,
                            Type = "post"
                        };
                        _tagRepository.Add(tag);
                    }

                    var postTag = new PostTag
                    {
                        PostId = post.Id,
                        TagId = tagId
                    };
                    _postTagRepository.Add(postTag);
                }
            }
            return post;
        }

        public void Delete(int id)
        {
            _postRepository.Delete(id);
        }

        public Post FindById(int id)
        {
            return _postRepository.GetSingleById(id);
        }
        public Post FindById(long id)
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

        public IEnumerable<Post> ActivingPosts(string q, string[] includes = null)
        {
            var query = _postRepository.GetMulti(x => x.IsDeleted == false, includes);
            if (!string.IsNullOrWhiteSpace(q))
                query = query.Where(x => x.Name.ToLower().Contains(q.ToLower()) || x.Id.ToString().Contains(q) || x.Content.ToLower().Contains(q.ToLower()));
            return query;
        }

        public IEnumerable<Post> PagingPosts(string q, int page, int pageSize, out int totalRow, string[] includes = null)
        {
            var query = _postRepository.GetMulti(x => x.IsDeleted == false, includes);
            if (!string.IsNullOrEmpty(q))
                query = query.Where(x => x.Name.ToLower().Contains(q.ToLower()) || x.Content.Contains(q));
            totalRow = query.Count();
            return query.OrderByDescending(x => x.Id).Skip((page - 1) * pageSize).Take(pageSize);
        }

        public void Update(Post entity)
        {
            _postRepository.Update(entity);
        }

        public void IncreaseView(int id)
        {
            var post = FindById(id);
            post.ViewCount += 1;
            SaveChanges();
        }
    }

    public interface IPostService : IGetDataService<Post>, ICrudService<Post>
    {
        void MoveToTrash(int id);
        Post FindById(long id);
        void IncreaseView(int id);
        IEnumerable<Post> ActivingPosts(string q, string[] includes = null);
        IEnumerable<Post> PagingPosts(string q, int page, int pageSize, out int totalRow, string[] includes = null);
    }
}
