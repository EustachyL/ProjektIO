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

            _context = new BaseContext(options);
        }

        [Fact]
        public void AddNewEntry_ShouldAddClassRoomToDatabase()
        {
            var BaseContext = new BaseContext();
            var context = BaseContext.GetContext();
            // Arrange
            var repository = new RemarkRepository();

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


            var sub = new Remark
            {
                StudentId = 0,

            };

           // var test = context.Remark.FirstOrDefault(x => x.Desc == "OpisTest");

           // Assert.NotNull(test);

        }
    }
}

