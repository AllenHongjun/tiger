<template>
  <div class="app-container">
    <div class="filter-container">
      <el-input v-model="listQuery.filter" :placeholder="$t('AbpUi[\'PagerSearch\']')" style="width: 200px;" class="filter-item" @keyup.enter.native="handleFilter" />
      <el-button class="filter-item" type="primary" icon="el-icon-search" @click="handleFilter">{{ $t('AbpUi.Search') }}</el-button>
      <el-button v-if="checkPermission('Platform.DataDictionary.Create')" class="filter-item" style="margin-left: 10px;" type="primary" icon="el-icon-plus" @click="handleCreate">
        {{ $t("AppPlatform['Data:AddNew']") }}
      </el-button>
    </div>

    <el-table :key="tableKey" v-loading="listLoading" :data="list" border fit highlight-current-row :stripe="true" style="width: 100%;" @sort-change="sortChange">

      <!-- 使用table自带的序号 -->
      <el-table-column type="index" width="80" />
      <el-table-column :label="$t('AppPlatform[\'DisplayName:Name\']')" prop="name" sortable align="left">
        <template slot-scope="{ row }">
          <span>{{ row.name }}</span>
        </template>
      </el-table-column>
      <el-table-column :label="$t('AppPlatform[\'DisplayName:DisplayName\']')" prop="displayName" sortable align="left">
        <template slot-scope="{ row }">
          <span>{{ row.displayName }}</span>
        </template>
      </el-table-column>
      <el-table-column :label="$t('AppPlatform[\'DisplayName:Code\']')" prop="code" sortable align="left">
        <template slot-scope="{ row }">
          <span>{{ row.code }}</span>
        </template>
      </el-table-column>
      <el-table-column :label="$t('AppPlatform[\'DisplayName:Description\']')" prop="description" sortable align="left">
        <template slot-scope="{ row }">
          <span>{{ row.description }}</span>
        </template>
      </el-table-column>
      <el-table-column :label="$t('AbpUi[\'DisplayName:CreationTime\']')" prop="creationTime" sortable align="left">
        <template slot-scope="{ row }">
          <span>{{ row.creationTime | moment }}</span>
        </template>
      </el-table-column>

      <el-table-column :label="$t('AbpUi[\'Actions\']')" align="left" width="240" class-name="small-padding fixed-width">
        <template slot-scope="{ row, $index }">
          <el-button v-if="checkPermission('Platform.DataDictionary.Update')" type="primary" size="mini" @click="handleUpdate(row)">
            {{ $t("AppPlatform['Data:Edit']") }}
          </el-button>
          <el-button v-if="checkPermission('Platform.DataDictionary.Update')" type="primary" size="mini" @click="handleDataItem(row)">
            字典项目
          </el-button>
          <el-button v-if="checkPermission('Platform.DataDictionary.Delete')" size="mini" type="danger" @click="handleDelete(row, $index)">
            {{ $t("AppPlatform['Data:Delete']") }}
          </el-button>
        </template>
      </el-table-column>
    </el-table>

    <pagination v-show="total > 0" :total="total" :page.sync="listQuery.page" :limit.sync="listQuery.limit" @pagination="getList" />

    <data-dialog ref="dataDialog" @handleFilter="handleFilter" />
    <data-item-dialog ref="dataItemDialog" @handleFilter="handleFilter" />
  </div>
</template>

<script>

import {
  getDataList,
  deleteData
} from '@/api/system-manage/platform/data'
import Pagination from '@/components/Pagination' // secondary package based on el-pagination
import baseListQuery, {
  checkPermission
} from '@/utils/abp'
import DataDialog from './components/data-dialog'
import DataItemDialog from './components/data-item-dialog'

export default {
  name: 'DataDictionary',
  components: {
    Pagination,
    DataDialog,
    DataItemDialog
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
      getDataList(this.listQuery).then(response => {
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
      this.$refs['dataDialog'].handleCreate()
    },
    handleUpdate(row) {
      this.$refs['dataDialog'].handleUpdate(row)
    },
    handleDataItem(row) {
      this.$refs['dataItemDialog'].getList(row)
    },
    handleDelete(row, index) {
      this.$confirm(
        this.$i18n.t(
          "AppPlatform['TenantDeletionConfirmationMessage']",
          [row.name]
        ),
        this.$i18n.t("AppPlatform['AreYouSure']"), {
          confirmButtonText: this.$i18n.t("AbpUi['Yes']"),
          cancelButtonText: this.$i18n.t("AbpUi['Cancel']"),
          type: 'warning'
        }
      ).then(async() => {
        deleteData(row.id).then(() => {
          this.handleFilter()
          this.$notify({
            title: this.$i18n.t("TigerUi['Success']"),
            message: this.$i18n.t("TigerUi['SuccessMessage']"),
            type: 'success',
            duration: 2000
          })
        })
      })
    }
  }
}
</script>
