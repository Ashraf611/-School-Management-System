using System;
using System.Collections.Generic;

namespace Quiq_Application.Entity;

public partial class Question
{
    public int QuestionsId { get; set; }

    public string Text { get; set; } = null!;

    public string FistOption { get; set; } = null!;

    public string SecontOption { get; set; } = null!;

    public string ThirdOption { get; set; } = null!;

    public string FourthOption { get; set; } = null!;

    public int Answer { get; set; }

    public int Grade { get; set; }

    public int CourseId { get; set; }

    public virtual Course Course { get; set; } = null!;
}
