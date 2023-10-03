<template>
  <div class="register-container">
    <el-form ref="dataForm" :model="dataForm" :rules="Rules" class="register-form" autocomplete="on" label-position="left">
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
        <!-- 增加取消按钮 返回登录 -->
        <el-row style="margin-top:25px;">
          <el-col :span="24">
            <el-link href="#/login" type="primary">{{ $t("AbpAccount['Cancel']") }}</el-link>
          </el-col>
        </el-row>
      </div>
    </el-form>

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
/* 修复input 背景不协调 和光标变色 */
/* Detail see https://github.com/PanJiaChen/vue-element-admin/pull/927 */

$bg: #283443;
$light_gray: #fff;
$cursor: #fff;

@supports (-webkit-mask: none) and (not (cater-color: $cursor)) {
    .register-container .register-form .el-input input {
        color: $cursor;
    }
}

/* reset element-ui css */
.register-container .register-form {
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

<style lang="scss" scoped>

$bg: #2d3a4b;
$dark_gray: #889aa4;
$light_gray: #eee;

.register-container {
    min-height: 100%;
    width: 100%;
    background-color: $bg;
    overflow: hidden;

    .register-form {
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

    .el-button--text {
        color: #606266;
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

    .title-container .el-button--text:hover {
        color: #1890ff;
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

    .thirdparty-button {
        bottom: 6px;
    }

    @media only screen and (max-width: 470px) {
        .thirdparty-button {
            display: none;
        }
    }
}
</style>
