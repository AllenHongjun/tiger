import request from '@/utils/request'
import { transformAbpListQuery } from '@/utils/abp'

export function getStores(query) {
  return request({
    url: '/api/basic/store',
    method: 'get',
    params: transformAbpListQuery(query)
  })
}

export function getStoreById(id) {
  return request({
    url: `/api/basic/store/${id}`,
    method: 'get'
  })
}

export function createStore(payload) {
  return request({
    url: '/api/basic/store',
    method: 'post',
    data: payload
  })
}

export function updateStore(payload) {
  return request({
    url: `/api/basic/store/${payload.id}`,
    method: 'put',
    data: payload
  })
}

export function deleteStore(id) {
  return request({
    url: `/api/basic/store/${id}`,
    method: 'delete'
  })
}
