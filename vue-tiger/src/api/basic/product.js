import request from '@/utils/request'
import { transformAbpListQuery } from '@/utils/abp'

export function getProducts(query) {
  return request({
    url: '/api/basic/product',
    method: 'get',
    params: transformAbpListQuery(query)
  })
}

export function getProductById(id) {
  return request({
    url: `/api/basic/product/${id}`,
    method: 'get'
  })
}

export function createProduct(payload) {
  return request({
    url: '/api/basic/product',
    method: 'post',
    data: payload
  })
}

export function updateProduct(payload) {
  return request({
    url: `/api/basic/product/${payload.id}`,
    method: 'put',
    data: payload
  })
}

export function deleteProduct(id) {
  return request({
    url: `/api/basic/product/${id}`,
    method: 'delete'
  })
}
