import request from '@/utils/request'
import { transformAbpListQuery } from '@/utils/abp'

export function createOrganization(payload) {
  return request({
    url: '/api/identity/organizations',
    method: 'post',
    data: payload
  })
}

export function updateOrganization(id, payload) {
  return request({
    url: `/api/identity/organizations/${id}`,
    method: 'put',
    data: payload
  })
}

export function deleteOrganization(id) {
  return request({
    url: `/api/identity/organizations/${id}`,
    method: 'delete'
  })
}

// 查询组织机构列表
export function getOrganizations(query) {
  return request({
    url: `/api/identity/organizations`,
    method: 'get',
    params: transformAbpListQuery(query)
  })
}

/**
 * 
 * @param {object} query 
 * @returns 
 */
export function getOrganizationsAllTree(query) {
  return request({
    url: `/api/identity/organizations/all-tree`,
    method: 'get',
    params: transformAbpListQuery(query)
  })
}


/**
 * 获取组织根节点
 * @returns 
 */
export function getOrganizationsRoot() {
  return request({
    url: `/api/identity/organizations/root`,
    method: 'get'
  })
}

export function organizationFindChildren(query) {
  return request({
    url: `/api/identity/organizations/find-children`,
    method: 'get',
    params: query
  })
}

export function getOrganizationLastChildren(id) {
  return request({
    url: `/api/identity/organizations/${id}/last-child`,
    method: 'get',
    params: query
  })
}

export function getOrganizationDetailTree(id) {
  return request({
    url: `/api/identity/organizations/${id}/detail-tree`,
    method: 'get',
    params: query
  })
}









export function getOrganizationChildren(pid) {
  return request({
    url: `/api/identity/organizations/children/${pid}`,
    method: 'get'
    
  })
}


export function getOrganizationsAllWithDetails(query) {
  return request({
    url: `/api/identity/organizations/all/details`,
    method: 'get',
    params: query
  })
}

/**
 * 获取组织机构
 * Example: ?filter&sorting&skipCount=0&maxResultCount=20
 * @param {object} query
 */
export function getOrganizationsWithDetails(query) {
  return request({
    url: `/api/identity/organizations/details`,
    method: 'get',
    params: transformAbpListQuery(query)
  })
}



export function getOrganizationSingleWithDetails(id) {
  return request({
    url: `/api/identity/organizations/${id}/details`,
    method: 'get'
  })
}

export function getOrganizationSingle(id) {
  return request({
    url: `/api/identity/organizations/${id}`,
    method: 'get'
  })
}












// 组织关联角色

export function getOrgRoles(query) {
  return request({
    url: `/api/identity/organizations/roles`,
    method: 'get',
    params: transformAbpListQuery(query)
  })
}

export function getUnaddedRoles(id, query) {
  return request({
    url: `/api/identity/organizations/${id}get-unadded-roles`,
    method: 'get',
    params: transformAbpListQuery(query)
  })
}

export function getRoleNames(id, query) {
  return request({
    url: `/api/identity/organizations/${id}/role-names`,
    method: 'get',
    params: transformAbpListQuery(query)
  })
}

export function AddRoles(id, payload) {
  return request({
    url: `/api/identity/organizations/${id}/add-roles`,
    method: 'post',
    data: payload
  })
}

export function removeRole(id) {
  return request({
    url: `/api/identity/organizations/${id}/remove-role`,
    method: 'delete'
  })
}



// 组织关联的用户

export function getOrgUsers(query) {
  return request({
    url: `/api/identity/organizations/users`,
    method: 'get',
    params: transformAbpListQuery(query)
  })
}

export function getUnAddedUsers(id, query) {
  return request({
    url: `/api/identity/organizations/${id}/users`,
    method: 'get',
    params: transformAbpListQuery(query)
  })
}

export function AddUsers(id, payload) {
  return request({
    url: `/api/identity/organizations/${id}/add-users`,
    method: 'post',
    data: payload
  })
}

export function removeUser(id) {
  return request({
    url: `/api/identity/organizations/${id}/remove-user`,
    method: 'delete'
  })
}


