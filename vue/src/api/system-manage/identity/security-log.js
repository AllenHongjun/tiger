import request from '@/utils/request'
import qs from 'querystring'
import { transformAbpListQuery } from '@/utils/abp'

export function getSecurityLogs(query){
    return request({
        url: `/api/identity/security-log`,
        method:'get',
        params:transformAbpListQuery(query)
    })
}

export function getSecurityLog(id) {
    return request({
        url: `/api/identity/security-log/${id}`,
        method:`get`
    })
}

export function deleteSecurityLog(id){
    return request({
        url:`/api/identity/security-log/${id}`,
        method:`delete`
    })
}

