import { login, logout, getInfo } from '@/api/user'
import { getToken, setToken, removeToken } from '@/utils/auth'
import { resetRouter } from '@/router'

const clientSetting = {
  grant_type: 'password',
  scope: 'TigerAdmin',
  username: '',
  password: '',
  client_id: 'TigerAdmin_App',
  client_secret: '1q2w3e*'
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
    state.avatar = avatar
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
        // console.log(response)
        // console.log(response.access_token)
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
        // commit('SET_USERNAME', userName)
        // commit('SET_TEL', phoneNumber)
        // commit('SET_EMAIL', email)
        // commit('SET_INTRODUCTION', extraProperties.Introduction)
        resolve(response)
      }).catch(error => {
        reject(error)
      })
    })
  },
  setRoles({ commit }, roles) {
    commit('SET_ROLES', roles)
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

