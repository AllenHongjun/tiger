<template>
  <div class="app-container">
    <div class="filter-container" style="margin-bottom:10px;">
      <el-form ref="logQueryForm" label-position="left" label-width="100px" :model="queryForm">
        <el-row :gutter="20">
          <el-col :span="8">
            <el-form-item label="日期">
              <el-date-picker v-model="queryDateTime" type="datetimerange" align="right" unlink-panels :picker-options="pickerOptions" :range-separator="$t('AbpAuditLogging[\'RangeSeparator\']')" :start-placeholder="$t('AbpAuditLogging[\'StartPlaceholder\']')" :end-placeholder="$t('AbpAuditLogging[\'EndPlaceholder\']')" @change="datePickerChange" />
            </el-form-item>
          </el-col>
          <el-col :span="4">
            <el-form-item prop="userName" :label="$t('AbpAuditLogging[\'UserName\']')">
              <el-input v-model="queryForm.userName" :placeholder="$t('AbpAuditLogging[\'PlaceholderInput\']')" />
            </el-form-item>
          </el-col>
          <el-col :span="4">
            <el-form-item prop="url" :label="$t('AbpAuditLogging[\'Url\']')">
              <el-input v-model="queryForm.url" :placeholder="$t('AbpAuditLogging[\'PlaceholderInput\']')" />
            </el-form-item>
          </el-col>

          <el-col :span="4">
            <el-form-item prop="hasException" :label="$t('AbpAuditLogging[\'Exceptions\']')">
              <el-select v-model="queryForm.hasException" clearable style="width:100%" @clear="queryForm.httpStatusCode=undefined">
                <el-option label="有" value="true" />
                <el-option label="无" value="false" />
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
                {{ advanced ? '收起' : '展开' }}
                <i :class="advanced ? 'el-icon-arrow-up' : 'el-icon-arrow-down'" />
              </el-link>
            </el-button-group>
          </el-col>
        </el-row>

        <el-collapse-transition>
          <div v-show="advanced">
            <el-row :gutter="20">
              <el-col :span="4">
                <el-form-item prop="httpMethod" :label="$t('AbpAuditLogging[\'HttpMethod\']')">
                  <el-select v-model="queryForm.httpMethod" clearable style="width:100%" @clear="queryForm.httpMethod=undefined">
                    <el-option label="GET" value="GET" />
                    <el-option label="PUT" value="PUT" />
                    <el-option label="POST" value="POST" />
                    <el-option label="DELETE" value="DELETE" />
                    <el-option label="HEAD" value="HEAD" />
                    <el-option label="CONNECT" value="CONNECT" />
                    <el-option label="OPTIONS" value="OPTIONS" />
                    <el-option label="TRACE" value="TRACE" />
                  </el-select>
                </el-form-item>
              </el-col>
              <el-col :span="4">
                <el-form-item prop="httpStatueCode" :label="$t('AbpAuditLogging[\'HttpStatusCode\']')">
                  <el-select v-model="queryForm.httpStatusCode" clearable style="width:100%" @clear="queryForm.httpStatusCode=undefined">
                    <el-option label=" 100 - Continue " value="100" />
                    <el-option label=" 200 - Ok " value="200" />
                    <el-option label=" 300 - Multiple Choices " value="300" />
                    <el-option label=" 400 - Bad Request " value="400" />
                    <el-option label=" 500 - Internal Server Error " value="500" />
                  </el-select>
                </el-form-item>
              </el-col>
              <el-col :span="4">
                <el-form-item prop="applicationName" :label="$t('AbpAuditLogging[\'ApplicationName\']')">
                  <el-input v-model="queryForm.applicationName" :placeholder="$t('AbpAuditLogging[\'PlaceholderInput\']')" />
                </el-form-item>
              </el-col>

              <el-col :span="4">
                <el-form-item prop="minExecutionDuration" :label="$t('AbpAuditLogging[\'MinExecutionDuration\']')">
                  <el-input v-model="queryForm.minExecutionDuration" type="number" :placeholder="$t('AbpAuditLogging[\'PlaceholderInput\']')" />
                </el-form-item>
              </el-col>
              <el-col :span="4">
                <el-form-item prop="maxExecutionDuration" :label="$t('AbpAuditLogging[\'MaxExecutionDuration\']')">
                  <el-input v-model="queryForm.maxExecutionDuration" type="number" :placeholder="$t('AbpAuditLogging[\'PlaceholderInput\']')" />
                </el-form-item>
              </el-col>
            </el-row>
          </div>
        </el-collapse-transition>
        <!-- 操作按钮 -->
        <el-row>
          <el-col :span="4">
            <el-button-group style="float:left">
              <el-button v-if="checkPermission('TaskManagement.BackgroundJobs.Create')" class="filter-item" style="margin-left: 10px;" type="primary" icon="el-icon-plus" @click="handleCreate">
                {{ $t("TaskManagement['Permissions:CreateJob']") }}
              </el-button>
              <el-button type="primary" icon="el-icon-refresh" @click="handleRefresh">
                刷新
              </el-button>
              <el-button type="reset" icon="el-icon-download" @click="handleDownload">
                导出
              </el-button>
            </el-button-group>

          </el-col>
        </el-row>

      </el-form>
    </div>

    <el-table :key="tableKey" v-loading="listLoading" :data="list" border fit highlight-current-row :stripe="true" style="width: 100%;" @sort-change="sortChange">
      <el-table-column type="index" width="80" />
      <el-table-column :label="$t('TaskManagement[\'DisplayName:Group\']')" prop="group" sortable align="center">
        <template slot-scope="{ row }">
          <span>{{ row.group }}</span>
        </template>
      </el-table-column>
      <el-table-column :label="$t('TaskManagement[\'DisplayName:Name\']')" prop="name" sortable align="center">
        <template slot-scope="{ row }">
          <span>{{ row.name }}</span>
        </template>
      </el-table-column>
      <el-table-column :label="$t('TaskManagement[\'DisplayName:Description\']')" prop="description" sortable align="center">
        <template slot-scope="{ row }">
          <span>{{ row.description }}</span>
        </template>
      </el-table-column>
      <el-table-column :label="$t('TaskManagement[\'DisplayName:CreationTime\']')" prop="creationTime" sortable align="center">
        <template slot-scope="{ row }">
          <span>{{ row.creationTime | moment }}</span>
        </template>
      </el-table-column>
      <el-table-column :label="$t('TaskManagement[\'DisplayName:Status\']')" prop="status" sortable align="center">
        <template slot-scope="{ row }">
          <span>{{ row.status }}</span>
        </template>
      </el-table-column>
      <el-table-column :label="$t('TaskManagement[\'DisplayName:Result\']')" prop="result" sortable align="center">
        <template slot-scope="{ row }">
          <span>{{ row.result }}</span>
        </template>
      </el-table-column>
      <el-table-column :label="$t('TaskManagement[\'DisplayName:LastRunTime\']')" prop="lastRunTime" sortable align="center">
        <template slot-scope="{ row }">
          <span>{{ row.lastRunTime | moment }}</span>
        </template>
      </el-table-column>
      <el-table-column :label="$t('TaskManagement[\'DisplayName:NextRunTime\']')" prop="nextRunTime" sortable align="center">
        <template slot-scope="{ row }">
          <span>{{ row.nextRunTime | moment }}</span>
        </template>
      </el-table-column>
      <el-table-column :label="$t('TaskManagement[\'DisplayName:JobType\']')" prop="jobType" sortable align="center">
        <template slot-scope="{ row }">
          <span>{{ row.jobType }}</span>
        </template>
      </el-table-column>
      <el-table-column :label="$t('TaskManagement[\'DisplayName:Priority\']')" prop="priority" sortable align="center">
        <template slot-scope="{ row }">
          <span>{{ row.priority }}</span>
        </template>
      </el-table-column>
      <el-table-column :label="$t('TaskManagement[\'DisplayName:Cron\']')" prop="cron" sortable align="center">
        <template slot-scope="{ row }">
          <span>{{ row.cron }}</span>
        </template>
      </el-table-column>
      <el-table-column :label="$t('TaskManagement[\'DisplayName:TriggerCount\']')" prop="triggerCount" sortable align="center">
        <template slot-scope="{ row }">
          <span>{{ row.triggerCount }}</span>
        </template>
      </el-table-column>
      <el-table-column :label="$t('TaskManagement[\'DisplayName:TryCount\']')" prop="tryCount" sortable align="center">
        <template slot-scope="{ row }">
          <span>{{ row.tryCount }}</span>
        </template>
      </el-table-column>

      <el-table-column :label="$t('AbpUi[\'Actions\']')" align="center" width="200" class-name="small-padding fixed-width">
        <template slot-scope="{ row, $index }">
          <el-button v-if="checkPermission('TaskManagement.BackgroundJobs.Update')" type="primary" @click="handleUpdate(row)">
            {{ $t("AbpUi['Edit']") }}
          </el-button>
          <el-button v-if="checkPermission('TaskManagement.BackgroundJobs.Delete')" type="danger" @click="handleDelete(row, $index)">
            {{ $t("AbpUi['Delete']") }}
          </el-button>
        </template>
      </el-table-column>
    </el-table>

    <pagination v-show="total > 0" :total="total" :page.sync="queryForm.page" :limit.sync="queryForm.limit" @pagination="getList" />

    <el-dialog :title=" dialogStatus == 'create'? $t('TaskManagement[\'Permissions:CreateJob\']'): $t('AbpUi[\'Edit\']')" :visible.sync="dialogFormVisible">
      <el-form ref="dataForm" :rules="rules" :model="temp" label-position="right" label-width="150px">

        <!-- <el-row>
          <el-col :span="12">
            <div class="grid-content">
              1
            </div>
          </el-col>
          <el-col :span="12">
            <div class="grid-content">
              2
            </div>
          </el-col>
        </el-row> -->

        <el-row>
          <el-col :span="12">
            <div class="grid-content">
              <el-form-item :label="$t('TaskManagement[\'DisplayName:Group\']')" prop="group">
                <el-input v-model="temp.group" />
              </el-form-item>
            </div>
          </el-col>
          <el-col :span="12">
            <div class="grid-content">
              <el-form-item :label="$t('TaskManagement[\'DisplayName:Name\']')" prop="name">
                <el-input v-model="temp.name" />
              </el-form-item>
            </div>
          </el-col>
        </el-row>

        <el-form-item :label="$t('TaskManagement[\'DisplayName:IsEnabled\']')" prop="isEnabled">
          <template>
            <el-radio v-model="temp.isEnabled" :label="true">是</el-radio>
            <el-radio v-model="temp.isEnabled" :label="false">否</el-radio>
          </template>
        </el-form-item>

        <el-form-item :label="$t('TaskManagement[\'DisplayName:Type\']')" prop="type">
          <el-input v-model="temp.type" type="textarea" :autosize="{ minRows: 3, maxRows: 6}" />
        </el-form-item>
        <el-row>
          <el-col :span="12">
            <div class="grid-content">
              <el-form-item :label="$t('TaskManagement[\'DisplayName:BeginTime\']')" prop="beginTime">
                <el-date-picker v-model="temp.beginTime" type="datetime" :placeholder="$t('TaskManagement[\'DisplayName:BeginTime\']')" default-time="12:00:00" />
              </el-form-item>
            </div>
          </el-col>
          <el-col :span="12">
            <div class="grid-content">
              <el-form-item :label="$t('TaskManagement[\'DisplayName:EndTime\']')" prop="endTime">
                <el-date-picker v-model="temp.endTime" type="datetime" :placeholder="$t('TaskManagement[\'DisplayName:EndTime\']')" default-time="12:00:00" />
              </el-form-item>
            </div>
          </el-col>
        </el-row>
        <el-row>
          <el-col :span="12">
            <div class="grid-content">
              <el-form-item :label="$t('TaskManagement[\'DisplayName:JobType\']')" prop="jobType">
                <el-select v-model="temp.jobType" placeholder="请选择..." :clearable="true">
                  <el-option label="一次性的" :value="0" />
                  <el-option label="周期性的" :value="1" />
                  <el-option label="持续性的" :value="2" />
                </el-select>
              </el-form-item>
            </div>
          </el-col>
          <el-col :span="12">
            <div class="grid-content">
              <el-form-item :label="$t('TaskManagement[\'DisplayName:Priority\']')" prop="priority">
                <el-select v-model="temp.priority" placeholder="请选择..." :clearable="true">
                  <el-option label="Low" :value="5" />
                  <el-option label="BelowNormal" :value="10" />
                  <el-option label="Normal" :value="15" />
                  <el-option label="AboveNormal" :value="20" />
                  <el-option label="High" :value="25" />
                </el-select>
              </el-form-item>
            </div>
          </el-col>
        </el-row>

        <el-row>
          <el-col :span="12">
            <div class="grid-content">
              <el-form-item :label="$t('TaskManagement[\'DisplayName:Cron\']')" prop="cron">
                <el-input v-model="temp.cron" />
              </el-form-item>
            </div>
          </el-col>
          <el-col :span="12">
            <div class="grid-content">

              <el-form-item :label="$t('TaskManagement[\'DisplayName:Interval\']')" prop="interval">
                <el-input-number v-model="temp.interval" :min="1" :max="100000000" />
              </el-form-item>
            </div>
          </el-col>
        </el-row>

        <el-row>
          <el-col :span="12">
            <div class="grid-content">
              <el-form-item :label="$t('TaskManagement[\'DisplayName:MaxCount\']')" prop="maxCount">
                <el-input-number v-model="temp.maxCount" :min="1" :max="100000000" />
              </el-form-item>
            </div>
          </el-col>
          <el-col :span="12">
            <div class="grid-content">
              <el-form-item :label="$t('TaskManagement[\'DisplayName:MaxTryCount\']')" prop="maxTryCount">
                <el-input-number v-model="temp.maxTryCount" :min="1" :max="100000000" />
              </el-form-item>
            </div>
          </el-col>
        </el-row>
        <el-form-item :label="$t('TaskManagement[\'DisplayName:LockTimeOut\']')" prop="lockTimeOut">
          <el-input-number v-model="temp.lockTimeOut" :min="1" :max="100000000" />
        </el-form-item>

        <el-form-item :label="$t('TaskManagement[\'DisplayName:Description\']')" prop="description">
          <el-input v-model="temp.description" type="textarea" :autosize="{ minRows: 2, maxRows: 4}" />
        </el-form-item>
        <el-form-item :label="$t('TaskManagement[\'DisplayName:Result\']')" prop="result">
          <el-input v-model="temp.result" type="textarea" :autosize="{ minRows: 1, maxRows: 4}" />
        </el-form-item>

      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button @click="dialogFormVisible = false">
          {{ $t("AbpUi['Cancel']") }}
        </el-button>
        <el-button type="primary" @click="dialogStatus === 'create' ? createData() : updateData()">
          {{ $t("AbpUi['Save']") }}
        </el-button>
      </div>
    </el-dialog>
  </div>
