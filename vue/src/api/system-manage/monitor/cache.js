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
    url: '/api/caching-management/cache/keys',
    method: 'get',
    params: transformAbpListQuery(params)
  })
}

export function getCache(params) {
  return request({
    url: `/api/caching-management/cache/values`,
    method: 'get',
    params: params
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

// 刷新缓存
export function refreshCache(payload) {
  return request({
    url: `/api/caching-management/cache/refresh`,
    method: 'put',
    data: payload
  })
}

export function deleteCache(params) {
  return request({
    url: `/api/caching-management/cache/delete`,
    method: 'delete',
    params: params
  })
}

