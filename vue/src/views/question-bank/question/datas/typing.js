export const QuestionType = {
  TrueOrFalse: 1,
  SingleChoice: 2,
  MultipleChoice: 3,
  Completion: 4,
  CalculationQuestions: 5,
  QA: 6,
  Btype: 7,
  ShortAnswer: 8,
  PracticalTraining: 9
}

export const QuestionTypeMap = {
  [QuestionType.TrueOrFalse]: '判断题',
  [QuestionType.SingleChoice]: '单选题',
  [QuestionType.MultipleChoice]: '多选题',
  [QuestionType.Completion]: '填空题',
  [QuestionType.CalculationQuestions]: '计算题',
  [QuestionType.QA]: '问答题',
  [QuestionType.Btype]: 'B型题',
  [QuestionType.ShortAnswer]: '简答题',
  [QuestionType.PracticalTraining]: '实训题'
}

export const QuestionDegree = {
  Simple: 1,
  Ordinary: 2,
  Difficult: 3
}

export const QuestionDegreeMap = {
  [QuestionDegree.Simple]: '简单',
  [QuestionDegree.Ordinary]: '普通',
  [QuestionDegree.Difficult]: '困难'
}

export default QuestionType

