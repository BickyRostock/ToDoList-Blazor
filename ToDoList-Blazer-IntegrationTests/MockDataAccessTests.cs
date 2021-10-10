using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.Configuration;
using ToDoList_Blazer.Data;
using ToDoList_Blazer.Data.DataSeeds.ToDoItem;

namespace ToDoList_Blazer_IntegrationTests
{
    [TestFixture]
    public class MockDataAccessTests
    {
        public IToDoItemService ToDoListService { get; private set; }

        private ToDoItem[] m_toDoItemsSeed;

        [OneTimeSetUp]
        public void SetUp()
        {
            
            //string integrationConnection = "Server=(localdb)\\mssqllocaldb;Database=aspnet-ToDoList-Blazer-Test;Trusted_Connection=True;MultipleActiveResultSets=true";
            string integrationConnection = "Server=(localdb)\\mssqllocaldb;Database=aspnet-ToDoList-Blazer-7B607C66-DE7E-488E-BC61-66FF66539F74;Trusted_Connection=True;MultipleActiveResultSets=true";

            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder.UseSqlServer(integrationConnection);

            ToDoDBContext context = new ToDoDBContext(optionsBuilder.Options);
            ToDoListService = new ToDoItemService(context);

            int result = ToDoItemSeed.InitialiseAsync(ToDoListService).Result;

            if (result != ToDoItemSeed.ToDoListSeed.Length)
            {
                throw new Exception("Failed to set up test seed data for integration tests.");
            }
            else
            {
                m_toDoItemsSeed = ToDoItemSeed.ToDoListSeed;
            }
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            ToDoItem[] items = ToDoListService.GetAll();

            int deletionResult = 0;

            foreach (var item in items)
            {
                deletionResult += ToDoListService.DeleteAsync(item).Result;
            }

            if(deletionResult != m_toDoItemsSeed.Length) throw new Exception("Failed to tear down test data for integration tests.");
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
    }
}
