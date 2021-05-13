import Layout from '@/layout'

const settingRouter = {
  path: '/setting',
  component: Layout,
  // redirect: '/setting/system',
  // name: 'Setting',
  // hidden: true,
  meta: {
    title: '设置',
    icon: 'el-icon-setting'
  },
  children: [
    // {
    //   path: 'system',
    //   component: () => import('@/views/setting/system/index'), // Parent router-view
    //   name: 'system',
    //   meta: { title: '系统' }
    // },
    {
      path: 'audit_log/list',
      component: () => import('@/views/setting/audit_log/index'), // Parent router-view
      name: 'audit_log_list',
      meta: { title: '审计日志' }

    },
    // {
    //   path: 'permistion',
    //   component: () => import('@/views/setting/permission/index'),
    //   name: 'permistion',
    //   meta: { title: '权限' }
    // },
    // {
    //   path: '/permistion/create',
    //   component: () => import('@/views/setting/permission/create'),
    //   name: 'permistion_create',
    //   hidden: true,
    //   meta: { title: '权限添加' }
    // },
    // {
    //   path: '/system_log/index',
    //   component: () => import('@/views/setting/system_log/index'),
    //   name: 'system_log',
    //   hidden: true,
    //   meta: { title: '系统日志' }
    // },
    {
      path: 'role/list',
      component: () => import('@/views/setting/role/index'),
      name: 'role',
      meta: { title: '角色' }
    },
    {
      path: 'role/create',
      component: () => import('@/views/setting/role/create'),
      name: 'role_create',
      hidden: true,
      meta: { title: '角色添加' }
    },
    {
      path: 'role/edit/:id',
      component: () => import('@/views/setting/role/edit'),
      name: 'role_edit',
      hidden: true,
      meta: { title: '角色修改' }
    },
    {
      path: '/user/list',
      component: () => import('@/views/setting/user/index'),
      name: 'user_list',
      meta: { title: '用户列表' }
    },
    // {
    //   path: '/user/create',
    //   component: () => import('@/views/setting/user/create'),
    //   hidden: true,
    //   name: 'user_create',
    //   meta: { title: '用户添加' }
    // },
    // {
    //   path: '/user/edit/:id',
    //   component: () => import('@/views/setting/user/edit'),
    //   name: 'user_edit',
    //   hidden: true,
    //   meta: { title: '用户编辑' }
    // },
    {
      path: 'tenant',
      component: () => import('@/views/setting/tenant/index'),
      name: 'Tenant',
      meta: { title: '租户' }
    },
    {
      path: 'menu',
      component: () => import('@/views/setting/menu/index'),
      name: 'menu',
      meta: { title: '菜单' }
    }

  ]
}

export default settingRouter
