import request from '@/utils/request'

export function getBackgroundJobAction(jobId) {
  return request({
    url: `/api/task-management/background-jobs/actions/${jobId}`,
    method: 'get'
  })
}

export function createBackgroundJobAction(jobId, payload) {
  return request({
    url: `/api/task-management/background-jobs/actions/${jobId}`,
    method: 'post',
    data: payload
  })
}

export function updateBackgroundJobAction(id, payload) {
  return request({
    url: `/api/task-management/background-jobs/${id}`,
    method: 'put',
    data: payload
  })
}

export function deleteBackgroundJobAction(id) {
  return request({
    url: `/api/task-management/background-jobs/${id}`,
    method: 'delete'
  })
}

export function getBackgroundJobActionDefinitions() {
  return request({
    url: `/api/task-management/background-jobs/actions/definitions`,
    method: 'get'
  })
}

