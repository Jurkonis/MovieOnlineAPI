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
	public class MovieController : ControllerBase
	{
		private readonly MovieOnlineContext _context;

		public MovieController(MovieOnlineContext context)
		{
			_context = context;
		}

		[HttpGet]
		public IEnumerable<Movie> GetMovie(string order, string search)
		{
			var movies = _context.Movies.Include(x => x.Genres);

			if(order != null && search != null)
			{
				switch (order)
				{
					case "name_desc":
						return movies.Where(x => x.Name.Contains(search)).OrderByDescending(m => m.Name);
					case "date":
						return movies.Where(x => x.Name.Contains(search)).OrderBy(m => m.ReleaseYear);
					case "date_desc":
						return movies.Where(x => x.Name.Contains(search)).OrderByDescending(m => m.ReleaseYear);
					default:
						return movies.Where(x => x.Name.Contains(search)).OrderBy(m => m.Name);
				}
			}
			if(order != null)
				switch (order)
				{
					case "name_desc":
						return movies.OrderByDescending(m => m.Name);
					case "date":
						return movies.OrderBy(m => m.ReleaseYear);
					case "date_desc":
						return movies.OrderByDescending(m => m.ReleaseYear);
					default:
						return movies.OrderBy(m => m.Name);
				}
			if (search != null)
				return movies.Where(x => x.Name.Contains(search));

			return movies;
		}

		[HttpGet("{id}")]
		public ActionResult GetMovie(int id)
		{
			Movie movie = _context.Movies.Include(x => x.Genres).FirstOrDefault(x => x.Id == id);

			if (movie == null)
			{
				return NotFound();
			}

			return Ok(movie);
		}

		[HttpPost]
		public ActionResult PostMovie(Movie movie)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			_context.Movies.Add(movie);
			_context.SaveChanges();

			return Ok(movie);
		}

		//[HttpPost("actor")]
		//public ActionResult AddActorToMovie(MovieActor ob)
		//{
		//	Movie movie = _context.Movies.Find(ob.MovieId);

		//	Actor actor = _context.Actors.Find(ob.ActorId);

		//	if( movie == null || actor == null)
		//	{
		//		return NotFound();
		//	}

		//	_context.MovieActors.Add(ob);
		//	_context.SaveChanges();

		//	return Ok(ob);
		//}

		[HttpPost("genre")]
		public ActionResult AddGenreToMovie(MovieGenre ob)
		{
			Movie movie = _context.Movies.Find(ob.MovieId);

			Genre genre = _context.Genres.Find(ob.GenreId);

			if (movie == null || genre == null)
			{
				return NotFound();
			}

			_context.MovieGenres.Add(ob);
			_context.SaveChanges();

			return Ok(ob);
		}

		[HttpDelete("genre/{id}")]
		public ActionResult RemoveGenreToMovie(int id)
		{
			var genre = _context.MovieGenres.Find(id);

			if (genre == null)
			{
				return NotFound();
			}

			_context.MovieGenres.Remove(genre);
			_context.SaveChanges();

			return Ok(genre);
		}

		[HttpDelete("genres/{id}")]
		public ActionResult RemoveAllGenreToMovie(int id)
		{
			var movie = _context.Movies.Include(x => x.Genres).FirstOrDefault(x => x.Id == id);

			if (movie == null)
			{
				return NotFound();
			}

			foreach(MovieGenre genre in movie.Genres)
			{
				_context.MovieGenres.Remove(genre);
				_context.SaveChanges();
			}

			return Ok(movie);
		}

		[HttpPut("{id}")]
		public IActionResult PutMovie(int id, Movie movie)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (!movieExists(id))
			{
				return BadRequest();
			}

			movie.Id = id;

			_context.Movies.Update(movie);
			_context.SaveChanges();

			movie = _context.Movies.Include(x => x.Genres).FirstOrDefault(x => x.Id == id);

			return Ok(movie);
		}

		[HttpDelete("{id}")]
		public IActionResult DeleteMovie(int id)
		{
			Movie movie = _context.Movies.Find(id);

			if (movie == null)
			{
				return NotFound();
			}

			_context.Movies.Remove(movie);
			_context.SaveChanges();

			return Ok(movie);
		}

		private bool movieExists(int id)
		{
			return _context.Movies.Count(e => e.Id == id) > 0;
		}
	}
}
