import Vue from 'vue'
import Router from 'vue-router'

/* Layout */
import Layout from '@/layout'

/* Router Modules */
import SystemManageRouter from './modules/system'
import SassRouter from './modules/sass'
// import componentsRouter from './modules/components'
// import chartsRouter from './modules/charts'
import tableRouter from './modules/table'
// import nestedRouter from './modules/nested'

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
  // 了解路由当中有哪些配置项目
  // 当设置 true 的时候该路由不会在侧边栏出现 如401，login等页面，或者如一些编辑页面/edit/1
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
    component: () => import('@/views/login/index'),
    hidden: true
  },
  {
    path: '/register',
    component: () => import('@/views/account/register/index'),
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
        component: () => import('@/views/profile/index'),
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
        meta: { title: '首页', icon: 'dashboard', affix: true }
      }
    ]
  },
  {
    path: '/',
    component: Layout,
    redirect: '/dashboard',
    // 这里开始对应的路由都会显示在app-main中 如上图所示
    children: [
      {
        path: '/documentation',
        component: () => import('@/views/documentation/index'),
        name: 'Documentation',
        meta: { title: '文档', icon: 'documentation', affix: true }
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
  SassRouter,
  SystemManageRouter,
  // tableRouter,
  // {
  //   path: '/test',
  //   name: '测试',
  //   meta: { title: '测试', icon: 'el-icon-coffee', policy: 'SettingUi.ShowSettingPage' },
  //   component: Layout,
  //   redirect: '/vue2',
  //   // 这里开始对应的路由都会显示在app-main中 如上图所示
  //   children: [
  //     {
  //       path: 'vue2',
  //       name: 'vue2',
  //       component: () => import('@/views/vue2.0/index'),
  //       meta: { title: 'vue2' }
  //     },
  //     {
  //       path: 'vue2/componet',
  //       name: 'vue2-componet',
  //       component: () => import('@/views/vue2.0/component'),
  //       meta: { title: 'vue2组件' }
  //     },
  //     {
  //       path: 'vue2/element-ui',
  //       name: 'element-ui',
  //       component: () => import('@/views/vue2.0/element-ui'),
  //       meta: { title: 'element-ui' }
  //     },
  //     {
  //       path: 'table1',
  //       name: 'table1',
  //       component: () => import('@/views/table/index'),
  //       meta: { title: 'table1' }
  //     }
  //   ]
  // },
  // {
  //   path: '/example',
  //   component: Layout,
  //   redirect: '/example/list',
  //   name: 'Example',
  //   meta: {
  //     title: 'Example',
  //     icon: 'el-icon-s-help'
  //   },
  //   children: [
  //     {
  //       path: 'create',
  //       component: () => import('@/views/example/create'),
  //       name: 'CreateArticle',
  //       meta: { title: 'Create Article', icon: 'edit' }
  //     },
  //     {
  //       path: 'edit/:id(\\d+)',
  //       component: () => import('@/views/example/edit'),
  //       name: 'EditArticle',
  //       meta: { title: 'Edit Article', noCache: true, activeMenu: '/example/list' },
  //       hidden: true
  //     },
  //     {
  //       path: 'list',
  //       component: () => import('@/views/example/list'),
  //       name: 'ArticleList',
  //       meta: { title: 'Article List', icon: 'list' }
  //     }
  //   ]
  // },
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
