<template>
  <div class="app-container" style="padding:0 20px;">
    <div class="filter-container">
      <el-row :gutter="20">
        <el-col :span="16">
          <el-input v-model="filterText" placeholder="查询组织" />
        </el-col>
        <el-col :span="8">
          <div class="grid-content">
            <el-button type="primary" @click="() => handleCreate()">添加根机构</el-button>
          </div>
        </el-col>
      </el-row>
    </div>
    <el-tree
      ref="orgTree"
      :data="orgTreeData"
      :props="orgTreeProps"
      :filter-node-method="filterOrg"
      :expand-on-click-node="false"
      :show-checkbox="showCheckbox"
      :check-strictly="true"
      node-key="id"
      highlight-current
      :default-expand-all="true"
      style="height: 500px;"
      @node-click="handleOrgClick"
      @check-change="checkChange"
    >
      <span slot-scope="{ node, data }" class="custom-tree-node">
        <span>{{ node.label }}</span>
        <span>
          <el-button type="text" @click="() => handleUpdate(data)">
            {{ $t('AbpUi[\'Edit\']') }}
          </el-button>
          <el-button type="text" @click="() => handleCreate(data)">
            创建子组织
          </el-button>
          <el-button type="text" @click="() => remove(node, data)">
            {{ $t('AbpUi[\'Delete\']') }}
          </el-button>
        </span>
      </span>
    </el-tree>

    <el-dialog
      :title="dialogStatus == 'create'? $t('AbpIdentity[\'AddClaim\']'): $t('AbpUi[\'Edit\']')"
      :visible.sync="dialogFormVisible"
      append-to-body
    >
      <el-form ref="dataForm" :rules="rules" :model="temp" label-position="right" label-width="150px">
        <el-form-item v-if="currentParentName!==''" :label="$t('AbpIdentity[\'OrganizationUnit:ParentDisplayName\']')">
          <el-input v-model="currentParentName" disabled />
        </el-form-item>

        <!-- Form 组件提供了表单验证的功能，只需要通过 rules 属性传入约定的验证规则，并将 Form-Item 的 prop 属性设置为需校验的字段名即可。校验规则参见 async-validator https://github.com/yiminghe/async-validator prop 字段的大小写需要保持一致 -->
        <el-form-item :label="$t('AbpIdentity[\'OrganizationUnit:DisplayName\']')" prop="displayName">
          <el-input v-model="temp.displayName" />
        </el-form-item>

      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button @click="dialogFormVisible = false">
          {{ $t("AbpUi['Cancel']") }}
        </el-button>
        <el-button type="primary" @click="dialogStatus === 'create' ||dialogStatus === 'createChild' ? createData() : updateData()">
          {{ $t("AbpUi['Save']") }}
        </el-button>
      </div>
    </el-dialog>
  </div>
</template>

