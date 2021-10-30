using System;
using System.Collections.Generic;
using Core.Models;

namespace DAL.Abstractions.Interfaces
{
    public interface IEntityDalService<T> where T : BaseEntity
    {
        void Add(T t);

        T Get(int id);

        List<T> Filter(Func<T, bool> criteriaFunc);

        void Update(T t);

        void Delete(int id);
    }
}
