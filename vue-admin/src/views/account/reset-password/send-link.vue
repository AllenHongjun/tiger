<template>
  <div class="app-container">
    <div class="title-container">
      <h3 class="title">
        {{ $t('AbpAccount.ForgotPassword') }}
        <lang-select class="set-language" />
      </h3>
      <p class="explain">
        {{ $t("AbpUiMultiTenancy['Tenant']") }}
        <el-tooltip :content="$t('AbpUiMultiTenancy[\'Switch\']')" effect="dark" placement="bottom">
          <el-link :underline="false" @click="handleSetTenant()"><i>{{
            currentTenantName
              ? currentTenantName
              : $t("AbpUiMultiTenancy['NotSelected']")
          }}</i></el-link>
        </el-tooltip>
      </p>
    </div>

    <el-form ref="dataForm" :model="dataForm" :rules="loginRules" class="data-form" auto-complete="on" label-position="left">
      <el-form-item prop="email">
        <el-tooltip class="item" effect="dark" :content="$t('AbpAccount[\'SendPasswordResetLink_Information\']')" placement="left-end">
          <span class="svg-container">
            <svg-icon icon-class="email" />
          </span>
        </el-tooltip>

        <el-input ref="email" v-model="dataForm.email" :placeholder="$t('AbpAccount[\'DisplayName:EmailAddress\']')" name="email" type="text" tabindex="1" auto-complete="on" />
      </el-form-item>

      <el-button :loading="loading" type="primary" style="width:100%;margin-bottom:30px;" @click.native.prevent="handleSendPasswordResetCode">
        {{ $t('AbpUi.Submit') }}
      </el-button>

    </el-form>

    <div class="bottom-container">
      <el-link href="#/login" type="primary">{{ $t('AbpAccount.BackToLogin') }}</el-link>
    </div>

    <switch-tenant ref="SwitchTenantDialog" @setTenantName="getTenantName" />
  </div>
</template>

<script>
import {
  validEmail
} from '@/utils/validate'
import {
  sendPasswordResetCode
} from '@/api/account'

import LangSelect from '@/components/LangSelect/index.vue'
import SwitchTenant from '../components/SwitchTenant.vue'

export default {
  name: 'ResetPassword',
  components: {
    LangSelect,
    SwitchTenant
  },
  data() {
    const validateUsername = (rule, value, callback) => {
      if (!validEmail(value)) {
        callback(new Error(this.$i18n.t("AbpAccount['ThisFieldIsNotAValidEmailAddress.']")))
      } else {
        callback()
      }
    }

    return {
      dialogVisible: false,
      dataForm: {
        email: undefined,
        appName: 'FrontWeb',
        returnUrl: undefined,
        returnUrlHash: undefined
      },
      loginRules: {
        email: [{
          required: true,
          trigger: 'blur',
          validator: validateUsername
        }]
      },
      loading: false,
      passwordType: 'password',
      redirect: undefined,
      tenant: undefined,
      currentTenantName: ''
    }
  },
  watch: {
    $route: {
      handler: function(route) {
        this.redirect = route.query && route.query.redirect
      },
      immediate: true
    }
  },
  methods: {

    handleSendPasswordResetCode() {
      this.$refs.dataForm.validate(valid => {
        if (valid) {
          this.loading = true
          sendPasswordResetCode(this.dataForm)
            .then(res => {
              /* Account recovery email sent to your e-mail address. If you don't see this email in your inbox within 15 minutes, look for it in your junk mail folder. If you find it there, please mark it as -Not Junk-.*/
              // this.$message({ type: 'success', message: this.$i18n.t('AbpAccount.SendPasswordResetLink_Information'), duration: 8000, showClose: true })
              this.$message({ type: 'success', message: this.$i18n.t('AbpAccount.PasswordResetMailSentMessage'), duration: 8000, showClose: true })
              this.dataForm.email = ''
              this.loading = false
            })
            .catch(() => {
              this.loading = false
            })
        } else {
          return false
        }
      })
    },
    handleSetTenant() {
      this.$refs['SwitchTenantDialog'].tenantDialogFormVisible = true
    },
    getTenantName(name) {
      // TODO: 优化 计算属性从 getters中获取
      this.currentTenantName = name
    }
  }
}
</script>

<style lang="scss">
@import "src/views/account/styles/account-global.scss";
</style>

<style lang="scss" scoped>
@import "src/views/account/styles/account.scss";
</style>
