using System.Collections.Generic;
using System.Linq;
using HoangGia.Data.Infrastructure;
using HoangGia.Model.Entities;

namespace HoangGia.Data.Repositories
{
    public interface IApplicationUserRepository : IRepository<ApplicationUser>
    {
        IEnumerable<string> GetUserIdByGroupId(int id);
    }

    public class ApplicationUserRepository : RepositoryBase<ApplicationUser>, IApplicationUserRepository
    {
        public ApplicationUserRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
        public IEnumerable<string> GetUserIdByGroupId(int id)
        {
            var query = from aug in DbContext.ApplicationUserGroups
                        join ag in DbContext.ApplicationGroups
                        on aug.GroupId equals ag.Id
                        where aug.GroupId == id
                        select aug.UserId;
            return query.ToList(); 
                 

        }
    }
}