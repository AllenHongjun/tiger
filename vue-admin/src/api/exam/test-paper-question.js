import request from '@/utils/request'
import { transformAbpListQuery } from '@/utils/abp'

export function getTestPaperQuestions(params) {
  return request({
    url: '/api/exam/test-paper-questions',
    method: 'get',
    params: transformAbpListQuery(params)
  })
}

export function getTestPaperQuestion(id) {
  return request({
    url: `/api/exam/test-paper-questions/${id}`,
    method: 'get'
  })
}

export function getAllTestPaperQuestion(testPaperSectionId) {
  return request({
    url: `/api/exam/test-paper-questions/all/${testPaperSectionId}`,
    method: 'get'
  })
}

export function createTestPaperQuestion(payload) {
  return request({
    url: '/api/exam/test-paper-questions',
    method: 'post',
    data: payload
  })
}

export function updateTestPaperQuestion(id, payload) {
  return request({
    url: `/api/exam/test-paper-questions/${id}`,
    method: 'put',
    data: payload
  })
}

export function deleteTestPaperQuestion(id) {
  return request({
    url: `/api/exam/test-paper-questions/${id}`,
    method: 'delete'
  })
}

export function comfirmSelectTestPaperQuestion(payload) {
  return request({
    url: '/api/exam/test-paper-questions/comfirm-select',
    method: 'post',
    data: payload
  })
}

