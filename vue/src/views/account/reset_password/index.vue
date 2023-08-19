<template>
  <div class="login-container">
    <el-form ref="dataForm" :model="dataForm" :rules="loginRules" class="login-form" auto-complete="on" label-position="left">

      <div class="title-container">
        <h3 class="title">密码重置</h3>
      </div>

      <el-form-item prop="email">
        <span class="svg-container">
          <svg-icon icon-class="email" />
        </span>
        <el-input ref="email" v-model="dataForm.email" placeholder="邮箱" name="email" type="text" tabindex="1" auto-complete="on" />
      </el-form-item>

      <el-button :loading="loading" type="primary" style="width:100%;margin-bottom:30px;" @click.native.prevent="handleSendPasswordResetCode">发送邮件</el-button>

      <el-row>
        <el-col :span="12">
          <el-link href="#/login" type="primary">返回 登录</el-link>
        </el-col>
      </el-row>

    </el-form>

  </div>
</template>

<script>
import {
  validEmail
} from '@/utils/validate'
import {
  sendPasswordResetCode
} from '@/api/account'

export default {
  name: 'ResetPassword',
  data() {
    const validateUsername = (rule, value, callback) => {
      if (!validEmail(value)) {
        callback(new Error('请输入正确的邮箱'))
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
      tenant: undefined
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
              this.$alert('帐户恢复电子邮件已发送到你的电子邮件地址.如果你在15分钟内未在收件箱中看到此电子邮件,请检查垃圾邮件,并标记为非垃圾邮件', '提醒')
              this.loading = false
            })
            .catch(() => {
              this.loading = false
            })
        } else {
          return false
        }
      })
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