<script>
import {
  createOrganization,
  updateOrganization,
  deleteOrganization,
  getOrganizationsAllTree
} from '@/api/system-manage/identity/organization-unit'
import {
  Tree
} from 'element-ui'
export default {
  name: 'OrgTree',
  mixins: [Tree],
  props: {
    supportSingleChecked: {
      type: Boolean,
      default: false
    },
    orgTreeNodeClick: {
      type: Function,
      default: () => {}
    }
  },
  data() {
    return {
      orgTreeData: null,
      checkStrictly: true,
      orgTreeProps: {
        label: 'displayName',
        children: 'children',
        disabled: '',
        isLeaf: 'isLeaf'
      },
      treeQuery: {
        sort: 'code',
        filter: undefined
      },
      filterText: '',
      currentParentName: '',
      temp: {
        parentId: undefined,
        displayName: ''
      },
      dialogFormVisible: false,
      dialogStatus: '',

      // 表单验证规则
      rules: {
        displayName: [{
          required: true,
          message: this.$i18n.t("AbpIdentity['The {0} field is required.']", [
            this.$i18n.t("AbpIdentity['OrganizationUnit:DisplayName']")
          ]),
          trigger: 'blur'
        },
        {
          // 在abp表单验证模块里面自带的配置规则
          max: 256,
          message: this.$i18n.t(
            "AbpIdentity['The field {0} must be a string with a maximum length of {1}.']",
            [this.$i18n.t("AbpIdentity['OrganizationUnit:DisplayName']"), '256']
          ),
          trigger: 'blur'
        }
        ]
      }
    }
  },
  watch: {
    filterText(val) {
      this.treeQuery.filter = val
      this.$refs.orgTree.filter(val)
    }
  },
  created() {
    this.getOrgs()
  },
  methods: {
    getOrgs() {
      getOrganizationsAllTree(this.treeQuery).then(response => {
        this.orgTreeData = response.items
      })
    },
    resetTemp() {
      this.temp = {
        parentId: undefined,
        displayName: ''
      }
    },
    // 点击创建按钮
    handleCreate(row) {
      this.resetTemp()
      // row 存放当前点击节点的信息
      if (!row || !row.id) {
        this.dialogStatus = 'create'
        this.currentParentName = ''
      } else {
        this.dialogStatus = 'createChild'
        this.temp.parentId = row.id
        this.currentParentName = `${row.displayName}(${row.code})`
      }
      this.dialogFormVisible = true
      this.$nextTick(() => {
        this.$refs['dataForm'].clearValidate()
      })
    },

    // 创建数据
    createData() {
      this.$refs['dataForm'].validate(valid => {
        if (valid) {
          createOrganization(this.temp).then((res) => {
            this.handleRefresh()
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
      const {
        id,
        displayName,
        concurrencyStamp
      } = row
      this.currentId = id
      this.temp.displayName = displayName
      this.temp.concurrencyStamp = concurrencyStamp

      this.dialogStatus = 'update'
      this.dialogFormVisible = true

      this.$nextTick(() => {
        this.$refs['dataForm'].clearValidate()
      })
    },

    updateData() {
      this.$refs['dataForm'].validate(valid => {
        if (valid) {
          updateOrganization(this.currentId, this.temp).then((res) => {
            // TODO:修改为前端添加节点刷新
            // 如果数据比较多 默认不展开全部 就无法看到效果
            this.handleRefresh()
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

    remove(node, data) {
      this.$confirm(
        this.$i18n.t("AbpIdentity['OrganizationUnit:WillDelete']", [
          data.displayName
        ]),
        this.$i18n.t("AbpIdentity['AreYouSure']"), {
          confirmButtonText: this.$i18n.t("AbpIdentity['Yes']"),
          cancelButtonText: this.$i18n.t("AbpIdentity['Cancel']"),
          type: 'warning'
        }
      ).then(async() => {
        deleteOrganization(node.key).then(() => {
          // tree组件中找到这个节点并且移除
          const parent = node.parent
          const children = parent.data.children || parent.data
          const index = children.findIndex(d => d.id === data.id)
          children.splice(index, 1)
          this.$notify({
            title: this.$i18n.t("TigerUi['Success']"),
            message: this.$i18n.t("TigerUi['SuccessMessage']"),
            type: 'success',
            duration: 2000
          })
        })
      })
    },
    handleRefresh() {
      this.getOrgs()
    },
    handleOrgClick(data) {
      this.orgTreeNodeClick(data)
    },
    filterOrg(value, data) {
      if (!value) return true
      return data.displayName.indexOf(value) !== -1
    },
    checkChange(data, checked, indeterminate) {
      // 单个
      let keys = ''
      if (this.supportSingleChecked) {
        if (checked) {
          // console.log(data, checked, indeterminate)
          keys = this.$refs.orgTree.getCheckedKeys()
          if (keys.length > 1) {
            this.$refs.orgTree.setCheckedKeys([])
            this.$refs.orgTree.setChecked(data, true)
            keys = this.$refs.orgTree.getCheckedKeys()
          }
          // console.log('单个-keys:', keys)
          this.$emit('handleCheckChange', data, keys)
        }
      } else {
        keys = this.$refs.orgTree.getCheckedKeys()
        // console.log('多个-keys:', keys)
        this.$emit('handleCheckChange', data, keys)
      }
    },
    handleDragStart(node, ev) {
      console.log('drag start', node)
    },
    handleDragEnter(draggingNode, dropNode, ev) {
      console.log('tree drag enter: ', dropNode.label)
    },
    handleDragLeave(draggingNode, dropNode, ev) {
      console.log('tree drag leave: ', dropNode.label)
    },
    handleDragOver(draggingNode, dropNode, ev) {
      console.log('tree drag over: ', dropNode.label)
    },
    handleDragEnd(draggingNode, dropNode, dropType, ev) {
      console.log('tree drag end: ', dropNode && dropNode.label, dropType)
    },
    handleDrop(draggingNode, dropNode, dropType, ev) {
      console.log('tree drop: ', dropNode.label, dropType)
    }
    // allowDrop(draggingNode, dropNode, type) {
    //     if (dropNode.data.label === '二级 3-1') {
    //         return type !== 'inner';
    //     } else {
    //         return true;
    //     }
    // },
    // allowDrag(draggingNode) {
    //     return draggingNode.data.label.indexOf('三级 3-2-2') === -1;
    // },
  }
}
</script>

<style lang="scss" scoped>
.app-container{
  overflow-y: scroll;
}
.custom-tree-node {
    flex: 1;
    display: flex;
    align-items: center;
    justify-content: space-between;
    font-size: 14px;
    padding-right: 8px;
}
</style>
