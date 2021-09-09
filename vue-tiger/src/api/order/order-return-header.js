import request from '@/utils/request'
import { transformAbpListQuery } from '@/utils/abp'

export function getOrderReturnHeaders(query) {
  return request({
    url: '/api/order/orderReturnHeader/all',
    method: 'get',
    params: transformAbpListQuery(query)
  })
}

export function getOrderReturnHeaderById(id) {
  return request({
    url: `/api/order/orderReturnHeader/${id}`,
    method: 'get'
  })
}

export function createOrderReturnHeader(payload) {
  return request({
    url: '/api/order/orderReturnHeader',
    method: 'post',
    data: payload
  })
}

export function updateOrderReturnHeader(payload) {
  return request({
    url: `/api/order/orderReturnHeader/${payload.id}`,
    method: 'put',
    data: payload
  })
}

export function deleteOrderReturnHeader(id) {
  return request({
    url: `/api/order/orderReturnHeader/${id}`,
    method: 'delete'
  })
}
