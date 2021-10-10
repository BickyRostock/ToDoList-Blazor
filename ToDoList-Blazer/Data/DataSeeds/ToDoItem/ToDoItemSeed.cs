using System;
using System.Threading.Tasks;

namespace ToDoList_Blazer.Data.DataSeeds.ToDoItem
{
    public class ToDoItemSeed
    {
        /// <summary>
        /// Note to Self: We will make a static class that can seed test data, but for this solution I will only call it from tests because
        /// I don't like the idea of having data access implementation seed the data for testing purposes, or do it through the start up
        /// of the application which shouldn't care about seed data. This is only for testing, the application doesn't care
        /// about the testing strategy we use. So it doesn't belong anywhere other than allowing someone to run a seed manually
        /// i.e. from a test fixture. We can use a build pipeline an env variables if we want to build the app that has seed data, or during
        /// development I can just run the test to seed me data. 
        /// </summary>

        public static Data.ToDoItem[] ToDoListSeed => new Data.ToDoItem[] {
                    new Data.ToDoItem { Done = false, What = "Wake up", When = new DateTime(2021, 1, 1, 7, 0, 0), Who = "Me", Notes = "Get out of bed" },
                    new Data.ToDoItem { Done = false, What = "Eat food", When = new DateTime(2021, 1, 1, 7, 30, 0), Who = "Me", Notes = "Omlette" },
                    new Data.ToDoItem { Done = false, What = "Get ready", When = new DateTime(2021, 1, 1, 8, 0, 0), Who = "Me", Notes = "Cool clothes" },
                    new Data.ToDoItem { Done = false, What = "Drive to work", When = new DateTime(2021, 1, 1, 8, 30, 0), Who = "Me", Notes = "In my car" },
                    new Data.ToDoItem { Done = false, What = "Write code", When = new DateTime(2021, 1, 1, 9, 0, 0), Who = "Me", Notes = "C#" }
                };

        public static async Task<int> InitialiseAsync(IToDoItemService toDoItemService)
        {
            return await toDoItemService.CreateRangeAsync(ToDoListSeed);
        }
    }
}
