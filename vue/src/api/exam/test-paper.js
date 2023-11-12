import request from '@/utils/request'
import { transformAbpListQuery } from '@/utils/abp'

export function getTestPapers(params) {
  return request({
    url: '/api/exam/test-papers',
    method: 'get',
    params: transformAbpListQuery(params)
  })
}

export function getTestPaper(id) {
  return request({
    url: `/api/exam/test-papers/${id}`,
    method: 'get'
  })
}

export function getAllTestPaper() {
  return request({
    url: `/api/exam/test-papers/all`,
    method: 'get'
  })
}

export function createTestPaper(payload) {
  return request({
    url: '/api/exam/test-papers',
    method: 'post',
    data: payload
  })
}

export function updateTestPaper(id, payload) {
  return request({
    url: `/api/exam/test-papers/${id}`,
    method: 'put',
    data: payload
  })
}

export function deleteTestPaper(id) {
  return request({
    url: `/api/exam/test-papers/${id}`,
    method: 'delete'
  })
}

