using System;
using System.Collections.Generic;

namespace Quiq_Application.Entity;

public partial class StudentAnswer
{
    public int StudentAnswersId { get; set; }

    public int QuestionsId { get; set; }

    public bool IsCorrect { get; set; }

    public int StudentQuizId { get; set; }

    public virtual Question Questions { get; set; } = null!;

    public virtual StudentQuiz StudentQuiz { get; set; } = null!;
}
