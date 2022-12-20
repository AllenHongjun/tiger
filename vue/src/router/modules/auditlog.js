import Layout from '@/layout'

const auditlogRouter = {
  path: '/auditlogs',
  component: Layout,
  children: [
    {
      path: 'auditlog',
      component: () => import('@/views/auditlogging/index'),
      name: 'AuditLog',
      meta: {
        title: '审计日志',
        policy: 'AbpAuditLogging.Default',
        icon: 'el-icon-document'
      }
    }
  ]
}

export default auditlogRouter
