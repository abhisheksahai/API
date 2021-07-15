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
        private ICourseLibraryRepository _courseLibraryRepository;
        private ILogger<AuthorsController> _logger;

        public AuthorsController(ICourseLibraryRepository courseLibraryRepository, ILogger<AuthorsController> logger)
        {
            _courseLibraryRepository = courseLibraryRepository ?? throw new ArgumentNullException(nameof(courseLibraryRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(courseLibraryRepository));
        }

        [HttpGet]
        public IActionResult Authors()
        {
            _logger.LogInformation(System.Reflection.MethodBase.GetCurrentMethod().Name);
            var authorsFromRepo = _courseLibraryRepository.GetAuthors();
            var authors = new List<AuthorDto>();
            foreach (var author in authorsFromRepo)
            {
                authors.Add(new AuthorDto()
                {
                    Id = author.Id,
                    Name = $"{author.FirstName} {author.LastName}",
                    Age = author.DateOfBirth.GetCurrentAge(),
                    MainCategory = author.MainCategory
                });
            }
            return Ok(authors);
        }

        [HttpGet("{authorId}")]
        public IActionResult Authors(Guid authorId)
        {
            _logger.LogInformation(System.Reflection.MethodBase.GetCurrentMethod().Name);
            var author = _courseLibraryRepository.GetAuthor(authorId);
            if (author == null)
            {
                return NotFound();
            }
            return Ok(author);
        }
    }
}
