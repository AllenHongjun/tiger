<template>
  <div class="app-container">
    <div class="filter-container">
      <el-row>
        <el-button v-if="checkPermission('Platform.Layout.Create')" class="filter-item" type="primary" icon="el-icon-plus" @click="handleCreate">
          {{ $t("AbpIdentityServer['Permissions:Create']") }}
        </el-button>
      </el-row>
    </div>

    <el-table :key="tableKey" v-loading="listLoading" :data="list" border fit highlight-current-row :stripe="true" style="width: 100%;" @sort-change="sortChange">
      <el-table-column type="index" width="80" />
      <el-table-column :label="$t('AbpIdentityServer[\'Name\']')" prop="name" align="left" width="160">
        <template slot-scope="{ row }">
          <span>{{ row.name }}</span>
        </template>
      </el-table-column>
      <el-table-column :label="$t('AbpIdentityServer[\'DisplayName\']')" prop="displayName" align="left" :show-overflow-tooltip="true" width="160">
        <template slot-scope="{ row }">
          <span>{{ row.displayName }}</span>
        </template>
      </el-table-column>
      <el-table-column :label="$t('AbpIdentityServer[\'Description\']')" prop="description" align="left" :show-overflow-tooltip="true">
        <template slot-scope="{ row }">
          <span>{{ row.description }}</span>
        </template>
      </el-table-column>

      <el-table-column :label="$t('AbpUi[\'Actions\']')" align="left" width="200" class-name="small-padding fixed-width">
        <template slot-scope="{ row, $index }">
          <el-button v-if="checkPermission('Platform.Layout.Update')" type="primary" @click="handleUpdate(row)">
            {{ $t("AbpUi['Edit']") }}
          </el-button>
          <el-button v-if="checkPermission('Platform.Layout.Delete')" type="danger" @click="handleDelete(row, $index)">
            {{ $t("AbpUi['Delete']") }}
          </el-button>
        </template>
      </el-table-column>
    </el-table>

    <el-dialog :title=" dialogStatus == 'create'? $t('AbpIdentityServer[\'Permissions:Create\']'): $t('AbpUi[\'Edit\']')" :visible.sync="dialogFormVisible" append-to-body>

      <el-form ref="dataForm" :model="temp" :rules="rules" label-width="160px" class="demo-ruleForm">
        <!-- <el-form-item label="DisplayName:ApiResources" prop="apiResourceId">
          <span slot="label">
            <el-tooltip content="The unique identifier for this resource. Used in requests" placement="top">
              <i class="el-icon-question" />
            </el-tooltip>
            {{ $t('AbpIdentityServer[\'DisplayName:ApiResources\']') }}
          </span>
          <el-input v-model="temp.apiResourceId" disabled />
        </el-form-item> -->

        <el-form-item label="Name" prop="name">
          <span slot="label">
            <el-tooltip content="The unique identifier for this resource. Used in requests" placement="top">
              <i class="el-icon-question" />
            </el-tooltip>
            {{ $t('AbpIdentityServer[\'Name\']') }}
          </span>
          <el-input v-model="temp.name" />
          <input v-model="temp.apiResourceId" type="hidden">
        </el-form-item>

        <el-form-item label="DisplayName" prop="displayName">
          <span slot="label">
            <el-tooltip content="Scope name that will be seen on consent screens" placement="top">
              <i class="el-icon-question" />
            </el-tooltip>
            {{ $t('AbpIdentityServer[\'DisplayName\']') }}
          </span>
          <el-input v-model="temp.displayName" />
        </el-form-item>

        <el-form-item label="Description" prop="description">
          <span slot="label">
            <el-tooltip content="Scope description that will be seen on consent screens" placement="top">
              <i class="el-icon-question" />
            </el-tooltip>
            {{ $t('AbpIdentityServer[\'Description\']') }}
          </span>
          <el-input v-model="temp.description" type="textarea" :autosize="{ minRows: 4, maxRows: 6}" />
        </el-form-item>

        <el-form-item label="Required" prop="required">
          <span slot="label">
            <el-tooltip content="Consent to use scope is required to complete authentication" placement="top">
              <i class="el-icon-question" />
            </el-tooltip>
            {{ $t('AbpIdentityServer[\'Required\']') }}
          </span>
          <el-switch v-model="temp.required" />
        </el-form-item>

        <el-form-item label="ShowInDiscoveryDocument" prop="showInDiscoveryDocument">
          <span slot="label">
            <el-tooltip content="Scope appears within Discovery Document (/.well-known/openid-configuration)" placement="top">
              <i class="el-icon-question" />
            </el-tooltip>
            {{ $t('AbpIdentityServer[\'ShowInDiscoveryDocument\']') }}
          </span>
          <el-switch v-model="temp.showInDiscoveryDocument" />
        </el-form-item>

        <el-form-item label="Emphasize" prop="emphasize">
          <span slot="label">
            <el-tooltip content="Emphasize scope name on consent screen" placement="top">
              <i class="el-icon-question" />
            </el-tooltip>
            {{ $t('AbpIdentityServer[\'Emphasize\']') }}
          </span>
          <el-switch v-model="temp.emphasize" />
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

