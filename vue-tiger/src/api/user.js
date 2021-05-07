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

export function getList(params) {
  return request({
    url: '/api/identity/users',
    method: 'get',
    params
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

