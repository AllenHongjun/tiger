<template>
  <div class="app-container">
    <div class="filter-container">
      <el-row style="margin-bottom: 20px">
        <el-input v-model="listQuery.filter" :placeholder="$t('AbpUi[\'PagerSearch\']')" style="width: 200px;" class="filter-item" @keyup.enter.native="handleFilter" />
        <el-button type="primary" class="filter-item" icon="el-icon-search" @click="handleFilter">
          {{ $t('AbpUi.Search') }}
        </el-button>
        <el-button v-if="checkPermission('IdentityServer.IdentityResources.Create')" class="filter-item" style="margin-left: 10px;" type="primary" icon="el-icon-plus" @click="handleCreate">
          {{ $t("AbpIdentityServer['Resource:New']") }}
        </el-button>
      </el-row>
    </div>

    <el-table :key="tableKey" v-loading="listLoading" :data="list" border fit highlight-current-row :stripe="true" style="width: 100%;" @sort-change="sortChange">
      <el-table-column type="index" width="80" />
      <el-table-column :label="$t('AbpIdentityServer[\'Enabled\']')" prop="enabled" align="center" width="100">
        <template slot-scope="{ row }">
          <el-tag :type="row.enabled ? 'primary' : 'danger'" disable-transitions>{{ row.enabled ? '启用' : '禁用' }}</el-tag>
        </template>
      </el-table-column>
      <el-table-column :label="$t('AbpIdentityServer[\'Required\']')" prop="required" align="center" width="100">
        <template slot-scope="{ row }">
          <el-tag :type="row.required ? 'primary' : 'danger'" disable-transitions>{{ row.required ? '是' : '否' }}</el-tag>
        </template>
      </el-table-column>
      <el-table-column :label="$t('AbpIdentityServer[\'Name\']')" prop="name" sortable align="center">
        <template slot-scope="{ row }">
          <span>{{ row.name }}</span>
        </template>
      </el-table-column>

      <el-table-column :label="$t('AbpIdentityServer[\'DisplayName\']')" align="center">
        <template slot-scope="{ row }">
          <span>{{ row.displayName }}</span>
        </template>
      </el-table-column>
      <el-table-column :label="$t('AbpIdentityServer[\'Description\']')" prop="description" sortable align="center">
        <template slot-scope="{ row }">
          <span>{{ row.description }}</span>
        </template>
      </el-table-column>
      <el-table-column :label="$t('AbpIdentityServer[\'Emphasize\']')" prop="emphasize" align="center" width="100">
        <template slot-scope="{ row }">
          <el-tag :type="row.emphasize ? 'primary' : 'danger'" disable-transitions>{{ row.emphasize ? '是' : '否' }}</el-tag>
        </template>
      </el-table-column>
      <el-table-column :label="$t('AbpIdentityServer[\'ShowInDiscoveryDocument\']')" prop="showInDiscoveryDocument" align="center" width="140">
        <template slot-scope="{ row }">
          <el-tag :type="row.showInDiscoveryDocument ? 'primary' : 'danger'" disable-transitions>{{ row.showInDiscoveryDocument ? '是' : '否' }}</el-tag>
        </template>
      </el-table-column>

      <el-table-column :label="$t('AbpUi[\'Actions\']')" align="center" width="200" class-name="small-padding fixed-width">
        <template slot-scope="{ row, $index }">
          <el-button v-if="checkPermission('IdentityServer.IdentityResources.Update')" type="primary" @click="handleUpdate(row)">
            {{ $t("AbpUi['Edit']") }}
          </el-button>
          <el-button v-if="checkPermission('IdentityServer.IdentityResources.Delete')" type="danger" @click="handleDelete(row, $index)">
            {{ $t("AbpUi['Delete']") }}
          </el-button>
        </template>
      </el-table-column>
    </el-table>

    <pagination v-show="total > 0" :total="total" :page.sync="listQuery.page" :limit.sync="listQuery.limit" @pagination="getList" />

    <el-dialog :title=" dialogStatus == 'create'? $t('AbpIdentityServer[\'Resource:New\']'): $t('AbpUi[\'Edit\']')" :visible.sync="dialogFormVisible">
      <el-form ref="dataForm" :rules="rules" :model="temp" label-position="right" label-width="180px">

        <el-tabs v-model="activeName" @tab-click="handleClick">
          <el-tab-pane :label="$t('AbpIdentityServer[\'Basics\']')" name="first">
            <el-form-item :label="$t('AbpIdentityServer[\'Enabled\']')" prop="requirePkce">
              <span slot="label">
                <el-tooltip content="Is Enable" placement="top">
                  <i class="el-icon-question" />
                </el-tooltip>
                {{ $t('AbpIdentityServer[\'Enabled\']') }}
              </span>
              <el-switch v-model="temp.enabled" />
            </el-form-item>
            <el-form-item :label="$t('AbpIdentityServer[\'Name\']')" prop="name">
              <span slot="label">
                <el-tooltip content="The unique identifier for this resource. Used in requests" placement="top">
                  <i class="el-icon-question" />
                </el-tooltip>
                {{ $t('AbpIdentityServer[\'Name\']') }}
              </span>
              <el-input v-model="temp.name" />
            </el-form-item>
            <el-form-item :label="$t('AbpIdentityServer[\'DisplayName\']')" prop="displayName">
              <span slot="label">
                <el-tooltip content="Resource name that will be seen on consent screens" placement="top">
                  <i class="el-icon-question" />
                </el-tooltip>
                {{ $t('AbpIdentityServer[\'DisplayName\']') }}
              </span>
              <el-input v-model="temp.displayName" />
            </el-form-item>
            <el-form-item :label="$t('AbpIdentityServer[\'Description\']')" prop="description">
              <span slot="label">
                <el-tooltip content="Resource description that will be seen on consent screens" placement="top">
                  <i class="el-icon-question" />
                </el-tooltip>
                {{ $t('AbpIdentityServer[\'Description\']') }}
              </span>
              <el-input v-model="temp.description" type="textarea" :autosize="{ minRows:4, maxRows:6}" />
            </el-form-item>
            <el-form-item :label="$t('AbpIdentityServer[\'Required\']')" prop="required">
              <span slot="label">
                <el-tooltip content="Consent to use resource is required to complete authentication" placement="top">
                  <i class="el-icon-question" />
                </el-tooltip>
                {{ $t('AbpIdentityServer[\'Required\']') }}
              </span>
              <el-switch v-model="temp.required" />
            </el-form-item>
            <el-form-item :label="$t('AbpIdentityServer[\'ShowInDiscoveryDocument\']')" prop="showInDiscoveryDocument">
              <span slot="label">
                <el-tooltip content="Resource appears within Discovery Document (/.well-known/openid-configuration)" placement="top">
                  <i class="el-icon-question" />
                </el-tooltip>
                {{ $t('AbpIdentityServer[\'ShowInDiscoveryDocument\']') }}
              </span>
              <el-switch v-model="temp.showInDiscoveryDocument" />
            </el-form-item>
            <el-form-item :label="$t('AbpIdentityServer[\'Emphasize\']')" prop="emphasize">
              <span slot="label">
                <el-tooltip content="Is Enable" placement="top">
                  <i class="el-icon-question" />
                </el-tooltip>
                {{ $t('AbpIdentityServer[\'Emphasize\']') }}
              </span>
              <el-switch v-model="temp.emphasize" />
            </el-form-item>

          </el-tab-pane>
          <el-tab-pane :label="$t('AbpIdentityServer[\'UserClaim\']')" name="second">
            UserClaim
          </el-tab-pane>
          <el-tab-pane :label="$t('AbpIdentityServer[\'Propertites\']')" name="third">Propertites</el-tab-pane>
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
  getIdentityResources,
  getIdentityResource,
  createIdentityResource,
  updateIdentityResource,
  deleteIdentityResource
} from '@/api/system-manage/identity-server/identity-resource'
import Pagination from '@/components/Pagination' // secondary package based on el-pagination
import baseListQuery, {
  checkPermission
} from '@/utils/abp'

