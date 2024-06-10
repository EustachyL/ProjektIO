using EdukuJez.Repositories;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EdukuJez.Repositories
{
    public class GroupsRepository : ARepository<Group>
    {
        public GroupsRepository()
        {
            Table = Context.Groups;
        }

        public Group GetById(int id)
        {
            foreach (var row in Table)
            {
                if (row.Id == id) return row;
            }
            return null;
        }

        public bool IsGroupInDatabase(string name)
        {
            return Table.Any(x => x.Name == name);
        }

        public override void Delete(Group group)
        {
            if (group == null)
                return;

            var groupUsers = Context.GroupUsers.Where(gu => gu.Group.Id == group.Id).ToList();
            Context.GroupUsers.RemoveRange(groupUsers);

            Context.Groups.Remove(group);
            Context.SaveChanges();
        }
    }
}
