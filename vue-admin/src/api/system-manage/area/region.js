import request from '@/utils/request'
import { transformAbpListQuery } from '@/utils/abp'

export function getRegions(params) {
  return request({
    url: '/api/area/regions',
    method: 'get',
    params: transformAbpListQuery(params)
  })
}

export function getRegion(id) {
  return request({
    url: `/api/area/regions/${id}`,
    method: 'get'
  })
}

export function getAllRegion() {
  return request({
    url: `/api/area/regions/all`,
    method: 'get'
  })
}

export function createRegion(payload) {
  return request({
    url: '/api/area/regions',
    method: 'post',
    data: payload
  })
}

export function updateRegion(id, payload) {
  return request({
    url: `/api/area/regions/${id}`,
    method: 'put',
    data: payload
  })
}

export function deleteRegion(id) {
  return request({
    url: `/api/area/regions/${id}`,
    method: 'delete'
  })
}

export function getRegionsByParentCode(parentCode) {
  return request({
    url: `/api/area/regions/get-by-parentCode/${parentCode}`,
    method: 'get'
  })
}

