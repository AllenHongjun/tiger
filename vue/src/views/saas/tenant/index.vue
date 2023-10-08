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
          value-format="yyyy-MM-dd HH:mm:ss"
          class="filter-item"
          type="daterange"
          range-separator="至"
          start-placeholder="截止日期开始"
          end-placeholder="截止日期结束"
          :clearable="true"
          :default-time="['00:00:00', '23:59:59']"
          @change="pickerChangeFn"
        />
        <el-select v-model="listQuery.isActive" class="filter-item" placeholder="活跃状态" clearable>
          <el-option label="启用" :value="true" />
          <el-option label="禁用" :value="false" />
        </el-select>
        <el-button type="primary" class="filter-item" icon="el-icon-search" @click="handleFilter">
          {{ $t('AbpUi.Search') }}
        </el-button>
        <el-button v-if="checkPermission('AbpSaasPermissions.Tenants.Create')" class="filter-item" style="margin-left: 10px;" type="primary" icon="el-icon-edit" @click="handleCreate">
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
      <el-table-column :label="$t('AbpUi[\'Actions\']')" align="left" width="680" class-name="small-padding fixed-width">
        <template slot-scope="{ row, $index }">
          <el-button v-if="checkPermission('AbpSaasPermissions.Tenants.Update')" type="primary" @click="handleUpdate(row)">
            {{ $t("AbpSaas['Permission:Edit']") }}
          </el-button>
          <el-button
            v-if="checkPermission( 'AbpSaasPermissions.Tenants.ManageConnectionStrings')"
            type="primary"
            plain
            @click="handleUpdateConnectionString(row)"
          >
            {{
              $t("AbpSaas['Permission:ManageConnectionStrings']")
            }}
          </el-button>
          <el-button v-if="checkPermission('AbpSaasPermissions.Tenants.ManageFeatures')" type="primary" plain @click="handleUpdateFeature(row)">
            {{ $t("AbpSaas['Permission:ManageFeatures']") }}
          </el-button>
          <el-button v-if="checkPermission('AbpSaasPermissions.Tenants.ChangeUserPassword')" type="primary" plain @click="handelChangePassword(row)">
            {{ $t('AbpSaas[\'Permission:ChangeUserPassword\']') }}
          </el-button>
          <el-button v-if="checkPermission('AbpSaasPermissions.Tenants.Delete')" type="danger" @click="handleDelete(row, $index)">
            {{ $t("AbpSaas['Permission:Delete']") }}
          </el-button>
        </template>
      </el-table-column>
    </el-table>

    <pagination v-show="total > 0" :total="total" :page.sync="listQuery.page" :limit.sync="listQuery.limit" @pagination="getList" />

    <tenant-dialog ref="tenantDialog" @handleFilter="handleFilter" />
    <connectionstring-dialog ref="connectionstringDialog" />
    <feature ref="featureDialog" provider-name="T" />
    <!-- 重置密码对话框 -->
    <el-dialog :title="$t('AbpSaas[\'Permission:ChangeUserPassword\']')" :visible.sync="dialogChangePasswordFormVisible" width="30%">
      <el-form ref="changePasswordForm" :model="changePasswordForm" label-width="120px" label-position="right">
        <el-form-item :label="$t('AbpSaas[\'DisplayName:UserName\']')" :inline="true">
          <el-input v-model="changePasswordForm.userName" prop="userName" type="text" auto-complete="off" style="width:90%" />
        </el-form-item>
        <el-form-item :label="$t('AbpIdentity[\'Password\']')" :inline="true">
          <el-input v-model="changePasswordForm.password" prop="password" type="text" auto-complete="off" show-password style="width:90%" />
          <el-button type="primary" icon="el-icon-refresh" @click="generateRandomPassword( 8 )" />
        </el-form-item>
      </el-form>
      <div style="text-align: right">
        <el-button type="danger" @click="dialogChangePasswordFormVisible = false">{{ $t("AbpUi['Cancel']") }}</el-button>
        <el-button type="primary" @click="changePassword()">{{ $t("AbpUi['Save']") }}</el-button>
      </div>
    </el-dialog>
  </div>
</template>

<script>
import {
  getTenants,
  deleteTenant,
  changePassword
} from '@/api/sass/tenant'

import {
  getEditions
} from '@/api/sass/edition'

import Pagination from '@/components/Pagination/index.vue' // secondary package based on el-pagination
import baseListQuery, { checkPermission } from '@/utils/abp'
import { generateRandomPassword } from '@/utils/random'
import TenantDialog from './components/TenantDialog.vue'
import ConnectionstringDialog from './components/ConnectionstringDialog.vue'
import Feature from '../components/Feature.vue'

export default {
  name: 'Tenants',
  components: {
    Pagination,
    TenantDialog,
    ConnectionstringDialog,
    Feature
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
      }, baseListQuery),
      changePasswordForm: {
        tenantId: undefined,
        tenantName: '',
        userName: 'admin',
        password: '' // 重置后的密码
      },
      dialogChangePasswordFormVisible: false
    }
  },
  created() {
    this.fetchEditionOptions()
    this.getList()
  },
  methods: {
    checkPermission,
    generateRandomPassword,
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
        // undefined 这个参数就不会带上去
        this.listQuery.disableBeginTime = undefined
        this.listQuery.disableEndTime = undefined
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
    },
    handelChangePassword(row) {
      this.changePasswordForm.tenantId = row.id
      this.changePasswordForm.tenantName = row.name
      this.changePasswordForm.password = ''
      this.dialogChangePasswordFormVisible = true
    },

    changePassword(row) {
      this.$refs['changePasswordForm'].validate((valid) => {
        if (valid) {
          changePassword(this.changePasswordForm).then(() => {
            this.dialogChangePasswordFormVisible = false
            this.$notify({
              title: this.$i18n.t("TigerUi['Success']"),
              message: this.$i18n.t("TigerUi['SuccessMessage']"),
              type: 'success',
              duration: 2000
            })
          })
        }
      })
    }
  }
}
</script>
