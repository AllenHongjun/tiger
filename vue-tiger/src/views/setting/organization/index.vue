<template>
  <div class="app-container">
    <div class="filter-container">
      <!-- <el-input
        v-model="listQuery.filter"
        :placeholder="$t('AbpUi[\'PagerSearch\']')"
        style="width: 200px;"
        class="filter-item"
        @keyup.enter.native="handleRefresh"
      /> -->
      <el-button v-if="checkPermission('AbpIdentity.OrganitaionUnits.Create')" class="filter-item" style="margin-left: 10px;" type="primary" icon="el-icon-plus" @click="handleCreate">
        新增
      </el-button>
    </div>
    <el-row :gutter="20">
      <el-col :span="6">
        <div class="grid-content bg-purple">
          <div class="custom-tree-container">
            <!-- <div class="block">
              <p>使用 render-content</p>
              <el-tree :data="data" show-checkbox node-key="id" default-expand-all :expand-on-click-node="false" :render-content="renderContent" />
            </div> -->
            <div class="block">
              <p>组织树
                <el-link type="warning" size="mini" style="float:right;diplay:inline-block;">
                  添加根组织
                </el-link>
              </p>
              <el-tree :data="data" show-checkbox node-key="id" default-expand-all :expand-on-click-node="false">
                <span slot-scope="{ node, data }" class="custom-tree-node">
                  <span>{{ node.label }}</span>
                  <span>
                    <el-button type="text" size="mini" @click="() => append(data)">
                      添加
                    </el-button>
                    <el-button type="text" size="mini" @click="() => append(data)">
                      编辑
                    </el-button>
                    <el-button type="text" size="mini" @click="() => remove(node, data)">
                      删除
                    </el-button>
                  </span>
                </span>
              </el-tree>
            </div>
          </div>
        </div>
      </el-col>
      <el-col :span="18">
        <div class="grid-content bg-purple-light">
          <el-table
            :data="list"
            :load="loadChildren"
            :tree-props="{ children: 'children', hasChildren: 'hasChildren' }"
            style="width: 100%"
            type="expand"
            row-key="id"
            :default-expand-all="false"
            border
            fit
            highlight-current-row
            lazy
          >
            <el-table-column label="编码" prop="code">
              <template slot-scope="{ row }">
                <span>{{ row.code }}</span>
              </template>
            </el-table-column>
            <el-table-column label="名称" prop="displayName">
              <template slot-scope="{ row }">
                <span>{{ row.displayName }}</span>
              </template>
            </el-table-column>
            <el-table-column label="创建时间" prop="displayName">
              <template slot-scope="{ row }">
                <span>{{
                  row.creationTime | formatDate("YYYY-MM-DD HH:mm:ss")
                }}</span>
              </template>
            </el-table-column>
            <!-- <el-table-column label="最后修改时间" prop="displayName">
        <template slot-scope="scope">
          <span>{{ scope.row.lastModificationTime == null ? '' : (scope.row.lastModificationTime | formatDate) }}</span>
        </template>
      </el-table-column> -->
            <el-table-column label="操作" align="center" width="300" class-name="small-padding fixed-width">
              <template slot-scope="{ row }">
                <el-button v-if="checkPermission('AbpIdentity.OrganitaionUnits.Create')" size="mini" type="primary" icon="el-icon-plus" @click="handleCreate(row)">
                  新增
                </el-button>
                <el-button v-if="checkPermission('AbpIdentity.OrganitaionUnits.Update')" size="mini" type="primary" icon="el-icon-edit" @click="handleUpdate(row)">
                  编辑
                </el-button>
                <el-button v-if="checkPermission('AbpIdentity.OrganitaionUnits.Delete')" size="mini" type="danger" icon="el-icon-delete" @click="handleDelete(row)">
                  删除
                </el-button>
              </template>
            </el-table-column>
          </el-table>
        </div>
      </el-col>
    </el-row>

    <el-dialog
      :title="
        dialogStatus === 'create'
          ? 'NewOrganitaionUnit'
          : dialogStatus === 'createChild'
            ? 'NewOrganitaionUnitChild'
            : 'Edit'
      "
      :visible.sync="dialogFormVisible"
    >
      <el-form ref="dataForm" :model="temp" label-position="right" label-width="120px">
        <el-form-item v-if="currentParentName !== ''" label="父级名称">
          <el-input v-model="currentParentName" disabled />
        </el-form-item>
        <el-form-item prop="名称" label="名称">
          <el-input v-model="temp.displayName" />
        </el-form-item>
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button @click="dialogFormVisible = false">
          取消
        </el-button>
        <el-button
          type="primary"
          @click="
            dialogStatus === 'create' || dialogStatus === 'createChild'
              ? createData()
              : updateData()
          "
        >
          保存
        </el-button>
      </div>
    </el-dialog>
  </div>
</template>

