// 导入请求工具方法
import request from '@/utils/request'
import qs from 'querystring'
import { transformAbpListQuery } from '@/utils/abp'

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

