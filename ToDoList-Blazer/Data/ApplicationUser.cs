using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ToDoList_Blazer.Data
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<ToDoItem> ToDoList { get; set; }

        public ApplicationUser()
        {
            ToDoList = new Collection<ToDoItem>();
        }
    }
}
