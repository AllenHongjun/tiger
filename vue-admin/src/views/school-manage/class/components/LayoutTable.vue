<template>
  <div class="app-container">
    <div class="filter-container">
      <el-row style="margin-bottom: 20px">
        <el-input v-model="listQuery.filter" :placeholder="$t('AbpUi[\'PagerSearch\']')" style="width: 200px;" clearable class="filter-item" @keyup.enter.native="handleFilter" />
        <el-button type="primary" class="filter-item" icon="el-icon-search" @click="handleFilter">
          {{ $t('AbpUi.Search') }}
        </el-button>
        <el-button v-if="checkPermission('Platform.Layout.Create')" class="filter-item" style="margin-left: 10px;" type="primary" icon="el-icon-plus" @click="handleCreate">
          {{ $t("AppPlatform['Permission:Create']") }}
        </el-button>
      </el-row>
    </div>

    <el-table :key="tableKey" v-loading="listLoading" :data="list" border fit highlight-current-row :stripe="true" style="width: 100%;" @sort-change="sortChange">
      <el-table-column type="selection" width="55" center />
      <el-table-column type="index" width="80" />
      <el-table-column :label="$t('AppPlatform[\'DisplayName:Name\']')" prop="name" sortable align="left" width="120">
        <template slot-scope="{ row }">
          <span>{{ row.name }}</span>
        </template>
      </el-table-column>
      <el-table-column :label="$t('AppPlatform[\'DisplayName:DisplayName\']')" align="left" width="120">
        <template slot-scope="{ row }">
          <span>{{ row.displayName }}</span>
        </template>
      </el-table-column>
      <el-table-column :label="$t('AppPlatform[\'DisplayName:Description\']')" prop="description" sortable align="left">
        <template slot-scope="{ row }">
          <span>{{ row.description }}</span>
        </template>
      </el-table-column>
      <el-table-column :label="$t('AppPlatform[\'DisplayName:Path\']')" prop="path" sortable align="left">
        <template slot-scope="{ row }">
          <span>{{ row.path }}</span>
        </template>
      </el-table-column>
      <el-table-column :label="$t('AppPlatform[\'DisplayName:Redirect\']')" prop="redirect" sortable align="left">
        <template slot-scope="{ row }">
          <span>{{ row.redirect }}</span>
        </template>
      </el-table-column>

      <el-table-column :label="$t('AbpUi[\'Actions\']')" align="left" width="280">
        <template slot-scope="{ row, $index }">
          <el-button v-if="checkPermission('Platform.Layout.Update')" type="primary" class="el-icon-edit" :title="$t('AbpUi[\'Edit\']')" @click="handleUpdate(row)" />
          <el-button v-if="checkPermission('Platform.Layout.Delete')" type="danger" class="el-icon-delete" :title="$t('AbpUi[\'Delete\']')" @click="handleDelete(row, $index)" />
        </template>
      </el-table-column>
    </el-table>

    <pagination v-show="total > 0" :total="total" :page.sync="listQuery.page" :limit.sync="listQuery.limit" @pagination="getList" />
  </div>
</template>

<script>
import baseListQuery, { checkPermission } from '@/utils/abp'
import {
  getLayouts,
  deleteLayout
} from '@/api/system-manage/platform/layout'
import Pagination from '@/components/Pagination' // secondary package based on el-pagination

export default {
  name: 'LayoutTable',
  components: {
    Pagination
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
    checkPermission, // 检查权限

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
    }
  }
}
</script>

<style scoped>
.line{
  text-align: center;
}
</style>

