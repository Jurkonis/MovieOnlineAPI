using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieOnlineAPI.Data;
using MovieOnlineAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace MovieOnlineAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class GenreController : ControllerBase
	{
		private readonly MovieOnlineContext _context;

		public GenreController(MovieOnlineContext context)
		{
			_context = context;
		}

		[HttpGet]
		public IEnumerable<Genre> GetGenrie()
		{
			var genres = _context.Genres.Include(x => x.GenreMovies);

			return genres;
		}

		[HttpPost("{left}")]
		public IEnumerable<Genre> GetLEftGenrie(List<MovieGenre> movieGenres)
		{
			var genres = _context.Genres.Include(x => x.GenreMovies).ToList();

			foreach (MovieGenre movieGenre in movieGenres)
			{
				var genre = _context.Genres.Find(movieGenre.GenreId);
				genres.Remove(genre);
			}

			return genres;
		}

		[HttpGet("{id}")]
		public ActionResult GetGenrie(int id)
		{
			Genre genre = _context.Genres.Include(x => x.GenreMovies).FirstOrDefault(x => x.Id == id);

			if (genre == null)
			{
				return NotFound();
			}

			return Ok(genre);
		}

		[HttpPost]
		public ActionResult PostGenrie(Genre genre)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var ob = _context.Genres.Where(x => x.Name.ToLower() == genre.Name.ToLower()).FirstOrDefault();

			if (ob != null)
			{
				return BadRequest("Genre name taken");
			}

			_context.Genres.Add(genre);
			_context.SaveChanges();

			genre = _context.Genres.Include(x => x.GenreMovies).FirstOrDefault(x => x.Id == genre.Id);

			return Ok(genre);
		}

		[HttpPut("{id}")]
		public IActionResult PutGenrie(int id, Genre genre)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (!movieExists(id))
			{
				return BadRequest();
			}

			genre.Id = id;

			_context.Genres.Update(genre);
			_context.SaveChanges();

			return Ok(genre);
		}

		[HttpDelete("{id}")]
		public IActionResult DeleteGenrie(int id)
		{
			Genre genre = _context.Genres.Find(id);

			if (genre == null)
			{
				return NotFound();
			}

			_context.Genres.Remove(genre);
			_context.SaveChanges();

			return Ok(genre);
		}

		private bool movieExists(int id)
		{
			return _context.Genres.Count(e => e.Id == id) > 0;
		}
	}
}
