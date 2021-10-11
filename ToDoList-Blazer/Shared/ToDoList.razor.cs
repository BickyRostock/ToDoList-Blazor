using Microsoft.AspNetCore.Components;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ToDoList_Blazer.Data;

namespace ToDoList_Blazer.Shared
{
    public class ToDoListStateHandler : ComponentBase
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

        [Inject]
        public IToDoItemService ToDoItemService { get; set; }

        private DateTime? m_dateTimeNow;
        public DateTime? DateTimeNow
        {
            get
            {
                if (m_dateTimeNow is null)
                {
                    return DateTime.Now;
                }
                else
                {
                    return m_dateTimeNow;
                }
            }
            set
            {
                m_dateTimeNow = value;
            }
        }

        private bool m_isInitalisedByApplication = false;

        protected override Task OnInitializedAsync()
        {
            m_isInitalisedByApplication = true;

            ToDoList = ToDoItemService.GetAll();

            return base.OnInitializedAsync();
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

            await ToDoItemService.CreateAsync(newItem);

            ToDoList = ToDoList.Append(newItem).ToArray();
            
            if(m_isInitalisedByApplication) StateHasChanged();
        }

        public async void DeleteItemHandler(ToDoItem itemToDelete)
        {
            await ToDoItemService.DeleteAsync(itemToDelete);

            ToDoList = ToDoList.Where(item => item.Id != itemToDelete.Id).ToArray();

            if (m_isInitalisedByApplication) StateHasChanged();
        }

        public async void UpdateItemHandler(ToDoItem itemToUpdate)
        {
            itemToUpdate.Done = !itemToUpdate.Done;

            itemToUpdate.DateDone = DateTimeNow.Value;
            
            await ToDoItemService.UpdateAsync(itemToUpdate);
        }
    }
}
