import axios from 'axios'
import { MessageBox, Message } from 'element-ui'
import store from '@/store'
// import { getToken } from '@/utils/auth'
import { param as encodeParam } from '@/utils'

//axios官网 https://axios-http.com/zh/

var timeout = 5000
if (process.env.NODE_ENV === 'production') {
  timeout = 5000 // request timeout
} else {
  timeout = 50000 // 开发环境增加调试请求时间
}

// create an axios instance
const service = axios.create({
  baseURL: process.env.VUE_APP_BASE_API_LOCAL, // url = base url + request url
  // withCredentials: true, // send cookies when cross-domain requests
  timeout: timeout // request timeout
})



// request interceptor 请求拦截器
service.interceptors.request.use(
  config => {
    // do something before request is sent
    // config.headers['accept-language'] = store.getters.language
    if (store.getters.token) {
      // let each request carry token
      // ['X-Token'] is a custom headers key
      // please modify it according to the actual situation
      // config.headers['X-Token'] = getToken()
      config.headers['authorization'] = 'Bearer ' + store.getters.token
    }
    config.headers['accept-language'] = 'zh-Hans'
    // config.paramsSerializer = function(params) {
    //   return encodeParam(params)
    // }
    // console.log('requestjs-config:' + config)
    // console.log(config)
    return config
  },
  error => {
    // do something with request error
    // console.log('request-err')
    console.log(error) // for debug
    return Promise.reject(error)
  }
)

// response interceptor
service.interceptors.response.use(
  /**
   * If you want to get http information such as headers or status
   * Please return  response => response
  */

  /**
   * Determine the request status by custom code
   * Here is just an example
   * You can also judge the status by HTTP Status Code
   */
  response => {
    const res = response.data
    return res
    
  },
  error => {
    console.log("error", error) // for debug
    // debugger

    if (error.status === 401) {
      // to re-login
      MessageBox.confirm(
        'You have been logged out, you can cancel to stay on this page, or log in again',
        'Confirm logout',
        {
          confirmButtonText: 'Re-Login',
          cancelButtonText: 'Cancel',
          type: 'warning'
        }
      ).then(() => {
        store.dispatch('user/resetToken').then(() => {
          location.reload()
        })
      })
    }

    let message = ''
    if(error.response && error.response.data && error.response.data.error && error.response.data.error.validationErrors){
      error.response.data.error.validationErrors.forEach(element => {
        message += element.message + '\n'
      });
    }
    else if(error.response && error.response.data && error.response.data.error) {
      message = error.response.data.error.message //+ '<br>' + error.response.data.error.details
    } else {
      message = error.message
    }

    Message({
      message: message,
      type: 'error',
      duration: 5 * 1000
    })

    return Promise.reject(error)
    
  }
)

export default service
