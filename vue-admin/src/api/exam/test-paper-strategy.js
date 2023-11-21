import request from '@/utils/request'
import { transformAbpListQuery } from '@/utils/abp'

export function getTestPaperStrategies(params) {
  return request({
    url: '/api/exam/test-paper-strategies',
    method: 'get',
    params: transformAbpListQuery(params)
  })
}

export function getTestPaperStrategy(id) {
  return request({
    url: `/api/exam/test-paper-strategies/${id}`,
    method: 'get'
  })
}

export function getAllTestPaperStrategy() {
  return request({
    url: `/api/exam/test-paper-strategies/all`,
    method: 'get'
  })
}

export function createTestPaperStrategy(payload) {
  return request({
    url: '/api/exam/test-paper-strategies',
    method: 'post',
    data: payload
  })
}

export function updateTestPaperStrategy(id, payload) {
  return request({
    url: `/api/exam/test-paper-strategies/${id}`,
    method: 'put',
    data: payload
  })
}

export function deleteTestPaperStrategy(id) {
  return request({
    url: `/api/exam/test-paper-strategies/${id}`,
    method: 'delete'
  })
}

