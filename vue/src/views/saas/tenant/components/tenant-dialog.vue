<template>
  <el-dialog :title="dialogStatus == 'create'? $t('AbpTenantManagement[\'NewTenant\']'): $t('AbpTenantManagement[\'Edit\']')" :visible.sync="dialogFormVisible">
    <el-form ref="dataForm" :rules="rules" :model="temp" label-position="right" label-width="180px">
      <el-form-item :label="$t('AbpTenantManagement[\'TenantName\']')" prop="name">
        <el-input v-model="temp.name" />
      </el-form-item>
      <el-form-item :label="$t('AbpSaas[\'DisplayName:EditionName\']')" prop="isActive">
        <el-select v-model="temp.editionId" placeholder="请选择">
          <el-option
            v-for="item in editionOptions"
            :key="item.id"
            :label="item.displayName"
            :value="item.id"
          />
        </el-select>
      </el-form-item>

      <el-form-item v-if="!temp.id" :label="$t('AbpSaas[\'DisplayName:AdminEmailAddress\']')" prop="adminEmailAddress">
        <el-input v-model="temp.adminEmailAddress" />
      </el-form-item>
      <el-form-item v-if="!temp.id" :label="$t('AbpSaas[\'DisplayName:AdminPassword\']')" prop="adminPassword">
        <el-input v-model="temp.adminPassword" type="password" auto-complete="off" show-password />
      </el-form-item>

      <el-form-item :label="$t('AbpSaas[\'DisplayName:IsActive\']')" prop="isActive">
        <template slot="label">
          <span>{{ $t('AbpSaas[\'DisplayName:IsActive\']') }}
            <el-tooltip class="item" effect="dark" placement="bottom">
              <i class="el-icon-question" style="font-size: 16px; vertical-align: middle;" />
              <div slot="content">
                <p>默认不限制租户使用时间范围</p>
              </div>
            </el-tooltip>
          </span>
        </template>
        <el-checkbox v-model="temp.isActive" />
      </el-form-item>

      <el-form-item v-if="temp.isActive" :label="$t('AbpSaas[\'DisplayName:EnableTime\']')" prop="enableTime">
        <template slot="label">
          <span>{{ $t('AbpSaas[\'DisplayName:EnableTime\']') }}
            <el-tooltip class="item" effect="dark" placement="bottom">
              <i class="el-icon-question" style="font-size: 16px; vertical-align: middle;" />
              <div slot="content">
                <p>时间不填写就不限制</p>
              </div>
            </el-tooltip>
          </span>
        </template>
        <el-date-picker v-model="temp.enableTime" type="datetime" placeholder="选择日期时间" />
      </el-form-item>
      <el-form-item v-if="temp.isActive" :label="$t('AbpSaas[\'DisplayName:DisableTime\']')" prop="disableTime">
        <template slot="label">
          <span>{{ $t('AbpSaas[\'DisplayName:DisableTime\']') }}
            <el-tooltip class="item" effect="dark" placement="bottom">
              <i class="el-icon-question" style="font-size: 16px; vertical-align: middle;" />
              <div slot="content">
                <p>时间不填写就不限制</p>
              </div>
            </el-tooltip>
          </span>
        </template>
        <el-date-picker v-model="temp.disableTime" type="datetime" placeholder="选择日期时间" />
      </el-form-item>

    </el-form>
    <div slot="footer" class="dialog-footer">
      <el-button @click="dialogFormVisible = false">
        {{ $t("AbpTenantManagement['Cancel']") }}
      </el-button>
      <el-button type="primary" @click="dialogStatus === 'create' ? createData() : updateData()">
        {{ $t("AbpTenantManagement['Save']") }}
      </el-button>
    </div>
  </el-dialog>
</template>

<script>
import {
  createTenant,
  getTenantById,
  updateTenant
} from '@/api/sass/tenant'
import {
  getEditions
} from '@/api/sass/edition'

