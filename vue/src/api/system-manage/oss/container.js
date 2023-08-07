import request from '@/utils/request'
import { transformAbpListQuery } from '@/utils/abp'

export function getContainers(params) {
  return request({
    url: '/api/oss-management/containers',
    method: 'get',
    params: transformAbpListQuery(params)
  })
}

export function getContainer(id) {
  return request({
    url: `/api/oss-management/containers/${id}`,
    method: 'get'
  })
}

export function createContainer(name) {
  return request({
    url: `/api/oss-management/containers/${name}`,
    method: 'post'
  })
}

export function updateContainer(id, payload) {
  return request({
    url: `/api/oss-management/containers/${id}`,
    method: 'put',
    data: payload
  })
}

export function deleteContainer(name) {
  return request({
    url: `/api/oss-management/containers/${name}`,
    method: 'delete'
  })
}

export function getContainerObject(params) {
  return request({
    url: `/api/oss-management/containers/objects`,
    method: 'get',
    params: transformAbpListQuery(params)
  })
}

