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
            <el-form-item prop="filter" label="考试类型">
              <el-select v-model="value" placeholder="-">
                <el-option key="" value="模拟考试" lable="模拟考试" />
                <el-option key="" value="普通考试" lable="普通考试" />
                <el-option key="" value="竞赛考试" lable="竞赛考试" />
                <el-option key="" value="强化训练" lable="强化训练" />
                <el-option key="" value="任务考试" lable="任务考试" />
                <el-option key="" value="章节测试" lable="章节测试" />
              </el-select>
            </el-form-item>
          </el-col>
          <el-col :span="4">
            <el-form-item prop="filter" label="状态">
              <el-select v-model="value" placeholder="-">
                <el-option key="" value="未开始" lable="固定试卷" />
                <el-option key="" value="进行中" lable="随机试卷" />
                <el-option key="" value="已结束" lable="抽题试卷" />
              </el-select>
            </el-form-item>
          </el-col>
          <el-col :span="4">
            <el-form-item prop="testPaperId" label="试卷">
              <el-select v-model="listQuery.testPaperId" placeholder="请选择" filterable clearable>
                <el-option
                  v-for="item in testPaperOptions"
                  :key="item.id"
                  :label="item.name"
                  :value="item.id"
                />
              </el-select>
            </el-form-item>
          </el-col>
          <el-col :span="4">
            <el-form-item prop="filter" label="试卷类型">
              <el-select v-model="value" placeholder="-">
                <el-option key="" value="固定试卷" lable="固定试卷" />
                <el-option key="" value="随机试卷" lable="随机试卷" />
                <el-option key="" value="抽题试卷" lable="抽题试卷" />
                <el-option key="" value="混合试卷" lable="混合试卷" />
              </el-select>
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
              <el-link type="info" :underline="false" style="margin-left: 8px;line-height: 28px;" @click="toggleAdvanced">
                {{ advanced ? $t('AbpUi.Close') : $t('TigerUi.Expand') }}
                <i :class="advanced ? 'el-icon-arrow-up' : 'el-icon-arrow-down'" />
              </el-link>
            </el-button-group>
          </el-col>
        </el-row>

        <el-collapse-transition>
          <div v-show="advanced">
            <el-row :gutter="20">
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
            </el-row>
          </div>
        </el-collapse-transition>
      </el-form>

      <!-- 操作按钮 -->
      <el-row>
        <el-col>
          <el-button-group style="float:left">
            <el-button v-if="checkPermission('Exam.Exam.Create')" style="margin-right: 5px;" type="primary" icon="el-icon-plus" @click="handleCreate">
              {{ $t("AppExam['Permission:Create']") }}
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
      <el-table-column type="index" width="80" />
      <el-table-column :label="$t('AppExam[\'DisplayName:Name\']')" prop="name" sortable align="left">
        <template slot-scope="{ row }">
          <b>{{ row.name }}</b>
        </template>
      </el-table-column>
      <el-table-column label="试卷类型" prop="examDuration" align="left" width="100">
        <template slot-scope="{ row }">
          <el-tag type="primary">固定题目</el-tag>
        </template>
      </el-table-column>
      <el-table-column label="状态" prop="examDuration" align="left" width="100">
        <template slot-scope="{ row }">
          <el-tag type="success">进行中</el-tag>
        </template>
      </el-table-column>
      <el-table-column :label="$t('AppExam[\'DisplayName:ExamDate\']')" sortable prop="startDate" align="left" width="320">
        <template slot-scope="{ row }">
          <span>{{ row.startDate | moment }} - {{ row.endDate | moment }}</span>
        </template>
      </el-table-column>
      <el-table-column :label="$t('AppExam[\'DisplayName:ExamDuration\']')" prop="examDuration" align="left" width="120">
        <template slot-scope="{ row }">
          <span>{{ row.examDuration }}</span>
        </template>
      </el-table-column>
      <el-table-column label="总分" prop="examDuration" align="left" width="100">
        <template slot-scope="{ row }">
          <span>100</span>
        </template>
      </el-table-column>
      <el-table-column :label="$t('AppExam[\'DisplayName:Enable\']')" prop="isEnable" align="left" width="120">
        <template slot-scope="{ row }">
          <el-tag :type="( row.isEnable ? 'success' : 'danger')" :class="[ row.isEnable ? 'el-icon-check':'el-icon-close' ]" />
        </template>
      </el-table-column>
      <el-table-column :label="$t('AbpUi[\'DisplayName:CreationTime\']')" prop="isEnable" sortable align="left" width="180">
        <template slot-scope="{ row }">
          <span>{{ row.creationTime | moment }}</span>
        </template>
      </el-table-column>

      <el-table-column :label="$t('AbpUi[\'Actions\']')" align="left" width="180">
        <template slot-scope="{ row, $index }">
          <el-button v-if="checkPermission('Exam.Exam.Update')" type="primary" class="el-icon-edit" :title="$t('AbpUi[\'Edit\']')" @click="handleUpdate(row)" />
          <el-button v-if="checkPermission('Exam.Exam.Delete')" type="danger" class="el-icon-delete" :title="$t('AbpUi[\'Delete\']')" @click="handleDelete(row, $index)" />

          <el-dropdown style="margin-left:8px;" @command="handleCommand">
            <el-button class="el-icon-more" :title="$t('AbpIdentity[\'Actions\']')" type="primary" plain />
            <el-dropdown-menu slot="dropdown">
              <el-dropdown-item :command="beforeHandleCommand(row, 'handleUpdateExamSetting')">
                考试设置
              </el-dropdown-item>
              <el-dropdown-item :command="beforeHandleCommand(row, 'handleUpdateExamSetting')">
                复制
              </el-dropdown-item>
              <el-dropdown-item :command="beforeHandleCommand(row, 'handleViewExamScore')">
                查看成绩
              </el-dropdown-item>
              <el-dropdown-item :command="beforeHandleCommand(row, 'handleExamJudge')">
                人工评卷
              </el-dropdown-item>
              <el-dropdown-item :command="beforeHandleCommand(row, 'handleViewExamScoreAnalysis')">
                成绩统计
              </el-dropdown-item>
              <el-dropdown-item :command="beforeHandleCommand(row, 'handleViewQuestionAnalysis')">
                答题统计
              </el-dropdown-item>
              <el-dropdown-item :command="beforeHandleCommand(row, 'handleViewExamOrgAnalysis')">
                组织统计
              </el-dropdown-item>
              <el-dropdown-item :command="beforeHandleCommand(row, 'handleViewExamAbsentUser')">
                缺考统计
              </el-dropdown-item>
            </el-dropdown-menu>
          </el-dropdown>
        </template>
      </el-table-column>
    </el-table>

    <pagination v-show="total > 0" :total="total" :page.sync="listQuery.page" :limit.sync="listQuery.limit" @pagination="getList" />

    <exam-setting ref="examSetting" />
    <exam-analysis ref="examAnalysise" />
  </div>
