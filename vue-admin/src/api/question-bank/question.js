import request from '@/utils/request'
import { transformAbpListQuery } from '@/utils/abp'

export function getQuestions(params) {
  return request({
    url: '/api/question-bank/questions',
    method: 'get',
    params: transformAbpListQuery(params)
  })
}

export function getQuestion(id) {
  return request({
    url: `/api/question-bank/questions/${id}`,
    method: 'get'
  })
}

export function getAllQuestion() {
  return request({
    url: `/api/question-bank/questions/all`,
    method: 'get'
  })
}

export function createQuestion(payload) {
  return request({
    url: '/api/question-bank/questions',
    method: 'post',
    data: payload
  })
}

export function updateQuestion(id, payload) {
  return request({
    url: `/api/question-bank/questions/${id}`,
    method: 'put',
    data: payload
  })
}

export function deleteQuestion(id) {
  return request({
    url: `/api/question-bank/questions/${id}`,
    method: 'delete'
  })
}

export function exportQuestionXlsxTemplate() {
  return request({
    url: `/api/question-bank/questions/export-xlxs-template`,
    method: 'get',
    header: {
      headers: { 'Content-Type': 'application/x-download' }
    },
    responseType: 'blob'
  })
}

export function importQuestionFromXlsx(payload) {
  return request({
    url: `/api/question-bank/questions/import-from-xlsx`,
    method: 'post',
    data: payload,
    headers: {
      'Content-Type': 'multipart/form-data;charset=utf-8'
    }
  })
}

export function exportQuestionToXlsx(params) {
  return request({
    url: `/api/question-bank/questions/export-to-xlsx`,
    method: 'get',
    params: transformAbpListQuery(params),
    header: {
      headers: { 'Content-Type': 'application/x-download' }
    },
    responseType: 'blob'
  })
}

export function getDifferentDegreeQuestionCount(params) {
  return request({
    url: '/api/question-bank/questions/different-degree-question-count',
    method: 'get',
    params: params
  })
}

