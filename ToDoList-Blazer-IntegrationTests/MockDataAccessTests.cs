using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.Configuration;
using ToDoList_Blazer.Data;

namespace ToDoList_Blazer_IntegrationTests
{
    public class MockDataAccessTests
    {
        private static readonly ToDoItem[] m_toDoList = new ToDoItem[]
        {
            new ToDoItem { Done = false, What = "Eat food", When = new DateTime(2021, 1, 1, 12, 0, 0), Who = "Me", Notes = "Omlette" },
            new ToDoItem { Done = false, What = "Eat food", When = new DateTime(2021, 1, 1, 12, 0, 0), Who = "Me", Notes = "Omlette" },
            new ToDoItem { Done = false, What = "Eat food", When = new DateTime(2021, 1, 1, 12, 0, 0), Who = "Me", Notes = "Omlette" },
            new ToDoItem { Done = false, What = "Eat food", When = new DateTime(2021, 1, 1, 12, 0, 0), Who = "Me", Notes = "Omlette" },
            new ToDoItem { Done = false, What = "Eat food", When = new DateTime(2021, 1, 1, 12, 0, 0), Who = "Me", Notes = "Omlette" }
        };

        public IToDoItemService ToDoListService { get; private set; }

        [OneTimeSetUp]
        public void SetUp()
        {
            //TODO - should really use a test database and run migrations against that then have a tear down which is what i would do if this was prod code
            string connectionString = "Server=(localdb)\\mssqllocaldb;Database=aspnet-ToDoList-Blazer-7B607C66-DE7E-488E-BC61-66FF66539F74;Trusted_Connection=True;MultipleActiveResultSets=true";

            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder.UseSqlServer(connectionString);

            ToDoDBContext context = new ToDoDBContext(optionsBuilder.Options);
            ToDoListService = new ToDoItemService(context);
        }

        //TODO - Tests currently depend on each other so need to use test DB and set up and tear down properly - one for another day
        [Test]
        public void CreateMockData()
        {
            int result = ToDoListService.CreateRangeAsync(m_toDoList).Result;
            Assert.That(result, Is.EqualTo(5));
        }

        [Test]
        public void GetData()
        {
            ToDoItem[] items = ToDoListService.GetAll();
            Assert.That(items.Length, Is.EqualTo(5));
        }

        [Test]
        public void UpdateData()
        {
            ToDoItem[] items = ToDoListService.GetAll();

            ToDoItem item = items[0];
            item.Done = true;

            int result = ToDoListService.UpdateAsync(item).Result;

            Assert.That(result, Is.EqualTo(1));

            ToDoItem updatedItem = ToDoListService.Get(item.Id).Result;

            Assert.That(updatedItem.Done, Is.True);
        }

        [Test]
        public void DeleteData()
        {
            ToDoItem[] items = ToDoListService.GetAll();

            foreach(var item in items)
            {
                int deletionResult = ToDoListService.DeleteAsync(item).Result;
                Assert.That(deletionResult, Is.EqualTo(1));
            }
            
            items = ToDoListService.GetAll();
            Assert.That(items.Length, Is.EqualTo(0));
        }
    }
}
