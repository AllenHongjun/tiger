// import request from '@/utils/request'
import request from '@/utils/requestMock'
import { transformAbpListQuery } from '@/utils/abp'

export function getMenuList(params) {
  return request({
    url: '/api/identity/menu/search',
    method: 'get',
    params: transformAbpListQuery(params)
  })
}

export function getMenu(id) {
  return request({
    url: `/api/identity/menu/${id}`,
    method: 'get'
  })
}

export function createMenu(payload) {
  return request({
    url: '/api/identity/menu',
    method: 'post',
    data: payload
  })
}

export function updateMenu(id, payload) {
  return request({
    url: `/api/identity/menu/${id}`,
    method: 'put',
    data: payload
  })
}

export function deleteMenu(id) {
  return request({
    url: `/api/identity/menu/${id}`,
    method: 'delete'
  })
}

