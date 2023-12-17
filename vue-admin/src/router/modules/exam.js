/** When your routing table is too long, you can split it into small modules **/

import Layout from '@/layout'

const examRouter = {
  path: '/exam',
  component: Layout,
  redirect: '/exam/list',
  alwaysShow: true,
  name: 'Exam',
  meta: {
    title: 'AppExam["Menu:ExamManage"]',
    policy: '',
    icon: 'el-icon-document'
  },
  children: [
    {
      path: '/exam/test-paper/list',
      component: () => import('@/views/exam/test-paper/index'),
      name: 'TestPaper',
      meta: {
        title: 'AppExam["Menu:TestPaper"]',
        // policy: 'AbpSaasPermissions.Tenants',
        icon: 'el-icon-wallet'
      }
    },
    {
      path: '/exam/list',
      component: () => import('@/views/exam/exam/index'),
      name: 'ExamManagement',
      meta: {
        title: 'AppExam["Menu:Exam"]',
        // policy: 'AbpSaasPermissions.Tenants',
        icon: 'el-icon-wallet'
      }
    },
    {
      path: '/exam/update/:id',
      component: () => import('@/views/exam/exam/components/ExamModel'),
      name: 'ExamUpdate',
      hidden: true,
      meta: {
        title: '考试详情', noCache: true, activeMenu: '/exam/list'
      }
    }
  ]
}
export default examRouter
