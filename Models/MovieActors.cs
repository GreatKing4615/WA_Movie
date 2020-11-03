﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace kinopoisk2._0.Models
{
    public class MovieActors
    {
        public int ActorId { get; set; }
        public Actor Actor { get; set; }

        public int MovieId { get; set; }
       
        public Movie Movie { get; set; }
    }
}
