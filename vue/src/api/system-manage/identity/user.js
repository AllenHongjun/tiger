import request from '@/utils/request'
import qs from 'querystring'
import { transformAbpListQuery } from '@/utils/abp'

// user
export function getUserList(params) {
  return request({
    url: '/api/identity/users',
    method: 'get',
    params: transformAbpListQuery(params)
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

export function updateUser(id, payload) {
  return request({
    url: `/api/identity/users/${id}`,
    method: 'put',
    data: payload
  })
}

export function ChangePassword(id, payload) {
  return request({
    url: `/api/identity/users/${id}/change-password`,
    method: 'put',
    data: payload
  })
}

export function Lock(id, payload) {
  return request({
    url: `/api/identity/users/${id}/lock`,
    method: 'put',
    params: payload
  })
}

export function UnLock(id) {
  return request({
    url: `/api/identity/users/${id}/unlock`,
    method: 'put'
  })
}

export function deleteUser(id) {
  return request({
    url: `/api/identity/users/${id}`,
    method: 'delete'
  })
}

// 添加用户同时关联组织机构
export function createUserToOrg(payload) {
  return request({
    url: '/api/identity/users/create-to-organizations',
    method: 'post',
    data: payload
  })
}

// 修改用户关联组织机构
export function updateUserToOrg(payload) {
  return request({
    url: `/api/identity/users/${payload.id}/update-to-organizations`,
    method: 'put',
    data: payload
  })
}

// 根据用户id获取关联的角色
export function getRolesByUserId(id) {
  return request({
    url: `/api/identity/users/${id}/roles`,
    method: 'get'
  })
}

//
export function getAssignableRoles() {
  return request({
    url: '/api/identity/users/assignable-roles',
    method: 'get'
  })
}

// 根据用户id获取关联的组织机构
export function getOrganizationsByUserId(id, includeDetails = false) {
  return request({
    url: `/api/identity/users/${id}/organizations`,
    method: 'get',
    params: includeDetails
  })
}
