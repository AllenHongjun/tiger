<template>
  <div class="app-container">
    <el-dialog :title="$t('TaskManagement[\'BackgroundJobDetail\']')" :visible.sync="dialogVisible" width="75%" top="7vh">
      <el-card class="box-card">
        <div class="text item">
          <el-descriptions class="margin-top" :title="$t('TaskManagement[\'BasicInfo\']')" :column="5" :size="size" border>
            <el-descriptions-item>
              <template slot="label">
                <i class="el-icon-user" />
                {{ $t("TaskManagement['DisplayName:Group']") }}
              </template>
              {{ jobInfo.group }}
            </el-descriptions-item>
            <el-descriptions-item>
              <template slot="label">
                <i class="el-icon-mobile-phone" />
                {{ $t("TaskManagement['DisplayName:Name']") }}
              </template>
              {{ jobInfo.name }}
            </el-descriptions-item>
            <el-descriptions-item>
              <template slot="label">
                <i class="el-icon-location-outline" />
                {{ $t("TaskManagement['DisplayName:IsEnabled']") }}
              </template>
              {{ jobInfo.IsEnabled ? '启用': '禁用' }}
            </el-descriptions-item>

            <!-- <el-descriptions-item>
              <template slot="label">
                <i class="el-icon-office-building" />
                {{ $t("TaskManagement['DisplayName:Type']") }}
              </template>
              {{ jobInfo.type }}
            </el-descriptions-item> -->
            <el-descriptions-item>
              <template slot="label">
                <i class="el-icon-tickets" />
                {{ $t("TaskManagement['DisplayName:BeginTime']") }}
              </template>
              <el-tag size="small">{{ jobInfo.beginTime | moment }}</el-tag>
            </el-descriptions-item>
            <el-descriptions-item>
              <template slot="label">
                <i class="el-icon-tickets" />
                {{ $t("TaskManagement['DisplayName:EndTime']") }}
              </template>
              <el-tag size="small">{{ jobInfo.endTime | moment }}</el-tag>
            </el-descriptions-item>
            <el-descriptions-item>
              <template slot="label">
                <i class="el-icon-tickets" />
                {{ $t("TaskManagement['DisplayName:Cron']") }}
              </template>
              <el-tag size="small">{{ jobInfo.cron }}</el-tag>
            </el-descriptions-item>
            <el-descriptions-item>
              <template slot="label">
                <i class="el-icon-tickets" />
                {{ $t("TaskManagement['DisplayName:MaxCount']") }}
              </template>
              <el-tag size="small">{{ jobInfo.maxCount }}</el-tag>
            </el-descriptions-item>
            <el-descriptions-item>
              <template slot="label">
                <i class="el-icon-tickets" />
                {{ $t("TaskManagement['DisplayName:LockTimeOut']") }}
              </template>
              <el-tag size="small">{{ jobInfo.lockTimeOut }}</el-tag>
            </el-descriptions-item>
          </el-descriptions>
        </div>
      </el-card>
      <el-card class="box-card">
        <div slot="header" class="clearfix">
          <b>{{ $t("TaskManagement['BackgroundJobLogs']") }}</b>
        </div>
        <div class="text item">
          <el-table :key="tableKey" v-loading="listLoading" max-height="380" :data="list" border fit highlight-current-row :stripe="true" style="width: 100%;" @sort-change="sortChange">
            <el-table-column type="index" width="80" />
            <el-table-column :label="$t('TaskManagement[\'DisplayName:Name\']')" prop="jobName" align="center">
              <template slot-scope="{ row }">
                <span>{{ row.jobName }}</span>
              </template>
            </el-table-column>
            <el-table-column :label="$t('TaskManagement[\'DisplayName:Group\']')" prop="jobGroup" sortable align="center" width="130">
              <template slot-scope="{ row }">
                <span>{{ row.jobGroup }}</span>
              </template>
            </el-table-column>
            <el-table-column :label="$t('TaskManagement[\'DisplayName:Type\']')" prop="jobType" sortable align="center" width="130">
              <template slot-scope="{ row }">
                <span>{{ row.jobType }}</span>
              </template>
            </el-table-column>
            <el-table-column label="消息" prop="message" align="center">
              <template slot-scope="{ row }">
                <span>{{ row.message }}</span>
              </template>
            </el-table-column>
            <el-table-column label="运行时间" prop="runTime" sortable align="center" width="140">
              <template slot-scope="{ row }">
                <span>{{ row.runTime | moment }}</span>
              </template>
            </el-table-column>
            <!-- <el-table-column :label="$t('TaskManagement[\'DisplayName:Group\']')" prop="exception" sortable align="center">
              <template slot-scope="{ row }">
                <span>{{ row.exception }}</span>
              </template>
            </el-table-column> -->

            <el-table-column :label="$t('AbpUi[\'Actions\']')" align="center" width="200" class-name="small-padding fixed-width">
              <template slot-scope="{ row, $index }">

                <el-button v-if="checkPermission('Platform.Layout.Delete')" type="danger" @click="handleDelete(row, $index)">
                  {{ $t("AbpUi['Delete']") }}
                </el-button>
              </template>
            </el-table-column>
          </el-table>

          <pagination v-show="total > 0" :total="total" :page.sync="listQuery.page" :limit.sync="listQuery.limit" @pagination="getJobLogList" />
        </div>
      </el-card>
      <span slot="footer" class="dialog-footer">
        <el-button @click="dialogVisible = false">取 消</el-button>
        <el-button type="primary" @click="dialogVisible = false">确 定</el-button>
      </span>
    </el-dialog>

  </div>
</template>

<script>

import { getBackgroundJob } from '@/api/system-manage/task/background-job'
import {
  getBackgroundJobLogs,
  getBackgroundJobLog,
  deleteBackgroundJobLog
} from '@/api/system-manage/task/background-job-log'
import Pagination from '@/components/Pagination' // secondary package based on el-pagination
import baseListQuery, {
  checkPermission
} from '@/utils/abp'

export default {
  name: 'JobDetail',
  components: {
    Pagination
  },
  props: {
    name: {
      type: String,
      required: true
    }
  },
  data() {
    return {
      blank: {

      },
      size: '',
      dialogVisible: false,
      jobInfo: {},
      tableKey: 0,
      list: null,
      total: 0,
      listLoading: true,
      listQuery: Object.assign({
        jobId: undefined,
        name: '',
        group: '',
        type: '',
        beginRunTime: undefined,
        endRunTime: undefined
      }, baseListQuery),
      jobLogs: []
    }
  },
  methods: {
    checkPermission, // 检查权限
    handleClose(done) {
      this.$confirm('确认关闭？')
        .then(_ => {
          done()
        })
        .catch(_ => {})
    },
    handleDetail(jobId) {
      this.dialogVisible = true
      getBackgroundJob(String(jobId)).then((res) => {
        this.jobInfo = res
        this.jobLogs = []
        this.listQuery.jobId = jobId
        this.handleFilter(1)
      })
    },
    // 获取任务日志列表数据
    getJobLogList() {
      this.listLoading = true
      getBackgroundJobLogs(this.listQuery).then(response => {
        this.list = response.items
        this.total = response.totalCount
        this.listLoading = false
      })
    },
    handleFilter(firstPage = true) {
      if (firstPage) this.listQuery.page = 1
      this.getJobLogList()
    },
    sortChange(data) {
      const {
        prop,
        order
      } = data
      this.queryForm.sort = order ? `${prop} ${order}` : undefined
      this.handleFilter()
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
.pagination-container{
  margin-top: 5px;
  padding: 10px 16px;
}
</style>

