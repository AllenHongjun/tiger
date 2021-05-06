import request from '@/utils/request'

export function fetchList(query) {
  return request({
    url: '/vue/security-log/list',
    method: 'get',
    params: query
  })
}

export function fetchArticle(id) {
  return request({
    url: '/vue/security-log/detail',
    method: 'get',
    params: { id }
  })
}

export function fetchPv(pv) {
  return request({
    url: '/vue/security-log/pv',
    method: 'get',
    params: { pv }
  })
}
