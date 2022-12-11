

// tenant
export function getTenantList(params) {
    return request({
      url: '/api/multi-tenancy/tenants',
      method: 'get',
      params: transformAbpListQuery(params)
    })
  }
  
  export function getTenant(id) {
    return request({
      url: `/api/multi-tenancy/tenants/${id}`,
      method: 'get'
    })
  }
  
  export function createTenant(payload) {
    return request({
      url: '/api/multi-tenancy/tenants',
      method: 'post',
      data: payload
    })
  }
  
  export function updateTenant(id, payload) {
    return request({
      url: `/api/multi-tenancy/tenants/${id}`,
      method: 'put',
      data: payload
    })
  }
  
  export function deleteTenant(id) {
    return request({
      url: `/api/multi-tenancy/tenants/${id}`,
      method: 'delete'
    })
  }
  