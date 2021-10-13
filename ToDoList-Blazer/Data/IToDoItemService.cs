using System.Threading.Tasks;

namespace ToDoList_Blazer.Data
{
    public interface IToDoItemService
    {
        ApplicationDbContext Context { get; }
        Task<int> CreateAsync(ToDoItem item);
        Task<int> CreateRangeAsync(ToDoItem[] items);
        Task<int> DeleteAsync(ToDoItem item);
        Task<ToDoItem> GetAsync(ApplicationUser user, int Id);
        Task<ToDoItem[]> GetAllAsync(ApplicationUser user);
        Task<int> UpdateAsync(ToDoItem item);
    }
}