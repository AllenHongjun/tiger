import request from '@/utils/request'
import { transformAbpListQuery } from '@/utils/abp'

export function getTestPaperSections(params) {
  return request({
    url: '/api/exam/test-paper-sections',
    method: 'get',
    params: transformAbpListQuery(params)
  })
}

export function getTestPaperSection(id) {
  return request({
    url: `/api/exam/test-paper-sections/${id}`,
    method: 'get'
  })
}

export function getAllTestPaperSection() {
  return request({
    url: `/api/exam/test-paper-sections/all`,
    method: 'get'
  })
}

export function createTestPaperSection(payload) {
  return request({
    url: '/api/exam/test-paper-sections',
    method: 'post',
    data: payload
  })
}

export function updateTestPaperSection(id, payload) {
  return request({
    url: `/api/exam/test-paper-sections/${id}`,
    method: 'put',
    data: payload
  })
}

export function deleteTestPaperSection(id) {
  return request({
    url: `/api/exam/test-paper-sections/${id}`,
    method: 'delete'
  })
}

