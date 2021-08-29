import request from '@/utils/request'
import { transformAbpListQuery } from '@/utils/abp'

export function getWarehouses(query) {
  return request({
    url: '/api/basic/warehouse',
    method: 'get',
    params: transformAbpListQuery(query)
  })
}

export function getWarehouseById(id) {
  return request({
    url: `/api/basic/warehouse/${id}`,
    method: 'get'
  })
}

export function createWarehouse(payload) {
  return request({
    url: '/api/basic/warehouse',
    method: 'post',
    data: payload
  })
}

export function updateWarehouse(payload) {
  return request({
    url: `/api/basic/warehouse/${payload.id}`,
    method: 'put',
    data: payload
  })
}

export function deleteWarehouse(id) {
  return request({
    url: `/api/basic/warehouse/${id}`,
    method: 'delete'
  })
}
