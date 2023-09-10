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

    <el-dialog :title=" dialogStatus == 'create'? $t('AbpIdentityServer[\'Client:New\']'): $t('AbpUi[\'Edit\']')" :visible.sync="dialogFormVisible" top="7vh" width="65%">
      <el-form ref="dataForm" :rules="rules" :model="temp" label-position="right" label-width="300px">
        <el-tabs v-model="dialogTabActiveName">
          <el-tab-pane :label="$t('AbpIdentityServer[\'Basics\']')" name="first">
            <el-form-item label="$t('AbpIdentityServer[\'Client:Id\']')" prop="requirePkce">
              <span slot="label">
                <el-tooltip content="Is Enable" placement="top">
                  <i class="el-icon-question" />
                </el-tooltip>
                {{ $t('AbpIdentityServer[\'Enabled\']') }}
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
          <el-tab-pane :label="$t('AbpIdentityServer[\'Secret\']')" name="second">
            <secret v-if="temp && formDataFlag" :secrets="temp.clientSecrets" @set-client-secret="temp.clientSecrets = $event" />
          </el-tab-pane>
          <el-tab-pane :label="$t('AbpIdentityServer[\'Client:Resources\']')" name="third">
            <client-identity-resource v-if="temp.allowedScopes && allowedScopesFlag" :identity-resources="temp.allowedScopes" @set-identity-resources="temp.allowedScopes = $event" />
          </el-tab-pane>
          <el-tab-pane class="advance" :label="$t('AbpIdentityServer[\'Advanced\']')" name="fourth">
            <el-tabs type="card">
              <el-tab-pane :label="$t('AbpIdentityServer[\'Consent\']')">
                <client-consent v-if="temp && formDataFlag" :rule-form="temp" @set-form-rules="Object.assign(rules,$event)" />
              </el-tab-pane>
              <el-tab-pane :label="$t('AbpIdentityServer[\'Token\']')">
                <client-token v-if="temp && formDataFlag" :rule-form="temp" />
              </el-tab-pane>
              <el-tab-pane label="RefreshToken">
                <client-refresh-token v-if="temp && formDataFlag" :rule-form="temp" />
              </el-tab-pane>
              <el-tab-pane label="Signin Signout">
                <client-signin-signout v-if="temp && formDataFlag" :rule-form="temp" @set-form-rules="Object.assign(rules,$event)" />
              </el-tab-pane>
              <el-tab-pane :label="$t('AbpIdentityServer[\'DeviceFlow\']')">
                <el-form-item label="Device Flow Request Lifetime" prop="deviceCodeLifetime">
                  <span slot="label">
                    <el-tooltip content="Lifetime in seconds" placement="top">
                      <i class="el-icon-question" />
                    </el-tooltip>
                    {{ $t('AbpIdentityServer[\'Client:DeviceCodeLifetime\']') }}
                  </span>
                  <el-input v-model="temp.deviceCodeLifetime" type="number" />
                </el-form-item>
                <el-form-item label="User Code Generator Type" prop="userCodeType">
                  <span slot="label">
                    <el-tooltip content="Override default type" placement="top">
                      <i class="el-icon-question" />
                    </el-tooltip>
                    {{ $t('AbpIdentityServer[\'Client:UserCodeType\']') }}
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
                    {{ $t('AbpIdentityServer[\'Client:RequiredPkce\']') }}
                  </span>
                  <el-switch v-model="temp.requirePkce" />
                </el-form-item>
                <el-form-item label="Allow Plain Text PKCE" prop="allowPlainTextPkce">
                  <span slot="label">
                    <el-tooltip content="Allows the client to send unhashed code verifiers. Not recommended" placement="top">
                      <i class="el-icon-question" />
                    </el-tooltip>
                    {{ $t('AbpIdentityServer[\'Client:AllowedPlainTextPkce\']') }}
                  </span>
                  <el-switch v-model="temp.allowPlainTextPkce" />
                </el-form-item>
              </el-tab-pane>
              <el-tab-pane :label="$t('AbpIdentityServer[\'Claims\']')">
                <userClaim :user-claims="temp.claims" @set-user-claims="temp.claims = $event" />
              </el-tab-pane>
              <el-tab-pane :label="$t('AbpIdentityServer[\'Client:AllowedGrantTypes\']')">
                <client-grant-type v-if="temp && formDataFlag" :allowed-grant-types="temp.allowedGrantTypes" />
              </el-tab-pane>
              <el-tab-pane label="Identity Providers">
                <client-identity-provider v-if="temp && formDataFlag" :rule-form="temp" :identity-provider-restrictions="temp.identityProviderRestrictions" />
              </el-tab-pane>
              <el-tab-pane :label="$t('AbpIdentityServer[\'Permissions:ManageProperties\']')">
                <client-properties v-if="temp && formDataFlag" :properties="temp.properties" />
              </el-tab-pane>
              <el-tab-pane label="Others">
                <el-form-item label="SSO Lifetime" prop="userSsoLifetime">
                  <span slot="label">
                    <el-tooltip content="Lifetime in seconds between user authentication challenges. Defaults to IdentityServer session lifetime" placement="top">
                      <i class="el-icon-question" />
                    </el-tooltip>
                    {{ $t('AbpIdentityServer[\'Client:UserSsoLifetime\']') }}
                  </span>
                  <el-input v-model="temp.userSsoLifetime" type="number" />
                </el-form-item>
                <el-form-item label="Pairwise Subject Salt" prop="pairWiseSubjectSalt">
                  <span slot="label">
                    <el-tooltip content="The salt value used when generating pairwise subject IDs" placement="top">
                      <i class="el-icon-question" />
                    </el-tooltip>
                    {{ $t('AbpIdentityServer[\'Client:PairWiseSubjectSalt\']') }}
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

