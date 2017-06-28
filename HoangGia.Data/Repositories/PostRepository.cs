using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using HoangGia.Data.Infrastructure;
using HoangGia.Model.Entities;

namespace HoangGia.Data.Repositories
{
    public interface IPostRepository : IRepository<Post>
    {
        IQueryable<Post> GetAllByTag(string tag, int pageIndex, int pageSize, out int totalRow);
    }

    public class PostRepository : RepositoryBase<Post>, IPostRepository
    {
        public PostRepository(IDbFactory dbFactory)
            : base(dbFactory)
        {
        }

        public IQueryable<Post> GetAllByTag(string tag, int pageIndex, int pageSize, out int totalRow)
        {
            var query = from p in DbContext.Posts
                        join pt in DbContext.PostTags
                        on p.Id equals pt.PostId
                        where pt.TagId == tag && p.IsDeleted == false
                        orderby p.CreatedDate descending
                        select p;
            totalRow = query.Count();
            query = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);

            return query;
        }
    }
}