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
      component: () => import('@/views/system/index'), // Parent router-view
      name: 'identity',
      redirect: '/user/list',
      meta: {
        title: '身份认证',
        icon: 'el-icon-user-solid',
        policy: ''
      },
      children: [
        {
          path: '/organization-units/list',
          component: () => import('@/views/system/identity/organization-units/index'),
          name: 'organization-units',
          meta: {
            title: '组织机构',
            policy: 'AbpIdentity.OrganizationUnits'
          }
        },
        // {
        //   path: '/post/list',
        //   component: () => import('@/views/system/identity/post/list'),
        //   name: 'post',
        //   meta: {
        //     title: '职位管理'
        //     // policy: 'AbpIdentity.OrganizationUnits'
        //   }
        // },
        {
          path: '/role/list',
          component: () => import('@/views/system/identity/roles/index'),
          name: 'role',
          meta: { title: '角色管理', policy: 'AbpIdentity.Roles' }
        },
        {
          path: '/user/list',
          component: () => import('@/views/system/identity/users/index'),
          name: 'user-list',
          meta: { title: '用户管理', policy: 'AbpIdentity.Users' }
        },
        {
          path: '/claim-type/list',
          component: () => import('@/views/system/identity/claim-types/index'),
          name: 'claim-type',
          meta: { title: '声明类型', policy: 'AbpIdentity.IdentityClaimTypes' }
        },

        {
          path: '/security-log/list',
          component: () => import('@/views/system/identity/security-logs/index'),
          name: 'security-log',
          meta: { title: '安全日志', policy: 'AbpIdentity.IdentitySecurityLogs' }
        }
      ]
    },
    {
      path: '/identity-server',
      component: () => import('@/views/system/index'), // Parent router-view
      name: 'identity-server',
      // redirect: '/user/list',
      meta: {
        title: '身份服务器',
        icon: 'peoples',
        policy: ''
      },
      children: [
        {
          path: '/client/list',
          component: () => import('@/views/system/identity-server/client/index'),
          name: 'client',
          meta: { title: '客户端', policy: 'IdentityServer.Clients' }
        },
        {
          path: '/api-resource/list',
          component: () => import('@/views/system/identity-server/api-resource/index'),
          name: 'api-resource',
          meta: {
            title: '接口资源'
            // policy: 'IdentityServer.ApiResources'
          }
        },
        {
          path: '/identity-resource/list',
          component: () => import('@/views/system/identity-server/identity-resource/index'),
          name: 'identity-resource',
          meta: { title: '标识资源', policy: 'IdentityServer.IdentityResources' }
        }

      ]
    },
    {
      path: '/platform',
      component: () => import('@/views/system/index'),
      name: 'platform',
      meta: {
        title: '平台管理', // the name show in sidebar and breadcrumb (recommend set)
        icon: 'el-icon-s-platform' // the icon show in the sidebar
        // policy: 'AbpTextTemplating.TextTemplates'
      },
      children: [
        {
          path: 'data/list',
          component: () => import('@/views/system/platform/data/index'), // Parent router-view
          name: 'data',
          meta: {
            title: '数据字典' // the name show in sidebar and breadcrumb (recommend set)
            // icon: 'el-icon-document-copy' // the icon show in the sidebar
            // policy: 'AbpTextTemplating.TextTemplates'
          }
        },
        {
          path: 'menu/list',
          component: () => import('@/views/system/platform/menu/index'), // Parent router-view
          name: 'menu',
          meta: {
            title: '菜单管理' // the name show in sidebar and breadcrumb (recommend set)
            // icon: 'el-icon-document-copy' // the icon show in the sidebar
            // policy: 'AbpTextTemplating.TextTemplates'
          }
        },
        {
          path: 'layout/list',
          component: () => import('@/views/system/platform/layout/index'), // Parent router-view
          name: 'layout',
          meta: {
            title: '布局管理' // the name show in sidebar and breadcrumb (recommend set)
            // icon: 'el-icon-document-copy' // the icon show in the sidebar
            // policy: 'AbpTextTemplating.TextTemplates'
          }
        }
      ]
    },
    {
      path: '/oss',
      component: () => import('@/views/system/index'),
      name: 'oss',
      meta: {
        title: '对象存储', // the name show in sidebar and breadcrumb (recommend set)
        icon: 'el-icon-files' // the icon show in the sidebar
        // policy: 'AbpTextTemplating.TextTemplates'
      },
      children: [
        {
          path: 'container/list',
          component: () => import('@/views/system/oss/container/index'), // Parent router-view
          name: 'container',
          meta: {
            title: '容器管理' // the name show in sidebar and breadcrumb (recommend set)
            // icon: 'el-icon-document-copy' // the icon show in the sidebar
            // policy: 'AbpTextTemplating.TextTemplates'
          }
        },
        {
          path: 'object/list',
          component: () => import('@/views/system/oss/object/index'), // Parent router-view
          name: 'object',
          meta: {
            title: '对象管理' // the name show in sidebar and breadcrumb (recommend set)
            // icon: 'el-icon-document-copy' // the icon show in the sidebar
            // policy: 'AbpTextTemplating.TextTemplates'
          }
        }
      ]
    },
    {
      path: '/task',
      component: () => import('@/views/system/index'),
      name: 'task',
      meta: {
        title: '定时任务', // the name show in sidebar and breadcrumb (recommend set)
        icon: 'el-icon-time' // the icon show in the sidebar
        // policy: 'AbpTextTemplating.TextTemplates'
      },
      children: [
        {
          path: 'background-job/list',
          component: () => import('@/views/system/task/background-job/index'), // Parent router-view
          name: 'background-job',
          meta: {
            title: '任务管理' // the name show in sidebar and breadcrumb (recommend set)
            // icon: 'el-icon-document-copy' // the icon show in the sidebar
            // policy: 'AbpTextTemplating.TextTemplates'
          }
        }
        // {
        //   path: 'object/list',
        //   component: () => import('@/views/system/oss/object/index'), // Parent router-view
        //   name: 'object',
        //   meta: {
        //     title: '对象管理' // the name show in sidebar and breadcrumb (recommend set)
        //     // icon: 'el-icon-document-copy' // the icon show in the sidebar
        //     // policy: 'AbpTextTemplating.TextTemplates'
        //   }
        // }
      ]
    },
    {
      path: 'text-template/list',
      component: () => import('@/views/system/text-template/text-template-table'), // Parent router-view
      name: 'text-template',
      meta: {
        title: '文本模板', // the name show in sidebar and breadcrumb (recommend set)
        icon: 'el-icon-document-copy', // the icon show in the sidebar
        policy: 'AbpTextTemplating.TextTemplates'
      }
    },
    {
      path: 'audit-log/list',
      component: () => import('@/views/system/auditing/index'), // Parent router-view
      name: 'audit-log-list',
      meta: {
        title: '审计日志', // the name show in sidebar and breadcrumb (recommend set)
        icon: 'el-icon-document', // the icon show in the sidebar
        policy: 'Auditing.AuditingLog'
        // 路由的权限设置和后端的页面权限需要匹配 如果有就添加到路由表显示 如果没有就隐藏掉
      }
    },
    {
      path: 'notice/list',
      component: () => import('@/views/system/notice/list'), // Parent router-view
      name: 'notice-list',
      meta: {
        title: '通知', // the name show in sidebar and breadcrumb (recommend set)
        icon: 'el-icon-message' // the icon show in the sidebar
        // policy: 'Auditing.AuditingLog'
        // 路由的权限设置和后端的页面权限需要匹配 如果有就添加到路由表显示 如果没有就隐藏掉
      }
    },

    // 运维监控
    {
      path: 'monitor/index',
      component: () => import('@/views/system/monitor/index'), // Parent router-view
      name: 'monitor',
      meta: {
        title: '运维监控',
        icon: 'el-icon-monitor'
        // policy: 'SettingUi.ShowSettingPage'
      },
      children: [
        {
          path: '/monitor/server/list',
          name: 'server-monitoring',
          component: () => import('@/views/system/monitor/server/index'),
          meta: {
            title: '主机信息',
            icon: 'el-icon-s-platform'
            // policy: 'SettingUi.ShowSettingPage'
          }
        },
        {
          path: '/cache/index',
          name: 'cache-monitoring',
          component: () => import('@/views/system/monitor/cache/index'),
          meta: {
            title: '缓存监控',
            icon: 'pc-monitor'
            // policy: 'SettingUi.ShowSettingPage'
          }
        },
        {
          path: '/cache/manage',
          name: 'cache-manage',
          component: () => import('@/views/system/monitor/cache/manage'),
          meta: {
            title: '缓存管理',
            icon: 'Redis'
            // policy: 'SettingUi.ShowSettingPage'
          }
        },
        {
          path: '/cache/table',
          name: 'cache-table',
          component: () => import('@/views/system/monitor/cache/table'),
          meta: {
            title: '缓存列表',
            icon: 'Redis'
            // policy: 'SettingUi.ShowSettingPage'
          }
        }
      ]
    },
    // 基础设施
    {
      path: '/infrastructure',
      component: () => import('@/views/system/infrastructure/index'), // Parent router-view
      name: 'infrastructure',
      meta: {
        title: '基础设施',
        icon: 'el-icon-user-solid',
        policy: ''
      },
      children: [
        {
          path: 'swagger-api/index',
          component: () => import('@/views/system/infrastructure/swagger-api/index'), // Parent router-view,
          name: 'swagger-api',
          meta: {
            title: '接口文档',
            icon: 'el-icon-notebook-1',
            policy: 'SettingUi.ShowSettingPage'
          }
        },
        {
          path: 'https://localhost:44306/hangfire/?period=week',
          component: Layout, // Parent router-view,
          name: 'background-job',
          meta: {
            title: '后台作业',
            icon: 'el-icon-message',
            policy: 'SettingUi.ShowSettingPage'
          }
        },
        {
          path: 'quartz/index',
          component: () => import('@/views/system/infrastructure/crystal-quartz/index'), // Parent router-view,
          name: 'background-worker',
          meta: {
            title: '定时任务',
            icon: 'el-icon-timer',
            policy: 'SettingUi.ShowSettingPage'
          }
        },
        {
          path: 'https://easyabp.io/abphelper/AbpHelper.GUI/#getting-started',
          component: Layout,
          name: 'code-generation',
          meta: {
            title: '代码生成',
            icon: 'el-icon-notebook-1',
            policy: 'SettingUi.ShowSettingPage'
          }
        },
        {
          path: '/dev-tool/vfrom2',
          component: () => import('@/views/system/infrastructure/vfrom2/index'),
          name: 'vfrom',
          meta: {
            title: '表单构建',
            icon: 'el-icon-notebook-1'
            // policy: 'SettingUi.ShowSettingPage'
          }
        }

      ]
    },
    {
      path: 'setting/index',
      component: () => import('@/views/system/setting/index'), // Parent router-view
      name: 'system',
      meta: {
        title: '系统设置',
        icon: 'el-icon-setting',
        policy: 'SettingUi.ShowSettingPage'
      }
    }

  ]
}

export default SystemManageRouter
