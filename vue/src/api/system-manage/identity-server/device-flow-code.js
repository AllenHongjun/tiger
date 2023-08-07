import request from '@/utils/request'
import { transformAbpListQuery } from '@/utils/abp'

export function getDeviceFlowCodes(params) {
  return request({
    url: '/api/identity-server/device-flow-codes',
    method: 'get',
    params: transformAbpListQuery(params)
  })
}

export function getDeviceFlowCode(id) {
  return request({
    url: `/api/identity-server/device-flow-codes/${id}`,
    method: 'get'
  })
}

export function getAllDeviceFlowCode() {
  return request({
    url: `/api/identity-server/device-flow-codes/all`,
    method: 'get'
  })
}

export function createDeviceFlowCode(payload) {
  return request({
    url: '/api/identity-server/device-flow-codes',
    method: 'post',
    data: payload
  })
}

export function updateDeviceFlowCode(id, payload) {
  return request({
    url: `/api/identity-server/device-flow-codes/${id}`,
    method: 'put',
    data: payload
  })
}

export function deleteDeviceFlowCode(id) {
  return request({
    url: `/api/identity-server/device-flow-codes/${id}`,
    method: 'delete'
  })
}

