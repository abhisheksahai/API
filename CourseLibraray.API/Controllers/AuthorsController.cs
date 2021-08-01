using AutoMapper;
using CourseLibraray.API.Helpers;
using CourseLibraray.API.Models;
using CourseLibraray.API.ResourceParameters;
using CourseLibrary.API.Entities;
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
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        [HttpHead]
        public ActionResult<IEnumerable<AuthorDto>> Authors([FromQuery] AuthorsResourceParameters authorsResourceParameters)
        {
            _logger.LogInformation(System.Reflection.MethodBase.GetCurrentMethod().Name);
            var authorsFromRepo = _courseLibraryRepository.GetAuthors(authorsResourceParameters);
            return Ok(_mapper.Map<IEnumerable<AuthorDto>>(authorsFromRepo));
        }

        [HttpGet("{authorId}", Name = "GetAuthor")]
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

        [HttpPost]
        public ActionResult<AuthorDto> CreateAuthor(AuthorForCreationDto authorForCreationDto)
        {
            _logger.LogInformation(System.Reflection.MethodBase.GetCurrentMethod().Name);
            var authorEntity     = _mapper.Map<Author>(authorForCreationDto);
            _courseLibraryRepository.AddAuthor(authorEntity);
            _courseLibraryRepository.Save();
            var authorToReturn = _mapper.Map<AuthorDto>(authorEntity);
            return CreatedAtRoute("GetAuthor", new { authorId = authorEntity.Id }, authorToReturn);
        }

        [HttpOptions]
        public IActionResult GetAuthorOptions()
        {
            Response.Headers.Add("Allow", "GET,OPTIONS,POST");
            return Ok();
        }
    }
}
