<template>
  <div class="app-container">
    <div class="filter-container">
      <el-row style="margin-bottom: 20px">
        <el-input v-model="listQuery.filter" style="width: 200px;" class="filter-item" :placeholder="$t('AbpUi[\'PagerSearch\']')" @keyup.enter.native="handleFilter" />
        <el-select v-model="listQuery.status" placeholder="状态" clearable style="width:130px;" class="filter-item" @clear="listQuery.status=undefined">
          <el-option label="正常" value="1" />
          <el-option label="禁用" value="0" />
        </el-select>

        <el-button-group class="filter-item">
          <el-button type="primary" icon="el-icon-search" @click="handleFilter">搜索</el-button>
          <el-button type="reset" icon="el-icon-remove-outline" @click="resetQueryForm">重置</el-button>
          <el-button icon="el-icon-refresh" @click="handleRefresh">刷新</el-button>
        </el-button-group>
        <el-button type="primary" icon="el-icon-plus" class="filter-item" style="margin-left: 10px;" @click="handleCreate">创建</el-button>
        <el-button type="primary" icon="el-icon-sort" class="filter-item" style="margin-left: 10px;" @click="toggleRowExpansion">全部{{ isExpansion ? "收缩" : "展开" }}</el-button>
      </el-row>
    </div>

    <el-table ref="dataTreeList" :data="tableData" style="width: 100%;margin-bottom: 20px;" row-key="id" border :default-expand="false" :tree-props="{children: 'children', hasChildren: 'hasChildren'}">
      <el-table-column prop="title" label="菜单名称" sortable width="180" />
      <el-table-column prop="icon" label="图标" sortable width="180">
        <template slot-scope="{row}">
          <!-- 绑定属性的值 在属性上不需要添加大括号 -->
          <i :class="row.icon" />
          <span>{{ row.icon }}</span>
        </template>

      </el-table-column>
      <el-table-column prop="type" label="类型" sortable width="180" />
      <el-table-column prop="path" label="路由路径" />
      <el-table-column prop="component" label="组件路径" />
      <el-table-column prop="permission" label="权限标识" />
      <el-table-column prop="orderNo" label="排序" />
      <el-table-column prop="status" label="状态">
        <template slot-scope="{row}">
          <el-tag :type="row.status | statusFilter">
            {{ row.status == '1' ? '正常' : '禁用' }}
          </el-tag>
        </template>
      </el-table-column>
      <el-table-column prop="updateTime" label="修改时间" />
      <el-table-column align="left" :label="$t('AbpIdentity[\'Actions\']')" width="320">
        <template slot-scope="scope">
          <el-button v-if="checkPermission('AbpIdentity.Roles.Update')" type="primary" @click="handleUpdate()">
            {{ $t("AbpIdentity['Edit']") }}
          </el-button>

          <el-button v-if="!scope.row.isStatic && checkPermission('AbpIdentity.Roles.Delete')" type="danger">
            {{ $t("AbpIdentity['Delete']") }}
          </el-button>

        </template>
      </el-table-column>
    </el-table>

    <el-dialog :title="dialogStatus == 'create'? '创建': $t('AbpIdentity[\'Edit\']')" :visible.sync="dialogFormVisible">

      <el-row>
        <el-form ref="dataForm" :model="dataForm" :rules="rules" label-width="100px">

          <el-form-item label="上级菜单" prop="pid" width="400px">
            <el-cascader v-model="value" placeholder="请选择上级菜单" :options="options" @change="handleChange" />
          </el-form-item>

          <el-form-item label="菜单类型" prop="resource">
            <el-radio-group v-model="dataForm.resource">
              <el-radio label="目录" />
              <el-radio label="菜单" />
              <el-radio label="按钮" />
            </el-radio-group>
          </el-form-item>

          <el-row>
            <el-col :span="12">
              <div class="grid-content bg-purple">
                <el-form-item label="菜单名称" placeholder="菜单名称" prop="name">
                  <el-input v-model="dataForm.name" />
                </el-form-item>
              </div>
            </el-col>
            <el-col :span="12">
              <div class="grid-content bg-purple-light">
                <el-form-item label="路由名称" aria-placeholder="路由名称" prop="pathName">
                  <el-input v-model="dataForm.pathName" />
                </el-form-item>
              </div>
            </el-col>
          </el-row>

          <el-row>
            <el-col :span="12">
              <div class="grid-content bg-purple">
                <el-form-item label="路由路径" placeholder="路由路径" prop="path">
                  <el-input v-model="dataForm.path" />
                </el-form-item>
              </div>
            </el-col>
            <el-col :span="12">
              <div class="grid-content bg-purple-light">
                <el-form-item label="组件路径" aria-placeholder="组件路径" prop="component">
                  <el-input v-model="dataForm.component" />
                </el-form-item>
              </div>
            </el-col>
          </el-row>

          <el-row>
            <el-col :span="12">
              <div class="grid-content bg-purple">
                <el-form-item label="菜单图标" placeholder="菜单图标" prop="icon">
                  <e-icon-picker v-model="dataForm.icon" />
                </el-form-item>
              </div>
            </el-col>
            <el-col :span="12">
              <div class="grid-content bg-purple-light">
                <el-form-item label="重定向" aria-placeholder="重定向" prop="redirect">
                  <el-input v-model="dataForm.redirect" />
                </el-form-item>
              </div>
            </el-col>
          </el-row>

          <el-row>
            <el-col :span="12">
              <div class="grid-content bg-purple">
                <el-form-item label="链接地址" placeholder="链接地址" prop="outLink">
                  <el-input v-model="dataForm.outLink" />
                </el-form-item>
              </div>
            </el-col>
            <el-col :span="12">
              <div class="grid-content bg-purple-light">
                <el-form-item label="菜单排序" aria-placeholder="菜单排序" prop="orderNo">
                  <el-input-number v-model="num" :min="1" :max="10000000" label="菜单排序" />
                </el-form-item>
              </div>
            </el-col>
          </el-row>

          <el-row>
            <el-col :span="12">
              <div class="grid-content bg-purple">
                <el-form-item label="状态" prop="status">
                  <template>
                    <el-radio v-model="dataForm.status" :label="1">正常</el-radio>
                    <el-radio v-model="dataForm.status" :label="0">禁用</el-radio>
                  </template>
                </el-form-item>
              </div>
            </el-col>
            <el-col :span="12">
              <div class="grid-content bg-purple-light">
                <el-form-item label="固定" prop="isAffix">
                  <!-- label绑定数字和bool 前面需要加: https://blog.csdn.net/a772116804/article/details/127230949 -->
                  <el-radio v-model="dataForm.isAffix" :label="true">是</el-radio>
                  <el-radio v-model="dataForm.isAffix" :label="false">否</el-radio>
                </el-form-item>
              </div>
            </el-col>
          </el-row>

          <el-form-item label="备注" prop="remark">
            <el-input v-model="dataForm.remark" type="textarea" />
          </el-form-item>
        </el-form>
      </el-row>
      <div style="text-align: right">
        <!-- <el-button @click="resetForm('dataForm')">重置</el-button> -->
        <el-button type="danger" @click="dialogFormVisible = false">{{ $t("AbpIdentity['Cancel']") }}</el-button>
        <el-button type="primary" @click="dialogStatus === 'create' ? createData() : updateData()">{{ $t("AbpIdentity['Save']") }}</el-button>
      </div>
    </el-dialog>

  </div>
</template>

<script>
import { EIcon, EIconPicker } from 'e-icon-picker'
import {
  baseListQuery,
  checkPermission
} from '@/utils/abp'

