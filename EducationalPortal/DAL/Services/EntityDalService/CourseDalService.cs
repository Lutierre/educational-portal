﻿using System;
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
        private readonly IGenericDtoService<CourseDto> _courseDtoService;
        private readonly IGenericDtoService<UserDto> _userDtoService; 
        private readonly IGenericDtoService<SkillDto> _skillDtoService;
        private readonly IGenericDtoService<MaterialDto> _materialDtoService;
        private readonly IGenericDtoService<CourseSkillDto> _courseSkillDtoService;
        private readonly IGenericDtoService<CourseMaterialDto> _courseMaterialDtoService;
        
        private readonly IMapper _mapper;

        public CourseDalService(IGenericDtoService<CourseDto> courseDtoService,
            IGenericDtoService<UserDto> userDtoService,
            IGenericDtoService<SkillDto> skillDtoService,
            IGenericDtoService<MaterialDto> materialDtoService, 
            IGenericDtoService<CourseSkillDto> courseSkillDtoService,
            IGenericDtoService<CourseMaterialDto> courseMaterialDtoService)
        {
            _courseDtoService = courseDtoService;
            _userDtoService = userDtoService;
            _skillDtoService = skillDtoService;
            _materialDtoService = materialDtoService;
            _courseSkillDtoService = courseSkillDtoService;
            _courseMaterialDtoService = courseMaterialDtoService;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CourseDto, Course>();
                cfg.CreateMap<Course, CourseDto>();
                cfg.CreateMap<UserDto, User>();
                cfg.CreateMap<MaterialDto, Material>();
            });
                
            _mapper = config.CreateMapper();
        }

        private Course Fetch(Course course)
        {
            course.Author = _mapper.Map<User>(_userDtoService.Get(course.AuthorId));
            
            var filteredSkills = 
                _courseSkillDtoService.Filter(courseSkillDto => courseSkillDto.CourseId == course.Id);

            course.Skills = new List<Skill>();

            foreach (var courseSkillDto in filteredSkills)
            {
                var skillDto = _skillDtoService.Get(courseSkillDto.SkillId);
                var skill = _mapper.Map<Skill>(skillDto);
                course.Skills.Add(skill);
            }
            
            var filteredMaterials = 
                _courseMaterialDtoService.Filter(courseMaterialDto => courseMaterialDto.CourseId == course.Id);

            course.Materials = new List<Material>();

            foreach (var courseMaterialDto in filteredMaterials)
            {
                var materialDto = _materialDtoService.Get(courseMaterialDto.MaterialId);
                var material = _mapper.Map<Material>(materialDto);
                course.Materials.Add(material);
            }

            return course;
        }
        
        public Course Add(Course course)
        {
            var dto = _mapper.Map<CourseDto>(course);
            dto.AuthorId = course.Author.Id;
            var id = _courseDtoService.Add(dto);
            var result = Get(id);

            return result;
        }

        public Course Get(int id)
        {
            var courseDto = _courseDtoService.Get(id);
            var course = Fetch(_mapper.Map<Course>(courseDto));
            
            return course;
        }

        public List<Course> Filter(Func<Course, bool> criteriaFunc)
        {
            var courseDtos = 
                _courseDtoService.Filter(courseDto => criteriaFunc(_mapper.Map<Course>(courseDto)));
            var filteredCourses = 
                courseDtos.Select(courseDto => Fetch(_mapper.Map<Course>(courseDto))).ToList();

            return filteredCourses;
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
