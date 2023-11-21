import request from '@/utils/request'
import { transformAbpListQuery } from '@/utils/abp'

export function getAuditLogs(query) {
  return request({
    url: '/api/audit-logging/audit-logs',
    method: 'get',
    params: transformAbpListQuery(query)
  })
}

export function getAuditLog(id) {
  return request({
    url: `/api/audit-logging/audit-logs/${id}`,
    method: 'get'
  })
}

export function deleteAuditLog(id) {
  return request({
    url: `/api/audit-logging/audit-logs/${id}`,
    method: 'delete'
  })
}

export function deleteManyAuditLog(ids) {
  return request({
    url: `/api/audit-logging/audit-logs/delete-many`,
    method: 'delete',
    data: ids
  })
}

// 获取日志每日请求平均时间
export function getAuditLogAverageExecutionDurationPerDay(query) {
  return request({
    url: '/api/audit-logging/audit-logs/average-execution-duration-per-day',
    method: 'get',
    params: query
  })
}

// 获取日志中的错误率
export function getAuditLogErrorRate(query) {
  return request({
    url: '/api/audit-logging/audit-logs/error-rate',
    method: 'get',
    params: query
  })
}

export function getAuditLogEntityChanges(query) {
  return request({
    url: '/api/audit-logging/audit-logs/entity-changes',
    method: 'get',
    params: transformAbpListQuery(query)
  })
}

export function getAuditLogEntityChange(id) {
  return request({
    url: `/api/audit-logging/audit-logs/entity-changes/${id}`,
    method: 'get'
  })
}

export function getAuditLogEntityChangeListWithUserName(query) {
  return request({
    url: `/api/audit-logging/audit-logs/entity-change-with-username`,
    method: 'get',
    params: query
  })
}

export function getAuditLogEntityChangeWithUserName(id) {
  return request({
    url: `/api/audit-logging/audit-logs/entity-change-with-username/${id}`,
    method: 'get'
  })
}

