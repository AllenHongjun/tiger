/** When your routing table is too long, you can split it into small modules **/

import Layout from '@/layout'

const ReportRouter = {
  path: '/report',
  component: Layout,
  redirect: '/report/list',
  alwaysShow: true,
  name: 'Report',
  meta: {
    title: '报表',
    policy: '',
    icon: 'el-icon-s-operation'
  },
  children: [
    {
      path: '/report/exam-scores',
      component: () => import('@/views/report/exam-scores/index'),
      name: 'exam-scores',
      meta: {
        title: '考试成绩',
        // policy: 'AbpSaasPermissions.Tenants',
        icon: 'el-icon-wallet'
      }
    },
    {
      path: '/report/user-analysis',
      component: () => import('@/views/report/user-analysis/index'),
      name: 'user-analysis',
      meta: {
        title: '考生分析',
        // policy: 'AbpSaasPermissions.Tenants',
        icon: 'el-icon-wallet'
      }
    },
    {
      path: '/report/exam-question-analysis',
      component: () => import('@/views/report/exam-question-analysis/index'),
      name: 'examgrade',
      meta: {
        title: '答题统计',
        // policy: 'AbpSaasPermissions.Tenants',
        icon: 'el-icon-wallet'
      }
    }
    // {
    //   path: '/report/absent-analysis',
    //   component: () => import('@/views/report/absent-analysis/index'),
    //   name: 'absent-analysis',
    //   meta: {
    //     title: '缺席统计',
    //     // policy: 'AbpSaasPermissions.Tenants',
    //     icon: 'el-icon-wallet'
    //   }
    // },
    // {
    //   path: '/report/judge-paper',
    //   component: () => import('@/views/report/judge-paper/index'),
    //   name: 'judge-paper',
    //   meta: {
    //     title: '人工评卷',
    //     // policy: 'AbpSaasPermissions.Tenants',
    //     icon: 'el-icon-wallet'
    //   }
    // },
    // {
    //   path: '/report/exer-analysis',
    //   component: () => import('@/views/report/exer-analysis/index'),
    //   name: 'exer-analysis',
    //   meta: {
    //     title: '练习统计',
    //     // policy: 'AbpSaasPermissions.Tenants',
    //     icon: 'el-icon-wallet'
    //   }
    // },
    // {
    //   path: '/report/exer-analysis',
    //   component: () => import('@/views/report/exer-analysis/index'),
    //   name: 'exer-analysis',
    //   meta: {
    //     title: '练习统计',
    //     // policy: 'AbpSaasPermissions.Tenants',
    //     icon: 'el-icon-wallet'
    //   }
    // },
    // {
    //   path: '/report/lesson-analysis',
    //   component: () => import('@/views/report/lesson-analysis/index'),
    //   name: 'lesson-analysis',
    //   meta: {
    //     title: '学习统计',
    //     // policy: 'AbpSaasPermissions.Tenants',
    //     icon: 'el-icon-wallet'
    //   }
    // },
    // {
    //   path: '/report/question-analysis',
    //   component: () => import('@/views/report/question-analysis/index'),
    //   name: 'question-analysis',
    //   meta: {
    //     title: '试题统计',
    //     // policy: 'AbpSaasPermissions.Tenants',
    //     icon: 'el-icon-wallet'
    //   }
    // },
    // {
    //   path: '/report/lesson-analysis',
    //   component: () => import('@/views/report/question-type-analysis/index'),
    //   name: 'lesson-analysis',
    //   meta: {
    //     title: '试题类型统计',
    //     // policy: 'AbpSaasPermissions.Tenants',
    //     icon: 'el-icon-wallet'
    //   }
    // }
  ]
}
export default ReportRouter
