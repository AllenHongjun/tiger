<template>
  <div class="app-container">
    <div class="filter-container">
      <el-input v-model="listQuery.name" placeholder="请输入名称" style="width: 200px;" class="filter-item" @keyup.enter.native="handleFilter" />

      <!-- <el-select v-model="listQuery.type" placeholder="状态" clearable class="filter-item" style="width: 130px">
        <el-option v-for="item in calendarTypeOptions" :key="item.key" :label="item.display_name" :value="item.key" />
      </el-select>
      <el-cascader :props="listQuery.props" /> -->

      <el-button-group>
        <el-button v-waves class="filter-item" size="mini" type="primary" icon="el-icon-search" @click="handleFilter">
          搜索
        </el-button>
        <!-- <el-button size="mini" type="primary" icon="el-icon-arrow-down" @click="handleSearch" /> -->
      </el-button-group>

      <el-button class="filter-item" size="mini" style="margin-left: 10px;" type="primary" icon="el-icon-edit" @click="handleCreate">
        添加
      </el-button>

    </div>

    <el-table v-loading="listLoading" :data="list" fit highlight-current-row style="width: 100%" @sort-change="sortChange">
      <el-table-column align="center" type="selection" width="55" />

      <el-table-column min-width="150" label="名称">
        <template slot-scope="{row}">
          <span>{{ row.name }}</span>
        </template>
      </el-table-column>

      <el-table-column width="80px" align="center" label="免运费标准">
        <template slot-scope="scope">
          <span>{{ scope.row.freeFreightPoint }}</span>
        </template>
      </el-table-column>

      <el-table-column width="140px" align="center" label="评价获取的成长值">
        <template slot-scope="scope">
          <span>{{ scope.row.commentGrowthPoint }}</span>
        </template>
      </el-table-column>

      <el-table-column class-name="status-col" label="免邮特权" width="110">
        <template slot-scope="{row}">
          <el-tag :type="row.priviledgeFreeFreight | statusFilter">
            {{ row.priviledgeFreeFreight == 1 ? "有" : "无" }}
          </el-tag>
        </template>
      </el-table-column>

      <el-table-column class-name="status-col" label="签到特权" width="110">
        <template slot-scope="{row}">
          <el-tag :type="row.priviledgeSignIn | statusFilter">
            {{ row.priviledgeSignIn == 1 ? "有" : "无" }}
          </el-tag>
        </template>
      </el-table-column>

      <el-table-column class-name="status-col" label="评论特权" width="110">
        <template slot-scope="{row}">
          <el-tag :type="row.privilegeComment | statusFilter">
            {{ row.privilegeComment == 1 ? "有" : "无" }}
          </el-tag>
        </template>
      </el-table-column>

      <el-table-column class-name="status-col" label="专享活动特权" width="110">
        <template slot-scope="{row}">
          <el-tag :type="row.privilegePromotin | statusFilter">
            {{ row.privilegePromotin == 1 ? "有" : "无" }}
          </el-tag>
        </template>
      </el-table-column>

      <el-table-column class-name="status-col" label="会员价特权" width="110">
        <template slot-scope="{row}">
          <el-tag :type="row.privilegeMemberPrice | statusFilter">
            {{ row.privilegeMemberPrice == 1 ? "有" : "无" }}
          </el-tag>
        </template>
      </el-table-column>

      <el-table-column class-name="status-col" label="生日特权" width="110">
        <template slot-scope="{row}">
          <el-tag :type="row.privilegeBirthDay | statusFilter">
            {{ row.privilegeBirthDay == 1 ? "有" : "无" }}
          </el-tag>
        </template>
      </el-table-column>

      <el-table-column min-width="160" label="等级说明">
        <template slot-scope="{row}">
          <span>{{ row.note }}</span>
        </template>
      </el-table-column>

      <el-table-column align="center" label="操作" width="300">
        <template slot-scope="scope">

          <el-button type="primary" size="mini" @click="handleUpdate(scope.row)">
            编辑
          </el-button>
          <el-button type="danger" size="mini" @click="handleDelete(scope.row)">
            删除
          </el-button>

        </template>

      </el-table-column>
    </el-table>

    <pagination v-show="total>0" :total="total" :page.sync="listQuery.page" :limit.sync="listQuery.limit" @pagination="getList" />

    <el-dialog :title="textMap[dialogStatus]" :visible.sync="dialogFormVisible">
      <el-form ref="dataForm" :rules="rules" :model="temp" label-position="right" label-width="140px" style=" margin-left:50px;">
        <el-form-item label="名称" prop="name">
          <el-input v-model="temp.name" />
        </el-form-item>

        <el-form-item label="免运费标准" prop="freeFreightPoint">
          <el-input v-model="temp.freeFreightPoint" />
        </el-form-item>

        <el-form-item label="评价获取的成长值" prop="commentGrowthPoint">
          <el-input v-model="temp.commentGrowthPoint" />
        </el-form-item>

        <el-form-item label="免邮特权">
          <el-switch
            v-model="temp.priviledgeFreeFreight"
            active-color="#13ce66"
            inactive-color="#ff4949"
            :active-value="1"
            :inactive-value="0"
          />
        </el-form-item>

        <el-form-item label="签到特权">
          <el-switch
            v-model="temp.priviledgeSignIn"
            active-color="#13ce66"
            inactive-color="#ff4949"
            :active-value="1"
            :inactive-value="0"
          />
        </el-form-item>

        <el-form-item label="评论特权">
          <el-switch
            v-model="temp.privilegeComment"
            active-color="#13ce66"
            inactive-color="#ff4949"
            :active-value="1"
            :inactive-value="0"
          />
        </el-form-item>

        <el-form-item label="促销特权">
          <el-switch
            v-model="temp.privilegePromotin"
            active-color="#13ce66"
            inactive-color="#ff4949"
            :active-value="1"
            :inactive-value="0"
          />
        </el-form-item>

        <el-form-item label="会员价特权">
          <el-switch
            v-model="temp.privilegeMemberPrice"
            active-color="#13ce66"
            inactive-color="#ff4949"
            :active-value="1"
            :inactive-value="0"
          />
        </el-form-item>

        <el-form-item label="生日特权">
          <el-switch
            v-model="temp.privilegeBirthDay"
            active-color="#13ce66"
            inactive-color="#ff4949"
            :active-value="1"
            :inactive-value="0"
          />
        </el-form-item>

        <el-form-item label="等级说明">
          <el-input v-model="temp.note" :autosize="{ minRows: 2, maxRows: 4}" type="textarea" placeholder="" />
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

    <!-- 导入excel -->
    <el-dialog title="导入" :visible.sync="importExcelDialogVisible" width="650px">
      <el-button v-waves :loading="downloadLoading" size="mini" icon="el-icon-download" @click="handleDownload">
        下载模板
      </el-button>
      <el-divider />
      <upload-excel-component :on-success="handleSuccess" :before-upload="beforeUpload" />
      <el-row style="margin-top:10px;">只能上传Excel文件,文件大小不能超过10M</el-row>
      <div slot="footer" class="dialog-footer">
        <el-button type="primary" @click="importExcelDialogVisible = false">
          关闭
        </el-button>
      </div>
    </el-dialog>

  </div>
