using System.Collections.Generic;
using HoangGia.Data.Repositories;
using HoangGia.Model.Entities;

namespace HoangGia.Service.Services
{
    public interface ITagService
    {
        IEnumerable<Tag> GetAll();
    }
    public class TagService : ITagService
    {
        private readonly ITagRepository _tagRepository;

        public TagService(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }
        public IEnumerable<Tag> GetAll()
        {
            return _tagRepository.GetAll();
        }
    }
}
