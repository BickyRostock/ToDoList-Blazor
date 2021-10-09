using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoList_Blazer.Data
{
    public class ToDoItem
    {
        [Key]
        public int Id { get; set; }
        public bool Done { get; set; }
        [Required]
        [MaxLength(100)]
        public string What { get; set; }
        [MaxLength(50)]
        public string Who { get; set; }
        public DateTime When { get; set; }
        [MaxLength(300)]
        public string Notes { get; set; }
        public DateTime DateDone { get; set; }
    }
}