</template>

<script>
import { getMemberLevels, createMemberLevel, updateMemberLevel, deleteMemberLevel } from '@/api/member/member-level'
import waves from '@/directive/waves' // waves directive
import { parseTime } from '@/utils'
import UploadExcelComponent from '@/components/UploadExcel/index.vue'

import Pagination from '@/components/Pagination' // secondary package based on el-pagination

const calendarTypeOptions = [
  { key: 'show', display_name: '显示' },
  { key: 'hidden', display_name: '隐藏' }
]

// const showCategoryTypeOptions = [
//   { key: 'show', display_name: '显示' },
//   { key: 'hidden', display_name: '隐藏' }
// ]

// arr to obj, such as { CN : "China", US : "USA" }
const calendarTypeKeyValue = calendarTypeOptions.reduce((acc, cur) => {
  acc[cur.key] = cur.display_name
  return acc
}, {})

export default {
  name: 'CategoryList',
  components: { Pagination, UploadExcelComponent },
  directives: { waves },
  filters: {
    statusFilter(status) {
      const statusMap = {
        1: 'success',
        3: 'info',
        0: 'danger'
      }
      return statusMap[status]
    },
    typeFilter(type) {
      return calendarTypeKeyValue[type]
    }
  },
  data() {
    return {
      list: null,
      total: 0,
      listLoading: true,
      listQuery: {
        page: 1,
        limit: 10,
        name: undefined
      },
      importanceOptions: [1, 2, 3],
      // statusOptions: [true, false],
      calendarTypeOptions,
      sortOptions: [{ label: '升序', key: '+id' }, { label: '降序', key: '-id' }],
      statusOptions: ['published', 'draft', 'deleted'],
      showReviewer: false,
      show: true,
      temp: {
        id: undefined,
        name: '',
        growthPoint: 0,
        freeFreightPoint: 0,
        commentGrowthPoint: 0,
        priviledgeFreeFreight: 0,
        priviledgeSignIn: 0,
        privilegeComment: 0,
        privilegePromotin: 0,
        privilegeMemberPrice: 0,
        privilegeBirthDay: 0,
        note: ''
      },
      searchDivVisible: false,
      dialogFormVisible: false,
      dialogStatus: '',
      textMap: {
        update: '编辑',
        create: '新增'
      },
      searchDivVisibilty: false,
      searchFilterDialogVisible: false,
      importExcelDialogVisible: false,
      dialogPvVisible: false,
      pvData: [],
      rules: {
        type: [{ required: true, message: 'type is required', trigger: 'change' }],
        timestamp: [{ type: 'date', required: true, message: 'timestamp is required', trigger: 'change' }],
        name: [{ required: true, message: 'title is required', trigger: 'blur' }]
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
      getMemberLevels(this.listQuery).then(response => {
        this.list = response.items
        this.total = response.totalCount
        this.listLoading = false
      })
    },
    handleFilter() {
      this.searchDivVisibilty = false
      this.listQuery.page = 1
      this.getList()
    },
    handleModifyStatus(row, status) {
      this.$message({
        message: '操作成功',
        type: 'success'
      })
      row.status = status
    },
    // 排序
    sortChange(data) {
      const { prop, order } = data
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
    resetSearchForm(formName) {
      console.log('formName', formName)
      this.$refs[formName].resetFields()
    },
    resetTemp() {
      this.temp = {
        id: undefined,
        name: '',
        growthPoint: 0,
        freeFreightPoint: 0,
        commentGrowthPoint: 0,
        priviledgeFreeFreight: 0,
        priviledgeSignIn: 0,
        privilegeComment: 0,
        privilegePromotin: 0,
        privilegeMemberPrice: 0,
        privilegeBirthDay: 0,
        note: ''
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
          this.temp.author = 'vue-element-admin'
          createMemberLevel(this.temp).then((res) => {
            // console.log('create-res-this.temp', res, this.temp)
            this.temp.id = res.id
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
      console.log('handleUpdate+row', row)
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
          updateMemberLevel(tempData).then(() => {
            for (const v of this.list) {
              if (v.id === this.temp.id) {
                const index = this.list.indexOf(v)
                this.list.splice(index, 1, this.temp)
                break
              }
            }
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
    handleDelete(row) {
      this.$confirm('此操作将永久删除数据, 是否继续?', '提示', {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        type: 'warning'
      }).then(() => {
        deleteMemberLevel(row.id)
          .then((response) => {
            console.log(response)
            const index = this.list.findIndex((v) => v.id === row.id)
            this.list.splice(index, 1)
            this.$notify({
              title: '成功',
              message: '删除成功',
              type: 'success',
              duration: 2000
            })
          })
          .catch((err) => {
            console.log(err)
          })
      }).catch((err) => {
        console.log(err)
      })
    },

    handleSearch() {
      this.searchDivVisibilty = !this.searchDivVisibilty
      console.log('handleSearch', this.searchDivVisibilty)
    },
    handleImport() {
      this.importExcelDialogVisible = true
      console.log('导入数据')
    },
    beforeUpload(file) {
      const isLt10M = file.size / 1024 / 1024 < 10

      if (isLt10M) {
        return true
      }

      this.$message({
        message: '请不要上传大于10MB的文件',
        type: 'warning'
      })
      return false
    },
    handleSuccess({ results, header }) {
      this.$message({
        message: '文件上传成功',
        type: 'success'
      })
      // this.tableData = results
      // this.tableHeader = header
    },
    handleDownload() {
      this.downloadLoading = true
      import('@/vendor/Export2Excel').then(excel => {
        const tHeader = ['timestamp', 'title', 'type', 'importance', 'status']
        const filterVal = ['timestamp', 'title', 'type', 'importance', 'status']
        const data = this.formatJson(filterVal, this.list)
        excel.export_json_to_excel({
          header: tHeader,
          data,
          filename: '商品列表' + (new Date()).toLocaleDateString()
        })
        this.downloadLoading = false
      })
    },
    formatJson(filterVal, jsonData) {
      return jsonData.map(v => filterVal.map(j => {
        if (j === 'timestamp') {
          return parseTime(v[j])
        } else {
          return v[j]
        }
      }))
    }

  }
}
</script>

<style scoped>
.edit-input {
  padding-right: 100px;
}
.cancel-btn {
  position: absolute;
  right: 15px;
  top: 10px;
}
.filter-container{
  margin-bottom: 20px;
  position:relative;

}
.search-container{
  position:absolute;
  border:rgb(230, 217, 217) solid 1px;
  padding:30px 20px;
  left:0px;
  top:40px;
  width:100%;
  /* height:500px; */
  background-color:rgb(255, 255, 255);
  z-index:99;
  display:block;
}

</style>

