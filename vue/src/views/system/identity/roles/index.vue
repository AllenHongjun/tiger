<template>
  <div class="app-container">
    <el-row :gutter="0">
      <el-col :span="24">
        <!-- 查询条件 -->
        <el-row style="margin-bottom: 20px">
          <el-input v-model="listQuery.filter" :placeholder="$t('AbpUi[\'PagerSearch\']')" clearable style="width: 150px" class="filter-item" />
          <el-button class="filter-item" type="primary" icon="el-icon-search" @click="handleFilter" />
          <el-button v-if="checkPermission('AbpIdentity.Roles.Create')" type="primary" icon="el-icon-edit" @click="handleCreate">{{ $t("AbpIdentity['NewRole']") }}</el-button>
          <el-button type="primary" plain class="filter-item" icon="el-icon-refresh" @click="handleRefresh">{{ $t("AbpIdentity['Refresh']") }}</el-button>
          <el-button v-if="checkPermission('AbpIdentity.Roles.ExportXlsx')" type="primary" icon="el-icon-download" :loading="downloadLoading" @click="handleDownload">{{ $t('AbpUi.Export') }}</el-button>
          <el-button v-if="checkPermission('AbpIdentity.Roles.ImportXlsx')" type="primary" icon="el-icon-upload" @click="handleImport">{{ $t('AbpUi.Import') }}</el-button>
        </el-row>

        <!-- 表格数据 -->
        <el-table v-loading="listLoading" :data="list" element-loading-text="Loading" border fit highlight-current-row @sort-change="sortChange">
          <el-table-column type="selection" width="55" center />
          <el-table-column align="center" label="ID" width="95">
            <template slot-scope="scope">{{ scope.$index }}</template>
          </el-table-column>
          <el-table-column :label="$t('AbpIdentity[\'RoleName\']')" align="left" prop="name" sortable>
            <template slot-scope="scope">
              {{ scope.row.name }}
              <el-tag v-if="scope.row.isPublic">
                {{ $t('AbpIdentity[\'DisplayName:IsPublic\']') }}
              </el-tag>
              <el-tag v-if="scope.row.isDefault" type="warning">
                {{ $t('AbpIdentity[\'DisplayName:IsDefault\']') }}
              </el-tag>
              <el-tag v-if="scope.row.isStatic" type="success">
                {{ $t('AbpIdentity[\'DisplayName:IsStatic\']') }}
              </el-tag>

            </template>
          </el-table-column>
          <el-table-column :label="$t('AbpIdentity[\'UserCount\']')" align="center" width="180">
            <template slot-scope="scope">{{ scope.row.extraProperties.UserCount }}</template>
          </el-table-column>

          <el-table-column :label="$t('AbpIdentity[\'Actions\']')" align="center" width="280" fixed="right">
            <template slot-scope="{ row, $index }">
              <el-button v-if="checkPermission('AbpIdentity.Roles.Update')" class="el-icon-edit" :title="$t('AbpIdentity[\'Edit\']')" type="primary" @click="handleUpdate(row)" />

              <el-button v-if="!row.isStatic && checkPermission('AbpIdentity.Roles.Delete')" class="el-icon-delete" :title="$t('AbpIdentity[\'Delete\']')" type="danger" @click="handleDelete(row, $index)" />
              <el-dropdown style="margin-left:12px;" @command="handleCommand">
                <el-button class="el-icon-more" :title="$t('AbpUi[\'Actions\']')" type="primary" plain />
                <el-dropdown-menu slot="dropdown">
                  <el-dropdown-item v-if="checkPermission('AbpIdentity.Users.ManageClaims')" :command="beforeHandleCommand(row, 'manageClaims')">
                    {{ $t("AbpIdentity['ManageClaim']") }}
                  </el-dropdown-item>
                  <el-dropdown-item v-if="checkPermission('AbpIdentity.Users.ManagePermissions')" :command="beforeHandleCommand(row, 'updatePermission')">
                    {{ $t("AbpIdentity['Permissions']") }}
                  </el-dropdown-item>
                  <el-dropdown-item v-if="checkPermission('AbpIdentity.Users.ResetPassword')" :command="beforeHandleCommand(row, 'moveAllUsers')">
                    {{ $t("AbpIdentity['MoveAllUsers']") }}
                  </el-dropdown-item>
                </el-dropdown-menu>
              </el-dropdown>

            </template>
          </el-table-column>
        </el-table>
        <pagination v-show="total > 0" :total="total" :page.sync="listQuery.page" :limit.sync="listQuery.limit" @pagination="fetchData" />

        <!-- 表单弹框 -->
        <el-dialog :title="dialogStatus == 'create'? $t('AbpIdentity[\'NewRole\']'): $t('AbpIdentity[\'Edit\']')" :visible.sync="dialogFormVisible">
          <el-form ref="dataForm" :rules="rules" :model="temp" label-width="120px" label-position="right">
            <el-form-item :label="$t('AbpIdentity[\'RoleName\']')" prop="name">
              <el-input v-model="temp.name" />
            </el-form-item>
            <el-form-item :label="$t('AbpIdentity[\'DisplayName:IsDefault\']')" prop="isDefault">
              <el-checkbox v-model="temp.isDefault" />
            </el-form-item>
            <el-form-item :label="$t('AbpIdentity[\'DisplayName:IsPublic\']')" prop="isPublic">
              <el-checkbox v-model="temp.isPublic" />
            </el-form-item>
          </el-form>
          <div style="text-align: right">
            <el-button type="danger" @click="dialogFormVisible = false">{{ $t("AbpIdentity['Cancel']") }}</el-button>
            <el-button type="primary" @click="dialogStatus === 'create' ? createData() : updateData()">{{ $t("AbpIdentity['Save']") }}</el-button>
          </div>
        </el-dialog>

        <upload-single-excel ref="ImportExcelDialog" file-name="role" :import-from-xlsx="ImportRoleFromXlsx" :import-xlsx-template="ImportRoleXlsxTemplate" @call-filter="handleFilter" />

        <!-- 移动所有用户 -->
        <el-dialog :title="$t('AbpIdentity[\'MoveAllUsers\']')" :visible.sync="moveUserDialogVisible">
          <el-form :model="temp" label-width="120px" label-position="right">
            <el-form-item prop="displayName">
              <span slot="label">
                <el-tooltip :content="$t('AbpIdentity[\'MoveAllUsersTips\']')" placement="top">
                  <i class="el-icon-question" />
                </el-tooltip>
                {{ $t('AbpIdentity[\'RoleName\']') }}
              </span>
              <el-select v-model="targetRoleId" placeholder="请选择">
                <el-option
                  v-for="item in roleOptions"
                  :key="item.id"
                  :label="item.name"
                  :value="item.id"
                />
              </el-select>
            </el-form-item>
          </el-form>
          <div style="text-align: right">
            <el-button type="danger" @click="moveUserDialogVisible = false">{{ $t("AbpUi['Cancel']") }}</el-button>
            <el-button type="primary" @click="moveAllUsersData()">{{ $t("AbpUi['Save']") }}</el-button>
          </div>
        </el-dialog>

        <role-claim ref="claimTypeDialog" />
        <grant-permission ref="permissionDialog" provider-name="R" />
      </el-col>
    </el-row>
  </div>
