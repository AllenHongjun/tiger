import request from '@/utils/request'

// profile
export function getInfo() {
  return request({
    url: '/api/identity/my-profile',
    method: 'get'
  })
}

export function setUserInfo(data) {
  return request({
    url: '/api/identity/my-profile',
    method: 'put',
    data: data
  })
}

export function changePassword(payload) {
  return request({
    url: `/api/identity/my-profile/change-password`,
    method: 'post',
    data: payload
  })
}

export function sendChangePhoneNumberCode(payload) {
  return request({
    url: `/api/account/profile/send-change-phone-number-code`,
    method: 'post',
    data: payload
  })
}

export function changePhoneNumber(payload) {
  return request({
    url: `/api/account/profile/change-phone-number`,
    method: 'post',
    data: payload
  })
}

export function sendEmailConfirmLink(payload) {
  return request({
    url: `/api/account/profile/send-email-confirm-link`,
    method: 'post',
    data: payload
  })
}

export function confirmEmail(payload) {
  return request({
    url: `/api/account/profile/confirm-email`,
    method: 'post',
    data: payload
  })
}

