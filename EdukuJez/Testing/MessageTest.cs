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
    public class MessageTest
    {
        public MessageTest()
        {
            var options = new DbContextOptionsBuilder<BaseContext>()
                .UseInMemoryDatabase(databaseName: "EdukuJezTestDb")
                .Options;

            BaseContext.options = options;
            BaseContext.testing = true;
        }

        [Fact]
        public void AddNewEntry_ShouldAddMessageForSingleUSerToDatabase()
        {

            var context = BaseContext.GetContext();
            // Arrange
            var repository = new MessageRepository();
            var repositoryMU = new MessageUsersRepository();
            var repositoryUser = new UsersRepository();

            var NewSender = new User
            {
                UserName = "Adam",
                UserSurname = "Kowalski",
                UserLogin = "AAAAAAAAAAAAAA",
                UserPassword = "AAAAAAAAAAAAA"

            };
            var NewRecipient = new User
            {
                UserName = "Jan",
                UserSurname = "NieKowalski",
                UserLogin = "AAA",
                UserPassword = "AAA"
            };
        


            var NewMessage = new Message
            {
                Topic = "new Topic",
                Content = "new Content",
                Sender = NewSender,
                IsGroupMsg = false,
                DateTime = DateTime.Now,
            };

            var MU = new MessageUsers() { User = NewRecipient, Message = NewMessage};

            repositoryMU.Insert(MU);

            var test = context.Messages.FirstOrDefault(x => x.Topic == "new Topic" && x.Recipients.Count == 1 && x.Sender.Id ==1);
            var test2 = context.MessageUsers.FirstOrDefault(x => x.User.Id == 2 && x.Message.Id==1);

            Assert.NotNull(test);
            Assert.NotNull(test2);
        }
        [Fact]
        public void AddNewEntry_ShouldAddMessageForGroupToDatabase()
        {

            var context = BaseContext.GetContext();
            // Arrange
            var repository = new MessageRepository();
            var repositoryMG = new MessageGroupsRepository();
            var repositoryUser = new UsersRepository();
            var repositoryGroup = new GroupsRepository();

            var NewSender = new User
            {
                UserName = "Adam",
                UserSurname = "Kowalski",
                UserLogin = "AAAAAAAAAAAAAA",
                UserPassword = "AAAAAAAAAAAAA"

            };
            var NewRecipientGroup = new Group
            {
             Name = "NewGroup",
            };


            repositoryUser.Insert(NewSender);

            var NewMessage = new Message
            {
                Topic = "new Topic for group",
                Content = "new Content",
                IsGroupMsg = false,
                DateTime = DateTime.Now,
            };

            var MG = new MessageGroups() {Message= NewMessage, Group = NewRecipientGroup };

            repositoryMG.Insert(MG);
            repositoryUser.Table.FirstOrDefault(s => s == NewSender).Sends.Add(NewMessage);
            repositoryUser.Update();
            var test = context.Messages.FirstOrDefault(x => x.Topic == "new Topic for group" && x.Sender.Id == 1 && x.GroupRecipients.Count==1);
            var test2 = context.MessageGroups.FirstOrDefault(x => x.Group.Id == 1 && x.Message.Id == 1);

            Assert.NotNull(test);
            Assert.NotNull(test2);
        }
    }
}
