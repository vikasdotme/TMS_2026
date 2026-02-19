using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS_Models.Entities
{
    public class TaskEntity
    {
        [Key]
        public int TaskId { get; set; }
        public string TaskTitle { get; set; }  
        public string? TaskDescription { get; set; }
        public DateTime TaskDueDate { get; set; }   
        public string TaskStatus { get; set; }     
        public string? TaskRemarks { get; set; } 

        public DateTime CreatedOn { get; set; }
        public DateTime? LastUpdatedOn { get; set; } 

        public Guid CreatedByUserId { get; set; }
        public string CreatedByUserName { get; set; }

        public Guid? LastUpdatedByUserId { get; set; }
        public string? LastUpdatedByUserName { get; set; } 
    }
}
