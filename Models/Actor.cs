using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieOnlineAPI.Models
{
	public class Actor
	{
		public int Id { get; set; }
		[Required]
		public string FirstName { get; set; }
		[Required]
		public string LastName { get; set; }


		public ICollection<MovieActor> ActorMovies { get; set; }
	}
}
