<template>
  <div class="app-container">
    <div class="filter-container">
      <el-row style="margin-bottom: 20px">
        <el-input v-model="listQuery.filter" :placeholder="$t('AbpUi[\'PagerSearch\']')" style="width: 200px;" clearable class="filter-item" @keyup.enter.native="handleFilter" />
        <el-button type="primary" class="filter-item" icon="el-icon-search" @click="handleFilter">
          {{ $t('AbpUi.Search') }}
        </el-button>
        <el-button v-if="checkPermission('IdentityServer.ApiResources.Create')" class="filter-item" style="margin-left: 10px;" type="primary" icon="el-icon-plus" @click="handleCreate">
          {{ $t("AbpIdentityServer['Resource:New']") }}
        </el-button>
      </el-row>
    </div>

    <el-table :key="tableKey" v-loading="listLoading" :data="list" border fit highlight-current-row :stripe="true" style="width: 100%;" @sort-change="sortChange">
      <el-table-column type="index" width="80" />
      <el-table-column :label="$t('AbpIdentityServer[\'Name\']')" prop="name" sortable align="center" width="120">
        <template slot-scope="{ row }">
          <span>{{ row.name }}</span>
        </template>
      </el-table-column>
      <el-table-column :label="$t('AbpIdentityServer[\'DisplayName\']')" align="center" width="120">
        <template slot-scope="{ row }">
          <span>{{ row.displayName }}</span>
        </template>
      </el-table-column>
      <el-table-column :label="$t('AbpIdentityServer[\'Description\']')" prop="description" sortable align="center">
        <template slot-scope="{ row }">
          <span>{{ row.description }}</span>
        </template>
      </el-table-column>
      <el-table-column :label="$t('AbpIdentityServer[\'Enabled\']')" prop="enabled" sortable align="center" width="80">
        <template slot-scope="{ row }">
          <el-tag :type="( row.enabled ? 'success' : 'danger')">
            {{ row.enabled ? '启用' : '禁用' }}
          </el-tag>
        </template>
      </el-table-column>
      <el-table-column :label="$t('AbpIdentityServer[\'AllowedAccessTokenSigningAlgorithms\']')" prop="allowedAccessTokenSigningAlgorithms" align="center" width="180">
        <template slot-scope="{ row }">
          <el-tag :type="( row.allowedAccessTokenSigningAlgorithms ? 'success' : 'danger')">
            {{ row.allowedAccessTokenSigningAlgorithms ? '启用' : '禁用' }}
          </el-tag>
        </template>
      </el-table-column>
      <el-table-column :label="$t('AbpIdentityServer[\'ShowInDiscoveryDocument\']')" prop="showInDiscoveryDocument" align="center" width="180">
        <template slot-scope="{ row }">
          <el-tag :type="( row.showInDiscoveryDocument ? 'success' : 'danger')">
            {{ row.showInDiscoveryDocument ? '启用' : '禁用' }}
          </el-tag>
        </template>
      </el-table-column>

      <el-table-column :label="$t('AbpUi[\'Actions\']')" align="center" width="200" class-name="small-padding fixed-width">
        <template slot-scope="{ row, $index }">
          <el-button v-if="checkPermission('IdentityServer.ApiResources.Update')" type="primary" @click="handleUpdate(row)">
            {{ $t("AbpUi['Edit']") }}
          </el-button>
          <el-button v-if="checkPermission('IdentityServer.ApiResources.Delete')" type="danger" @click="handleDelete(row, $index)">
            {{ $t("AbpUi['Delete']") }}
          </el-button>
        </template>
      </el-table-column>
    </el-table>

    <pagination v-show="total > 0" :total="total" :page.sync="listQuery.page" :limit.sync="listQuery.limit" @pagination="getList" />

    <el-dialog :title=" dialogStatus == 'create'? $t('AbpIdentityServer[\'Resource:New\']'): $t('AbpUi[\'Edit\']')" :visible.sync="dialogFormVisible" top="8vh" width="65%">
      <el-form ref="dataForm" :rules="rules" :model="temp" label-position="right" label-width="200px">
        <el-tabs v-model="activeName" @tab-click="handleTabClick">
          <!-- Api 资源基本信息 -->
          <el-tab-pane :label="$t('AbpIdentityServer[\'Basics\']')" name="basic">
            <el-form-item :label="$t('AbpIdentityServer[\'Name\']')" prop="name">
              <el-input v-model="temp.name" />
            </el-form-item>
            <el-form-item :label="$t('AbpIdentityServer[\'DisplayName\']')" prop="displayName">
              <el-input v-model="temp.displayName" />
            </el-form-item>
            <el-form-item :label="$t('AbpIdentityServer[\'Description\']')" prop="description">
              <el-input v-model="temp.description" />
            </el-form-item>
            <el-form-item :label="$t('AbpIdentityServer[\'Enabled\']')" prop="enabled">
              <template>
                <el-radio v-model="temp.enabled" :label="true">启用</el-radio>
                <el-radio v-model="temp.enabled" :label="false">禁用</el-radio>
              </template>
            </el-form-item>
            <el-form-item :label="$t('AbpIdentityServer[\'ShowInDiscoveryDocument\']')" prop="showInDiscoveryDocument">
              <template>
                <el-radio v-model="temp.showInDiscoveryDocument" :label="true">启用</el-radio>
                <el-radio v-model="temp.showInDiscoveryDocument" :label="false">禁用</el-radio>
              </template>
            </el-form-item>
            <el-form-item :label="$t('AbpIdentityServer[\'AllowedAccessTokenSigningAlgorithms\']')" prop="allowedAccessTokenSigningAlgorithms">
              <template>
                <el-radio v-model="temp.allowedAccessTokenSigningAlgorithms" :label="true">启用</el-radio>
                <el-radio v-model="temp.allowedAccessTokenSigningAlgorithms" :label="false">禁用</el-radio>
              </template>
            </el-form-item>
          </el-tab-pane>
          <!-- Api 资源范围 -->
          <el-tab-pane :label="$t('AbpIdentityServer[\'Scope\']')" name="scope">
            <!-- @set-api-scopes="temp.scopes = $event" -->
            <scope v-if="temp && formDataFlag" :api-resource-id="temp.id" :scopes="temp.scopes" />
          </el-tab-pane>
          <!-- Api 资源用户声明 -->
          <el-tab-pane :label="$t('AbpIdentityServer[\'UserClaim\']')" name="claim">
            <user-claim v-if="temp && formDataFlag" :user-claims="temp.userClaims" @set-user-claims="temp.userClaims = $event" />
          </el-tab-pane>
          <el-tab-pane :label="$t('AbpIdentityServer[\'Secret\']')" name="secret">
            <secret v-if="temp && formDataFlag" :secrets="temp.secrets" @set-client-secret="temp.secrets = $event" />
          </el-tab-pane>
          <!-- Api 资源密钥/属性 -->
          <el-tab-pane :label="$t('AbpIdentityServer[\'Advanced\']')" name="properties">
            <properties v-if="temp && formDataFlag" :properties="temp.properties" @set-properties="temp.properties = $event" />
          </el-tab-pane>
        </el-tabs>

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
  getApiResources,
  getApiResource,
  createApiResource,
  updateApiResource,
  deleteApiResource
} from '@/api/system-manage/identity-server/api-resource'
import Pagination from '@/components/Pagination' // secondary package based on el-pagination
import UserClaim from '../components/UserClaim.vue'
import Properties from '../components/Properties.vue'
import Scope from './components/Scope.vue'
import Secret from '../components/Secret.vue'
import baseListQuery, {
  checkPermission
} from '@/utils/abp'