<script>
import {
  // getOrganizations,
  getOrganizationsRoot,
  getOrganizationChildren,
  createOrganization,
  updateOrganization,
  deleteOrganization
} from '@/api/identity/organization'
import { checkPermission } from '@/utils/abp'

// const id = 1000
export default {
  name: 'Organizations',
  filters: {
    httpCodeFilter(code) {
      code = parseInt(code)
      var result = ''
      if (code <= 100) {
        result = 'info'
      } else if (code <= 200) {
        result = 'success'
      } else if (code <= 300) {
        result = 'warning'
      } else {
        result = 'danger'
      }
      return result
    }
  },
  data() {
    const data = [{
      id: 1,
      label: '一级 1',
      children: [{
        id: 4,
        label: '二级 1-1',
        children: [{
          id: 9,
          label: '三级 1-1-1'
        }, {
          id: 10,
          label: '三级 1-1-2'
        }]
      }]
    }, {
      id: 2,
      label: '一级 2',
      children: [{
        id: 5,
        label: '二级 2-1'
      }, {
        id: 6,
        label: '二级 2-2'
      }]
    }, {
      id: 3,
      label: '一级 3',
      children: [{
        id: 7,
        label: '二级 3-1'
      }, {
        id: 8,
        label: '二级 3-2'
      }]
    }]
    return {

      tableKey: 0,
      list: null,
      listLoading: true,
      currentId: '',
      currentParentName: '',
      temp: {
        parentId: '',
        displayName: ''
      },
      dialogFormVisible: false,
      dialogStatus: '',
      id: 1000,
      data: JSON.parse(JSON.stringify(data))

    }
  },
  created() {
    this.getList()
  },

  mounted() { },

  methods: {
    checkPermission,
    // expandAll() {
    //   const els = this.$el.getElementsByClassName('el-table__expand-icon')
    //   for (let i = 0; i < els.length; i++) {
    //     els[i].click()
    //   }
    // },
    getList() {
      this.listLoading = true
      // 这里会出现感觉是重新刷新了(给人感觉不好的感觉),后期考虑下通过level进行控制返回层架数据,
      this.list = []
      getOrganizationsRoot().then(response => {
        this.list = this.processingChildrenLeaf(response.items)
        this.listLoading = false
      })
    },
    loadChildren(row, treeNode, resolve) {
      getOrganizationChildren(row.id).then(response => {
        resolve(this.processingChildrenLeaf(response))
      })
    },
    handleCreate(row) {
      this.resetTemp()
      if (!row.id) {
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
    createData() {
      this.$refs['dataForm'].validate(valid => {
        if (valid) {
          createOrganization(this.temp).then(() => {
            this.handleRefresh()
            this.dialogFormVisible = false
            this.$notify({
              title: 'Success',
              message: 'SuccessMessage',
              type: 'success',
              duration: 2000
            })
          })
        }
      })
    },
    handleUpdate(row) {
      const { id, displayName, concurrencyStamp } = row
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
          updateOrganization(this.currentId, this.temp).then(() => {
            this.handleRefresh(false)
            // console.log(this.list)
            // const index = this.list.findIndex((v) => v.id === this.temp.id)
            // this.list.splice(index, 1, this.temp)
            this.dialogFormVisible = false
            this.$notify({
              title: '成功',
              message: '修改成功',
              type: 'success',
              duration: 2000
            })
          })
        }
      })
    },
    handleDelete(row, index) {
      this.$confirm('确认删除？')
        .then(_ => {
          deleteOrganization(row.id).then(() => {
            this.handleRefresh()
            this.$notify({
              title: '成功',
              message: '删除成功',
              type: 'success',
              duration: 2000
            })
          })
        })
        .catch(_ => { })
    },
    handleRefresh(firstPage = true) {
      // if (firstPage) this.listQuery.page = 1
      this.getList()
    },
    resetTemp() {
      this.currentId = ''
      this.currentParentName = ''
      this.temp = {
        parentId: '',
        displayName: ''
      }
    },
    processingChildrenLeaf(response) {
      // 统一处理返回值
      return response.map(item => {
        if (item.children) {
          item.children = []
        }
        item.hasChildren = !item.isLeaf
        return item
      })
    },
    append(data) {
      const newChild = { id: this.id++, label: 'testtest', children: [] }
      if (!data.children) {
        this.$set(data, 'children', [])
      }
      data.children.push(newChild)
    },

    remove(node, data) {
      const parent = node.parent
      const children = parent.data.children || parent.data
      const index = children.findIndex(d => d.id === data.id)
      children.splice(index, 1)
    },

    renderContent(h, { node, data, store }) {
      return (
        <span class='custom-tree-node'>
          <span>{node.label}</span>
          <span>
            <el-button size='mini' type='text' on-click={() => this.append(data)}>添加</el-button>
            <el-button size='mini' type='text' on-click={() => this.remove(node, data)}>删除</el-button>
          </span>
        </span>)
    }
  }
}
</script>

<style>
.filter-container {
  margin-bottom: 20px;
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
