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
      <el-table-column type="index" width="80" />
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
      <el-table-column align="left" :label="$t('AbpUi[\'Actions\']')" width="320">
        <template>
          <el-button v-if="checkPermission('Platform.Menu.Update')" type="primary" @click="handleUpdate()">
            {{ $t("AbpUi['Edit']") }}
          </el-button>

          <el-button v-if="checkPermission('Platform.Menu.Delete')" type="danger">
            {{ $t("AbpUi['Delete']") }}
          </el-button>

        </template>
      </el-table-column>
    </el-table>

    <el-dialog :title="dialogStatus == 'create'? '创建': $t('AbpUi[\'Edit\']')" :visible.sync="dialogFormVisible">

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
import {
  getMenus,
  getMenu,
  getMenusAll,
  createMenu,
  updateMenu,
  deleteMenu
} from '@/api/system-manage/platform/layout'
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
      tableData: [],
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
        id: undefined,
        path: '',
        name: '',
        displayName: '',
        description: '',
        redirect: '',
        meta: {},
        code: '',
        component: '',
        framework: '',
        parentId: undefined,
        layoutId: undefined,
        isPublic: true,
        startup: true
      },

      rules: {
        name: [
          {
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
        path: [
          {
            required: true,
            message: '请输入路由路径',
            trigger: 'blur'
          }
        ],
        pathName: [
          {
            required: true,
            message: '请输入路由名称',
            trigger: 'blur'
          }
        ],
        component: [
          {
            required: true,
            message: '请输入组件路径',
            trigger: 'blur'
          }
        ]
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
    getList() {
      this.listLoading = true
      getMenusAll(this.listQuery).then(response => {
        this.list = response.items.filter(_ => _.pid == null)
        // TODO:封装帮助方法 list-to-tree
        this.setChildren(this.list, response.items)
        this.listLoading = false
      })
    },
    setChildren(roots, items) {
      roots.forEach(element => {
        items.forEach(item => {
          if (item.pid === element.id) {
            if (!element.children) { element.children = [] }
            element.children.push(item)
          }
        })
        if (element.children) {
          this.setChildren(element.children, items)
        }
      })
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
