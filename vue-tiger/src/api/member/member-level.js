import request from '@/utils/request'
import { transformAbpListQuery } from '@/utils/abp'

export function getMemberLevels(query) {
  return request({
    url: '/api/basic/memberlevel/all',
    method: 'get',
    params: transformAbpListQuery(query)
  })
}

export function getMemberLevelById(id) {
  return request({
    url: `/api/basic/memberlevel/${id}`,
    method: 'get'
  })
}

export function createMemberLevel(payload) {
  return request({
    url: '/api/basic/memberlevel',
    method: 'post',
    data: payload
  })
}

export function updateMemberLevel(payload) {
  return request({
    url: `/api/basic/memberlevel/${payload.id}`,
    method: 'put',
    data: payload
  })
}

export function deleteMemberLevel(id) {
  return request({
    url: `/api/basic/memberlevel/${id}`,
    method: 'delete'
  })
}
