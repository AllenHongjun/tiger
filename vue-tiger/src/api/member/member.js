import request from '@/utils/request'
import { transformAbpListQuery } from '@/utils/abp'

export function getMembers(query) {
  return request({
    url: '/api/basic/member',
    method: 'get',
    params: transformAbpListQuery(query)
  })
}

export function getMemberById(id) {
  return request({
    url: `/api/basic/member/${id}`,
    method: 'get'
  })
}

export function createMember(payload) {
  return request({
    url: '/api/basic/member',
    method: 'post',
    data: payload
  })
}

export function updateMember(payload) {
  return request({
    url: `/api/basic/member/${payload.id}`,
    method: 'put',
    data: payload
  })
}

export function deleteMember(id) {
  return request({
    url: `/api/basic/member/${id}`,
    method: 'delete'
  })
}
