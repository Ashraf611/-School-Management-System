using System;
using System.Collections.Generic;

namespace Quiq_Application.Entity;

public partial class Quiz
{
    public int QuizId { get; set; }

    public int NumberOfQuestions { get; set; }

    public int CourseId { get; set; }

    public string Name { get; set; } = null!;

    public DateOnly CreatedDate { get; set; }

    public string RoomCode { get; set; } = null!;

    public int Mark { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual ICollection<StudentQuiz> StudentQuizzes { get; set; } = new List<StudentQuiz>();
}
