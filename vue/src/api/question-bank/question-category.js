import request from '@/utils/request'
import { transformAbpListQuery } from '@/utils/abp'

export function getQuestionCategories(params) {
  return request({
    url: '/api/question-bank/question-categories',
    method: 'get',
    params: transformAbpListQuery(params)
  })
}

export function getQuestionCategory(id) {
  return request({
    url: `/api/question-bank/question-categories/${id}`,
    method: 'get'
  })
}

export function getAllQuestionCategory(params) {
  return request({
    url: `/api/question-bank/question-categories/all`,
    method: 'get'
  })
}

export function createQuestionCategory(payload) {
  return request({
    url: '/api/question-bank/question-categories',
    method: 'post',
    data: payload
  })
}

export function updateQuestionCategory(id, payload) {
  return request({
    url: `/api/question-bank/question-categories/${id}`,
    method: 'put',
    data: payload
  })
}

export function deleteQuestionCategory(id) {
  return request({
    url: `/api/question-bank/question-categories/${id}`,
    method: 'delete'
  })
}

