using AutoMapper;
using CourseLibraray.API.Models;
using CourseLibrary.API.Entities;

namespace CourseLibraray.API.Profiles
{
    public class CoursesProfile : Profile
    {
        public CoursesProfile()
        {
            CreateMap<Course, CourseDto>();
        }
    }
}
