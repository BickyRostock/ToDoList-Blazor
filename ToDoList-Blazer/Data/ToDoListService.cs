using System;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoList_Blazer.Data
{
    public class ToDoItemService : IToDoItemService
    {
        public ApplicationDbContext Context { get; }

        public ToDoItemService(ApplicationDbContext context)
        {
            Context = context;
        }

        //TODO - we could take save changes out of each call, because if they are tracked by the framework, we could limit transactions on the DB and give the app more control

        public async Task<int> CreateRangeAsync(ToDoItem[] items)
        {
            await Context.AddRangeAsync(items);
            return await Context.SaveChangesAsync();
        }

        public async Task<int> CreateAsync(ToDoItem item)
        {
            await Context.AddAsync(item); //TODO - this might be a problem if await here when we save it might not tracked the entity yet.
            return await Context.SaveChangesAsync(); //TODO - Add failed call back
        }

        public ToDoItem[] GetAll()
        {
            return Context.ToDoItem.ToArray(); //TODO - how to make this async
        }

        public async Task<ToDoItem> Get(int Id)
        {
            return await Context.FindAsync<ToDoItem>(Id);
        }

        public async Task<int> UpdateAsync(ToDoItem item)
        {
            Context.Update(item);
            return await Context.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(ToDoItem item)
        {
            Context.Remove(item);
            return await Context.SaveChangesAsync();
        }
    }
}
