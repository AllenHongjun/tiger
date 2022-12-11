import request from '@/utils/request'
import { transformAbpListQuery } from '@/utils/abp'

export function getProductAttributeTypes(query) {
  return request({
    url: '/api/basic/productAttributeType',
    method: 'get',
    params: transformAbpListQuery(query)
  })
}

// 获取所有规格类型
export function getAllProductAttributeTypes() {
  return request({
    url: '/api/basic/productAttributeType/all',
    method: 'get'
  })
}

export function getProductAttributeTypeById(id) {
  return request({
    url: `/api/basic/productAttributeType/${id}`,
    method: 'get'
  })
}

export function createProductAttributeType(payload) {
  return request({
    url: '/api/basic/productAttributeType',
    method: 'post',
    data: payload
  })
}

export function updateProductAttributeType(payload) {
  return request({
    url: `/api/basic/productAttributeType/${payload.id}`,
    method: 'put',
    data: payload
  })
}

export function deleteProductAttributeType(id) {
  return request({
    url: `/api/basic/productAttributeType/${id}`,
    method: 'delete'
  })
}
