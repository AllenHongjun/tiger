<template>
  <div class="app-container">
    <div class="filter-container">
      <el-row style="margin-bottom: 20px">
        <el-input v-model="listQuery.filter" :placeholder="$t('AbpUi[\'PagerSearch\']')" style="width: 200px;" class="filter-item" @keyup.enter.native="handleFilter" />
        <el-button type="primary" class="filter-item" icon="el-icon-search" @click="handleFilter">
          {{ $t('AbpUi.Search') }}
        </el-button>
        <el-button v-if="checkPermission('IdentityServer.Grants.Create')" class="filter-item" style="margin-left: 10px;" type="primary" icon="el-icon-plus" @click="handleCreate">
          {{ $t("AbpIdentityServer['Permissions:Create']") }}
        </el-button>
      </el-row>
    </div>

    <el-table :key="tableKey" v-loading="listLoading" :data="list" border fit highlight-current-row :stripe="true" style="width: 100%;" @sort-change="sortChange">
      <el-table-column type="index" width="80" />
      <el-table-column :label="$t('AbpIdentityServer[\'Grants:Key\']')" prop="key" sortable align="center">
        <template slot-scope="{ row }">
          <span>{{ row.key }}</span>
        </template>
      </el-table-column>
      <el-table-column :label="$t('AbpIdentityServer[\'Grants:Type\']')" prop="type" align="center">
        <template slot-scope="{ row }">
          <span>{{ row.type }}</span>
        </template>
      </el-table-column>
      <el-table-column :label="$t('AbpIdentityServer[\'Grants:SessionId\']')" prop="sessionId" sortable align="center">
        <template slot-scope="{ row }">
          <span>{{ row.sessionId }}</span>
        </template>
      </el-table-column>
      <el-table-column :label="$t('AbpIdentityServer[\'Grants:SubjectId\']')" prop="subjectId" sortable align="center">
        <template slot-scope="{ row }">
          <span>{{ row.subjectId }}</span>
        </template>
      </el-table-column>
      <el-table-column :label="$t('AbpIdentityServer[\'Grants:ConsumedTime\']')" prop="consumedTime" sortable align="center">
        <template slot-scope="{ row }">
          <span>{{ row.consumedTime }}</span>
        </template>
      </el-table-column>
      <el-table-column :label="$t('AbpIdentityServer[\'Grants:Profile\']')" prop="profile" sortable align="center">
        <template slot-scope="{ row }">
          <span>{{ row.profile }}</span>
        </template>
      </el-table-column>
      <el-table-column :label="$t('AbpIdentityServer[\'Grants:Data\']')" prop="data" sortable align="center">
        <template slot-scope="{ row }">
          <span>{{ row.data }}</span>
        </template>
      </el-table-column>

      <el-table-column :label="$t('AbpUi[\'Actions\']')" align="center" width="200" class-name="small-padding fixed-width">
        <template slot-scope="{ row, $index }">
          <el-button v-if="checkPermission('IdentityServer.Grants.Update')" type="primary" @click="handleUpdate(row)">
            {{ $t("AbpUi['Edit']") }}
          </el-button>
          <el-button v-if="checkPermission('IdentityServer.Grants.Delete')" type="danger" @click="handleDelete(row, $index)">
            {{ $t("AbpUi['Delete']") }}
          </el-button>
        </template>
      </el-table-column>
    </el-table>

    <pagination v-show="total > 0" :total="total" :page.sync="listQuery.page" :limit.sync="listQuery.limit" @pagination="getList" />

    <el-dialog :title=" dialogStatus == 'create'? $t('AbpIdentityServer[\'Permissions:Create\']'): $t('AbpUi[\'Edit\']')" :visible.sync="dialogFormVisible">
      <el-form ref="dataForm" :rules="rules" :model="temp" label-position="right" label-width="150px">
        <el-form-item :label="$t('AbpIdentityServer[\'Grants:Key\']')" prop="key">
          <el-input v-model="temp.key" />
        </el-form-item>
        <el-form-item :label="$t('AbpIdentityServer[\'Grants:Type\']')" prop="type">
          <el-input v-model="temp.type" />
        </el-form-item>
        <el-form-item :label="$t('AbpIdentityServer[\'Grants:SubjectId\']')" prop="subjectId">
          <el-input v-model="temp.subjectId" />
        </el-form-item>
        <el-form-item :label="$t('AbpIdentityServer[\'Client:Id\']')" prop="clientId">
          <el-select v-model="temp.clientId" placeholder="请选择">
            <el-option
              v-for="item in clientSelectOptions"
              :key="item.id"
              :label="item.clientName"
              :value="item.id"
            />
          </el-select>
        </el-form-item>
        <el-form-item :label="$t('AbpIdentityServer[\'Grants:ConsumedTime\']')" prop="consumedTime">
          <el-input v-model="temp.consumedTime" />
        </el-form-item>
        <el-form-item :label="$t('AbpIdentityServer[\'Grants:Profile\']')" prop="profile">
          <el-input v-model="temp.profile" />
        </el-form-item>
        <el-form-item :label="$t('AbpIdentityServer[\'Grants:Data\']')" prop="data">
          <el-input v-model="temp.data" type="textarea" :autosize="{ minRows:2 , maxRows:4}" />
        </el-form-item>

      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button @click="dialogFormVisible = false">
          {{ $t("AbpUi['Cancel']") }}
        </el-button>
        <el-button type="primary" @click="dialogStatus === 'create' ? createData() : updateData()">
          {{ $t("AbpUi['Save']") }}
        </el-button>
      </div>
    </el-dialog>
  </div>