</template>

<script>
import ExamSetting from './ExamSetting.vue'
import Pagination from '@/components/Pagination' // secondary package based on el-pagination
import { pickerRangeWithHotKey } from '@/utils/picker'
import baseListQuery, { checkPermission } from '@/utils/abp'
import {
  getExams,
  deleteExam
} from '@/api/exam/exam'
import {
  getAllTestPaper
} from '@/api/exam/test-paper'

import ExamAnalysis from './ExamAnalysis.vue'

export default {
  name: 'ExamTable',
  components: {
    Pagination,
    ExamSetting,
    ExamAnalysis
  },
  data() {
    return {
      testPaperOptions: [],
      queryCreateDateTime: undefined,
      advanced: false,
      pickerOptions: pickerRangeWithHotKey,
      tableKey: 0,
      list: null,
      total: 0,
      listLoading: true,
      listQuery: Object.assign({
        courseId: undefined,
        testPaperId: undefined,
        examType: undefined,
        startDate: undefined,
        endDate: undefined,
        isEnable: undefined,
        createStartTime: undefined,
        createEndTime: undefined
      }, baseListQuery)
    }
  },
  created() {
    this.fetchTestPaperOptions()
    this.getList()
  },
  methods: {
    checkPermission, // 检查权限
    fetchTestPaperOptions() {
      getAllTestPaper().then(response => {
        this.testPaperOptions = response.items
      })
    },
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
      getExams(this.listQuery).then(response => {
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
    beforeHandleCommand(scope, command) {
      return {
        scope: scope,
        command: command
      }
    },
    handleCommand(param) {
      switch (param.command) {
        case 'handleUpdateExamSetting':
          this.handleUpdateExamSetting(param.scope)
          break
        case 'handleViewExamScore':
          this.handleViewExamScore(param.scope)
          break
        case 'handleExamJudge':
          this.handleExamJudge(param.scope)
          break
        case 'handleViewExamScoreAnalysis':
          this.handleViewExamScoreAnalysis(param.scope)
          break
        case 'handleViewQuestionAnalysis':
          this.handleViewQuestionAnalysis(param.scope)
          break
        case 'handleViewExamOrgAnalysis':
          this.handleViewExamOrgAnalysis(param.scope)
          break
        case 'handleViewExamAbsentUser':
          this.handleViewExamAbsentUser(param.scope)
          break
        default:
          break
      }
    },
    handleCreate() {
      this.$emit('handleCreate')
    },
    handleUpdate(row) {
      this.$emit('handleUpdate', row)
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
        deleteExam(row.id).then(() => {
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
    // 更新考试设置
    handleUpdateExamSetting(row) {
      this.$refs['examSetting'].handleUpdate(row)
    },
    // 查看考试成绩
    handleViewExamScore(row) {
      this.$refs['examAnalysise'].handleViewExamAnalysis(row, 'first')
    },
    handleExamJudge(row) {
      this.$refs['examAnalysise'].handleViewExamAnalysis(row, 'second')
    },
    handleViewExamScoreAnalysis(row) {
      this.$refs['examAnalysise'].handleViewExamAnalysis(row, 'third')
    },
    handleViewQuestionAnalysis(row) {
      this.$refs['examAnalysise'].handleViewExamAnalysis(row, 'fourth')
    },
    handleViewExamOrgAnalysis(row) {
      this.$refs['examAnalysise'].handleViewExamAnalysis(row, 'fifth')
    },
    handleViewExamAbsentUser(row) {
      this.$refs['examAnalysise'].handleViewExamAnalysis(row, 'sixth')
    }
  }
}
</script>

<style scoped>
.line{
  text-align: center;
}
</style>

