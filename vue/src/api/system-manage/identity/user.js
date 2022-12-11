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

export function deleteUser(id) {
  return request({
    url: `/api/identity/users/${id}`,
    method: 'delete'
  })
}