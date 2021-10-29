using AutoMapper;
using Core.Models;
using DAL.Abstractions.Interfaces;
using DTO.Models;

namespace DAL.Services.EntityDalService
{
    public class SkillDalService : IEntityDalService<Skill>
    {
        private readonly IGenericDalService<SkillDto> _skillDtoService;

        private readonly IMapper _mapper;

        public SkillDalService(IGenericDalService<SkillDto> skillDtoService)
        {
            _skillDtoService = skillDtoService;
            
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
            _skillDtoService.Add(dto);
        }

        public Skill Get(int id)
        {
            var dto = _skillDtoService.Get(id);
            var skill = _mapper.Map<Skill>(dto);
            
            return skill;
        }

        public void Update(Skill skill)
        {
            var dto = _mapper.Map<SkillDto>(skill);
            _skillDtoService.Update(dto);
        }

        public void Delete(int id)
        {
            _skillDtoService.Delete(id);
        }
    }
}
