/** When your routing table is too long, you can split it into small modules **/

import Layout from '@/layout'

const sassRouter = {
  path: '/sass',
  component: Layout,
  redirect: '/tenant/list',
  alwaysShow: true,
  name: 'Saas',
  meta: {
    title: 'AbpSaas["Menu:Saas"]',
    policy: '',
    icon: 'el-icon-cloudy'
  },
  children: [
    {
      path: '/tenant/list',
      component: () => import('@/views/saas/tenant/index'),
      name: 'Tenant',
      meta: {
        title: 'AbpSaas["Tenants"]',
        policy: 'AbpSaasPermissions.Tenants',
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
        policy: 'AbpSaasPermissions.Editions'
      }
    }
  ]
}
export default sassRouter
