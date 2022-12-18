import Cookies from 'js-cookie'
import { getLanguage, setLocale } from '@/lang/index'
import { getApplicationConfiguration } from '@/api/user'

const state = {
  sidebar: {
    opened: Cookies.get('sidebarStatus') ? !!+Cookies.get('sidebarStatus') : true,
    withoutAnimation: false
  },
  device: 'desktop',
  size: Cookies.get('size') || 'mini',
  language: getLanguage(),
  abpConfig: null  // TODO: 添加读取这个配置会无法登录
}

const mutations = {
  TOGGLE_SIDEBAR: state => {
    state.sidebar.opened = !state.sidebar.opened
    state.sidebar.withoutAnimation = false
    if (state.sidebar.opened) {
      Cookies.set('sidebarStatus', 1)
    } else {
      Cookies.set('sidebarStatus', 0)
    }
  },
  CLOSE_SIDEBAR: (state, withoutAnimation) => {
    Cookies.set('sidebarStatus', 0)
    state.sidebar.opened = false
    state.sidebar.withoutAnimation = withoutAnimation
  },
  TOGGLE_DEVICE: (state, device) => {
    state.device = device
  },
  SET_SIZE: (state, size) => {
    state.size = size
    Cookies.set('size', size)
  },
  SET_LANGUAGE: (state, language) => {
    state.language = language
    Cookies.set('language', language)
  },
  SET_ABPCONFIG: (state, abpConfig) => {
    state.abpConfig = abpConfig
  }
}

const actions = {
  toggleSideBar({ commit }) {
    commit('TOGGLE_SIDEBAR')
  },
  closeSideBar({ commit }, { withoutAnimation }) {
    commit('CLOSE_SIDEBAR', withoutAnimation)
  },
  toggleDevice({ commit }, device) {
    commit('TOGGLE_DEVICE', device)
  },
  setSize({ commit }, size) {
    commit('SET_SIZE', size)
  },
  setLanguage({ commit }, language) {
    commit('SET_LANGUAGE', language)
  },
  applicationConfiguration({ commit }) {
    return new Promise((resolve, reject) => {
      getApplicationConfiguration()
        .then(response => {
          commit('SET_ABPCONFIG', response)
          const language = response.localization.currentCulture.cultureName
          const values = response.localization.values
          setLocale(language, values)
          
          resolve(response)
        })
        .catch(error => {
          reject(error)
        })
    })
  },
}

export default {
  namespaced: true,
  state,
  mutations,
  actions
}
