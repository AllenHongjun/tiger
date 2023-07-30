import request from '@/utils/request'
import { transformAbpListQuery } from '@/utils/abp'

// 数据字典接口

export function getDataList(params) {
  return request({
    url: '/api/platform/datas',
    method: 'get',
    params: transformAbpListQuery(params)
  })
}

export function getData(id) {
  return request({
    url: `/api/platform/datas/${id}`,
    method: 'get'
  })
}

export function createData(payload) {
  return request({
    url: '/api/platform/datas',
    method: 'post',
    data: payload
  })
}

export function updateData(id, payload) {
  return request({
    url: `/api/platform/datas/${id}`,
    method: 'put',
    data: payload
  })
}

export function deleteData(id) {
  return request({
    url: `/api/platform/datas/${id}`,
    method: 'delete'
  })
}

export function getDataByName(name) {
  return request({
    url: `/api/platform/datas/by-name/${name}`,
    method: 'get'
  })
}

export function getDataAll() {
  return request({
    url: '/api/platform/datas/all',
    method: 'get',
    params: transformAbpListQuery()
  })
}

export function MoveData(id) {
  return request({
    url: `/api/platform/datas/${id}/move`,
    method: 'put'
  })
}

// 数据字典项目

export function createDataItem(id, payload) {
  return request({
    url: `/api/platform/datas/${id}/items`,
    method: 'post',
    data: payload
  })
}

export function updateDataItem(id, name, payload) {
  return request({
    url: `/api/platform/datas/${id}/items/${name}`,
    method: 'put',
    data: payload
  })
}

export function deleteDataItem(id, name) {
  return request({
    url: `/api/platform/datas/${id}/items/${name}`,
    method: 'delete'
  })
}
