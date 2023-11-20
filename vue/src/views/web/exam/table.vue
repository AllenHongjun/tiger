<template>
  <div class="app-container">
    <div class="filter-container" style="margin-bottom:10px;">
      <el-form ref="logQueryForm" label-position="left" label-width="80px" :model="listQuery">
        <el-row :gutter="20">
          <el-col :span="4">
            <el-form-item prop="filter" :label="$t('AbpUi[\'Search\']')">
              <el-input v-model="listQuery.filter" :placeholder="$t('AbpUi[\'PlaceholderInput\']')" clearable />
            </el-form-item>
          </el-col>

          <el-col :span="8">
            <el-form-item :label="$t('AbpUi[\'DisplayName:CreationTime\']')">
              <el-date-picker
                v-model="queryCreateDateTime"
                value-format="yyyy-MM-dd HH:mm:ss"
                :default-time="['00:00:00', '23:59:59']"
                type="datetimerange"
                align="right"
                unlink-panels
                :picker-options="pickerOptions"
                range-separator="-"
                :start-placeholder="$t('AbpUi[\'StartTime\']')"
                :end-placeholder="$t('AbpUi[\'EndTime\']')"
                @change="datePickerChange"
              />
            </el-form-item>
          </el-col>

          <el-col :span="4">
            <el-button-group>
              <el-button type="primary" class="filter-item" icon="el-icon-search" @click="handleFilter">
                {{ $t('AbpUi.Search') }}
              </el-button>
              <el-button type="reset" icon="el-icon-remove-outline" @click="resetQueryForm">
                {{ $t('AbpAuditLogging.Reset') }}
              </el-button>

            </el-button-group>
          </el-col>
        </el-row>

      </el-form>

      <!-- 操作按钮 -->
      <!-- <el-row>
        <el-col>
          <el-button-group style="float:left">
            <el-button style="margin-right: 5px;" type="primary" icon="el-icon-plus" @click="handleCreate">
              {{ $t("AppExam['Permission:Create']") }}
            </el-button>
            <el-button icon="el-icon-download" @click="handleDownload">
              导出
            </el-button>
          </el-button-group>

        </el-col>
      </el-row> -->
    </div>
    <div class="card-caontainer">
      <el-card v-for="o in 4" :key="o" class="box-card">
        <el-row>
          <el-col :span="22">
            <h3>考试名称</h3>
            <div>
              <el-tag type="success">进行中</el-tag>
              <span> 考试时间：11-13 21:51 至 11-20 21:51</span> |
              <span> 答题时间：60分钟 </span> |
              <span> 限考 <el-tag type="info">12</el-tag> 次 </span>
            </div>
          </el-col>
          <el-col :span="2">
            <el-button type="primary">参加考试</el-button>
          </el-col>
        </el-row>
        <div class="text item" />
      </el-card>

      <pagination v-show="total > 0" :total="total" :page.sync="listQuery.page" :limit.sync="listQuery.limit" />
    </div>

  </div>
</template>

<script>
import Pagination from '@/components/Pagination' // secondary package based on el-pagination
import { pickerRangeWithHotKey } from '@/utils/picker'
import baseListQuery, { checkPermission } from '@/utils/abp'

export default {
  name: 'CN',
  components: {
    Pagination
  },
  data() {
    return {
      blank: {

      },
      queryCreateDateTime: undefined,
      currentPage4: 4,
      pickerOptions: pickerRangeWithHotKey,
      tableKey: 0,
      list: null,
      total: 150,
      listLoading: true,
      listQuery: Object.assign({
        createStartTime: undefined,
        createEndTime: undefined
      }, baseListQuery)
    }
  },
  methods: {
    datePickerChange(value) {
      if (!value) {
        // 日期选择器改变事件 ~ 解决日期选择器清空 值不清空的问题
        this.listQuery.createStartTime = undefined
        this.listQuery.createEndTime = undefined
      }
    },
    // 搜索展开切换
    toggleAdvanced() {
      this.advanced = !this.advanced
    },
    // 刷新页面
    handleRefresh() {
      this.handleFilter()
    },
    // 重置查询参数
    resetQueryForm() {
      this.queryCreateDateTime = undefined
      this.listQuery = Object.assign({
        degree: undefined,
        questionCategoryId: undefined,
        createStartTime: undefined,
        createEndTime: undefined,
        type: undefined,
        enable: undefined
      }, baseListQuery)
    },
    // 获取列表数据
    getList() {
      this.listLoading = true
      if (this.queryCreateDateTime) {
        this.listQuery.createStartTime = this.queryCreateDateTime[0]
        this.listQuery.createEndTime = this.queryCreateDateTime[1]
      }
    },
    handleFilter(firstPage = true) {
      if (firstPage) this.listQuery.page = 1
      this.getList()
    },
    sortChange(data) {
      const {
        prop,
        order
      } = data
      this.listQuery.sort = order ? `${prop} ${order}` : undefined
      this.handleFilter()
    },
    handleSizeChange(val) {
      console.log(`每页 ${val} 条`)
    },
    handleCurrentChange(val) {
      console.log(`当前页: ${val}`)
    },
    onSubmit() {
      this.$message('submit!')
    },
    onCancel() {
      this.$message({
        message: 'cancel!',
        type: 'warning'
      })
    }
  }
}
</script>

<style scoped>
.line{
  text-align: center;
}
.el-card{
  margin-bottom: 20px;
}
</style>

