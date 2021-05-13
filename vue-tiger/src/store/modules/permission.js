import { asyncRoutes, constantRoutes } from '@/router'

/**
 * Use meta.policy to determine if the current user has permission
 * 你可以在后台通过一个 tree 控件或者其它展现形式给每一个页面动态配置权限
 * @param grantedPolicies
 * @param route
 */
function hasPermission(grantedPolicies, route) {
  if (route.meta && route.meta.policy) {
    const policy = route.meta.policy
    return grantedPolicies[policy]
  } else {
    return true
  }
}

/**
 * Filter asynchronous routing tables by recursion
 * @param routes asyncRoutes
 * @param grantedPolicies
 */
export function filterAsyncRoutes(routes, grantedPolicies) {
  const res = []

  routes.forEach(route => {
    const tmp = { ...route }
    if (hasPermission(grantedPolicies, tmp)) {
      if (tmp.children) {
        tmp.children = filterAsyncRoutes(tmp.children, grantedPolicies)
      }

      if (route.meta && route.meta.policy === '') {
        if (tmp.children.length > 0) {
          res.push(tmp)
        }
      } else {
        res.push(tmp)
      }
    }
  })

  return res
}

const state = {
  routes: [],
  addRoutes: []
}

const mutations = {
  SET_ROUTES: (state, routes) => {
    state.addRoutes = routes
    state.routes = constantRoutes.concat(routes)
  }
}

const actions = {
  generateRoutes({ commit }, grantedPolicies) {
    return new Promise(resolve => {
      console.log('grantedPolicies',grantedPolicies)
      const accessedRoutes = filterAsyncRoutes(asyncRoutes, grantedPolicies)
      console.log('accessedRoutes',accessedRoutes)
      commit('SET_ROUTES', accessedRoutes)
      resolve(accessedRoutes)
    })
  }
}

export default {
  namespaced: true,
  state,
  mutations,
  actions
}
