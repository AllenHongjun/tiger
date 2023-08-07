import request from '@/utils/request'
import { transformAbpListQuery } from '@/utils/abp'

export function getClients(params) {
  return request({
    url: '/api/identity-server/clients',
    method: 'get',
    params: transformAbpListQuery(params)
  })
}

export function getClient(id) {
  return request({
    url: `/api/identity-server/clients/${id}`,
    method: 'get'
  })
}

export function createClient(payload) {
  return request({
    url: '/api/identity-server/clients',
    method: 'post',
    data: payload
  })
}

export function updateClient(id, payload) {
  return request({
    url: `/api/identity-server/clients/${id}`,
    method: 'put',
    data: payload
  })
}

export function deleteClient(id) {
  return request({
    url: `/api/identity-server/clients/${id}`,
    method: 'delete'
  })
}

