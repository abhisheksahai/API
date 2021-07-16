using AutoMapper;
using CourseLibraray.API.Models;
using CourseLibrary.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseLibraray.API.Controllers
{

    [ApiController]
    [Route("api/authors/{authorId}/courses")]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseLibraryRepository _courseLibraryRepository;
        private readonly ILogger<AuthorsController> _logger;
        private readonly IMapper _mapper;

        public CoursesController(ICourseLibraryRepository courseLibraryRepository, ILogger<AuthorsController> logger, IMapper mapper)
        {
            _courseLibraryRepository = courseLibraryRepository ?? throw new ArgumentNullException(nameof(courseLibraryRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(courseLibraryRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public ActionResult<IEnumerable<CourseDto>> Courses(Guid authorId) 
        { 
            if(!_courseLibraryRepository.AuthorExists(authorId))
            {
                return NotFound();
            }
            var course = _courseLibraryRepository.GetCourses(authorId);
            return Ok(_mapper.Map<IEnumerable<CourseDto>>(course));
        }

        [HttpGet("{courseId}")]
        public ActionResult<IEnumerable<CourseDto>> Courses(Guid authorId, Guid courseId)
        {
            if (!_courseLibraryRepository.AuthorExists(authorId))
            {
                return NotFound();
            }
            var course = _courseLibraryRepository.GetCourse(authorId,courseId);
            if(course==null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<CourseDto>(course));
        }

    }
}
