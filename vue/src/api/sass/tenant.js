import request from '@/utils/request'
import { transformAbpListQuery } from '@/utils/abp'

export function getTenants(query) {
  return request({
    url: '/api/saas/tenants',
    method: 'get',
    params: transformAbpListQuery(query)
  })
}

export function getTenantById(id) {
  return request({
    url: `/api/saas/tenants/${id}`,
    method: 'get'
  })
}

export function createTenant(payload) {
  return request({
    url: '/api/saas/tenants',
    method: 'post',
    data: payload
  })
}

export function updateTenant(payload) {
  return request({
    url: `/api/saas/tenants/${payload.id}`,
    method: 'put',
    data: payload
  })
}

export function deleteTenant(id) {
  return request({
    url: `/api/saas/tenants/${id}`,
    method: 'delete'
  })
}

export function getDefaultConnectionString(id) {
  return request({
    url: `/api/saas/tenants/${id}/connection-string`,
    method: 'get'
  })
}

export function getDefaultConnectionStringByName(id, name) {
  return request({
    url: `/api/saas/tenants/${id}/connection-string/${name}`,
    method: 'get'
  })
}

export function updateDefaultConnectionString(id, payload) {
  return request({
    url: `/api/saas/tenants/${id}/connection-string`,
    method: 'put',
    data: payload
  })
}

export function deleteDefaultConnectionString(id) {
  return request({
    url: `/api/saas/tenants/${id}/connection-string`,
    method: 'delete'
  })
}
