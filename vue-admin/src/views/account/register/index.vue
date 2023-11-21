<template>
  <div class="app-container">
    <div class="title-container">
      <h3 class="title">
        {{ $t('AbpUi["Register"]') }}
        <lang-select class="set-language" />
      </h3>
      <p class="explain">
        {{ $t("AbpUiMultiTenancy['Tenant']") }}
        <el-tooltip :content="$t('AbpUiMultiTenancy[\'Switch\']')" effect="dark" placement="bottom">
          <el-link :underline="false" @click="handleSetTenant()"><i>{{
            currentTenant
              ? currentTenant
              : $t("AbpUiMultiTenancy['NotSelected']")
          }}</i></el-link>
        </el-tooltip>
      </p>
    </div>

    <el-form ref="registerForm" :model="registerForm" :rules="registerRules" class="data-form" autocomplete="on" label-position="left">
      <el-form-item prop="username">
        <span class="svg-container">
          <svg-icon icon-class="user" />
        </span>
        <el-input ref="username" v-model="registerForm.username" :placeholder="$t('AbpAccount[\'UserName\']')" name="username" type="text" tabindex="1" autocomplete="on" />
      </el-form-item>
      <el-form-item prop="emailAddress">
        <span class="svg-container">
          <svg-icon icon-class="email" />
        </span>
        <el-input ref="email" v-model="registerForm.emailAddress" :placeholder="$t('AbpAccount[\'EmailAddress\']')" name="password" tabindex="2" autocomplete="on" />
      </el-form-item>
      <el-form-item prop="password">
        <span class="svg-container">
          <svg-icon icon-class="password" />
        </span>
        <el-input :key="passwordType" ref="password" v-model="registerForm.password" :type="passwordType" :placeholder="$t('AbpAccount[\'Password\']')" name="password" tabindex="3" autocomplete="on" @keyup.enter.native="handleRegiter" />
        <span class="show-pwd" @click="showPwd">
          <svg-icon :icon-class="passwordType === 'password' ? 'eye' : 'eye-open'" />
        </span>
      </el-form-item>
      <el-button :loading="loading" type="primary" style="width:100%;" @click.native.prevent="handleRegiter">
        {{ $t('AbpUi["Register"]') }}
      </el-button>

    </el-form>

    <div class="bottom-container">
      <p class="explain">
        {{ $t("AbpAccount['AlreadyRegistered']") }}
        <el-link :underline="false" @click="navitoLogin()"><i>{{ $t("AbpAccount['Login']") }}</i></el-link>
      </p>
    </div>

    <switch-tenant ref="SwitchTenantDialog" />
  </div>
</template>

<script>
import {
  register
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
    return {
      registerForm: {
        username: '',
        password: '',
        emailAddress: '',
        appName: 'test1' // 默认注册方法 后端这个字段未使用
      },
      registerRules: {
        username: [
          {
            required: true,
            message: this.$i18n.t("AbpIdentity['The {0} field is required.']", [this.$i18n.t("AbpAccount['UserName']")]),
            trigger: 'blur'
          },
          {
            max: 256,
            message: this.$i18n.t("AbpIdentity['The field {0} must be a string with a maximum length of {1}.']", [this.$i18n.t("AbpAccount['UserName']"), '256']),
            trigger: 'blur'
          }
        ],
        password: [
          {
            required: true,
            message: this.$i18n.t("AbpIdentity['The {0} field is required.']", [this.$i18n.t("AbpAccount['Password']")]),
            trigger: ['blur', 'change']
          }
        ],
        emailAddress: [
          {
            required: true,
            message: this.$i18n.t("AbpIdentity['The {0} field is required.']", [this.$i18n.t("AbpAccount['EmailAddress']")]),
            trigger: 'blur'
          },
          {
            type: 'email',
            message: this.$i18n.t("AbpIdentity['The {0} field is not a valid e-mail address.']", [this.$i18n.t("AbpAccount['EmailAddress']")]),
            trigger: ['blur', 'change']
          },
          {
            max: 256,
            message: this.$i18n.t(
              "AbpIdentity['The field {0} must be a string with a maximum length of {1}.']",
              [this.$i18n.t("AbpAccount['EmailAddress']"), '256']
            ),
            trigger: 'blur'
          }
        ]
      },
      loading: false,
      passwordType: 'password',
      tenantDialogFormVisible: false,
      tenant: {
        name: this.$store.getters.abpConfig.currentTenant.name
      }
    }
  },
  computed: {
    currentTenant() {
      return this.$store.getters.abpConfig.currentTenant.name
    }
  },
  mounted() {},
  destroyed() {},
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
    handleRegiter() {
      this.$refs.registerForm.validate(valid => {
        if (valid) {
          this.loading = true
          register(this.registerForm)
            .then(res => {
              // 注册成功跳转登录页面
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
