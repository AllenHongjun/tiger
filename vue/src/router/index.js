import Vue from 'vue'
import Router from 'vue-router'

/* Layout */
import Layout from '@/layout'

/* Router Modules */
import examRouter from './modules/exam'
import QuestionBankRouter from './modules/question-bank'
import schoolManageRouter from './modules/school-manage'
import SystemManageRouter from './modules/system'
import SassRouter from './modules/sass'

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

  hidden: true // (默认 false)

  //当设置 noRedirect 的时候该路由在面包屑导航中不可被点击
  redirect: 'noRedirect'

  // 当你一个路由下面的 children 声明的路由大于1个时，自动会变成嵌套的模式--如组件页面
  // 只有一个时，会将那个子路由当做根路由显示在侧边栏--如引导页面
  // 若你想不管路由下面的 children 声明的个数都显示你的根路由
  // 你可以设置 alwaysShow: true，这样它就会忽略之前定义的规则，一直显示根路由
  alwaysShow: true

  name: 'router-name' // 设定路由的名字，一定要填写不然使用<keep-alive>时会出现各种问题
  meta: {
    roles: ['admin', 'editor'] // 设置该路由进入的权限，支持多个权限叠加
    title: 'title' // 设置该路由在侧边栏和面包屑中展示的名字
    icon: 'svg-name' // 设置该路由的图标，支持 svg-class，也支持 el-icon-x element-ui 的 icon
    noCache: true // 如果设置为true，则不会被 <keep-alive> 缓存(默认 false)
    breadcrumb: false //  如果设置为false，则不会在breadcrumb面包屑中显示(默认 true)
    affix: true // 如果设置为true，它则会固定在tags-view中(默认 false)

    // 当路由设置了该属性，则会高亮相对应的侧边栏。
    // 这在某些场景非常有用，比如：一个文章的列表页路由为：/article/list
    // 点击文章进入文章详情页，这时候路由为/article/1，但你想在侧边栏高亮文章列表的路由，就可以进行如下设置
    activeMenu: '/article/list'
 */

/**
 * constantRoutes
 * a base page that does not have permission requirements
 * 代表那些不需要动态判断权限的路由，如登录页、404、等通用页面。
 * all roles can be accessed
 *
 * 需要加不验证权限的菜单加入到 permission.js 的白名单中,不然会跳转
 */
export const constantRoutes = [
  {
    path: '/redirect',
    // 你可以选择不同的layout组件
    component: Layout,
    hidden: true,
    // 这里开始对应的路由都会显示在app-main中 如上图所示
    children: [
      {
        path: '/redirect/:path(.*)',
        component: () => import('@/views/redirect/index')
      }
    ]
  },
  {
    path: '/login',
    component: () => import('@/views/account/login/index'),
    hidden: true
  },
  {
    path: '/register',
    component: () => import('@/views/account/register/index'),
    hidden: true
  },
  {
    path: '/email-confirm',
    component: () => import('@/views/account/email-confirm/index'),
    hidden: true
  },
  {
    path: '/send-reset-password-link',
    component: () => import('@/views/account/reset-password/send-link'),
    hidden: true
  },
  {
    path: '/reset-password',
    component: () => import('@/views/account/reset-password/reset-password'),
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
        component: () => import('@/views/account/profile/index'),
        name: 'Profile',
        meta: { title: '我的账户', icon: 'user', noCache: true }
      }
    ]
  },
  {
    path: '/',
    component: Layout,
    redirect: '/dashboard',
    children: [
      {
        path: 'dashboard',
        component: () => import('@/views/dashboard/index'),
        name: 'dashboard',
        meta: { title: 'TigerUi["Menu:Dashboard"]', icon: 'dashboard', affix: true }
      }
    ]
  },
  {
    path: '/documentation',
    redirect: '/documentation',
    component: Layout,
    // 这里开始对应的路由都会显示在app-main中 如上图所示
    children: [
      {
        path: '/documentation',
        component: () => import('@/views/documentation/index'),
        name: 'Documentation',
        meta: { title: 'TigerUi["Menu:Documentation"]', icon: 'documentation', affix: true }
      }
    ]
  }
]

/**
 * asyncRoutes
 * the routes that need to be dynamically loaded based on user roles
 * 代表那些需求动态判断权限并通过 addRoutes 动态添加的页面。
 */
export const asyncRoutes = [
  /** when your routing map is too long, you can split it into small modules **/
  // identityRouter,
  // dictionaryRouter,
  // auditlogRouter,
  examRouter,
  QuestionBankRouter,
  schoolManageRouter,
  SassRouter,
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
