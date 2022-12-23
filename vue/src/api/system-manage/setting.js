import request from '@/utils/request'

export function getSettingValues() {
  return request({
    url: '/api/setting/get-list',
    method: 'get'
  })
}

export function setSettingValues(values) {
  return request({
    url: '/api/setting/set-setting-values',
    method: 'put',
    data: values
  })
}

export function resetSettingValues(values) {
  return request({
    url: '/api/setting-ui/reset-setting-values',
    method: 'put',
    data: values
  })
}
