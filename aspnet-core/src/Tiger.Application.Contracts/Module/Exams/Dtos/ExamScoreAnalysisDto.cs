using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Module.Exams.Dtos
{
    public class ExamScoreAnalysisDto
    {
        public Guid CreatorId { get; set; }

        public string UserName { get; set; }

        public int NumberOfParticipates { get; set; }

        public double AverageExamDuration { get; set; }

        public decimal? AverageTotalScore { get; set; }

        public decimal? HighestTotalScore { get; set; }

        public decimal? LowestTotalScore { get; set; }

        public bool IsPass { get; set; }
    }
}
