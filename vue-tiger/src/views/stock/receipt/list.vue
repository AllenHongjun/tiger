<template>
  <div class="app-container">
    <el-row style="margin-bottom:5px;">
      <el-button icon="el-icon-search">查询</el-button>
      <el-button icon="el-icon-set-up" @click="swithBillContainer">切换</el-button>
      <el-button type="info" icon="el-icon-refresh">刷新</el-button>
      <el-button type="info" icon="el-icon-printer">打印</el-button>
      <el-button type="primary" icon="el-icon-plus">添加</el-button>
      <el-button type="primary" icon="el-icon-edit">编辑</el-button>
      <el-button type="danger" icon="el-icon-delete">删除</el-button>
      <el-button type="warning" icon="el-icon-check">审核</el-button>
      <el-dropdown trigger="click" style="margin:0 5px;">
        <el-button type="success" icon="el-icon-document-copy">
          生单<i class="el-icon-arrow-down el-icon--right" />
        </el-button>

        <el-dropdown-menu slot="dropdown">
          <el-dropdown-item>
            <el-link><i class="el-icon-document el-icon--left" />生成退货单 </el-link>
          </el-dropdown-item>
          <el-dropdown-item><el-link><i class="el-icon-document el-icon--left" />生成调拨单 </el-link></el-dropdown-item>
          <el-dropdown-item><el-link><i class="el-icon-document el-icon--left" />生成入库单 </el-link></el-dropdown-item>
          <el-dropdown-item><el-link><i class="el-icon-document el-icon--left" />生成出库单 </el-link></el-dropdown-item>
        </el-dropdown-menu>
      </el-dropdown>
      <el-button type="danger" icon="el-icon-delete">作废</el-button>
      <el-button type="info" icon="el-icon-message">到货/入库</el-button>

      <el-dropdown trigger="click" style="margin:0 5px;">
        <el-button type="warning" icon="el-icon-edit-outline">
          批处理<i class="el-icon-arrow-down el-icon--right" />
        </el-button>

        <el-dropdown-menu slot="dropdown">
          <el-dropdown-item icon="el-icon-printer" divided>批量打印</el-dropdown-item>
          <!-- <el-divider /> -->
          <el-dropdown-item icon="el-icon-delete" divided>批量删除</el-dropdown-item>
          <el-dropdown-item icon="el-icon-check" divided @click="handleUpdate(scope.row)">批量审核</el-dropdown-item>
          <el-dropdown-item icon="el-icon-check" divided disabled>批量修改备注</el-dropdown-item>
          <el-dropdown-item icon="el-icon-message" divided>批量到货</el-dropdown-item>
          <el-dropdown-item icon="el-icon-close" divided>批量订单关闭</el-dropdown-item>
        </el-dropdown-menu>
      </el-dropdown>
      <el-dropdown trigger="click" style="margin:0 5px;">
        <el-button type="info" icon="el-icon-more">
          更多<i class="el-icon-arrow-down el-icon--right" />
        </el-button>

        <el-dropdown-menu slot="dropdown">
          <el-dropdown-item icon="el-icon-plus" divided>复制新增</el-dropdown-item>
          <!-- <el-divider /> -->
          <el-dropdown-item icon="el-icon-circle-plus" divided>草稿新增</el-dropdown-item>
          <el-dropdown-item icon="el-icon-edit" divided @click="handleUpdate(scope.row)">标记已付</el-dropdown-item>
          <el-dropdown-item icon="el-icon-edit" divided disabled>修改备注</el-dropdown-item>
          <el-dropdown-item icon="el-icon-document" divided>导出excel</el-dropdown-item>
        </el-dropdown-menu>
      </el-dropdown>
    </el-row>

    <div v-show="billContainerVisibilty" class="bill-container">
      <div class="filter-container">
        <el-input v-model="listQuery.title" placeholder="请输入分类名称" style="width: 200px;" class="filter-item" @keyup.enter.native="handleFilter" />
        <!-- <el-select v-model="listQuery.importance" placeholder="条件" clearable style="width: 90px" class="filter-item">
        <el-option v-for="item in importanceOptions" :key="item" :label="item" :value="item" />
      </el-select> -->
        <!-- <el-select v-model="listQuery.status" placeholder="条件" clearable style="width: 90px" class="filter-item">
        <el-option v-for="item in statusOptions" :key="item" :label="item" :value="item" />
      </el-select> -->
        <el-select v-model="listQuery.type" placeholder="状态" clearable class="filter-item" style="width: 130px">
          <el-option v-for="item in calendarTypeOptions" :key="item.key" :label="item.display_name" :value="item.key" />
        </el-select>
        <el-cascader :props="listQuery.props" />
        <!-- <el-select v-model="listQuery.sort" style="width: 140px" class="filter-item" @change="handleFilter">
        <el-option v-for="item in sortOptions" :key="item.key" :label="item.label" :value="item.key" />
      </el-select> -->

        <div v-show="searchDivVisibilty" class="search-container">
          <el-form ref="searchForm" :model="listQuery" label-width="80px">
            <el-row>
              <el-col :span="6">
                <el-form-item v-model="listQuery.title" label="名称" placeholder="请输入名称" label-width="120px" class="postInfo-container-item">
                  <el-input v-model="listQuery.title" />
                </el-form-item>
              </el-col>
              <el-col :span="6">
                <el-form-item label="重要性" label-width="120px" class="postInfo-container-item">
                  <el-select v-model="listQuery.importance" placeholder="重要性" clearable style="width: 90px" class="filter-item">
                    <el-option v-for="item in importanceOptions" :key="item" :label="item" :value="item" />
                  </el-select>
                </el-form-item>
              </el-col>

              <el-col :span="6">
                <el-form-item label="类型" label-width="120px" class="postInfo-container-item">
                  <el-select v-model="listQuery.type" placeholder="类型" clearable class="filter-item" style="width: 130px">
                    <el-option v-for="item in calendarTypeOptions" :key="item.key" :label="item.display_name+'('+item.key+')'" :value="item.key" />
                  </el-select>
                </el-form-item>
              </el-col>
              <el-col :span="6">
                <el-form-item label="排序" label-width="120px" class="postInfo-container-item">
                  <el-select v-model="listQuery.sort" style="width: 140px" class="filter-item" @change="handleFilter">
                    <el-option v-for="item in sortOptions" :key="item.key" :label="item.label" :value="item.key" />
                  </el-select>
                </el-form-item>
              </el-col>

            </el-row>

            <el-form-item label="" label-width="120px">
              <el-button type="primary" @click="handleFilter">
                搜索
              </el-button>
              <el-button type="primary" @click="resetSearchForm('searchForm')">
                重置
              </el-button>
              <el-button type="primary" @click="handleSearch">
                关闭
              </el-button>
            </el-form-item>
          </el-form>
        </div>
      </div>

      <el-table v-loading="listLoading" :data="list" border fit highlight-current-row style="width: 100%" @sort-change="sortChange">
        <el-table-column align="center" type="selection" width="55" />

        <el-table-column min-width="180px" label="单号">
          <template slot-scope="{row}">
            <router-link :to="'/example/edit/'+row.id" class="link-type">
              <span>{{ row.code }}</span>
            </router-link>
          </template>
        </el-table-column>

        <el-table-column class-name="status-col" label="类型" width="110">
          <template slot-scope="{row}">
            <el-tag :type="row.receiptType | statusFilter">
              {{ row.receiptType }}
            </el-tag>
          </template>
        </el-table-column>

        <el-table-column width="180px" align="center" label="单据日期" sortable>
          <template slot-scope="scope">
            <span>{{ scope.row.creationTime | formatDate }}</span>
          </template>
        </el-table-column>

        <el-table-column width="180px" align="center" label="到货时间" sortable>
          <template slot-scope="scope">
            <span>{{ scope.row.arriveDatetime | formatDate }}</span>
          </template>
        </el-table-column>

        <el-table-column min-width="100px" label="总数量">
          <template slot-scope="{row}">
            <span>{{ row.totalQty }}</span>
          </template>
        </el-table-column>

        <el-table-column min-width="100px" label="总重量">
          <template slot-scope="{row}">
            <span>{{ row.totalWeight }}</span>
          </template>
        </el-table-column>

        <el-table-column min-width="100px" label="总体积">
          <template slot-scope="{row}">
            <span>{{ row.totalVolume }}</span>
          </template>
        </el-table-column>

        <el-table-column min-width="100px" label="备注">
          <template slot-scope="{row}">
            <span>{{ row.note }}</span>
          </template>
        </el-table-column>

      </el-table>

      <pagination v-show="total>0" :total="total" :page.sync="listQuery.page" :limit.sync="listQuery.limit" @pagination="getList" />

      <el-dialog :title="textMap[dialogStatus]" :visible.sync="dialogFormVisible">
        <el-form ref="dataForm" :rules="rules" :model="temp" label-position="left" label-width="120px" style="width: 400px; margin-left:50px;">
          <el-form-item label="类型" prop="type">
            <el-select v-model="temp.type" class="filter-item" placeholder="Please select">
              <el-option v-for="item in calendarTypeOptions" :key="item.key" :label="item.display_name" :value="item.key" />
            </el-select>
          </el-form-item>
          <el-form-item label="父级分类" prop="title">
            <el-cascader :props="listQuery.props" />
          </el-form-item>
          <el-form-item label="时间" prop="timestamp">
            <el-date-picker v-model="temp.timestamp" type="datetime" placeholder="Please pick a date" />
          </el-form-item>
          <el-form-item label="标题" prop="title">
            <el-input v-model="temp.title" />
          </el-form-item>
          <el-form-item label="状态">
            <el-select v-model="temp.status" class="filter-item" placeholder="Please select">
              <el-option v-for="item in statusOptions" :key="item" :label="item" :value="item" />
            </el-select>
          </el-form-item>
          <el-form-item label="重要性">
            <el-rate v-model="temp.importance" :colors="['#99A9BF', '#F7BA2A', '#FF9900']" :max="3" style="margin-top:8px;" />
          </el-form-item>
          <el-form-item label="点评">
            <el-input v-model="temp.remark" :autosize="{ minRows: 2, maxRows: 4}" type="textarea" placeholder="Please input" />
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

    <div v-show="!billContainerVisibilty" class="bill-detail-container">

      <el-form ref="ruleUserForm" :rules="rules" :model="temp" label-width="120px">

        <el-row>
          <el-col :span="6">
            <el-form-item label="创建日期" prop="username">
              <el-date-picker v-model="temp.creationTime" type="date" placeholder="创建日期" style="width: 100%;" />
            </el-form-item>

          </el-col>
          <el-col :span="6">
            <el-form-item label="单号" prop="username">
              <el-input v-model="temp.code" />
            </el-form-item></el-col>
          <el-col :span="6">
            <el-form-item label="入库仓库" prop="username">
              <el-input v-model="temp.warehouseId" />
            </el-form-item></el-col>
          <el-col :span="6">
            <el-form-item label="类型" prop="username">
              <el-input v-model="temp.receiptType" />
            </el-form-item></el-col>
        </el-row>

        <!-- <el-form-item label="用户名" prop="username">
          <el-input v-model="userForm.username" />
        </el-form-item>

        <el-form-item label="手机号" prop="phone">
          <el-input v-model="userForm.phone" />
        </el-form-item>

        <el-form-item label="邮箱" prop="email">
          <el-input v-model="userForm.email" />
        </el-form-item>

        <el-form-item label="旧密码" prop="oldPassword">
          <el-input v-model="userForm.oldPassword" show-password />
        </el-form-item>

        <el-form-item label="新密码" prop="password">
          <el-input v-model="userForm.password" show-password />
        </el-form-item>

        <el-form-item label="确认密码" prop="comfirmPwd">
          <el-input v-model="userForm.comfirmPwd" show-password />
        </el-form-item>

        <el-form-item label="地址" prop="address">
          <el-input v-model="userForm.address" />
        </el-form-item>

        <el-form-item label="生日" prop="birthday">
          <el-col :span="11">
            <el-date-picker v-model="userForm.birthday" type="date" placeholder="日期" style="width: 100%;" />
          </el-col>
        </el-form-item>

        <el-form-item label="性别" prop="sex">
          <el-radio-group v-model="userForm.sex">
            <el-radio label="男" />
            <el-radio label="女" />
          </el-radio-group>
        </el-form-item>

        <el-form-item label="城市" prop="city">
          <el-input v-model="userForm.city" />
        </el-form-item>
        <el-form-item label="年龄" prop="age">
          <el-input v-model.number="userForm.age" />
        </el-form-item> -->

        <el-table v-loading="listLoading" :data="list" border fit highlight-current-row style="width: 100%" @sort-change="sortChange">
          <el-table-column align="center" type="selection" width="55" />

          <el-table-column min-width="180px" label="单号">
            <template slot-scope="{row}">
              <router-link :to="'/example/edit/'+row.id" class="link-type">
                <span>{{ row.code }}</span>
              </router-link>
            </template>
          </el-table-column>

          <el-table-column class-name="status-col" label="类型" width="110">
            <template slot-scope="{row}">
              <el-tag :type="row.receiptType | statusFilter">
                {{ row.receiptType }}
              </el-tag>
            </template>
          </el-table-column>

          <el-table-column width="180px" align="center" label="单据日期" sortable>
            <template slot-scope="scope">
              <span>{{ scope.row.creationTime | formatDate }}</span>
            </template>
          </el-table-column>

          <el-table-column width="180px" align="center" label="到货时间" sortable>
            <template slot-scope="scope">
              <span>{{ scope.row.arriveDatetime | formatDate }}</span>
            </template>
          </el-table-column>

          <el-table-column min-width="100px" label="总数量">
            <template slot-scope="{row}">
              <span>{{ row.totalQty }}</span>
            </template>
          </el-table-column>

          <el-table-column min-width="100px" label="总重量">
            <template slot-scope="{row}">
              <span>{{ row.totalWeight }}</span>
            </template>
          </el-table-column>

          <el-table-column min-width="100px" label="总体积">
            <template slot-scope="{row}">
              <span>{{ row.totalVolume }}</span>
            </template>
          </el-table-column>

          <el-table-column min-width="100px" label="备注">
            <template slot-scope="{row}">
              <span>{{ row.note }}</span>
            </template>
          </el-table-column>

        </el-table>

        <el-form-item>
          <el-button type="primary" @click="onSubmit('ruleUserForm')">提交</el-button>
          <!-- <el-button @click="onCancel">取消</el-button> -->
        </el-form-item>
      </el-form>

    </div>

  </div>
