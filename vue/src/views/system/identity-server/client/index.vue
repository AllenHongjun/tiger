<template>
  <div class="app-container">
    <div class="filter-container">
      <el-row style="margin-bottom: 20px">
        <el-input v-model="listQuery.filter" :placeholder="$t('AbpUi[\'PagerSearch\']')" style="width: 200px;" class="filter-item" @keyup.enter.native="handleFilter" />
        <el-button type="primary" class="filter-item" icon="el-icon-search" @click="handleFilter">
          {{ $t('AbpUi.Search') }}
        </el-button>
        <el-button v-if="checkPermission('IdentityServer.Clients.Create')" class="filter-item" style="margin-left: 10px;" type="primary" icon="el-icon-plus" @click="handleCreate">
          {{ $t("AbpIdentityServer['Client:New']") }}
        </el-button>
      </el-row>
    </div>

    <el-table :key="tableKey" v-loading="listLoading" :data="list" border fit highlight-current-row :stripe="true" style="width: 100%;" @sort-change="sortChange">
      <el-table-column type="index" width="80" />
      <el-table-column :label="$t('AbpIdentityServer[\'Client:Id\']')" prop="clientId" sortable align="center" width="180">
        <template slot-scope="{ row }">
          <span>{{ row.clientId }}</span>
        </template>
      </el-table-column>
      <el-table-column :label="$t('AbpIdentityServer[\'Client:Name\']')" prop="clientName" sortable align="center" width="180">
        <template slot-scope="{ row }">
          <span>{{ row.clientName }}</span>
        </template>
      </el-table-column>
      <el-table-column :label="$t('AbpIdentityServer[\'Client:Enabled\']')" prop="enabled" sortable align="center" width="120">
        <template slot-scope="{row}">
          <el-tag :type="row.enabled | enableFilter">
            {{ row.enabled == true ? '启用' : '禁用' }}
          </el-tag>
        </template>
      </el-table-column>
      <el-table-column :label="$t('AbpIdentityServer[\'Description\']')" prop="description" sortable align="center">
        <template slot-scope="{ row }">
          <span>{{ row.description }}</span>
        </template>
      </el-table-column>

      <el-table-column :label="$t('AbpUi[\'Actions\']')" align="center" width="200" class-name="small-padding fixed-width">
        <template slot-scope="{ row, $index }">
          <el-button v-if="checkPermission('IdentityServer.Clients.Update')" type="primary" @click="handleUpdate(row)">
            {{ $t("AbpUi['Edit']") }}
          </el-button>
          <el-button v-if="checkPermission('IdentityServer.Clients.Delete')" type="danger" @click="handleDelete(row, $index)">
            {{ $t("AbpUi['Delete']") }}
          </el-button>
        </template>
      </el-table-column>
    </el-table>

    <pagination v-show="total > 0" :total="total" :page.sync="listQuery.page" :limit.sync="listQuery.limit" @pagination="getList" />

    <el-dialog :title=" dialogStatus == 'create'? $t('AbpIdentityServer[\'Client:New\']'): $t('AbpUi[\'Edit\']')" :visible.sync="dialogFormVisible">
      <el-form ref="dataForm" :rules="rules" :model="temp" label-position="right" label-width="170px">
        <el-tabs v-model="dialogTabActiveName">
          <el-tab-pane :label="$t('AbpIdentityServer[\'Basics\']')" name="first">
            <el-form-item :label="$t('AbpIdentityServer[\'Client:Id\']')" prop="clientId">
              <el-input v-model="temp.clientId" />
            </el-form-item>
            <el-form-item :label="$t('AbpIdentityServer[\'Client:Name\']')" prop="clientName">
              <el-input v-model="temp.clientName" />
            </el-form-item>
            <el-form-item :label="$t('AbpIdentityServer[\'Description\']')" prop="description">
              <el-input v-model="temp.description" />
            </el-form-item>
            <el-form-item :label="$t('AbpIdentityServer[\'Client:ClientUri\']')" prop="clientUri">
              <el-input v-model="temp.clientUri" />
            </el-form-item>
            <el-form-item :label="$t('AbpIdentityServer[\'Client:LogoUri\']')" prop="logoUri">
              <el-input v-model="temp.logoUri" />
            </el-form-item>

            <el-row>
              <el-col :span="12">
                <el-form-item :label="$t('AbpIdentityServer[\'Client:Enabled\']')" prop="enabled">
                  <el-checkbox v-model="temp.enabled" />
                </el-form-item>
              </el-col>
              <el-col :span="12">
                <el-form-item :label="$t('AbpIdentityServer[\'Client:ProtocolType\']')" prop="protocolType">
                  <el-select v-model="temp.protocolType" placeholder="请选择">
                    <el-option key="" label="OpenID Connect" value="oidc" />
                  </el-select>
                </el-form-item>
              </el-col>
            </el-row>

            <el-row>
              <el-col :span="12">
                <el-form-item :label="$t('AbpIdentityServer[\'Client:RequireRequestObject\']')" prop="requireRequestObject">
                  <el-checkbox v-model="temp.requireRequestObject" />
                </el-form-item>
              </el-col>
              <el-col :span="12">
                <el-form-item :label="$t('AbpIdentityServer[\'Client:AllowedIdentityTokenSigningAlgorithms\']')" prop="allowedIdentityTokenSigningAlgorithms">
                  <el-checkbox v-model="temp.allowedIdentityTokenSigningAlgorithms" />
                </el-form-item>
              </el-col>
            </el-row>

            <el-row>
              <el-col :span="12">
                <el-form-item :label="$t('AbpIdentityServer[\'Client:RequiredPkce\']')" prop="requiredPkce">
                  <el-checkbox v-model="temp.requiredPkce" />
                </el-form-item>
              </el-col>
              <el-col :span="12">
                <el-form-item :label="$t('AbpIdentityServer[\'Client:AllowedPlainTextPkce\']')" prop="allowedPlainTextPkce">
                  <el-checkbox v-model="temp.allowedPlainTextPkce" />
                </el-form-item>
              </el-col>
            </el-row>

            <el-row>
              <el-col :span="12">
                <el-form-item :label="$t('AbpIdentityServer[\'Client:RequireConsent\']')" prop="requireConsent">
                  <el-checkbox v-model="temp.requireConsent" />
                </el-form-item>
              </el-col>
              <el-col :span="12"><el-form-item :label="$t('AbpIdentityServer[\'Client:AllowRememberConsent\']')" prop="allowRememberConsent">
                <el-checkbox v-model="temp.allowRememberConsent" />
              </el-form-item></el-col>
            </el-row>
          </el-tab-pane>
          <el-tab-pane label="Secret" name="second">
            <client-secret>11</client-secret>
          </el-tab-pane>
          <el-tab-pane label="ClientIdentityResource" name="third">
            <client-identity-resource />
          </el-tab-pane>
          <el-tab-pane class="advance" label="Advance" name="fourth">
            <el-tabs type="card">
              <el-tab-pane label="Tokens">
                <client-token />
              </el-tab-pane>
              <el-tab-pane label="Claims">配置管理</el-tab-pane>
              <el-tab-pane label="Grant Types">角色管理</el-tab-pane>
              <el-tab-pane label="Properties">定时任务补偿</el-tab-pane>
              <el-tab-pane label="Others">定时任务补偿</el-tab-pane>
            </el-tabs>
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
ClientToken
<script>
import {
  getClients,
  getClient,
  getClientsAll,
  createClient,
  updateClient,
  deleteClient
} from '@/api/system-manage/identity-server/client'
import Pagination from '@/components/Pagination' // secondary package based on el-pagination