export default {
  name: 'TenantDialog',
  data() {
    const passwordValidator = (rule, value, callback) => {
      if (!value) {
        callback(
          new Error(
            this.$i18n.t("AbpIdentity['The {0} field is required.']", [
              this.$i18n.t("AbpTenantManagement['DisplayName:AdminPassword']")
            ])
          )
        )
        return
      }
      if (value.length < 6) {
        callback(
          new Error(
            this.$i18n.t("AbpIdentity['Identity.PasswordTooShort']", ['6'])
          )
        )
        return
      }
      if (value.length > 128) {
        callback(
          new Error(
            this.$i18n.t(
              "AbpIdentity['The field {0} must be a string with a maximum length of {1}.']",
              [
                this.$i18n.t(
                  "AbpTenantManagement['DisplayName:AdminPassword']"
                ),
                '128'
              ]
            )
          )
        )
        return
      }
      let reg = /\d+/
      if (!reg.test(value)) {
        callback(
          new Error(
            this.$i18n.t("AbpIdentity['Identity.PasswordRequiresDigit']")
          )
        )
        return
      }
      reg = /[a-z]+/
      if (!reg.test(value)) {
        callback(
          new Error(
            this.$i18n.t("AbpIdentity['Identity.PasswordRequiresLower']")
          )
        )
        return
      }
      reg = /[A-Z]+/
      if (!reg.test(value)) {
        callback(
          new Error(
            this.$i18n.t("AbpIdentity['Identity.PasswordRequiresUpper']")
          )
        )
        return
      }
      reg = /\W+/
      if (!reg.test(value)) {
        callback(
          new Error(
            this.$i18n.t(
              "AbpIdentity['Identity.PasswordRequiresNonAlphanumeric']"
            )
          )
        )
        return
      }

      callback()
    }
    return {
      editionOptions: [],
      temp: {
        id: undefined,
        name: undefined,
        isActive: true,
        editionId: undefined,
        enableTime: undefined,
        disableTime: undefined,
        adminEmailAddress: undefined,
        adminPassword: undefined,
        useSharedDatabase: true,
        defaultConnectionString: undefined
      },
      dialogStatus: '',
      dialogFormVisible: false,
      rules: {
        name: [
          {
            required: true,
            message: this.$i18n.t(
              "AbpTenantManagement['The {0} field is required.']",
              [this.$i18n.t("AbpTenantManagement['TenantName']")]
            ),
            trigger: 'blur'
          },
          {
            max: 256,
            message: this.$i18n.t(
              "AbpTenantManagement['The field {0} must be a string with a maximum length of {1}.']",
              [this.$i18n.t("AbpTenantManagement['TenantName']"), '256']
            ),
            trigger: 'blur'
          }
        ],
        adminEmailAddress: [{
          required: true,
          message: this.$i18n.t(
            "AbpTenantManagement['The {0} field is required.']",
            [
              this.$i18n.t(
                "AbpTenantManagement['DisplayName:AdminEmailAddress']"
              )
            ]
          ),
          trigger: 'blur'
        },
        {
          type: 'email',
          message: this.$i18n.t(
            "AbpTenantManagement['The {0} field is not a valid e-mail address.']",
            [
              this.$i18n.t(
                "AbpTenantManagement['DisplayName:AdminEmailAddress']"
              )
            ]
          ),
          trigger: ['blur', 'change']
        },
        {
          max: 256,
          message: this.$i18n.t(
            "AbpTenantManagement['The field {0} must be a string with a maximum length of {1}.']",
            [
              this.$i18n.t(
                "AbpTenantManagement['DisplayName:AdminEmailAddress']"
              ),
              '256'
            ]
          ),
          trigger: 'blur'
        }
        ],
        adminPassword: [{
          required: true,
          validator: passwordValidator,
          trigger: ['blur', 'change']
        }]
      }
    }
  },
  created() {

  },
  methods: {
    // 获取版本选项的值
    getEditionOptions() {
      var listQuery = {
        page: 1,
        limit: 100 // 版本数量不会超过100
      }
      getEditions(listQuery).then(response => {
        this.editionOptions = response.items
      })
    },
    resetTemp() {
      this.temp = {
        id: undefined,
        name: undefined,
        isActive: true,
        editionId: undefined,
        enableTime: undefined,
        disableTime: undefined,
        adminEmailAddress: undefined,
        adminPassword: undefined,
        useSharedDatabase: true,
        defaultConnectionString: undefined
      }
    },
    handleCreate() {
      this.getEditionOptions()
      this.resetTemp()
      this.dialogStatus = 'create'
      this.dialogFormVisible = true

      this.$nextTick(() => {
        this.$refs['dataForm'].clearValidate()
      })
    },
    createData() {
      this.$refs['dataForm'].validate(valid => {
        if (valid) {
          createTenant(this.temp).then(() => {
            this.$emit('handleFilter')
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
    handleUpdate(row) {
      this.getEditionOptions()
      this.temp = Object.assign({}, row) // copy obj
      this.dialogStatus = 'update'
      this.dialogFormVisible = true

      getTenantById(row.id).then(response => {
        this.temp = response
      })

      this.$nextTick(() => {
        this.$refs['dataForm'].clearValidate()
      })
    },
    updateData() {
      this.$refs['dataForm'].validate(valid => {
        if (valid) {
          updateTenant(this.temp).then(() => {
            this.$emit('handleFilter', false)
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
    }
  }
}
</script>
