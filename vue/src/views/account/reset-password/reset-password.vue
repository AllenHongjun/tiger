<template>
  <div class="register-container">
    <div class="title-container">
      <h3 class="title">
        {{ $t('AbpAccount.ResetPassword') }}
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

    <el-form ref="dataForm" :model="dataForm" :rules="Rules" class="data-form" autocomplete="on" label-position="left">
      <el-form-item prop="password">
        <span class="svg-container">
          <svg-icon icon-class="password" />
        </span>
        <el-input :key="passwordType" ref="password" v-model="dataForm.password" :type="passwordType" :placeholder="$t('AbpAccount[\'DisplayName:NewPassword\']')" name="password" tabindex="3" autocomplete="on" />
        <span class="show-pwd" @click="showPwd">
          <svg-icon :icon-class="passwordType === 'password' ? 'eye' : 'eye-open'" />
        </span>
      </el-form-item>
      <el-form-item prop="comfirmPassword">
        <span class="svg-container">
          <svg-icon icon-class="password" />
        </span>
        <el-input :key="passwordType" ref="comfirmPassword" v-model="dataForm.comfirmPassword" :type="passwordType" :placeholder="$t('AbpAccount[\'DisplayName:NewPasswordConfirm\']')" name="password" tabindex="3" autocomplete="on" />
        <span class="show-pwd" @click="showPwd">
          <svg-icon :icon-class="passwordType === 'password' ? 'eye' : 'eye-open'" />
        </span>
      </el-form-item>
      <el-button :loading="loading" type="primary" style="width:100%;" @click.native.prevent="handleResetPassword">
        {{ $t('AbpUi.Submit') }}
      </el-button>

    </el-form>

    <div class="bottom-container">
      <el-link href="#/login" type="primary">{{ $t("AbpAccount['Cancel']") }}</el-link>
    </div>

    <switch-tenant ref="SwitchTenantDialog" @setTenantName="getTenantName" />
  </div>
</template>

<script>
import {
  resetPassword
} from '@/api/account'

import LangSelect from '@/components/LangSelect/index.vue'
import SwitchTenant from '../components/SwitchTenant.vue'

export default {
  name: 'Register',
  components: {
    LangSelect,
    SwitchTenant
  },
  data() {
    var avalidatePass = (rule, value, callback) => {
      if (value === '') {
        callback(new Error(
          this.$i18n.t("AbpAccount['ThisFieldIsNotValid.']")
        ))
      } else if (value !== this.dataForm.password) {
        callback(new Error(
          this.$i18n.t("AbpIdentity['Volo.Abp.Identity:PasswordConfirmationFailed']")
        ))
      } else {
        callback()
      }
    }
    return {
      dataForm: {
        password: '',
        comfirmPassword: '', // 确认密码
        userId: '',
        tenantId: '', // TODO: 根据返回链接中的tenantId参数设置租户
        resetToken: '',
        returnUrl: '',
        returnUrlHash: ''
      },

      Rules: {
        password: [
          {
            required: true,
            message: this.$i18n.t("AbpAccount['ThisFieldIsRequired.']"),
            trigger: ['blur', 'change']
          }
        ],
        comfirmPassword: [
          {
            required: true,
            message: this.$i18n.t("AbpAccount['ThisFieldIsRequired.']"),
            trigger: ['blur', 'change']
          },
          {
            validator: avalidatePass,
            trigger: ['blur', 'change']
          }
        ]
      },
      loading: false,
      passwordType: 'password',
      currentTenantName: ''
    }
  },
  computed: {
    // currentTenant() {
    //   return this.$store.getters.abpConfig.currentTenant.name
    // }
  },

  mounted() {},
  destroyed() { },
  created() {
    var query = this.$route.query
    console.log(query)
    if (query) {
      this.userId = query.userId
      this.resetToken = query.resetToken
      this.returnUrl = query.returnUrl
      this.returnUrlHash = query.returnUrlHash
    }
  },
  methods: {
    showPwd() {
      if (this.passwordType === 'password') {
        this.passwordType = ''
      } else {
        this.passwordType = 'password'
      }
      this.$nextTick(() => {
        this.$refs.password.focus()
      })
    },
    navitoLogin() {
      this.$router.push({
        path: '/login'
      })
    },
    handleResetPassword() {
      this.$refs.dataForm.validate(valid => {
        if (valid) {
          // 获取请求url当中的参数 来重置密码
          var req = {
            userId: this.userId,
            resetToken: this.resetToken,
            password: this.dataForm.password,
            returnUrl: this.returnUrl,
            returnUrlHash: this.returnUrlHash
          }
          this.loading = true
          resetPassword(req)
            .then(res => {
              debugger
              this.$router.push({
                path: '/login'
              })
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
