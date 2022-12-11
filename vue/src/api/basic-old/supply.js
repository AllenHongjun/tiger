import request from '@/utils/request'
import { transformAbpListQuery } from '@/utils/abp'

export function getSupplies(query) {
  return request({
    url: '/api/basic/supply',
    method: 'get',
    params: transformAbpListQuery(query)
  })
}

export function getSupplyById(id) {
  return request({
    url: `/api/basic/supply/${id}`,
    method: 'get'
  })
}

export function createSupply(payload) {
  return request({
    url: '/api/basic/supply',
    method: 'post',
    data: payload
  })
}

export function updateSupply(payload) {
  return request({
    url: `/api/basic/supply/${payload.id}`,
    method: 'put',
    data: payload
  })
}

export function deleteSupply(id) {
  return request({
    url: `/api/basic/supply/${id}`,
    method: 'delete'
  })
}
