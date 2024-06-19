using EdukuJez.Repositories;
using EdukuJez.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Xunit;
using Microsoft.EntityFrameworkCore.Internal;

namespace Testing
{
    public class SubstitutionTest
    {
        private readonly BaseContext _context;

        public SubstitutionTest()
        {
            var options = new DbContextOptionsBuilder<BaseContext>()
                .UseInMemoryDatabase(databaseName: "EdukuJezTestDb")
                .Options;

            BaseContext.options = options;
            BaseContext.testing = true;
        }

        [Fact]
        public void AddNewEntry_ShouldAddClassRoomToDatabase()
        {
            var context = BaseContext.GetContext();
            // Arrange
            var repository = new SubstitutionRepository();
            var repositoryClass = new ScheduleRepository();
            var repositoryUser = new UsersRepository();

            var NewUser = new User
            {
                UserName = "Adam",
                UserSurname = "Kowalski",
                UserLogin = "AAAAAAAAAAAAAA",
                UserPassword = "AAAAAAAAAAAAA"
            };

            repositoryUser.Insert(NewUser);

            var Class = new ClassC
            {
                Warden = NewUser,
                Hour = "8:00 – 8:45",
                Day = "Wtorek"

            };

            repositoryClass.Insert(Class);  

            var sub = new Substitution
            {
                Desc = "OpisTest",
                Class = Class,
                SubTeacher = NewUser
            };

            repository.Insert(sub);

            var test = repository.Table.FirstOrDefault(x => x.Desc == "OpisTest" && x.SubTeacher.Id == 1 && x.Class.Id ==1);

            Assert.NotNull(test);

        }
    }
}