import request from '@/utils/request'
import { transformAbpListQuery } from '@/utils/abp'

export function getTextTemplates(query) {
  return request({
    url: '/api/text-template/templates',
    method: 'get',
    params: transformAbpListQuery(query)
  })
}

export function getTextTemplateByName(name) {
  return request({
    url: `/api/text-template/templates/${name}`,
    method: 'get'
  })
}

// 通过名称查询文本模板内容
export function getTextTemplateContentByName(name) {
  return request({
    url: `/api/text-template/templates/content/${name}`,
    method: 'get'
  })
}

export function updateTextTemplate(payload) {
  return request({
    url: `/api/text-template/templates`,
    method: 'post',
    data: payload
  })
}

export function restoreToDefault(payload) {
  return request({
    url: `/api/text-template/templates/restore-to-default`,
    method: 'put',
    data: payload
  })
}

