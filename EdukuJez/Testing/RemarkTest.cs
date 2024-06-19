using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EdukuJez.Repositories;
using EdukuJez.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Xunit;
using Microsoft.EntityFrameworkCore.Internal;

namespace Testing
{
    public class RemarkTest
    {
        private readonly BaseContext _context;
            public RemarkTest()
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
                var repository = new RemarkRepository();
                var repositoryUser = new UsersRepository();

            var Student = new User 
            {
                UserName = "Adam",
                UserSurname = "Kowalski",
                UserLogin = "AAAAAAAAA",
                UserPassword = "AAAAAAAAA"
            };

            var Submitter = new User
            {
                UserName = "NieAdam",
                UserSurname = "NieKowalski",
                UserLogin = "BBBBBB",
                UserPassword = "BBBBBB"
            };
            repositoryUser.Insert(Student);
            repositoryUser.Insert(Submitter);
            var sub = new Remark
            {
                   Student = Student,
                   Submitter = Submitter,
                   Contents ="opis"

    };
            repository.Insert(sub);
            var test = context.Remark.FirstOrDefault(x => x.Contents == "opis" && x.SubmitterId==2&& x.StudentId==1);

            Assert.NotNull(test);

        }
    }
}

