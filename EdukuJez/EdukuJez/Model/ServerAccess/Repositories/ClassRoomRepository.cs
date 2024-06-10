using EdukuJez.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.UI.WebControls;

namespace EdukuJez.Repositories
{
    public class ClassRoomsRepository : ARepository<ClassRoom>
    {
        public ClassRoomsRepository()
        {
            Table = Context.ClassRoom;
        }

        public void AddNewEntry(ClassRoom entry)
        {
            Insert(entry);
        }
        public void RemoveEntry(ClassRoom entry)
        {
            Delete(entry);
        }

        public void EditEntry(ClassRoom entry)
        {
            UpdateRow(entry);
        }
    }
}