</template>

<script>
import {
  getPersistedGrants,
  getPersistedGrant,
  getPersistedGrantsAll,
  createPersistedGrant,
  updatePersistedGrant,
  deletePersistedGrant
} from '@/api/system-manage/identity-server/persisted-grant'
import {
  getClients
} from '@/api/system-manage/identity-server/client'

import Pagination from '@/components/Pagination' // secondary package based on el-pagination
import baseListQuery, {
  checkPermission
} from '@/utils/abp'

export default {
  name: 'PersistedGrants',
  components: {
    Pagination
  },
  data() {
    return {
      clientSelectOptions: [],
      tableKey: 0,
      list: null,
      total: 0,
      listLoading: true,
      listQuery: baseListQuery,
      temp: {
        id: undefined,
        name: '',
        displayName: '',
        description: '',
        path: '',
        redirect: '',
        dataId: undefined,
        freamwork: ''
      },
      dialogFormVisible: false,
      dialogStatus: '',

      // 表单验证规则
      rules: {
        name: [
          {
            required: true,
            message: this.$i18n.t("AbpValidation['The {0} field is required.']", [
              this.$i18n.t("AbpIdentityServer['DisplayName:Name']")
            ]),
            trigger: 'blur'
          },
          {
            max: 64,
            message: this.$i18n.t(
              "AbpValidation['The field {0} must be a string with a maximum length of {1}.']",
              [this.$i18n.t("AbpIdentityServer['DisplayName:Name']"), '64']
            ),
            trigger: 'blur'
          }
        ],
        displayName: [
          {
            required: true,
            message: this.$i18n.t("AbpValidation['The {0} field is required.']", [
              this.$i18n.t("AbpIdentityServer['DisplayName:DisplayName']")
            ]),
            trigger: 'blur'
          },
          {
            max: 256,
            message: this.$i18n.t(
              "AbpValidation['The field {0} must be a string with a maximum length of {1}.']",
              [this.$i18n.t("AbpIdentityServer['DisplayName:Name']"), '256']
            ),
            trigger: 'blur'
          }
        ],
        description: [
          {
            max: 256,
            message: this.$i18n.t(
              "AbpValidation['The field {0} must be a string with a maximum length of {1}.']",
              [this.$i18n.t("AbpIdentityServer['DisplayName:Description']"), '256']
            ),
            trigger: 'blur'
          }
        ]

      }
    }
  },
  created() {
    this.getList()
    this.fetchClients()
  },
  methods: {
    checkPermission, // 检查权限
    // 获取列表数据
    fetchClients() {
      var input = {
        page: 1,
        limit: 999
      }
      getClients(input).then(response => {
        this.clientSelectOptions = response.items
      })
    },
    // 获取列表数据
    getList() {
      this.listLoading = true
      getPersistedGrants(this.listQuery).then(response => {
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

    // 重置表单
    resetTemp() {
      this.temp = {
        name: '',
        displayName: '',
        description: '',
        path: '',
        redirect: '',
        dataId: undefined,
        freamwork: '',

        id: undefined,
        key: '',
        type: '',
        subjectId: '',
        clientId: '',
        expiration: '',
        data: ''
      }
    },

    // 点击创建按钮
    handleCreate() {
      this.resetTemp()
      this.dialogStatus = 'create'
      this.dialogFormVisible = true
      this.$nextTick(() => {
        this.$refs['dataForm'].clearValidate()
      })
    },

    // 创建数据
    createData() {
      this.$refs['dataForm'].validate(valid => {
        if (valid) {
          createPersistedGrant(this.temp).then(() => {
            this.list.unshift(this.temp)
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

    // 更新按钮点击
    handleUpdate(row) {
      this.temp = Object.assign({}, row) // copy obj
      this.dialogStatus = 'update'
      this.dialogFormVisible = true

      getPersistedGrant(row.id).then(response => {
        this.temp = response
      })

      this.$nextTick(() => {
        this.$refs['dataForm'].clearValidate()
      })
    },

    // 更新数据
    updateData() {
      this.$refs['dataForm'].validate(valid => {
        if (valid) {
          updatePersistedGrant(this.temp.id, this.temp).then(() => {
            const index = this.list.findIndex((v) => v.id === this.temp.id)
            this.list.splice(index, 1, this.temp)
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

    // 删除
    handleDelete(row, index) {
      this.$confirm(
        // 消息
        this.$i18n.t("AbpUi['ItemWillBeDeletedMessage']", [
          row.name
        ]),
        // title
        this.$i18n.t("AbpUi['ItemWillBeDeletedMessage']"), {
          confirmButtonText: this.$i18n.t("AbpUi['Yes']"), // 确认按钮
          cancelButtonText: this.$i18n.t("AbpUi['Cancel']"), // 取消按钮
          type: 'warning' // 弹框类型
        }
      ).then(async() => {
        // 回调函数
        deletePersistedGrant(row.id).then(() => {
          const index = this.list.findIndex((v) => v.id === row.id)
          this.list.splice(index, 1)
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
