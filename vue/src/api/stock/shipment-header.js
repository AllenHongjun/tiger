import request from '@/utils/request'
import { transformAbpListQuery } from '@/utils/abp'

export function getShipmentHeaders(query) {
  return request({
    url: '/api/stock/shipmentHeader/all',
    method: 'get',
    params: transformAbpListQuery(query)
  })
}

export function getShipmentHeaderById(id) {
  return request({
    url: `/api/stock/shipmentHeader/${id}`,
    method: 'get'
  })
}

export function createShipmentHeader(payload) {
  return request({
    url: '/api/stock/shipmentHeader',
    method: 'post',
    data: payload
  })
}

export function updateShipmentHeader(payload) {
  return request({
    url: `/api/stock/shipmentHeader/${payload.id}`,
    method: 'put',
    data: payload
  })
}

export function deleteShipmentHeader(id) {
  return request({
    url: `/api/stock/shipmentHeader/${id}`,
    method: 'delete'
  })
}
