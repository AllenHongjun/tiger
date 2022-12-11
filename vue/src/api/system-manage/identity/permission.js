import request from '@/utils/request'
import qs from 'querystring'
import { transformAbpListQuery } from '@/utils/abp'


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