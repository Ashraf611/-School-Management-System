using System;
using System.Collections.Generic;

namespace Quiq_Application.Entity;

public partial class StudentQuiz
{
    public int StudentQuizId { get; set; }

    public int StudentId { get; set; }

    public decimal Score { get; set; }

    public bool Status { get; set; }

    public bool Completed { get; set; }

    public int QuizeId { get; set; }

    public int CourseId { get; set; }

    public DateOnly AttemptDate { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual Quiz Quize { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}
