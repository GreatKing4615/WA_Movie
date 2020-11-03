using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace kinopoisk2._0.Models
{
    public class Actor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Bio { get; set; }
        public DateTime Birthday { get; set; }
        public List<MovieActors> Movies { get; set; }
        public Actor()
        {
            Movies = new List<MovieActors>();

        }

    }
}
