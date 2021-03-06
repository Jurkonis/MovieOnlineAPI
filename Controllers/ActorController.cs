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
	public class ActorController : ControllerBase
	{
		private readonly MovieOnlineContext _context;

		public ActorController(MovieOnlineContext context)
		{
			_context = context;
		}

		[HttpGet]
		public IEnumerable<Actor> GetActor()
		{
			var actors = _context.Actors.Include(x => x.ActorMovies);

			return actors;
		}

		[HttpPost("{left}")]
		public IEnumerable<Actor> GetLEftActor(List<MovieActor> movieActors)
		{
			var actors = _context.Actors.Include(x => x.ActorMovies).ToList();

			foreach (MovieActor movieActor in movieActors)
			{
				var actor = _context.Actors.Find(movieActor.ActorId);
				actors.Remove(actor);
			}

			return actors;
		}

		[HttpGet("{id}")]
		public ActionResult GetActor(int id)
		{
			Actor actor = _context.Actors.Include(x => x.ActorMovies).FirstOrDefault(x => x.Id == id);

			if (actor == null)
			{
				return NotFound();
			}

			return Ok(actor);
		}

		[HttpPost]
		public ActionResult PostActor(Actor actor)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var ob = _context.Actors.Where(x => x.FirstName.ToLower() == actor.FirstName.ToLower() && x.LastName.ToLower() == actor.LastName.ToLower()).FirstOrDefault();

			if (ob != null)
			{
				return BadRequest("Actor name taken");
			}

			_context.Actors.Add(actor);
			_context.SaveChanges();

			actor = _context.Actors.Include(x => x.ActorMovies).FirstOrDefault(x => x.Id == actor.Id);

			return Ok(actor);
		}

		[HttpPut("{id}")]
		public IActionResult PutActor(int id, Actor actor)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (!movieExists(id))
			{
				return BadRequest();
			}

			actor.Id = id;

			_context.Actors.Update(actor);
			_context.SaveChanges();

			return Ok(actor);
		}

		[HttpDelete("{id}")]
		public IActionResult DeleteActor(int id)
		{
			Actor actor = _context.Actors.Find(id);

			if (actor == null)
			{
				return NotFound();
			}

			_context.Actors.Remove(actor);
			_context.SaveChanges();

			return Ok(actor);
		}

		private bool movieExists(int id)
		{
			return _context.Actors.Count(e => e.Id == id) > 0;
		}
	}
}
