using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI.WebControls;

namespace EdukuJez.Repositories
{
    public class Substitution : EntityBase
    {
        public string Desc { get; set; }
        public ClassC Class { get; set; }
        public User SubTeacher { get; set; }

    }
}