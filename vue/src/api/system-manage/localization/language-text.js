import request from '@/utils/request'
import { transformAbpListQuery } from '@/utils/abp'

export function getLanguageTexts(params) {
  return request({
    url: '/api/localization/language-text',
    method: 'get',
    params: transformAbpListQuery(params)
  })
}

export function getLanguageText(id) {
  return request({
    url: `/api/localization/language-text/${id}`,
    method: 'get'
  })
}

export function getAllLanguageText() {
  return request({
    url: `/api/localization/language-text/all`,
    method: 'get'
  })
}

export function createLanguageText(payload) {
  return request({
    url: '/api/localization/language-text',
    method: 'post',
    data: payload
  })
}

export function updateLanguageText(id, payload) {
  return request({
    url: `/api/localization/language-text/${id}`,
    method: 'put',
    data: payload
  })
}

export function deleteLanguageText(id) {
  return request({
    url: `/api/localization/language-text/${id}`,
    method: 'delete'
  })
}

