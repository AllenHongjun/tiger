import request from '@/utils/request'
import { transformAbpListQuery } from '@/utils/abp'

export function getObjects(params) {
  return request({
    url: '/api/oss-management/objects',
    method: 'get',
    params: transformAbpListQuery(params)
  })
}

export function getObject(id) {
  return request({
    url: `/api/oss-management/objects/${id}`,
    method: 'get'
  })
}

export function createObject(payload) {
  return request({
    url: '/api/oss-management/objects',
    method: 'post',
    data: payload,
    headers: {
      'Content-Type': 'multipart/form-data;charset=utf-8'
    }

  })
}

export function updateObject(id, payload) {
  return request({
    url: `/api/oss-management/objects/${id}`,
    method: 'put',
    data: payload
  })
}

export function deleteObject(payload) {
  return request({
    url: `/api/oss-management/objects`,
    method: 'delete',
    data: payload
  })
}

export function downloadObject(params) {
  return request({
    url: `/api/oss-management/objects/download`,
    method: 'get',
    params: transformAbpListQuery(params)
  })
}

