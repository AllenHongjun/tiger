/** When your routing table is too long, you can split it into small modules **/

import Layout from '@/layout'

const TeachingRouter = {
  path: '/teaching',
  component: Layout,
  redirect: '/teaching/list',
  alwaysShow: true,
  name: 'Teaching',
  meta: {
    title: 'AppTeaching["Menu:TeachingManage"]',
    policy: '',
    icon: 'el-icon-notebook-1'
  },
  children: [
    {
      path: '/teaching/course/list',
      component: () => import('@/views/teaching/course/index'),
      name: 'TeachingManagement',
      meta: {
        title: 'AppTeaching["Menu:Course"]',
        // policy: 'AbpSaasPermissions.Tenants',
        icon: 'el-icon-wallet'
      }
    }
    // {
    //   path: '/teaching/test-paper/list',
    //   component: () => import('@/views/teaching/test-paper/index'),
    //   name: 'TestPaper',
    //   meta: {
    //     title: 'AppTeaching["Menu:TestPaper"]',
    //     // policy: 'AbpSaasPermissions.Tenants',
    //     icon: 'el-icon-wallet'
    //   }
    // }
  ]
}
export default TeachingRouter
