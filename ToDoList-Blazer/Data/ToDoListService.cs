using Microsoft.EntityFrameworkCore;
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

        public async Task<int> CreateRangeAsync(ToDoItem[] items)
        {
            await Context.AddRangeAsync(items);
            return await Context.SaveChangesAsync();
        }

        public async Task<int> CreateAsync(ToDoItem item)
        {
            await Context.AddAsync(item);
            return await Context.SaveChangesAsync();
        }

        public async Task<ToDoItem[]> GetAllAsync(ApplicationUser user)
        {
            ApplicationUser theUser = await Context.Users
                .Where(u => u.Id == user.Id)
                .Include(u => u.ToDoList).FirstOrDefaultAsync();

            return theUser.ToDoList.ToArray();
        }

        public async Task<ToDoItem> GetAsync(ApplicationUser user, int Id)
        {
            ApplicationUser theUser = await Context.Users
                .Where(u => u.Id == user.Id)
                .Include(u => u.ToDoList).FirstOrDefaultAsync();

            return theUser.ToDoList.FirstOrDefault(item => item.Id == Id);
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
