import request from '@/utils/request'
import { transformAbpListQuery } from '@/utils/abp'

export function getRoleList(params) {
  return request({
    url: '/api/identity/roles/search',
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

export function updateRole(id, payload) {
  return request({
    url: `/api/identity/roles/${id}`,
    method: 'put',
    data: payload
  })
}

export function deleteRole(id) {
  return request({
    url: `/api/identity/roles/${id}`,
    method: 'delete'
  })
}

export function moveAllUsers(roleId, targetRoleId) {
  return request({
    url: `/api/identity/roles/${roleId}/move-all-users/${targetRoleId}`,
    method: 'put'
  })
}

export function createRoleToOrg(payload) {
  return request({
    url: '/api/identity/roles/create-to-organization',
    method: 'post',
    data: payload
  })
}

// 角色声明
export function getRoleClaims(roleId) {
  return request({
    url: `/api/identity/roles/${roleId}/claims`,
    method: 'get'
  })
}

export function createRoleClaim(roleId, payload) {
  return request({
    url: `/api/identity/roles/${roleId}/create-claim`,
    method: 'post',
    data: payload
  })
}

export function updateRoleClaim(roleId, payload) {
  return request({
    url: `/api/identity/roles/${roleId}/update-claim`,
    method: 'put',
    data: payload
  })
}

export function deleteRoleClaim(roleId, payload) {
  return request({
    url: `/api/identity/roles/${roleId}/delete-claim`,
    method: 'post',
    data: payload
  })
}
