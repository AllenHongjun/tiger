<template>
  <div class="app-container">
    <div class="filter-container">
      <el-input v-model="listQuery.filter" :placeholder="$t('AbpUi[\'PagerSearch\']')" style="width: 200px;" class="filter-item" @keyup.enter.native="handleFilter" />
      <el-button v-if="checkPermission('AbpTenantManagement.Tenants.Create')" class="filter-item" style="margin-left: 10px;" type="primary" icon="el-icon-edit" @click="handleCreate">
        {{ $t("AbpTenantManagement['NewTenant']") }}
      </el-button>
    </div>

    <el-table :key="tableKey" v-loading="listLoading" :data="list" border fit highlight-current-row :stripe="true" style="width: 100%;" @sort-change="sortChange">
      <el-table-column type="selection" width="55" />
      <el-table-column type="index" width="55" />
      <el-table-column :label="$t('AbpSaas[\'TenantName\']')" prop="name" sortable align="left">
        <template slot-scope="{ row }">
          <span>{{ row.name }}</span>
        </template>
      </el-table-column>
      <el-table-column :label="$t('AbpSaas[\'DisplayName:IsActive\']')" prop="isActive" align="isActive">
        <template slot-scope="{ row }">
          <el-tag :type="row.isActive?'primary':'danger'">{{ row.isActive ?"启用" : "禁用" }}</el-tag>
        </template>
      </el-table-column>
      <el-table-column :label="$t('AbpSaas[\'DisplayName:EditionName\']')" prop="editionName" align="left">
        <template slot-scope="{ row }">
          <span>{{ row.editionName }}</span>
        </template>
      </el-table-column>
      <el-table-column :label="$t('AbpSaas[\'DisplayName:DisableTime\']')" prop="disableTime" align="left">
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
      tableKey: 0,
      list: null,
      total: 0,
      listLoading: true,
      listQuery: baseListQuery
    }
  },
  created() {
    this.getList()
  },
  methods: {
    checkPermission,
    getList() {
      this.listLoading = true
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
