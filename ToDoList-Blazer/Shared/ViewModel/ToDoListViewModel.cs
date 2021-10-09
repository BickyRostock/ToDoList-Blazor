using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ToDoList_Blazer.Data;

namespace ToDoList_Blazer.Shared.ViewModel
{
    public class ToDoListViewModel
    {
        public ToDoItem[] ToDoList { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "The 'What' is too long, maybe add the detail to notes")]
        public string What { get; set; }
        [Required]
        public DateTime When { get; set; }
        [MaxLength(50, ErrorMessage = "Woah there, is this for a whole army!?")]
        public string Who { get; set; }
        [MaxLength(300, ErrorMessage = "Steady on there, this doesn't have to be war and peace!")]
        public string Notes { get; set; }

        private IToDoItemService m_toDoItemService;

        public ToDoListViewModel(IToDoItemService service)
        {
            m_toDoItemService = service;
            ToDoList = m_toDoItemService.GetAll();
        }

        public async void OnNewToDoItemSubmitted()
        {
            //TODO - need on successful message to user
            ToDoItem newItem = new ToDoItem
            {
                What = What,
                When = When,
                Who = Who,
                Notes = Notes,
            };

            await m_toDoItemService.CreateAsync(newItem);

            ToDoList = ToDoList.Append(newItem).ToArray();
        }
    }
}
