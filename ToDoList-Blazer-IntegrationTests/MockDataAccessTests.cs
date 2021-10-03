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
            string integrationConnection = "Server=(localdb)\\mssqllocaldb;Database=aspnet-ToDoList-Blazer-Test;Trusted_Connection=True;MultipleActiveResultSets=true";

            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder.UseSqlServer(integrationConnection);

            ToDoDBContext context = new ToDoDBContext(optionsBuilder.Options);
            ToDoListService = new ToDoItemService(context);

            int result = ToDoListService.CreateRangeAsync(m_toDoList).Result;

            if(result != m_toDoList.Length) throw new Exception("Failed to set up test data for integration tests.");
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

            if(deletionResult != m_toDoList.Length) throw new Exception("Failed to tear down test data for integration tests.");
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
