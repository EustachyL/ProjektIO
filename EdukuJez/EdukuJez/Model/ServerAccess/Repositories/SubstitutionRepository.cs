using EdukuJez.Repositories;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EdukuJez.Repositories
{
    public class SubstitutionRepository : ARepository<Substitution>
    {
        public SubstitutionRepository()
        {
            Table = Context.Substitutions;
        }
    }
}