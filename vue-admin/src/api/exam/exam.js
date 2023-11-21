import request from '@/utils/request'
import { transformAbpListQuery } from '@/utils/abp'

export function getExams(params) {
  return request({
    url: '/api/exam/exams',
    method: 'get',
    params: transformAbpListQuery(params)
  })
}

export function getExam(id) {
  return request({
    url: `/api/exam/exams/${id}`,
    method: 'get'
  })
}

export function getAllExam() {
  return request({
    url: `/api/exam/exams/all`,
    method: 'get'
  })
}

export function createExam(payload) {
  return request({
    url: '/api/exam/exams',
    method: 'post',
    data: payload
  })
}

export function updateExam(id, payload) {
  return request({
    url: `/api/exam/exams/${id}`,
    method: 'put',
    data: payload
  })
}

export function deleteExam(id) {
  return request({
    url: `/api/exam/exams/${id}`,
    method: 'delete'
  })
}

