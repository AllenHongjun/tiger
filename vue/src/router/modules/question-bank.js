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
    icon: 'el-icon-s-operation'
  },
  children: [
    {
      path: '/question-bank/list',
      component: () => import('@/views/question-bank/question-category/index'),
      name: 'Tenant',
      meta: {
        title: 'AppQuestionBank["Menu:QuestionCategory"]',
        // policy: 'AbpSaasPermissions.Tenants',
        icon: 'el-icon-wallet'
      }
    }

  ]
}
export default questionBankRouter
