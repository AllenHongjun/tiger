import request from '@/utils/requestMock'
import { transformAbpListQuery } from '@/utils/abp'

export function getEditions(query) {
  return request({
    url: '/api/sass/edtions',
    method: 'get',
    params: transformAbpListQuery(query)
  })
}

export function getEditionById(id) {
  return request({
    url: `/api/sass/edtions/${id}`,
    method: 'get'
  })
}

export function createEdition(payload) {
  return request({
    url: '/api/sass/edtions',
    method: 'post',
    data: payload
  })
}

export function updateEdition(payload) {
  return request({
    url: `/api/sass/edtions/${payload.id}`,
    method: 'put',
    data: payload
  })
}

export function deleteEdition(id) {
  return request({
    url: `/api/sass/edtions/${id}`,
    method: 'delete'
  })
}

