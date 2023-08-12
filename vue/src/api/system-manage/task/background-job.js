import request from '@/utils/request'
import { transformAbpListQuery } from '@/utils/abp'

export function getBackgroundJobs(params) {
  return request({
    url: '/api/task-management/background-jobs',
    method: 'get',
    params: transformAbpListQuery(params)
  })
}

export function getBackgroundJob(id) {
  return request({
    url: `/api/task-management/background-jobs/${id}`,
    method: 'get'
  })
}

export function getAllBackgroundJob() {
  return request({
    url: `/api/task-management/background-jobs/all`,
    method: 'get'
  })
}

export function createBackgroundJob(payload) {
  return request({
    url: '/api/task-management/background-jobs',
    method: 'post',
    data: payload
  })
}

export function updateBackgroundJob(id, payload) {
  return request({
    url: `/api/task-management/background-jobs/${id}`,
    method: 'put',
    data: payload
  })
}

export function deleteBackgroundJob(id) {
  return request({
    url: `/api/task-management/background-jobs/${id}`,
    method: 'delete'
  })
}

export function bulkStopBackgroundJob(payload) {
  return request({
    url: `/api/task-management/background-jobs/bulk-stop`,
    method: 'put',
    data: payload
  })
}

export function bulkOperateBackgroundJob(operator, payload) {
  return request({
    url: `/api/task-management/background-jobs/${operator}`,
    method: 'put',
    data: payload
  })
}

