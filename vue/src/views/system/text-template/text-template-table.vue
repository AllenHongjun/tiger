<template>
  <div class="app-container">
    <div class="filter-container">
      <el-input v-model="listQuery.filter" :placeholder="$t('AbpUi[\'PagerSearch\']')" style="width: 200px;" class="filter-item" @keyup.enter.native="handleFilter" />
      <el-button class="filter-item" type="primary" icon="el-icon-search" @click="handleFilter">{{ $t("AbpUi['Search']") }}</el-button>

    </div>

    <el-table :key="tableKey" v-loading="listLoading" :data="list" border fit highlight-current-row style="width: 100%;" @sort-change="sortChange">
      <el-table-column :label="$t('AbpTextTemplate[\'DisplayName:DisplayName\']')" prop="name" sortable align="left">
        <template slot-scope="{ row }">
          <span>{{ row.displayName }}</span>
        </template>
      </el-table-column>
      <el-table-column :label="$t('AbpTextTemplate[\'DisplayName:IsLayout\']')" prop="name" sortable align="left">
        <template slot-scope="{ row }">
          <el-tag :type="( row.isLayout ? 'success' : 'danger')" :class="[row.isLayout ? 'el-icon-check':'el-icon-close' ]" />
        </template>
      </el-table-column>
      <el-table-column :label="$t('AbpTextTemplate[\'DisplayName:IsInlineLocalized\']')" prop="name" sortable align="left">
        <template slot-scope="{ row }">
          <el-tag :type="( row.isInlineLocalized ? 'success' : 'danger')" :class="[row.isInlineLocalized ? 'el-icon-check':'el-icon-close' ]" />
        </template>
      </el-table-column>
      <el-table-column :label="$t('AbpTextTemplate[\'Layout\']')" prop="name" sortable align="left">
        <template slot-scope="{ row }">
          <span>{{ row.layout }}</span>
        </template>
      </el-table-column>
      <el-table-column :label="$t('AbpTextTemplate[\'DisplayName:DefaultCultureName\']')" prop="name" sortable align="left">
        <template slot-scope="{ row }">
          <span>{{ row.defaultCultureName }}</span>
        </template>
      </el-table-column>
      <el-table-column :label="$t('AbpUi[\'Actions\']')" align="left" width="180" class-name="small-padding fixed-width">
        <template slot-scope="{ row }">
          <el-button v-if="checkPermission('AbpTextTemplating.TextTemplates.Update')" type="primary" size="mini" @click="handleUpdate(row)">
            {{ $t("AbpTextTemplate['Permission:Edit']") }}
          </el-button>
        </template>
      </el-table-column>
    </el-table>

    <pagination v-show="total > 0" :total="total" :page.sync="listQuery.page" :limit.sync="listQuery.limit" @pagination="getList" />

    <text-template-model ref="textTemplateModel" @handleFilter="handleFilter" />
  </div>
</template>

<script>
import {
  getTextTemplates
} from '@/api/system-manage/text-template'
import Pagination from '@/components/Pagination' // secondary package based on el-pagination
import baseListQuery, {
  checkPermission
} from '@/utils/abp'
import TextTemplateModel from './components/text-template-model'

export default {
  name: 'TextTemplates',
  components: {
    Pagination,
    TextTemplateModel
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
      getTextTemplates(this.listQuery).then(response => {
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
    handleUpdate(row) {
      this.$refs['textTemplateModel'].handleUpdate(row)
    }

  }
}
</script>
