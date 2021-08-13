import Layout from '@/layout'

const settingRouter = {
  path: '/setting',
  component: Layout,
  alwaysShow: true,
  redirect: '/setting/audit_log/list',
  name: 'Setting',
  // hidden: true,
  meta: {
    title: '设置',
    icon: 'el-icon-setting',
    policy: 'AbpIdentity.Users'
  },
  children: [
    {
      path: 'audit_log/list',
      component: () => import('@/views/setting/audit_log/index'), // Parent router-view
      name: 'audit_log_list',
      meta: { title: '系统日志' }

    },
    {
      path: '/identity',
      component: () => import('@/views/setting/identity/index'), // Parent router-view
      name: 'identity',
      redirect: '/user/list',
      meta: { title: '身份认证' },
      children: [
        {
          path: 'role/list',
          component: () => import('@/views/setting/role/index'),
          name: 'role',
          meta: { title: '角色', policy: 'AbpIdentity.Roles' }
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
          meta: { title: '用户', policy: 'AbpIdentity.Users' }
        },
        {
          path: 'tenant',
          component: () => import('@/views/setting/tenant/index'),
          name: 'Tenant',
          meta: { title: '租户', policy: 'AbpTenantManagement.Tenants' }
        },
        {
          path: '/organization/list',
          component: () => import('@/views/setting/organization/index'),
          name: 'organization',
          meta: { title: '组织', policy: 'AbpIdentity.OrganitaionUnits' }
        }
      ]

    }

  ]
}

export default settingRouter
