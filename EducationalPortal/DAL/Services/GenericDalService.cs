using System.Collections.Generic;
using System.IO;
using DAL.Abstractions.Interfaces;
using DTO.Models;
using Newtonsoft.Json;

namespace DAL.Services
{
    public class GenericDalService<T> : IGenericDalService<T> where T : BaseEntityDto
    {
        private Dictionary<int, T> _entries;

        private readonly string _path = $"Json/{typeof(T).Name}.json";

        private readonly IGenericDalService<T> _genericDalService;
        
        public GenericDalService(IGenericDalService<T> genericDalService)
        {
            _genericDalService = genericDalService;
        }

        public void Deserialize()
        {
            _entries = JsonConvert.DeserializeObject<Dictionary<int, T>>(File.ReadAllText(_path));
        }

        public void Serialize()
        {
            var serializer = new JsonSerializer();
            var path = $"Json/{typeof(T).Name}.json";
            
            using (StreamWriter sw = new StreamWriter(path))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, _entries);
            }
            
            _entries.Clear();
        }

        public void Add(T t) // add element to collection
        {
            Deserialize();
            _entries.Add(t.Id, t);
            Serialize();
        }

        public T Get(int id) // get element from collection
        {
            Deserialize();
            T entryDto = _entries[id];
            _entries.Clear();
            
            return entryDto;
        }

        public void Update(T user)
        {
            Deserialize();
            _entries[user.Id] = user;
            Serialize();
        }

        public void Delete(int id)
        {
            Deserialize();
            _entries.Remove(id);
            Serialize();
        }
    }
}
