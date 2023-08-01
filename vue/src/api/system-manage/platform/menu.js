import request from '@/utils/request'
import { transformAbpListQuery } from '@/utils/abp'

export function getMenuList(params) {
  return request({
    url: '/api/identity/menus/search',
    method: 'get',
    params: transformAbpListQuery(params)
  })
}

export function getMenu(id) {
  return request({
    url: `/api/identity/menus/${id}`,
    method: 'get'
  })
}

export function getAllMenu(params) {
  return request({
    url: `/api/identity/menus/all`,
    method: 'get',
    params: transformAbpListQuery(params)
  })
}

export function createMenu(payload) {
  return request({
    url: '/api/identity/menus',
    method: 'post',
    data: payload
  })
}

export function updateMenu(id, payload) {
  return request({
    url: `/api/identity/menus/${id}`,
    method: 'put',
    data: payload
  })
}

export function deleteMenu(id) {
  return request({
    url: `/api/identity/menus/${id}`,
    method: 'delete'
  })
}

export function createMenuToOrg(payload) {
  return request({
    url: '/api/identity/menus/create-to-organization',
    method: 'post',
    data: payload
  })
}
