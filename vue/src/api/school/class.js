import request from '@/utils/request'
import { transformAbpListQuery } from '@/utils/abp'

export function getClasses(params) {
  return request({
    url: '/api/exam/classes',
    method: 'get',
    params: transformAbpListQuery(params)
  })
}

export function getClass(id) {
  return request({
    url: `/api/exam/classes/${id}`,
    method: 'get'
  })
}

export function getAllClass() {
  return request({
    url: `/api/exam/classes/all`,
    method: 'get'
  })
}

export function createClass(payload) {
  return request({
    url: '/api/exam/classes',
    method: 'post',
    data: payload
  })
}

export function updateClass(id, payload) {
  return request({
    url: `/api/exam/classes/${id}`,
    method: 'put',
    data: payload
  })
}

export function deleteClass(id) {
  return request({
    url: `/api/exam/classes/${id}`,
    method: 'delete'
  })
}

