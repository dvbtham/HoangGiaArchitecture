using HoangGia.Data.Infrastructure;
using HoangGia.Model.Entities;

namespace HoangGia.Repository.Repositories
{
    public interface IProjectCategoryRepository : IRepository<ProjectCategory> { }

    public class ProjectCategoryRepository : RepositoryBase<ProjectCategory>, IProjectCategoryRepository
    {
        public ProjectCategoryRepository(IDbFactory dbFactory)
            : base(dbFactory)
        {
        }
    }
}
