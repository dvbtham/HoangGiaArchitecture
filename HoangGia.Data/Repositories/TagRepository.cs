using HoangGia.Data.Infrastructure;
using HoangGia.Model.Entities;

namespace HoangGia.Data.Repositories
{
    public interface ITagRepository : IRepository<Tag> { }

    public class TagRepository : RepositoryBase<Tag>, ITagRepository
    {
        public TagRepository(IDbFactory dbFactory)
            : base(dbFactory)
        {
        }
    }
}