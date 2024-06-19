using EdukuJez.Repositories;
using EdukuJez.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Xunit;

namespace Testing
{
    public class AttachmentTest
    {
        public AttachmentTest()
        {
            var options = new DbContextOptionsBuilder<BaseContext>()
                .UseInMemoryDatabase(databaseName: "EdukuJezTestDb")
                .Options;

            BaseContext.options = options;
            BaseContext.testing = true;
        }

        [Fact]
        public void AddNewEntry_ShouldAddAttachmentToDatabase()
        {
            var context = BaseContext.GetContext();
            // Arrange
            var repository = new AttachmentsRepository();

            var newAttachment = new Attachment
            {
                Name = "Lecture Notes",
                ContentType = Attachment.FILE,
                Text = "Notes.txt",
                Content = new byte[] { 0x01, 0x02, 0x03 }  
            };

            // Act
            repository.Insert(newAttachment);
            context.SaveChanges();

            // Assert
            var addedAttachment = context.Attachments.FirstOrDefault(a => a.Name == "Lecture Notes");
            Assert.NotNull(addedAttachment);
        }
    }
}
