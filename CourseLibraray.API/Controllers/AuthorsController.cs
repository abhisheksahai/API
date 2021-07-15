using AutoMapper;
using CourseLibraray.API.Helpers;
using CourseLibraray.API.Models;
using CourseLibrary.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;


/// <summary>
/// CourseLibraray.API.Controllers
/// </summary>
namespace CourseLibraray.API.Controllers
{
    [ApiController]
    [Route("api/authors")]
    public class AuthorsController : ControllerBase
    {
        private readonly ICourseLibraryRepository _courseLibraryRepository;
        private readonly ILogger<AuthorsController> _logger;
        private readonly IMapper _mapper;

        public AuthorsController(ICourseLibraryRepository courseLibraryRepository, ILogger<AuthorsController> logger, IMapper mapper)
        {
            _courseLibraryRepository = courseLibraryRepository ?? throw new ArgumentNullException(nameof(courseLibraryRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(courseLibraryRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(Mapper));
        }

        [HttpGet]
        public ActionResult<IEnumerable<AuthorDto>> Authors()
        {
            _logger.LogInformation(System.Reflection.MethodBase.GetCurrentMethod().Name);
            var authorsFromRepo = _courseLibraryRepository.GetAuthors();
            return Ok(_mapper.Map<IEnumerable<AuthorDto>>(authorsFromRepo));
        }

        [HttpGet("{authorId}")]
        public ActionResult<AuthorDto> Authors(Guid authorId)
        {
            _logger.LogInformation(System.Reflection.MethodBase.GetCurrentMethod().Name);
            var author = _courseLibraryRepository.GetAuthor(authorId);
            if (author == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<AuthorDto>(author));
        }
    }
}
