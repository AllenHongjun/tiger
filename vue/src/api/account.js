import request from '@/utils/request'
import qs from 'querystring'

// 发送短信登录验证码
export function SendSignCode(payload) {
  return request({
    url: '/api/account/phone/send-signin-code',
    method: 'post',
    data: payload
  })
}

export function register(payload) {
  return request({
    url: '/api/account/register',
    method: 'post',
    data: payload
  })
}

export function sendPasswordResetCode(payload) {
  return request({
    url: '/api/account/send-password-reset-code',
    method: 'post',
    data: payload
  })
}

export function resetPassword(payload) {
  return request({
    url: '/api/account/reset-password',
    method: 'post',
    data: payload
  })
}

