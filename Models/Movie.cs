using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieOnlineAPI.Models
{
	public class Movie
	{
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public string Description { get; set; }
		[Required]
		[Range(1, int.MaxValue, ErrorMessage = "The Year field is required.")]
		public int ReleaseYear { get; set; }
		[Required]
		public string Actors { get; set; }

		public ICollection<MovieGenre> Genres { get; set; }
	}
}
