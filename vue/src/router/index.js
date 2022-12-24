import Vue from 'vue'
import Router from 'vue-router'

/* Layout */
import Layout from '@/layout'

/* Router Modules */
import SystemManageRouter from './modules/system-manage'
import tenantRouter from './modules/tenant'

Vue.use(Router)
/**
 * Note: sub-menu only appear when route children.length >= 1
 * Detail see: https://panjiachen.github.io/vue-element-admin-site/guide/essentials/router-and-nav.html
 *
 * hidden: true                   if set true, item will not show in the sidebar(default is false)
 * alwaysShow: true               if set true, will always show the root menu
 *                                if not set alwaysShow, when item has more than one children route,
 *                                it will becomes nested mode, otherwise not show the root menu
 * redirect: noRedirect           if set noRedirect will no redirect in the breadcrumb
 * name:'router-name'             the name is used by <keep-alive> (must set!!!)
 * meta : {
    roles: ['admin','editor']    control the page roles (you can set multiple roles)
    title: 'title'               the name show in sidebar and breadcrumb (recommend set)
    icon: 'svg-name'/'el-icon-x' the icon show in the sidebar
    breadcrumb: false            if set false, the item will hidden in breadcrumb(default is true)
    activeMenu: '/example/list'  if set path, the sidebar will highlight the path you set
  }
 */

/**
 * constantRoutes
 * a base page that does not have permission requirements
 * all roles can be accessed
 */
export const constantRoutes = [
  {
    path: '/redirect',
    component: Layout,
    hidden: true,
    children: [
      {
        path: '/redirect/:path(.*)',
        component: () => import('@/views/redirect/index')
      }
    ]
  },
  {
    path: '/login',
    component: () => import('@/views/login/index'),
    hidden: true
  },
  {
    path: '/register',
    component: () => import('@/views/account/register/index'),
    hidden: true
  },
  {
    path: '/reset_password',
    component: () => import('@/views/account/reset_password/index'),
    hidden: true
  },
  {
    path: '/404',
    component: () => import('@/views/404'),
    hidden: true
  },

  {
    path: '/profile',
    component: Layout,
    redirect: '/profile/index',
    hidden: true,
    children: [
      {
        path: 'index',
        component: () => import('@/views/profile/index'),
        name: 'Profile',
        meta: { title: '我的账户', icon: 'user', noCache: true }
      }
    ]
  },

  {
    path: '/',
    name: '主页',
    meta: { title: '主页', icon: 'el-icon-s-home' },
    // 你可以选择不同的layout组件
    component: Layout,
    redirect: '/dashboard',
    // 这里开始对应的路由都会显示在app-main中 如上图所示
    children: [
      {
        path: '/documentation',
        component: () => import('@/views/documentation/index'),
        name: 'Documentation',
        meta: { title: '文档', icon: 'documentation', affix: true },
      },
      {
        path: 'dashboard',
        name: '仪表盘',
        component: () => import('@/views/dashboard/index'),
        meta: { title: '仪表盘', icon: 'dashboard', affix: true }
      }
    ]
  },
  {
    path: '/test',
    name: '测试',
    meta: { title: '测试', icon: 'el-icon-coffee' },
    component: Layout,
    redirect: '/vue2',
    // 这里开始对应的路由都会显示在app-main中 如上图所示
    children: [
      {
        path: 'vue2',
        name: 'vue2',
        component: () => import('@/views/vue2.0/index'),
        meta: { title: 'vue2' }
      },
      {
        path: 'vue2/componet',
        name: 'vue2-componet',
        component: () => import('@/views/vue2.0/component'),
        meta: { title: 'vue2组件' }
      },
      {
        path: 'vue2/element-ui',
        name: 'element-ui',
        component: () => import('@/views/vue2.0/element-ui'),
        meta: { title: 'element-ui' }
      }
    ]
  }
  
]

/**
 * asyncRoutes
 * the routes that need to be dynamically loaded based on user roles
 */
export const asyncRoutes = [
  /** when your routing map is too long, you can split it into small modules **/
  // identityRouter,
  // dictionaryRouter,
  // auditlogRouter,
  tenantRouter,
  SystemManageRouter,
  // 404 page must be placed at the end !!!
  { path: '*', redirect: '/404', hidden: true }
]

const createRouter = () => new Router({
  // mode: 'history', // require service support
  scrollBehavior: () => ({ y: 0 }),
  routes: constantRoutes
})

const router = createRouter()

// Detail see: https://github.com/vuejs/vue-router/issues/1234#issuecomment-357941465
export function resetRouter() {
  const newRouter = createRouter()
  router.matcher = newRouter.matcher // reset router
}

export default router
