using Moq;
using NUnit.Framework;
using System;
using ToDoList_Blazer.Data;
using ToDoList_Blazer.Shared;

namespace ToDoList_Blazer_UnitTests
{
    [TestFixture]
    public class ToDoListStateHandlerTest
    {
        [Test]
        public void When_OnNewToDoItemSubmitted_Then_ItemIsAddedToArray()
        {
            Mock<IToDoItemService> service = new Mock<IToDoItemService>(MockBehavior.Strict);
            service.Setup(service => service.CreateAsync(It.IsAny<ToDoItem>())).ReturnsAsync(1);

            ToDoListStateHandler razorComponentHandler = new ToDoListStateHandler
            {
                ToDoItemService = service.Object,
                ToDoList = new ToDoItem[0],
                What = "Eat food",
                Who = "Me",
                When = new System.DateTime(2021, 1, 1),
                Notes = "Some pie",
            };

            razorComponentHandler.OnNewToDoItemSubmitted();

            Assert.That(razorComponentHandler.ToDoList.Length, Is.EqualTo(1));
        }

        [Test]
        public void When_DeleteItemHandler_Then_ItemIsDeletedFromArray()
        {
            Mock<IToDoItemService> service = new Mock<IToDoItemService>(MockBehavior.Strict);
            service.Setup(service => service.DeleteAsync(It.IsAny<ToDoItem>())).ReturnsAsync(1);

            ToDoItem itemToDelete = new ToDoItem { Id = 1 };

            ToDoListStateHandler razorComponentHandler = new ToDoListStateHandler
            {
                ToDoItemService = service.Object,
                ToDoList = new ToDoItem[] { itemToDelete },
            };

            razorComponentHandler.DeleteItemHandler(itemToDelete);

            Assert.That(razorComponentHandler.ToDoList.Length, Is.EqualTo(0));
        }

        [Test]
        public void When_UpdateItemHandler_Then_ItemIsUpdatedInArray()
        {
            Mock<IToDoItemService> service = new Mock<IToDoItemService>(MockBehavior.Strict);
            service.Setup(service => service.UpdateAsync(It.IsAny<ToDoItem>())).ReturnsAsync(1);

            ToDoItem itemToUpdate = new ToDoItem { Id = 1, Done = false };

            ToDoListStateHandler razorComponentHandler = new ToDoListStateHandler
            {
                ToDoItemService = service.Object,
                ToDoList = new ToDoItem[] { itemToUpdate },
                DateTimeNow = new DateTime(2021, 1, 1),
            };

            razorComponentHandler.UpdateItemHandler(itemToUpdate);

            Assert.That(razorComponentHandler.ToDoList[0].Done, Is.True);
            Assert.That(DateTime.Compare(razorComponentHandler.ToDoList[0].DateDone, new DateTime(2021, 1, 1)), Is.EqualTo(0));
        }
    }
}
