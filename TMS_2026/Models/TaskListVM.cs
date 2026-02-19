using TMS_Models.Entities;

namespace TMS_2026.Models
{
    public class TaskListVM
    {
        public List<TaskEntity> Tasks { get; set; }

        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalRecords { get; set; }

        public string SearchTitle { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
    }

}