import ClientSecret from './components/ClientSecret'
import ClientIdentityResource from './components/ClientIdentityResource'
import ClientToken from './components/ClientToken'

import baseListQuery, {
  checkPermission
} from '@/utils/abp'

export default {
  name: 'Clients',
  components: {
    Pagination,
    ClientSecret,
    ClientIdentityResource,
    ClientToken
  },
  filters: {
    enableFilter(enable) {
      const enableMap = {
        true: 'success',
        false: 'danger'
      }
      return enableMap[enable]
    }
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
        clientId: '',
        clientName: '',
        description: '',
        clientUri: '',
        logoUri: '',
        enabled: true,
        protocolType: '',
        requireClientSecret: true,
        requireConsent: true,
        allowRememberConsent: true,
        alwaysIncludeUserClaimsInIdToken: true,
        requirePkce: true,
        allowPlainTextPkce: true,
        allowAccessTokensViaBrowser: true,
        frontChannelLogoutUri: '',
        frontChannelLogoutSessionRequired: true,
        backChannelLogoutUri: '',
        backChannelLogoutSessionRequired: true,
        allowOfflineAccess: true,
        identityTokenLifetime: 0,
        accessTokenLifetime: 0,
        authorizationCodeLifetime: 0,
        consentLifetime: 0,
        absoluteRefreshTokenLifetime: 0,
        slidingRefreshTokenLifetime: 0,
        refreshTokenUsage: 0,
        updateAccessTokenClaimsOnRefresh: true,
        refreshTokenExpiration: 0,
        accessTokenType: 0,
        enableLocalLogin: true,
        includeJwtId: true,
        alwaysSendClientClaims: true,
        clientClaimsPrefix: '',
        pairWiseSubjectSalt: '',
        userSsoLifetime: 0,
        userCodeType: '',
        deviceCodeLifetime: 0
      },
      dialogFormVisible: true,
      dialogStatus: '',
      dialogTabActiveName: 'fourth',
      // 表单验证规则
      rules: {
        name: [
          {
            required: true,
            message: this.$i18n.t("AbpValidation['The {0} field is required.']", [
              this.$i18n.t("AbpIdentityServer['Client:Name']")
            ]),
            trigger: 'blur'
          },
          {
            max: 64,
            message: this.$i18n.t(
              "AbpValidation['The field {0} must be a string with a maximum length of {1}.']",
              [this.$i18n.t("AbpIdentityServer['Client:Name']"), '64']
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
      getClients(this.listQuery).then(response => {
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
        id: undefined,
        name: '',
        displayName: '',
        description: '',
        path: '',
        redirect: '',
        dataId: undefined,
        freamwork: ''
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
          createClient(this.temp).then(() => {
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

      getClient(row.id).then(response => {
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
          updateClient(this.temp.id, this.temp).then(() => {
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
        deleteClient(row.id).then(() => {
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

<style lang="scss" scoped>
.line{
  text-align: center;
}

/*.advance .el-tabs .el-tabs__item{
 margin-right:5px;
}*/
</style>

