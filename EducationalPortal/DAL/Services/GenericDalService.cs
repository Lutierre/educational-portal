using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DAL.Abstractions.Interfaces;
using DTO.Models;
using Newtonsoft.Json;

namespace DAL.Services
{
    public class GenericDalService<T> : IGenericDalService<T> where T : BaseEntityDto
    {
        private Dictionary<int, T> _entries;
        
        private readonly string _path = $"{AppDomain.CurrentDomain.BaseDirectory}/Json/{typeof(T).Name}.json";

        public void Deserialize()
        {
            _entries = JsonConvert.DeserializeObject<Dictionary<int, T>>(File.ReadAllText(_path));
        }

        public void Serialize()
        {
            var path = $"Json/{typeof(T).Name}.json";
            var output = JsonConvert.SerializeObject(_entries, Formatting.Indented);
            
            using (var sw = new StreamWriter(path))
            {
                sw.Write(output);
            }

            _entries.Clear();
        }

        public void Add(T t)
        {
            Deserialize();
            t.Id = _entries.Keys.DefaultIfEmpty(0).Max() + 1;
            _entries.Add(t.Id, t);
            Serialize();
        }

        public void AddMany(List<T> entries)
        {
            Deserialize();
            var newId = _entries.Keys.DefaultIfEmpty(0).Max() + 1;
            
            foreach (var entry in entries)
            {
                entry.Id = newId++;
                _entries.Add(entry.Id, entry);
            }
            
            Serialize();
        }

        public List<T> Filter(Func<T, bool> criteriaFunc)
        {
            Deserialize();
            var filteredEntries = _entries.Values.Where(criteriaFunc).ToList();
            _entries.Clear();
            
            return filteredEntries;
        }

        public T Get(int id) 
        {
            Deserialize();
            var entryDto = _entries[id];
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

        public void DeleteMany(Func<T, bool> criteriaFunc)
        {
            Deserialize();
            
            foreach (var dto in _entries.Values.Where(criteriaFunc))
            {
                _entries.Remove(dto.Id);
            }
            
            Serialize();
        }
    }
}
