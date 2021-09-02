import request from '@/utils/request'
import { transformAbpListQuery } from '@/utils/abp'

export function getOrders(query) {
  return request({
    url: '/api/basic/order',
    method: 'get',
    params: transformAbpListQuery(query)
  })
}

export function getOrderById(id) {
  return request({
    url: `/api/basic/order/${id}`,
    method: 'get'
  })
}

export function createOrder(payload) {
  return request({
    url: '/api/basic/order',
    method: 'post',
    data: payload
  })
}

export function updateOrder(payload) {
  return request({
    url: `/api/basic/order/${payload.id}`,
    method: 'put',
    data: payload
  })
}

export function deleteOrder(id) {
  return request({
    url: `/api/basic/order/${id}`,
    method: 'delete'
  })
}
