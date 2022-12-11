import { login, logout, getInfo, setUserInfo } from '@/api/user'
import { getToken, setToken, removeToken } from '@/utils/auth'
import { resetRouter } from '@/router'

// IdentityServer客户端授权信息配置
const clientSetting = {
  grant_type: 'password',
  issuer: 'https://localhost:44367',
  redirectUri: process.env.VUE_APP_BASE_API,
  responseType: 'code',
  scope: 'Tiger',
  username: '',
  password: '',
  client_id: 'Tiger_App',  // 客户端id
  client_secret: '1q2w3e*'   // 客户端secert
}


const getDefaultState = () => {
  return {
    token: getToken(),
    name: '',
    userName: '',
    avatar: '',
    email: '',
    introduction: '',
    phoneNumber: '',
    roles: []
  }
}

const state = getDefaultState()

const mutations = {
  RESET_STATE: (state) => {
    Object.assign(state, getDefaultState())
  },
  SET_TOKEN: (state, token) => {
    state.token = token
  },
  SET_NAME: (state, name) => {
    state.name = name
  },
  SET_AVATAR: (state, avatar) => {
    if (!avatar) avatar = 'https://pic4.zhimg.com/80/v2-aed9b5ce797df38ae4298ee6cb81f47e_720w.jpg?source=1940ef5c'
    state.avatar = avatar
  },
  SET_USERNAME: (state, userName) => {
    state.userName = userName
  },
  SET_TEL: (state, phoneNumber) => {
    state.phoneNumber = phoneNumber
  },
  SET_EMAIL: (state, email) => {
    state.email = email
  },
  SET_INTRODUCTION: (state, Introduction) => {
    state.Introduction = Introduction
  },
  SET_ROLES: (state, roles) => {
    state.roles = roles
  },
  CLEAN: state => {
    state.token = ''
    state.name = ''
    state.userName = ''
    state.avatar = ''
    state.email = ''
    state.introduction = ''
    state.phoneNumber = ''
    state.roles = []
  }
}

const actions = {
  // user login
  login({ commit }, userInfo) {
    const { username, password } = userInfo
    clientSetting.username = username.trim()
    clientSetting.password = password
    return new Promise((resolve, reject) => {
      login(clientSetting).then(response => {
        commit('SET_TOKEN', response.access_token)
        setToken(response.access_token)
        resolve()
      }).catch(error => {
        reject(error)
      })
    })
  },

  // get user info
  getInfo({ commit }) {
    return new Promise((resolve, reject) => {
      getInfo().then(response => {
        if (!response) {
          return reject('Verification failed, please Login again.')
        }
        const { userName, name, phoneNumber, email, extraProperties } = response
        commit('SET_NAME', name)
        commit('SET_AVATAR', extraProperties.Avatar)
        commit('SET_USERNAME', userName)
        commit('SET_TEL', phoneNumber)
        commit('SET_EMAIL', email)
        commit('SET_INTRODUCTION', extraProperties.Introduction)
        resolve(response)
      }).catch(error => {
        reject(error)
      })
    })
  },
  setRoles({ commit }, roles) {
    commit('SET_ROLES', roles)
  },
  setUserInfo({ commit }, userInfo) {
    return new Promise((resolve, reject) => {
      setUserInfo(userInfo)
        .then(response => {
          const { userName, name, phoneNumber, email, extraProperties } = userInfo
          commit('SET_NAME', name)
          commit('SET_USERNAME', userName)
          commit('SET_TEL', phoneNumber)
          commit('SET_AVATAR', extraProperties.Avatar)
          commit('SET_EMAIL', email)
          commit('SET_INTRODUCTION', extraProperties.Introduction)
          resolve(response)
        })
        .catch(error => {
          reject(error)
        })
    })
  },
  // user logout
  logout({ commit, dispatch }) {
    return new Promise((resolve, reject) => {
      logout(state.token).then(() => {
        removeToken() // must remove  token  first
        resetRouter()
        commit('CLEAN')
        resolve()
      }).catch(error => {
        reject(error)
      })
    })
  },

  // remove token
  resetToken({ commit }) {
    return new Promise(resolve => {
      removeToken() // must remove  token  first
      commit('RESET_STATE')
      resolve()
    })
  },

  // get role list
  getRoleList({ commit }) {
    return new Promise((resolve, reject) => {
      getRoleList().then(response => {
        console.log(response)

        if (!response) {
          return reject('角色信息获取失败')
        }
        resolve(response)
      }).catch(error => {
        reject(error)
      })
    })
  }
}

export default {
  namespaced: true,
  state,
  mutations,
  actions
}