import baseListQuery, {
  checkPermission
} from '@/utils/abp'

export default {
  name: 'Scope',
  components: {
  },
  props: {
    apiResourceId: {
      type: String,
      require: false,
      default: undefined
    },
    scopes: {
      type: Array,
      require: false,
      // 对象或数组默认值必须从一个工厂函数获取
      default: function() {
        return []
      }
    }
  },
  data() {
    return {
      tableKey: 0,
      list: null,
      total: 0,
      listLoading: true,
      listQuery: baseListQuery,
      dialogFormVisible: false,
      dialogStatus: '',

      temp: {
        apiResourceId: undefined,
        name: '',
        displayName: '',
        description: '',
        required: true,
        emphasize: true,
        showInDiscoveryDocument: true

      },
      rules: {
        name: [
          {
            required: true,
            message: this.$i18n.t("AbpValidation['The {0} field is required.']", [
              this.$i18n.t("AbpIdentityServer['Name']")
            ]),
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
            max: 200,
            message: this.$i18n.t(
              "AbpValidation['The field {0} must be a string with a maximum length of {1}.']",
              [this.$i18n.t("AbpIdentityServer['DisplayName']"), '200']
            ),
            trigger: 'blur'
          }
        ],
        description: [
          {
            max: 2000,
            message: this.$i18n.t(
              "AbpValidation['The field {0} must be a string with a maximum length of {1}.']",
              [this.$i18n.t("AbpIdentityServer['Description']"), '2000']
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
      this.list = this.scopes
      this.listLoading = false
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
        apiResourceId: undefined,
        name: '',
        displayName: '',
        description: '',
        required: true,
        emphasize: true,
        showInDiscoveryDocument: true
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
          this.temp.apiResourceId = this.apiResourceId
          this.list.unshift(this.temp)
          this.dialogFormVisible = false
          this.$emit('set-api-scopes', this.list)

          console.log('this.apiResourceId', this.apiResourceId)
          console.log('this.temp', this.temp)
          console.log('this.list', this.list)
          this.$notify({
            title: this.$i18n.t("TigerUi['Success']"),
            message: this.$i18n.t("TigerUi['SuccessMessage']"),
            type: 'success',
            duration: 2000
          })
        }
      })
    },

    // 更新按钮点击
    handleUpdate(row) {
      this.temp = Object.assign({}, row) // copy obj
      this.dialogStatus = 'update'
      this.dialogFormVisible = true
      this.$nextTick(() => {
        this.$refs['dataForm'].clearValidate()
      })
    },

    // 更新数据
    updateData() {
      this.$refs['dataForm'].validate(valid => {
        if (valid) {
          this.temp.apiResourceId = this.apiResourceId
          const index = this.list.findIndex((v) => v.name === this.temp.name)
          this.list.splice(index, 1, this.temp)
          // 通过自定义事件给父组件赋值
          this.$emit('set-api-scopes', this.list)

          this.dialogFormVisible = false
          this.$notify({
            title: this.$i18n.t("TigerUi['Success']"),
            message: this.$i18n.t("TigerUi['SuccessMessage']"),
            type: 'success',
            duration: 2000
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
        this.temp.apiResourceId = this.apiResourceId
        // 回调函数
        const index = this.list.findIndex((v) => v.name === row.name)
        this.list.splice(index, 1)
        this.$emit('set-api-scopes', this.list)

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
