using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoangGia.Services.Interfaces
{
    public interface ICrudService<T> where T : class
    {
        T Add(T entity);
        void Update(T entity);
        void Delete(int id);
        void SaveChanges();

    }
}
