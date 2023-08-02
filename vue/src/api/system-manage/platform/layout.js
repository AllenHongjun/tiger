import request from '@/utils/request'
import { transformAbpListQuery } from '@/utils/abp'

export function getLayouts(params) {
  return request({
    url: '/api/platform/layouts',
    method: 'get',
    params: transformAbpListQuery(params)
  })
}

export function getLayout(id) {
  return request({
    url: `/api/platform/layouts/${id}`,
    method: 'get'
  })
}

export function getAllLayout(params) {
  return request({
    url: `/api/platform/layouts/all`,
    method: 'get',
    params: transformAbpListQuery(params)
  })
}

export function createLayout(payload) {
  return request({
    url: '/api/platform/layouts',
    method: 'post',
    data: payload
  })
}

export function updateLayout(id, payload) {
  return request({
    url: `/api/platform/layouts/${id}`,
    method: 'put',
    data: payload
  })
}

export function deleteLayout(id) {
  return request({
    url: `/api/platform/layouts/${id}`,
    method: 'delete'
  })
}

