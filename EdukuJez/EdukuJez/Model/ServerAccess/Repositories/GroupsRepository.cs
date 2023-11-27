using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace EdukuJez.Repositories
{
    public class GroupsRepository : IRepository<Group>
    {
        List<Group> GroupList;
        const string CREATE_QUARY = "Select * from Groups";
        public GroupsRepository()
        {
            GroupList = new List<Group>();
            var response = ServerClient.StartConnection().ReturnDataReader(CREATE_QUARY);
            response.Wait();
            MapEntities(response.Result);
        }

        //implementacja interfejsu
        public Group GetById(int id)
        {
            return GroupList.First(x => x.Id == id);
        }
        public List<Group> GetAll()
        {
            return GroupList;
        }
        public void Create(Group entity)
        {
            GroupList.Add(entity);
            string query = "INSERT INTO Groups ( [Name], [Parent_Group]) " +
                             "VALUES ('" +entity.Name +"','" + entity.ParentGroup + "')";
            ServerClient.StartConnection().SendRequestToSqlServer(query);
        }
        public void Update(Group entity)
        {
            throw new NotImplementedException();
        }
        public void Delete(Group entity)
        {

        }
        //Mapowanie
        private void MapEntities(SqlDataReader reader)
        {
            while (reader.Read())
            {
                Group Group = new Group();
                
                Group.Id = reader.GetInt32(0);
                Group.Name = reader.GetString(1);
                if(reader.IsDBNull(2))
                    Group.ParentGroupID = -1;
                else
                    Group.ParentGroupID = reader.GetInt32(2);

                GroupList.Add(Group);
            }
            foreach (var g in GroupList)
            {
                if(g.ParentGroupID!=-1)
                g.ParentGroup = GroupList.First(x => x.Id == g.ParentGroupID).Name;
            }
        }
        //Wyszukiwanie linq na kolekcji repozytorium
        public Group GetByName(string name)
        {
            return GroupList.First(x => x.Name == name);
        }
    }
}