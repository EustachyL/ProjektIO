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

            _context = new BaseContext(options);
        }

        [Fact]
        public void AddNewEntry_ShouldAddClassRoomToDatabase()
        {
            var BaseContext = new BaseContext();
            var context = BaseContext.GetContext();
            // Arrange
            var repository = new SubstitutionRepository();

            var sub = new Substitution
            {
                Desc = "OpisTest"
            };

            var test = repository.Table.FirstOrDefault(x => x.Desc == "OpisTest");

            Assert.NotNull(test);

        }
    }
}