import Secret from '../components/Secret.vue'
import ClientIdentityResource from './components/ClientIdentityResource.vue'
import ClientConsent from './components/ClientConsent.vue'
import ClientToken from './components/ClientToken.vue'
import ClientRefreshToken from './components/ClientRefreshToken.vue'
import ClientSigninSignout from './components/ClientSigninSignout.vue'
import ClientGrantType from './components/ClientGrantType.vue'
import UserClaim from '../components/UserClaim.vue'
import ClientIdentityProvider from './components/ClientIdentityProvider.vue'
import ClientProperties from './components/ClientProperties.vue'

import baseListQuery, {
  checkPermission
} from '@/utils/abp'

export default {
  name: 'Clients',
  components: {
    Pagination,
    Secret,
    ClientIdentityResource,
    ClientConsent,
    ClientToken,
    ClientRefreshToken,
    ClientSigninSignout,
    ClientGrantType,
    UserClaim,
    ClientIdentityProvider,
    ClientProperties
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
        deviceCodeLifetime: 0,
        allowedGrantTypes: [
          {
            grantType: ''
          }
        ],
        allowedScopes: [

        ],
        clientSecrets: [

        ],
        allowedCorsOrigins: [

        ],
        redirectUris: [

        ],
        postLogoutRedirectUris: [

        ],
        identityProviderRestrictions: [

        ],
        claims: [

        ],
        properties: [

        ]
      },
      formDataFlag: false,
      allowedScopesFlag: false, // allowedScopesFlag 标记只有接口请求成功的时候才向子组件传递数据
      dialogFormVisible: false,
      dialogStatus: '',
      dialogTabActiveName: 'first',
      // 表单验证规则
      rules: {
        clientId: [
          {
            required: true,
            message: this.$i18n.t("AbpValidation['The {0} field is required.']", [
              this.$i18n.t("AbpIdentityServer['Client:Name']")
            ]),
            trigger: 'blur'
          },
          {
            max: 200,
            message: this.$i18n.t(
              "AbpValidation['The field {0} must be a string with a maximum length of {1}.']",
              [this.$i18n.t("AbpIdentityServer['Client:Name']"), '200']
            ),
            trigger: 'blur'
          }
        ],
        clientName: [
          {
            required: true,
            message: this.$i18n.t("AbpValidation['The {0} field is required.']", [
              this.$i18n.t("AbpIdentityServer['Client:ClientName']")
            ]),
            trigger: 'blur'
          },
          {
            max: 200,
            message: this.$i18n.t(
              "AbpValidation['The field {0} must be a string with a maximum length of {1}.']",
              [this.$i18n.t("AbpIdentityServer['Client:ClientName']"), '200']
            ),
            trigger: 'blur'
          }
        ],
        description: [
          {
            max: 1000,
            message: this.$i18n.t(
              "AbpValidation['The field {0} must be a string with a maximum length of {1}.']",
              [this.$i18n.t("AbpIdentityServer['Client:Description']"), '1000']
            ),
            trigger: 'blur'
          }
        ],
        pairWiseSubjectSalt: [
          {
            max: 100,
            message: this.$i18n.t(
              "AbpValidation['The field {0} must be a string with a maximum length of {1}.']",
              [this.$i18n.t("AbpIdentityServer['Client:Name']"), '100']
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
    onSetFormData: function(consent) {
      console.log('consent', consent)
      this.temp.consentLifetime = consent.consentLifetime
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
      this.dialogStatus = 'update'
      this.dialogFormVisible = true

      getClient(row.id).then(response => {
        this.temp = response
        this.formDataFlag = true
        this.allowedScopesFlag = true
        // console.log('this.temp.allowdScope', this.temp.allowedScopes)
      })

      this.$nextTick(() => {
        this.$refs['dataForm'].clearValidate()
      })
    },

    // 更新数据
    updateData() {
      this.$refs['dataForm'].validate(valid => {
        if (valid) {
          // console.log('temp', this.temp)
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