export default {
  name: 'IdentityResources',
  components: {
    Pagination
  },
  data() {
    return {
      tableKey: 0,
      list: null,
      total: 0,
      listLoading: true,
      listQuery: baseListQuery,
      temp: {
        name: '',
        displayName: '',
        description: '',
        enabled: true,
        required: true,
        emphasize: true,
        showInDiscoveryDocument: true,
        userClaims: [
          {
            type: ''
          }
        ],
        properties: [
          {
            'key': '',
            'value': ''
          }
        ]
      },
      dialogFormVisible: false,
      dialogStatus: '',
      activeName: 'first',

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
        ],
        displayName: [
          {
            required: true,
            message: this.$i18n.t("AbpValidation['The {0} field is required.']", [
              this.$i18n.t("AbpIdentityServer['DisplayName']")
            ]),
            trigger: 'blur'
          },
          {
            max: 256,
            message: this.$i18n.t(
              "AbpValidation['The field {0} must be a string with a maximum length of {1}.']",
              [this.$i18n.t("AbpIdentityServer['DisplayName']"), '256']
            ),
            trigger: 'blur'
          }
        ],
        description: [
          {
            max: 256,
            message: this.$i18n.t(
              "AbpValidation['The field {0} must be a string with a maximum length of {1}.']",
              [this.$i18n.t("AbpIdentityServer['Description']"), '256']
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
      getIdentityResources(this.listQuery).then(response => {
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
          createIdentityResource(this.temp).then(() => {
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

      getIdentityResource(row.id).then(response => {
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
          updateIdentityResource(this.temp.id, this.temp).then(() => {
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
        deleteIdentityResource(row.id).then(() => {
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
    },

    // Tab 点击事件
    handleClick(tab, event) {
      console.log(tab, event)
    }
  }
}
</script>
