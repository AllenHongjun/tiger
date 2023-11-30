using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Module.QuestionBank
{
    public class DifferentDegreeQuestionCountInfo
    {
        public Guid QuestionCategoryId { get; set; }

        public int UnlimitedDifficultyCount { get; set; }

        public int SimpleCount { get; set; }

        public int OrdinaryCount { get; set; }

        public int DifficultCount { get; set; }
    }
}
