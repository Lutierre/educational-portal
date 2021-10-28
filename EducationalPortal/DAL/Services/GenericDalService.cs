using DAL.Abstractions.Interfaces;
using DTO.Models;

namespace DAL.Services
{
    public class GenericDalService<T> : IGenericDalService<T> where T : BaseEntity
    {
        public void Add(T t)
        {
            throw new System.NotImplementedException();
        }

        public T Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public T Update(T user)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
