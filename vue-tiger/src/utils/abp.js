import store from '@/store'
import router, { resetRouter } from '@/router'

var photoPrefix = ''
var baseUrl = ''
if (process.env.NODE_ENV === 'production') {
  photoPrefix = 'http://tiger.hongjy.cn/'
  baseUrl = 'https://localhost:44306/'
} else {
  photoPrefix = 'https://blogimg.hongjy.cn/'
  baseUrl = 'https://localhost:44306/'
}

export const Url = {
  photoPrefix: photoPrefix,
  filePrefix: '',
  downloadPrefix: '',
  baseUrl: baseUrl
}

const baseListQuery = {
  page: 1,
  limit: 10,
  sort: undefined,
  filter: undefined
}

export const httpCode = [
  {
    label: 200,
    value: 200
  },
  {
    label: 401,
    value: 401
  },
  {
    label: 403,
    value: 403
  },
  {
    label: 500,
    value: 500
  }
]

// 转换分页参数为abp需要的参数
export function transformAbpListQuery(query) {
  query.filter = query.filter === '' ? undefined : query.filter

  const abpListQuery = {
    maxResultCount: query.limit,
    skipCount: (query.page - 1) * query.limit,
    sorting:
      query.sort && query.sort.endsWith('ending')
        ? query.sort.replace('ending', '')
        : query.sort,
    ...query
  }

  delete abpListQuery.page
  delete abpListQuery.limit
  delete abpListQuery.sort

  return abpListQuery
}

function shouldFetchAppConfig(providerKey, providerName) {
  const currentUser = store.getters.abpConfig.currentUser

  if (providerName === 'R') {
    return currentUser.roles.some(role => role === providerKey)
  }

  if (providerName === 'U') return currentUser.id === providerKey

  return false
}

export function fetchAppConfig(providerKey, providerName) {
  if (shouldFetchAppConfig(providerKey, providerName)) {
    store.dispatch('app/applicationConfiguration').then(abpConfig => {
      resetRouter()

      store.dispatch('user/setRoles', abpConfig.currentUser.roles)

      const grantedPolicies = abpConfig.auth.grantedPolicies

      // generate accessible routes map based on grantedPolicies
      store
        .dispatch('permission/generateRoutes', grantedPolicies)
        .then(accessRoutes => {
          // dynamically add accessible routes
          router.addRoutes(accessRoutes)
        })

      // reset visited views and cached views
      // store.dispatch("tagsView/delAllViews", null, { root: true });
    })
  }
}

export function checkPermission(policy) {
  const abpConfig = store.getters.abpConfig
  return true
  if (abpConfig.auth.grantedPolicies[policy]) {
    return true
  } else {
    return false
  }
}

export function getFilePathByName(name) {
  const reg = /http(s)?:\/\/([\w-]+\.)+[\w-]+(\/[\w- .\/?%&=]*)?/

  if (reg.test(name)) {
    return name
  }

  return process.env.VUE_APP_BASE_API + '/api/file-management/' + name
}

export default baseListQuery
