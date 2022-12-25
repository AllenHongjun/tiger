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
        title: '请求日志',               //the name show in sidebar and breadcrumb (recommend set)
        icon: 'el-icon-document', //the icon show in the sidebar
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
    // 添加外链
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
      url: "http://www.baidu.com",
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
      path: '/test',
      name: '测试',
      meta: { title: '测试', icon: 'el-icon-coffee', policy: 'SettingUi.ShowSettingPage' },
      component: Layout,
      redirect: '/vue2',
      // 这里开始对应的路由都会显示在app-main中 如上图所示
      children: [
        {
          path: 'vue2',
          name: 'vue2',
          component: () => import('@/views/vue2.0/index'),
          meta: { title: 'vue2' }
        },
        {
          path: 'vue2/componet',
          name: 'vue2-componet',
          component: () => import('@/views/vue2.0/component'),
          meta: { title: 'vue2组件' }
        },
        {
          path: 'vue2/element-ui',
          name: 'element-ui',
          component: () => import('@/views/vue2.0/element-ui'),
          meta: { title: 'element-ui' }
        }
      ]
    }

  ]
}

export default SystemManageRouter
