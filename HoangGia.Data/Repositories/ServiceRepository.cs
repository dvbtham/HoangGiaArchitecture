using HoangGia.Data.Infrastructure;
using HoangGia.Model.Entities;

namespace HoangGia.Repository.Repositories
{
    public interface IServiceRepository : IRepository<Service>
    {
    }
    public class ServiceRepository : RepositoryBase<Service>, IServiceRepository
    {
        public ServiceRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
