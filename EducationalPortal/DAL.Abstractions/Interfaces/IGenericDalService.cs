using DTO.Models;

namespace DAL.Abstractions.Interfaces
{
    public interface IGenericDalService<T> where T : BaseEntityDto
    {
        void Deserialize();

        void Serialize();
        
        void Add(T t);

        T Get(int id);

        void Update(T user);

        void Delete(int id);
    }
}
