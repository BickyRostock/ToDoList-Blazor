using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoList_Blazer.Data
{
    public class ToDoDBContext : DbContext
    {
        public ToDoDBContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<ToDoItem> ToDoItem { get; set; } //TODO - how to make this async
    }
}
