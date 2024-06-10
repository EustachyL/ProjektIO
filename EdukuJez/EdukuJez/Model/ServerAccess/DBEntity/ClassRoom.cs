using EdukuJez.Repositories;
using System;
using System.Collections.Generic;

namespace EdukuJez.Repositories
{
    public class ClassRoom : EntityBase
    {
        public string Number { get; set; }
        public string Desc { get; set; }
        public ICollection<ClassC> Classes { get; set; } = new List<ClassC>();
    }
}