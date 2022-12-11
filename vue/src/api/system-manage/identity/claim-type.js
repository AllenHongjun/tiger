// 导入请求工具方法
import request from '@/utils/request'

import { transformAbpListQuery } from '@/utils/abp'

export function getClaimTypes(query) {
    return request({
        url: `/api/identity/claim-type`,
        method: 'get',
        params: transformAbpListQuery(query)
    })
}

export function getClaimType(id) {
    return request({
        url: `/api/identity/claim-type/${id}`,
        method: `get`
    })
}

export function createClaimType(payload) {
    return request({
        url: '/api/identity/claim-type',
        method: 'post',
        data: payload
    })
}

export function updateClaimType(payload) {
    return request({
        url: `/api/identity/claim-type/${payload.id}`,
        method: 'put',
        data: payload
    })
}

export function deleteClaimType(id) {
    return request({
        url: `/api/identity/claim-type/${id}`,
        method: `delete`
    })
}

