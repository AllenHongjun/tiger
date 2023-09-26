import Layout from '@/layout'

const SystemManageRouter = {
  path: '/system-manage',
  component: Layout,
  alwaysShow: true,
  name: 'Setting',
  meta: {
    title: 'TigerUi["Menu:Administration"]',
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
        title: 'AbpIdentity["Menu:IdentityManagement"]',
        icon: 'el-icon-user-solid',
        policy: ''
      },
      children: [
        {
          path: '/organization-units/list',
          component: () => import('@/views/system/identity/organization-units/index'),
          name: 'organization-units',
          meta: {
            title: 'AbpIdentity["OrganizationUnits"]',
            policy: 'AbpIdentity.OrganizationUnits'
          }
        },
        {
          path: '/role/list',
          component: () => import('@/views/system/identity/roles/index'),
          name: 'role',
          meta: { title: 'AbpIdentity["Roles"]', policy: 'AbpIdentity.Roles' }
        },
        {
          path: '/user/list',
          component: () => import('@/views/system/identity/users/index'),
          name: 'user-list',
          meta: { title: 'AbpIdentity["Users"]', policy: 'AbpIdentity.Users' }
        },
        {
          path: '/claim-type/list',
          component: () => import('@/views/system/identity/claim-types/index'),
          name: 'claim-type',
          meta: { title: 'AbpIdentity["ClaimTypes"]', policy: 'AbpIdentity.IdentityClaimTypes' }
        },

        {
          path: '/security-log/list',
          component: () => import('@/views/system/identity/security-logs/index'),
          name: 'security-log',
          meta: { title: 'AbpIdentity["SecurityLog"]', policy: 'AbpIdentity.IdentitySecurityLogs' }
        }
      ]
    },
    {
      path: '/identity-server',
      component: () => import('@/views/system/index'), // Parent router-view
      name: 'identity-server',
      // redirect: '/user/list',
      meta: {
        title: 'AbpIdentityServer["Permissions:IdentityServer"]',
        icon: 'peoples',
        policy: ''
      },
      children: [
        {
          path: '/client/list',
          component: () => import('@/views/system/identity-server/client/index'),
          name: 'client',
          meta: { title: 'AbpIdentityServer["DisplayName:Clients"]', policy: 'IdentityServer.Clients' }
        },
        {
          path: '/api-resource/list',
          component: () => import('@/views/system/identity-server/api-resource/index'),
          name: 'api-resource',
          meta: {
            title: 'AbpIdentityServer["DisplayName:ApiResources"]'
            // policy: 'IdentityServer.ApiResources'
          }
        },
        {
          path: '/identity-resource/list',
          component: () => import('@/views/system/identity-server/identity-resource/index'),
          name: 'identity-resource',
          meta: { title: 'AbpIdentityServer["DisplayName:IdentityResources"]', policy: 'IdentityServer.IdentityResources' }
        },
        {
          path: '/persisted-grant/list',
          component: () => import('@/views/system/identity-server/persisted-grant/index'),
          name: 'persisted-grant',
          meta: { title: 'AbpIdentityServer["DisplayName:PersistedGrants"]', policy: 'IdentityServer.IdentityResources' }
        }

      ]
    },
    {
      path: '/localization',
      component: () => import('@/views/system/index'),
      name: 'localization',
      meta: {
        title: 'LocalizationManagement["Permissions:LocalizationManagement"]', // the name show in sidebar and breadcrumb (recommend set)
        icon: 'international' // the icon show in the sidebar<svg-icon icon-class="international" />
        // policy: 'LocalizationManagement'
      },
      children: [
        {
          path: 'localization/language',
          component: () => import('@/views/system/localization/language/index'), // Parent router-view
          name: 'language',
          meta: {
            title: 'LocalizationManagement["Permissions:Language"]' // the name show in sidebar and breadcrumb (recommend set)
            // policy: 'LocalizationPermissions.Languages'
          }
        },
        {
          path: 'localization/language-text',
          component: () => import('@/views/system/localization/language-text/index'), // Parent router-view
          name: 'language-text',
          meta: {
            title: 'LocalizationManagement["Permissions:LanguageText"]' // the name show in sidebar and breadcrumb (recommend set)
            // policy: 'LocalizationPermissions.Languages'
          }
        }
      ]
    },
    {
      path: '/platform',
      component: () => import('@/views/system/index'),
      name: 'platform',
      meta: {
        title: 'AppPlatform["Permission:Platform"]', // the name show in sidebar and breadcrumb (recommend set)
        icon: 'el-icon-s-platform' // the icon show in the sidebar
        // policy: 'AbpTextTemplating.TextTemplates'
      },
      children: [
        {
          path: 'data/list',
          component: () => import('@/views/system/platform/data/index'), // Parent router-view
          name: 'data',
          meta: {
            title: 'AppPlatform["Permission:Data"]' // the name show in sidebar and breadcrumb (recommend set)
            // policy: 'AbpTextTemplating.TextTemplates'
          }
        },
        {
          path: 'menu/list',
          component: () => import('@/views/system/platform/menu/index'), // Parent router-view
          name: 'menu',
          meta: {
            title: 'AppPlatform["DisplayName:Menus"]' // the name show in sidebar and breadcrumb (recommend set)
            // policy: 'AbpTextTemplating.TextTemplates'
          }
        },
        {
          path: 'layout/list',
          component: () => import('@/views/system/platform/layout/index'), // Parent router-view
          name: 'layout',
          meta: {
            title: 'AppPlatform["Permission:Layout"]' // the name show in sidebar and breadcrumb (recommend set)
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
        title: 'AbpOssManagement["Permission:OssManagement"]', // the name show in sidebar and breadcrumb (recommend set)
        icon: 'el-icon-files' // the icon show in the sidebar
        // policy: 'AbpTextTemplating.TextTemplates'
      },
      children: [
        {
          path: 'container/list',
          component: () => import('@/views/system/oss/container/index'), // Parent router-view
          name: 'container',
          meta: {
            title: 'AbpOssManagement["Permission:Container"]' // the name show in sidebar and breadcrumb (recommend set)
            // policy: 'AbpTextTemplating.TextTemplates'
          }
        },
        {
          path: 'object/list',
          component: () => import('@/views/system/oss/object/index'), // Parent router-view
          name: 'object',
          meta: {
            title: 'AbpOssManagement["Permission:OssObject"]' // the name show in sidebar and breadcrumb (recommend set)
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
        title: 'TaskManagement["Permissions:TaskManagement"]', // the name show in sidebar and breadcrumb (recommend set)
        icon: 'el-icon-time' // the icon show in the sidebar
        // policy: 'AbpTextTemplating.TextTemplates'
      },
      children: [
        {
          path: 'background-worker/list',
          component: () => import('@/views/system/task/background-job/index'), // Parent router-view
          name: 'background-worker',
          meta: {
            title: 'TaskManagement["Permissions:BackgroundJobs"]' // the name show in sidebar and breadcrumb (recommend set)
            // policy: 'AbpTextTemplating.TextTemplates'
          }
        }
      ]
    },
    {
      path: 'text-template/list',
      component: () => import('@/views/system/text-template/text-template-table'), // Parent router-view
      name: 'text-template',
      meta: {
        title: 'AbpTextTemplate["Permission:TextTemplating"]', // the name show in sidebar and breadcrumb (recommend set)
        icon: 'el-icon-document-copy', // the icon show in the sidebar
        policy: 'AbpTextTemplating.TextTemplates'
      }
    },
    {
      path: '/audit-log',
      component: () => import('@/views/system/index'), // Parent router-view
      name: 'audit-log-module',
      meta: {
        title: 'AbpAuditLogging["Menu:AuditLogging"]', // the name show in sidebar and breadcrumb (recommend set)
        icon: 'el-icon-document' // the icon show in the sidebar
      },
      children: [
        {
          path: 'audit-log/list',
          component: () => import('@/views/system/auditing/index'), // Parent router-view
          name: 'audit-log',
          meta: {
            title: 'AbpAuditLogging["Menu:AuditLogging"]', // the name show in sidebar and breadcrumb (recommend set)
            policy: 'Auditing.AuditingLog'
          }
        },
        {
          path: 'audit-log/entity-changes',
          component: () => import('@/views/system/auditing/entity-changes/index'),
          name: 'entity-changes',
          meta: {
            title: 'AbpAuditLogging["EntityChanges"]',
            policy: 'Auditing.AuditingLog'
          }
        }
      ]
    },
    {
      path: 'notice/list',
      component: () => import('@/views/system/notice/list'), // Parent router-view
      name: 'notice-list',
      meta: {
        title: '通知', // the name show in sidebar and breadcrumb (recommend set)
        icon: 'el-icon-message' // the icon show in the sidebar
        // policy: 'Auditing.AuditingLog'
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
          name: 'background-worker-crystal',
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
        title: 'AbpSettingUi["Settings"]',
        icon: 'el-icon-setting',
        policy: 'SettingUi.ShowSettingPage'
      }
    }

  ]
}

export default SystemManageRouter
