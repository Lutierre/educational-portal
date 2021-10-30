using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Core.Models;
using Core.Models.Materials;
using DTO.Models;
using DTO.Models.ManyToMany;
using DAL.Abstractions.Interfaces;
using DTO.Models.Materials;

namespace DAL.Services.EntityDalService
{
    public class UserDalService : IEntityDalService<User>
    {
        private readonly IGenericDalService<UserDto> _userDtoService;
        private readonly IGenericDalService<CourseDto> _courseDtoService;
        private readonly IGenericDalService<SkillDto> _skillDtoService;
        private readonly IGenericDalService<MaterialDto> _materialDtoService;
        private readonly IGenericDalService<UserSkillDto> _userSkillDtoService;
        private readonly IGenericDalService<UserCourseDto> _userCourseDtoService;
        private readonly IGenericDalService<UserMaterialDto> _userMaterialDtoService;

        private readonly IMapper _mapper;

        public UserDalService(IGenericDalService<UserDto> genericDalService, 
            IGenericDalService<CourseDto> courseDtoService,
            IGenericDalService<UserSkillDto> userSkillDtoService,
            IGenericDalService<UserCourseDto> userCourseDtoService,
            IGenericDalService<UserMaterialDto> userMaterialDtoService, 
            IGenericDalService<SkillDto> skillDtoService, 
            IGenericDalService<MaterialDto> materialDtoService)
        {
            _userDtoService = genericDalService;
            _courseDtoService = courseDtoService;
            _userSkillDtoService = userSkillDtoService;
            _userCourseDtoService = userCourseDtoService;
            _userMaterialDtoService = userMaterialDtoService;
            _skillDtoService = skillDtoService;
            _materialDtoService = materialDtoService;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserDto, User>();
                cfg.CreateMap<User, UserDto>();
                cfg.CreateMap<CourseDto, Course>();
                cfg.CreateMap<UserSkillDto, UserSkill>();
            });
                
            _mapper = config.CreateMapper();
        }
        
        public User Add(User user)
        {
            var dto = _mapper.Map<UserDto>(user);
            var id = _userDtoService.Add(dto);
            var result = Get(id);

            return result;
        }

        public User Get(int id)
        {
            var dto = _userDtoService.Get(id);
            var user = _mapper.Map<User>(dto);

            var filteredCourses = 
                _userCourseDtoService.Filter(userCourseDto => userCourseDto.UserId == id);

            user.CurrentCourses = new List<Course>();

            foreach (var userCourseDto in filteredCourses)
            {
                var courseDto = _courseDtoService.Get(userCourseDto.CourseId);
                var course = _mapper.Map<Course>(courseDto);
                user.CurrentCourses.Add(course);
            }
            
            var filteredSkills = 
                _userSkillDtoService.Filter(userSkillDto => userSkillDto.UserId == id);

            user.Skills = new List<UserSkill>();

            foreach (var userSkillDto in filteredSkills)
            {
                var skillDto = _skillDtoService.Get(userSkillDto.SkillId);
                var skill = _mapper.Map<Skill>(skillDto);
                var userSkill = _mapper.Map<UserSkill>(userSkillDto);
                userSkill.Skill = skill;
                user.Skills.Add(userSkill);
            }
            
            var filteredMaterials = 
                _userMaterialDtoService.Filter(userMaterialDto => userMaterialDto.UserId == id);

            user.Materials = new List<Material>();

            foreach (var userMaterialDto in filteredMaterials)
            {
                var materialDto = _materialDtoService.Get(userMaterialDto.MaterialId);
                var material = _mapper.Map<Material>(materialDto);
                user.Materials.Add(material);
            }
            
            return user;
        }

        public List<User> Filter(Func<User, bool> criteriaFunc)
        {
            var userDtos = 
                _userDtoService.Filter(userDto => criteriaFunc(_mapper.Map<User>(userDto)));
            var filteredUsers = userDtos.Select(userDto => _mapper.Map<User>(userDto)).ToList();

            return filteredUsers;
        }

        public void Update(User user)
        {
            var dto = _mapper.Map<UserDto>(user);
            _userDtoService.Update(dto);
            
            _userCourseDtoService.DeleteMany(userCourseDto => userCourseDto.UserId == user.Id);
            _userSkillDtoService.DeleteMany(userSkillDto => userSkillDto.UserId == user.Id);
            _userMaterialDtoService.DeleteMany(userMaterialDto => userMaterialDto.UserId == user.Id);

            var userCourseDtos = user.CurrentCourses
                .Select(course => new UserCourseDto { UserId = user.Id, CourseId = course.Id })
                .ToList();

            _userCourseDtoService.AddMany(userCourseDtos);

            var userSkillDtos = user.Skills
                .Select(skill => new UserSkillDto { UserId = user.Id, SkillId = skill.Id, Points = skill.Points })
                .ToList();

            _userSkillDtoService.AddMany(userSkillDtos);
            
            var userMaterialDtos = user.Materials
                .Select(material => new UserMaterialDto { UserId = user.Id, MaterialId = material.Id })
                .ToList();

            _userMaterialDtoService.AddMany(userMaterialDtos);
        }

        public void Delete(int id)
        {
            _userDtoService.Delete(id);
        }
    }
}
