using DAL.Abstractions.Interfaces;
using DTO.Models;

namespace DAL.Services
{
    public class CourseDalService<T> : IEntityDalService<T> where T : BaseEntityDto
    {
        public void Add(T t)
        {
            throw new System.NotImplementedException();
        }

        public T Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Update(T t)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
