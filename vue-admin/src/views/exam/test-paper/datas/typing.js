
export const TestPaperType = {
  FixedQuestions: 1,
  RandomQuestion: 2
}

export const TestPaperTypeMap = {
  [TestPaperType.FixedQuestions]: '固定题目',
  [TestPaperType.RandomQuestion]: '随机题目'
}

export const TestPaperTagType = {
  [TestPaperType.FixedQuestions]: 'warning',
  [TestPaperType.RandomQuestion]: 'success'
}

export const TestPaperTypeOptions = [
  {
    key: 'FixedQuestions',
    type: 'primary',
    lable: '固定题目',
    value: 1
  },
  {
    key: 'RandomQuestion',
    type: 'success',
    lable: '随机题目',
    value: 2
  }
]
