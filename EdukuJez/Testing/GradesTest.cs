using EdukuJez.Repositories;
using EdukuJez.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Xunit;

namespace Testing
{
    public class GradeTest
    {
        public GradeTest()
        {
            var options = new DbContextOptionsBuilder<BaseContext>()
                .UseInMemoryDatabase(databaseName: "EdukuJezTestDb")
                .Options;

            BaseContext.options = options;
            BaseContext.testing = true;
        }

        [Fact]
        public void AddNewEntry_ShouldAddGradeToDatabase()
        {
            var context = BaseContext.GetContext();
            // Arrange
            var repository = new GradesRepository();

            var newGrade = new Grade
            {
                GradeValue = 5,
                GradeType = "Nominal",
                GradeWeight = 2,
                Subject = new Subject { SubjectName = "Math" },
                Activity = new Activity { Name = "Homework" },
                Users = new User { UserLogin = "student1" },
                Teacher = new User { UserLogin = "teacher1" }
            };

            // Act
            repository.Insert(newGrade);
            context.SaveChanges();

            // Assert
            var addedGrade = context.Grades.FirstOrDefault(x => x == newGrade);
            Assert.NotNull(addedGrade);
        }
    }
}
