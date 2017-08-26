using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using HoangGia.Data.Infrastructure;
using HoangGia.Data.Repositories;
using HoangGia.Model.Entities;
using HoangGia.Services.Interfaces;

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
                    var tagId = Alias(t);

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

        private string Alias(string input)
        {
            if (string.IsNullOrEmpty(input)) return "";
            input = input.Trim();
            for (var i = 0x20; i < 0x30; i++)
            {
                input = input.Replace(((char)i).ToString(), " ");
            }
            input = input.Replace(".", "-");
            input = input.Replace(" ", "-");
            input = input.Replace(",", "-");
            input = input.Replace(";", "-");
            input = input.Replace(":", "-");
            input = input.Replace("  ", "-");
            var regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");
            var str = input.Normalize(NormalizationForm.FormD);
            var str2 = regex.Replace(str, string.Empty).Replace('đ', 'd').Replace('Đ', 'D');
            while (str2.IndexOf("?", StringComparison.Ordinal) >= 0)
            {
                str2 = str2.Remove(str2.IndexOf("?", StringComparison.Ordinal), 1);
            }
            while (str2.Contains("--"))
            {
                str2 = str2.Replace("--", "-");
            }
            return str2.ToLower();
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
