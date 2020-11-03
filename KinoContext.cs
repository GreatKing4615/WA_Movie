using kinopoisk2._0.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kinopoisk2._0
{
    public class KinoContext:DbContext
    {
        public KinoContext(DbContextOptions<KinoContext> options) : base(options)
        {

        }


        public DbSet<User> Users { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Actor> Actors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MovieActors>().HasKey(u => new { u.ActorId, u.MovieId });

            modelBuilder.Entity<MovieActors>()
            .HasOne(a => a.Actor)
            .WithMany(am => am.Movies)
            .HasForeignKey(us => us.ActorId)
            .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<MovieActors>()
               .HasOne(u => u.Movie)
               .WithMany(uw => uw.Actors)
               .HasForeignKey(us => us.MovieId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
