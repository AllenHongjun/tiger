import request from '@/utils/request'
import { transformAbpListQuery } from '@/utils/abp'

export function getEditions(query) {
  return request({
    url: '/api/saas/editions',
    method: 'get',
    params: transformAbpListQuery(query)
  })
}

export function getEditionById(id) {
  return request({
    url: `/api/saas/editions/${id}`,
    method: 'get'
  })
}

export function createEdition(payload) {
  return request({
    url: '/api/saas/editions',
    method: 'post',
    data: payload
  })
}

export function updateEdition(id, payload) {
  return request({
    url: `/api/saas/editions/${id}`,
    method: 'put',
    data: payload
  })
}

export function deleteEdition(id) {
  return request({
    url: `/api/saas/editions/${id}`,
    method: 'delete'
  })
}

export function moveAllTenants(editionId, dstEditionId) {
  return request({
    url: `/api/saas/editions/${editionId}/move-all-tenant/${dstEditionId}`,
    method: 'put'
  })
}

export function getEditionUsageStat() {
  return request({
    url: '/api/saas/editions/statistics/usage-statistic',
    method: 'get'
  })
}

