import router from './router'
import store from './store'
import { Message } from 'element-ui'
import NProgress from 'nprogress' // progress bar
import 'nprogress/nprogress.css' // progress bar style
import { getToken } from '@/utils/auth' // get token from cookie
import getPageTitle from '@/utils/get-page-title'
import constantRoutes from './router'


NProgress.configure({ showSpinner: false }) // NProgress Configuration

const whiteList = ['/login','/register','/reset_password'] // no redirect whitelist

router.beforeEach(async(to, from, next) => {
  // start progress bar
  NProgress.start()

  // set page title
  document.title = getPageTitle(to.meta.title)

  // determine whether the user has logged in
  const hasToken = getToken()

  let abpConfig = store.getters.abpConfig
  if (!abpConfig) {
    abpConfig = await store.dispatch('app/applicationConfiguration')
  }
  // store.dispatch('user/getInfo')

  if (abpConfig.currentUser.isAuthenticated) {
    if (to.path === '/login') {
      // if is logged in, redirect to the home page
      next({ path: '/' })
      NProgress.done()
    } else {
      const hasGetUserInfo = store.getters.name
      // console.log('hasGetUserInfo')
      // console.log(hasGetUserInfo)
      if (hasGetUserInfo) {
        next()
      } else {
        try {
          // return;
          // get user info
          await store.dispatch('user/getInfo')

          store.dispatch('user/setRoles', abpConfig.currentUser.roles)

          const grantedPolicies = abpConfig.auth.grantedPolicies
          // console.log('grantedPolicies',grantedPolicies)
          // generate accessible routes map based on grantedPolicies
          const accessRoutes = await store.dispatch(
            'permission/generateRoutes',
            grantedPolicies
          )

          // console.log('accessRoutes',accessRoutes)
          // dynamically add accessible routes

          // console.log('router_before',router)
          router.addRoutes(accessRoutes)

          // console.log('router_after',router)
          // hack method to ensure that addRoutes is complete
          // set the replace: true, so the navigation will not leave a history record
          next({ ...to, replace: true })
        } catch (error) {
          // remove token and go to login page to re-login
          await store.dispatch('user/resetToken')
          Message.error(error || 'Has Error')
          next(`/login?redirect=${to.path}`)
          NProgress.done()
        }
      }
    }
  } else {
    /* has no token*/
    // console.log(router)
    // return
    // constantRoutes.forEach(function (item) {
    //     if (item.path === to.path) {
    //       next()
    //     }
    //     console.log(item);
    // });
    if (whiteList.indexOf(to.path) !== -1) {
      // in the free login whitelist, go directly
      // console.log(to.path);
      // return;
      next()
    } else {
      if (to.path === '/login') {
        next()
      }
      // next({path: '/login'})
      // other pages that do not have permission to access are redirected to the login page.
      next(`/login?redirect=${to.path}`)
      NProgress.done()
    }
  }
})

router.afterEach(() => {
  // finish progress bar
  NProgress.done()
})
