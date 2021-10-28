using AutoMapper;
using Core.Models;
using DAL.Abstractions.Interfaces;
using DTO.Models;

namespace DAL.Services.EntityDalService
{
    public class CourseDalService : IEntityDalService<Course>
    {
        private readonly IGenericDalService<CourseDto> _genericDalService;

        private readonly IMapper _mapper;

        public CourseDalService(IGenericDalService<CourseDto> genericDalService)
        {
            _genericDalService = genericDalService;
            
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
            _genericDalService.Add(dto);
        }

        public Course Get(int id)
        {
            var dto = _genericDalService.Get(id);
            var course = _mapper.Map<Course>(dto);
            
            // TODO: enhance mapping / fill collections
            
            return course;
        }

        public void Update(Course course)
        {
            var dto = _mapper.Map<CourseDto>(course);
            _genericDalService.Update(dto);
            
            // TODO: process collections in Course
        }

        public void Delete(int id)
        {
            _genericDalService.Delete(id);
        }
    }
}
