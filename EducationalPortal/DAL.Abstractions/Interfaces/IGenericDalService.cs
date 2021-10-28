using DTO.Models;

namespace DAL.Abstractions.Interfaces
{
    public interface IGenericDalService<T> where T : BaseEntity
    {
        void Add(T t);

        T Get(int id);

        T Update(T user);

        void Delete(int id);
    }
}
