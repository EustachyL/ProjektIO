using EdukuJez.Repositories;
using EdukuJez.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Xunit;
using System;

namespace Testing
{
    public class CalendarTest
    {
        public CalendarTest()
        {
            var options = new DbContextOptionsBuilder<BaseContext>()
                .UseInMemoryDatabase(databaseName: "EdukuJezTestDb")
                .Options;

            BaseContext.options = options;
            BaseContext.testing = true;
        }

        [Fact]
        public void AddNewEntry_ShouldAddCalendarToDatabase()
        {
            var context = BaseContext.GetContext();
            // Arrange
            var repository = new CalendarRepository();

            var newCalendarEntry = new Calendar
            {
                Date = new DateTime(2023, 6, 18),
                Desc = "End of School Year"
            };

            // Act
            repository.Insert(newCalendarEntry);
            context.SaveChanges();

            // Assert
            var addedCalendarEntry = context.Calendar.FirstOrDefault(c => c.Desc == "End of School Year");
            Assert.NotNull(addedCalendarEntry);
        }
    }
}
