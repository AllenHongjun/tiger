<template>
  <div class="app-container">
    <!-- <el-form ref="ruleForm" :model="ruleForm" :rules="rules" label-width="160px" class="demo-ruleForm" /> -->
    <el-form-item prop="clientUri">
      <span slot="label">
        <el-tooltip content="Application URL that will be seen on consent screens" placement="top">
          <i class="el-icon-question" />
        </el-tooltip>
        {{ $t('AbpIdentityServer[\'Client:ClientUri\']') }}
      </span>
      <el-input v-model="ruleForm.clientUri" />
    </el-form-item>
    <el-form-item prop="logoUri">
      <span slot="label">
        <el-tooltip content="Application logo that will be seen on consent screens. These protocols rely upon TLS in production" placement="top">
          <i class="el-icon-question" />
        </el-tooltip>
        {{ $t('AbpIdentityServer[\'Client:LogoUri\']') }}
      </span>
      <el-input v-model="ruleForm.logoUri" />
    </el-form-item>
    <el-form-item prop="requireConsent">
      <span slot="label">
        <el-tooltip content="Specifies if a consent screen is required" placement="top">
          <i class="el-icon-question" />
        </el-tooltip>
        {{ $t('AbpIdentityServer[\'Client:RequireConsent\']') }}
      </span>
      <el-switch v-model="ruleForm.requireConsent" />
    </el-form-item>
    <el-form-item prop="allowRememberConsent">
      <span slot="label">
        <el-tooltip content="Specifies if the consent screen is remembered after consent is given" placement="top">
          <i class="el-icon-question" />
        </el-tooltip>
        {{ $t('AbpIdentityServer[\'Client:AllowRememberConsent\']') }}
      </span>
      <el-switch v-model="ruleForm.allowRememberConsent" :disabled="!ruleForm.requireConsent" />
    </el-form-item>
    <el-form-item prop="consentLifetime" :inline="true">
      <span slot="label">
        <el-tooltip content="Lifetime in seconds" placement="top">
          <i class="el-icon-question" />
        </el-tooltip>
        {{ $t('AbpIdentityServer[\'Client:ConsentLifetime\']') }}
      </span>
      <el-input v-model="ruleForm.consentLifetime" type="number" :disabled="!ruleForm.requireConsent" style="width:300px;" />
      <el-button-group style="margin-left:10px;vertical-align: top;">
        <el-button type="primary" plain @click="setConsentLifetime(15)">15分钟</el-button>
        <el-button type="primary" plain @click="setConsentLifetime(30)">30分钟</el-button>
        <el-button type="primary" plain @click="setConsentLifetime(60)">1小时</el-button>
        <el-button type="primary" plain @click="setConsentLifetime(300)">5小时</el-button>
      </el-button-group>
    </el-form-item>
  </div>
</template>

<script>
export default {
  name: 'ClientConsent',
  props: {
    ruleForm: {
      type: Object,
      require: false,
      // 对象或数组默认值必须从一个工厂函数获取
      default: function() {
        return {
          clientUri: undefined,
          logoUri: undefined,
          requireConsent: false,
          allowRememberConsent: true,
          consentLifetime: undefined
        }
      }
    }
  },
  data() {
    return {
      rules: {
        clientUri: [
          {
            max: 2000,
            message: this.$i18n.t(
              "AbpValidation['The field {0} must be a string with a maximum length of {1}.']",
              [this.$i18n.t("AbpIdentityServer['Client:ClientUri']"), '2000']
            ),
            trigger: 'blur'
          }
        ],
        logoUri: [
          {
            max: 2000,
            message: this.$i18n.t(
              "AbpValidation['The field {0} must be a string with a maximum length of {1}.']",
              [this.$i18n.t("AbpIdentityServer['Client:ClientUri']"), '2000']
            ),
            trigger: 'blur'
          }
        ]
      }
    }
  },
  watch: {
    ruleForm(newValue, oldValue) {
      // ...
      this.$emit('set-form-data', newValue)
    }
  },
  created() {
    // 将子组件的表单验证规则传递给父组件
    this.$emit('set-form-rules', this.rules)
  },
  methods: {
    setConsentLifetime(minitues) {
      this.ruleForm.consentLifetime = minitues * 60
    },
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
    },
    onSubmit() {
      this.$message('submit!')
    },
    onCancel() {
      this.$message({
        message: 'cancel!',
        type: 'warning'
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

