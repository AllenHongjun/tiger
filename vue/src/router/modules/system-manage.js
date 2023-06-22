import Layout from '@/layout'

const SystemManageRouter = {
  path: '/system-manage',
  component: Layout,
  alwaysShow: true,
  name: 'Setting',
  meta: {
    title: '管理',
    icon: 'el-icon-setting',
    policy: ''
  },
  children: [
    {
      path: '/identity',
      component: () => import('@/views/system-manage/index'), // Parent router-view
      name: 'identity',
      redirect: '/user/list',
      meta: {
        title: '身份认证',
        icon: 'el-icon-user-solid',
        policy: ''
      },
      children: [
        {
          path: 'organization-units/list',
          component: () => import('@/views/system-manage/identity/organization-units/index'),
          name: 'organization-units',
          meta: {
            title: '组织机构',
            policy: 'AbpIdentity.OrganizationUnits'
          }
        },
        {
          path: 'role/list',
          component: () => import('@/views/system-manage/identity/roles/index'),
          name: 'role',
          meta: { title: '角色', policy: 'AbpIdentity.Roles' }
        },
        {
          path: '/user/list',
          component: () => import('@/views/system-manage/identity/users/index'),
          name: 'user_list',
          meta: { title: '用户', policy: 'AbpIdentity.Users' }
        },
        {
          path: '/claim-type/list',
          component: () => import('@/views/system-manage/identity/claim-types/index'),
          name: 'claim-type',
          meta: { title: '声明类型', policy: 'AbpIdentity.IdentityClaimTypes' }
        },

        {
          path: '/security-log/list',
          component: () => import('@/views/system-manage/identity/security-logs/index'),
          name: 'security-log',
          meta: { title: '安全日志', policy: 'AbpIdentity.IdentitySecurityLogs' }
        }
      ]

    },
    {
      path: 'audit-log/list',
      component: () => import('@/views/system-manage/auditing/index'), // Parent router-view
      name: 'audit-log-list',
      meta: {
        title: '请求日志', // the name show in sidebar and breadcrumb (recommend set)
        icon: 'el-icon-document', // the icon show in the sidebar
        policy: 'Auditing.AuditingLog'
        // 路由的权限设置和后端的页面权限需要匹配 如果有就添加到路由表显示 如果没有就隐藏掉
      }
    },
    {
      path: 'setting/index',
      component: () => import('@/views/system-manage/setting/index'), // Parent router-view
      name: 'system',
      meta: {
        title: '系统设置',
        icon: 'el-icon-setting',
        policy: 'SettingUi.ShowSettingPage'
      }
    },
    // 基础设置
    {
      path: '/infrastructure',
      component: () => import('@/views/system-manage/infrastructure/index'), // Parent router-view
      name: 'infrastructure',
      meta: {
        title: '基础设施',
        icon: 'el-icon-user-solid',
        policy: ''
      },
      children: [
        {
          path: 'https://localhost:44306/index.html',
          component: Layout,
          name: 'swagger-api',
          meta: {
            title: '接口文档',
            icon: 'el-icon-notebook-1',
            policy: 'SettingUi.ShowSettingPage'
          }
        },
        {
          path: 'https://localhost:44306/hangfire/',
          component: Layout,
          url: 'http://www.baidu.com',
          name: 'background-job',
          meta: {
            title: '后台作业',
            icon: 'el-icon-message',
            policy: 'SettingUi.ShowSettingPage'
          }
        },
        {
          path: 'https://localhost:44306/quartz',
          component: Layout,
          name: 'background-worker',
          meta: {
            title: '定时任务',
            icon: 'el-icon-timer',
            policy: 'SettingUi.ShowSettingPage'
          }
        },
        {
          path: 'https://localhost:44306/index1.html',
          component: Layout,
          name: 'code-generation',
          meta: {
            title: '代码生成',
            icon: 'el-icon-notebook-1',
            policy: 'SettingUi.ShowSettingPage'
          }
        },
        {
          path: 'https://localhost:44306/index2.html',
          component: Layout,
          name: 'code-generation1',
          meta: {
            title: '表单构建',
            icon: 'el-icon-notebook-1',
            policy: 'SettingUi.ShowSettingPage'
          }
        },
        {
          path: '/server-monitoring/list',
          name: 'server-monitoring',
          component: () => import('@/views/system-manage/infrastructure/server-monitoring/index'),
          meta: {
            title: '服务监控',
            icon: 'el-icon-notebook-1'
            // policy: 'SettingUi.ShowSettingPage'
          }
        },
        {
          path: '/cache/index',
          name: 'server-monitoring',
          component: () => import('@/views/system-manage/infrastructure/cache/index'),
          meta: {
            title: '缓存监控',
            icon: 'el-icon-notebook-1'
            // policy: 'SettingUi.ShowSettingPage'
          }
        }
      ]
    }

  ]
}

export default SystemManageRouter
