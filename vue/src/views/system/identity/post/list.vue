<template>
  <div class="app-container">
    <div class="filter-container">
      <el-input v-model="listQuery.filter" placeholder="职位名称" style="width: 200px;" class="filter-item" @keyup.enter.native="handleFilter" />
      <el-select v-model="listQuery.status" placeholder="状态" clearable style="width:130px;" class="filter-item" @clear="listQuery.status=undefined">
        <el-option label="正常" value="1" />
        <el-option label="禁用" value="0" />
      </el-select>
      <el-button-group class="filter-item">
        <el-button type="primary" icon="el-icon-search" @click="handleFilter">
          查询
        </el-button>
        <el-button type="reset" icon="el-icon-remove-outline" @click="resetQueryForm">
          重置
        </el-button>
        <el-button type="primary" icon="el-icon-refresh" @click="handleRefresh">
          刷新
        </el-button>
      </el-button-group>
      <el-button class="filter-item" style="margin-left: 10px;" type="primary" icon="el-icon-plus" @click="handleCreate">
        创建
      </el-button>
      <el-button v-waves :loading="downloadLoading" class="filter-item" type="primary" icon="el-icon-download" @click="handleDownload">
        导出
      </el-button>
    </div>

    <el-table :key="tableKey" v-loading="listLoading" :data="list" border fit highlight-current-row style="width: 100%;" @sort-change="sortChange">
      <el-table-column label="序号" prop="id" sortable="custom" align="center" width="80" :class-name="getSortClass('id')">
        <template slot-scope="{row}">
          <span>{{ row.id }}</span>
        </template>
      </el-table-column>
      <el-table-column label="职位名称" align="center">
        <template slot-scope="{row}">
          <span class="link-type" @click="handleUpdate(row)">{{ row.postName }}</span>
        </template>
      </el-table-column>

      <el-table-column label="职位编码" align="center">
        <template slot-scope="{row}">
          <span>{{ row.postCode }}</span>
        </template>
      </el-table-column>
      <el-table-column label="状态" class-name="status-col">
        <template slot-scope="{row}">
          <el-tag :type="row.status | statusFilter">
            {{ row.status == '1' ? '正常' : '禁用' }}
          </el-tag>
        </template>
      </el-table-column>

      <el-table-column label="修改日期" align="center">
        <template slot-scope="{row}">
          <span>{{ row.lastModificationTime | parseTime('{y}-{m}-{d} {h}:{i}') }}</span>
        </template>
      </el-table-column>

      <el-table-column label="操作" align="left" width="250" class-name="small-padding fixed-width">
        <template slot-scope="{row,$index}">
          <el-button type="primary" @click="handleUpdate(row)">
            {{ $t("AbpUi['Edit']") }}
          </el-button>
          <el-button v-if="row.status!='deleted'" type="danger" @click="handleDelete(row,$index)">
            {{ $t("AbpUi['Delete']") }}
          </el-button>
        </template>
      </el-table-column>
    </el-table>

    <pagination v-show="total>0" :total="total" :page.sync="listQuery.page" :limit.sync="listQuery.limit" @pagination="getList" />

    <el-dialog :title="textMap[dialogStatus]" :visible.sync="dialogFormVisible">
      <el-form ref="dataForm" :rules="rules" :model="temp" label-position="right" label-width="90px" style="margin-left:50px;">

        <el-form-item label="职位名称" prop="postName">
          <el-input v-model="temp.postName" />
        </el-form-item>
        <el-form-item label="职位编码" prop="postCode">
          <el-input v-model="temp.postCode" />
        </el-form-item>

        <el-row>
          <el-col :span="12"><div class="grid-content bg-purple"><el-form-item label="状态">
            <el-radio v-model="temp.status" label="1">正常</el-radio>
            <el-radio v-model="temp.status" label="0">禁用</el-radio>
          </el-form-item></div></el-col>
          <el-col :span="12"><div class="grid-content bg-purple-light">
            <el-form-item label="排序">
              <el-input-number v-model="temp.postSort" :min="1" :max="100000" label="排序" />
            </el-form-item></div></el-col>
        </el-row>

        <el-form-item label="备注">
          <el-input v-model="temp.remark" type="textarea" placeholder="请输入备注" />
        </el-form-item>
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button @click="dialogFormVisible = false">
          取消
        </el-button>
        <el-button type="primary" @click="dialogStatus==='create'?createData():updateData()">
          确认
        </el-button>
      </div>
    </el-dialog>

  </div>
</template>

<script>

import {
  getPostList,
  createPost,
  updatePost,
  deletePost
} from '@/api/system-manage/identity/post'

import waves from '@/directive/waves' // waves directive
import {
  parseTime
} from '@/utils'
import Pagination from '@/components/Pagination' // secondary package based on el-pagination
import baseListQuery from '@/utils/abp'

