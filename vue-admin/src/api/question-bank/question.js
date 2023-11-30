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

export function getDifferentDegreeQuestionCount(params) {
  return request({
    url: '/api/question-bank/questions/different-degree-question-count',
    method: 'get',
    params: params
  })
}

