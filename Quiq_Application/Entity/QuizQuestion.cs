using System;
using System.Collections.Generic;

namespace Quiq_Application.Entity;

public partial class QuizQuestion
{
    public int QuizQuestionsId { get; set; }

    public int QuizId { get; set; }

    public int QuestionsId { get; set; }

    public virtual Question Questions { get; set; } = null!;

    public virtual Quiz Quiz { get; set; } = null!;
}
