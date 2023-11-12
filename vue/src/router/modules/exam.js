/** When your routing table is too long, you can split it into small modules **/

import Layout from '@/layout'

const examRouter = {
  path: '/exam',
  component: Layout,
  redirect: '/exam/list',
  alwaysShow: true,
  name: 'Exam',
  meta: {
    title: 'AppExam["Menu:Exam"]',
    policy: '',
    icon: 'el-icon-s-operation'
  },
  children: [
    // {
    //   path: '/exam/exams/list',
    //   component: () => import('@/views/exam/exams/index'),
    //   name: 'ExamManagement',
    //   meta: {
    //     title: 'AppExam["Menu:Exam"]',
    //     // policy: 'AbpSaasPermissions.Tenants',
    //     icon: 'el-icon-wallet'
    //   }
    // },
    // {
    //   path: '/exam/test-paper/list',
    //   component: () => import('@/views/exam/test-paper/index'),
    //   name: 'TestPaper',
    //   meta: {
    //     title: 'AppExam["Menu:TestPaper"]',
    //     // policy: 'AbpSaasPermissions.Tenants',
    //     icon: 'el-icon-wallet'
    //   }
    // }

  ]
}
export default examRouter
