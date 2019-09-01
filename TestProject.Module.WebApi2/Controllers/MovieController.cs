//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Newtonsoft.Json;
//using TestProject.Application.Movies;
//using TestProject.Domain.Movies;

//namespace TestProject.Module.WebApi
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class MovieController : ControllerBase
//    {
//        private readonly IMovieRepository _movieRepository;

//        public MovieController(IMovieRepository movieRepository)
//        {
//            _movieRepository = movieRepository;
//        }

//        // GET api/movie
//        [HttpGet]
//        public async Task<IActionResult> Get()
//        {
//            var movies = await _movieRepository.GetListAsync();
//            if (movies == null || movies.Count == 0)
//                return NotFound();
//            return new JsonResult(movies);
//        }

//        // GET api/movie/5
//        [HttpGet("{id}")]
//        public async Task<IActionResult> GetById(int id)
//        {
//            var movie = await _movieRepository.GetAsync(id);
//            if (movie == null)
//                return NotFound();
//            return new JsonResult(movie);
//        }

//        // GET api/movie/5
//        [HttpPost]
//        public async Task<IActionResult> Create(string entityJson)
//        {
//            var movie = JsonConvert.DeserializeObject(entityJson) as Movie;
//            if (movie == null)
//                return BadRequest();
//            await _movieRepository.CreateAsync(movie);
//            return Ok();
//        }

//        // PUT api/movie/5
//        [HttpPut]
//        public async Task<IActionResult> Edit(string entityJson)
//        {
//            var movie = JsonConvert.DeserializeObject(entityJson) as Movie;
//            if (movie == null)
//                return BadRequest();
//            await _movieRepository.EditAsync(movie);
//            return Ok();
//        }

//        // DELETE 
//        // api/movie/5
//        [HttpDelete("{id}")]
//        public async Task Delete(int id)
//        {
//            await _movieRepository.DeleteAsync(id);
//        }
//    }
//}