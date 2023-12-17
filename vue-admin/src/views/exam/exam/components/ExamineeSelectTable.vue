<template>
  <el-dialog title="指定考生" :visible.sync="dialogExamineeSelectVisible" append-to-body top="3vh" width="90%" class="middle-dialog">
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

      <el-table
        :key="tableKey"
        v-loading="listLoading"
        :data="list"
        :row-key="getRowKey"
        border
        fit
        highlight-current-row
        :stripe="true"
        style="width: 100%;"
        @selection-change="handleSelectionChange"
        @sort-change="sortChange"
      >
        <el-table-column type="selection" width="55" center :reserve-selection="true" />
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
    <div slot="footer" class="dialog-footer">
      <el-button @click="dialogExamineeSelectVisible = false">取 消</el-button>
      <el-button type="primary" @click="confirmSelect()">确认选择</el-button>
    </div>
  </el-dialog>

</template>

<script>
import baseListQuery, { checkPermission } from '@/utils/abp'

import {
  getOrganizationsAllTree
} from '@/api/system-manage/identity/organization-unit'
import {
  getExaminees,
  batchCreateExaminee
} from '@/api/exam/examinee'

import Pagination from '@/components/Pagination/index.vue' // secondary package based on el-pagination

export default {
  name: 'ExamineeSelectTable',
  components: {
    Pagination
  },
  props: {
    examId: {
      type: String,
      require: true,
      default: undefined
    }
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
      dialogExamineeSelectVisible: false,
      // 获取row的key值
      getRowKey(row) {
        return row.id
      },
      multipleSelectionIds: []

    }
  },
  created() {
    this.getOrgs()
    // this.getList()
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
      this.dialogExamineeSelectVisible = true
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
    handleSelectionChange(objArr) {
      console.log('objArr', objArr)
      this.multipleSelectionIds = objArr.map((item) => item.id)
      console.log('this.multipleSelectionIds', this.multipleSelectionIds)
    },
    confirmSelect() {
      var req = {
        examId: this.examId,
        userIds: this.multipleSelectionIds
      }
      console.log('req', req)
      batchCreateExaminee(req).then(() => {
        this.$emit('created', false)
        this.dialogExamineeSelectVisible = false
        this.$notify({
          title: this.$i18n.t("TigerUi['Success']"),
          message: this.$i18n.t("TigerUi['SuccessMessage']"),
          type: 'success',
          duration: 2000
        })
      })
    }
  }
}
</script>

<style lang="scss" scoped>
.line{
  text-align: center;
}
</style>

