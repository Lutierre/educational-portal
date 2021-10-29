using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Core.Models;
using Core.Models.Materials;
using DAL.Abstractions.Interfaces;
using DTO.Models;
using DTO.Models.ManyToMany;
using DTO.Models.Materials;

namespace DAL.Services.EntityDalService
{
    public class CourseDalService : IEntityDalService<Course>
    {
        private readonly IGenericDalService<CourseDto> _courseDtoService;
        private readonly IGenericDalService<UserDto> _userDtoService; 
        private readonly IGenericDalService<SkillDto> _skillDtoService;
        private readonly IGenericDalService<MaterialDto> _materialDtoService;
        private readonly IGenericDalService<CourseSkillDto> _courseSkillDtoService;
        private readonly IGenericDalService<CourseMaterialDto> _courseMaterialDtoService;
        
        private readonly IMapper _mapper;

        public CourseDalService(IGenericDalService<CourseDto> genericDalService,
            IGenericDalService<UserDto> userDtoService,
            IGenericDalService<SkillDto> skillDtoService,
            IGenericDalService<MaterialDto> materialDtoService, 
            IGenericDalService<CourseSkillDto> courseSkillDtoService,
            IGenericDalService<CourseMaterialDto> courseMaterialDtoService)
        {
            _courseDtoService = genericDalService;
            _userDtoService = userDtoService;
            _skillDtoService = skillDtoService;
            _materialDtoService = materialDtoService;
            _courseSkillDtoService = courseSkillDtoService;
            _courseMaterialDtoService = courseMaterialDtoService;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CourseDto, Course>();
                cfg.CreateMap<Course, CourseDto>();
            });
                
            _mapper = config.CreateMapper();
        }
        
        public void Add(Course course)
        {
            var dto = _mapper.Map<CourseDto>(course);
            dto.AuthorId = course.Author.Id;
            _courseDtoService.Add(dto);
        }

        public Course Get(int id)
        {
            var dto = _courseDtoService.Get(id);
            var course = _mapper.Map<Course>(dto);

            course.Author = _mapper.Map<User>(_userDtoService.Get(dto.AuthorId));
            
            var filteredSkills = 
                _courseSkillDtoService.Filter(courseSkillDto => courseSkillDto.CourseId == id);

            course.Skills = new List<Skill>();

            foreach (var courseSkillDto in filteredSkills)
            {
                var skillDto = _skillDtoService.Get(courseSkillDto.SkillId);
                var skill = _mapper.Map<Skill>(skillDto);
                course.Skills.Add(skill);
            }
            
            var filteredMaterials = 
                _courseMaterialDtoService.Filter(courseMaterialDto => courseMaterialDto.CourseId == id);

            course.Materials = new List<Material>();

            foreach (var courseMaterialDto in filteredMaterials)
            {
                var materialDto = _materialDtoService.Get(courseMaterialDto.MaterialId);
                var material = _mapper.Map<Material>(materialDto);
                course.Materials.Add(material);
            }
            
            return course;
        }

        public void Update(Course course)
        {
            var dto = _mapper.Map<CourseDto>(course);
            dto.AuthorId = course.Author.Id;
            _courseDtoService.Update(dto);
            
            _courseSkillDtoService.DeleteMany(courseSkillDto => courseSkillDto.CourseId == course.Id);
            _courseMaterialDtoService.DeleteMany(courseMaterialDto => courseMaterialDto.CourseId == course.Id);

            var courseSkillDtos = course.Skills
                .Select(skill => new CourseSkillDto { CourseId = course.Id, SkillId = skill.Id })
                .ToList();

            _courseSkillDtoService.AddMany(courseSkillDtos);
            
            var courseMaterialDtos = course.Materials
                .Select(material => new CourseMaterialDto { CourseId = course.Id, MaterialId = material.Id })
                .ToList();

            _courseMaterialDtoService.AddMany(courseMaterialDtos);
        }

        public void Delete(int id)
        {
            _courseDtoService.Delete(id);
        }
    }
}
