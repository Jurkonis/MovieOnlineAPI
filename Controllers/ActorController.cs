using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using MovieOnlineAPI.Data;
using MovieOnlineAPI.Models;

namespace ActorOnlineAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ActorController : ControllerBase
	{
		//private readonly MovieOnlineContext _context;

		//public ActorController(MovieOnlineContext context)
		//{
		//	_context = context;
		//}

		//[HttpGet]
		//public IEnumerable<Actor> GetActor(string order, string search)
		//{
		//	var actors = _context.Actors;

		//	if (order != null && search != null)
		//	{
		//		switch (order)
		//		{
		//			case "name_desc":
		//				return actors.Where(x => x.FirstName.Contains(search) || x.LastName.Contains(search)).OrderByDescending(x => x.FirstName);
		//			default:
		//				return actors.Where(x => x.FirstName.Contains(search) || x.LastName.Contains(search)).OrderBy(x => x.FirstName);
		//		}
		//	}
		//	if (order != null)
		//		switch (order)
		//		{
		//			case "name_desc":
		//				return actors.OrderByDescending(x => x.FirstName);
		//			default:
		//				return actors.OrderBy(x => x.FirstName);
		//		}
		//	if (search != null)
		//		return actors.Where(x => x.FirstName.Contains(search) || x.LastName.Contains(search));

		//	return actors;
		//}

		//[HttpGet("{id}")]
		//public ActionResult GetActor(int id)
		//{
		//	Actor actor = _context.Actors.Find(id);

		//	if (actor == null)
		//	{
		//		return NotFound();
		//	}

		//	return Ok(actor);
		//}

		//[HttpPost]
		//public ActionResult PostActor(Actor actor)
		//{
		//	if (!ModelState.IsValid)
		//	{
		//		return BadRequest(ModelState);
		//	}

		//	_context.Actors.Add(actor);
		//	_context.SaveChanges();

		//	return Ok(actor);
		//}

		//[HttpPut]
		//public IActionResult PutActor(Actor actor)
		//{
		//	if (!ModelState.IsValid)
		//	{
		//		return BadRequest(ModelState);
		//	}

		//	if (!actorExists(actor.Id))
		//	{
		//		return BadRequest();
		//	}

		//	_context.Actors.Update(actor);
		//	_context.SaveChanges();

		//	return Ok(actor);
		//}

		//[HttpDelete("{id}")]
		//public IActionResult DeleteActor(int id)
		//{
		//	Actor actor = _context.Actors.Find(id);

		//	if (actor == null)
		//	{
		//		return NotFound();
		//	}

		//	_context.Actors.Remove(actor);
		//	_context.SaveChanges();

		//	return Ok(actor);
		//}

		//private bool actorExists(int id)
		//{
		//	return _context.Actors.Count(e => e.Id == id) > 0;
		//}
	}
}
