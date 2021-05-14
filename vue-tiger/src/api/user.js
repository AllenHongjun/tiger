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

export function logout() {
  return request({
    url: '/api/account/logout',
    method: 'get'
  })
}

// 根据名称获取租户
export function getTenantByName(name) {
  return request({
    url: `/api/abp/multi-tenancy/tenants/by-name/${name}`,
    method: 'get'
  })
}

// 获取全局配置信息
export function getApplicationConfiguration(name) {
  return request({
    url: `/api/abp/application-configuration`,
    method: 'get'
  })
}


export function getInfo() {
  return request({
    url: '/api/identity/my-profile',
    method: 'get'
  })
}

// user
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

export function updateUser(id,payload) {
  return request({
    url: `/api/identity/users/${id}`,
    method: 'put',
    data: payload
  })
}

export function deleteUser(id) {
  return request({
    url: `/api/identity/users/${id}`,
    method: 'delete'
  })
}


export function changePassword(payload) {
  return request({
    url: `/api/identity/my-profile/change-password`,
    method: 'post',
    data: payload
  })
}




// 获取用户所有可用的角色
export function getAssignableRoles() {
  return request({
    url: '/api/identity/users/assignable-roles',
    method: 'get',
  })
}

// 获取用户关联的角色
export function getUserRoles(id) {
  return request({
    url: `/api/identity/users/${id}/roles`,
    method: 'get',
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

