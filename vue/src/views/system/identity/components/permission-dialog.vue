<template>
  <!-- permissionsQuery.providerKey -->
  <el-dialog :title="$t('AbpIdentity.PermissionGrant')" :visible.sync="dialogPermissionFormVisible" top="7vh">
    <!-- `checked` 为 true 或 false -->
    <el-checkbox v-model="allPermissionChecked" @change="toggleCheckAll">{{ $t('AbpIdentity.GrantAllPermissions') }}</el-checkbox>
    <!-- <el-button type="primary" icon="el-icon-sort" class="filter-item" style="margin-left: 10px;" @click="toggleRowExpansion">全部{{ isExpansion ? "收缩" : "展开" }}</el-button> -->
    <el-divider />
    <el-form label-position="top" style="min-height:500px;">
      <el-tabs v-model="activeName" tab-position="left">
        <el-tab-pane v-for="group in permissionData.groups" :key="group.name" :label="group.displayName">
          <el-checkbox v-model="groupAllPermissionChecked" @change="toggleCheckGroupAll">{{ $t('AbpUi.SelectAll') }}</el-checkbox>
          <el-divider />
          <el-form-item :label="group.displayName">
            <el-tree ref="permissionTree" :data="transformPermissionTree(group.permissions)" :props="treeDefaultProps" show-checkbox node-key="name" :default-expand-all="true" />
          </el-form-item>
        </el-tab-pane>
      </el-tabs>
    </el-form>
    <div slot="footer" class="dialog-footer">
      <el-button @click="dialogPermissionFormVisible = false">
        {{ $t("AbpIdentity['Cancel']") }}
      </el-button>
      <el-button type="primary" @click="updatePermissionData()">
        {{ $t("AbpIdentity['Save']") }}
      </el-button>
    </div>
  </el-dialog>
</template>

<script>
import {
  getPermissions,
  updatePermissions
} from '@/api/system-manage/identity/permission'
import {
  fetchAppConfig
} from '@/utils/abp'

