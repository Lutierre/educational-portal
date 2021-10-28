using AutoMapper;
using Core.Models;
using DAL.Abstractions.Interfaces;

namespace DAL.Services
{
    public class UserDalService : IEntityDalService<User>
    {
        private readonly IGenericDalService<UserDto> _genericDalService;

        private readonly IMapper _mapper;

        public UserDalService(IGenericDalService<UserDto> genericDalService)
        {
            _genericDalService = genericDalService;
            
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserDto, User>();
                cfg.CreateMap<User, UserDto>();
            });
                
            _mapper = config.CreateMapper();
        }
        
        public void Add(User user)
        {
            var dto = _mapper.Map<UserDto>(user);
            _genericDalService.Add(dto);
        }

        public User Get(int id)
        {
            var dto = _genericDalService.Get(id);
            var user = _mapper.Map<User>(dto);
            
            // TODO: enhance mapping / fill collections

            return user;
        }

        public void Update(User user)
        {
            var dto = _mapper.Map<UserDto>(user);
            _genericDalService.Update(dto);
            
            // TODO: process collections in User
        }

        public void Delete(int id)
        {
            _genericDalService.Delete(id);
        }
    }
}
