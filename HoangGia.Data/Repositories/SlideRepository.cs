using HoangGia.Data.Infrastructure;
using HoangGia.Model.Entities;

namespace HoangGia.Data.Repositories
{
    public interface ISlideRepository : IRepository<Slide> { }
    public class SlideRepository : RepositoryBase<Slide>, ISlideRepository
    {
        public SlideRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
