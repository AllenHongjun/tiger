import request from '@/utils/requestMock'

// 查询服务器信息
export function getServerInfo() {
  return request({
    url: `/api/monitor/server-info`,
    method: 'get'
  })
}

// 查询.NET运行时信息
export function getCLRInfo() {
  return request({
    url: `/api/monitor/clr-info`,
    method: 'get'
  })
}

// 获取系统使用信息
export function getSystemUsedInfo() {
  return request({
    url: `/api/monitor/system-used-info`,
    method: 'get'
  })
}

// 获取服务器磁盘信息
export function getDiskInfo() {
  return request({
    url: '/api/monitor/disk-info',
    method: 'get'
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

// role
export function getRoleList(params) {
  return request({
    url: '/api/identity/roles/search',
    method: 'get'
    // params: transformAbpListQuery(params)
  })
}

export function getRole(id) {
  return request({
    url: `/api/identity/roles/${id}`,
    method: 'get'
  })
}

export function createRole(payload) {
  return request({
    url: '/api/identity/roles',
    method: 'post',
    data: payload
  })
}

export function updateRole(id, payload) {
  return request({
    url: `/api/identity/roles/${id}`,
    method: 'put',
    data: payload
  })
}

export function deleteRole(id) {
  return request({
    url: `/api/identity/roles/${id}`,
    method: 'delete'
  })
}

export function createRoleToOrg(payload) {
  return request({
    url: '/api/identity/roles/create-to-organization',
    method: 'post',
    data: payload
  })
}
