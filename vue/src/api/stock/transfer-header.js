import request from '@/utils/request'
import { transformAbpListQuery } from '@/utils/abp'

export function getTransferHeaders(query) {
  return request({
    url: '/api/stock/transferHeader/all',
    method: 'get',
    params: transformAbpListQuery(query)
  })
}

export function getTransferHeaderById(id) {
  return request({
    url: `/api/stock/transferHeader/${id}`,
    method: 'get'
  })
}

export function createTransferHeader(payload) {
  return request({
    url: '/api/stock/transferHeader',
    method: 'post',
    data: payload
  })
}

export function updateTransferHeader(payload) {
  return request({
    url: `/api/stock/transferHeader/${payload.id}`,
    method: 'put',
    data: payload
  })
}

export function deleteTransferHeader(id) {
  return request({
    url: `/api/stock/transferHeader/${id}`,
    method: 'delete'
  })
}
