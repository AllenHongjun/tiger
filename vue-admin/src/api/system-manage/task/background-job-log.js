import request from '@/utils/request'
import { transformAbpListQuery } from '@/utils/abp'

export function getBackgroundJobLogs(params) {
  return request({
    url: '/api/task-management/background-jobs/logs',
    method: 'get',
    params: transformAbpListQuery(params)
  })
}

export function getBackgroundJobLog(id) {
  return request({
    url: `/api/task-management/background-jobs/logs/${id}`,
    method: 'get'
  })
}

export function deleteBackgroundJobLog(id) {
  return request({
    url: `/api/task-management/background-jobs/logs/${id}`,
    method: 'delete'
  })
}

