
export const JobType = {
  Once: 0,
  Period: 1,
  Persistent: 2
}

export const JobTypeMap = {
  [JobType.Once]: '一次性的',
  [JobType.Period]: '周期性的',
  [JobType.Persistent]: '持续性的'
}

export const JobPriority = {
  Low: 5,
  BelowNormal: 10,
  Normal: 0xF,
  AboveNormal: 20,
  High: 25
}

export const JobPriorityMap = {
  [JobPriority.Low]: 'Low',
  [JobPriority.BelowNormal]: 'BelowNormal',
  [JobPriority.Normal]: 'Normal',
  [JobPriority.AboveNormal]: 'AboveNormal',
  [JobPriority.High]: 'High'
}

export const JobPriorityType = {
  [JobPriority.Low]: 'info',
  [JobPriority.BelowNormal]: 'warning',
  [JobPriority.Normal]: '',
  [JobPriority.AboveNormal]: 'success',
  [JobPriority.High]: 'danger'
}

export const JobStatus = {
  None: -1,
  Completed: 0,
  Running: 10,
  FailedRetry: 15,
  Paused: 20,
  Stopped: 30
}

export const JobStatusMap = {
  [JobStatus.None]: '未知',
  [JobStatus.Completed]: '已完成',
  [JobStatus.Running]: '运行中',
  [JobStatus.FailedRetry]: '失败重试',
  [JobStatus.Paused]: '已暂停',
  [JobStatus.Stopped]: '已停止'
}

export const JobStatusType = {
  [JobStatus.None]: 'warning',
  [JobStatus.Completed]: 'success',
  [JobStatus.Running]: '',
  [JobStatus.FailedRetry]: 'danger',
  [JobStatus.Paused]: 'info',
  [JobStatus.Stopped]: 'danger'
}

export default JobStatusType
