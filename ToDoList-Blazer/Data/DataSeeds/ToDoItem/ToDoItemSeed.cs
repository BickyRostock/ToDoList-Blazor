using System;
using System.Threading.Tasks;

namespace ToDoList_Blazer.Data.DataSeeds.ToDoItem
{
    public class ToDoItemSeed
    {
        public static Data.ToDoItem[] ToDoListSeed => new Data.ToDoItem[] 
        {
            new Data.ToDoItem { ApplicationUserId = "1", Done = false, What = "Wake up", When = new DateTime(2021, 1, 1, 7, 0, 0), Who = "Me", Notes = "Get out of bed" },
            new Data.ToDoItem { ApplicationUserId = "1", Done = false, What = "Eat food", When = new DateTime(2021, 1, 1, 7, 30, 0), Who = "Me", Notes = "Omlette" },
            new Data.ToDoItem { ApplicationUserId = "2", Done = false, What = "Get ready", When = new DateTime(2021, 1, 1, 8, 0, 0), Who = "Me", Notes = "Cool clothes" },
            new Data.ToDoItem { ApplicationUserId = "3", Done = false, What = "Drive to work", When = new DateTime(2021, 1, 1, 8, 30, 0), Who = "Me", Notes = "In my car" },
            new Data.ToDoItem { ApplicationUserId = "3", Done = false, What = "Write code", When = new DateTime(2021, 1, 1, 9, 0, 0), Who = "Me", Notes = "C#" }
        };

        public static async Task<int> InitialiseAsync(IToDoItemService toDoItemService)
        {
            return await toDoItemService.CreateRangeAsync(ToDoListSeed);
        }
    }
}
