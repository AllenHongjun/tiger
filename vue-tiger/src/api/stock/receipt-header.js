import request from '@/utils/request'
import { transformAbpListQuery } from '@/utils/abp'

export function getReceiptHeaders(query) {
  return request({
    url: '/api/stock/receiptHeader/all',
    method: 'get',
    params: transformAbpListQuery(query)
  })
}

export function getReceiptHeaderById(id) {
  return request({
    url: `/api/stock/receiptHeader/${id}`,
    method: 'get'
  })
}

export function createReceiptHeader(payload) {
  return request({
    url: '/api/stock/receiptHeader',
    method: 'post',
    data: payload
  })
}

export function updateReceiptHeader(payload) {
  return request({
    url: `/api/stock/receiptHeader/${payload.id}`,
    method: 'put',
    data: payload
  })
}

export function deleteReceiptHeader(id) {
  return request({
    url: `/api/stock/receiptHeader/${id}`,
    method: 'delete'
  })
}
