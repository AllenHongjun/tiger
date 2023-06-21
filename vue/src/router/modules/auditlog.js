import Layout from '@/layout'

const auditlogRouter = {
  path: '/auditlogs',
  // component: Layout,
  component: () => import('@/views/auditlogging/index'),
  name: 'AuditLog',
  meta: {
    title: '审计日志',
    policy: 'Auditing.AuditingLog',
    icon: 'el-icon-document'
  }
}

export default auditlogRouter
