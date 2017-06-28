using HoangGia.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HoangGia.Data.Repositories;
using HoangGia.Model.Entities;

namespace HoangGia.Repository.Repositories
{
    public interface IConsultantRepository : IRepository<Consultant> { }
    public class ConsultantRepository : RepositoryBase<Consultant>, IConsultantRepository
    {
        public ConsultantRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
