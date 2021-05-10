<template>
  <div class="app-container">
    <el-row style="margin-bottom:20px">
      <!-- <FilenameOption v-model="filename" />
      <AutoWidthOption v-model="autoWidth" />
      <BookTypeOption v-model="bookType" /> -->
      <router-link :to="'/setting/role/create'">
        <el-button type="primary" size="small" icon="el-icon-edit">
          添加
        </el-button>
      </router-link>
    </el-row>
    <el-table
      v-loading="listLoading"
      :data="list"
      element-loading-text="Loading"
      border
      fit
      highlight-current-row
    >
      <el-table-column align="center" label="ID" width="95">
        <template slot-scope="scope">
          {{ scope.$index }}
        </template>
      </el-table-column>
      <el-table-column label="名称" align="center" >
        <template slot-scope="scope">
          {{ scope.row.name }}
        </template>
      </el-table-column>
      <el-table-column label="是否默认" align="center" width="95">
        <template slot-scope="scope">
          {{ scope.row.isDefault == true? '是':'否' }}
        </template>
      </el-table-column>
      <el-table-column label="是否发布" align="center" width="95">
        <template slot-scope="scope">
          {{ scope.row.isPublish == true? '是':'否' }}
        </template>
      </el-table-column>
      <el-table-column label="是否静态" align="center" width="95">
        <template slot-scope="scope">
          {{ scope.row.isStatic == true? '是':'否' }}
        </template>
      </el-table-column>

      <el-table-column align="center" label="操作" width="200">
        <template slot-scope="scope">
          <router-link :to="'/setting/role/edit/'+scope.row.id">
            <el-button type="info" size="small"  icon="el-icon-edit" plain>
            </el-button>
          </router-link>
          <el-button type="info" size="small" @click="handlePermission(scope)"  plain>
            授权
          </el-button>
          <el-button type="danger" size="small"  icon="el-icon-delete" @click="deleteData(scope.row.id)" plain>
          </el-button>
        </template>
      </el-table-column>
    </el-table>
    <pagination
      v-show="total > 0"
      :total="total"
      :page.sync="listQuery.page"
      :limit.sync="listQuery.limit"
      @pagination="fetchData"
    />

    <el-dialog :visible.sync="dialogVisible" title='角色授权'>
      <el-form label-width="80px" label-position="left">
        <el-tabs tab-position="left">
        <el-tab-pane
          v-for="group in permissionData.groups"
          :key="group.name"
          :label="group.displayName"
        >
          <el-form-item :label="group.displayName">
            <el-tree
              ref="permissionTree"
              :data="transformPermissionTree(group.permissions)"
              :props="treeDefaultProps"
              show-checkbox
              check-strictly
              node-key="name"
              default-expand-all
            />
          </el-form-item>
        </el-tab-pane>
      </el-tabs>
      </el-form>
      <div style="text-align:right;">
        <el-button type="danger" @click="dialogVisible=false">取消</el-button>
        <el-button type="primary" @click="updatePermissionData()">确认</el-button>
      </div>
    </el-dialog>

  </div>
</template>

<script>
import { getRoleList,deleteRole,getPermissions,updatePermissions } from '@/api/user'
import Pagination from "@/components/Pagination"; // Secondary package based on el-pagination

export default {
  name: 'Role',
  props: {
    providerName: {
      type: String,
      required: false
    }
  },
  data() {
    return {
      list: null,
      listLoading: true,
      total: 0,
      listQuery: {
        page: 1,
        limit: 20,
        SkipCount: 0,
        // MaxResultCount: 1,
        Sorting: 'name desc'
      },
      dialogVisible:false,
      permissionData: {
        groups: []
      },
      treeDefaultProps: {
        children: 'children',
        label: 'label'
      },
      dialogPermissionFormVisible: false,
      permissionsQuery: { providerKey: '', providerName: 'R' }
    }
  },
  created() {
    this.fetchData()
    this.permissionsQuery.providerName = 'R'
  },
  methods: {
    fetchData() {
      this.listLoading = true
      getRoleList(this.listQuery).then(response => {
        this.list = response.items
        this.total = response.totalCount
        this.listLoading = false
      })
    },
    deleteData(id){
      console.log('delete')
      deleteRole(id).then(response => {
        this.$message({
              message: '删除成功',
              type: 'success'
            });
      }).catch(err => {
        console.log(err)
      })
    },
    handlePermission(scope) {
      this.dialogVisible = true
      // this.dialogPermissionFormVisible = true
      // console.log(row)
      // console.log(row.name)
      if (this.permissionsQuery.providerName === 'R') {
        this.permissionsQuery.providerKey = scope.row.name
      } else if (this.permissionsQuery.providerName === 'U') {
        this.permissionsQuery.providerKey = scope.row.id
      }
      // console.log(this.permissionsQuery)
      getPermissions(this.permissionsQuery).then(response => {
        // console.log(response)
        this.permissionData = response

        for (const i in this.permissionData.groups) {
          const keys = []
          const group = this.permissionData.groups[i]
          for (const j in group.permissions) {

            if (group.permissions[j].isGranted) {
              console.log(group.permissions[j])
              keys.push(group.permissions[j].name)
            }
          }

          // console.log(this.$refs['permissionTree'])
          this.$nextTick(() => {
            // console.log(this.$refs)
            // console.log(this.$refs.permissionTree)
            // console.log(keys)
            this.$refs.permissionTree[i].setCheckedKeys(keys)
          })
        }
      })
    },
    transformPermissionTree(permissions, name = null) {
      const arr = []
      if (!permissions || !permissions.some(v => v.parentName === name)) { return arr }

      const parents = permissions.filter(v => v.parentName === name)
      for (const i in parents) {
        let label = ''
        if (this.permissionsQuery.providerName === 'R') {
          label = parents[i].displayName
        } else if (this.permissionsQuery.providerName === 'U') {
          label =
            parents[i].displayName +
            ' ' +
            parents[i].grantedProviders.map(provider => {
              return `${provider.providerName}: ${provider.providerKey}`
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
    isGrantedByOtherProviderName(grantedProviders) {
      if (grantedProviders.length) {
        return (
          grantedProviders.findIndex(
            p => p.providerName !== this.permissionsQuery.providerName
          ) > -1
        )
      }
      return false
    },
    updatePermissionData() {
      const tempData = []
      for (const i in this.permissionData.groups) {
        const keys = this.$refs.permissionTree[i].getCheckedKeys()
        const group = this.permissionData.groups[i]
        for (const j in group.permissions) {
          if (
            group.permissions[j].isGranted &&
            !keys.some(v => v === group.permissions[j].name)
          ) {
            tempData.push({
              isGranted: false,
              name: group.permissions[j].name
            })
          } else if (
            !group.permissions[j].isGranted &&
            keys.some(v => v === group.permissions[j].name)
          ) {
            tempData.push({ isGranted: true, name: group.permissions[j].name })
          }
        }
      }
      updatePermissions(this.permissionsQuery, { permissions: tempData }).then(
        () => {
          this.dialogPermissionFormVisible = false
          this.$message({
            message: '权限添加成功',
            type: 'success'
          });
          // fetchAppConfig(
          //   this.permissionsQuery.providerKey,
          //   this.permissionsQuery.providerName
          // )
        }
      )
    },
  }
}
</script>
