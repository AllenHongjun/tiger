/** When your routing table is too long, you can split it into small modules **/

import Layout from '@/layout'

const schoolManageRouter = {
  path: '/school-mange',
  component: Layout,
  redirect: '/school-mange/list',
  alwaysShow: true,
  name: 'SchoolManage',
  meta: {
    title: 'AppSchoolManage["Menu:SchoolManage"]',
    policy: '',
    icon: 'el-icon-s-operation'
  },
  children: [
    {
      path: '/school-mange/school/list',
      component: () => import('@/views/school-manage/school/index'),
      name: 'School',
      meta: {
        title: 'AppSchoolManage["Menu:School"]',
        // policy: 'AbpSaasPermissions.Tenants',
        icon: 'el-icon-wallet'
      }
    },
    {
      path: '/school-mange/class/list',
      component: () => import('@/views/school-manage/class/index'),
      name: 'Class',
      meta: {
        title: 'AppSchoolManage["Menu:Class"]',
        // policy: 'AbpSaasPermissions.Tenants',
        icon: 'el-icon-wallet'
      }
    }

  ]
}
export default schoolManageRouter
