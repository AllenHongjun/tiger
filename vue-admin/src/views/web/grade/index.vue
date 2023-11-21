<template>
  <div class="table-container">
    <div class="filter-container" style="margin-bottom:10px;">
      <el-form ref="logQueryForm" label-position="left" label-width="80px" :model="listQuery">
        <el-row :gutter="20">
          <el-col :span="4">
            <el-form-item prop="filter" :label="$t('AbpUi[\'Search\']')">
              <el-input v-model="listQuery.filter" :placeholder="$t('AbpUi[\'PlaceholderInput\']')" clearable />
            </el-form-item>
          </el-col>
          <el-col :span="4">
            <el-form-item prop="filter" label="考试状态">
              <el-select v-model="value" placeholder="-">
                <el-option key="" value="考试中" lable="考试中" />
                <el-option key="" value="已交卷" lable="已交卷" />
                <el-option key="" value="已评卷" lable="已评卷" />
              </el-select>
            </el-form-item>
          </el-col>

          <el-col :span="4">
            <el-form-item prop="filter" label="是否及格">
              <el-select v-model="value" placeholder="-">
                <el-option key="" value="及格" lable="及格" />
                <el-option key="" value="不及格" lable="不及格" />
              </el-select>
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
            <el-button icon="el-icon-download" @click="handleDownload">
              导出
            </el-button>
          </el-button-group>

        </el-col>
      </el-row> -->
    </div>

    <el-table :key="tableKey" v-loading="listLoading" :data="list" border fit highlight-current-row :stripe="true" style="width: 100%;" @sort-change="sortChange">
      <el-table-column type="selection" width="55" center />
      <el-table-column type="index" width="50" />
      <el-table-column label="考试名称" prop="name" align="left" width="80">
        <template slot-scope="{ row }">
          <span>2021-高级实操考试</span>
        </template>
      </el-table-column>
      <el-table-column label="总分/及格分" prop="path" align="left">
        <template slot-scope="{ row }">
          <span>100.00</span>/<b style="color: red;">23.00</b>
        </template>
      </el-table-column>
      <el-table-column label="答题时间" align="left" width="240">
        <template slot-scope="{ row }">
          <span>2023-11-13 21:51 ~ 2023-11-20 21:51</span>
        </template>
      </el-table-column>
      <el-table-column label="用时" prop="description" align="left">
        <template slot-scope="{ row }">
          <span>2小时42分12秒</span>
        </template>
      </el-table-column>

      <el-table-column label="成绩" prop="path" align="left">
        <template slot-scope="{ row }">
          <span style="color: red;">69</span>
        </template>
      </el-table-column>

      <el-table-column label="是否及格" prop="path" align="left">
        <template slot-scope="{ row }">
          <!-- <el-tag :type="( row.isPublic ? 'success' : 'danger')" :class="[ row.isPublic ? 'el-icon-check':'el-icon-close' ]" /> -->
          <el-tag type="success" class="el-icon-check" />
        </template>
      </el-table-column>

      <el-table-column :label="$t('AbpUi[\'Actions\']')" align="left" width="210">
        <template slot-scope="{ row, $index }">
          <el-button type="text" title="查看考试结果" class="el-icon-view">查看</el-button>
        </template>
      </el-table-column>
    </el-table>

    <pagination v-show="total > 0" :total="total" :page.sync="listQuery.page" :limit.sync="listQuery.limit" @pagination="getList" />

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
  name: 'ExamScore',
  components: {
    Pagination
  },
  data() {
    return {
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
    }
  }
}
</script>

<style scoped>
.line{
  text-align: center;
}
</style>

