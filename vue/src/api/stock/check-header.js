import request from '@/utils/request'
import { transformAbpListQuery } from '@/utils/abp'

export function getCheckHeaders(query) {
  return request({
    url: '/api/stock/checkHeader/all',
    method: 'get',
    params: transformAbpListQuery(query)
  })
}

export function getCheckHeaderById(id) {
  return request({
    url: `/api/stock/checkHeader/${id}`,
    method: 'get'
  })
}

export function createCheckHeader(payload) {
  return request({
    url: '/api/stock/checkHeader',
    method: 'post',
    data: payload
  })
}

export function updateCheckHeader(payload) {
  return request({
    url: `/api/stock/checkHeader/${payload.id}`,
    method: 'put',
    data: payload
  })
}

export function deleteCheckHeader(id) {
  return request({
    url: `/api/stock/checkHeader/${id}`,
    method: 'delete'
  })
}
