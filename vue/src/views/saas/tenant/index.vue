<template>
  <div class="app-container">
    <div class="filter-container">
      <el-row>
        <el-input v-model="listQuery.filter" :placeholder="$t('AbpUi[\'PagerSearch\']')" style="width: 200px;" class="filter-item" @keyup.enter.native="handleFilter" />
        <el-select v-model="listQuery.editionId" class="filter-item" placeholder="请选择版本" clearable="">
          <el-option
            v-for="item in editionOptions"
            :key="item.id"
            :label="item.displayName"
            :value="item.id"
          />
        </el-select>
        <el-date-picker
          v-model="disableTime"
          class="filter-item"
          type="daterange"
          range-separator="至"
          start-placeholder="截止日期开始"
          end-placeholder="截止日期结束"
          :clearable="true"
          @change="pickerChangeFn"
        />
        <el-select v-model="listQuery.isActive" class="filter-item" placeholder="活跃状态" clearable>
          <el-option label="启用" :value="true" />
          <el-option label="禁用" :value="false" />
        </el-select>
        <el-button type="primary" class="filter-item" icon="el-icon-search" @click="handleFilter">
          {{ $t('AbpUi.Search') }}
        </el-button>
        <el-button v-if="checkPermission('AbpTenantManagement.Tenants.Create')" class="filter-item" style="margin-left: 10px;" type="primary" icon="el-icon-edit" @click="handleCreate">
          {{ $t("AbpTenantManagement['NewTenant']") }}
        </el-button>
      </el-row>

    </div>

    <el-table :key="tableKey" v-loading="listLoading" :data="list" border fit highlight-current-row :stripe="true" style="width: 100%;" @sort-change="sortChange">
      <el-table-column type="selection" width="55" />
      <el-table-column type="index" width="55" />
      <el-table-column :label="$t('AbpSaas[\'TenantName\']')" prop="name" sortable align="left">
        <template slot-scope="{ row }">
          <span>{{ row.name }}</span>
        </template>
      </el-table-column>
      <el-table-column :label="$t('AbpSaas[\'DisplayName:IsActive\']')" prop="isActive" align="isActive" width="100">
        <template slot-scope="{ row }">
          <el-tag :type="row.isActive?'primary':'danger'">{{ row.isActive ?"启用" : "禁用" }}</el-tag>
        </template>
      </el-table-column>
      <el-table-column :label="$t('AbpSaas[\'DisplayName:EditionName\']')" prop="editionName" align="left" width="120">
        <template slot-scope="{ row }">
          <span>{{ row.editionName }}</span>
        </template>
      </el-table-column>
      <el-table-column :label="$t('AbpSaas[\'DisplayName:DisableTime\']')" prop="disableTime" align="left" width="180">
        <template slot-scope="{ row }">
          <span>{{ row.disableTime | moment }}</span>
        </template>
      </el-table-column>
      <el-table-column :label="$t('AbpTenantManagement[\'Actions\']')" align="left" width="500" class-name="small-padding fixed-width">
        <template slot-scope="{ row, $index }">
          <el-button v-if="checkPermission('AbpTenantManagement.Tenants.Update')" type="primary" size="mini" @click="handleUpdate(row)">
            {{ $t("AbpTenantManagement['Edit']") }}
          </el-button>
          <el-button
            v-if="checkPermission( 'AbpTenantManagement.Tenants.ManageConnectionStrings')"
            type="primary"
            size="mini"
            @click="handleUpdateConnectionString(row)"
          >
            {{
              $t("AbpTenantManagement['Permission:ManageConnectionStrings']")
            }}
          </el-button>
          <el-button v-if="checkPermission('AbpTenantManagement.Tenants.ManageFeatures')" type="primary" size="mini" @click="handleUpdateFeature(row)">
            {{ $t("AbpTenantManagement['Permission:ManageFeatures']") }}
          </el-button>
          <el-button v-if="checkPermission('AbpTenantManagement.Tenants.Delete')" size="mini" type="danger" @click="handleDelete(row, $index)">
            {{ $t("AbpTenantManagement['Delete']") }}
          </el-button>
        </template>
      </el-table-column>
    </el-table>

    <pagination v-show="total > 0" :total="total" :page.sync="listQuery.page" :limit.sync="listQuery.limit" @pagination="getList" />

    <tenant-dialog ref="tenantDialog" @handleFilter="handleFilter" />
    <connectionstring-dialog ref="connectionstringDialog" />
    <feature-dialog ref="featureDialog" provider-name="T" />
  </div>
</template>

<script>
import {
  getTenants,
  deleteTenant
} from '@/api/sass/tenant'

import {
  getEditions
} from '@/api/sass/edition'

import Pagination from '@/components/Pagination' // secondary package based on el-pagination
import baseListQuery, {
  checkPermission
} from '@/utils/abp'
import TenantDialog from './components/tenant-dialog'
import ConnectionstringDialog from './components/connectionstring-dialog'
import FeatureDialog from './components/feature-dialog'

export default {
  name: 'Tenants',
  components: {
    Pagination,
    TenantDialog,
    ConnectionstringDialog,
    FeatureDialog
  },
  data() {
    return {
      editionOptions: [], // 版本选项
      disableTime: undefined, // 截止日期范围过滤条件
      tableKey: 0,
      list: null,
      total: 0,
      listLoading: true,
      listQuery: Object.assign({
        editionId: undefined,
        disableBeginTime: undefined,
        disableEndTime: undefined,
        isActive: undefined
      }, baseListQuery)
    }
  },
  created() {
    this.fetchEditionOptions()
    this.getList()
  },
  methods: {
    checkPermission,
    fetchEditionOptions() {
      var input = {
        page: 1,
        limit: 999
      }
      getEditions(input).then(response => {
        this.editionOptions = response.items
      })
    },
    // 日期选择器改变事件 ~ 解决日期选择器清空 值不清空的问题
    pickerChangeFn(value) {
      if (value === null) {
        this.listQuery.disableBeginTime = value
        this.listQuery.disableEndTime = value
      }
    },
    getList() {
      this.listLoading = true
      if (this.disableTime) {
        this.listQuery.disableBeginTime = this.disableTime[0]
        this.listQuery.disableEndTime = this.disableTime[1]
      }
      getTenants(this.listQuery).then(response => {
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
      this.$refs['tenantDialog'].handleCreate()
    },
    handleUpdate(row) {
      this.$refs['tenantDialog'].handleUpdate(row)
    },
    handleDelete(row, index) {
      this.$confirm(
        this.$i18n.t(
          "AbpTenantManagement['TenantDeletionConfirmationMessage']",
          [row.name]
        ),
        this.$i18n.t("AbpTenantManagement['AreYouSure']"), {
          confirmButtonText: this.$i18n.t("AbpTenantManagement['Yes']"),
          cancelButtonText: this.$i18n.t("AbpTenantManagement['Cancel']"),
          type: 'warning'
        }
      ).then(async() => {
        deleteTenant(row.id).then(() => {
          this.handleFilter()
          this.$notify({
            title: this.$i18n.t("TigerUi['Success']"),
            message: this.$i18n.t("TigerUi['SuccessMessage']"),
            type: 'success',
            duration: 2000
          })
        })
      })
    },
    handleUpdateConnectionString(row) {
      this.$refs['connectionstringDialog'].handleUpdate(row)
    },
    handleUpdateFeature(row) {
      this.$refs['featureDialog'].handleUpdate(row)
    }
  }
}
</script>
