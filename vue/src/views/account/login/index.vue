<template>
  <div class="app-container">
    <div class="title-container">
      <h3 class="title">{{ $t('AbpUi.AppName') }}</h3>
      <lang-select class="set-language" />
      <h5>{{ $t('AbpUi.AppAbout') }}</h5>

    </div>
    <el-tabs v-model="activeName" class="data-tab" @tab-click="handleClick">
      <el-tab-pane :label="$t('AbpAccount[\'PasswordLogin\']')" name="pwd-login">
        <el-form ref="loginForm" :model="loginForm" :rules="loginRules" class="data-form" auto-complete="off" label-position="left">

          <el-form-item prop="tenent">
            <span class="svg-container">
              <svg-icon icon-class="international" />
            </span>
            <el-input v-model="tenant.name" :placeholder="$t('AbpUiMultiTenancy[\'NotSelected\']')" name="tenent" type="text" tabindex="1" auto-complete="off" />
            <el-button type="info" size="mini" class="switchBth" @click="dialogVisible = true">{{ $t('AbpUiMultiTenancy[\'Switch\']') }}</el-button>
          </el-form-item>

          <el-form-item prop="username">
            <span class="svg-container">
              <svg-icon icon-class="user" />
            </span>
            <el-input ref="username" v-model="loginForm.username" :placeholder="$t('AbpAccount[\'UserNamePlaceholder\']')" name="username" type="text" tabindex="1" auto-complete="off" />
          </el-form-item>

          <el-form-item prop="password">
            <span class="svg-container">
              <svg-icon icon-class="password" />
            </span>
            <el-input :key="passwordType" ref="password" v-model="loginForm.password" :type="passwordType" :placeholder="$t('AbpAccount[\'Password\']')" name="password" tabindex="2" auto-complete="new-password" @keyup.enter.native="handleLogin" />
            <span class="show-pwd" @click="showPwd">
              <svg-icon :icon-class="passwordType === 'password' ? 'eye' : 'eye-open'" />
            </span>
          </el-form-item>

          <el-button :loading="loading" type="primary" style="width:100%;margin-bottom:30px;" @click.native.prevent="handleLogin">{{ $t("AbpAccount['Login']") }}</el-button>

        </el-form>
      </el-tab-pane>
      <el-tab-pane :label="$t('AbpAccount[\'SmsLogin\']')" name="sms-login">
        <el-form ref="smsLoginForm" :model="smsLoginForm" :rules="smsLoginRules" class="data-form" auto-complete="off" label-position="left">
          <el-form-item prop="tenent">
            <span class="svg-container">
              <svg-icon icon-class="international" />
            </span>
            <el-input v-model="tenant.name" :placeholder="$t('AbpUiMultiTenancy[\'NotSelected\']')" name="tenent" type="text" tabindex="1" auto-complete="off" />
            <el-button type="info" size="mini" class="switchBth" @click="dialogVisible = true">{{ $t('AbpUiMultiTenancy[\'Switch\']') }}</el-button>
          </el-form-item>

          <el-form-item prop="phone">
            <span class="svg-container">
              <svg-icon icon-class="user" />
            </span>
            <el-input ref="phone" v-model="smsLoginForm.phone" :placeholder="$t('AbpAccount[\'DisplayName:PhoneNumber\']')" name="phone" type="text" tabindex="1" auto-complete="off" />
          </el-form-item>

          <el-form-item prop="code">
            <span class="svg-container">
              <svg-icon icon-class="password" />
            </span>
            <el-input ref="code" v-model="smsLoginForm.code" type="text" :placeholder="$t('AbpAccount[\'DisplayName:SmsVerifyCode\']')" name="code" class="el-input-code" />
            <el-button :disabled="codeDisabled" type="primary" class="switchBth" @click="handelSendSmsCode">
              {{ timer != null ? codeMsg : $t("AbpAccount['SendVerifyCode']") }}
            </el-button>

          </el-form-item>

          <el-button :loading="loading" type="primary" style="width:100%;margin-bottom:30px;" @click.native.prevent="handleSmsLogin">{{ $t("AbpAccount['Login']") }}</el-button>
        </el-form>
      </el-tab-pane>
    </el-tabs>

    <div class="bottom-container">
      <!-- <div style="text-align: right;">
        <el-button class="thirdparty-button" type="primary" size="small" @click="showDialog = true">
          第三方登录
        </el-button>
      </div> -->
      <el-row>
        <el-col :span="24">
          <el-link href="#/register" type="primary">{{ $t("AbpAccount['Register']") }}</el-link>
          <el-link href="#/send-reset-password-link" type="primary">{{ $t("AbpAccount['ForgotPassword']") }}</el-link>
          <!-- <el-link href="#/reset-password" type="primary">{{ $t("AbpAccount['ResetPassword']") }}</el-link> -->
        </el-col>
      </el-row>

      <el-row style="margin-top:10px;">
        <el-col>
          <div class="tips">
            <span style="margin-right:20px;">默认账号: admin</span>
            <span>默认密码: 1q2w3E*</span>
          </div>
        </el-col>

      </el-row>
    </div>

    <el-dialog :title="$t('AbpUiMultiTenancy[\'SwitchTenant\']')" :visible.sync="dialogVisible" width="30%">
      <el-form ref="dataForm" :model="tenant" label-width="80px" label-position="top">
        <el-form-item :label="$t('AbpUiMultiTenancy[\'Name\']')">
          <el-input v-model="tenant.name" type="text" />
        </el-form-item>
        <span>{{ $t("AbpUiMultiTenancy['SwitchTenantHint']") }}</span>
      </el-form>

      <span slot="footer" class="dialog-footer">
        <el-button @click="dialogVisible = false">{{ $t("AbpUi['Cancel']") }}</el-button>
        <el-button type="primary" :disabled="tenantDisabled" @click="handleSetTenant()"> {{ $t("AbpUi['Save']") }}</el-button>
      </span>
    </el-dialog>

    <el-dialog title="第三方登录" :visible.sync="showDialog">
      提示
      <br>
      <br>
      <br>
      <social-sign />
    </el-dialog>

  </div>
