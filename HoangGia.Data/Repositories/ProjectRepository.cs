using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HoangGia.Data.Infrastructure;
using HoangGia.Model.Entities;

namespace HoangGia.Repository.Repositories
{
    public interface IProjectRepository : IRepository<Project> { }
    public class ProjectRepository : RepositoryBase<Project>, IProjectRepository
    {
        public ProjectRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