export default {
  name: 'PermissionDialog',
  props: {
    providerName: {
      type: String,
      required: true
    }
  },
  data() {
    return {
      isExpansion: false,
      allPermissionChecked: false,
      groupAllPermissionChecked: false,
      activeName: '',
      permissionData: {
        groups: []
      }, // 所有权限数据
      treeDefaultProps: {
        children: 'children',
        label: 'label'
      },
      dialogPermissionFormVisible: false,
      permissionsQuery: {
        providerKey: '',
        providerName: ''
      }
    }
  },
  created() {
    this.permissionsQuery.providerName = this.providerName
  },
  methods: {
    // 切换展开收合
    toggleRowExpansion() {
      for (const i in this.permissionData.groups) {
        const keys = []
        const group = this.permissionData.groups[i]
        for (const j in group.permissions) {
          keys.push(group.permissions[j].name)
        }
        console.log("this.$refs['permissionTree'][i]", this.$refs['permissionTree'][i])
        this.$refs['permissionTree'][i].expanded = false
      }
    },

    toggleCheckAll() {
      if (this.allPermissionChecked) {
        for (const i in this.permissionData.groups) {
          const keys = []
          const group = this.permissionData.groups[i]
          for (const j in group.permissions) {
            keys.push(group.permissions[j].name)
          }
          this.$refs['permissionTree'][i].setCheckedKeys(keys)
        }
      } else {
        for (const i in this.permissionData.groups) {
          this.$refs['permissionTree'][i].setCheckedKeys([])
        }
      }
    },
    toggleCheckGroupAll() {
      var i = this.activeName
      // 获取全局分组的id
      if (this.groupAllPermissionChecked) {
        const keys = []
        const group = this.permissionData.groups[i]
        for (const j in group.permissions) {
          keys.push(group.permissions[j].name)
        }
        this.$refs['permissionTree'][i].setCheckedKeys(keys)
      } else {
        this.$refs['permissionTree'][i].setCheckedKeys([])
      }
    },
    handleUpdatePermission(row) {
      this.dialogPermissionFormVisible = true
      if (this.permissionsQuery.providerName === 'R') {
        this.permissionsQuery.providerKey = row.name
      } else if (this.permissionsQuery.providerName === 'U') {
        this.permissionsQuery.providerKey = row.id
      }

      getPermissions(this.permissionsQuery).then(response => {
        // 绑定权限数据
        this.permissionData = response

        for (const i in this.permissionData.groups) {
          const checkedKeys = []
          const group = this.permissionData.groups[i]
          for (const j in group.permissions) {
            // 如果不是子权限，并且所有子权限isGrand = true 才选中
            if (group.permissions[j].parentName == null && group.permissions.every(v => v.isGranted === true)) {
              checkedKeys.push(group.permissions[j].name)
            }

            if (group.permissions[j].parentName !== null && group.permissions[j].isGranted) {
              checkedKeys.push(group.permissions[j].name)
            }
          }

          // 修改数据后立刻得到更新后的DOM结构，可以使用Vue.nextTick()
          this.$nextTick(() => {
            this.$refs['permissionTree'][i].setCheckedKeys(checkedKeys)
            // this.$refs['permissionTree'][i].setHalfCheckedKeys(halfCheckedKeys)
          })
        }
      })
    },
    transformPermissionTree(permissions, name = null) {
      const arr = []
      if (!permissions || !permissions.some(v => v.parentName === name)) {
        return arr
      }

      const parents = permissions.filter(v => v.parentName === name)
      for (const i in parents) {
        let label = ''
        if (this.permissionsQuery.providerName === 'R') {
          label = parents[i].displayName
        } else if (this.permissionsQuery.providerName === 'U') {
          label = parents[i].displayName +
                  ' ' +
                  parents[i].grantedProviders.map(provider => {
                    // return `${provider.providerName}: ${provider.providerKey}`
                    return `${provider.providerName}`
                  })
        }
        arr.push({
          name: parents[i].name,
          label,
          disabled: this.isGrantedByOtherProviderName(
            parents[i].grantedProviders
          ),
          children: this.transformPermissionTree(permissions, parents[i].name)
        })
      }
      return arr
    },
    updatePermissionData() {
      const tempData = []
      for (const i in this.permissionData.groups) {
        const checkedKeys = this.$refs['permissionTree'][i].getCheckedKeys()
        // Bugfix: 增加返回半选的节点
        const haleCheckedKeys = this.$refs['permissionTree'][i].getHalfCheckedKeys()
        const keys = [].concat(checkedKeys, haleCheckedKeys)

        const group = this.permissionData.groups[i]
        for (const j in group.permissions) {
          if (group.permissions[j].isGranted && !keys.some(v => v === group.permissions[j].name)) {
            tempData.push({
              isGranted: false,
              name: group.permissions[j].name
            })
          } else if (!group.permissions[j].isGranted && keys.some(v => v === group.permissions[j].name)) {
            tempData.push({
              isGranted: true,
              name: group.permissions[j].name
            })
          }
        }
      }
      updatePermissions(this.permissionsQuery, { permissions: tempData }).then(() => {
        this.dialogPermissionFormVisible = false

        this.$notify({
          title: '成功',
          message: '操作成功',
          type: 'success',
          duration: 2000
        })

        fetchAppConfig(
          this.permissionsQuery.providerKey,
          this.permissionsQuery.providerName
        )
      })
    },
    isGrantedByOtherProviderName(grantedProviders) {
      if (grantedProviders.length) {
        return (
          grantedProviders.findIndex(
            p => p.providerName !== this.permissionsQuery.providerName
          ) > -1
        )
      }
      return false
    }
  }
}
</script>

<style lang="scss" scoped>
.el-form-item {
    height: 450px;
    overflow-y: scroll;
}
</style>
