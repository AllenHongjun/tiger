import request from '@/utils/request'
import { transformAbpListQuery } from '@/utils/abp'

export function getApiResources(params) {
  return request({
    url: '/api/identity-server/api-resources',
    method: 'get',
    params: transformAbpListQuery(params)
  })
}

export function getApiResource(id) {
  return request({
    url: `/api/identity-server/api-resources/${id}`,
    method: 'get'
  })
}

export function createApiResource(payload) {
  return request({
    url: '/api/identity-server/api-resources',
    method: 'post',
    data: payload
  })
}

export function updateApiResource(id, payload) {
  return request({
    url: `/api/identity-server/api-resources/${id}`,
    method: 'put',
    data: payload
  })
}

export function deleteApiResource(id) {
  return request({
    url: `/api/identity-server/api-resources/${id}`,
    method: 'delete'
  })
}

