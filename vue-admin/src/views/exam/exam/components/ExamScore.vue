<template>
  <div class="table-container">

    <exam-score-panel />
    <div class="filter-container" style="margin-bottom:10px;">
      <el-form ref="logQueryForm" label-position="left" label-width="80px" :model="listQuery">
        <el-row :gutter="20">
          <el-col :span="4">
            <el-form-item prop="filter" label="考试状态">
              <el-select v-model="value" placeholder="-">
                <el-option key="1" value="考试中" lable="考试中" />
                <el-option key="2" value="已交卷" lable="已交卷" />
                <el-option key="3" value="已评卷" lable="已评卷" />
              </el-select>
            </el-form-item>
          </el-col>

          <el-col :span="4">
            <el-form-item prop="filter" label="是否及格">
              <el-select v-model="value" placeholder="-">
                <el-option key="1" value="及格" lable="及格" />
                <el-option key="2" value="不及格" lable="不及格" />
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
      <el-row>
        <el-col>
          <el-button-group style="float:left">
            <el-button icon="el-icon-refresh" @click="handleRefresh">
              刷新
            </el-button>
            <el-button icon="el-icon-download" @click="handleDownload">
              导出
            </el-button>
          </el-button-group>

        </el-col>
      </el-row>
    </div>

    <el-table :key="tableKey" v-loading="listLoading" :data="list" border fit highlight-current-row :stripe="true" style="width: 100%;" @sort-change="sortChange">
      <el-table-column type="selection" width="55" center />
      <el-table-column type="index" width="50" />
      <el-table-column label="姓名" prop="name" align="left" width="80">
        <template slot-scope="{ row }">
          <span>{{ row.creatorUserName }}</span>
        </template>
      </el-table-column>
      <el-table-column label="开始时间/交卷时间" align="left" width="240">
        <template slot-scope="{ row }">
          <span>{{ row.creationTime | moment }} - {{ row.submitTime | moment }}</span>
        </template>
      </el-table-column>
      <el-table-column label="用时(分)" prop="description" align="left">
        <template slot-scope="{ row }">
          <span>{{ row.examDuration }}  分钟</span>
        </template>
      </el-table-column>
      <el-table-column label="总分/及格分" prop="path" align="left">
        <template slot-scope="{ row }">
          <span>100.00</span>--<b style="color: red;">{{ row.passingScore }}</b>
        </template>
      </el-table-column>
      <el-table-column label="成绩" prop="path" align="left">
        <template slot-scope="{ row }">
          <span>{{ row.totalScore }}</span>
        </template>
      </el-table-column>
      <!-- <el-table-column label="正确率" prop="path" align="left">
        <template slot-scope="{ row }">
          <span>{{ row.totalScore / 100.00 }}%</span>
        </template>
      </el-table-column> -->
      <el-table-column label="得分率" prop="path" align="left">
        <template slot-scope="{ row }">
          <span>85%</span>
        </template>
      </el-table-column>
      <el-table-column label="是否及格" prop="path" align="left">
        <template slot-scope="{ row }">
          <el-tag v-if="row.status === 3" :type="( row.totalScore > row.passingScore ? 'success' : 'danger')" :class="[ row.isPublic ? 'el-icon-check':'el-icon-close' ]" />
        </template>
      </el-table-column>
      <el-table-column label="状态" prop="path" align="left">
        <template slot-scope="{ row }">
          <el-tag>未交卷</el-tag>
          <!-- <span>未评卷</span>
          <span>已评卷</span> -->
        </template>
      </el-table-column>
      <!-- <el-table-column label="排名" prop="path" align="left">
        <template slot-scope="{ row }">
          <span>23</span>
        </template>
      </el-table-column> -->

      <el-table-column :label="$t('AbpUi[\'Actions\']')" align="left" width="210">
        <template slot-scope="{ row, $index }">
          <el-button type="text" title="查看答卷" class="el-icon-view" />
          <!-- <el-button type="primary" title="补交" plain>补交</el-button>
                <el-button type="text" title="删除" class="el-icon-delete" /> -->
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
  getAnswerSheets,
  deleteAnswerSheet
} from '@/api/exam/answer-sheet'
import Pagination from '@/components/Pagination' // secondary package based on el-pagination
import ExamScorePanel from './ExamScorePanel.vue'

export default {
  name: 'ExamScore',
  components: {
    Pagination,
    ExamScorePanel
  },
  data() {
    return {
      queryCreateDateTime: undefined,
      advanced: false,
      pickerOptions: pickerRangeWithHotKey,
      tableKey: 0,
      list: null,
      total: 0,
      listLoading: false,
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
      getAnswerSheets(this.listQuery).then(response => {
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
        deleteAnswerSheet(row.id).then(() => {
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

