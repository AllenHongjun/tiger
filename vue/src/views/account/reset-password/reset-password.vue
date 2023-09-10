<template>
  <div class="register-container">
    <el-form ref="dataForm" :model="dataForm" :rules="registerRules" class="register-form" autocomplete="on" label-position="left">
      <div class="title-container">
        <h3 class="title">
          重置密码
          <lang-select class="set-language" />
        </h3>
        <p class="explain">
          {{ $t("AbpUiMultiTenancy['Tenant']") }}
          <el-tooltip :content="$t('AbpUiMultiTenancy[\'Switch\']')" effect="dark" placement="bottom">
            <el-link :underline="false" @click="tenantDialogFormVisible = true"><i>{{
              currentTenant
                ? currentTenant
                : $t("AbpUiMultiTenancy['NotSelected']")
            }}</i></el-link>
          </el-tooltip>
        </p>

        <el-form-item prop="password">
          <span class="svg-container">
            <svg-icon icon-class="password" />
          </span>
          <el-input :key="passwordType" ref="password" v-model="dataForm.password" :type="passwordType" placeholder="新密码" name="password" tabindex="3" autocomplete="on" />
          <span class="show-pwd" @click="showPwd">
            <svg-icon :icon-class="passwordType === 'password' ? 'eye' : 'eye-open'" />
          </span>
        </el-form-item>
        <el-form-item prop="comfirmPassword">
          <span class="svg-container">
            <svg-icon icon-class="password" />
          </span>
          <el-input :key="passwordType" ref="comfirmPassword" v-model="dataForm.comfirmPassword" :type="passwordType" placeholder="确认新密码" name="password" tabindex="3" autocomplete="on" />
          <span class="show-pwd" @click="showPwd">
            <svg-icon :icon-class="passwordType === 'password' ? 'eye' : 'eye-open'" />
          </span>
        </el-form-item>
        <el-button :loading="loading" type="primary" style="width:100%;" @click.native.prevent="handleResetPassword">
          提交
        </el-button>

      </div>
    </el-form>

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
        <el-button type="primary" :disabled="tenantDisabled" @click="saveTenant()">
          {{ $t("AbpTenantManagement['Save']") }}
        </el-button>
      </div>
    </el-dialog>
  </div>
</template>

<script>
import {
  register
} from '@/api/account'
import LangSelect from '@/components/LangSelect'
export default {
  name: 'Register',
  components: {
    LangSelect
  },
  data() {
    return {
      dataForm: {
        username: '',
        password: '',
        comfirmPassword: '', // 确认密码
        emailAddress: '',
        appName: 'test1' // 默认注册方法 后端这个字段未使用
      },
      registerRules: {

        password: [
          {
            required: true,
            message: this.$i18n.t("AbpAccount['ThisFieldIsRequired.']"),
            trigger: ['blur', 'change']
          }]

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
    },
    tenantDisabled() {
      if (
        this.tenant.name &&
                this.tenant.name === this.$store.getters.abpConfig.currentTenant.name
      ) {
        return true
      }
      return false
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
    handleResetPassword() {
      this.$refs.dataForm.validate(valid => {
        if (valid) {
          this.loading = true
          register(this.dataForm)
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
    saveTenant() {
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
        this.tenantDialogFormVisible = false
      })
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