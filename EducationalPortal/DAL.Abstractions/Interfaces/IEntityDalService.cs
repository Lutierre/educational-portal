using Core.Models;
using DTO.Models;

namespace DAL.Abstractions.Interfaces
{
    public interface IEntityDalService<T> where T : BaseEntity
    {
        void Add(T t);

        T Get(int id);

        void Update(T t);

        void Delete(int id);
    }
}
