using EdukuJez.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.UI.WebControls;

namespace EdukuJez.Model.ServerAccess.Repositories
{
    public class ClassRoomsRepository : ARepository<ClassRoom>
    {
        public ClassRoomsRepository()
        {
            Table = Context.ClassRoom;
        }

    }
}