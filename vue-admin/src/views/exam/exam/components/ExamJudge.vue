<template>
  <div class="table-container">
    <div class="filter-container" style="margin-bottom:10px;">
      <el-form ref="logQueryForm" label-position="left" label-width="80px" :model="listQuery">
        <el-row :gutter="20">
          <el-col :span="4">
            <el-form-item prop="filter" label="答卷状态">
              <el-select v-model="listQuery.answerSheetStatus" placeholder="-" filterable clearable>
                <el-option key="1" :value="1" label="未交卷" />
                <el-option key="2" :value="2" label="已交卷" />
                <el-option key="3" :value="3" label="已阅卷" />
              </el-select>
            </el-form-item>
          </el-col>

          <el-col :span="4">
            <el-form-item prop="filter" label="是否及格">
              <el-select v-model="listQuery.isPass" placeholder="-" filterable clearable>
                <el-option key="1" :value="true" label="及格" />
                <el-option key="2" :value="false" label="不及格" />
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
            <!-- <el-button icon="el-icon-download" @click="handleDownload">
              导出
            </el-button>
            <el-button type="primary" icon="el-icon-download" @click="handleDownload">
              发布成绩
            </el-button> -->
          </el-button-group>

        </el-col>
      </el-row>
    </div>

    <el-table :key="tableKey" v-loading="listLoading" :data="list" border fit highlight-current-row :stripe="true" style="width: 100%;" @sort-change="sortChange">
      <el-table-column type="selection" width="55" center />
      <el-table-column type="index" width="50" />
      <el-table-column label="用户名" prop="name" align="left" width="120">
        <template slot-scope="{ row }">
          <span>ZhangSan</span>
        </template>
      </el-table-column>
      <el-table-column label="姓名" prop="name" align="left" width="80">
        <template slot-scope="{ row }">
          <span>张三</span>
        </template>
      </el-table-column>
      <el-table-column label="所属组织" prop="name" align="left" width="180">
        <template slot-scope="{ row }">
          <span>浙江大学/电气2班</span>
        </template>
      </el-table-column>
      <el-table-column label="开始时间/交卷时间" align="left" width="240">
        <template slot-scope="{ row }">
          <span>{{ row.creationTime | moment }}   {{ row.submitDateTime |moment }}</span>
        </template>
      </el-table-column>
      <el-table-column label="用时" prop="description" align="left">
        <template slot-scope="{ row }">
          <span>{{ row.answerTotalDuration }} 分钟</span>
        </template>
      </el-table-column>
      <el-table-column label="客观题得分" prop="path" align="left">
        <template slot-scope="{ row }">
          <span>{{ row.objectiveScore }}</span>
        </template>
      </el-table-column>
      <el-table-column label="主观题得分" prop="path" align="left">
        <template slot-scope="{ row }">
          <span>{{ row.operateManualScore }}</span>
        </template>
      </el-table-column>
      <el-table-column label="成绩" prop="path" align="left">
        <template slot-scope="{ row }">
          <span>{{ row.totalScore }}</span>
        </template>
      </el-table-column>
      <el-table-column label="是否及格" prop="path" align="left">
        <template slot-scope="{ row }">
          <el-tag :type="( row.isPass ? 'success' : 'danger')" :class="[ row.isPass ? 'el-icon-check':'el-icon-close' ]" />
        </template>
      </el-table-column>
      <el-table-column label="状态" prop="path" align="left">
        <template slot-scope="{ row }">
          <el-tag :type="AnswerSheetStatusType[row.status]">{{ AnswerSheetStatusMap[row.status] }}</el-tag>
        </template>
      </el-table-column>
      <el-table-column label="排名" prop="path" align="left">
        <template slot-scope="{ row }">
          <span>23</span>
        </template>
      </el-table-column>
      <!-- <el-table-column label="评语" prop="path" align="left" width="180" :show-overflow-tooltip="true">
        <template slot-scope="{ row }">
          <span>做的不错，再接再厉！</span>
        </template>
      </el-table-column> -->

      <el-table-column :label="$t('AbpUi[\'Actions\']')" align="left" width="210">
        <template slot-scope="{ row, $index }">
          <el-button type="text" title="查看答卷" @click="handleJudgePaper(row)">去评卷</el-button>
          <el-button type="text" title="补交" plain>补交</el-button>
        </template>
      </el-table-column>
    </el-table>

    <pagination v-show="total > 0" :total="total" :page.sync="listQuery.page" :limit.sync="listQuery.limit" @pagination="getList" />

    <judge-paper ref="judgePaper" />
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
import { AnswerSheetStatusMap, AnswerSheetStatusType } from '../datas/typing'
import JudgePaper from './JudgePaper.vue'

export default {
  name: 'ExamJudge',
  components: {
    Pagination,
    JudgePaper
  },
  data() {
    return {
      AnswerSheetStatusMap,
      AnswerSheetStatusType,
      value: '',
      queryCreateDateTime: undefined,
      advanced: false,
      pickerOptions: pickerRangeWithHotKey,
      tableKey: 0,
      list: null,
      total: 0,
      listLoading: true,
      listQuery: Object.assign({
        examId: undefined,
        organizationUnitId: undefined,
        isPass: undefined,
        answerSheetStatus: undefined,
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
        examId: undefined,
        organizationUnitId: undefined,
        isPass: undefined,
        answerSheetStatus: undefined,
        createStartTime: undefined,
        createEndTime: undefined
      }, baseListQuery)
    },
    // 获取列表数据
    getList() {
      this.listLoading = true
      if (this.queryCreateDateTime) {
        this.listQuery.createStartTime = this.queryCreateDateTime[0]
        this.listQuery.createEndTime = this.queryCreateDateTime[1]
      }
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
    // 打开阅卷页面
    handleJudgePaper(row) {
      this.$refs['judgePaper'].getAnswerSheet(row)
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