</template>

<script>
import Pagination from '@/components/Pagination/index.vue' // Secondary package based on el-pagination
import GrantPermission from '../components/GrantPermission.vue'
import RoleClaim from './components/RoleClaim.vue'
import UploadSingleExcel from '@/components/UploadSingleExcel/index.vue'

import {
  getRoleList,
  deleteRole,
  createRole,
  updateRole,
  moveAllUsers,
  ImportRoleXlsxTemplate,
  ImportRoleFromXlsx,
  ExportRoleToXlsx
} from '@/api/system-manage/identity/role'

import {
  checkPermission
} from '@/utils/abp'

import baseListQuery from '@/utils/abp'
import { downloadByData } from '@/utils/download'

export default {
  name: 'Role',
  components: {
    Pagination,
    RoleClaim,
    UploadSingleExcel,
    GrantPermission
  },
  data() {
    return {
      tableKey: 0,
      list: null,
      listLoading: true,
      downloadLoading: false,
      total: 0,
      listQuery: baseListQuery,
      dialogStatus: '',
      dialogFormVisible: false,
      claimTypeDialogFormVisible: false,
      moveUserDialogVisible: false,
      targetRoleId: undefined,
      roleOptions: [],
      temp: {
        id: '',
        name: '',
        orgId: '',
        isDefault: false,
        isPublic: false,
        isStatic: false
      },
      rules: {
        name: [{
          required: true,
          message: this.$i18n.t("AbpIdentity['The {0} field is required.']", [
            this.$i18n.t("AbpIdentity['RoleName']")
          ]),
          trigger: 'blur'
        },
        {
          max: 256,
          message: this.$i18n.t(
            "AbpIdentity['The field {0} must be a string with a maximum length of {1}.']",
            [this.$i18n.t("AbpIdentity['RoleName']"), '256']
          ),
          trigger: 'blur'
        }
        ]
      }
    }
  },

  created() {
    this.fetchData()
  },
  methods: {
    checkPermission,
    ImportRoleXlsxTemplate,
    ImportRoleFromXlsx,
    fetchRoleOptions() {
      var req = {
        page: 1,
        limit: 1000
      }
      getRoleList(req).then(response => {
        this.roleOptions = response.items
      })
    },
    fetchData() {
      this.listLoading = true
      getRoleList(this.listQuery).then((response) => {
        this.list = response.items
        this.total = response.totalCount
        this.listLoading = false
      })
    },
    sortChange(data) {
      const {
        prop,
        order
      } = data
      this.listQuery.sort = order ? `${prop} ${order}` : undefined
      this.handleFilter()
    },
    handleFilter(firstPage = false) {
      if (firstPage) {
        this.listQuery.page = 1
      }
      this.fetchData()
    },
    handleRefresh() {
      this.handleFilter()
    },
    resetTemp() {
      this.temp = {
        name: '',
        isDefault: false,
        isPublic: false
      }
    },
    // 导出 所有
    handleDownload() {
      this.$confirm('是否确认导出全部查询结果?', this.$i18n.t("AbpUi['AreYouSure']"), {
        confirmButtonText: this.$i18n.t("AbpUi['Yes']"),
        cancelButtonText: this.$i18n.t("AbpUi['Cancel']"),
        type: 'warning'
      }).then(response => {
        this.downloadLoading = true
        ExportRoleToXlsx(this.listQuery).then(response => {
          downloadByData(response, 'role.xlsx')
          this.downloadLoading = false
        }).catch(err => {
          console.log(err)
          this.downloadLoading = false
          this.$message.warning(err)
        })
      })
    },
    handleImport(row) {
      this.$refs['ImportExcelDialog'].handleUploadExcel()
    },

    handleCommand(param) {
      switch (param.command) {
        case 'manageClaims':
          this.handleManageClaims(param.scope)
          break
        case 'updatePermission':
          this.handleUpdatePermission(param.scope)
          break
        case 'moveAllUsers':
          this.handleMoveAllUsers(param.scope)
          break
        default:
          break
      }
    },
    beforeHandleCommand(scope, command) {
      return {
        scope: scope,
        command: command
      }
    },

    handleCreate() {
      this.resetTemp()
      this.dialogStatus = 'create'
      this.dialogFormVisible = true
      this.$nextTick(() => {
        this.$refs['dataForm'].clearValidate()
      })
    },
    createData() {
      this.$refs['dataForm'].validate((valid) => {
        if (valid) {
          createRole(this.temp).then(() => {
            this.list.unshift(this.temp)
            this.handleFilter()
            this.dialogFormVisible = false
            this.$notify({
              title: '成功',
              message: '操作成功',
              type: 'success',
              duration: 2000
            })
          })
        }
      })
    },

    handleUpdate(row) {
      this.temp = Object.assign({}, row) // copy obj
      this.dialogStatus = 'update'
      this.dialogFormVisible = true
      this.$nextTick(() => {
        this.$refs['dataForm'].clearValidate()
      })
    },
    updateData() {
      this.$refs['dataForm'].validate((valid) => {
        if (valid) {
          const tempData = Object.assign({}, this.temp)
          updateRole(tempData.id, tempData).then(() => {
            this.handleFilter()
            this.dialogFormVisible = false
            this.$notify({
              title: this.$i18n.t("TigerUi['Success']"),
              message: this.$i18n.t("TigerUi['SuccessMessage']"),
              type: 'success',
              duration: 2000
            })
          })
        }
      })
    },
    handleMoveAllUsers(row) {
      if (row.extraProperties.UserCount <= 0) {
        this.$message.error(this.$i18n.t('AbpIdentity[\'NoUserFoundInRole\']'))
        return
      }
      this.fetchRoleOptions()

      this.temp = Object.assign({}, row) // copy obj
      this.moveUserDialogVisible = true
    },
    moveAllUsersData() {
      moveAllUsers(this.temp.id, this.targetRoleId).then(() => {
        this.handleFilter()
        this.moveUserDialogVisible = false
        this.$notify({
          title: this.$i18n.t("TigerUi['Success']"),
          message: this.$i18n.t("TigerUi['SuccessMessage']"),
          type: 'success',
          duration: 2000
        })
      })
    },
    deleteData(row) {
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
      ).then(() => {
        deleteRole(row.id)
          .then((response) => {
            this.handleFilter()
            this.$message({
              title: this.$i18n.t("TigerUi['Success']"),
              message: this.$i18n.t("TigerUi['SuccessMessage']"),
              type: 'success',
              duration: 2000
            })
          })
          .catch((err) => {
            console.log(err)
          })
      }).catch(() => {
        this.$message({
          type: 'info',
          message: this.$i18n.t("AbpUi['Cancel']")
        })
      })
    },
    handleManageClaims(row) {
      this.$refs['claimTypeDialog'].handleManageClaims(row)
    },
    handleUpdatePermission(row) {
      this.$refs['permissionDialog'].handleUpdatePermission(row)
    }
  }
}
</script>

<style lang="scss" scoped>
.permissionTree {
    height: 450px;
    overflow-y: scroll;
}
</style>
