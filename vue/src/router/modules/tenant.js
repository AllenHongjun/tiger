/** When your routing table is too long, you can split it into small modules **/

import Layout from '@/layout'

const tenantRouter = {
  path: '/sass',
  component: Layout,
  redirect: '/tenant/list',
  alwaysShow: true,
  name: 'Saas',
  meta: {
    title: 'Saas',
    policy: 'AbpTenantManagement.Tenants',
    icon: 'peoples'
  },
  children: [
    {
      path: '/tenant/list',
      component: () => import('@/views/saas/tenant/index'),
      name: 'Tenant',
      meta: {
        title: '租户',
        policy: 'AbpTenantManagement.Tenants'
      }
    },
    {
      path: '/edition/list',
      component: () => import('@/views/saas/edition/index'),
      name: 'edition',
      meta: { title: '版本', policy: 'AbpTenantManagement.Editions' }
    }
  ]
}
export default tenantRouter
