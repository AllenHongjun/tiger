<template>
  <div class="app-container">
    <div class="filter-container">
      <el-row style="margin-bottom: 20px">
        <el-input v-model="listQuery.filter" :placeholder="$t('AbpUi[\'PagerSearch\']')" style="width: 200px;" clearable class="filter-item" @keyup.enter.native="handleFilter" />
        <el-date-picker
          v-model="queryDateTime"
          class="filter-item"
          value-format="yyyy-MM-dd HH:mm:ss"
          type="datetimerange"
          align="right"
          unlink-panels
          :picker-options="pickerOptions"
          :range-separator="$t('AbpAuditLogging[\'RangeSeparator\']')"
          :start-placeholder="$t('AbpAuditLogging[\'StartPlaceholder\']')"
          :end-placeholder="$t('AbpAuditLogging[\'EndPlaceholder\']')"
          :default-time="['00:00:00', '23:59:59']"
          @change="datePickerChange"
        />
        <el-select v-model="listQuery.changeType" :placeholder="$t('AbpAuditLogging[\'ChangeType\']')" style="width:200px" :clearable="true" class="filter-item" @clear="listQuery.changeType=undefined">
          <el-option label="Create" :value="0" />
          <el-option label="Update" :value="1" />
          <el-option label="Delete" :value="2" />
        </el-select>
        <el-input v-model="listQuery.entityTypeFullName" :placeholder="$t('AbpAuditLogging[\'EntityTypeFullName\']')" style="width: 200px;" clearable class="filter-item" @keyup.enter.native="handleFilter" />
        <el-button type="primary" class="filter-item" icon="el-icon-search" @click="handleFilter">
          {{ $t('AbpUi.Search') }}
        </el-button>
      </el-row>
    </div>

    <el-table :key="tableKey" v-loading="listLoading" :data="list" border fit highlight-current-row :stripe="true" style="width: 100%;" @sort-change="sortChange">
      <el-table-column type="selection" width="55" center />
      <el-table-column type="index" width="80" />
      <el-table-column :label="$t('AbpAuditLogging[\'ChangeTime\']')" align="left" width="160">
        <template slot-scope="{ row }">
          <span>{{ row.changeTime | moment }}</span>
        </template>
      </el-table-column>
      <el-table-column :label="$t('AbpAuditLogging[\'ChangeType\']')" align="left" width="160">
        <template slot-scope="{ row }">
          <el-tag :type="ChangeTypeMap[row.changeType]">{{ ChangeType[row.changeType] }}</el-tag>
        </template>
      </el-table-column>
      <el-table-column :label="$t('AbpAuditLogging[\'TenantId\']')" align="left">
        <template slot-scope="{ row }">
          <span>{{ row.tenantId }}</span>
        </template>
      </el-table-column>

      <el-table-column :label="$t('AbpAuditLogging[\'EntityTypeFullName\']')" align="left">
        <template slot-scope="{ row }">
          <span>{{ row.entityTypeFullName }}</span>
        </template>
      </el-table-column>

      <el-table-column :label="$t('AbpUi[\'Actions\']')" align="left" width="200" class-name="small-padding fixed-width">
        <template slot-scope="{ row, $index }">
          <el-button v-if="checkPermission('Platform.Layout.Update')" type="primary" @click="handleDetail(row)">
            {{ $t("AbpAuditLogging['Detail']") }}
          </el-button>
        </template>
      </el-table-column>
    </el-table>

    <pagination v-show="total > 0" :total="total" :page.sync="listQuery.page" :limit.sync="listQuery.limit" @pagination="getList" />

    <change-detail ref="ChangeDetail" />
  </div>
</template>

<script>
import {
  getAuditLogAverageExecutionDurationPerDay,
  getAuditLogEntityChanges,
  getAuditLogEntityChange,
  getAuditLogEntityChangeListWithUserName,
  getAuditLogEntityChangeWithUserName
} from '@/api/system-manage/auditing/auditlog'
import Pagination from '@/components/Pagination' // secondary package based on el-pagination
import baseListQuery, {
  checkPermission
} from '@/utils/abp'
import {
  pickerRangeWithHotKey
} from '@/utils/picker'

import ChangeDetail from './components/ChangeDetail.vue'

const ChangeType = {
  0: 'Create',
  1: 'Update',
  2: 'Delete'
}

const ChangeTypeMap = {
  [ChangeType.Create]: 'warning',
  [ChangeType.Update]: 'success',
  [ChangeType.Delete]: 'danger'
}

export default {
  name: 'Layouts',
  components: {
    Pagination,
    ChangeDetail
  },
  filters: {
    changeTypeFilter(changeType) {
      let type = 'success'
      switch (changeType.toUpperCase()) {
        case 0:
          type = ''
          break
        case 1:
          type = 'warning'
          break
        case 2:
          type = 'success'
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
      queryDateTime: undefined,
      ChangeType,
      ChangeTypeMap,
      pickerOptions: pickerRangeWithHotKey,
      listQuery: Object.assign({
        auditLogId: undefined,
        startTime: undefined,
        endTime: undefined,
        changeType: undefined,
        entityId: '',
        entityTypeFullName: '',
        includeDetails: ''
      }, baseListQuery),

      dialogFormVisible: false,
      dialogStatus: ''

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
        this.listQuery.startTime = undefined
        this.listQuery.endTime = undefined
      }
    },
    // 获取列表数据
    getList() {
      this.listLoading = true
      if (this.queryDateTime) {
        this.listQuery.startTime = this.queryDateTime[0]
        this.listQuery.endTime = this.queryDateTime[1]
      }
      getAuditLogEntityChanges(this.listQuery).then(response => {
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
    // 查看详情
    handleDetail(row) {
      this.$refs['ChangeDetail'].createLogInfo(row)
    }

  }
}
</script>