</template>

<script>
import {
  getBackgroundJobs,
  getBackgroundJob,
  getBackgroundJobsAll,
  createBackgroundJob,
  updateBackgroundJob,
  deleteBackgroundJob
} from '@/api/system-manage/task/background-job'
import Pagination from '@/components/Pagination' // secondary package based on el-pagination
import baseListQuery, {
  checkPermission
} from '@/utils/abp'
import {
  pickerRangeWithHotKey
} from '@/utils/picker'
import {
  parseTime
} from '@/utils'

export default {
  name: 'BackgroundJobs',
  components: {
    Pagination
  },
  filters: {
    requestDurationFilter(duration) {
      let type = 'success'
      if (duration > 2 * 1000) {
        type = 'warning'
      } else if (duration > 5 * 1000) {
        type = 'error'
      }
      return type
    },
    requestStatusCode(code) {
      let type = 'success'
      switch (code) {
        case 401:
        case 403:
        case 404:
          type = 'warning'
          break
        case 500:
          type = 'danger'
          break
      }
      return type
    },
    requestMethodFilter(method) {
      let type = 'success'
      switch (method.toUpperCase()) {
        case 'GET':
          type = ''
          break
        case 'PUT':
          type = 'warning'
          break
        case 'POST':
          type = 'success'
          break
        case 'DELETE':
          type = 'danger'
          break
        default:
          type = 'Info'
      }
      return type
    }
  },
  data() {
    return {
      tableKey: 0,
      list: null,
      total: 0,
      listLoading: true,
      advanced: false, // 判断搜索栏展开/收起
      queryDateTime: undefined,
      queryForm: Object.assign({
        startTime: undefined,
        endTime: undefined,
        userName: undefined,
        url: undefined,
        httpMethod: undefined,
        httpStatusCode: undefined,
        applicationName: undefined,
        clientIpAddress: undefined,
        hasException: undefined,
        minExecutionDuration: undefined,
        maxExecutionDuration: undefined
      }, baseListQuery),
      pickerOptions: pickerRangeWithHotKey,
      downloadLoading: false, // 下载控制
      temp: {
        id: undefined,
        isEnabled: true,
        args: {},
        description: '',
        jobType: undefined,
        cron: '',
        maxTryCount: 0,
        maxCount: 0,
        interval: 0,
        priority: 0,
        lockTimeOut: 0,
        name: '',
        group: '',
        type: '',
        nodeName: '',
        beginTime: undefined,
        endTime: undefined,
        source: 0
      },
      dialogFormVisible: false,
      dialogStatus: '',

      // 表单验证规则
      rules: {
        group: [
          {
            required: true,
            message: this.$i18n.t("AbpValidation['The {0} field is required.']", [
              this.$i18n.t("TaskManagement['DisplayName:Group']")
            ]),
            trigger: 'blur'
          },
          {
            max: 256,
            message: this.$i18n.t(
              "AbpValidation['The field {0} must be a string with a maximum length of {1}.']",
              [this.$i18n.t("TaskManagement['DisplayName:Group']"), '256']
            ),
            trigger: 'blur'
          }
        ],
        name: [
          {
            required: true,
            message: this.$i18n.t("AbpValidation['The {0} field is required.']", [
              this.$i18n.t("TaskManagement['DisplayName:Name']")
            ]),
            trigger: 'blur'
          },
          {
            max: 100,
            message: this.$i18n.t(
              "AbpValidation['The field {0} must be a string with a maximum length of {1}.']",
              [this.$i18n.t("TaskManagement['DisplayName:Name']"), '100']
            ),
            trigger: 'blur'
          }
        ],

        type: [
          {
            required: true,
            message: this.$i18n.t("AbpValidation['The {0} field is required.']", [
              this.$i18n.t("TaskManagement['DisplayName:Type']")
            ]),
            trigger: 'blur'
          },
          {
            max: 1000,
            message: this.$i18n.t(
              "AbpValidation['The field {0} must be a string with a maximum length of {1}.']",
              [this.$i18n.t("TaskManagement['DisplayName:Type']"), '1000']
            ),
            trigger: 'blur'
          }
        ],
        beginTime: [
          {
            required: true,
            message: this.$i18n.t("AbpValidation['The {0} field is required.']", [
              this.$i18n.t("TaskManagement['DisplayName:BeginTime']")
            ]),
            trigger: 'blur'
          }
        ],
        description: [
          {
            max: 255,
            message: this.$i18n.t(
              "AbpValidation['The field {0} must be a string with a maximum length of {1}.']",
              [this.$i18n.t("TaskManagement['DisplayName:Description']"), '255']
            ),
            trigger: 'blur'
          }
        ],
        cron: [
          {
            max: 50,
            message: this.$i18n.t(
              "AbpValidation['The field {0} must be a string with a maximum length of {1}.']",
              [this.$i18n.t("TaskManagement['DisplayName:Cron']"), '50']
            ),
            trigger: 'blur'
          }
        ]
      }
    }
  },
  created() {
    this.getList()
  },
  methods: {
    checkPermission, // 检查权限
    // 重置查询参数
    resetQueryForm() {
      this.queryForm = Object.assign({
        startTime: undefined,
        endTime: undefined,
        httpMethod: undefined,
        url: undefined,
        userName: undefined,
        tenantName: undefined,
        applicationName: undefined,
        hasException: undefined,
        httpStatusCode: undefined,
        minExecutionDuration: undefined,
        maxExecutionDuration: undefined
      }, baseListQuery)
    },
    datePickerChange(value) {
      if (!value) {
        // 日期选择器改变事件 ~ 解决日期选择器清空 值不清空的问题
        this.queryForm.startTime = undefined
        this.queryForm.endTime = undefined
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
    // 获取列表数据
    getList() {
      this.listLoading = true
      getBackgroundJobs(this.queryForm).then(response => {
        this.list = response.items
        this.total = response.totalCount

        this.listLoading = false
      })
    },
    handleFilter(firstPage = true) {
      if (firstPage) this.queryForm.page = 1
      this.getList()
    },
    sortChange(data) {
      const {
        prop,
        order
      } = data
      this.queryForm.sort = order ? `${prop} ${order}` : undefined
      this.handleFilter()
    },

    // 重置表单
    resetTemp() {
      this.temp = {
        id: undefined,
        isEnabled: true,
        args: {},
        description: '',
        jobType: undefined,
        cron: '',
        maxTryCount: 0,
        maxCount: 0,
        interval: 0,
        priority: 0,
        lockTimeOut: 0,
        name: '',
        group: '',
        type: '',
        nodeName: '',
        beginTime: undefined,
        endTime: undefined,
        source: 0
      }
    },

    // 点击创建按钮
    handleCreate() {
      this.resetTemp()
      this.dialogStatus = 'create'
      this.dialogFormVisible = true
      this.$nextTick(() => {
        this.$refs['dataForm'].clearValidate()
      })
    },

    // 创建数据
    createData() {
      this.$refs['dataForm'].validate(valid => {
        if (valid) {
          createBackgroundJob(this.temp).then(() => {
            this.list.unshift(this.temp)
            this.dialogFormVisible = false
            this.$notify({
              title: this.$i18n.t("TigerUi['Success']"),
              message: this.$i18n.t("TigerUi['SuccessMessage']"),
              type: 'success',
              duration: 2000
            })
          })
        }
      })
    },

    // 更新按钮点击
    handleUpdate(row) {
      this.temp = Object.assign({}, row) // copy obj
      this.dialogStatus = 'update'
      this.dialogFormVisible = true

      getBackgroundJob(row.id).then(response => {
        this.temp = response
      })

      this.$nextTick(() => {
        this.$refs['dataForm'].clearValidate()
      })
    },

    // 更新数据
    updateData() {
      this.$refs['dataForm'].validate(valid => {
        if (valid) {
          updateBackgroundJob(this.temp.id, this.temp).then(() => {
            const index = this.list.findIndex((v) => v.id === this.temp.id)
            this.list.splice(index, 1, this.temp)
            this.dialogFormVisible = false
            this.$notify({
              title: this.$i18n.t("TigerUi['Success']"),
              message: this.$i18n.t("TigerUi['SuccessMessage']"),
              type: 'success',
              duration: 2000
            })
          })
        }
      })
    },

    // 删除
    handleDelete(row, index) {
      this.$confirm(
        // 消息
        this.$i18n.t("AbpUi['ItemWillBeDeletedMessage']", [
          row.name
        ]),
        // title
        this.$i18n.t("AbpUi['ItemWillBeDeletedMessage']"), {
          confirmButtonText: this.$i18n.t("AbpUi['Yes']"), // 确认按钮
          cancelButtonText: this.$i18n.t("AbpUi['Cancel']"), // 取消按钮
          type: 'warning' // 弹框类型
        }
      ).then(async() => {
        // 回调函数
        deleteBackgroundJob(row.id).then(() => {
          const index = this.list.findIndex((v) => v.id === row.id)
          this.list.splice(index, 1)
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
      this.downloadLoading = true
      import('@/vendor/Export2Excel').then(excel => {
        const tHeader = ['browserInfo', 'clientId', 'clientIpAddress', 'clientName', 'correlationId', 'exceptions', 'executionDuration', 'executionTime', 'httpMethod', 'httpStatusCode', 'url', 'userId', 'userName']
        const filterVal = ['browserInfo', 'clientId', 'clientIpAddress', 'clientName', 'correlationId', 'exceptions', 'executionDuration', 'executionTime', 'httpMethod', 'httpStatusCode', 'url', 'userId', 'userName']

        // TODO: 修改为当前查询条件下所有页数的数据
        const list = this.list
        const data = this.formatJson(filterVal, list)
        excel.export_json_to_excel({
          header: tHeader,
          data,
          filename: this.filename,
          autoWidth: this.autoWidth,
          bookType: this.bookType
        })
        this.downloadLoading = false
      })
    },
    formatJson(filterVal, jsonData) {
      return jsonData.map(v => filterVal.map(j => {
        if (j === 'executionTime') {
          return parseTime(v[j])
        } else {
          return v[j]
        }
      }))
    }
  }
}
</script>
