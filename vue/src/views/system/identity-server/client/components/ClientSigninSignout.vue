<template>
  <div class="app-container">
    <el-form ref="ruleForm" :model="ruleForm" :rules="rules" label-width="300px" class="demo-ruleForm">
      <el-form-item label="Front Channel Logout URI" prop="frontChannelLogoutUri">
        <span slot="label">
          <el-tooltip content="Endpoint IdentityServer will call in a browser iframe when single sign-out is triggered" placement="top">
            <i class="el-icon-question" />
          </el-tooltip>
          {{ $t('AbpIdentityServer[\'Client:FrontChannelLogoutUri\']') }}
        </span>
        <el-input v-model="ruleForm.frontChannelLogoutUri" />
      </el-form-item>
      <el-form-item label="Front Channel Logout Session Required" prop="frontChannelLogoutSessionRequired">
        <span slot="label">
          <el-tooltip content="Enable to send the session ID during single sign-out" placement="top">
            <i class="el-icon-question" />
          </el-tooltip>
          {{ $t('AbpIdentityServer[\'Client:FrontChannelLogoutSessionRequired\']') }}
        </span>
        <el-switch v-model="ruleForm.frontChannelLogoutSessionRequired" />
      </el-form-item>
      <el-form-item label="Back Channel Logout URI" prop="backChannelLogoutUri">
        <span slot="label">
          <el-tooltip content="Endpoint IdentityServer will call via HTTP when single sign-out is triggered" placement="top">
            <i class="el-icon-question" />
          </el-tooltip>
          {{ $t('AbpIdentityServer[\'Client:BackChannelLogoutUri\']') }}
        </span>
        <el-input v-model="ruleForm.backChannelLogoutUri" />
      </el-form-item>
      <el-form-item label="Back Channel Logout Session Required" prop="backChannelLogoutSessionRequired">
        <span slot="label">
          <el-tooltip content="Enable to send the session ID during single sign-out" placement="top">
            <i class="el-icon-question" />
          </el-tooltip>
          {{ $t('AbpIdentityServer[\'Client:BackChannelLogoutSessionRequired\']') }}
        </span>
        <el-switch v-model="ruleForm.backChannelLogoutSessionRequired" />
      </el-form-item>

    </el-form>
  </div>
</template>

<script>
export default {
  name: 'ClientSigninSignout',
  props: {
    ruleForm: {
      type: Object,
      require: false,
      // 对象或数组默认值必须从一个工厂函数获取
      default: function() {
        return {
          frontChannelLogoutUri: '',
          frontChannelLogoutSessionRequired: false,
          backChannelLogoutUri: '',
          backChannelLogoutSessionRequired: false
        }
      }
    }
  },
  data() {
    return {
      rules: {
        frontChannelLogoutUri: [
          {
            max: 2000,
            message: this.$i18n.t(
              "AbpValidation['The field {0} must be a string with a maximum length of {1}.']",
              [this.$i18n.t("AbpIdentityServer['Client:FrontChannelLogoutUri']"), '2000']
            ),
            trigger: 'blur'
          }
        ],
        backChannelLogoutUri: [
          {
            max: 2000,
            message: this.$i18n.t(
              "AbpValidation['The field {0} must be a string with a maximum length of {1}.']",
              [this.$i18n.t("AbpIdentityServer['Client:BackChannelLogoutUri']"), '2000']
            ),
            trigger: 'blur'
          }
        ]
      }
    }
  },
  created() {
    // 将子组件的表单验证规则传递给父组件
    this.$emit('set-form-rules', this.rules)
  },
  methods: {
    submitForm(formName) {
      this.$refs[formName].validate((valid) => {
        if (valid) {
          alert('submit!')
        } else {
          console.log('error submit!!')
          return false
        }
      })
    },
    resetForm(formName) {
      this.$refs[formName].resetFields()
    }
  }
}
</script>

<style scoped>
.line{
  text-align: center;
}
</style>

