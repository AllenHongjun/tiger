/** When your routing table is too long, you can split it into small modules **/

import Layout from '@/layout'

const identityRouter = {
  path: '/identity',
  component: Layout,
  redirect: 'noRedirect',
  alwaysShow: true,
  name: 'Identity',
  meta: {
    title: '身份认证', 
    icon: 'user',
    policy: ''
  },
  children: [
    {
      path: 'organizations',
      component: () => import('@/views/identity/organizations'),
      name: 'Organizations',
      meta: { title: '组织机构', policy: 'AbpIdentity.OrganitaionUnits' }
    },
    {
      path: 'roles',
      component: () => import('@/views/identity/roles'),
      name: 'Roles',
      meta: { title: '角色', policy: 'AbpIdentity.Roles' }
    },
    {
      path: 'users',
      component: () => import('@/views/identity/users'),
      name: 'Users',
      meta: { title: '用户', policy: 'AbpIdentity.Users' }
    },
    {
      path: 'claim-types',
      component: () => import('@/views/identity/claim-types'),
      name: 'ClaimTypes',
      meta: { title: '声明类型', policy: 'AbpIdentity.ClaimTypes' }
    }
  ]
}
export default identityRouter
