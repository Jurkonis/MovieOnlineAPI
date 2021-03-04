using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieOnlineAPI.Models
{
	public class Genre
	{
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }

		public ICollection<MovieGenre> GenreMovies { get; set; }
	}
}
