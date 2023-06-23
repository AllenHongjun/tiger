import request from '@/utils/requestMock'

export function fetchList(query) {
  console.log('process.env.BASE_API2', process.env.BASE_API2)
  return request({
    url: '/article/list',
    method: 'get',
    params: query
  })
}

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
