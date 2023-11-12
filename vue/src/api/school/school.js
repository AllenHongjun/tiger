import request from '@/utils/request'
import { transformAbpListQuery } from '@/utils/abp'

export function getSchools(params) {
  return request({
    url: '/api/school/schools',
    method: 'get',
    params: transformAbpListQuery(params)
  })
}

export function getSchool(id) {
  return request({
    url: `/api/school/schools/${id}`,
    method: 'get'
  })
}

export function getAllSchool() {
  return request({
    url: `/api/school/schools/all`,
    method: 'get'
  })
}

export function createSchool(payload) {
  return request({
    url: '/api/school/schools',
    method: 'post',
    data: payload
  })
}

export function updateSchool(id, payload) {
  return request({
    url: `/api/school/schools/${id}`,
    method: 'put',
    data: payload
  })
}

export function deleteSchool(id) {
  return request({
    url: `/api/school/schools/${id}`,
    method: 'delete'
  })
}

