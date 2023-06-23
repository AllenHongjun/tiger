import request from '@/utils/requestMock'

export function getServerInfo(query) {
  return request({
    url: `/api/minitor/server-info`,
    method: 'get',
    params: query
  })
}

export function fetchArticle(id) {
  return request({
    url: '/vue-element-template/article/detail',
    method: 'get',
    params: { id }
  })
}

export function fetchPv(pv) {
  return request({
    url: '/vue-element-template/article/pv',
    method: 'get',
    params: { pv }
  })
}

export function createArticle(data) {
  return request({
    url: '/vue-element-template/article/create',
    method: 'post',
    data
  })
}

export function updateArticle(data) {
  return request({
    url: '/vue-element-template/article/update',
    method: 'post',
    data
  })
}

export function fetchComments(id) {
  return request({
    url: `/article/${id}/comments`,
    method: 'get'
  })
}
