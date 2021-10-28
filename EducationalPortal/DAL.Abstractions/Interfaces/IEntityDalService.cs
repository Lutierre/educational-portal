using DTO.Models;

namespace DAL.Abstractions.Interfaces
{
    public interface IEntityDalService<T> where T : BaseEntityDto
    {
        void Add(T t);

        T Get(int id);

        void Update(T t);

        void Delete(int id);
    }
}
