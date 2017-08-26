using System.Collections.Generic;
using System.Linq;
using HoangGia.Data.Infrastructure;
using HoangGia.Data.Repositories;
using HoangGia.Model.Entities;
using HoangGia.Services.Interfaces;

namespace HoangGia.Service.Services
{
    public interface IContactService : ICrudService<Contact>, IGetDataService<Contact>
    {
    }
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;
        private readonly IUnitOfWork _unitOfWork;
        public ContactService(IContactRepository contactRepository, IUnitOfWork unitOfWork)
        {
            _contactRepository = contactRepository;
            _unitOfWork = unitOfWork;
        }
        public Contact Add(Contact entity)
        {
           return _contactRepository.Add(entity);
        }

        public void Update(Contact entity)
        {
            _contactRepository.Update(entity);
        }
        
        public void Delete(int id)
        {
            _contactRepository.Delete(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public Contact FindById(int id)
        {
            return _contactRepository.GetSingleById(id);
        }

        public IEnumerable<Contact> GetAll(string keyword = null)
        {
            var query = _contactRepository.GetAll();
            if (!string.IsNullOrEmpty(keyword))
                query = query.Where(x => x.Name.Contains(keyword));
            return query;
        }
    }
}
