using Microsoft.AspNetCore.Mvc;
using Movies.Api.Mapping;
using Movies.Application.Models;
using Movies.Application.Repositories;
using Movies.Contracts.Requests;


namespace Movies.Api.Controllers
{
    [ApiController]
    //[Route("api")]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieRepository _movierepository;
        public MoviesController(IMovieRepository movieRepository)
        {
            _movierepository = movieRepository;
        }

        [HttpPost(ApiEndPoints.Movies.Create)]
        public async Task<IActionResult> Create([FromBody] CreateMovieRequest request)
        {
            //var movie = new Movie
            //{
            //    Id = Guid.NewGuid(),
            //    Title = request.Title,
            //    YearOfRelease = request.YearOfRelease,
            //    Genres=request.Genres.ToList(),
            //};
            var movie = request.MapToMovie();
            await _movierepository.CreateAsync(movie);
            //return Ok(movie);
            return CreatedAtAction(nameof(Get), new { id = movie.Id }, movie);
            //return Created($"/api/movies/{movie.Id}", movie);

        }

        [HttpGet(ApiEndPoints.Movies.Get)]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var movie = await _movierepository.GetByIdAsync(id);
            if (movie is null)
            {
                return NotFound();
            }
            return Ok(movie.MapToResponse());
        }

        //[HttpGet(ApiEndPoints.Movies.Get)]
        //public async Task<IActionResult> Get([FromRoute] string idorslu)
        //{
        //    var movie=Guid.TryParse(idorslu, out var id) ?
        //        await _movierepository.GetByIdAsync(id)
        //        :await _movierepository.GetBySlugAsync(idorslu);
        //    if (movie is null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(movie.MapToResponse());
        //}

        [HttpGet(ApiEndPoints.Movies.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            var movies = await _movierepository.GetAllAsync();
            return Ok(movies.MapToResponse());
        }

        [HttpPut(ApiEndPoints.Movies.Update)]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateMovieRequest request)
        {
            var movie = request.MapToMovie(id);
            var updated = await _movierepository.UpdateAsync(movie);
            if (!updated)
            {
                return NotFound();
            }


            return Ok(movie.MapToResponse());
        }

        [HttpDelete(ApiEndPoints.Movies.Delete)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var delete=await _movierepository.DeleteByIdAsync(id);
            if (!delete)
            {
                return NotFound();
            }

            return Ok();

        }

    }
}
