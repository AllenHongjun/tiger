import request from '@/utils/request'
import { transformAbpListQuery } from '@/utils/abp'
import qs from 'querystring'
import internal from 'stream'

export function getQuestionCategories(params) {
  return request({
    url: '/api/question-bank/question-categories',
    method: 'get',
    params: transformAbpListQuery(params)
  })
}

export function getQuestionCategory(id) {
  return request({
    url: `/api/question-bank/question-categories/${id}`,
    method: 'get'
  })
}

export function getAllQuestionCategory(params) {
  return request({
    url: `/api/question-bank/question-categories/all`,
    method: 'get',
    params: params
  })
}

export function createQuestionCategory(payload) {
  return request({
    url: '/api/question-bank/question-categories',
    method: 'post',
    data: payload
  })
}

export function updateQuestionCategory(id, payload) {
  return request({
    url: `/api/question-bank/question-categories/${id}`,
    method: 'put',
    data: payload
  })
}

export function deleteQuestionCategory(id) {
  return request({
    url: `/api/question-bank/question-categories/${id}`,
    method: 'delete'
  })
}

export function ExportQuestionCategoryXlsxTemplate() {
  return request({
    url: `/api/question-bank/question-categories/export-xlxs-template`,
    method: 'get',
    header: {
      headers: { 'Content-Type': 'application/x-download' }
    },
    responseType: 'blob'
  })
}

export function ImportQuestionCategoryFromXlsx(payload) {
  return request({
    url: `/api/question-bank/question-categories/import-from-xlsx`,
    method: 'post',
    data: payload,
    headers: {
      'Content-Type': 'multipart/form-data;charset=utf-8'
    }
  })
}

export function ExportQuestionCategoryToXlsx(params) {
  return request({
    url: `/api/question-bank/question-categories/export-to-xlsx`,
    method: 'get',
    params: transformAbpListQuery(params),
    header: {
      headers: { 'Content-Type': 'application/x-download' }
    },
    responseType: 'blob'
  })
}

/**
 * 切换启用
 * @param {internal} id
 * @returns
 */
export function toggleEnable(id) {
  return request({
    url: `/api/question-bank/question-categories/${id}/toggle-enable`,
    method: 'put'
  })
}

/**
 * 更新序号
 * @param {internal} id
 * @param {internal} sort
 * @returns
 */
export function updateSort(id, sort) {
  return request({
    url: `/api/question-bank/question-categories/${id}/update-sort/${sort}`,
    method: 'put'
  })
}

