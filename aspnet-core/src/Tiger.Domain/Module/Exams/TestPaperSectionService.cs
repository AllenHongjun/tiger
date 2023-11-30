using DocumentFormat.OpenXml.Office2010.CustomUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tiger.Module.TaskManagement;
using Volo.Abp.Domain.Services;
using Volo.Abp.Uow;

namespace Tiger.Module.Exams
{
    public class TestPaperSectionManager : DomainService
    {
        private readonly ITestPaperStrategyRepository _repository;
        private readonly ITestPaperQuestionRepository _questionRepository;
        private readonly ITestPaperSectionRepository _sectionRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public TestPaperSectionManager(
            ITestPaperStrategyRepository repository,
            ITestPaperSectionRepository sectionRepository,
            IUnitOfWorkManager unitOfWorkManager,
            ITestPaperQuestionRepository questionRepository)
        {
            _repository=repository;
            _sectionRepository=sectionRepository;
            _unitOfWorkManager=unitOfWorkManager;
            _questionRepository=questionRepository;
        }

        /// <summary>
        /// 计算大题总分和题目数量
        /// </summary>
        /// <param name="testPaperSectionId"></param>
        /// <returns></returns>
        public async Task CalcuTotalScoreAndQusetionCount(Guid testPaperSectionId)
        {
            // 计算试卷大题题目数量和题目总分
            var testPaperSection = await _sectionRepository.GetAsync(testPaperSectionId);
            if (testPaperSection.Type == TestPaperSectionType.RandomQuestion)
            {
                var testPaperStrategies = _repository.Where(x => x.TestPaperSectionId == testPaperSectionId).ToList();
                testPaperSection.QuestionCount = testPaperStrategies.Sum(x => x.UnlimitedDifficultyCount + x.EasyCount + x.OrdinaryCount + x.DifficultCount);
                testPaperSection.TotalScore = testPaperStrategies
                    .Sum(x => (x.UnlimitedDifficultyCount + x.EasyCount + x.OrdinaryCount + x.DifficultCount) * x.ScorePerQuestion);
            }
            else if (testPaperSection.Type == TestPaperSectionType.FixedQuestions)
            {
                var testPaperQuestions = _questionRepository.Where(x => x.TestPaperSectionId == testPaperSectionId).ToList();
                testPaperSection.QuestionCount = testPaperQuestions.Count();
                testPaperSection.TotalScore = testPaperQuestions.Sum(x => x.Score);
            }
            
            await _sectionRepository.UpdateAsync(testPaperSection);
            await _unitOfWorkManager.Current.SaveChangesAsync();
        }
    }
}