export default {
  // 职位管理
  name: 'Post',
  components: {
    Pagination
  },
  directives: {
    waves
  },
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

      statusOptions: ['正常', '禁用'],
      temp: {
        id: undefined,
        postId: 1,
        postCode: '',
        postName: '',
        postSort: 100,
        status: 1,
        flag: false,
        remark: ''
      },
      dialogFormVisible: false,
      dialogStatus: '',
      textMap: {
        update: '编辑',
        create: '创建'
      },
      rules: {
        status: [{
          required: true,
          message: '请选择状态',
          trigger: 'change'
        }],
        postCode: [{
          required: true,
          message: '请输入职位编码',
          trigger: 'blur'
        }],
        postName: [{
          required: true,
          message: '请输入职位名称',
          trigger: 'blur'
        }]
      },
      downloadLoading: false
    }
  },
  created() {
    this.getList()
  },
  methods: {
    getList() {
      this.listLoading = true
      getPostList(this.listQuery).then(response => {
        this.list = response.data.items
        this.total = response.data.totalCount
        // Just to simulate the time of the request
        setTimeout(() => {
          this.listLoading = false
        }, 1.5 * 1000)
      })
    },
    // 重置查询参数
    resetQueryForm() {
      this.listQuery = Object.assign({
        status: undefined
      }, baseListQuery)
    },
    // 刷新页面
    handleRefresh() {
      this.handleFilter()
    },
    handleFilter() {
      this.listQuery.page = 1
      this.getList()
    },
    sortChange(data) {
      const {
        prop,
        order
      } = data
      if (prop === 'id') {
        this.sortByID(order)
      }
    },
    sortByID(order) {
      if (order === 'ascending') {
        this.listQuery.sort = '+id'
      } else {
        this.listQuery.sort = '-id'
      }
      this.handleFilter()
    },
    resetTemp() {
      this.temp = {
        id: undefined,
        postId: 1,
        postCode: '',
        postName: '',
        postSort: 100,
        status: '1',
        flag: false,
        remark: ''
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
          this.temp.id = parseInt(Math.random() * 100) + 1024 // mock a id
          createPost(this.temp).then(() => {
            this.list.unshift(this.temp)
            this.dialogFormVisible = false
            this.$notify({
              title: '成功',
              message: '创建成功',
              type: 'success',
              duration: 2000
            })
          })
        }
      })
    },
    handleUpdate(row) {
      this.temp = Object.assign({}, row) // copy obj
      this.temp.timestamp = new Date(this.temp.timestamp)
      this.dialogStatus = 'update'
      this.dialogFormVisible = true
      this.$nextTick(() => {
        this.$refs['dataForm'].clearValidate()
      })
    },
    updateData() {
      this.$refs['dataForm'].validate((valid) => {
        if (valid) {
          const tempData = Object.assign({}, this.temp)
          tempData.timestamp = +new Date(tempData.timestamp) // change Thu Nov 30 2017 16:41:05 GMT+0800 (CST) to 1512031311464
          updatePost(tempData.id, tempData).then(() => {
            const index = this.list.findIndex(v => v.id === this.temp.id)
            this.list.splice(index, 1, this.temp)
            this.dialogFormVisible = false
            this.$notify({
              title: '成功',
              message: '更新成功',
              type: 'success',
              duration: 2000
            })
          })
        }
      })
    },
    handleDelete(row, index) {
      this.$confirm('此操作将永久删除数据, 是否继续?', '提示', {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        type: 'warning'
      }).then(() => {
        deletePost(row.id)
          .then((response) => {
            const index = this.list.findIndex((v) => v.id === row.id)
            this.list.splice(index, 1)
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
      }).catch(() => {
        this.$message({
          type: 'info',
          message: '已取消删除'
        })
      })
    },
    // 导出
    handleDownload() {
      this.downloadLoading = true
      // 使用懒加载
      import('@/vendor/Export2Excel').then(excel => {
        // 表头
        const tHeader = ['id', 'createBy', 'creationTime', 'updateBy', 'lastModificationTime', 'remark', 'postCode', 'postName', 'postSort']
        const filterVal = ['id', 'createBy', 'creationTime', 'updateBy', 'lastModificationTime', 'remark', 'postCode', 'postName', 'postSort']
        // 具体数据
        const data = this.formatJson(filterVal)
        excel.export_json_to_excel({
          header: tHeader,
          data,
          filename: 'post-list'
        })
        this.downloadLoading = false
      })
    },
    formatJson(filterVal) {
      return this.list.map(v => filterVal.map(j => {
        if (j === 'timestamp') {
          return parseTime(v[j])
        } else {
          return v[j]
        }
      }))
    },
    getSortClass: function(key) {
      const sort = this.listQuery.sort
      return sort === `+${key}` ? 'ascending' : 'descending'
    }
  }
}
</script>
