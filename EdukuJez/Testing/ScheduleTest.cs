using EdukuJez.Repositories;
using EdukuJez.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Xunit;

namespace EdukuJez.Tests
{
    public class ScheduleTest
    {
        private readonly BaseContext _context;

        public ScheduleTest()
        {
            var options = new DbContextOptionsBuilder<BaseContext>()
                .UseInMemoryDatabase(databaseName: "EdukuJezTestDb")
                .Options;

            _context = new BaseContext(options);
        }

        [Fact]
        public void AddNewEntry_ShouldAddClassToDatabase()
        {
            var BaseContext = new BaseContext();
            var context = BaseContext.GetContext();
            // Arrange
            var repository = new ScheduleRepository();

            var Class = new ClassC
            {
                Hour = "8:00 – 8:45",
                Day = "Wtorek"

            };


           var test = context.Classes.FirstOrDefault(x=> x.Day == "Wtorek" && x.Hour == "8:00 – 8:45");

            Assert.NotNull(test);
        }
    }
}