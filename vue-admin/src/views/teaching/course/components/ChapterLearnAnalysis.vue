<template>
  <div class="table-container">
    <el-dialog :title=" dialogStatus == 'create'? $t('AppExam[\'Exam:AddNew\']'): $t('AbpUi[\'Edit\']')" :visible.sync="dialogFormVisible" top="1vh" width="98%" height="95%" append-to-body>

      <div class="table-container">

        <!-- <div class="filter-container" style="margin-bottom:10px;">
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

            <el-collapse-transition>
              <div v-show="advanced">
                <el-row :gutter="20">
                  <el-col :span="4" />
                </el-row>
              </div>
            </el-collapse-transition>
          </el-form>

          <el-row>
            <el-col />
          </el-row>
        </div> -->

        <el-table :key="tableKey" v-loading="listLoading" :data="list" border fit highlight-current-row :stripe="true" style="width: 100%;" @sort-change="sortChange">
          <el-table-column type="index" width="50" />
          <el-table-column label="课程名称" prop="content" align="left">
            <template slot-scope="{ row }">
              <span>新商科大数据</span>
            </template>
          </el-table-column>
          <el-table-column label="章节" prop="path" align="left">
            <template slot-scope="{ row }">
              <span>数据获取</span>
            </template>
          </el-table-column>
          <el-table-column label="子章节" prop="path" align="left">
            <template slot-scope="{ row }">
              <span>数据连接</span>
            </template>
          </el-table-column>
          <el-table-column label="首次学习时间" align="left" width="240">
            <template slot-scope="{ row }">
              <span>2023-11-13 21:51</span>
            </template>
          </el-table-column>
          <el-table-column label="最近学习时间" align="left" width="240">
            <template slot-scope="{ row }">
              <span>2023-11-13 21:51</span>
            </template>
          </el-table-column>
          <el-table-column label="学习时长" prop="description" align="left">
            <template slot-scope="{ row }">
              <span>2小时42分12秒</span>
            </template>
          </el-table-column>
          <el-table-column label="状态" prop="path" align="left" sortable>
            <template slot-scope="{ row }">
              <el-tag>未学习</el-tag>
            </template>
          </el-table-column>

        </el-table>

        <pagination v-show="total > 0" :total="total" :page.sync="listQuery.page" :limit.sync="listQuery.limit" @pagination="getList" />

      </div>

      <div slot="footer" class="dialog-footer">
        <el-button type="primary" @click="dialogFormVisible = false">
          {{ $t("AbpUi['Close']") }}
        </el-button>
      </div>
    </el-dialog>
  </div>
</template>

<script>

import baseListQuery, { checkPermission } from '@/utils/abp'
import { pickerRangeWithHotKey } from '@/utils/picker'
import {
  getLayouts,
  deleteLayout
} from '@/api/system-manage/platform/layout'
import Pagination from '@/components/Pagination' // secondary package based on el-pagination

export default {
  name: 'ChapterLearnAnalysis',
  components: {
    Pagination
  },
  data() {
    return {
      blank: {

      },
      activeName: 'first',
      dialogFormVisible: false,
      dialogStatus: '',
      queryCreateDateTime: undefined,
      advanced: false,
      pickerOptions: pickerRangeWithHotKey,
      tableKey: 0,
      list: null,
      total: 0,
      listLoading: true,
      listQuery: Object.assign({
        createStartTime: undefined,
        createEndTime: undefined
      }, baseListQuery)
    }
  },
  created() {
    this.getList()
  },
  methods: {
    checkPermission, // 检查权限
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
      getLayouts(this.listQuery).then(response => {
        this.list = response.items
        this.total = response.totalCount
        this.listLoading = false
      })
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
    handleCreate() {
      this.$emit('handleCreate')
    },

    // 删除
    handleDelete(row, index) {
      this.$confirm(
        // 消息
        this.$i18n.t("AbpUi['ItemWillBeDeletedMessageWithFormat']", [
          row.name
        ]),
        // title
        this.$i18n.t("AbpUi['AreYouSure']"), {
          confirmButtonText: this.$i18n.t("AbpUi['Yes']"), // 确认按钮
          cancelButtonText: this.$i18n.t("AbpUi['Cancel']"), // 取消按钮
          type: 'warning' // 弹框类型
        }
      ).then(async() => {
        // 回调函数
        deleteLayout(row.id).then(() => {
          this.handleFilter(false)
          this.$notify({
            title: this.$i18n.t("TigerUi['Success']"),
            message: this.$i18n.t("TigerUi['SuccessMessage']"),
            type: 'success',
            duration: 2000
          })
        })
      })
    },
    // 下载数据
    handleDownload() {
      this.$alert('开发中...')
      return
    },
    // 查看课程分析
    handleViewChapterLearnAnalysis(row, activeName) {
      this.dialogStatus = 'update'
      this.dialogFormVisible = true
      this.activeName = activeName
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
::v-deep .el-dialog {
  margin: 0 auto 0px;
}
::v-deep .el-dialog__body{
  height: calc(90vh - 50px);
  overflow: auto;
}
</style>

