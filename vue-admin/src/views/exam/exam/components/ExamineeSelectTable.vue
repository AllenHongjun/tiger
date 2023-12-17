<template>
  <div class="table-container">
    <div class="filter-container">
      <el-row style="margin-bottom: 20px">
        <el-input v-model="listQuery.filter" :placeholder="$t('AbpUi[\'PagerSearch\']')" style="width: 200px;" clearable class="filter-item" @keyup.enter.native="handleFilter" />
        <el-cascader
          v-model="listQuery.organizationUnitId"
          :options="orgTreeData"
          :props="{ checkStrictly: true, value:'id',label:'displayName',children:'children',emitPath:false }"
          class="filter-item"
          placeholder="请选择组织"
          filterable
          clearable
        />
        <el-button type="primary" class="filter-item" icon="el-icon-search" @click="handleFilter">
          {{ $t('AbpUi.Search') }}
        </el-button>
      </el-row>
    </div>

    <el-table :key="tableKey" v-loading="listLoading" :data="list" border fit highlight-current-row :stripe="true" style="width: 100%;" @sort-change="sortChange">
      <el-table-column type="selection" width="55" center />
      <el-table-column type="index" width="80" />
      <el-table-column :label="$t('AbpIdentity[\'OrganizationUnits\']')" align="left" width="280" prop="organizationUnitName">
        <template slot-scope="{ row }">
          <el-tag v-if="row.organizationUnitName" type="info">{{ row.organizationUnitName }}</el-tag>
        </template>
      </el-table-column>
      <el-table-column :label="$t('AbpIdentity[\'UserName\']')" prop="userName" sortable align="left" width="180">
        <template slot-scope="{ row }">
          <span>{{ row.userName }}</span>
        </template>
      </el-table-column>
      <el-table-column label="姓名" prop="name" sortable align="left" width="120">
        <template slot-scope="{ row }">
          <el-tag v-if="row.surname || row.name " type="primary">{{ row.surname + row.name }}</el-tag>
        </template>
      </el-table-column>
      <el-table-column :label="$t('AbpIdentity[\'PhoneNumber\']')" prop="phoneNumber" sortable align="left">
        <template slot-scope="{ row }">
          <span>{{ row.phoneNumber }}</span>
        </template>
      </el-table-column>
      <el-table-column :label="$t('AbpIdentity[\'EmailAddress\']')" prop="email" sortable align="left">
        <template slot-scope="{ row }">
          <span>{{ row.email }}</span>
        </template>
      </el-table-column>
      <el-table-column :label="$t('AbpIdentity[\'CreationTime\']')" prop="name" sortable align="left" width="180">
        <template slot-scope="{ row }">
          <span>{{ row.creationTime | moment }}</span>
        </template>
      </el-table-column>

    </el-table>

    <pagination v-show="total > 0" :total="total" :page.sync="listQuery.page" :limit.sync="listQuery.limit" @pagination="getList" />

  </div>
</template>

<script>
import baseListQuery, { checkPermission } from '@/utils/abp'

import {
  getOrganizationsAllTree
} from '@/api/system-manage/identity/organization-unit'
import {
  getExaminees
} from '@/api/exam/examinee'

import Pagination from '@/components/Pagination/index.vue' // secondary package based on el-pagination

export default {
  name: 'ExamineeSelectTable',
  components: {
    Pagination
  },
  data() {
    return {
      orgTreeData: undefined,
      tableKey: 0,
      list: null,
      total: 0,
      listLoading: true,
      listQuery: Object.assign({
        inExamineeTable: false,
        organizationUnitId: undefined
      }, baseListQuery),

      dialogFormVisible: false

    }
  },
  created() {
    this.getOrgs()
    this.getList()
  },
  methods: {
    checkPermission, // 检查权限
    getOrgs() {
      getOrganizationsAllTree(baseListQuery).then(response => {
        this.orgTreeData = response.items
      })
    },
    // 获取列表数据
    getList() {
      this.listLoading = true
      getExaminees(this.listQuery).then(response => {
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
    handleSelectExaminee() {
      this.$emit('handleCreate')
    },
    handleSelectOrg(row) {
      this.dialogFormVisible = true
    }

  }
}
</script>

<style lang="scss" scoped>
.line{
  text-align: center;
}
</style>

