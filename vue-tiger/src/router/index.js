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
    path: '/',
    name: '主页',
    meta: { title: '主页', icon: 'dashboard' },
    // 你可以选择不同的layout组件
    component: Layout,
    redirect: '/dashboard',
    // 这里开始对应的路由都会显示在app-main中 如上图所示
    children: [{
      path: 'dashboard',
      name: '首页',
      component: () => import('@/views/dashboard/index1'),
      meta: { title: '首页', icon: 'dashboard' }
    }

    ]
  },

  {
    path: '/basic',
    name: '产品',
    meta: { title: '基础资料', icon: 'el-icon-bangzhu' },
    // 你可以选择不同的layout组件
    component: Layout,
    redirect: '/dashboard',
    // 这里开始对应的路由都会显示在app-main中 如上图所示
    children: [
      {
        path: 'product/list',
        name: 'ProductList',
        component: () => import('@/views/basic/product/list'),
        meta: { title: '商品', icon: 'el-icon-discover' }
      },
      {
        path: 'product/create',
        component: () => import('@/views/basic/product/create'),
        name: 'CreateProduct',
        meta: { title: '添加产品', icon: 'theme', noCache: true, activeMenu: '/basic/product/list' },
        hidden: true
      },
      {
        path: 'product/edit/:id',
        component: () => import('@/views/basic/product/edit'),
        name: 'EditProduct',
        meta: { title: '编辑产品', noCache: true, activeMenu: '/basic/product/list' },
        hidden: true
      },
      {
        path: 'category/list',
        name: '分类',
        component: () => import('@/views/basic/category/list'),
        meta: { title: '分类', icon: 'form' }
      },
      {
        path: 'warehouse/list',
        name: 'Warehouse',
        component: () => import('@/views/basic/warehouse/list'),
        meta: { title: '仓库', icon: 'eye-open' }
      },
      {
        path: 'supply/list',
        name: 'Supply',
        component: () => import('@/views/basic/supply/list'),
        meta: { title: '供应商', icon: 'bug' }
      },
      {
        path: 'store/list',
        name: 'Store',
        component: () => import('@/views/basic/store/list'),
        meta: { title: '店铺', icon: 'search' }
      },
      {
        path: 'product-attribute-type/list',
        name: '规格',
        component: () => import('@/views/basic/product-attribute-type/list'),
        meta: { title: '规格', icon: 'qq' },
        children: [
        ]
      },
      {
        path: '/basic/product-attribute/list',
        name: '规格属性',
        component: () => import('@/views/basic/product-attribute/list'),
        meta: { title: '规格属性', icon: 'search', activeMenu: '/basic/product-attribute-type/list' },
        hidden: true
      }

      // {
      //   path: 'comment/list',
      //   name: '产品评论',
      //   component: () => import('@/views/basic/comment/list'),
      //   meta: { title: '评论', icon: 'eye-open' }
      // }
    ]
  },
  {
    path: '/purchase',
    name: 'Purchase',
    meta: { title: '采购', icon: 'message' },
    // 你可以选择不同的layout组件
    component: Layout,
    redirect: '/dashboard',
    // 这里开始对应的路由都会显示在app-main中 如上图所示
    children: [
      {
        path: '/list',
        name: 'PurchaseList',
        component: () => import('@/views/purchase/list'),
        meta: { title: '进货单', icon: '404' }
      },
      {
        path: 'purchase-order-return-apply/list',
        component: () => import('@/views/order/order-return-apply/list'),
        name: 'PurchaseOrderReturnApply',
        meta: { title: '采购退货', icon: 'bug' }
      }

      // {
      //   path: 'order-setting/list',
      //   name: 'OrderSetting',
      //   component: () => import('@/views/order/order-setting/list'),
      //   meta: { title: '订单设置', icon: 'form' }
      // }

    ]
  },
  {
    path: '/order',
    name: 'order',
    meta: { title: '订单', icon: 'el-icon-bangzhu' },
    // 你可以选择不同的layout组件
    component: Layout,
    redirect: '/dashboard',
    // 这里开始对应的路由都会显示在app-main中 如上图所示
    children: [
      {
        path: 'order/list',
        name: 'OrderList',
        component: () => import('@/views/order/list'),
        meta: { title: '订单列表', icon: 'el-icon-discover' }
      },
      {
        path: 'order-return-apply/list',
        component: () => import('@/views/order/order-return-apply/list'),
        name: 'OrderReturnApply',
        meta: { title: '退款', icon: 'theme' }
      },

      {
        path: 'order-setting/list',
        name: 'OrderSetting',
        component: () => import('@/views/order/order-setting/list'),
        meta: { title: '订单设置', icon: 'form' }
      }

    ]
  },
  {
    path: '/stock',
    name: 'Stock',
    meta: { title: '库存', icon: 'international' },
    // 你可以选择不同的layout组件
    component: Layout,
    redirect: '/dashboard',
    // 这里开始对应的路由都会显示在app-main中 如上图所示
    children: [
      {
        path: 'inventory/list',
        name: 'InventoryList',
        component: () => import('@/views/stock/inventory/list'),
        meta: { title: '库存量', icon: 'table' }
      },
      {
        path: 'receipt/list',
        component: () => import('@/views/stock/receipt/list'),
        name: 'ReceiptList',
        meta: { title: '入库单', icon: 'international' }
      },
      {
        path: 'out-of-stock/list',
        component: () => import('@/views/stock/out-of-stock/list'),
        name: 'out-of-stockList',
        meta: { title: '出库单', icon: 'lock' }
      },
      {
        path: 'check/list',
        component: () => import('@/views/stock/check/list'),
        name: 'CheckList',
        meta: { title: '盘点单', icon: 'message' }
      },
      {
        path: 'transfer/list',
        component: () => import('@/views/stock/transfer/list'),
        name: 'TransferList',
        meta: { title: '调拨单', icon: 'money' }
      },
      {
        path: 'reverse/list',
        component: () => import('@/views/stock/reverse/list'),
        name: 'ReverseList',
        meta: { title: '拆套单', icon: 'skill' }
      }

      // {
      //   path: 'order-setting/list',
      //   name: 'OrderSetting',
      //   component: () => import('@/views/order/order-setting/list'),
      //   meta: { title: '订单设置', icon: 'form' }
      // }

    ]
  },
  {
    path: '/member',
    name: 'Member',
    meta: { title: '会员', icon: 'size' },
    // 你可以选择不同的layout组件
    component: Layout,
    redirect: '/dashboard',
    // 这里开始对应的路由都会显示在app-main中 如上图所示
    children: [
      {
        path: 'member/list',
        name: 'MemberList',
        component: () => import('@/views/member/list'),
        meta: { title: '会员列表', icon: 'skill' }
      },
      {
        path: 'member-level/list',
        component: () => import('@/views/member/member-level/list'),
        name: 'CouponHistory',
        meta: { title: '会员等级', icon: 'star' }
      },
      {
        path: 'member-statistic/list',
        component: () => import('@/views/member/member-statistic/list'),
        name: 'CouponHistory',
        meta: { title: '会员统计', icon: 'tab' }
      }

      // {
      //   path: 'order-setting/list',
      //   name: 'OrderSetting',
      //   component: () => import('@/views/order/order-setting/list'),
      //   meta: { title: '订单设置', icon: 'form' }
      // }
      // {
      //   path: 'attribute/list',
      //   name: '产品规格',
      //   component: () => import('@/views/product/attribute/list'),
      //   meta: { title: '规格', icon: 'eye' }
      // },
      // {
      //   path: 'comment/list',
      //   name: '产品评论',
      //   component: () => import('@/views/product/comment/list'),
      //   meta: { title: '评论', icon: 'eye-open' }
      // }
    ]
  },
  {
    path: '/marketing',
    name: 'marketing',
    meta: { title: '营销', icon: 'email' },
    // 你可以选择不同的layout组件
    component: Layout,
    redirect: '/dashboard',
    // 这里开始对应的路由都会显示在app-main中 如上图所示
    children: [
      {
        path: 'coupon/list',
        name: 'CouponList',
        component: () => import('@/views/marketing/coupon/list'),
        meta: { title: '优惠券', icon: 'education' }
      },
      {
        path: 'coupon-history/list',
        component: () => import('@/views/marketing/coupon-history/list'),
        name: 'CouponHistory',
        meta: { title: '会员领取记录', icon: 'shopping' }
      }

      // {
      //   path: 'order-setting/list',
      //   name: 'OrderSetting',
      //   component: () => import('@/views/order/order-setting/list'),
      //   meta: { title: '订单设置', icon: 'form' }
      // }
      // {
      //   path: 'attribute/list',
      //   name: '产品规格',
      //   component: () => import('@/views/product/attribute/list'),
      //   meta: { title: '规格', icon: 'eye' }
      // },
      // {
      //   path: 'comment/list',
      //   name: '产品评论',
      //   component: () => import('@/views/product/comment/list'),
      //   meta: { title: '评论', icon: 'eye-open' }
      // }
    ]
  },

  // // 图表
  // {
  //   path: '/chart',
  //   name: '图表',
  //   meta: { title: '图表', icon: 'dashboard' },
  //   // 你可以选择不同的layout组件
  //   component: Layout,
  //   // 这里开始对应的路由都会显示在app-main中 如上图所示
  //   children: [
  //     {
  //       path: '/chart/line',
  //       name: '简单示例',
  //       component: () => import('@/views/charts/line'),
  //       meta: { title: '简单示例', icon: 'dashboard' },
  //       hidden: false
  //     },
  //     {
  //       path: '/chart/keybord',
  //       name: '键盘图',
  //       component: () => import('@/views/charts/keyboard'),
  //       meta: { title: '折线图', icon: 'dashboard' },
  //       hidden: false
  //     },
  //     {
  //       path: '/chart/mix-chart',
  //       name: '混合图',
  //       component: () => import('@/views/charts/mix-chart'),
  //       meta: { title: '混合图', icon: 'dashboard' },
  //       hidden: false
  //     }
  //   ]
  // },
  {
    path: '/profile',
    // 你可以选择不同的layout组件
    component: Layout,
    // 这里开始对应的路由都会显示在app-main中 如上图所示
    children: [
      {
        path: '/profile/index',
        name: '个人中心',
        component: () => import('@/views/profile/index'),
        meta: { title: '个人中心', icon: 'dashboard' },
        hidden: true
      }
    ]
  }

  // // 例子
  // {
  //   path: '/example',
  //   component: Layout,
  //   redirect: '/example/table',
  //   name: '例子',
  //   meta: { title: '例子', icon: 'el-icon-s-help' },
  //   children: [
  //     {
  //       path: 'table',
  //       name: 'Table1',
  //       component: () => import('@/views/table/index'),
  //       meta: { title: '表格', icon: 'table' }
  //     },
  //     {
  //       path: 'tree',
  //       name: 'Tree',
  //       component: () => import('@/views/tree/index'),
  //       meta: { title: '树结构', icon: 'tree' }
  //     }
  //   ]
  // },

  // // 表单
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
  // }

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
