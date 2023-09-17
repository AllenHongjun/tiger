/** When your routing table is too long, you can split it into small modules **/

import Layout from '@/layout'

const sassRouter = {
  path: '/sass',
  component: Layout,
  redirect: '/tenant/list',
  alwaysShow: true,
  name: 'Saas',
  meta: {
    title: 'Saas',
    policy: 'AbpTenantManagement.Tenants',
    icon: 'el-icon-cloudy'
  },
  children: [
    {
      path: '/tenant/list',
      component: () => import('@/views/saas/tenant/index'),
      name: 'Tenant',
      meta: {
        title: 'AbpSaas["Tenants"]',
        policy: 'AbpTenantManagement.Tenants',
        icon: 'el-icon-wallet'
      }
    },
    {
      path: '/edition/list',
      component: () => import('@/views/saas/edition/index'),
      name: 'edition',
      meta: {
        title: 'AbpSaas["Editions"]',
        icon: 'el-icon-notebook-2',
        // TODO: 增加权限控制
        policy: 'AbpTenantManagement.Tenants'
      }
      // meta: { title: '版本', policy: 'AbpTenantManagement.Editions' }
    }
  ]
}
export default sassRouter
