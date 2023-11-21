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

export const Type = [
  {
    key: 'TrueOrFalse',
    type: 'primary',
    lable: '判断题',
    value: 1
  },
  {
    key: 'SingleChoice',
    type: 'success',
    lable: '单选题',
    value: 2
  },
  {
    key: 'MultipleChoice',
    type: 'danger',
    lable: '多选题',
    value: 3
  },
  {
    key: 'Completion',
    type: 'primary',
    lable: '填空题',
    value: 4
  },
  {
    key: 'Calculation',
    type: 'primary',
    lable: '计算题',
    value: 5
  },
  {
    key: 'QA',
    type: 'primary',
    lable: '问答题',
    value: 6
  },
  {
    key: 'Btype',
    type: 'primary',
    lable: 'B型题',
    value: 7
  },
  {
    key: 'ShortAnswer',
    type: 'primary',
    lable: '简答题',
    value: 8
  },
  {
    key: 'PracticalTraining',
    type: 'primary',
    lable: '实训题',
    value: 9
  }
]

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

export const Degree = [
  {
    key: 'simple',
    type: 'primary',
    lable: '简单',
    value: 1
  },
  {
    key: 'ordinary',
    type: 'success',
    lable: '普通',
    value: 2
  },
  {
    key: 'difficult',
    type: 'danger',
    lable: '困难',
    value: 3
  }
]

export default QuestionType

