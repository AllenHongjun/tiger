import request from '@/utils/request'
import { transformAbpListQuery } from '@/utils/abp'

export function getRedisInfo() {
  return request({
    url: `/api/caching-management/cache/info`,
    method: 'get'
  })
}

export function getCaches(params) {
  return request({
    url: '/api/caching-management/cache',
    method: 'get',
    params: transformAbpListQuery(params)
  })
}

export function getCache(id) {
  return request({
    url: `/api/caching-management/cache/${id}`,
    method: 'get'
  })
}

export function getAllCache() {
  return request({
    url: `/api/caching-management/cache/all`,
    method: 'get'
  })
}

export function createCache(payload) {
  return request({
    url: '/api/caching-management/cache',
    method: 'post',
    data: payload
  })
}

export function updateCache(id, payload) {
  return request({
    url: `/api/caching-management/cache/${id}`,
    method: 'put',
    data: payload
  })
}

export function deleteCache(id) {
  return request({
    url: `/api/caching-management/cache/${id}`,
    method: 'delete'
  })
}

