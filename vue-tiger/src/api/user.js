import request from '@/utils/request'
import qs from 'querystring'
import { transformAbpListQuery } from '@/utils/abp'

export function login(data) {
  return request({
    url: '/connect/token',
    method: 'post',
    headers: { 'content-type': 'application/x-www-form-urlencoded' },
    data: qs.stringify(data)
  })
}

export function getInfo() {
  return request({
    url: '/api/identity/my-profile',
    method: 'get'
  })
}

export function logout() {
  return request({
    url: '/api/account/logout',
    method: 'get'
  })
}

export function getUserList(params) {
  return request({
    url: '/api/identity/users',
    method: 'get',
    params:transformAbpListQuery(params)
  })
}

export function getUser(id) {
  return request({
    url: `/api/identity/users/${id}`,
    method: 'get'
  })
}

export function createUser(payload) {
  return request({
    url: '/api/identity/users',
    method: 'post',
    data: payload
  })
}

// role
export function getRoleList(params) {
  return request({
    url: '/api/identity/roles',
    method: 'get',
    params: transformAbpListQuery(params)
  })
}

export function getRole(id) {
  return request({
    url: `/api/identity/roles/${id}`,
    method: 'get'
  })
}

export function createRole(payload) {
  return request({
    url: '/api/identity/roles',
    method: 'post',
    data: payload
  })
}

export function deleteRole(id) {
  return request({
    url: `/api/identity/roles/${id}`,
    method: 'delete'
  })
}


// audit_log
export function getAuditLogList(params) {
  return request({
    url: '/api/app/auditLog',
    method: 'get',
    params: transformAbpListQuery(params)
  })
}

// permission
export function getPermissions(query) {
  return request({
    url: '/api/permission-management/permissions',
    method: 'get',
    params: query
  })
}

export function updatePermissions(query, payload) {
  return request({
    url: `/api/permission-management/permissions`,
    method: 'put',
    params: query,
    data: payload
  })
}


// tenant
export function getTenantList(params) {
  return request({
    url: '/api/multi-tenancy/tenants',
    method: 'get',
    params: transformAbpListQuery(params)
  })
}

export function getTenant(id) {
  return request({
    url: `/api/multi-tenancy/tenants/${id}`,
    method: 'get'
  })
}

export function createTenant(payload) {
  return request({
    url: '/api/multi-tenancy/tenants',
    method: 'post',
    data: payload
  })
}

export function updateTenant(id,payload) {
  return request({
    url: `/api/multi-tenancy/tenants/${id}`,
    method: 'put',
    data: payload
  })
}

export function deleteTenant(id) {
  return request({
    url: `/api/multi-tenancy/tenants/${id}`,
    method: 'delete'
  })
}

