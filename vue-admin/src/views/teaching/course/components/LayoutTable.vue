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
            <el-form-item prop="filter" label="课程状态">
              <el-select v-model="value" placeholder="-">
                <el-option key="" value="进行中" lable="进行中" />
                <el-option key="" value="未开始" lable="未开始" />
                <el-option key="" value="已结束" lable="已结束" />
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
                range-separator="---"
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
                {{ $t('AbpUi.Reset') }}
              </el-button>
              <!-- <el-link type="info" :underline="false" style="margin-left: 8px;line-height: 28px;" @click="toggleAdvanced">
                {{ advanced ? $t('AbpUi.Close') : $t('TigerUi.Expand') }}
                <i :class="advanced ? 'el-icon-arrow-up' : 'el-icon-arrow-down'" />
              </el-link> -->
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

      <!-- 操作按钮 -->
      <el-row>
        <el-col>
          <el-button-group style="float:left">
            <el-button v-if="checkPermission('Exam.TestPaper.Create')" style="margin-right: 5px;" type="primary" icon="el-icon-plus" @click="handleCreate">
              {{ $t("AppExam['Permission:Create']") }}
            </el-button>
          </el-button-group>

        </el-col>
      </el-row>
    </div>

    <el-table :key="tableKey" v-loading="listLoading" :data="list" border fit highlight-current-row :stripe="true" style="width: 100%;" @sort-change="sortChange">
      <el-table-column type="selection" width="55" center />
      <el-table-column type="index" width="80" />
      <el-table-column label="封面" align="center" width="240">
        <template slot-scope="{ row }">
          <span>
            <el-image style="width: 240px; height: 80px" src="https://img-ph-mirror.nosdn.127.net/tYhzuDVilzlDOo2bEyH_Qg==/6608226511143817333.jpg" fit="contain" :preview-src-list="['https://img-ph-mirror.nosdn.127.net/tYhzuDVilzlDOo2bEyH_Qg==/6608226511143817333.jpg']">
              <div slot="error" class="image-slot">
                <i class="el-icon-picture-outline" />
              </div>
            </el-image>
          </span>
        </template>
      </el-table-column>

      <el-table-column label="课程名称" prop="name" sortable align="left">
        <template slot-scope="{ row }">
          <span>{{ row.name }}</span>
        </template>
      </el-table-column>
      <el-table-column label="状态" prop="name" sortable align="left" width="80">
        <template slot-scope="{ row }">
          <el-tag type="primary">未开始</el-tag>
        </template>
      </el-table-column>
      <el-table-column label="时长" prop="duration" sortable align="left" width="120">
        <template slot-scope="{ row }">
          <span>12小时</span>
        </template>
      </el-table-column>
      <el-table-column label="开始时间/结束时间" sortable prop="startDate" align="left" width="320">
        <template slot-scope="{ row }">
          <span>{{ row.startDate | moment }} - {{ row.endDate | moment }}</span>
        </template>
      </el-table-column>
      <el-table-column label="创建时间" prop="name" sortable align="left" width="180">
        <template slot-scope="{ row }">
          <span>{{ row.creationTime | moment }}</span>
        </template>
      </el-table-column>

      <el-table-column :label="$t('AbpUi[\'Actions\']')" align="left" width="280">
        <template slot-scope="{ row, $index }">
          <el-button v-if="checkPermission('Platform.Layout.Update')" type="primary" class="el-icon-edit" :title="$t('AbpUi[\'Edit\']')" @click="handleUpdate(row)" />
          <el-button v-if="checkPermission('Platform.Layout.Delete')" type="danger" class="el-icon-delete" :title="$t('AbpUi[\'Delete\']')" @click="handleDelete(row, $index)" />

          <el-dropdown style="margin-left:8px;" @command="handleCommand">
            <el-button class="el-icon-more" :title="$t('AbpIdentity[\'Actions\']')" type="primary" plain />
            <el-dropdown-menu slot="dropdown">
              <el-dropdown-item :command="beforeHandleCommand(row, 'handleCourseSetting')">
                课程设置
              </el-dropdown-item>
              <el-dropdown-item :command="beforeHandleCommand(row, 'handleViewLearnAnalysis')">
                学习统计
              </el-dropdown-item>
              <el-dropdown-item :command="beforeHandleCommand(row, 'handleViewCourseComment')">
                课程评论
              </el-dropdown-item>
            </el-dropdown-menu>
          </el-dropdown>
        </template>
      </el-table-column>
    </el-table>

    <pagination v-show="total > 0" :total="total" :page.sync="listQuery.page" :limit.sync="listQuery.limit" @pagination="getList" />

    <course-setting ref="courseSetting" />

    <course-analysis ref="courseAnalysis" />
  </div>
</template>

<script>
import baseListQuery, { checkPermission } from '@/utils/abp'
import {
  getLayouts,
  deleteLayout
} from '@/api/system-manage/platform/layout'
import Pagination from '@/components/Pagination' // secondary package based on el-pagination
import { pickerRangeWithHotKey } from '@/utils/picker'
import CourseSetting from './CourseSetting.vue'
import CourseAnalysis from './CourseAnalysis.vue'

export default {
  name: 'LayoutTable',
  components: {
    Pagination,
    CourseSetting,
    CourseAnalysis
  },
  data() {
    return {
      tableKey: 0,
      list: null,
      total: 0,
      listLoading: true,
      pickerOptions: pickerRangeWithHotKey,
      advanced: false,
      queryCreateDateTime: undefined,
      listQuery: Object.assign({
        degree: undefined,
        questionCategoryId: undefined,
        createStartTime: undefined,
        createEndTime: undefined,
        type: undefined,
        enable: undefined
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
    beforeHandleCommand(scope, command) {
      return {
        scope: scope,
        command: command
      }
    },
    handleCommand(param) {
      switch (param.command) {
        case 'handleCourseSetting':
          this.handleCourseSetting(param.scope)
          break
        case 'handleViewLearnAnalysis':
          this.handleViewLearnAnalysis(param.scope)
          break
        case 'handleViewCourseComment':
          this.handleViewCourseComment(param.scope)
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
    // 更新课程设置
    handleCourseSetting(row) {
      this.$refs['courseSetting'].handleUpdate(row)
    },
    // 查看学生学习进度
    handleViewLearnAnalysis(row) {
      this.$refs['courseAnalysis'].handleViewCourseAnalysis(row, 'first')
    },
    handleViewCourseComment(row) {
      this.$refs['courseAnalysis'].handleViewCourseAnalysis(row, 'second')
    },
    handleViewExamScoreAnalysis(row) {
      this.$refs['examAnalysise'].handleViewCourseAnalysis(row, 'third')
    },
    handleViewQuestionAnalysis(row) {
      this.$refs['examAnalysise'].handleViewCourseAnalysis(row, 'fourth')
    },
    handleViewExamOrgAnalysis(row) {
      this.$refs['examAnalysise'].handleViewCourseAnalysis(row, 'fifth')
    },
    handleViewExamAbsentUser(row) {
      this.$refs['examAnalysise'].handleViewCourseAnalysis(row, 'sixth')
    }
  }
}
</script>

<style scoped>
.line{
  text-align: center;
}
</style>

