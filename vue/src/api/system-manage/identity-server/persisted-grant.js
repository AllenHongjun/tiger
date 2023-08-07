import request from '@/utils/request'
import { transformAbpListQuery } from '@/utils/abp'

export function getPersistedGrants(params) {
  return request({
    url: '/api/identity-server/persisted-grants',
    method: 'get',
    params: transformAbpListQuery(params)
  })
}

export function getPersistedGrant(id) {
  return request({
    url: `/api/identity-server/persisted-grants/${id}`,
    method: 'get'
  })
}

export function getAllPersistedGrant() {
  return request({
    url: `/api/identity-server/persisted-grants/all`,
    method: 'get'
  })
}

export function createPersistedGrant(payload) {
  return request({
    url: '/api/identity-server/persisted-grants',
    method: 'post',
    data: payload
  })
}

export function updatePersistedGrant(id, payload) {
  return request({
    url: `/api/identity-server/persisted-grants/${id}`,
    method: 'put',
    data: payload
  })
}

export function deletePersistedGrant(id) {
  return request({
    url: `/api/identity-server/persisted-grants/${id}`,
    method: 'delete'
  })
}

