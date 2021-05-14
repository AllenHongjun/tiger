import Vue from 'vue'
import Router from 'vue-router'

Vue.use(Router)

/* Layout */
import Layout from '@/layout'

/* Router Modules */
import settingRouter from './modules/setting'

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
    path: '/login',
    component: () => import('@/views/login/index'),
    hidden: true
  },

  {
    path: '/404',
    component: () => import('@/views/404'),
    hidden: true
  },

  {
    path: '/',
    // 你可以选择不同的layout组件
    component: Layout,
    redirect: '/dashboard',
    // 这里开始对应的路由都会显示在app-main中 如上图所示
    children: [{
      path: 'dashboard',
      name: '主页1',
      component: () => import('@/views/dashboard/index'),
      meta: { title: '主页', icon: 'dashboard' }
    }]
  },

  {
    path: '/profile',
    // 你可以选择不同的layout组件
    component: Layout,
    // 这里开始对应的路由都会显示在app-main中 如上图所示
    children: [{
      path: '/profile/index',
      name: '个人中心',
      component: () => import('@/views/profile/index'),
      meta: { title: '个人中心', icon: 'dashboard' },
      hidden:true
    }]
  },

  // {
  //   path: '/example',
  //   component: Layout,
  //   redirect: '/example/table',
  //   name: 'Example',
  //   meta: { title: 'Example', icon: 'el-icon-s-help' },
  //   children: [
  //     {
  //       path: 'table',
  //       name: 'Table1',
  //       component: () => import('@/views/table/index'),
  //       meta: { title: 'Table', icon: 'table' }
  //     },
  //     {
  //       path: 'tree',
  //       name: 'Tree',
  //       component: () => import('@/views/tree/index'),
  //       meta: { title: '树结构', icon: 'tree' }
  //     }
  //   ]
  // },

  // {
  //   path: '/form',
  //   component: Layout,
  //   children: [
  //     {
  //       path: 'index',
  //       name: 'Form',
  //       component: () => import('@/views/form/index'),
  //       meta: { title: '表单', icon: 'form' }
  //     }
  //   ]
  // },

  // {
  //   path: '/blank',
  //   component: Layout,
  //   name: 'Blank',
  //   meta: {
  //     title: '空白',
  //     icon: 'nested'
  //   },
  //   children: [
  //     {
  //       path: 'Blank2',
  //       name: 'Blank2',
  //       component: () => import('@/views/blank/index'),
  //       meta: { title: '空白页面', icon: 'form' }
  //     },
  //     {
  //       path: 'Blank3',
  //       name: 'Blank3',
  //       component: () => import('@/views/blank/index'),
  //       meta: { title: '空白页面2', icon: 'form' }
  //     }
  //   ]
  // },

  // {
  //   path: '/nested',
  //   component: Layout,
  //   redirect: '/nested/menu1',
  //   name: 'Nested',
  //   meta: {
  //     title: 'Nested',
  //     icon: 'nested'
  //   },
  //   children: [
  //     {
  //       path: 'menu1',
  //       component: () => import('@/views/nested/menu1/index'), // Parent router-view
  //       name: 'Menu1',
  //       meta: { title: 'Menu1' },
  //       children: [
  //         {
  //           path: 'menu1-1',
  //           component: () => import('@/views/nested/menu1/menu1-1'),
  //           name: 'Menu1-1',
  //           meta: { title: 'Menu1-1' }
  //         },
  //         {
  //           path: 'menu1-2',
  //           component: () => import('@/views/nested/menu1/menu1-2'),
  //           name: 'Menu1-2',
  //           meta: { title: 'Menu1-2' },
  //           children: [
  //             {
  //               path: 'menu1-2-1',
  //               component: () => import('@/views/nested/menu1/menu1-2/menu1-2-1'),
  //               name: 'Menu1-2-1',
  //               meta: { title: 'Menu1-2-1' }
  //             },
  //             {
  //               path: 'menu1-2-2',
  //               component: () => import('@/views/nested/menu1/menu1-2/menu1-2-2'),
  //               name: 'Menu1-2-2',
  //               meta: { title: 'Menu1-2-2' }
  //             }
  //           ]
  //         },
  //         {
  //           path: 'menu1-3',
  //           component: () => import('@/views/nested/menu1/menu1-3'),
  //           name: 'Menu1-3',
  //           meta: { title: 'Menu1-3' }
  //         }
  //       ]
  //     },
  //     {
  //       path: 'menu2',
  //       component: () => import('@/views/nested/menu2/index'),
  //       name: 'Menu2',
  //       meta: { title: 'menu2' }
  //     }
  //   ]
  // },
  // {
  //   path: '/table',
  //   component: Layout,
  //   name: 'Table',
  //   meta: {
  //     title: '图表',
  //     icon: 'pie-table'
  //   },
  //   children: [
  //     {
  //       path: 'simple_table',
  //       name: 'SimpleTable',
  //       meta: { title: '简单表格', icon: 'link' }
  //     },
  //     {
  //       path: 'complex_table',
  //       name: 'complex_table',
  //       meta: { title: '复杂表格', icon: 'link' }
  //     }
  //   ]
  // },
  // {
  //   path: '/form_layout',
  //   component: Layout,
  //   name: 'FormLayout',
  //   meta: {
  //     title: '表单',
  //     icon: 'form'
  //   },
  //   children: [
  //     {
  //       path: 'form_layout',
  //       name: 'FormLayout1',
  //       meta: { title: '单列表单', icon: 'link' }
  //     },
  //     {
  //       path: 'form_layout2',
  //       name: 'FormLayout2',
  //       meta: { title: '多列表单', icon: 'link' }
  //     },
  //     {
  //       path: 'file_upload',
  //       name: 'FileUpload',
  //       meta: { title: '文件上传', icon: 'link' }
  //     },
  //     {
  //       path: 'form_upload',
  //       name: 'FormUpload',
  //       meta: { title: '表单文件上传', icon: 'link' }
  //     },
  //     {
  //       path: 'form_editor',
  //       name: 'FormEditor',
  //       meta: { title: '表格编辑器', icon: 'link' }
  //     }

  //   ]
  // },
  // {
  //   path: '/stat',
  //   component: Layout,
  //   name: 'Stat',
  //   meta: {
  //     title: '图表统计',
  //     icon: 'table'
  //   },
  //   children: [
  //     {
  //       path: 'chart',
  //       name: 'Chart',
  //       meta: { title: '图表', icon: 'link' }
  //     },
  //     {
  //       path: 'flex',
  //       name: 'Flex',
  //       meta: { title: '数字排版', icon: 'link' }
  //     }
  //   ]
  // }

]

/**
 * asyncRoutes
 * the routes that need to be dynamically loaded based on user roles
 */
 export const asyncRoutes = [
  /** when your routing map is too long, you can split it into small modules **/
  // identityRouter,
  // tenantRouter,
  // dictionaryRouter,
  // auditlogRouter,
  settingRouter,
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
