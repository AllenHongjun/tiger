// import request from '@/utils/request'
import { transformAbpListQuery } from '@/utils/abp'
import request from '@/utils/requestMock'

export function getPostList(params) {
  return request({
    url: '/api/identity/post/search',
    method: 'get',
    params: transformAbpListQuery(params)
  })
}

export function getPost(id) {
  return request({
    url: `/api/identity/post/${id}`,
    method: 'get'
  })
}

export function createPost(payload) {
  return request({
    url: '/api/identity/post',
    method: 'post',
    data: payload
  })
}

export function updatePost(id, payload) {
  return request({
    url: `/api/identity/post/${id}`,
    method: 'put',
    data: payload
  })
}

export function deletePost(id) {
  return request({
    url: `/api/identity/post/${id}`,
    method: 'delete'
  })
}

