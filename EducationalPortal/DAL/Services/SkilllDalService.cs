using AutoMapper;
using Core.Models;
using DAL.Abstractions.Interfaces;

namespace DAL.Services
{
    public class SkillDalService : IEntityDalService<Skill>
    {
        private readonly IGenericDalService<SkillDto> _genericDalService;

        private readonly IMapper _mapper;

        public SkillDalService(IGenericDalService<SkillDto> genericDalService)
        {
            _genericDalService = genericDalService;
            
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SkillDto, Skill>();
                cfg.CreateMap<Skill, SkillDto>();
            });
                
            _mapper = config.CreateMapper();
        }
        
        public void Add(Skill skill)
        {
            var dto = _mapper.Map<SkillDto>(skill);
            _genericDalService.Add(dto);
        }

        public Skill Get(int id)
        {
            var dto = _genericDalService.Get(id);
            var skill = _mapper.Map<Skill>(dto);
            
            // TODO: enhance mapping / fill collections

            return skill;
        }

        public void Update(Skill skill)
        {
            var dto = _mapper.Map<SkillDto>(skill);
            _genericDalService.Update(dto);
            
            // TODO: process collections in Skill
        }

        public void Delete(int id)
        {
            _genericDalService.Delete(id);
        }
    }
}
