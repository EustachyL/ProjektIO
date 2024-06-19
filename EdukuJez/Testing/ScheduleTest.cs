using EdukuJez.Repositories;
using EdukuJez.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Xunit;
using System.Security;

namespace Testing
{
    public class ScheduleTest
    {

        public ScheduleTest()
        {
            var options = new DbContextOptionsBuilder<BaseContext>()
                .UseInMemoryDatabase(databaseName: "EdukuJezTestDb")
                .Options;

            BaseContext.options = options;
            BaseContext.testing = true;
        }

        [Fact]
        public void AddNewEntry_ShouldAddClassToDatabase()
        {

            var context = BaseContext.GetContext();
            // Arrange
            var repository = new ScheduleRepository();
            var repositoryUser = new UsersRepository();
            var repositoryGroup = new GroupsRepository();


            var NewUser = new User
            {
                UserName = "Adam",
                UserSurname = "Kowalski",
                UserLogin = "AAAAAAAAAAAAAA",
                UserPassword = "AAAAAAAAAAAAA"
            };

            var NewGroup = new Group
            {
                Name = "NewGroup",
                Educator = NewUser,

            };

            repositoryUser.Insert(NewUser);

            repositoryGroup.Insert(NewGroup);

            var Class = new ClassC
            {
                Warden = NewUser,
                Group = NewGroup,
                Hour = "8:00 – 8:45",
                Day = "Wtorek"

            };

            repository.AddNewEntry(Class);

           var test = context.Classes.FirstOrDefault(x=> x.Day == "Wtorek" && x.Hour == "8:00 – 8:45" && x.Warden.Id == 1 && x.Group.Id ==1);

            Assert.NotNull(test);
        }
    }
}