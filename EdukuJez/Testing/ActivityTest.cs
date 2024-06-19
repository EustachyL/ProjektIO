using EdukuJez.Repositories;
using EdukuJez.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Xunit;

namespace Testing
{
    public class ActivityTest
    {
        public ActivityTest()
        {
            var options = new DbContextOptionsBuilder<BaseContext>()
                .UseInMemoryDatabase(databaseName: "EdukuJezTestDb")
                .Options;

            BaseContext.options = options;
            BaseContext.testing = true;
        }

        [Fact]
        public void AddNewEntry_ShouldAddActivityToDatabase()
        {
            var context = BaseContext.GetContext();
            // Arrange
            var repository = new ActivitiesRepository();

            var newActivity = new Activity
            {
                Name = "Homework",
                IsFinalGrade = false,
                HasSubmissions = true
            };

            // Act
            repository.Insert(newActivity);
            context.SaveChanges();

            // Assert
            var addedActivity = context.Activities.FirstOrDefault(a => a.Name == "Homework");
            Assert.NotNull(addedActivity);
        }
    }

}