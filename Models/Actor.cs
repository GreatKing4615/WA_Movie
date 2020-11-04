using System;
using System.Collections.Generic;

namespace kinopoisk.Models
{
    public class Actor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Bio { get; set; }
        public DateTime Birthday { get; set; }
        public List<MovieActors> Movies { get; set; }
        public double Rating { get; set; }
        public Actor()
        {
            Movies = new List<MovieActors>();
            Rating = 0;

        }

    }
}
