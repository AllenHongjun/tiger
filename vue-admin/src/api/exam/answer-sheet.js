import request from '@/utils/request'
import { transformAbpListQuery } from '@/utils/abp'

export function getAnswerSheets(params) {
  return request({
    url: '/api/exam/answer-sheets',
    method: 'get',
    params: transformAbpListQuery(params)
  })
}

export function getAnswerSheet(id) {
  return request({
    url: `/api/exam/answer-sheets/${id}`,
    method: 'get'
  })
}

export function getAllAnswerSheet() {
  return request({
    url: `/api/exam/answer-sheets/all`,
    method: 'get'
  })
}

export function createAnswerSheet(payload) {
  return request({
    url: '/api/exam/answer-sheets',
    method: 'post',
    data: payload
  })
}

export function updateAnswerSheet(id, payload) {
  return request({
    url: `/api/exam/answer-sheets/${id}`,
    method: 'put',
    data: payload
  })
}

export function deleteAnswerSheet(id) {
  return request({
    url: `/api/exam/answer-sheets/${id}`,
    method: 'delete'
  })
}

export function getExamScorePanelData(id) {
  return request({
    url: `/api/exam/answer-sheets/${id}/exam-score-panel-data`,
    method: 'get'
  })
}