</template>

<script>
import { fetchList, fetchPv, createArticle, updateArticle } from '@/api/article'
import { getReceiptHeaders, getReceiptHeaderById, createReceiptHeader, updateReceiptHeader, deleteReceiptHeader } from '@/api/stock/receipt-header'
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

const id = 0

export default {
  name: 'CategoryList',
  components: { Pagination, UploadExcelComponent },
  directives: { waves },
  filters: {
    statusFilter(status) {
      const statusMap = {
        published: 'success',
        draft: 'info',
        deleted: 'danger'
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
        // status: undefined,
        name: undefined
        // sort: '+id'

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
        creationTime: '',
        code: '',
        receiptType: 0,
        purchaseOrderId: '3fa85f64-5717-4562-b3fc-2c963f66afa6',
        arriveDatetime: '',
        closeAt: '',
        totalQty: 0,
        totalCases: 0,
        totalWeight: 0,
        totalVolume: 0,
        note: '',
        warehouseId: '3fa85f64-5717-4562-b3fc-2c963f66afa6'
      },

      dialogFormVisible: false,
      dialogStatus: '',
      textMap: {
        update: 'Edit',
        create: 'Create'
      },
      searchDivVisibilty: false,
      billContainerVisibilty: true,
      searchFilterDialogVisible: false,
      importExcelDialogVisible: false,
      dialogPvVisible: false,
      pvData: [],
      rules: {
        type: [{ required: true, message: 'type is required', trigger: 'change' }],
        timestamp: [{ type: 'date', required: true, message: 'timestamp is required', trigger: 'change' }],
        title: [{ required: true, message: 'title is required', trigger: 'blur' }]
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
      getReceiptHeaders(this.listQuery).then(response => {
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
        importance: 1,
        remark: '',
        timestamp: new Date(),
        title: '',
        status: 'published',
        type: ''
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
          createArticle(this.temp).then(() => {
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
          updateArticle(tempData).then(() => {
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
      this.$notify({
        title: '成功',
        message: '删除成功',
        type: 'success',
        duration: 2000
      })
      console.log(row)
      const index = this.list.indexOf(row)
      this.list.splice(index, 1)
    },
    handleFetchPv(pv) {
      fetchPv(pv).then(response => {
        this.pvData = response.data.pvData
        this.dialogPvVisible = true
      })
    },
    handleSearch() {
      this.searchDivVisibilty = !this.searchDivVisibilty
      console.log('handleSearch', this.searchDivVisibilty)
    },
    swithBillContainer() {
      this.billContainerVisibilty = !this.billContainerVisibilty
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
  margin-bottom: 5px;
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

.bill-detail-container{
  /* border: 1px solid rgb(197, 204, 202); */
  width:100%;
  min-height:800px;
  /* background-color:aquamarine; */
}
</style>

