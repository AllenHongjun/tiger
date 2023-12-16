import request from '@/utils/request'
import { transformAbpListQuery } from '@/utils/abp'

export function getMenuList(params) {
  return request({
    url: '/api/platform/menus/search',
    method: 'get',
    params: transformAbpListQuery(params)
  })
}

export function getMenu(id) {
  return request({
    url: `/api/platform/menus/${id}`,
    method: 'get'
  })
}

export function getAllMenu(params) {
  return request({
    url: `/api/platform/menus/all`,
    method: 'get',
    params: transformAbpListQuery(params)
  })
}

export function createMenu(payload) {
  return request({
    url: '/api/platform/menus',
    method: 'post',
    data: payload
  })
}

export function updateMenu(id, payload) {
  return request({
    url: `/api/platform/menus/${id}`,
    method: 'put',
    data: payload
  })
}

export function deleteMenu(id) {
  return request({
    url: `/api/platform/menus/${id}`,
    method: 'delete'
  })
}

