using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Linq;
using ToDoList_Blazer.Data;
using ToDoList_Blazer.Data.DataSeeds.ApplicationUser;
using ToDoList_Blazer.Data.DataSeeds.ToDoItem;

namespace ToDoList_Blazer_IntegrationTests
{
    [TestFixture]
    public class MockDataAccessTests
    {
        public IToDoItemService ToDoListService { get; private set; }

        private ApplicationUser[] m_applicationUserSeed;
        private ToDoItem[] m_toDoItemsSeed;

        [OneTimeSetUp]
        public void SetUp()
        {
            DbContextOptionsBuilder<ApplicationDbContext> optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseInMemoryDatabase("tests");

            ApplicationDbContext context = new ApplicationDbContext(optionsBuilder.Options);
            ToDoListService = new ToDoItemService(context);

            int applicationUserResult = ApplicationUserSeed.InitialiseAsync(context).Result;
            int toDoItemResult = ToDoItemSeed.InitialiseAsync(ToDoListService).Result;

            if (applicationUserResult != ApplicationUserSeed.ApplicationUsersSeed.Length 
                || toDoItemResult != ToDoItemSeed.ToDoListSeed.Length)
            {
                throw new Exception("Failed to set up test seed data for integration tests.");
            }
            else
            {
                m_applicationUserSeed = ApplicationUserSeed.ApplicationUsersSeed;
                m_toDoItemsSeed = ToDoItemSeed.ToDoListSeed;
            }
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            foreach(ApplicationUser user in m_applicationUserSeed)
            {
                ToDoItem[] items = ToDoListService.GetAllAsync(user).Result;
                int deletionResult = 0;
                foreach (var item in items)
                {
                    deletionResult += ToDoListService.DeleteAsync(item).Result;

                    Assert.That(deletionResult, Is.GreaterThan(0));
                }
            }
        }

        [Test]
        public void GetData()
        {
            foreach(ApplicationUser user in m_applicationUserSeed)
            {
                int expectedResult = m_toDoItemsSeed.Where(i => i.ApplicationUserId == user.Id).Count();

                ToDoItem[] items = ToDoListService.GetAllAsync(user).Result;

                Assert.That(items.Length, Is.EqualTo(expectedResult));
            }
        }

        [Test]
        public void UpdateData()
        {
            foreach(ApplicationUser user in m_applicationUserSeed)
            {
                ToDoItem[] items = ToDoListService.GetAllAsync(user).Result;

                ToDoItem item = items[0];
                item.Done = true;

                int result = ToDoListService.UpdateAsync(item).Result;

                Assert.That(result, Is.EqualTo(1));

                ToDoItem updatedItem = ToDoListService.GetAsync(user, item.Id).Result;

                Assert.That(updatedItem.Done, Is.True);
            }
        }
    }
}
