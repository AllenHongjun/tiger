/** When your routing table is too long, you can split it into small modules **/

import Layout from '@/layout'

const questionBankRouter = {
  path: '/question-bank',
  component: Layout,
  redirect: '/question-bank/list',
  alwaysShow: true,
  name: 'QuestionBank',
  meta: {
    title: 'AppQuestionBank["Menu:QuestionBank"]',
    policy: '',
    icon: 'el-icon-box'
  },
  children: [
    {
      path: '/question-bank/question-category/list',
      component: () => import('@/views/question-bank/question-category/index'),
      name: 'QuestionCategory',
      meta: {
        title: 'AppQuestionBank["Menu:QuestionCategory"]',
        // policy: 'AbpSaasPermissions.Tenants',
        icon: 'el-icon-wallet'
      }
    },
    {
      path: '/question-bank/question/list',
      component: () => import('@/views/question-bank/question/index'),
      name: 'Question',
      meta: {
        title: 'AppQuestionBank["Menu:Question"]',
        // policy: 'AbpSaasPermissions.Tenants',
        icon: 'el-icon-wallet'
      }
    },
    {
      path: '/question-bank/train-platform/list',
      component: () => import('@/views/question-bank/train-platform/index'),
      name: 'TrainPlatform',
      meta: {
        title: 'AppQuestionBank["Menu:TrainPlatform"]',
        // policy: 'AbpSaasPermissions.Tenants',
        icon: 'el-icon-wallet'
      }
    }

  ]
}
export default questionBankRouter
