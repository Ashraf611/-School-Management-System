using System.ComponentModel.DataAnnotations;

namespace Quiq_Application.Models
{
    public class StudentsResults_Model
    {
        public string? StudentName { get; set; }

        public string? CourseName { get; set; }

        public decimal Score { get; set; }

        public bool? Status { get; set; }

        public bool? Completed { get; set; }

        public DateOnly? AttemptDate { get; set; }

    }
}
