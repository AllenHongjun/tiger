import request from '@/utils/request'
import { transformAbpListQuery } from '@/utils/abp'

export function getExaminees(params) {
  return request({
    url: '/api/exam/examinees',
    method: 'get',
    params: transformAbpListQuery(params)
  })
}

export function getExaminee(id) {
  return request({
    url: `/api/exam/examinees/${id}`,
    method: 'get'
  })
}

export function getAllExaminee() {
  return request({
    url: `/api/exam/examinees/all`,
    method: 'get'
  })
}

export function createExaminee(payload) {
  return request({
    url: '/api/exam/examinees',
    method: 'post',
    data: payload
  })
}

export function updateExaminee(id, payload) {
  return request({
    url: `/api/exam/examinees/${id}`,
    method: 'put',
    data: payload
  })
}

export function deleteExaminee(id) {
  return request({
    url: `/api/exam/examinees/${id}`,
    method: 'delete'
  })
}

/**
 * 批量创建考生
 * @param {object} payload
 * @returns
 */
export function batchCreateExaminee(payload) {
  return request({
    url: '/api/exam/examinees/batch-create',
    method: 'post',
    data: payload
  })
}
