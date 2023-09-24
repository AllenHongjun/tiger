import request from '@/utils/request'
import { transformAbpListQuery } from '@/utils/abp'

export function getLanguages(params) {
  return request({
    url: '/api/localization/languages',
    method: 'get',
    params: transformAbpListQuery(params)
  })
}

export function getLanguage(id) {
  return request({
    url: `/api/localization/languages/${id}`,
    method: 'get'
  })
}

export function getAllLanguage() {
  return request({
    url: `/api/localization/languages/all`,
    method: 'get'
  })
}

export function createLanguage(payload) {
  return request({
    url: '/api/localization/languages',
    method: 'post',
    data: payload
  })
}

export function updateLanguage(id, payload) {
  return request({
    url: `/api/localization/languages/${id}`,
    method: 'put',
    data: payload
  })
}

export function deleteLanguage(id) {
  return request({
    url: `/api/localization/languages/${id}`,
    method: 'delete'
  })
}

