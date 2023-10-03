<template>
  <div class="login-container">
    <el-form ref="dataForm" :model="dataForm" :rules="loginRules" class="login-form" auto-complete="on" label-position="left">
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

        <el-row>
          <el-col :span="12">
            <el-link href="#/login" type="primary">{{ $t('AbpAccount.BackToLogin') }}</el-link>
          </el-col>
        </el-row>
      </div>
    </el-form>
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
/* 修复input 背景不协调 和光标变色 */
/* Detail see https://github.com/PanJiaChen/vue-element-admin/pull/927 */

$bg:#283443;
$light_gray:#fff;
$cursor: #fff;

@supports (-webkit-mask: none) and (not (cater-color: $cursor)) {
    .login-container .login-form .el-input input {
        color: $cursor;
    }
}

/* reset element-ui css */
.login-form {
    .el-input {
        display: inline-block;
        height: 47px;
        width: 85%;

        input {
            background: transparent;
            border: 0px;
            -webkit-appearance: none;
            border-radius: 0px;
            padding: 12px 5px 12px 15px;
            color: $light_gray;
            height: 47px;
            caret-color: $cursor;

            &:-webkit-autofill {
                box-shadow: 0 0 0px 1000px $bg inset !important;
                -webkit-text-fill-color: $cursor !important;
            }
        }
    }

    .el-form-item {
        border: 1px solid rgba(255, 255, 255, 0.1);
        background: rgba(0, 0, 0, 0.1);
        border-radius: 5px;
        color: #454545;
    }
}
</style>

<!--TODO: 账号管理的页面分装一个统一的样式引入 https://blog.csdn.net/weixin_53072519/article/details/120526935 -->

<style lang="scss" scoped>
$bg:#2d3a4b;
$dark_gray:#889aa4;
$light_gray:#eee;

.login-container {
    min-height: 100%;
    width: 100%;
    background-color: $bg;
    overflow: hidden;

    .login-form {
        position: relative;
        width: 520px;
        max-width: 100%;
        padding: 160px 35px 0;
        margin: 0 auto;
        overflow: hidden;

        .explain {
          color: #fff;
          font-size: 14px;
          padding-right: 15px;
      }
    }

    .tips {
        font-size: 14px;
        color: #fff;
        margin-bottom: 10px;

        span {
            &:first-of-type {
                margin-right: 16px;
            }
        }
    }

    .svg-container {
        padding: 6px 5px 6px 15px;
        color: $dark_gray;
        vertical-align: middle;
        width: 30px;
        display: inline-block;
    }

    .title-container {
        position: relative;

        .title {
            font-size: 26px;
            color: $light_gray;
            margin: 0px auto 40px auto;
            text-align: center;
            font-weight: bold;
        }

        h5 {
            color: #fff;
            font-size: 16px;

            .el-button {
                font-size: 16px;
            }
        }

      .set-language {
          color: #fff;
          position: absolute;
          top: 3px;
          font-size: 18px;
          right: 0px;
          cursor: pointer;
      }
    }

    .show-pwd {
        position: absolute;
        right: 10px;
        top: 7px;
        font-size: 16px;
        color: $dark_gray;
        cursor: pointer;
        user-select: none;
    }

    .switchBth {
        position: absolute;
        right: 10px;
        top: 7px;
        font-size: 16px;
    }

    .tenantInput {
        background-color: #2d3a4b;
        line-height: 40px;
        // font-size:20px;
    }
}
</style>
