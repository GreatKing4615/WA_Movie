using System;
using System.Collections.Generic;

namespace kinopoisk.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string description { get; set; }
        public DateTime ReleaseDate { get; set; }

        public double Rating { get; set; }
        public List<MovieActors> Actors { get; set; }

        public Movie()
        {
            Actors = new List<MovieActors>();

        }
    }
}
