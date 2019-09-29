using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TestProject.Application.Movies;
using TestProject.Domain.Movies;

namespace TestProject.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly ILogger<MovieController> _logger;
        private readonly IMovieRepository _movieRepository;

        public MovieController(ILogger<MovieController> logger, IMovieRepository movieRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _movieRepository = movieRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation(nameof(GetAll));
            var movies = await _movieRepository.GetListAsync();
            if (movies == null || movies.Count == 0)
            {
                _logger.LogWarning($"{nameof(GetAll)} - нет результатов");
                return NotFound();
            }
            return new JsonResult(movies);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingle(int id)
        {
            _logger.LogInformation(nameof(GetSingle));
            var movie = await _movieRepository.GetAsync(id);
            if (movie == null)
            {
                _logger.LogWarning($"{nameof(GetSingle)} - {id} - нет результатов");
                return NotFound();
            }
            return new JsonResult(movie);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Movie movie)
        {
            _logger.LogInformation(nameof(Create));
            await _movieRepository.CreateAsync(movie);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(Movie movie)
        {
            _logger.LogInformation(nameof(Edit));
            await _movieRepository.EditAsync(movie);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            _logger.LogInformation(nameof(Delete));
            await _movieRepository.DeleteAsync(id);
        }
    }
}