export const AnswerSheetStatus = {
  UnSubmit: 1,
  Submited: 2,
  Reviewed: 3
}

export const AnswerSheetStatusMap = {
  [AnswerSheetStatus.UnSubmit]: '未交卷',
  [AnswerSheetStatus.Submited]: '已交卷',
  [AnswerSheetStatus.Reviewed]: '已评卷'
}

export const AnswerSheetStatusType = {
  [AnswerSheetStatus.UnSubmit]: 'info',
  [AnswerSheetStatus.Submited]: 'success',
  [AnswerSheetStatus.Reviewed]: 'primary'
}

export default AnswerSheetStatus
