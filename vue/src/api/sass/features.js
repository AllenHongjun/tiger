// import request from '@/utils/request'
import request from '@/utils/requestMock'

export function getFeatures(params) {
  return request({
    url: '/feature-management/features',
    method: 'get',
    params: params
  })
}

export function updateFeatures(query, payload) {
  return request({
    url: '/feature-management/features',
    method: 'put',
    params: query,
    data: payload
  })
}
