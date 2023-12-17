<template>
  <div class="table-container">
    <div class="filter-container">
      <el-row style="margin-bottom: 20px">
        <el-input v-model="listQuery.filter" :placeholder="$t('AbpUi[\'PagerSearch\']')" style="width: 200px;" clearable class="filter-item" @keyup.enter.native="handleFilter" />
        <el-button type="primary" class="filter-item" icon="el-icon-search" @click="handleFilter">
          {{ $t('AbpUi.Search') }}
        </el-button>
        <el-button v-if="checkPermission('Platform.Layout.Create')" class="filter-item" style="margin-left: 10px;" type="primary" icon="el-icon-plus" @click="handleSelectExaminee">
          指定考生账号
        </el-button>
        <el-button v-if="checkPermission('Platform.Layout.Create')" class="filter-item" style="margin-left: 10px;" type="primary" icon="el-icon-plus" @click="handleSelectOrg">
          指定组织
        </el-button>
        <el-button type="danger" icon="el-icon-delete" class="filter-item" @click="handleBatchDelete()">批量删除</el-button>
      </el-row>
    </div>

    <el-table ref="multipleTable" :key="tableKey" v-loading="listLoading" :data="list" border fit highlight-current-row :stripe="true" style="width: 100%;" @sort-change="sortChange">
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

      <el-table-column :label="$t('AbpUi[\'Actions\']')" align="left" width="280">
        <template slot-scope="{ row, $index }">
          <el-button v-if="checkPermission('Platform.Layout.Delete')" type="danger" class="el-icon-delete" :title="$t('AbpUi[\'Delete\']')" @click="handleDelete(row, $index)" />
        </template>
      </el-table-column>
    </el-table>

    <pagination v-show="total > 0" :total="total" :page.sync="listQuery.page" :limit.sync="listQuery.limit" @pagination="getList" />

    <examinee-select-table ref="examineeSelectTable" :exam-id="examId" @created="handleFilter" />

    <el-dialog title="指定组织" :visible.sync="dialogFormVisible" append-to-body top="5vh">
      <el-form :model="form">
        <org-tree />
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button @click="dialogFormVisible = false">取 消</el-button>
        <el-button type="primary" @click="dialogFormVisible = false">确 定</el-button>
      </div>
    </el-dialog>

  </div>
</template>

<script>
import baseListQuery, { checkPermission } from '@/utils/abp'
import {
  getExaminees,
  deleteExaminee,
  batchDeleteExaminee
} from '@/api/exam/examinee'
import Pagination from '@/components/Pagination/index.vue' // secondary package based on el-pagination
import ExamineeSelectTable from './ExamineeSelectTable.vue'
import OrgTree from '@/views/system/identity/components/OrgTree.vue'

export default {
  name: 'ExamineeTable',
  components: {
    Pagination,
    ExamineeSelectTable,
    OrgTree
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
      tableKey: 0,
      list: null,
      total: 0,
      listLoading: true,
      listQuery: Object.assign({
        inExamineeTable: true
      }, baseListQuery),

      dialogFormVisible: false,
      form: {
      },
      formLabelWidth: '120px'
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
      this.$refs['examineeSelectTable'].getList()
    },
    handleSelectOrg(row) {
      this.dialogFormVisible = true
    },
    // 删除
    handleDelete(row) {
      var req = {
        examId: this.examId,
        userIds: [row.userId]
      }
      this.$confirm(
        // 消息
        this.$i18n.t("AbpUi['ItemWillBeDeletedMessageWithFormat']", [
          row.userName
        ]),
        // title
        this.$i18n.t("AbpUi['AreYouSure']"), {
          confirmButtonText: this.$i18n.t("AbpUi['Yes']"), // 确认按钮
          cancelButtonText: this.$i18n.t("AbpUi['Cancel']"), // 取消按钮
          type: 'warning' // 弹框类型
        }
      ).then(async() => {
        // 回调函数
        batchDeleteExaminee(req).then(() => {
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
    handleBatchDelete() {
      var selections = this.$refs.multipleTable.selection
      if (selections.length <= 0) {
        this.$message({
          message: '请先选中一行数据 !',
          type: 'warning'
        })
        return
      }
      const ids = selections.map((x) => x.userId)
      var req = {
        examId: this.examId,
        userIds: ids
      }

      this.$confirm(
        this.$i18n.t("AbpUi['ItemsWillBeDeletedMessage']"),
        this.$i18n.t("AbpUi['AreYouSure']"), {
          confirmButtonText: this.$i18n.t("AbpUi['Yes']"), // 确认按钮
          cancelButtonText: this.$i18n.t("AbpUi['Cancel']"), // 取消按钮
          type: 'warning' // 弹框类型
        }
      ).then(async() => {
        batchDeleteExaminee(req).then(() => {
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

<style scoped>
.line{
  text-align: center;
}

</style>

