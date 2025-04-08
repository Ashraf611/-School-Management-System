using System;
using System.Collections.Generic;

namespace Quiq_Application.Entity;

public partial class Student
{
    public int StudentId { get; set; }

    public int UserId { get; set; }

    public int CourseId { get; set; }

    public bool Validation { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual ICollection<StudentQuiz> StudentQuizzes { get; set; } = new List<StudentQuiz>();

    public virtual User User { get; set; } = null!;
}
