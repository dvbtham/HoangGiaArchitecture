using HoangGia.Data.Infrastructure;
using HoangGia.Model.Entities;

namespace HoangGia.Data.Repositories
{
    public interface IPostCategoryRepository : IRepository<PostCategory> { }

    public class PostCategoryRepository : RepositoryBase<PostCategory>,IPostCategoryRepository
    {
        public PostCategoryRepository(IDbFactory dbFactory)
            : base(dbFactory)
        {
        }
    }
}