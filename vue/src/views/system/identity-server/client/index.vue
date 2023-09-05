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

    <el-dialog :title=" dialogStatus == 'create'? $t('AbpIdentityServer[\'Client:New\']'): $t('AbpUi[\'Edit\']')" :visible.sync="dialogFormVisible" top="10vh" width="65%">
      <el-form ref="dataForm" :rules="rules" :model="temp" label-position="right" label-width="300px">
        <el-tabs v-model="dialogTabActiveName">
          <el-tab-pane :label="$t('AbpIdentityServer[\'Basics\']')" name="first">
            <el-form-item label="Enabled" prop="requirePkce">
              <span slot="label">
                <el-tooltip content="Is Enable" placement="top">
                  <i class="el-icon-question" />
                </el-tooltip>
                Enabled
              </span>
              <el-switch v-model="temp.enabled" />
            </el-form-item>
            <el-form-item :label="$t('AbpIdentityServer[\'Client:Id\']')" prop="clientId">
              <span slot="label">
                <el-tooltip content="The unique identifier for this application" placement="top">
                  <i class="el-icon-question" />
                </el-tooltip>
                {{ $t('AbpIdentityServer[\'Client:Id\']') }}
              </span>
              <el-input v-model="temp.clientId" />
            </el-form-item>
            <el-form-item :label="$t('AbpIdentityServer[\'Client:Name\']')" prop="clientName">
              <span slot="label">
                <el-tooltip content="Friendly name for this client application" placement="top">
                  <i class="el-icon-question" />
                </el-tooltip>
                {{ $t('AbpIdentityServer[\'Client:Name\']') }}
              </span>
              <el-input v-model="temp.clientName" />
            </el-form-item>
            <el-form-item :label="$t('AbpIdentityServer[\'Description\']')" prop="description">
              <span slot="label">
                <el-tooltip content="Application description for use within Tiger" placement="top">
                  <i class="el-icon-question" />
                </el-tooltip>
                {{ $t('AbpIdentityServer[\'Description\']') }}
              </span>
              <el-input v-model="temp.description" type="textarea" :autosize="{ minRows:4, maxRows:6}" />
            </el-form-item>

          </el-tab-pane>
          <el-tab-pane label="Secret" name="second">
            <client-secret>11</client-secret>
          </el-tab-pane>
          <el-tab-pane label="ClientIdentityResource" name="third">
            <client-identity-resource />
          </el-tab-pane>
          <el-tab-pane class="advance" label="Advance" name="fourth">
            <el-tabs type="card">
              <el-tab-pane label="Consent">
                <client-consent />
              </el-tab-pane>
              <el-tab-pane label="Tokens">
                <client-token />
              </el-tab-pane>
              <el-tab-pane label="Refresh Token">
                <client-refresh-token />
              </el-tab-pane>
              <el-tab-pane label="Signin Signout">
                <client-signin-signout />
              </el-tab-pane>
              <el-tab-pane label="Device Flow">
                <el-form-item label="Device Flow Request Lifetime" prop="deviceCodeLifetime">
                  <span slot="label">
                    <el-tooltip content="Lifetime in seconds" placement="top">
                      <i class="el-icon-question" />
                    </el-tooltip>
                    Device Flow Request Lifetime
                  </span>
                  <el-input v-model="temp.deviceCodeLifetime" />
                </el-form-item>
                <el-form-item label="User Code Generator Type" prop="userCodeType">
                  <span slot="label">
                    <el-tooltip content="Override default type" placement="top">
                      <i class="el-icon-question" />
                    </el-tooltip>
                    Device Flow Request Lifetime
                  </span>
                  <el-input v-model="temp.userCodeType" />
                </el-form-item>
              </el-tab-pane>
              <el-tab-pane label="PKCE">
                <el-form-item label="Require PKCE" prop="requirePkce">
                  <span slot="label">
                    <el-tooltip content="Enable to send the session ID during single sign-out" placement="top">
                      <i class="el-icon-question" />
                    </el-tooltip>
                    Require PKCE
                  </span>
                  <el-switch v-model="temp.requirePkce" />
                </el-form-item>
                <el-form-item label="Allow Plain Text PKCE" prop="allowPlainTextPkce">
                  <span slot="label">
                    <el-tooltip content="Allows the client to send unhashed code verifiers. Not recommended" placement="top">
                      <i class="el-icon-question" />
                    </el-tooltip>
                    Allow Plain Text PKCE
                  </span>
                  <el-switch v-model="temp.allowPlainTextPkce" />
                </el-form-item>
              </el-tab-pane>
              <el-tab-pane label="Claims">
                <userClaim />
              </el-tab-pane>
              <el-tab-pane label="Grant Types">
                <client-grant-type />
              </el-tab-pane>
              <el-tab-pane label="Identity Providers">Identity Providers</el-tab-pane>
              <el-tab-pane label="Properties">
                <properties />
              </el-tab-pane>
              <el-tab-pane label="Others">
                <el-form-item label="SSO Lifetime" prop="userSsoLifetime">
                  <span slot="label">
                    <el-tooltip content="Lifetime in seconds between user authentication challenges. Defaults to IdentityServer session lifetime" placement="top">
                      <i class="el-icon-question" />
                    </el-tooltip>
                    SSO Lifetime
                  </span>
                  <el-input v-model="temp.userSsoLifetime" />
                </el-form-item>
                <el-form-item label="Pairwise Subject Salt" prop="pairWiseSubjectSalt">
                  <span slot="label">
                    <el-tooltip content="The salt value used when generating pairwise subject IDs" placement="top">
                      <i class="el-icon-question" />
                    </el-tooltip>
                    Pairwise Subject Salt
                  </span>
                  <el-input v-model="temp.pairWiseSubjectSalt" />
                </el-form-item>
              </el-tab-pane>
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

import ClientSecret from './components/ClientSecret.vue'
import ClientIdentityResource from './components/ClientIdentityResource.vue'
import ClientConsent from './components/ClientConsent.vue'
import ClientToken from './components/ClientToken.vue'
import ClientRefreshToken from './components/ClientRefreshToken.vue'
import ClientSigninSignout from './components/ClientSigninSignout.vue'
import ClientGrantType from './components/ClientGrantType.vue'
import UserClaim from '../components/UserClaim.vue'
import Properties from '../components/Properties.vue'

import baseListQuery, {
  checkPermission
} from '@/utils/abp'

export default {
  name: 'Clients',
  components: {
    Pagination,
    ClientSecret,
    ClientIdentityResource,
    ClientConsent,
    ClientToken,
    ClientRefreshToken,
    ClientSigninSignout,
    ClientGrantType,
    UserClaim,
    Properties
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

