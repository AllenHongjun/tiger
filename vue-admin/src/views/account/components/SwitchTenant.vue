<template>
  <div class="swich-tenant-container">
    <el-dialog :title="$t('AbpUiMultiTenancy[\'SwitchTenant\']')" :visible.sync="tenantDialogFormVisible">
      <el-form ref="dataForm" :model="tenant" label-position="top">
        <el-form-item :label="$t('AbpUiMultiTenancy[\'Name\']')">
          <el-input v-model="tenant.name" type="text" />
          <span>{{ $t("AbpUiMultiTenancy['SwitchTenantHint']") }}</span>
        </el-form-item>
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button @click="tenantDialogFormVisible = false">
          {{ $t("AbpTenantManagement['Cancel']") }}
        </el-button>
        <el-button type="primary" :disabled="tenantDisabled" @click="setTenant()">
          {{ $t("AbpTenantManagement['Save']") }}
        </el-button>
      </div>
    </el-dialog>
  </div>
</template>

<script>
export default {
  name: 'SwitchTenant',
  data() {
    return {
      tenantDialogFormVisible: false,
      tenant: {
        name: this.$store.getters.abpConfig.currentTenant.name
      }
    }
  },
  computed: {
    currentTenant() {
      return this.$store.getters.abpConfig.currentTenant.name
    },
    tenantDisabled() {
      if (this.tenant.name && this.tenant.name === this.$store.getters.abpConfig.currentTenant.name) {
        return true
      }
      return false
    }
  },
  methods: {
    setTenant() {
      this.$store.dispatch('app/setTenant', this.tenant.name).then(response => {
        if (response && !response.success) {
          this.$notify({
            title: this.$i18n.t("AbpUi['Error']"),
            message: this.$i18n.t(
              "AbpUiMultiTenancy['GivenTenantIsNotAvailable']",
              [this.tenant.name]
            ),
            type: 'error',
            duration: 2000
          })
          return
        }
        this.$emit('setTenantName', this.tenant.name)
        // bug:后端原因没有在请求头加上 __tenant 所有暂时从配置重获取 租户名称
        // console.log('this.$store.getters.abpConfig.currentTenant.name', this.$store.getters.abpConfig.currentTenant.name)
        this.tenantDialogFormVisible = false
      })
    }
  }
}
</script>

<style scoped>
.line{
  text-align: center;
}
</style>

