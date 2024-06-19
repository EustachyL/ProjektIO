using EdukuJez.Model.ServerAccess.Repositories;
using EdukuJez.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;


namespace Testing
{
    public class SubjectTest
    {

        private  SubjectsRepository _repository;
        private  GroupsRepository _repositoryGroup;
        private  BaseContext _context;
        public SubjectTest()
        {
            var options = new DbContextOptionsBuilder<BaseContext>()
                .UseInMemoryDatabase(databaseName: "EdukuJezTestDb")
                .Options;

            BaseContext.options = options;
            BaseContext.testing = true;
        }
        [Fact]
        public void AddNewEntry_ShouldAddSubjectToDatabase()
        {
             _context = BaseContext.GetContext();
            // Arrange
             _repository = new SubjectsRepository();
             _repositoryGroup = new GroupsRepository();


            var NewTecherGroup = new Group
            {
                Name = "NewTecherGroup"

            };
            var NewStudentGroup = new Group
            {
                Name = "NewStudentGroup"

            };

            _repositoryGroup.Insert(NewTecherGroup);
            _repositoryGroup.Insert(NewStudentGroup);

            var newSubject = new Subject
            {
                SubjectName = "SubNAme",
                SubjectDesc = "SubDesc",
                StudentGroupId = NewStudentGroup.Id,
                TeacherGroupId = NewTecherGroup.Id
            };

            // Act
            _repository.Insert(newSubject);

            // Assert
            var addedSubject = _context.Subjects.First(c => c.SubjectName == "SubNAme" && c.TeacherGroupId==1 && c.StudentGroupId ==2 && c.SubjectDesc == "SubDesc");

            Assert.NotNull(addedSubject);


        }


    }
}
