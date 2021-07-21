using AutoMapper;
using CourseLibraray.API.Helpers;
using CourseLibraray.API.Models;
using CourseLibrary.API.Entities;

namespace CourseLibraray.API.Profiles
{
    public class AuthorsProfile : Profile
    {
        public AuthorsProfile()
        {
            CreateMap<Author, AuthorDto>()
                .ForMember(
                dest => dest.Name,
                opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}")).
                ForMember(
                  dest => dest.Age,
                  opt => opt.MapFrom(src => src.DateOfBirth.GetCurrentAge())
                );
            CreateMap<AuthorForCreationDto, Author>();
        }
    }
}
