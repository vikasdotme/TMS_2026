
using System.ComponentModel.DataAnnotations;

namespace TMS_2026.Models
{
    public class TaskEntityvm
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
