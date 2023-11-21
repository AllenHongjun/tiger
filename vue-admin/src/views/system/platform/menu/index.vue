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

    <el-table ref="dataTreeList" :data="list" style="width: 100%;margin-bottom: 20px;" row-key="id" border :default-expand="false" :tree-props="{children: 'children', hasChildren: 'hasChildren'}">
      <el-table-column type="index" width="80" />
      <el-table-column :label="$t('AppPlatform[\'DisplayName:Name\']')" prop="name" align="left">
        <template slot-scope="{ row }">
          <span>{{ row.name }}</span>
        </template>
      </el-table-column>
      <el-table-column prop="icon" label="图标" width="180">
        <template slot-scope="{row}">
          <!-- 绑定属性的值 在属性上不需要添加大括号 -->
          <i :class="row.icon" style="margin-right:10px;" />
          <span>{{ row.icon }}</span>
        </template>
      </el-table-column>
      <el-table-column :label="$t('AppPlatform[\'DisplayName:DisplayName\']')" prop="displayName" align="left">
        <template slot-scope="{ row }">
          <span>{{ row.displayName }}</span>
        </template>
      </el-table-column>
      <el-table-column :label="$t('AppPlatform[\'DisplayName:Path\']')" prop="path" align="left">
        <template slot-scope="{ row }">
          <span>{{ row.path }}</span>
        </template>
      </el-table-column>
      <el-table-column :label="$t('AppPlatform[\'DisplayName:Redirect\']')" prop="redirect" align="left">
        <template slot-scope="{ row }">
          <span>{{ row.redirect }}</span>
        </template>
      </el-table-column>
      <el-table-column :label="$t('AppPlatform[\'DisplayName:Component\']')" prop="component" align="left">
        <template slot-scope="{ row }">
          <span>{{ row.component }}</span>
        </template>
      </el-table-column>
      <!-- <el-table-column :label="$t('AppPlatform[\'DisplayName:Sort\']')" prop="sort"  align="left">
        <template slot-scope="{ row }">
          <span>{{ row.sort }}</span>
        </template>
      </el-table-column> -->
      <el-table-column prop="status" label="状态" width="80">
        <template slot-scope="{row}">
          <el-tag :type="row.status | statusFilter">
            {{ row.status == true ? '启用' : '禁用' }}
          </el-tag>
        </template>
      </el-table-column>
      <!-- <el-table-column prop="isPublic" label="公用" width="80">
        <template slot-scope="{row}">
          <el-tag :type="row.isPublic | statusFilter">
            {{ row.isPublic == '1' ? '是' : '否' }}
          </el-tag>
        </template>
      </el-table-column> -->

      <el-table-column align="left" :label="$t('AbpUi[\'Actions\']')" width="180">
        <template slot-scope="scope">
          <el-button v-if="checkPermission('Platform.Menu.Update')" type="primary" @click="handleUpdate(scope.row)">
            {{ $t("AbpUi['Edit']") }}
          </el-button>

          <el-button v-if="checkPermission('Platform.Menu.Delete')" type="danger" @click="deleteData(scope.row)">
            {{ $t("AbpUi['Delete']") }}
          </el-button>

        </template>
      </el-table-column>
    </el-table>

    <el-dialog :title="dialogStatus == 'create'? '创建': $t('AbpUi[\'Edit\']')" :visible.sync="dialogFormVisible">

      <el-row>
        <el-form ref="dataForm" :model="dataForm" :rules="rules" label-width="100px">
          <el-row>
            <el-col :span="12">
              <el-form-item label="上级菜单" prop="pid" width="400px">
                <el-cascader v-model="dataForm.parentId" placeholder="请选择上级菜单" :options="menuOptions" :props="{ checkStrictly: true ,emitPath:false}" @change="handleChange" />
              </el-form-item>
            </el-col>
            <el-col :span="12">
              <div class="grid-content bg-purple-light">
                <!-- 布局添加后不允许修改 -->
                <el-form-item v-if="dialogStatus === 'create'" :label="$t('AppPlatform[\'DisplayName:Layout\']')" prop="layoutId">
                  <el-select v-model="dataForm.layoutId" placeholder="请选择">
                    <el-option v-for="item in LayoutOptions" :key="item.id" :label="item.name" :value="item.id" />
                  </el-select>
                </el-form-item>
              </div>
            </el-col>
          </el-row>

          <el-row>
            <el-col :span="12">
              <div class="grid-content bg-purple">
                <el-form-item :label="$t('AppPlatform[\'DisplayName:Name\']')" placeholder="菜单名称" prop="name">
                  <el-input v-model="dataForm.name" />
                </el-form-item>
              </div>
            </el-col>
            <el-col :span="12">
              <div class="grid-content bg-purple-light">
                <el-form-item :label="$t('AppPlatform[\'DisplayName:DisplayName\']')" prop="displayName">
                  <el-input v-model="dataForm.displayName" />
                </el-form-item>
              </div>
            </el-col>
          </el-row>

          <el-row>
            <el-col :span="12">
              <div class="grid-content bg-purple">
                <el-form-item :label="$t('AppPlatform[\'DisplayName:Path\']')" placeholder="路由路径" prop="path">
                  <el-input v-model="dataForm.path" />
                </el-form-item>
              </div>
            </el-col>
            <el-col :span="12">
              <div class="grid-content bg-purple-light">
                <el-form-item :label="$t('AppPlatform[\'DisplayName:Redirect\']')" aria-placeholder="重定向" prop="redirect">
                  <el-input v-model="dataForm.redirect" />
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
                <el-form-item :label="$t('AppPlatform[\'DisplayName:Component\']')" aria-placeholder="组件路径" prop="component">
                  <el-input v-model="dataForm.component" />
                </el-form-item>
              </div>
            </el-col>
          </el-row>

          <el-row>
            <el-col :span="12">
              <div class="grid-content bg-purple">
                <el-form-item :label="$t('AppPlatform[\'DisplayName:IsPublic\']')" prop="isPublic">
                  <template>
                    <el-radio v-model="dataForm.isPublic" :label="true">是</el-radio>
                    <el-radio v-model="dataForm.isPublic" :label="false">否</el-radio>
                  </template>
                </el-form-item>
              </div>
            </el-col>
            <el-col :span="12">
              <div class="grid-content bg-purple">
                <el-form-item :label="$t('AppPlatform[\'DisplayName:Status\']')" prop="status">
                  <template>
                    <el-radio v-model="dataForm.status" :label="true">启用</el-radio>
                    <el-radio v-model="dataForm.status" :label="false">禁用</el-radio>
                  </template>
                </el-form-item>
              </div>
            </el-col>

          </el-row>

          <el-row>
            <el-col :span="12">
              <div class="grid-content bg-purple-light">
                <el-form-item label="菜单排序" aria-placeholder="菜单排序" prop="orderNo">
                  <el-input-number v-model="num" :min="1" :max="10000000" label="菜单排序" />
                </el-form-item>
              </div>
            </el-col>
          </el-row>

          <el-form-item :label="$t('AppPlatform[\'DisplayName:Description\']')" prop="description">
            <el-input v-model="dataForm.description" type="textarea" />
          </el-form-item>
        </el-form>
      </el-row>
      <div style="text-align: right">
        <el-button type="danger" @click="dialogFormVisible = false">{{ $t("AbpIdentity['Cancel']") }}</el-button>
        <el-button type="primary" @click="dialogStatus === 'create' ? createData() : updateData()">{{ $t("AbpIdentity['Save']") }}</el-button>
      </div>
    </el-dialog>

  </div>
