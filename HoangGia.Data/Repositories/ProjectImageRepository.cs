using HoangGia.Data.Infrastructure;
using HoangGia.Model.Entities;

namespace HoangGia.Repository.Repositories
{
    public interface IProjectImageRepository : IRepository<ProjectImage> { }

    public class ProjectImageRepository : RepositoryBase<ProjectImage>, IProjectImageRepository
    {
        public ProjectImageRepository(IDbFactory dbFactory)
            : base(dbFactory)
        {
        }
    }
}
