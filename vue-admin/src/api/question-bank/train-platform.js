import request from '@/utils/request'
import { transformAbpListQuery } from '@/utils/abp'

export function getTrainPlatforms(params) {
  return request({
    url: '/api/question-bank/train-platforms',
    method: 'get',
    params: transformAbpListQuery(params)
  })
}

export function getTrainPlatform(id) {
  return request({
    url: `/api/question-bank/train-platforms/${id}`,
    method: 'get'
  })
}

export function getAllTrainPlatform(params) {
  return request({
    url: `/api/question-bank/train-platforms/all`,
    method: 'get'
  })
}

export function createTrainPlatform(payload) {
  return request({
    url: '/api/question-bank/train-platforms',
    method: 'post',
    data: payload
  })
}

export function updateTrainPlatform(id, payload) {
  return request({
    url: `/api/question-bank/train-platforms/${id}`,
    method: 'put',
    data: payload
  })
}

export function deleteTrainPlatform(id) {
  return request({
    url: `/api/question-bank/train-platforms/${id}`,
    method: 'delete'
  })
}

