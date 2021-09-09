import request from '@/utils/request'
import { transformAbpListQuery } from '@/utils/abp'

export function getPurchaseHeaders(query) {
  return request({
    url: '/api/purchase/purchaseHeader/all',
    method: 'get',
    params: transformAbpListQuery(query)
  })
}

export function getPurchaseHeaderById(id) {
  return request({
    url: `/api/purchase/purchaseHeader/${id}`,
    method: 'get'
  })
}

export function createPurchaseHeader(payload) {
  return request({
    url: '/api/purchase/purchaseHeader',
    method: 'post',
    data: payload
  })
}

export function updatePurchaseHeader(payload) {
  return request({
    url: `/api/purchase/purchaseHeader/${payload.id}`,
    method: 'put',
    data: payload
  })
}

export function deletePurchaseHeader(id) {
  return request({
    url: `/api/purchase/purchaseHeader/${id}`,
    method: 'delete'
  })
}