export default {
  name: 'ApiResources',
  components: {
    Pagination,
    Scope,
    UserClaim,
    Secret,
    Properties
  },
  data() {
    return {
      tableKey: 0,
      list: null,
      total: 0,
      listLoading: true,
      listQuery: baseListQuery,
      temp: {
        id: undefined,
        displayName: '',
        description: '',
        enabled: true,
        allowedAccessTokenSigningAlgorithms: true,
        showInDiscoveryDocument: true,
        secrets: [
          {
            type: '',
            value: '',
            description: '',
            expiration: undefined,
            hashType: 0
          }
        ],
        scopes: [
          {
            scope: ''
          }
        ],
        userClaims: [
          {
            type: ''
          }
        ],
        properties: [
          {
            key: '',
            value: ''
          }
        ],
        name: ''
      },
      formDataFlag: false,
      dialogFormVisible: false,
      dialogStatus: '',
      activeName: 'basic',

      // 表单验证规则
      rules: {
        name: [
          {
            required: true,
            message: this.$i18n.t("AbpValidation['The {0} field is required.']", [
              this.$i18n.t("AbpIdentityServer['Name']")
            ]),
            trigger: 'blur'
          },
          {
            max: 64,
            message: this.$i18n.t(
              "AbpValidation['The field {0} must be a string with a maximum length of {1}.']",
              [this.$i18n.t("AbpIdentityServer['Name']"), '64']
            ),
            trigger: 'blur'
          }
        ]

      }
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
      getApiResources(this.listQuery).then(response => {
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
    handleTabClick(tab, event) {
      this.activeName = tab.name
      // console.log(tab, event)
    },
    // 重置表单
    resetTemp() {
      this.temp = {
        id: undefined,
        displayName: '',
        description: '',
        enabled: true,
        allowedAccessTokenSigningAlgorithms: true,
        showInDiscoveryDocument: true,
        secrets: [
          {
            type: '',
            value: '',
            description: '',
            expiration: undefined,
            hashType: 0
          }
        ],
        scopes: [

        ],
        userClaims: [
          {
            type: ''
          }
        ],
        properties: [
          {
            key: '',
            value: ''
          }
        ],
        name: ''
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
          createApiResource(this.temp).then(() => {
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
      this.dialogStatus = 'update'
      this.dialogFormVisible = true

      getApiResource(row.id).then(response => {
        this.temp = response
        this.formDataFlag = true
      })
      this.$nextTick(() => {
        this.$refs['dataForm'].clearValidate()
      })
    },

    // 更新数据
    updateData() {
      this.$refs['dataForm'].validate(valid => {
        if (valid) {
          updateApiResource(this.temp.id, this.temp).then(() => {
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
        deleteApiResource(row.id).then(() => {
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
