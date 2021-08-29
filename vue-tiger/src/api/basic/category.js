import request from '@/utils/request'
import { transformAbpListQuery } from '@/utils/abp'

export function getCategories(query) {
  return request({
    url: '/api/basic/category',
    method: 'get',
    params: transformAbpListQuery(query)
  })
}

export function getCategoryById(id) {
  return request({
    url: `/api/basic/category/${id}`,
    method: 'get'
  })
}

export function createCategory(payload) {
  return request({
    url: '/api/basic/category',
    method: 'post',
    data: payload
  })
}

export function updateCategory(payload) {
  return request({
    url: `/api/basic/category/${payload.id}`,
    method: 'put',
    data: payload
  })
}

export function deleteCategory(id) {
  return request({
    url: `/api/basic/category/${id}`,
    method: 'delete'
  })
}
