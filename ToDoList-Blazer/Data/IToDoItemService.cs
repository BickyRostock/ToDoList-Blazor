using System.Threading.Tasks;

namespace ToDoList_Blazer.Data
{
    public interface IToDoItemService
    {
        ToDoDBContext Context { get; }
        Task<int> CreateAsync(ToDoItem item);
        Task<int> CreateRangeAsync(ToDoItem[] items);
        Task<int> DeleteAsync(ToDoItem item);
        Task<ToDoItem> Get(int Id);
        ToDoItem[] GetAll();
        Task<int> UpdateAsync(ToDoItem item);
    }
}