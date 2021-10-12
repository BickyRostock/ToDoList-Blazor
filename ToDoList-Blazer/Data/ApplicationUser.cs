using Microsoft.AspNetCore.Identity;

namespace ToDoList_Blazer.Data
{
    public class ApplicationUser : IdentityUser
    {
        public ToDoItem[] ToDoList { get; set; }
    }
}
