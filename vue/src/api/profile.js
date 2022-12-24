import request from '@/utils/request'
import qs from 'querystring'
import { transformAbpListQuery } from '@/utils/abp'

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