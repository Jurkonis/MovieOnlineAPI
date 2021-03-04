using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieOnlineAPI.Data;
using MovieOnlineAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace GenrieOnlineAPI.Controllers
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
		public IEnumerable<Genre> GetGenrie(string order, string search)
		{
			var genres = _context.Genres.Include(x => x.GenreMovies);

			if (order != null && search != null)
			{
				switch (order)
				{
					case "name_desc":
						return genres.Where(x => x.Name.Contains(search)).OrderByDescending(x => x.Name);
					default:
						return genres.Where(x => x.Name.Contains(search)).OrderBy(x => x.Name);
				}
			}
			if (order != null)
				switch (order)
				{
					case "name_desc":
						return genres.OrderByDescending(x => x.Name);
					default:
						return genres.OrderBy(x => x.Name);
				}
			if (search != null)
				return genres.Where(x => x.Name.Contains(search));

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
