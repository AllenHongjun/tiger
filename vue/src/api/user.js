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



// profile 修改用户信息
export function setUserInfo(data) {
  return request({
    url: '/api/identity/my-profile',
    method: 'put',
    data: data
  })
}

export function changePassword(payload) {
  return request({
    url: `/api/identity/my-profile/change-password`,
    method: 'post',
    data: payload
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