</template>

<script>

// TODO: 优化CRUD操作刷新改成前端刷新不用 getList()
import {
  getMenus,
  getMenu,
  getAllMenu,
  createMenu,
  updateMenu,
  deleteMenu

} from '@/api/system-manage/platform/menu'
import { getAllLayout } from '@/api/system-manage/platform/layout'
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
        true: 'success',
        false: 'danger'
      }
      return statusMap[status]
    }
  },
  data() {
    return {
      value: [],
      menuOptions: [],
      tableData: [],
      tableKey: 0,
      list: [],
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
      LayoutOptions: [],
      dataForm: {
        id: undefined,
        path: '',
        name: '',
        displayName: '',
        description: '',
        redirect: '',
        icon: '',
        meta: {},
        code: '',
        component: '',
        framework: '',
        parentId: undefined,
        layoutId: undefined,
        isPublic: true,
        status: true,
        startup: true
      },

      rules: {
        name: [
          {
            required: true,
            // 表单验证可以 扩展 AbpValidation 这个基类的资源，添加需要的验证
            message: this.$i18n.t("AbpValidation['The {0} field is required.']", [
              this.$i18n.t("AppPlatform['DisplayName:Name']")
            ]),
            trigger: 'blur'
          },
          {
            max: 30,
            message: this.$i18n.t(
              "AbpValidation['The field {0} must be a string with a maximum length of {1}.']",
              [this.$i18n.t("AppPlatform['DisplayName:Name']"), '30']
            ),
            trigger: 'blur'
          }
        ],
        displayName: [
          {
            required: true,
            // 表单验证可以 扩展 AbpValidation 这个基类的资源，添加需要的验证
            message: this.$i18n.t("AbpValidation['The {0} field is required.']", [
              this.$i18n.t("AppPlatform['DisplayName:DisplayName']")
            ]),
            trigger: 'blur'
          },
          {
            max: 128,
            message: this.$i18n.t(
              "AbpValidation['The field {0} must be a string with a maximum length of {1}.']",
              [this.$i18n.t("AppPlatform['DisplayName:DisplayName']"), '128']
            ),
            trigger: 'blur'
          }
        ],
        path: [
          {
            required: true,
            // 表单验证可以 扩展 AbpValidation 这个基类的资源，添加需要的验证
            message: this.$i18n.t("AbpValidation['The {0} field is required.']", [
              this.$i18n.t("AppPlatform['DisplayName:Path']")
            ]),
            trigger: 'blur'
          },
          {
            max: 256,
            message: this.$i18n.t(
              "AbpValidation['The field {0} must be a string with a maximum length of {1}.']",
              [this.$i18n.t("AppPlatform['DisplayName:Path']"), '128']
            ),
            trigger: 'blur'
          }
        ],
        redirect: [
          {
            max: 256,
            message: this.$i18n.t(
              "AbpValidation['The field {0} must be a string with a maximum length of {1}.']",
              [this.$i18n.t("AppPlatform['DisplayName:Redirect']"), '128']
            ),
            trigger: 'blur'
          }
        ],
        component: [
          {
            required: true,
            // 表单验证可以 扩展 AbpValidation 这个基类的资源，添加需要的验证
            message: this.$i18n.t("AbpValidation['The {0} field is required.']", [
              this.$i18n.t("AppPlatform['DisplayName:Component']")
            ]),
            trigger: 'blur'
          },
          {
            max: 256,
            message: this.$i18n.t(
              "AbpValidation['The field {0} must be a string with a maximum length of {1}.']",
              [this.$i18n.t("AppPlatform['DisplayName:Component']"), '128']
            ),
            trigger: 'blur'
          }
        ],
        description: [
          {
            max: 1024,
            message: this.$i18n.t(
              "AbpValidation['The field {0} must be a string with a maximum length of {1}.']",
              [this.$i18n.t("AppPlatform['DisplayName:DisplayName']"), '1024']
            ),
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
  created() {
    this.getList()
    this.getMenuSelect()
  },
  methods: {
    checkPermission,
    getList() {
      this.listLoading = true
      getAllMenu(this.listQuery).then(response => {
        // TODO:封装帮助方法 list-to-tree 封装到utils当中去
        this.list = this.buildTree(response.items, null)
        this.listLoading = false
      })
    },
    // 把数组转换成tree
    listToTree(list) {
      const tree = []
      for (const node of list) {
        // 如果没有pid就可以认为是根节点
        if (!node.parentId) {
          const p = { ...node }
          p.children = getChildren(p.id, list)
          tree.push(p)
        }
      }
      function getChildren(id, list) {
        const children = []
        for (const node of list) {
          if (node.parentId === id) {
            children.push(node)
          }
        }
        for (const node of children) {
          const children = getChildren(node.id, list)
          if (children.length) {
            node.children = children
          }
        }
        return children
      }
      return tree
    },
    buildTree(list, parentId) {
      const tree = []
      for (let i = 0; i < list.length; i++) {
        if (list[i].parentId === parentId) {
          const node = Object.assign({}, list[i])
          node.children = this.buildTree(list, list[i].id)
          // 如果children 为空数组，组件显示空白页，不好看
          if (node.children.length < 1) { node.children = undefined }
          tree.push(node)
        }
      }
      return tree
    },
    // 获取父级菜单选择数据
    getMenuSelect() {
      getAllMenu(this.listQuery).then(response => {
        this.menuOptions = this.TurnTree(response.items, null)
      })
    },
    TurnTree(list, parentId) {
      const tree = []
      for (let i = 0; i < list.length; i++) {
        if (list[i].parentId === parentId) {
          const node = {
            value: list[i].id,
            label: list[i].name
          }
          node.children = this.TurnTree(list, list[i].id)
          if (node.children.length < 1) { node.children = undefined }
          tree.push(node)
        }
      }
      return tree
    },
    getLayoutData() {
      getAllLayout().then(response => {
        this.LayoutOptions = response.items
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
      this.parentId = value
    },
    // 切换数据表格树形展开
    toggleRowExpansion() {
      this.isExpansion = !this.isExpansion
      this.toggleRowExpansionAll(this.list, this.isExpansion)
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
        id: undefined,
        path: '',
        name: '',
        displayName: '',
        description: '',
        redirect: '',
        icon: '',
        meta: {},
        code: '',
        component: '',
        framework: '',
        parentId: undefined,
        layoutId: undefined,
        isPublic: true,
        status: true,
        startup: true
      }
    },
    handleCreate() {
      this.resetTemp()
      this.getMenuSelect()
      this.getLayoutData()
      this.dialogStatus = 'create'
      this.dialogFormVisible = true
      this.$nextTick(() => {
        this.$refs['dataForm'].clearValidate()
      })
    },
    createData() {
      this.$refs['dataForm'].validate((valid) => {
        if (valid) {
          createMenu(this.dataForm).then(() => {
            // this.list.unshift(this.dataForm)
            this.getList()
            this.dialogFormVisible = false
            this.$notify({
              title: this.$i18n.t("TigerUi['Success']"),
              message: this.$i18n.t("TigerUi['SuccessMessage']"),
              type: 'success',
              duration: 2000
            })
          })
        }
      })
    },
    handleUpdate(row) {
      this.getMenuSelect()
      this.getLayoutData()
      this.dataForm = Object.assign({}, row) // copy obj
      this.dialogStatus = 'update'
      this.dialogFormVisible = true
      this.$nextTick(() => {
        this.$refs['dataForm'].clearValidate()
      })
    },
    updateData() {
      this.$refs['dataForm'].validate((valid) => {
        if (valid) {
          const tempData = Object.assign({}, this.dataForm)
          updateMenu(tempData.id, tempData).then(() => {
            this.getList()
            this.dialogFormVisible = false
            this.$notify({
              title: this.$i18n.t("TigerUi['Success']"),
              message: this.$i18n.t("TigerUi['SuccessMessage']"),
              type: 'success',
              duration: 2000
            })
          })
        }
      })
    },
    deleteData(row) {
      deleteMenu(row.id)
        .then((response) => {
          this.getList()
          this.$message({
            title: this.$i18n.t("TigerUi['Success']"),
            message: this.$i18n.t("TigerUi['SuccessMessage']"),
            type: 'success',
            duration: 2000
          })
        })
        .catch((err) => {
          console.log(err)
        })
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
