using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore;
using MovieOnlineAPI.Models;

namespace MovieOnlineAPI.Data
{
	public class MovieOnlineContext : DbContext
	{
		public MovieOnlineContext(DbContextOptions<MovieOnlineContext> options) : base(options) { }

		public DbSet<Movie> Movies { get; set; }

		public DbSet<Genre> Genres { get; set; }

		public DbSet<MovieGenre> MovieGenres { get; set; }

	}
}
