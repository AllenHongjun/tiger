import request from '@/utils/request'
import { transformAbpListQuery } from '@/utils/abp'

export function getPurchaseReturnHeaders(query) {
  return request({
    url: '/api/purchase/purchaseReturnHeader/all',
    method: 'get',
    params: transformAbpListQuery(query)
  })
}

export function getPurchaseReturnHeaderById(id) {
  return request({
    url: `/api/purchase/purchaseReturnHeader/${id}`,
    method: 'get'
  })
}

export function createPurchaseReturnHeader(payload) {
  return request({
    url: '/api/purchase/purchaseReturnHeader',
    method: 'post',
    data: payload
  })
}

export function updatePurchaseReturnHeader(payload) {
  return request({
    url: `/api/purchase/purchaseReturnHeader/${payload.id}`,
    method: 'put',
    data: payload
  })
}

export function deletePurchaseReturnHeader(id) {
  return request({
    url: `/api/purchase/purchaseReturnHeader/${id}`,
    method: 'delete'
  })
}
