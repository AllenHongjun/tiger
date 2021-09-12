import request from '@/utils/request'
import qs from 'qs'
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

export function batchDeletePurchaseHeader(ids) {
  return request({
    url: `/api/purchase/purchaseHeader/batchDelete?${qs.stringify(ids, { indices: false })}`,
    method: 'delete'
  })
}

export function batchAuditPurchaseHeader(ids) {
  return request({
    url: `/api/purchase/purchaseHeader/batchAudit?${qs.stringify(ids, { indices: false })}`,
    method: 'delete'
  })
}

export function batchClosePurchaseHeader(ids) {
  return request({
    url: `/api/purchase/purchaseHeader/batchClose?${qs.stringify(ids, { indices: false })}`,
    method: 'delete'
  })
}

