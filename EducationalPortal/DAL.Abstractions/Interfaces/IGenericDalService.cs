using System;
using System.Collections.Generic;
using DTO.Models;

namespace DAL.Abstractions.Interfaces
{
    public interface IGenericDalService
    {
        void Deserialize();

        void Serialize();
    }
    
    public interface IGenericDalService<T> : IGenericDalService where T : BaseEntityDto
    {
        int Add(T t);

        void AddMany(List<T> entries);

        List<T> Filter(Func<T, bool> criteriaFunc);
        
        T Get(int id);

        void Update(T user);

        void Delete(int id);

        void DeleteMany(Func<T, bool> criteriaFunc);
    }
}
