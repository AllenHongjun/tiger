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
      component: () => import('@/views/system/index'),
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
      component: () => import('@/views/system/index'),
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
            title: 'AbpIdentityServer["DisplayName:ApiResources"]',
            policy: 'IdentityServer.ApiResources'
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
          meta: { title: 'AbpIdentityServer["DisplayName:PersistedGrants"]', policy: 'IdentityServer.Grants' }
        }

      ]
    },
    {
      path: '/localization',
      component: () => import('@/views/system/index'),
      name: 'localization',
      meta: {
        title: 'LocalizationManagement["Permission:LocalizationManagement"]',
        icon: 'international',
        policy: ''
      },
      children: [
        {
          path: 'localization/language',
          component: () => import('@/views/system/localization/language/index'),
          name: 'language',
          meta: {
            title: 'LocalizationManagement["Permission:Languages"]',
            policy: 'LocalizationManagement.Languages'
          }
        },
        {
          path: 'localization/language-text',
          component: () => import('@/views/system/localization/language-text/index'),
          name: 'language-text',
          meta: {
            title: 'LocalizationManagement["Permission:LanguageTexts"]',
            policy: 'LocalizationManagement.LanguageTexts'
          }
        }
      ]
    },
    {
      path: '/platform',
      component: () => import('@/views/system/index'),
      name: 'platform',
      meta: {
        title: 'AppPlatform["Permission:Platform"]',
        icon: 'el-icon-s-platform',
        policy: ''
      },
      children: [
        {
          path: 'data/list',
          component: () => import('@/views/system/platform/data/index'),
          name: 'data',
          meta: {
            title: 'AppPlatform["Permission:Data"]',
            policy: 'Platform.DataDictionary'
          }
        },
        {
          path: 'menu/list',
          component: () => import('@/views/system/platform/menu/index'),
          name: 'menu',
          meta: {
            title: 'AppPlatform["DisplayName:Menus"]',
            policy: 'Platform.Menu'
          }
        },
        {
          path: 'layout/list',
          component: () => import('@/views/system/platform/layout/index'),
          name: 'layout',
          meta: {
            title: 'AppPlatform["Permission:Layout"]',
            policy: 'Platform.Layout'
          }
        }
      ]
    },
    {
      path: '/oss',
      component: () => import('@/views/system/index'),
      name: 'oss',
      meta: {
        title: 'AbpOssManagement["Permission:OssManagement"]',
        icon: 'el-icon-files', // the icon show in the sidebar
        policy: ''
      },
      children: [
        {
          path: 'container/list',
          component: () => import('@/views/system/oss/container/index'),
          name: 'container',
          meta: {
            title: 'AbpOssManagement["Permission:Container"]',
            policy: 'AbpOssManagement.Container'
          }
        },
        {
          path: 'object/list',
          component: () => import('@/views/system/oss/object/index'),
          name: 'object',
          meta: {
            title: 'AbpOssManagement["Permission:OssObject"]',
            policy: 'AbpOssManagement.OssObject'
          }
        }
      ]
    },
    {
      path: '/task',
      component: () => import('@/views/system/index'),
      name: 'task',
      meta: {
        title: 'TaskManagement["Permissions:TaskManagement"]',
        icon: 'el-icon-time',
        policy: ''
      },
      children: [
        {
          path: 'background-worker/list',
          component: () => import('@/views/system/task/background-job/index'),
          name: 'background-worker',
          meta: {
            title: 'TaskManagement["Permissions:BackgroundJobs"]',
            policy: 'TaskManagement.BackgroundJobs'
          }
        }
      ]
    },
    {
      path: 'text-template/list',
      component: () => import('@/views/system/text-template/text-template-table'),
      name: 'text-template',
      meta: {
        title: 'AbpTextTemplate["Permission:TextTemplating"]',
        icon: 'el-icon-document-copy', // the icon show in the sidebar
        policy: 'AbpTextTemplating.TextTemplates'
      }
    },
    {
      path: '/audit-log',
      component: () => import('@/views/system/index'),
      name: 'audit-log-module',
      meta: {
        title: 'AbpAuditLogging["Menu:AuditLogging"]',
        icon: 'el-icon-document',
        policy: ''
      },
      children: [
        {
          path: 'audit-log/list',
          component: () => import('@/views/system/auditing/index'),
          name: 'audit-log',
          meta: {
            title: 'AbpAuditLogging["Menu:AuditLogging"]',
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
      path: 'notice/index',
      component: () => import('@/views/system/notice/index'),
      name: 'notice-list',
      meta: {
        title: 'TigerUi["Menu:Notice"]',
        icon: 'el-icon-message-solid' // the icon show in the sidebar
        // policy: '11'
      },
      children: [
        {
          path: 'notice/list',
          component: () => import('@/views/system/notice/notification/index'),
          name: 'notice-list',
          meta: {
            title: 'TigerUi["Menu:Notice"]',
            icon: 'el-icon-message' // the icon show in the sidebar
            // policy: '11'
          }
        },
        {
          path: '/notice/my-notification/list',
          name: 'my-notification',
          component: () => import('@/views/system/notice/my-notification/index'),
          meta: {
            title: '我的消息',
            icon: 'el-icon-s-comment'
            // policy: 'SettingUi.ShowSettingPage'
          }
        },
        {
          path: '/notice/my-subcribe/list',
          name: 'my-notification',
          component: () => import('@/views/system/notice/my-subscribe/index'),
          meta: {
            title: '我的订阅',
            icon: 'el-icon-bell'
            // policy: 'SettingUi.ShowSettingPage'
          }
        }
      ]
    },

    // 运维监控
    {
      path: 'monitor/index',
      component: () => import('@/views/system/monitor/index'),
      name: 'monitor',
      meta: {
        title: 'TigerUi["Menu:Monitor"]',
        icon: 'el-icon-monitor',
        policy: 'SettingUi.ShowSettingPage'
      },
      children: [
        {
          path: '/monitor/server/list',
          name: 'server-monitoring',
          component: () => import('@/views/system/monitor/server/index'),
          meta: {
            title: 'TigerUi["Menu:ServerMonitor"]',
            icon: 'el-icon-s-platform',
            policy: 'SettingUi.ShowSettingPage'
          }
        },
        {
          path: '/cache/index',
          name: 'cache-monitoring',
          component: () => import('@/views/system/monitor/cache/index'),
          meta: {
            title: 'TigerUi["Menu:CacheMonitor"]',
            icon: 'pc-monitor',
            policy: 'SettingUi.ShowSettingPage'
          }
        },
        {
          path: '/cache/manage',
          name: 'cache-manage',
          component: () => import('@/views/system/monitor/cache/manage'),
          meta: {
            title: 'TigerUi["Menu:CacheManage"]',
            icon: 'Redis',
            policy: 'SettingUi.ShowSettingPage'
          }
        },
        {
          path: '/cache/table',
          name: 'cache-table',
          component: () => import('@/views/system/monitor/cache/table'),
          meta: {
            title: 'TigerUi["Menu:CacheTable"]',
            icon: 'Redis',
            policy: 'SettingUi.ShowSettingPage'
          }
        }
      ]
    },
    // 基础设施
    {
      path: '/infrastructure',
      component: () => import('@/views/system/infrastructure/index'),
      name: 'infrastructure',
      meta: {
        title: 'TigerUi["Menu:Infrastructure"]',
        icon: 'el-icon-user-solid',
        policy: ''
      },
      children: [
        {
          path: 'swagger-api/index',
          component: () => import('@/views/system/infrastructure/swagger-api/index'),
          name: 'swagger-api',
          meta: {
            title: 'TigerUi["Menu:SwaggerApi"]',
            icon: 'el-icon-notebook-1',
            policy: 'SettingUi.ShowSettingPage'
          }
        },
        {
          path: 'https://localhost:44306/hangfire/?period=week',
          component: Layout,
          name: 'background-job',
          meta: {
            title: 'TigerUi["Menu:BackgroundJob"]',
            icon: 'el-icon-message',
            policy: 'SettingUi.ShowSettingPage'
          }
        },
        {
          path: 'quartz/index',
          component: () => import('@/views/system/infrastructure/crystal-quartz/index'),
          name: 'background-worker-crystal',
          meta: {
            title: 'TigerUi["Menu:BackgroundWorkerCrystal"]',
            icon: 'el-icon-timer',
            policy: 'SettingUi.ShowSettingPage'
          }
        },
        {
          path: 'https://easyabp.io/abphelper/AbpHelper.GUI/#getting-started',
          component: Layout,
          name: 'code-generation',
          meta: {
            title: 'TigerUi["Menu:CodeGeneration"]',
            icon: 'el-icon-notebook-1',
            policy: 'SettingUi.ShowSettingPage'
          }
        },
        {
          path: '/dev-tool/vfrom2',
          component: () => import('@/views/system/infrastructure/vfrom2/index'),
          name: 'vfrom',
          meta: {
            title: 'TigerUi["Menu:Vfrom"]',
            icon: 'el-icon-notebook-1',
            policy: 'SettingUi.ShowSettingPage'
          }
        }

      ]
    },
    {
      path: 'setting/index',
      component: () => import('@/views/system/setting/index'),
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
