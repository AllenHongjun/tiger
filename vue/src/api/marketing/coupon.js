import request from '@/utils/request'
import { transformAbpListQuery } from '@/utils/abp'

export function getCoupons(query) {
  return request({
    url: '/api/marketing/coupon',
    method: 'get',
    params: transformAbpListQuery(query)
  })
}

export function getCouponById(id) {
  return request({
    url: `/api/marketing/coupon/${id}`,
    method: 'get'
  })
}

export function createCoupon(payload) {
  return request({
    url: '/api/marketing/coupon',
    method: 'post',
    data: payload
  })
}

export function updateCoupon(payload) {
  return request({
    url: `/api/marketing/coupon/${payload.id}`,
    method: 'put',
    data: payload
  })
}

export function deleteCoupon(id) {
  return request({
    url: `/api/marketing/coupon/${id}`,
    method: 'delete'
  })
}
