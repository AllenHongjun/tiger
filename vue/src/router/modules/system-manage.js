import Layout from '@/layout'

const SystemManageRouter = {
  path: '/system-manage',
  component: Layout,
  alwaysShow: true,
  name: 'Setting',
  meta: {
    title: '管理',
    icon: 'el-icon-setting',
    policy: 'AbpIdentity.Users'
  },
  children: [
    {
      path: '/identity',
      component: () => import('@/views/system-manage/index'), // Parent router-view
      name: 'identity',
      redirect: '/user/list',
      meta: {
        title: '身份认证',
        icon: 'el-icon-user-solid'
      },
      children: [
        {
          path: 'organization-units/list',
          component: () => import('@/views/system-manage/identity/organization-units/index'),
          name: 'organization-units',
          meta: { title: '组织机构', policy: 'AbpIdentity.Roles' }
        },
        {
          path: 'organization-units/list-2',
          component: () => import('@/views/system-manage/identity/organization-units/index-2'),
          name: 'organization-units',
          meta: { title: '组织机构2', policy: 'AbpIdentity.Roles' }
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
          meta: { title: '声明类型', policy: 'AbpIdentity.Users' }
        },

        {
          path: '/security-log/list',
          component: () => import('@/views/system-manage/identity/security-logs/index'),
          name: 'security-log',
          meta: { title: '安全日志', }
        }
      ]

    },
    {
      path: 'audit-log/list',
      component: () => import('@/views/system-manage/auditing/index'), // Parent router-view
      name: 'audit-log-list',
      meta: {
        // roles: ['admin','editor'],    //control the page roles (you can set multiple roles)
        title: '审计日志',               //the name show in sidebar and breadcrumb (recommend set)
        icon: 'el-icon-document', //the icon show in the sidebar
        // breadcrumb: false,            //if set false, the item will hidden in breadcrumb(default is true)
        // activeMenu: '/example/list'  //if set path, the sidebar will highlight the path you set
      }
    },
    {
      path: 'setting/index',
      component: () => import('@/views/system-manage/setting/index'), // Parent router-view
      name: 'system',
      meta: {
        title: '系统设置',
        icon: 'el-icon-setting'
      }
    },
    // 添加外链
    {
      path: 'https://localhost:44306/index.html',
      component: Layout,
      name: 'swagger-api',
      meta: {
        title: '接口文档',
        icon: 'el-icon-notebook-1'
      }
    },
    {
      path: 'https://localhost:44306/hangfire/',
      component: Layout,
      url: "http://www.baidu.com",
      name: 'background-job',
      meta: {
        title: '后台作业',
        icon: 'el-icon-message'
      }
    },
    {
      path: 'https://localhost:44306/quartz',
      component: Layout,
      name: 'background-worker',
      meta: {
        title: '定时任务',
        icon: 'el-icon-timer'
      }
    }

  ]
}

export default SystemManageRouter
