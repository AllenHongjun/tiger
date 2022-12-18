import request from '@/utils/request'
import qs from 'querystring'
import { transformAbpListQuery } from '@/utils/abp'

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

  export function createRoleToOrg(payload) {
    return request({
      url: '/api/identity/roles/create-to-organization',
      method: 'post',
      data: payload
    })
  }