export default {
  name: 'Menu',
  components: { EIconPicker, EIcon },
  filters: {
    statusFilter(status) {
      const statusMap = {
        1: 'success',
        0: 'danger'
      }
      return statusMap[status]
    }
  },
  data() {
    return {
      value: [],
      options: [{
        value: 'zhinan',
        label: '指南',
        children: [{
          value: 'shejiyuanze',
          label: '设计原则',
          children: [{
            value: 'yizhi',
            label: '一致'
          }, {
            value: 'fankui',
            label: '反馈'
          }, {
            value: 'xiaolv',
            label: '效率'
          }, {
            value: 'kekong',
            label: '可控'
          }]
        }, {
          value: 'daohang',
          label: '导航',
          children: [{
            value: 'cexiangdaohang',
            label: '侧向导航'
          }, {
            value: 'dingbudaohang',
            label: '顶部导航'
          }]
        }]
      }, {
        value: 'zujian',
        label: '组件',
        children: [{
          value: 'basic',
          label: 'Basic',
          children: [{
            value: 'layout',
            label: 'Layout 布局'
          }, {
            value: 'color',
            label: 'Color 色彩'
          }, {
            value: 'typography',
            label: 'Typography 字体'
          }, {
            value: 'icon',
            label: 'Icon 图标'
          }, {
            value: 'button',
            label: 'Button 按钮'
          }]
        }, {
          value: 'form',
          label: 'Form',
          children: [{
            value: 'radio',
            label: 'Radio 单选框'
          }, {
            value: 'checkbox',
            label: 'Checkbox 多选框'
          }, {
            value: 'input',
            label: 'Input 输入框'
          }, {
            value: 'input-number',
            label: 'InputNumber 计数器'
          }, {
            value: 'select',
            label: 'Select 选择器'
          }, {
            value: 'cascader',
            label: 'Cascader 级联选择器'
          }, {
            value: 'switch',
            label: 'Switch 开关'
          }, {
            value: 'slider',
            label: 'Slider 滑块'
          }, {
            value: 'time-picker',
            label: 'TimePicker 时间选择器'
          }, {
            value: 'date-picker',
            label: 'DatePicker 日期选择器'
          }, {
            value: 'datetime-picker',
            label: 'DateTimePicker 日期时间选择器'
          }, {
            value: 'upload',
            label: 'Upload 上传'
          }, {
            value: 'rate',
            label: 'Rate 评分'
          }, {
            value: 'form',
            label: 'Form 表单'
          }]
        }, {
          value: 'data',
          label: 'Data',
          children: [{
            value: 'table',
            label: 'Table 表格'
          }, {
            value: 'tag',
            label: 'Tag 标签'
          }, {
            value: 'progress',
            label: 'Progress 进度条'
          }, {
            value: 'tree',
            label: 'Tree 树形控件'
          }, {
            value: 'pagination',
            label: 'Pagination 分页'
          }, {
            value: 'badge',
            label: 'Badge 标记'
          }]
        }, {
          value: 'notice',
          label: 'Notice',
          children: [{
            value: 'alert',
            label: 'Alert 警告'
          }, {
            value: 'loading',
            label: 'Loading 加载'
          }, {
            value: 'message',
            label: 'Message 消息提示'
          }, {
            value: 'message-box',
            label: 'MessageBox 弹框'
          }, {
            value: 'notification',
            label: 'Notification 通知'
          }]
        }, {
          value: 'navigation',
          label: 'Navigation',
          children: [{
            value: 'menu',
            label: 'NavMenu 导航菜单'
          }, {
            value: 'tabs',
            label: 'Tabs 标签页'
          }, {
            value: 'breadcrumb',
            label: 'Breadcrumb 面包屑'
          }, {
            value: 'dropdown',
            label: 'Dropdown 下拉菜单'
          }, {
            value: 'steps',
            label: 'Steps 步骤条'
          }]
        }, {
          value: 'others',
          label: 'Others',
          children: [{
            value: 'dialog',
            label: 'Dialog 对话框'
          }, {
            value: 'tooltip',
            label: 'Tooltip 文字提示'
          }, {
            value: 'popover',
            label: 'Popover 弹出框'
          }, {
            value: 'card',
            label: 'Card 卡片'
          }, {
            value: 'carousel',
            label: 'Carousel 走马灯'
          }, {
            value: 'collapse',
            label: 'Collapse 折叠面板'
          }]
        }]
      }, {
        value: 'ziyuan',
        label: '资源',
        children: [{
          value: 'axure',
          label: 'Axure Components'
        }, {
          value: 'sketch',
          label: 'Sketch Templates'
        }, {
          value: 'jiaohu',
          label: '组件交互文档'
        }]
      }],
      tableData: [{
        'pid': 0,
        'type': 1,
        'name': 'dashboard',
        'path': '/dashboard',
        'component': 'Layout',
        'redirect': '/dashboard/home',
        'permission': null,
        'title': '工作台',
        'icon': 'ele-HomeFilled',
        'isIframe': false,
        'outLink': null,
        'isHide': false,
        'isKeepAlive': true,
        'isAffix': false,
        'orderNo': 0,
        'status': 1,
        'remark': null,
        'children': [{
          'pid': 1300000000101,
          'type': 2,
          'name': 'home',
          'path': '/dashboard/home',
          'component': '/home/index',
          'redirect': null,
          'permission': null,
          'title': '工作台',
          'icon': 'ele-HomeFilled',
          'isIframe': false,
          'outLink': null,
          'isHide': false,
          'isKeepAlive': true,
          'isAffix': true,
          'orderNo': 100,
          'status': 1,
          'remark': null,
          'children': [],
          'createTime': '2023-03-22 09:58:07',
          'updateTime': null,
          'createUserId': null,
          'updateUserId': null,
          'isDelete': false,
          'id': 1300000000111
        },
        {
          'pid': 1300000000101,
          'type': 2,
          'name': 'notice',
          'path': '/dashboard/notice',
          'component': '/home/notice/index',
          'redirect': null,
          'permission': null,
          'title': '站内信',
          'icon': 'ele-Bell',
          'isIframe': false,
          'outLink': null,
          'isHide': false,
          'isKeepAlive': true,
          'isAffix': false,
          'orderNo': 101,
          'status': 1,
          'remark': null,
          'children': [],
          'createTime': '2023-03-22 09:58:07',
          'updateTime': null,
          'createUserId': null,
          'updateUserId': null,
          'isDelete': false,
          'id': 1300000000121
        }
        ],
        'createTime': '2023-03-22 09:58:07',
        'updateTime': null,
        'createUserId': null,
        'updateUserId': null,
        'isDelete': false,
        'id': 1300000000101
      },
      {
        'pid': 0,
        'type': 1,
        'name': 'system',
        'path': '/system',
        'component': 'Layout',
        'redirect': '/system/user',
        'permission': null,
        'title': '系统管理',
        'icon': 'ele-Setting',
        'isIframe': false,
        'outLink': null,
        'isHide': false,
        'isKeepAlive': true,
        'isAffix': false,
        'orderNo': 1000,
        'status': 1,
        'remark': null,
        'children': [{
          'pid': 1310000000101,
          'type': 2,
          'name': 'sysUser',
          'path': '/system/user',
          'component': '/system/user/index',
          'redirect': null,
          'permission': null,
          'title': '账号管理',
          'icon': 'ele-User',
          'isIframe': false,
          'outLink': null,
          'isHide': false,
          'isKeepAlive': true,
          'isAffix': false,
          'orderNo': 100,
          'status': 1,
          'remark': null,
          'children': [{
            'pid': 1310000000111,
            'type': 3,
            'name': null,
            'path': null,
            'component': null,
            'redirect': null,
            'permission': 'sysUser:page',
            'title': '查询',
            'icon': null,
            'isIframe': false,
            'outLink': null,
            'isHide': false,
            'isKeepAlive': true,
            'isAffix': false,
            'orderNo': 100,
            'status': 1,
            'remark': null,
            'children': [],
            'createTime': '2023-03-22 09:58:07',
            'updateTime': null,
            'createUserId': null,
            'updateUserId': null,
            'isDelete': false,
            'id': 1310000000112
          },
          {
            'pid': 1310000000111,
            'type': 3,
            'name': null,
            'path': null,
            'component': null,
            'redirect': null,
            'permission': 'sysUser:update',
            'title': '编辑',
            'icon': null,
            'isIframe': false,
            'outLink': null,
            'isHide': false,
            'isKeepAlive': true,
            'isAffix': false,
            'orderNo': 100,
            'status': 1,
            'remark': null,
            'children': [],
            'createTime': '2023-03-22 09:58:07',
            'updateTime': null,
            'createUserId': null,
            'updateUserId': null,
            'isDelete': false,
            'id': 1310000000113
          },
          {
            'pid': 1310000000111,
            'type': 3,
            'name': null,
            'path': null,
            'component': null,
            'redirect': null,
            'permission': 'sysUser:add',
            'title': '增加',
            'icon': null,
            'isIframe': false,
            'outLink': null,
            'isHide': false,
            'isKeepAlive': true,
            'isAffix': false,
            'orderNo': 100,
            'status': 1,
            'remark': null,
            'children': [],
            'createTime': '2023-03-22 09:58:07',
            'updateTime': null,
            'createUserId': null,
            'updateUserId': null,
            'isDelete': false,
            'id': 1310000000114
          },
          {
            'pid': 1310000000111,
            'type': 3,
            'name': null,
            'path': null,
            'component': null,
            'redirect': null,
            'permission': 'sysUser:delete',
            'title': '删除',
            'icon': null,
            'isIframe': false,
            'outLink': null,
            'isHide': false,
            'isKeepAlive': true,
            'isAffix': false,
            'orderNo': 100,
            'status': 1,
            'remark': null,
            'children': [],
            'createTime': '2023-03-22 09:58:07',
            'updateTime': null,
            'createUserId': null,
            'updateUserId': null,
            'isDelete': false,
            'id': 1310000000115
          },
          {
            'pid': 1310000000111,
            'type': 3,
            'name': null,
            'path': null,
            'component': null,
            'redirect': null,
            'permission': 'sysUser:detail',
            'title': '详情',
            'icon': null,
            'isIframe': false,
            'outLink': null,
            'isHide': false,
            'isKeepAlive': true,
            'isAffix': false,
            'orderNo': 100,
            'status': 1,
            'remark': null,
            'children': [],
            'createTime': '2023-03-22 09:58:07',
            'updateTime': null,
            'createUserId': null,
            'updateUserId': null,
            'isDelete': false,
            'id': 1310000000116
          },
          {
            'pid': 1310000000111,
            'type': 3,
            'name': null,
            'path': null,
            'component': null,
            'redirect': null,
            'permission': 'sysUser:grantRole',
            'title': '授权角色',
            'icon': null,
            'isIframe': false,
            'outLink': null,
            'isHide': false,
            'isKeepAlive': true,
            'isAffix': false,
            'orderNo': 100,
            'status': 1,
            'remark': null,
            'children': [],
            'createTime': '2023-03-22 09:58:07',
            'updateTime': null,
            'createUserId': null,
            'updateUserId': null,
            'isDelete': false,
            'id': 1310000000117
          },
          {
            'pid': 1310000000111,
            'type': 3,
            'name': null,
            'path': null,
            'component': null,
            'redirect': null,
            'permission': 'sysUser:resetPwd',
            'title': '重置密码',
            'icon': null,
            'isIframe': false,
            'outLink': null,
            'isHide': false,
            'isKeepAlive': true,
            'isAffix': false,
            'orderNo': 100,
            'status': 1,
            'remark': null,
            'children': [],
            'createTime': '2023-03-22 09:58:07',
            'updateTime': null,
            'createUserId': null,
            'updateUserId': null,
            'isDelete': false,
            'id': 1310000000118
          },
          {
            'pid': 1310000000111,
            'type': 3,
            'name': null,
            'path': null,
            'component': null,
            'redirect': null,
            'permission': 'sysUser:setStatus',
            'title': '设置状态',
            'icon': null,
            'isIframe': false,
            'outLink': null,
            'isHide': false,
            'isKeepAlive': true,
            'isAffix': false,
            'orderNo': 100,
            'status': 1,
            'remark': null,
            'children': [],
            'createTime': '2023-03-22 09:58:07',
            'updateTime': null,
            'createUserId': null,
            'updateUserId': null,
            'isDelete': false,
            'id': 1310000000119
          },
          {
            'pid': 1310000000111,
            'type': 3,
            'name': null,
            'path': null,
            'component': null,
            'redirect': null,
            'permission': 'sysOnlineUser:forceOffline',
            'title': '强制下线',
            'icon': null,
            'isIframe': false,
            'outLink': null,
            'isHide': false,
            'isKeepAlive': true,
            'isAffix': false,
            'orderNo': 100,
            'status': 1,
            'remark': null,
            'children': [],
            'createTime': '2023-03-22 09:58:07',
            'updateTime': null,
            'createUserId': null,
            'updateUserId': null,
            'isDelete': false,
            'id': 1310000000120
          }
          ],
          'createTime': '2023-03-22 09:58:07',
          'updateTime': null,
          'createUserId': null,
          'updateUserId': null,
          'isDelete': false,
          'id': 1310000000111
        },
        {
          'pid': 1310000000101,
          'type': 2,
          'name': 'sysRole',
          'path': '/system/role',
          'component': '/system/role/index',
          'redirect': null,
          'permission': null,
          'title': '角色管理',
          'icon': 'ele-Help',
          'isIframe': false,
          'outLink': null,
          'isHide': false,
          'isKeepAlive': true,
          'isAffix': false,
          'orderNo': 110,
          'status': 1,
          'remark': null,
          'children': [{
            'pid': 1310000000131,
            'type': 3,
            'name': null,
            'path': null,
            'component': null,
            'redirect': null,
            'permission': 'sysRole:page',
            'title': '查询',
            'icon': null,
            'isIframe': false,
            'outLink': null,
            'isHide': false,
            'isKeepAlive': true,
            'isAffix': false,
            'orderNo': 100,
            'status': 1,
            'remark': null,
            'children': [],
            'createTime': '2023-03-22 09:58:07',
            'updateTime': null,
            'createUserId': null,
            'updateUserId': null,
            'isDelete': false,
            'id': 1310000000132
          },
          {
            'pid': 1310000000131,
            'type': 3,
            'name': null,
            'path': null,
            'component': null,
            'redirect': null,
            'permission': 'sysRole:update',
            'title': '编辑',
            'icon': null,
            'isIframe': false,
            'outLink': null,
            'isHide': false,
            'isKeepAlive': true,
            'isAffix': false,
            'orderNo': 100,
            'status': 1,
            'remark': null,
            'children': [],
            'createTime': '2023-03-22 09:58:07',
            'updateTime': null,
            'createUserId': null,
            'updateUserId': null,
            'isDelete': false,
            'id': 1310000000133
          },
          {
            'pid': 1310000000131,
            'type': 3,
            'name': null,
            'path': null,
            'component': null,
            'redirect': null,
            'permission': 'sysRole:add',
            'title': '增加',
            'icon': null,
            'isIframe': false,
            'outLink': null,
            'isHide': false,
            'isKeepAlive': true,
            'isAffix': false,
            'orderNo': 100,
            'status': 1,
            'remark': null,
            'children': [],
            'createTime': '2023-03-22 09:58:07',
            'updateTime': null,
            'createUserId': null,
            'updateUserId': null,
            'isDelete': false,
            'id': 1310000000134
          },
          {
            'pid': 1310000000131,
            'type': 3,
            'name': null,
            'path': null,
            'component': null,
            'redirect': null,
            'permission': 'sysRole:delete',
            'title': '删除',
            'icon': null,
            'isIframe': false,
            'outLink': null,
            'isHide': false,
            'isKeepAlive': true,
            'isAffix': false,
            'orderNo': 100,
            'status': 1,
            'remark': null,
            'children': [],
            'createTime': '2023-03-22 09:58:07',
            'updateTime': null,
            'createUserId': null,
            'updateUserId': null,
            'isDelete': false,
            'id': 1310000000135
          },
          {
            'pid': 1310000000131,
            'type': 3,
            'name': null,
            'path': null,
            'component': null,
            'redirect': null,
            'permission': 'sysRole:grantMenu',
            'title': '授权菜单',
            'icon': null,
            'isIframe': false,
            'outLink': null,
            'isHide': false,
            'isKeepAlive': true,
            'isAffix': false,
            'orderNo': 100,
            'status': 1,
            'remark': null,
            'children': [],
            'createTime': '2023-03-22 09:58:07',
            'updateTime': null,
            'createUserId': null,
            'updateUserId': null,
            'isDelete': false,
            'id': 1310000000136
          },
          {
            'pid': 1310000000131,
            'type': 3,
            'name': null,
            'path': null,
            'component': null,
            'redirect': null,
            'permission': 'sysRole:grantDataScope',
            'title': '授权数据',
            'icon': null,
            'isIframe': false,
            'outLink': null,
            'isHide': false,
            'isKeepAlive': true,
            'isAffix': false,
            'orderNo': 100,
            'status': 1,
            'remark': null,
            'children': [],
            'createTime': '2023-03-22 09:58:07',
            'updateTime': null,
            'createUserId': null,
            'updateUserId': null,
            'isDelete': false,
            'id': 1310000000137
          },
          {
            'pid': 1310000000131,
            'type': 3,
            'name': null,
            'path': null,
            'component': null,
            'redirect': null,
            'permission': 'sysRole:setStatus',
            'title': '设置状态',
            'icon': null,
            'isIframe': false,
            'outLink': null,
            'isHide': false,
            'isKeepAlive': true,
            'isAffix': false,
            'orderNo': 100,
            'status': 1,
            'remark': null,
            'children': [],
            'createTime': '2023-03-22 09:58:07',
            'updateTime': null,
            'createUserId': null,
            'updateUserId': null,
            'isDelete': false,
            'id': 1310000000138
          },
          {
            'pid': 1310000000131,
            'type': 3,
            'name': null,
            'path': null,
            'component': null,
            'redirect': null,
            'permission': 'sysTenant:setStatus',
            'title': '设置状态',
            'icon': null,
            'isIframe': false,
            'outLink': null,
            'isHide': false,
            'isKeepAlive': true,
            'isAffix': false,
            'orderNo': 100,
            'status': 1,
            'remark': null,
            'children': [],
            'createTime': '2023-03-22 09:58:07',
            'updateTime': null,
            'createUserId': null,
            'updateUserId': null,
            'isDelete': false,
            'id': 1310000000319
          }
          ],
          'createTime': '2023-03-22 09:58:07',
          'updateTime': null,
          'createUserId': null,
          'updateUserId': null,
          'isDelete': false,
          'id': 1310000000131
        },
        {
          'pid': 1310000000101,
          'type': 2,
          'name': 'sysOrg',
          'path': '/system/org',
          'component': '/system/org/index',
          'redirect': null,
          'permission': null,
          'title': '机构管理',
          'icon': 'ele-OfficeBuilding',
          'isIframe': false,
          'outLink': null,
          'isHide': false,
          'isKeepAlive': true,
          'isAffix': false,
          'orderNo': 120,
          'status': 1,
          'remark': null,
          'children': [{
            'pid': 1310000000141,
            'type': 3,
            'name': null,
            'path': null,
            'component': null,
            'redirect': null,
            'permission': 'sysOrg:list',
            'title': '查询',
            'icon': null,
            'isIframe': false,
            'outLink': null,
            'isHide': false,
            'isKeepAlive': true,
            'isAffix': false,
            'orderNo': 100,
            'status': 1,
            'remark': null,
            'children': [],
            'createTime': '2023-03-22 09:58:07',
            'updateTime': null,
            'createUserId': null,
            'updateUserId': null,
            'isDelete': false,
            'id': 1310000000142
          },
          {
            'pid': 1310000000141,
            'type': 3,
            'name': null,
            'path': null,
            'component': null,
            'redirect': null,
            'permission': 'sysOrg:update',
            'title': '编辑',
            'icon': null,
            'isIframe': false,
            'outLink': null,
            'isHide': false,
            'isKeepAlive': true,
            'isAffix': false,
            'orderNo': 100,
            'status': 1,
            'remark': null,
            'children': [],
            'createTime': '2023-03-22 09:58:07',
            'updateTime': null,
            'createUserId': null,
            'updateUserId': null,
            'isDelete': false,
            'id': 1310000000143
          },
          {
            'pid': 1310000000141,
            'type': 3,
            'name': null,
            'path': null,
            'component': null,
            'redirect': null,
            'permission': 'sysOrg:add',
            'title': '增加',
            'icon': null,
            'isIframe': false,
            'outLink': null,
            'isHide': false,
            'isKeepAlive': true,
            'isAffix': false,
            'orderNo': 100,
            'status': 1,
            'remark': null,
            'children': [],
            'createTime': '2023-03-22 09:58:07',
            'updateTime': null,
            'createUserId': null,
            'updateUserId': null,
            'isDelete': false,
            'id': 1310000000144
          },
          {
            'pid': 1310000000141,
            'type': 3,
            'name': null,
            'path': null,
            'component': null,
            'redirect': null,
            'permission': 'sysOrg:delete',
            'title': '删除',
            'icon': null,
            'isIframe': false,
            'outLink': null,
            'isHide': false,
            'isKeepAlive': true,
            'isAffix': false,
            'orderNo': 100,
            'status': 1,
            'remark': null,
            'children': [],
            'createTime': '2023-03-22 09:58:07',
            'updateTime': null,
            'createUserId': null,
            'updateUserId': null,
            'isDelete': false,
            'id': 1310000000145
          }
          ],
          'createTime': '2023-03-22 09:58:07',
          'updateTime': null,
          'createUserId': null,
          'updateUserId': null,
          'isDelete': false,
          'id': 1310000000141
        },
        {
          'pid': 1310000000101,
          'type': 2,
          'name': 'sysPos',
          'path': '/system/pos',
          'component': '/system/pos/index',
          'redirect': null,
          'permission': null,
          'title': '职位管理',
          'icon': 'ele-Mug',
          'isIframe': false,
          'outLink': null,
          'isHide': false,
          'isKeepAlive': true,
          'isAffix': false,
          'orderNo': 130,
          'status': 1,
          'remark': null,
          'children': [{
            'pid': 1310000000151,
            'type': 3,
            'name': null,
            'path': null,
            'component': null,
            'redirect': null,
            'permission': 'sysPos:list',
            'title': '查询',
            'icon': null,
            'isIframe': false,
            'outLink': null,
            'isHide': false,
            'isKeepAlive': true,
            'isAffix': false,
            'orderNo': 100,
            'status': 1,
            'remark': null,
            'children': [],
            'createTime': '2023-03-22 09:58:07',
            'updateTime': null,
            'createUserId': null,
            'updateUserId': null,
            'isDelete': false,
            'id': 1310000000152
          },
          {
            'pid': 1310000000151,
            'type': 3,
            'name': null,
            'path': null,
            'component': null,
            'redirect': null,
            'permission': 'sysPos:update',
            'title': '编辑',
            'icon': null,
            'isIframe': false,
            'outLink': null,
            'isHide': false,
            'isKeepAlive': true,
            'isAffix': false,
            'orderNo': 100,
            'status': 1,
            'remark': null,
            'children': [],
            'createTime': '2023-03-22 09:58:07',
            'updateTime': null,
            'createUserId': null,
            'updateUserId': null,
            'isDelete': false,
            'id': 1310000000153
          },
          {
            'pid': 1310000000151,
            'type': 3,
            'name': null,
            'path': null,
            'component': null,
            'redirect': null,
            'permission': 'sysPos:add',
            'title': '增加',
            'icon': null,
            'isIframe': false,
            'outLink': null,
            'isHide': false,
            'isKeepAlive': true,
            'isAffix': false,
            'orderNo': 100,
            'status': 1,
            'remark': null,
            'children': [],
            'createTime': '2023-03-22 09:58:07',
            'updateTime': null,
            'createUserId': null,
            'updateUserId': null,
            'isDelete': false,
            'id': 1310000000154
          },
          {
            'pid': 1310000000151,
            'type': 3,
            'name': null,
            'path': null,
            'component': null,
            'redirect': null,
            'permission': 'sysPos:delete',
            'title': '删除',
            'icon': null,
            'isIframe': false,
            'outLink': null,
            'isHide': false,
            'isKeepAlive': true,
            'isAffix': false,
            'orderNo': 100,
            'status': 1,
            'remark': null,
            'children': [],
            'createTime': '2023-03-22 09:58:07',
            'updateTime': null,
            'createUserId': null,
            'updateUserId': null,
            'isDelete': false,
            'id': 1310000000155
          }
          ],
          'createTime': '2023-03-22 09:58:07',
          'updateTime': null,
          'createUserId': null,
          'updateUserId': null,
          'isDelete': false,
          'id': 1310000000151
        },
        {
          'pid': 1310000000101,
          'type': 2,
          'name': 'sysUserCenter',
          'path': '/system/userCenter',
          'component': '/system/user/component/userCenter',
          'redirect': null,
          'permission': null,
          'title': '个人中心',
          'icon': 'ele-Medal',
          'isIframe': false,
          'outLink': null,
          'isHide': false,
          'isKeepAlive': true,
          'isAffix': false,
          'orderNo': 140,
          'status': 1,
          'remark': null,
          'children': [{
            'pid': 1310000000161,
            'type': 3,
            'name': null,
            'path': null,
            'component': null,
            'redirect': null,
            'permission': 'sysUser:changePwd',
            'title': '修改密码',
            'icon': null,
            'isIframe': false,
            'outLink': null,
            'isHide': false,
            'isKeepAlive': true,
            'isAffix': false,
            'orderNo': 100,
            'status': 1,
            'remark': null,
            'children': [],
            'createTime': '2023-03-22 09:58:07',
            'updateTime': null,
            'createUserId': null,
            'updateUserId': null,
            'isDelete': false,
            'id': 1310000000162
          },
          {
            'pid': 1310000000161,
            'type': 3,
            'name': null,
            'path': null,
            'component': null,
            'redirect': null,
            'permission': 'sysUser:baseInfo',
            'title': '基本信息',
            'icon': null,
            'isIframe': false,
            'outLink': null,
            'isHide': false,
            'isKeepAlive': true,
            'isAffix': false,
            'orderNo': 100,
            'status': 1,
            'remark': null,
            'children': [],
            'createTime': '2023-03-22 09:58:07',
            'updateTime': null,
            'createUserId': null,
            'updateUserId': null,
            'isDelete': false,
            'id': 1310000000163
          },
          {
            'pid': 1310000000161,
            'type': 3,
            'name': null,
            'path': null,
            'component': null,
            'redirect': null,
            'permission': 'sysFile:uploadSignature',
            'title': '电子签名',
            'icon': null,
            'isIframe': false,
            'outLink': null,
            'isHide': false,
            'isKeepAlive': true,
            'isAffix': false,
            'orderNo': 100,
            'status': 1,
            'remark': null,
            'children': [],
            'createTime': '2023-03-22 09:58:07',
            'updateTime': null,
            'createUserId': null,
            'updateUserId': null,
            'isDelete': false,
            'id': 1310000000164
          },
          {
            'pid': 1310000000161,
            'type': 3,
            'name': null,
            'path': null,
            'component': null,
            'redirect': null,
            'permission': 'sysFile:uploadAvatar',
            'title': '上传头像',
            'icon': null,
            'isIframe': false,
            'outLink': null,
            'isHide': false,
            'isKeepAlive': true,
            'isAffix': false,
            'orderNo': 100,
            'status': 1,
            'remark': null,
            'children': [],
            'createTime': '2023-03-22 09:58:07',
            'updateTime': null,
            'createUserId': null,
            'updateUserId': null,
            'isDelete': false,
            'id': 1310000000165
          }
          ],
          'createTime': '2023-03-22 09:58:07',
          'updateTime': null,
          'createUserId': null,
          'updateUserId': null,
          'isDelete': false,
          'id': 1310000000161
        },
        {
          'pid': 1310000000101,
          'type': 2,
          'name': 'sysNotice',
          'path': '/system/notice',
          'component': '/system/notice/index',
          'redirect': null,
          'permission': null,
          'title': '通知公告',
          'icon': 'ele-Bell',
          'isIframe': false,
          'outLink': null,
          'isHide': false,
          'isKeepAlive': true,
          'isAffix': false,
          'orderNo': 150,
          'status': 1,
          'remark': null,
          'children': [{
            'pid': 1310000000171,
            'type': 3,
            'name': null,
            'path': null,
            'component': null,
            'redirect': null,
            'permission': 'sysNotice:page',
            'title': '查询',
            'icon': null,
            'isIframe': false,
            'outLink': null,
            'isHide': false,
            'isKeepAlive': true,
            'isAffix': false,
            'orderNo': 100,
            'status': 1,
            'remark': null,
            'children': [],
            'createTime': '2023-03-22 09:58:07',
            'updateTime': null,
            'createUserId': null,
            'updateUserId': null,
            'isDelete': false,
            'id': 1310000000172
          },
          {
            'pid': 1310000000171,
            'type': 3,
            'name': null,
            'path': null,
            'component': null,
            'redirect': null,
            'permission': 'sysNotice:update',
            'title': '编辑',
            'icon': null,
            'isIframe': false,
            'outLink': null,
            'isHide': false,
            'isKeepAlive': true,
            'isAffix': false,
            'orderNo': 100,
            'status': 1,
            'remark': null,
            'children': [],
            'createTime': '2023-03-22 09:58:07',
            'updateTime': null,
            'createUserId': null,
            'updateUserId': null,
            'isDelete': false,
            'id': 1310000000173
          },
          {
            'pid': 1310000000171,
            'type': 3,
            'name': null,
            'path': null,
            'component': null,
            'redirect': null,
            'permission': 'sysNotice:add',
            'title': '增加',
            'icon': null,
            'isIframe': false,
            'outLink': null,
            'isHide': false,
            'isKeepAlive': true,
            'isAffix': false,
            'orderNo': 100,
            'status': 1,
            'remark': null,
            'children': [],
            'createTime': '2023-03-22 09:58:07',
            'updateTime': null,
            'createUserId': null,
            'updateUserId': null,
            'isDelete': false,
            'id': 1310000000174
          },
          {
            'pid': 1310000000171,
            'type': 3,
            'name': null,
            'path': null,
            'component': null,
            'redirect': null,
            'permission': 'sysNotice:delete',
            'title': '删除',
            'icon': null,
            'isIframe': false,
            'outLink': null,
            'isHide': false,
            'isKeepAlive': true,
            'isAffix': false,
            'orderNo': 100,
            'status': 1,
            'remark': null,
            'children': [],
            'createTime': '2023-03-22 09:58:07',
            'updateTime': null,
            'createUserId': null,
            'updateUserId': null,
            'isDelete': false,
            'id': 1310000000175
          },
          {
            'pid': 1310000000171,
            'type': 3,
            'name': null,
            'path': null,
            'component': null,
            'redirect': null,
            'permission': 'sysNotice:public',
            'title': '发布',
            'icon': null,
            'isIframe': false,
            'outLink': null,
            'isHide': false,
            'isKeepAlive': true,
            'isAffix': false,
            'orderNo': 100,
            'status': 1,
            'remark': null,
            'children': [],
            'createTime': '2023-03-22 09:58:07',
            'updateTime': null,
            'createUserId': null,
            'updateUserId': null,
            'isDelete': false,
            'id': 1310000000176
          },
          {
            'pid': 1310000000171,
            'type': 3,
            'name': null,
            'path': null,
            'component': null,
            'redirect': null,
            'permission': 'sysNotice:cancel',
            'title': '撤回',
            'icon': null,
            'isIframe': false,
            'outLink': null,
            'isHide': false,
            'isKeepAlive': true,
            'isAffix': false,
            'orderNo': 100,
            'status': 1,
            'remark': null,
            'children': [],
            'createTime': '2023-03-22 09:58:07',
            'updateTime': null,
            'createUserId': null,
            'updateUserId': null,
            'isDelete': false,
            'id': 1310000000177
          }
          ],
          'createTime': '2023-03-22 09:58:07',
          'updateTime': null,
          'createUserId': null,
          'updateUserId': null,
          'isDelete': false,
          'id': 1310000000171
        },
        {
          'pid': 1310000000101,
          'type': 2,
          'name': 'weChatUser',
          'path': '/system/weChatUser',
          'component': '/system/weChatUser/index',
          'redirect': null,
          'permission': null,
          'title': '三方账号',
          'icon': 'ele-ChatDotRound',
          'isIframe': false,
          'outLink': null,
          'isHide': false,
          'isKeepAlive': true,
          'isAffix': false,
          'orderNo': 160,
          'status': 1,
          'remark': null,
          'children': [{
            'pid': 1310000000181,
            'type': 3,
            'name': null,
            'path': null,
            'component': null,
            'redirect': null,
            'permission': 'sysWechatUser:page',
            'title': '查询',
            'icon': null,
            'isIframe': false,
            'outLink': null,
            'isHide': false,
            'isKeepAlive': true,
            'isAffix': false,
            'orderNo': 100,
            'status': 1,
            'remark': null,
            'children': [],
            'createTime': '2023-03-22 09:58:07',
            'updateTime': null,
            'createUserId': null,
            'updateUserId': null,
            'isDelete': false,
            'id': 1310000000182
          },
          {
            'pid': 1310000000181,
            'type': 3,
            'name': null,
            'path': null,
            'component': null,
            'redirect': null,
            'permission': 'sysWechatUser:update',
            'title': '编辑',
            'icon': null,
            'isIframe': false,
            'outLink': null,
            'isHide': false,
            'isKeepAlive': true,
            'isAffix': false,
            'orderNo': 100,
            'status': 1,
            'remark': null,
            'children': [],
            'createTime': '2023-03-22 09:58:07',
            'updateTime': null,
            'createUserId': null,
            'updateUserId': null,
            'isDelete': false,
            'id': 1310000000183
          },
          {
            'pid': 1310000000181,
            'type': 3,
            'name': null,
            'path': null,
            'component': null,
            'redirect': null,
            'permission': 'sysWechatUser:add',
            'title': '增加',
            'icon': null,
            'isIframe': false,
            'outLink': null,
            'isHide': false,
            'isKeepAlive': true,
            'isAffix': false,
            'orderNo': 100,
            'status': 1,
            'remark': null,
            'children': [],
            'createTime': '2023-03-22 09:58:07',
            'updateTime': null,
            'createUserId': null,
            'updateUserId': null,
            'isDelete': false,
            'id': 1310000000184
          },
          {
            'pid': 1310000000181,
            'type': 3,
            'name': null,
            'path': null,
            'component': null,
            'redirect': null,
            'permission': 'sysWechatUser:delete',
            'title': '删除',
            'icon': null,
            'isIframe': false,
            'outLink': null,
            'isHide': false,
            'isKeepAlive': true,
            'isAffix': false,
            'orderNo': 100,
            'status': 1,
            'remark': null,
            'children': [],
            'createTime': '2023-03-22 09:58:07',
            'updateTime': null,
            'createUserId': null,
            'updateUserId': null,
            'isDelete': false,
            'id': 1310000000185
          }
          ],
          'createTime': '2023-03-22 09:58:07',
          'updateTime': null,
          'createUserId': null,
          'updateUserId': null,
          'isDelete': false,
          'id': 1310000000181
        }
        ],
        'createTime': '2023-03-22 09:58:07',
        'updateTime': null,
        'createUserId': null,
        'updateUserId': null,
        'isDelete': false,
        'id': 1310000000101
      },
      {
        'pid': 0,
        'type': 1,
        'name': 'platform',
        'path': '/platform',
        'component': 'Layout',
        'redirect': '/platform/tenant',
        'permission': null,
        'title': '平台管理',
        'icon': 'ele-Menu',
        'isIframe': false,
        'outLink': null,
        'isHide': false,
        'isKeepAlive': true,
        'isAffix': false,
        'orderNo': 1100,
        'status': 1,
        'remark': null,
        'children': [{
          'pid': 1310000000301,
          'type': 2,
          'name': 'sysTenant',
          'path': '/platform/tenant',
          'component': '/system/tenant/index',
          'redirect': null,
          'permission': null,
          'title': '租户管理',
          'icon': 'ele-School',
          'isIframe': false,
          'outLink': null,
          'isHide': false,
          'isKeepAlive': true,
          'isAffix': false,
          'orderNo': 100,
          'status': 1,
          'remark': null,
          'children': [{
            'pid': 1310000000311,
            'type': 3,
            'name': null,
            'path': null,
            'component': null,
            'redirect': null,
            'permission': 'sysTenant:page',
            'title': '查询',
            'icon': null,
            'isIframe': false,
            'outLink': null,
            'isHide': false,
            'isKeepAlive': true,
            'isAffix': false,
            'orderNo': 100,
            'status': 1,
            'remark': null,
            'children': [],
            'createTime': '2023-03-22 09:58:07',
            'updateTime': null,
            'createUserId': null,
            'updateUserId': null,
            'isDelete': false,
            'id': 1310000000312
          },
          {
            'pid': 1310000000311,
            'type': 3,
            'name': null,
            'path': null,
            'component': null,
            'redirect': null,
            'permission': 'sysTenant:update',
            'title': '编辑',
            'icon': null,
            'isIframe': false,
            'outLink': null,
            'isHide': false,
            'isKeepAlive': true,
            'isAffix': false,
            'orderNo': 100,
            'status': 1,
            'remark': null,
            'children': [],
            'createTime': '2023-03-22 09:58:07',
            'updateTime': null,
            'createUserId': null,
            'updateUserId': null,
            'isDelete': false,
            'id': 1310000000313
          },
          {
            'pid': 1310000000311,
            'type': 3,
            'name': null,
            'path': null,
            'component': null,
            'redirect': null,
            'permission': 'sysTenant:add',
            'title': '增加',
            'icon': null,
            'isIframe': false,
            'outLink': null,
            'isHide': false,
            'isKeepAlive': true,
            'isAffix': false,
            'orderNo': 100,
            'status': 1,
            'remark': null,
            'children': [],
            'createTime': '2023-03-22 09:58:07',
            'updateTime': null,
            'createUserId': null,
            'updateUserId': null,
            'isDelete': false,
            'id': 1310000000314
          },
          {
            'pid': 1310000000311,
            'type': 3,
            'name': null,
            'path': null,
            'component': null,
            'redirect': null,
            'permission': 'sysTenant:delete',
            'title': '删除',
            'icon': null,
            'isIframe': false,
            'outLink': null,
            'isHide': false,
            'isKeepAlive': true,
            'isAffix': false,
            'orderNo': 100,
            'status': 1,
            'remark': null,
            'children': [],
            'createTime': '2023-03-22 09:58:07',
            'updateTime': null,
            'createUserId': null,
            'updateUserId': null,
            'isDelete': false,
            'id': 1310000000315
          },
          {
            'pid': 1310000000311,
            'type': 3,
            'name': null,
            'path': null,
            'component': null,
            'redirect': null,
            'permission': 'sysTenant:grantMenu',
            'title': '授权菜单',
            'icon': null,
            'isIframe': false,
            'outLink': null,
            'isHide': false,
            'isKeepAlive': true,
            'isAffix': false,
            'orderNo': 100,
            'status': 1,
            'remark': null,
            'children': [],
            'createTime': '2023-03-22 09:58:07',
            'updateTime': null,
            'createUserId': null,
            'updateUserId': null,
            'isDelete': false,
            'id': 1310000000316
          },
          {
            'pid': 1310000000311,
            'type': 3,
            'name': null,
            'path': null,
            'component': null,
            'redirect': null,
            'permission': 'sysTenant:resetPwd',
            'title': '重置密码',
            'icon': null,
            'isIframe': false,
            'outLink': null,
            'isHide': false,
            'isKeepAlive': true,
            'isAffix': false,
            'orderNo': 100,
            'status': 1,
            'remark': null,
            'children': [],
            'createTime': '2023-03-22 09:58:07',
            'updateTime': null,
            'createUserId': null,
            'updateUserId': null,
            'isDelete': false,
            'id': 1310000000317
          },
          {
            'pid': 1310000000311,
            'type': 3,
            'name': null,
            'path': null,
            'component': null,
            'redirect': null,
            'permission': 'sysTenant:createDb',
            'title': '生成库',
            'icon': null,
            'isIframe': false,
            'outLink': null,
            'isHide': false,
            'isKeepAlive': true,
            'isAffix': false,
            'orderNo': 100,
            'status': 1,
            'remark': null,
            'children': [],
            'createTime': '2023-03-22 09:58:07',
            'updateTime': null,
            'createUserId': null,
            'updateUserId': null,
            'isDelete': false,
            'id': 1310000000318
          }
          ],
          'createTime': '2023-03-22 09:58:07',
          'updateTime': null,
          'createUserId': null,
          'updateUserId': null,
          'isDelete': false,
          'id': 1310000000311
        },
        {
          'pid': 1310000000301,
          'type': 2,
          'name': 'sysMenu',
          'path': '/platform/menu',
          'component': '/system/menu/index',
          'redirect': null,
          'permission': null,
          'title': '菜单管理',
          'icon': 'ele-Menu',
          'isIframe': false,
          'outLink': null,
          'isHide': false,
          'isKeepAlive': true,
          'isAffix': false,
          'orderNo': 110,
          'status': 1,
          'remark': null,
          'children': [{
            'pid': 1310000000321,
            'type': 3,
            'name': null,
            'path': null,
            'component': null,
            'redirect': null,
            'permission': 'sysMenu:list',
            'title': '查询',
            'icon': null,
            'isIframe': false,
            'outLink': null,
            'isHide': false,
            'isKeepAlive': true,
            'isAffix': false,
            'orderNo': 100,
            'status': 1,
            'remark': null,
            'children': [],
            'createTime': '2023-03-22 09:58:07',
            'updateTime': null,
            'createUserId': null,
            'updateUserId': null,
            'isDelete': false,
            'id': 1310000000322
          },
          {
            'pid': 1310000000321,
            'type': 3,
            'name': null,
            'path': null,
            'component': null,
            'redirect': null,
            'permission': 'sysMenu:update',
            'title': '编辑',
            'icon': null,
            'isIframe': false,
            'outLink': null,
            'isHide': false,
            'isKeepAlive': true,
            'isAffix': false,
            'orderNo': 100,
            'status': 1,
            'remark': null,
            'children': [],
            'createTime': '2023-03-22 09:58:07',
            'updateTime': null,
            'createUserId': null,
            'updateUserId': null,
            'isDelete': false,
            'id': 1310000000323
          },
          {
            'pid': 1310000000321,
            'type': 3,
            'name': null,
            'path': null,
            'component': null,
            'redirect': null,
            'permission': 'sysMenu:add',
            'title': '增加',
            'icon': null,
            'isIframe': false,
            'outLink': null,
            'isHide': false,
            'isKeepAlive': true,
            'isAffix': false,
            'orderNo': 100,
            'status': 1,
            'remark': null,
            'children': [],
            'createTime': '2023-03-22 09:58:07',
            'updateTime': null,
            'createUserId': null,
            'updateUserId': null,
            'isDelete': false,
            'id': 1310000000324
          },
          {
            'pid': 1310000000321,
            'type': 3,
            'name': null,
            'path': null,
            'component': null,
            'redirect': null,
            'permission': 'sysMenu:delete',
            'title': '删除',
            'icon': null,
            'isIframe': false,
            'outLink': null,
            'isHide': false,
            'isKeepAlive': true,
            'isAffix': false,
            'orderNo': 100,
            'status': 1,
            'remark': null,
            'children': [],
            'createTime': '2023-03-22 09:58:07',
            'updateTime': null,
            'createUserId': null,
            'updateUserId': null,
            'isDelete': false,
            'id': 1310000000325
          }
          ],
          'createTime': '2023-03-22 09:58:07',
          'updateTime': null,
          'createUserId': null,
          'updateUserId': null,
          'isDelete': false,
          'id': 1310000000321
        },
        {
          'pid': 1310000000301,
          'type': 2,
          'name': 'sysConfig',
          'path': '/platform/config',
          'component': '/system/config/index',
          'redirect': null,
          'permission': null,
          'title': '参数配置',
          'icon': 'ele-DocumentCopy',
          'isIframe': false,
          'outLink': null,
          'isHide': false,
          'isKeepAlive': true,
          'isAffix': false,
          'orderNo': 120,
          'status': 1,
          'remark': null,
          'children': [{
            'pid': 1310000000331,
            'type': 3,
            'name': null,
            'path': null,
            'component': null,
            'redirect': null,
            'permission': 'sysConfig:page',
            'title': '查询',
            'icon': null,
            'isIframe': false,
            'outLink': null,
            'isHide': false,
            'isKeepAlive': true,
            'isAffix': false,
            'orderNo': 100,
            'status': 1,
            'remark': null,
            'children': [],
            'createTime': '2023-03-22 09:58:07',
            'updateTime': null,
            'createUserId': null,
            'updateUserId': null,
            'isDelete': false,
            'id': 1310000000332
          },
          {
            'pid': 1310000000331,
            'type': 3,
            'name': null,
            'path': null,
            'component': null,
            'redirect': null,
            'permission': 'sysConfig:update',
            'title': '编辑',
            'icon': null,
            'isIframe': false,
            'outLink': null,
            'isHide': false,
            'isKeepAlive': true,
            'isAffix': false,
            'orderNo': 100,
            'status': 1,
            'remark': null,
            'children': [],
            'createTime': '2023-03-22 09:58:07',
            'updateTime': null,
            'createUserId': null,
            'updateUserId': null,
            'isDelete': false,
            'id': 1310000000333
          },
          {
            'pid': 1310000000331,
            'type': 3,
            'name': null,
            'path': null,
            'component': null,
            'redirect': null,
            'permission': 'sysConfig:add',
            'title': '增加',
            'icon': null,
            'isIframe': false,
            'outLink': null,
            'isHide': false,
            'isKeepAlive': true,
            'isAffix': false,
            'orderNo': 100,
            'status': 1,
            'remark': null,
            'children': [],
            'createTime': '2023-03-22 09:58:07',
            'updateTime': null,
            'createUserId': null,
            'updateUserId': null,
            'isDelete': false,
            'id': 1310000000334
          },
          {
            'pid': 1310000000331,
            'type': 3,
            'name': null,
            'path': null,
            'component': null,
            'redirect': null,
            'permission': 'sysConfig:delete',
            'title': '删除',
            'icon': null,
            'isIframe': false,
            'outLink': null,
            'isHide': false,
            'isKeepAlive': true,
            'isAffix': false,
            'orderNo': 100,
            'status': 1,
            'remark': null,
            'children': [],
            'createTime': '2023-03-22 09:58:07',
            'updateTime': null,
            'createUserId': null,
            'updateUserId': null,
            'isDelete': false,
            'id': 1310000000335
          }
          ],
          'createTime': '2023-03-22 09:58:07',
          'updateTime': null,
          'createUserId': null,
          'updateUserId': null,
          'isDelete': false,
          'id': 1310000000331
        },
        {
          'pid': 1310000000301,
          'type': 2,
          'name': 'sysDict',
          'path': '/platform/dict',
          'component': '/system/dict/index',
          'redirect': null,
          'permission': null,
          'title': '字典管理',
          'icon': 'ele-Collection',
          'isIframe': false,
          'outLink': null,
          'isHide': false,
          'isKeepAlive': true,
          'isAffix': false,
          'orderNo': 130,
          'status': 1,
          'remark': null,
          'children': [{
            'pid': 1310000000341,
            'type': 3,
            'name': null,
            'path': null,
            'component': null,
            'redirect': null,
            'permission': 'sysDictType:page',
            'title': '查询',
            'icon': null,
            'isIframe': false,
            'outLink': null,
            'isHide': false,
            'isKeepAlive': true,
            'isAffix': false,
            'orderNo': 100,
            'status': 1,
            'remark': null,
            'children': [],
            'createTime': '2023-03-22 09:58:07',
            'updateTime': null,
            'createUserId': null,
            'updateUserId': null,
            'isDelete': false,
            'id': 1310000000342
          },
          {
            'pid': 1310000000341,
            'type': 3,
            'name': null,
            'path': null,
            'component': null,
            'redirect': null,
            'permission': 'sysDictType:update',
            'title': '编辑',
            'icon': null,
            'isIframe': false,
            'outLink': null,
            'isHide': false,
            'isKeepAlive': true,
            'isAffix': false,
            'orderNo': 100,
            'status': 1,
            'remark': null,
            'children': [],
            'createTime': '2023-03-22 09:58:07',
            'updateTime': null,
            'createUserId': null,
            'updateUserId': null,
            'isDelete': false,
            'id': 1310000000343
          },
          {
            'pid': 1310000000341,
            'type': 3,
            'name': null,
            'path': null,
            'component': null,
            'redirect': null,
            'permission': 'sysDictType:add',
            'title': '增加',
            'icon': null,
            'isIframe': false,
            'outLink': null,
            'isHide': false,
            'isKeepAlive': true,
            'isAffix': false,
            'orderNo': 100,
            'status': 1,
            'remark': null,
            'children': [],
            'createTime': '2023-03-22 09:58:07',
            'updateTime': null,
            'createUserId': null,
            'updateUserId': null,
            'isDelete': false,
            'id': 1310000000344
          },
          {
            'pid': 1310000000341,
            'type': 3,
            'name': null,
            'path': null,
            'component': null,
            'redirect': null,
            'permission': 'sysDictType:delete',
            'title': '删除',
            'icon': null,
            'isIframe': false,
            'outLink': null,
            'isHide': false,
            'isKeepAlive': true,
            'isAffix': false,
            'orderNo': 100,
            'status': 1,
            'remark': null,
            'children': [],
            'createTime': '2023-03-22 09:58:07',
            'updateTime': null,
            'createUserId': null,
            'updateUserId': null,
            'isDelete': false,
            'id': 1310000000345
          }
          ],
          'createTime': '2023-03-22 09:58:07',
          'updateTime': null,
          'createUserId': null,
          'updateUserId': null,
          'isDelete': false,
          'id': 1310000000341
        },
        {
          'pid': 1310000000301,
          'type': 2,
          'name': 'sysJob',
          'path': '/platform/job',
          'component': '/system/job/index',
          'redirect': null,
          'permission': null,
          'title': '任务调度',
          'icon': 'ele-AlarmClock',
          'isIframe': false,
          'outLink': null,
          'isHide': false,
          'isKeepAlive': true,
          'isAffix': false,
          'orderNo': 140,
          'status': 1,
          'remark': null,
          'children': [{
            'pid': 1310000000351,
            'type': 3,
            'name': null,
            'path': null,
            'component': null,
            'redirect': null,
            'permission': 'sysJob:pageJobDetail',
            'title': '查询',
            'icon': null,
            'isIframe': false,
            'outLink': null,
            'isHide': false,
            'isKeepAlive': true,
            'isAffix': false,
            'orderNo': 100,
            'status': 1,
            'remark': null,
            'children': [],
            'createTime': '2023-03-22 09:58:07',
            'updateTime': null,
            'createUserId': null,
            'updateUserId': null,
            'isDelete': false,
            'id': 1310000000352
          },
          {
            'pid': 1310000000351,
            'type': 3,
            'name': null,
            'path': null,
            'component': null,
            'redirect': null,
            'permission': 'sysJob:updateJobDetail',
            'title': '编辑',
            'icon': null,
            'isIframe': false,
            'outLink': null,
            'isHide': false,
            'isKeepAlive': true,
            'isAffix': false,
            'orderNo': 100,
            'status': 1,
            'remark': null,
            'children': [],
            'createTime': '2023-03-22 09:58:07',
            'updateTime': null,
            'createUserId': null,
            'updateUserId': null,
            'isDelete': false,
            'id': 1310000000353
          },
          {
            'pid': 1310000000351,
            'type': 3,
            'name': null,
            'path': null,
            'component': null,
            'redirect': null,
            'permission': 'sysJob:addJobDetail',
            'title': '增加',
            'icon': null,
            'isIframe': false,
            'outLink': null,
            'isHide': false,
            'isKeepAlive': true,
            'isAffix': false,
            'orderNo': 100,
            'status': 1,
            'remark': null,
            'children': [],
            'createTime': '2023-03-22 09:58:07',
            'updateTime': null,
            'createUserId': null,
            'updateUserId': null,
            'isDelete': false,
            'id': 1310000000354
          },
          {
            'pid': 1310000000351,
            'type': 3,
            'name': null,
            'path': null,
            'component': null,
            'redirect': null,
            'permission': 'sysJob:deleteJobDetail',
            'title': '删除',
            'icon': null,
            'isIframe': false,
            'outLink': null,
            'isHide': false,
            'isKeepAlive': true,
            'isAffix': false,
            'orderNo': 100,
            'status': 1,
            'remark': null,
            'children': [],
            'createTime': '2023-03-22 09:58:07',
            'updateTime': null,
            'createUserId': null,
            'updateUserId': null,
            'isDelete': false,
            'id': 1310000000355
          }
          ],
          'createTime': '2023-03-22 09:58:07',
          'updateTime': null,
          'createUserId': null,
          'updateUserId': null,
          'isDelete': false,
          'id': 1310000000351
        },
        {
          'pid': 1310000000301,
          'type': 2,
          'name': 'sysServer',
          'path': '/platform/server',
          'component': '/system/server/index',
          'redirect': null,
          'permission': null,
          'title': '系统监控',
          'icon': 'ele-Monitor',
          'isIframe': false,
          'outLink': null,
          'isHide': false,
          'isKeepAlive': true,
          'isAffix': false,
          'orderNo': 150,
          'status': 1,
          'remark': null,
          'children': [],
          'createTime': '2023-03-22 09:58:07',
          'updateTime': null,
          'createUserId': null,
          'updateUserId': null,
          'isDelete': false,
          'id': 1310000000361
        },
        {
          'pid': 1310000000301,
          'type': 2,
          'name': 'sysCache',
          'path': '/platform/cache',
          'component': '/system/cache/index',
          'redirect': null,
          'permission': null,
          'title': '缓存管理',
          'icon': 'ele-Loading',
          'isIframe': false,
          'outLink': null,
          'isHide': false,
          'isKeepAlive': true,
          'isAffix': false,
          'orderNo': 160,
          'status': 1,
          'remark': null,
          'children': [{
            'pid': 1310000000371,
            'type': 3,
            'name': null,
            'path': null,
            'component': null,
            'redirect': null,
            'permission': 'sysCache:keyList',
            'title': '查询',
            'icon': null,
            'isIframe': false,
            'outLink': null,
            'isHide': false,
            'isKeepAlive': true,
            'isAffix': false,
            'orderNo': 100,
            'status': 1,
            'remark': null,
            'children': [],
            'createTime': '2023-03-22 09:58:07',
            'updateTime': null,
            'createUserId': null,
            'updateUserId': null,
            'isDelete': false,
            'id': 1310000000372
          },
          {
            'pid': 1310000000371,
            'type': 3,
            'name': null,
            'path': null,
            'component': null,
            'redirect': null,
            'permission': 'sysCache:delete',
            'title': '删除',
            'icon': null,
            'isIframe': false,
            'outLink': null,
            'isHide': false,
            'isKeepAlive': true,
            'isAffix': false,
            'orderNo': 100,
            'status': 1,
            'remark': null,
            'children': [],
            'createTime': '2023-03-22 09:58:07',
            'updateTime': null,
            'createUserId': null,
            'updateUserId': null,
            'isDelete': false,
            'id': 1310000000373
          }
          ],
          'createTime': '2023-03-22 09:58:07',
          'updateTime': null,
          'createUserId': null,
          'updateUserId': null,
          'isDelete': false,
          'id': 1310000000371
        },
        {
          'pid': 1310000000301,
          'type': 2,
          'name': 'sysRegion',
          'path': '/platform/region',
          'component': '/system/region/index',
          'redirect': null,
          'permission': null,
          'title': '行政区域',
          'icon': 'ele-LocationInformation',
          'isIframe': false,
          'outLink': null,
          'isHide': false,
          'isKeepAlive': true,
          'isAffix': false,
          'orderNo': 170,
          'status': 1,
          'remark': null,
          'children': [{
            'pid': 1310000000381,
            'type': 3,
            'name': null,
            'path': null,
            'component': null,
            'redirect': null,
            'permission': 'sysRegion:page',
            'title': '查询',
            'icon': null,
            'isIframe': false,
            'outLink': null,
            'isHide': false,
            'isKeepAlive': true,
            'isAffix': false,
            'orderNo': 100,
            'status': 1,
            'remark': null,
            'children': [],
            'createTime': '2023-03-22 09:58:07',
            'updateTime': null,
            'createUserId': null,
            'updateUserId': null,
            'isDelete': false,
            'id': 1310000000382
          },
          {
            'pid': 1310000000381,
            'type': 3,
            'name': null,
            'path': null,
            'component': null,
            'redirect': null,
            'permission': 'sysRegion:update',
            'title': '编辑',
            'icon': null,
            'isIframe': false,
            'outLink': null,
            'isHide': false,
            'isKeepAlive': true,
            'isAffix': false,
            'orderNo': 100,
            'status': 1,
            'remark': null,
            'children': [],
            'createTime': '2023-03-22 09:58:07',
            'updateTime': null,
            'createUserId': null,
            'updateUserId': null,
            'isDelete': false,
            'id': 1310000000383
          },
          {
            'pid': 1310000000381,
            'type': 3,
            'name': null,
            'path': null,
            'component': null,
            'redirect': null,
            'permission': 'sysRegion:add',
            'title': '增加',
            'icon': null,
            'isIframe': false,
            'outLink': null,
            'isHide': false,
            'isKeepAlive': true,
            'isAffix': false,
            'orderNo': 100,
            'status': 1,
            'remark': null,
            'children': [],
            'createTime': '2023-03-22 09:58:07',
            'updateTime': null,
            'createUserId': null,
            'updateUserId': null,
            'isDelete': false,
            'id': 1310000000384
          },
          {
            'pid': 1310000000381,
            'type': 3,
            'name': null,
            'path': null,
            'component': null,
            'redirect': null,
            'permission': 'sysRegion:delete',
            'title': '删除',
            'icon': null,
            'isIframe': false,
            'outLink': null,
            'isHide': false,
            'isKeepAlive': true,
            'isAffix': false,
            'orderNo': 100,
            'status': 1,
            'remark': null,
            'children': [],
            'createTime': '2023-03-22 09:58:07',
            'updateTime': null,
            'createUserId': null,
            'updateUserId': null,
            'isDelete': false,
            'id': 1310000000385
          },
          {
            'pid': 1310000000381,
            'type': 3,
            'name': null,
            'path': null,
            'component': null,
            'redirect': null,
            'permission': 'sysRegion:sync',
            'title': '同步',
            'icon': null,
            'isIframe': false,
            'outLink': null,
            'isHide': false,
            'isKeepAlive': true,
            'isAffix': false,
            'orderNo': 100,
            'status': 1,
            'remark': null,
            'children': [],
            'createTime': '2023-03-22 09:58:07',
            'updateTime': null,
            'createUserId': null,
            'updateUserId': null,
            'isDelete': false,
            'id': 1310000000386
          }
          ],
          'createTime': '2023-03-22 09:58:07',
          'updateTime': null,
          'createUserId': null,
          'updateUserId': null,
          'isDelete': false,
          'id': 1310000000381
        },
        {
          'pid': 1310000000301,
          'type': 2,
          'name': 'sysFile',
          'path': '/platform/file',
          'component': '/system/file/index',
          'redirect': null,
          'permission': null,
          'title': '文件管理',
          'icon': 'ele-Document',
          'isIframe': false,
          'outLink': null,
          'isHide': false,
          'isKeepAlive': true,
          'isAffix': false,
          'orderNo': 180,
          'status': 1,
          'remark': null,
          'children': [{
            'pid': 1310000000391,
            'type': 3,
            'name': null,
            'path': null,
            'component': null,
            'redirect': null,
            'permission': 'sysFile:page',
            'title': '查询',
            'icon': null,
            'isIframe': false,
            'outLink': null,
            'isHide': false,
            'isKeepAlive': true,
            'isAffix': false,
            'orderNo': 100,
            'status': 1,
            'remark': null,
            'children': [],
            'createTime': '2023-03-22 09:58:07',
            'updateTime': null,
            'createUserId': null,
            'updateUserId': null,
            'isDelete': false,
            'id': 1310000000392
          },
          {
            'pid': 1310000000391,
            'type': 3,
            'name': null,
            'path': null,
            'component': null,
            'redirect': null,
            'permission': 'sysFile:uploadFile',
            'title': '上传',
            'icon': null,
            'isIframe': false,
            'outLink': null,
            'isHide': false,
            'isKeepAlive': true,
            'isAffix': false,
            'orderNo': 100,
            'status': 1,
            'remark': null,
            'children': [],
            'createTime': '2023-03-22 09:58:07',
            'updateTime': null,
            'createUserId': null,
            'updateUserId': null,
            'isDelete': false,
            'id': 1310000000393
          },
          {
            'pid': 1310000000391,
            'type': 3,
            'name': null,
            'path': null,
            'component': null,
            'redirect': null,
            'permission': 'sysFile:downloadFile',
            'title': '下载',
            'icon': null,
            'isIframe': false,
            'outLink': null,
            'isHide': false,
            'isKeepAlive': true,
            'isAffix': false,
            'orderNo': 100,
            'status': 1,
            'remark': null,
            'children': [],
            'createTime': '2023-03-22 09:58:07',
            'updateTime': null,
            'createUserId': null,
            'updateUserId': null,
            'isDelete': false,
            'id': 1310000000394
          },
          {
            'pid': 1310000000391,
            'type': 3,
            'name': null,
            'path': null,
            'component': null,
            'redirect': null,
            'permission': 'sysFile:delete',
            'title': '删除',
            'icon': null,
            'isIframe': false,
            'outLink': null,
            'isHide': false,
            'isKeepAlive': true,
            'isAffix': false,
            'orderNo': 100,
            'status': 1,
            'remark': null,
            'children': [],
            'createTime': '2023-03-22 09:58:07',
            'updateTime': null,
            'createUserId': null,
            'updateUserId': null,
            'isDelete': false,
            'id': 1310000000395
          }
          ],
          'createTime': '2023-03-22 09:58:07',
          'updateTime': null,
          'createUserId': null,
          'updateUserId': null,
          'isDelete': false,
          'id': 1310000000391
        }
        ],
        'createTime': '2023-03-22 09:58:07',
        'updateTime': null,
        'createUserId': null,
        'updateUserId': null,
        'isDelete': false,
        'id': 1310000000301
      },
      {
        'pid': 0,
        'type': 1,
        'name': 'log',
        'path': '/log',
        'component': 'Layout',
        'redirect': '/log/vislog',
        'permission': null,
        'title': '日志管理',
        'icon': 'ele-DocumentCopy',
        'isIframe': false,
        'outLink': null,
        'isHide': false,
        'isKeepAlive': true,
        'isAffix': false,
        'orderNo': 1200,
        'status': 1,
        'remark': null,
        'children': [{
          'pid': 1310000000501,
          'type': 2,
          'name': 'sysVislog',
          'path': '/log/vislog',
          'component': '/system/log/vislog/index',
          'redirect': null,
          'permission': null,
          'title': '访问日志',
          'icon': 'ele-Document',
          'isIframe': false,
          'outLink': null,
          'isHide': false,
          'isKeepAlive': true,
          'isAffix': false,
          'orderNo': 100,
          'status': 1,
          'remark': null,
          'children': [{
            'pid': 1310000000511,
            'type': 3,
            'name': null,
            'path': null,
            'component': null,
            'redirect': null,
            'permission': 'sysVislog:page',
            'title': '查询',
            'icon': null,
            'isIframe': false,
            'outLink': null,
            'isHide': false,
            'isKeepAlive': true,
            'isAffix': false,
            'orderNo': 100,
            'status': 1,
            'remark': null,
            'children': [],
            'createTime': '2023-03-22 09:58:07',
            'updateTime': null,
            'createUserId': null,
            'updateUserId': null,
            'isDelete': false,
            'id': 1310000000512
          },
          {
            'pid': 1310000000511,
            'type': 3,
            'name': null,
            'path': null,
            'component': null,
            'redirect': null,
            'permission': 'sysVislog:clear',
            'title': '清空',
            'icon': null,
            'isIframe': false,
            'outLink': null,
            'isHide': false,
            'isKeepAlive': true,
            'isAffix': false,
            'orderNo': 100,
            'status': 1,
            'remark': null,
            'children': [],
            'createTime': '2023-03-22 09:58:07',
            'updateTime': null,
            'createUserId': null,
            'updateUserId': null,
            'isDelete': false,
            'id': 1310000000513
          }
          ],
          'createTime': '2023-03-22 09:58:07',
          'updateTime': null,
          'createUserId': null,
          'updateUserId': null,
          'isDelete': false,
          'id': 1310000000511
        },
        {
          'pid': 1310000000501,
          'type': 2,
          'name': 'sysOplog',
          'path': '/log/oplog',
          'component': '/system/log/oplog/index',
          'redirect': null,
          'permission': null,
          'title': '操作日志',
          'icon': 'ele-Document',
          'isIframe': false,
          'outLink': null,
          'isHide': false,
          'isKeepAlive': true,
          'isAffix': false,
          'orderNo': 110,
          'status': 1,
          'remark': null,
          'children': [{
            'pid': 1310000000521,
            'type': 3,
            'name': null,
            'path': null,
            'component': null,
            'redirect': null,
            'permission': 'sysOplog:page',
            'title': '查询',
            'icon': null,
            'isIframe': false,
            'outLink': null,
            'isHide': false,
            'isKeepAlive': true,
            'isAffix': false,
            'orderNo': 100,
            'status': 1,
            'remark': null,
            'children': [],
            'createTime': '2023-03-22 09:58:07',
            'updateTime': null,
            'createUserId': null,
            'updateUserId': null,
            'isDelete': false,
            'id': 1310000000522
          },
          {
            'pid': 1310000000521,
            'type': 3,
            'name': null,
            'path': null,
            'component': null,
            'redirect': null,
            'permission': 'sysOplog:clear',
            'title': '清空',
            'icon': null,
            'isIframe': false,
            'outLink': null,
            'isHide': false,
            'isKeepAlive': true,
            'isAffix': false,
            'orderNo': 100,
            'status': 1,
            'remark': null,
            'children': [],
            'createTime': '2023-03-22 09:58:07',
            'updateTime': null,
            'createUserId': null,
            'updateUserId': null,
            'isDelete': false,
            'id': 1310000000523
          },
          {
            'pid': 1310000000521,
            'type': 3,
            'name': null,
            'path': null,
            'component': null,
            'redirect': null,
            'permission': 'sysOplog:export',
            'title': '导出',
            'icon': null,
            'isIframe': false,
            'outLink': null,
            'isHide': false,
            'isKeepAlive': true,
            'isAffix': false,
            'orderNo': 100,
            'status': 1,
            'remark': null,
            'children': [],
            'createTime': '2023-03-22 09:58:07',
            'updateTime': null,
            'createUserId': null,
            'updateUserId': null,
            'isDelete': false,
            'id': 1310000000524
          }
          ],
          'createTime': '2023-03-22 09:58:07',
          'updateTime': null,
          'createUserId': null,
          'updateUserId': null,
          'isDelete': false,
          'id': 1310000000521
        },
        {
          'pid': 1310000000501,
          'type': 2,
          'name': 'sysDifflog',
          'path': '/log/difflog',
          'component': '/system/log/difflog/index',
          'redirect': null,
          'permission': null,
          'title': '差异日志',
          'icon': 'ele-Document',
          'isIframe': false,
          'outLink': null,
          'isHide': false,
          'isKeepAlive': true,
          'isAffix': false,
          'orderNo': 130,
          'status': 1,
          'remark': null,
          'children': [{
            'pid': 1310000000531,
            'type': 3,
            'name': null,
            'path': null,
            'component': null,
            'redirect': null,
            'permission': 'sysDifflog:page',
            'title': '查询',
            'icon': null,
            'isIframe': false,
            'outLink': null,
            'isHide': false,
            'isKeepAlive': true,
            'isAffix': false,
            'orderNo': 100,
            'status': 1,
            'remark': null,
            'children': [],
            'createTime': '2023-03-22 09:58:07',
            'updateTime': null,
            'createUserId': null,
            'updateUserId': null,
            'isDelete': false,
            'id': 1310000000532
          },
          {
            'pid': 1310000000531,
            'type': 3,
            'name': null,
            'path': null,
            'component': null,
            'redirect': null,
            'permission': 'sysDifflog:clear',
            'title': '清空',
            'icon': null,
            'isIframe': false,
            'outLink': null,
            'isHide': false,
            'isKeepAlive': true,
            'isAffix': false,
            'orderNo': 100,
            'status': 1,
            'remark': null,
            'children': [],
            'createTime': '2023-03-22 09:58:07',
            'updateTime': null,
            'createUserId': null,
            'updateUserId': null,
            'isDelete': false,
            'id': 1310000000533
          }
          ],
          'createTime': '2023-03-22 09:58:07',
          'updateTime': null,
          'createUserId': null,
          'updateUserId': null,
          'isDelete': false,
          'id': 1310000000531
        }
        ],
        'createTime': '2023-03-22 09:58:07',
        'updateTime': null,
        'createUserId': null,
        'updateUserId': null,
        'isDelete': false,
        'id': 1310000000501
      },
      {
        'pid': 0,
        'type': 1,
        'name': 'develop',
        'path': '/develop',
        'component': 'Layout',
        'redirect': '/develop/database',
        'permission': null,
        'title': '开发工具',
        'icon': 'ele-Cpu',
        'isIframe': false,
        'outLink': null,
        'isHide': false,
        'isKeepAlive': true,
        'isAffix': false,
        'orderNo': 1300,
        'status': 1,
        'remark': null,
        'children': [{
          'pid': 1310000000601,
          'type': 2,
          'name': 'sysDatabase',
          'path': '/develop/database',
          'component': '/system/database/index',
          'redirect': null,
          'permission': null,
          'title': '库表管理',
          'icon': 'ele-Coin',
          'isIframe': false,
          'outLink': null,
          'isHide': false,
          'isKeepAlive': true,
          'isAffix': false,
          'orderNo': 100,
          'status': 1,
          'remark': null,
          'children': [],
          'createTime': '2023-03-22 09:58:07',
          'updateTime': null,
          'createUserId': null,
          'updateUserId': null,
          'isDelete': false,
          'id': 1310000000611
        },
        {
          'pid': 1310000000601,
          'type': 2,
          'name': 'sysFormDes',
          'path': '/develop/formDes',
          'component': '/system/formDes/index',
          'redirect': null,
          'permission': null,
          'title': '表单设计',
          'icon': 'ele-Edit',
          'isIframe': false,
          'outLink': null,
          'isHide': false,
          'isKeepAlive': true,
          'isAffix': false,
          'orderNo': 110,
          'status': 1,
          'remark': null,
          'children': [],
          'createTime': '2023-03-22 09:58:07',
          'updateTime': null,
          'createUserId': null,
          'updateUserId': null,
          'isDelete': false,
          'id': 1310000000621
        },
        {
          'pid': 1310000000601,
          'type': 2,
          'name': 'sysCodeGen',
          'path': '/develop/codeGen',
          'component': '/system/codeGen/index',
          'redirect': null,
          'permission': null,
          'title': '代码生成',
          'icon': 'ele-Crop',
          'isIframe': false,
          'outLink': null,
          'isHide': false,
          'isKeepAlive': true,
          'isAffix': false,
          'orderNo': 120,
          'status': 1,
          'remark': null,
          'children': [],
          'createTime': '2023-03-22 09:58:07',
          'updateTime': null,
          'createUserId': null,
          'updateUserId': null,
          'isDelete': false,
          'id': 1310000000631
        },
        {
          'pid': 1310000000601,
          'type': 2,
          'name': 'sysApi',
          'path': '/develop/api',
          'component': 'layout/routerView/iframe',
          'redirect': null,
          'permission': null,
          'title': '系统接口',
          'icon': 'ele-Help',
          'isIframe': true,
          'outLink': 'https://localhost:44326/',
          'isHide': false,
          'isKeepAlive': true,
          'isAffix': false,
          'orderNo': 130,
          'status': 1,
          'remark': null,
          'children': [],
          'createTime': '2023-03-22 09:58:07',
          'updateTime': null,
          'createUserId': null,
          'updateUserId': null,
          'isDelete': false,
          'id': 1310000000641
        }
        ],
        'createTime': '2023-03-22 09:58:07',
        'updateTime': null,
        'createUserId': null,
        'updateUserId': null,
        'isDelete': false,
        'id': 1310000000601
      },
      {
        'pid': 0,
        'type': 1,
        'name': 'doc',
        'path': '/doc',
        'component': 'Layout',
        'redirect': '/doc/furion',
        'permission': null,
        'title': '帮助文档',
        'icon': 'ele-Notebook',
        'isIframe': false,
        'outLink': null,
        'isHide': false,
        'isKeepAlive': true,
        'isAffix': false,
        'orderNo': 1400,
        'status': 1,
        'remark': null,
        'children': [{
          'pid': 1310000000701,
          'type': 2,
          'name': 'sysFurion',
          'path': '/doc/furion',
          'component': 'layout/routerView/link',
          'redirect': null,
          'permission': null,
          'title': '后台教程',
          'icon': 'ele-Promotion',
          'isIframe': false,
          'outLink': 'https://furion.baiqian.ltd/',
          'isHide': false,
          'isKeepAlive': false,
          'isAffix': false,
          'orderNo': 100,
          'status': 1,
          'remark': null,
          'children': [],
          'createTime': '2023-03-22 09:58:07',
          'updateTime': null,
          'createUserId': null,
          'updateUserId': null,
          'isDelete': false,
          'id': 1310000000711
        },
        {
          'pid': 1310000000701,
          'type': 2,
          'name': 'sysElement',
          'path': '/doc/element',
          'component': 'layout/routerView/link',
          'redirect': null,
          'permission': null,
          'title': '前端教程',
          'icon': 'ele-Position',
          'isIframe': false,
          'outLink': 'https://element-plus.gitee.io/zh-CN/',
          'isHide': false,
          'isKeepAlive': false,
          'isAffix': false,
          'orderNo': 110,
          'status': 1,
          'remark': null,
          'children': [],
          'createTime': '2023-03-22 09:58:07',
          'updateTime': null,
          'createUserId': null,
          'updateUserId': null,
          'isDelete': false,
          'id': 1310000000712
        }
        ],
        'createTime': '2023-03-22 09:58:07',
        'updateTime': null,
        'createUserId': null,
        'updateUserId': null,
        'isDelete': false,
        'id': 1310000000701
      }
      ],
      tableKey: 0,
      list: null,
      total: 0,
      listLoading: true,
      listQuery: {
        page: 1,
        limit: 10,
        status: undefined,
        filter: '',
        sorting: 'name desc'
      },
      isExpansion: false, // 是否展开
      dataForm: {
        name: '',
        pid: 1310000000701,
        type: 2,
        pathName: '',
        path: '',
        component: '',
        redirect: null,
        permission: null,
        title: '',
        icon: '',
        isIframe: false,
        outLink: '',
        isHide: false,
        isKeepAlive: false,
        isAffix: false,
        orderNo: undefined,
        status: 1,
        remark: null,
        children: [],
        isDelete: false,
        id: 1310000000712
      },

      rules: {
        name: [{
          required: true,
          message: '请输入菜单名称',
          trigger: 'blur'
        },
        {
          min: 3,
          max: 5,
          message: '长度在 1 到 15 个字符',
          trigger: 'blur'
        }
        ],
        path: [{
          required: true,
          message: '请输入路由路径',
          trigger: 'blur'
        }],
        pathName: [{
          required: true,
          message: '请输入路由名称',
          trigger: 'blur'
        }],
        component: [{
          required: true,
          message: '请输入组件路径',
          trigger: 'blur'
        }]
      },
      icon: '',

      num: 10,
      dialogStatus: '',
      dialogFormVisible: false
    }
  },
  methods: {
    checkPermission,
    load(tree, treeNode, resolve) {
      setTimeout(() => {
        resolve([{
          id: 31,
          date: '2016-05-01',
          name: '王小虎',
          address: '上海市普陀区金沙江路 1519 弄'
        }, {
          id: 32,
          date: '2016-05-01',
          name: '王小虎',
          address: '上海市普陀区金沙江路 1519 弄'
        }])
      }, 1000)
    },
    // 重置查询参数
    resetQueryForm() {
      this.listQuery = Object.assign({
        status: undefined
      }, baseListQuery)
    },
    handleFilter() {
      this.listQuery.page = 1
      this.getList()
    },
    // 刷新页面
    handleRefresh() {
      this.handleFilter()
    },
    // 级联选择器切换
    handleChange(value) {
      console.log(value)
    },
    // 切换数据表格树形展开
    toggleRowExpansion() {
      this.isExpansion = !this.isExpansion
      this.toggleRowExpansionAll(this.tableData, this.isExpansion)
    },
    toggleRowExpansionAll(data, isExpansion) {
      // toggleRowExpansion 切换某一行的展开状态
      // 遍历得到row和布尔值isExpansion，通过ref找到table执行方法toggleRowExpansion(row, expanded)
      data.forEach((item) => {
        // vue ref属性的作用
        this.$refs.dataTreeList.toggleRowExpansion(item, isExpansion)
        if (item.children !== undefined && item.children !== null) {
          this.toggleRowExpansionAll(item.children, isExpansion)
        }
      })
    },
    resetTemp() {
      this.dataForm = {
        name: '',
        pid: 1310000000701,
        type: 2,
        pathName: '',
        path: '',
        component: '',
        redirect: null,
        permission: null,
        title: '',
        icon: '',
        isIframe: false,
        outLink: '',
        isHide: false,
        isKeepAlive: false,
        isAffix: false,
        orderNo: undefined,
        status: 1,
        remark: null,
        children: [],
        isDelete: false,
        id: 1310000000712
      }
    },
    handleCreate() {
      this.resetTemp()
      this.dialogStatus = 'create'
      this.dialogFormVisible = true
      this.$nextTick(() => {
        this.$refs['dataForm'].clearValidate()
      })
    },
    createData() {
      this.$refs['dataForm'].validate((valid) => {
        if (valid) {
          // createRole(this.temp).then(() => {
          //   this.list.unshift(this.temp)
          //   this.dialogFormVisible = false
          //   this.$notify({
          //     title: '成功',
          //     message: '操作成功',
          //     type: 'success',
          //     duration: 2000
          //   })
          // })
        }
      })
    },
    handleUpdate(row) {
      this.temp = Object.assign({}, row) // copy obj
      // this.temp.timestamp = new Date(this.temp.timestamp)
      this.dialogStatus = 'update'
      this.dialogFormVisible = true
      this.$nextTick(() => {
        this.$refs['dataForm'].clearValidate()
      })
    },
    updateData() {
      this.$refs['dataForm'].validate((valid) => {
        if (valid) {
          // const tempData = Object.assign({}, this.temp)
          // tempData.timestamp = +new Date(tempData.timestamp) // change Thu Nov 30 2017 16:41:05 GMT+0800 (CST) to 1512031311464
          // updateRole(tempData.id, tempData).then(() => {
          //   const index = this.list.findIndex((v) => v.id === this.temp.id)
          //   this.list.splice(index, 1, this.temp)
          //   this.dialogFormVisible = false
          //   this.$notify({
          //     title: '成功',
          //     message: '操作成功',
          //     type: 'success',
          //     duration: 2000
          //   })
          // })
        }
      })
    },
    deleteData(row) {
      // deleteRole(row.id)
      //   .then((response) => {
      //     const index = this.list.findIndex((v) => v.id === row.id)
      //     this.list.splice(index, 1)
      //     this.$message({
      //       title: this.$i18n.t("TigerUi['Success']"),
      //       message: this.$i18n.t("TigerUi['SuccessMessage']"),
      //       type: 'success',
      //       duration: 2000
      //     })
      //   })
      //   .catch((err) => {
      //     console.log(err)
      //   })
    },
    submitForm(formName) {
      this.$refs[formName].validate((valid) => {
        if (valid) {
          alert('submit!')
        } else {
          console.log('error submit!!')
          return false
        }
      })
    },
    resetForm(formName) {
      this.$refs[formName].resetFields()
    }
  }
}
</script>

<style lang="css" scoped>

/*
.el-row {
    margin-bottom: 20px;

    &:last-child {
        margin-bottom: 0;
    }
}

.el-col {
    border-radius: 4px;
}

.bg-purple-dark {
    background: #99a9bf;
}

.grid-content {
    border-radius: 4px;
    min-height: 36px;
}

.row-bg {
    padding: 10px 0;
    background-color: #f9fafc;
}*/
</style>
