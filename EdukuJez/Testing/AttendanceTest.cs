using EdukuJez.Repositories;
using EdukuJez.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Xunit;
using System;

namespace Testing
{
    public class AttendanceTest
    {
        public AttendanceTest()
        {
            var options = new DbContextOptionsBuilder<BaseContext>()
                .UseInMemoryDatabase(databaseName: "EdukuJezTestDb")
                .Options;

            BaseContext.options = options;
            BaseContext.testing = true;
        }

        [Fact]
        public void AddNewEntry_ShouldAddAttendanceToDatabase()
        {
            var context = BaseContext.GetContext();
            // Arrange
            var repository = new AttendancesRepository();

            var newAttendance = new Attendance
            {
                Student = new User { UserLogin = "student1" }, 
                Date = DateTime.Now,
                Class = new ClassC {}, 
                Presence = "obecny"
            };

            // Act
            repository.Insert(newAttendance);
            context.SaveChanges();

            // Assert
            var addedAttendance = context.Attendances.FirstOrDefault(a => a == newAttendance);
            Assert.NotNull(addedAttendance);
        }
    }
}
