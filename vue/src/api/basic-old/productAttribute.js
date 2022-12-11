import request from '@/utils/request'
import { transformAbpListQuery } from '@/utils/abp'

export function getProductAttributes(query) {
  return request({
    url: '/api/basic/productAttribute',
    method: 'get',
    params: transformAbpListQuery(query)
  })
}

export function getProductAttributeById(id) {
  return request({
    url: `/api/basic/productAttribute/${id}`,
    method: 'get'
  })
}

// 根据规格类型id获取 属性
export function getProductAttributeByTypeId(id) {
  return request({
    url: `/api/basic/productAttribute/getListByType?productAttributeTypeId=${id}`,
    method: 'get'
  })
}

export function createProductAttribute(payload) {
  return request({
    url: '/api/basic/productAttribute',
    method: 'post',
    data: payload
  })
}

export function updateProductAttribute(payload) {
  return request({
    url: `/api/basic/productAttribute/${payload.id}`,
    method: 'put',
    data: payload
  })
}

export function deleteProductAttribute(id) {
  return request({
    url: `/api/basic/productAttribute/${id}`,
    method: 'delete'
  })
}
