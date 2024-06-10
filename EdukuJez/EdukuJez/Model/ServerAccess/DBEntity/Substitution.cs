using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EdukuJez.Repositories
{
    [Table("Substitutions")]
    public class Substitution : EntityBase
    {
        public string Desc { get; set; }
        public ClassC Class { get; set; }
        public User SubTeacher { get; set; }

    }
}