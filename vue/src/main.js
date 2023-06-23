import Vue from 'vue'
import Cookies from 'js-cookie'

import 'normalize.css/normalize.css' // A modern alternative to CSS resets

import ElementUI from 'element-ui'
import 'element-ui/lib/theme-chalk/index.css'
// import locale from 'element-ui/lib/locale/lang/en' // lang i18n
import i18n from './lang' // internationalization

import '@/styles/index.scss' // global css

import App from './App'
import store from './store'
import router from './router'

import './utils/filter' // 注入全局的过滤器

import '@/icons' // icon
import '@/permission' // permission control

import * as filters from './filters' // global filters

/**
 * If you don't want to use mock-server
 * you want to use mockjs for request interception
 * you can execute:
 *
 * import { mockXHR } from '../mock'
 * mockXHR()
 */

// mock-server只会在开发环境中使用，线上生产环境目前使用MockJs进行模拟。如果不需要请移除。具体代码：main.js
import { mockXHR } from '../mock'
if (process.env.NODE_ENV === 'production') {
  mockXHR()
}

// set ElementUI lang to EN
// Vue.use(ElementUI, { locale })
// 如果想要中文版 element-ui，按如下方式声明

Vue.use(ElementUI, {
  size: Cookies.get('size') || 'mini', // set element-ui default size
  i18n: (key, value) => i18n.t(key, value)
})

// Vue.use(
//   ElementUI, { size: 'mini', zIndex: 3000 }
//   )

// register global utility filters
Object.keys(filters).forEach(key => {
  Vue.filter(key, filters[key])
})

Vue.config.productionTip = false

new Vue({
  el: '#app',
  router,
  store,
  i18n, // 在main.js 中注入i18n 插件
  render: h => h(App)
})
