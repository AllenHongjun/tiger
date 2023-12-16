import request from '@/utils/request'
import { transformAbpListQuery } from '@/utils/abp'

export function getIdentityResources(params) {
  return request({
    url: '/api/identity-server/identity-resources',
    method: 'get',
    params: transformAbpListQuery(params)
  })
}

export function getIdentityResource(id) {
  return request({
    url: `/api/identity-server/identity-resources/${id}`,
    method: 'get'
  })
}

export function getAllIdentityResource() {
  return request({
    url: `/api/identity-server/identity-resources/all`,
    method: 'get'
  })
}

export function createIdentityResource(payload) {
  return request({
    url: '/api/identity-server/identity-resources',
    method: 'post',
    data: payload
  })
}

export function updateIdentityResource(id, payload) {
  return request({
    url: `/api/identity-server/identity-resources/${id}`,
    method: 'put',
    data: payload
  })
}

export function deleteIdentityResource(id) {
  return request({
    url: `/api/identity-server/identity-resources/${id}`,
    method: 'delete'
  })
}

