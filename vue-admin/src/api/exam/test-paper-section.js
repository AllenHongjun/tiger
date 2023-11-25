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

export function getAllTestPaperSections(params) {
  return request({
    url: `/api/exam/test-paper-sections/all`,
    method: 'get',
    params: transformAbpListQuery(params)
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

/**
 * 下移大题
 * @param {guid} id 大题id
 * @returns
 */
export function moveDownTestPaperSection(id) {
  return request({
    url: `/api/exam/test-paper-sections/move-down/${id}`,
    method: 'put'
  })
}