</template>

<script>
import {
  getTenantByName
} from '@/api/user'
import {
  SendSignCode
} from '@/api/account'
import { validPhone } from '@/utils/validate'
import LangSelect from '@/components/LangSelect'
import SocialSign from './components/SocialSignin'

export default {
  name: 'Login',
  components: {
    LangSelect,
    SocialSign
  },
  data() {
    return {
      activeName: 'pwd-login',
      dialogVisible: false,
      loginForm: {
        username: '',
        password: ''
      },
      loginRules: {
        username: [{
          required: true,
          message: this.$i18n.t("AbpAccount['ThisFieldIsRequired.']"),
          trigger: ['blur', 'change']
        }],
        password: [{
          required: true,
          message: this.$i18n.t("AbpAccount['The {0} field is required.']", [this.$i18n.t("AbpAccount['Password']")]),
          trigger: ['blur', 'change']
        }]
      },
      loading: false,
      passwordType: 'password',
      redirect: undefined,
      tenant: {
        name: this.$store.getters.abpConfig.currentTenant.name
      },
      showDialog: false,

      smsLoginForm: {
        phone: '', // 手机号
        code: '' // 短信验证码
      },
      smsLoginRules: {
        phone: [{
          required: true,
          message: this.$i18n.t("AbpAccount['The {0} field is required.']", [this.$i18n.t("AbpAccount['DisplayName:PhoneNumber']")]),
          trigger: ['blur', 'change']
        }],
        code: [{
          required: true,
          message: this.$i18n.t("AbpAccount['The {0} field is required.']", [this.$i18n.t("AbpAccount['DisplayName:SmsVerifyCode']")]),
          trigger: ['blur', 'change']
        }]
      },
      // 是否禁用按钮
      codeDisabled: false,
      // 倒计时秒数
      countdown: 60,
      // 按钮上的文字
      codeMsg: this.$i18n.t("AbpAccount['SendVerifyCode']"),
      // 定时器
      timer: null

    }
  },
  computed: {
    currentTenant() {
      return this.$store.getters.abpConfig.currentTenant.name
    },
    features() {
      return this.$store.getters.abpConfig.features.values
    },
    tenantDisabled() {
      if (this.tenant.name && this.tenant.name === this.$store.getters.abpConfig.currentTenant.name
      ) {
        return true
      }
      return false
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
  created() {
    console.log(this.tenant.name)
  },
  methods: {
    handleClick(tab, event) {
      console.log(tab, event)
    },
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

    // 用户登录
    handleLogin() {
      this.$refs.loginForm.validate(valid => {
        if (valid) {
          this.loading = true

          this.$store.dispatch('user/login', this.loginForm)
            .then(() => {
              // TODO: 增加调用用户登录接口
              this.$router.push({
                path: this.redirect || '/',
                query: this.otherQuery
              })
              this.loading = false
            }).catch(() => {
              console.log('login err')
              this.loading = false
            })
        } else {
          console.log('error submit!!')
          return false
        }
      })
    },
    // 执行倒计时
    execCountdown() {
      if (this.countdown > 0 && this.countdown <= 60) {
        this.countdown--
        if (this.countdown !== 0) {
          this.codeMsg = this.$i18n.t("AbpAccount['ReSendVerifyCode']") + '(' + this.countdown + ')'
          this.codeDisabled = true
        } else {
          clearInterval(this.timer)
          this.codeMsg = this.$i18n.t("AbpAccount['SendVerifyCode']")
          this.countdown = 60
          this.timer = null
          this.codeDisabled = false
        }
      }
    },
    // 发送短信验证码
    handelSendSmsCode() {
      if (!this.smsLoginForm.phone) {
        this.$message({
          message: this.$i18n.t("AbpAccount['The {0} field is required.']", [this.$i18n.t("AbpAccount['DisplayName:PhoneNumber']")]),
          type: 'error',
          duration: 2000
        })
        return
      }

      if (!validPhone(this.smsLoginForm.phone)) {
        this.$message({
          message: this.$i18n.t("AbpAccount['The {0} field is not a valid phone number.']", [this.$i18n.t("AbpAccount['DisplayName:PhoneNumber']")]),
          type: 'error',
          duration: 2000
        })
        return
      }

      var input = {
        phoneNumber: this.smsLoginForm.phone
      }
      SendSignCode(input).then((response) => {
        // 验证码60秒倒计时
        if (!this.timer) {
          this.execCountdown()
          this.timer = setInterval(this.execCountdown, 1000)
        }
        this.$notify({
          title: this.$i18n.t("TigerUi['Success']"),
          message: this.$i18n.t("TigerUi['SuccessMessage']"),
          type: 'success',
          duration: 2000
        })
      })
    },
    handleSmsLogin() {
      this.$refs.smsLoginForm.validate(valid => {
        if (valid) {
          this.loading = true
          this.$store.dispatch('user/smsLogin', this.smsLoginForm)
            .then(() => {
              this.$router.push({
                path: this.redirect || '/',
                query: this.otherQuery
              })
              this.loading = false
            }).catch(() => {
              console.log('login err')
              this.loading = false
            })
        } else {
          console.log('error submit!!')
          return false
        }
      })
    },
    getOtherQuery(query) {
      return Object.keys(query).reduce((acc, cur) => {
        if (cur !== 'redirect') {
          acc[cur] = query[cur]
        }
        return acc
      }, {})
    },
    handleClose(done) {
      this.$confirm('确认关闭？')
        .then(_ => {
          done()
        })
        .catch(_ => {})
    },

    // 切换租户 作废
    handleSwichTenant() {
      if (!this.tenant) {
        this.dialogVisible = false
        return
      }
      getTenantByName(this.tenant).then((response) => {
        if (response.success) {
          // 请求头设置租户信息
          this.dialogVisible = false
          this.$notify({
            title: this.$i18n.t("TigerUi['Success']"),
            message: this.$i18n.t("TigerUi['SuccessMessage']"),
            type: 'success',
            duration: 2000
          })
        } else {
          this.dialogVisible = true
          this.$notify({
            title: '失败',
            message: '租户信息获取失败',
            type: 'fail',
            duration: 2000
          })
        }
      })
    },

    // 设置当前租户
    handleSetTenant() {
      debugger
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
        this.dialogVisible = false
      })
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
