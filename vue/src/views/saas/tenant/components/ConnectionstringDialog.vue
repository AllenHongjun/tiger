<template>
  <el-dialog :title="$t('AbpTenantManagement[\'ConnectionStrings\']')" :visible.sync="dialogFormVisible">
    <el-form ref="dataForm" :rules="rules" :model="temp" label-position="right" label-width="200px">
      <el-form-item>
        <el-checkbox v-model="useSharedDatabase" :label="$t('AbpTenantManagement[\'DisplayName:UseSharedDatabase\']')" />
      </el-form-item>
      <el-form-item v-if="!useSharedDatabase" :label=" $t('AbpSaas[\'DisplayName:Value\']')" prop="value">
        <el-input v-model="temp.value" />
      </el-form-item>
    </el-form>
    <div slot="footer" class="dialog-footer">
      <el-button @click="dialogFormVisible = false">
        {{ $t("AbpUi['Cancel']") }}
      </el-button>
      <el-button type="primary" @click="updateData()">
        {{ $t("AbpUi['Save']") }}
      </el-button>
    </div>
  </el-dialog>
</template>

<script>
import {
  getDefaultConnectionString,
  getDefaultConnectionStringByName,
  updateDefaultConnectionString,
  deleteDefaultConnectionString
} from '@/api/sass/tenant'

export default {
  name: 'ConnectionstringDialog',
  data() {
    return {
      tenantId: '',
      useSharedDatabase: true,
      temp: {
        name: 'Default',
        value: '',
        defaultConnectionString: ''
      },
      dialogFormVisible: false,
      rules: {
        defaultConnectionString: [{
          max: 1024,
          message: this.$i18n.t("AbpTenantManagement['The field {0} must be a string with a maximum length of {1}.']",
            [this.$i18n.t("AbpSaas['DisplayName:Value']"), '1024']
          ),
          trigger: 'blur'
        }]
      }
    }
  },
  methods: {
    resetTemp() {
      this.temp = {
        name: 'Default',
        value: '',
        defaultConnectionString: ''
      }
    },
    handleUpdate(row) {
      this.resetTemp()
      this.tenantId = row.id
      this.dialogFormVisible = true

      getDefaultConnectionStringByName(row.id, 'Default').then(response => {
        if (response.value) {
          this.useSharedDatabase = false
          this.temp.value = response.value
        } else {
          this.useSharedDatabase = true
        }
      })

      this.$nextTick(() => {
        this.$refs['dataForm'].clearValidate()
      })
    },
    updateData() {
      this.$refs['dataForm'].validate(valid => {
        if (valid) {
          if (this.useSharedDatabase || (!this.useSharedDatabase && !this.temp.value)) {
            deleteDefaultConnectionString(this.tenantId).then(() => {
              this.dialogFormVisible = false
              this.$notify({
                title: this.$i18n.t("TigerUi['Success']"),
                message: this.$i18n.t("TigerUi['SuccessMessage']"),
                type: 'success',
                duration: 2000
              })
            })
          } else {
            updateDefaultConnectionString(this.tenantId, this.temp).then(() => {
              this.dialogFormVisible = false
              this.$notify({
                title: this.$i18n.t("TigerUi['Success']"),
                message: this.$i18n.t("TigerUi['SuccessMessage']"),
                type: 'success',
                duration: 2000
              })
            })
          }
        }
      })
    }
  }
}
</script>
