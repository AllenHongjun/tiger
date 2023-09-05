<template>
  <div class="app-container">
    <el-form ref="ruleForm" :model="ruleForm" :rules="rules" label-width="160px" class="demo-ruleForm">
      <el-form-item label="Display URL" prop="clientUri">
        <span slot="label">
          <el-tooltip content="Application URL that will be seen on consent screens" placement="top">
            <i class="el-icon-question" />
          </el-tooltip>
          Display URL
        </span>
        <el-input v-model="ruleForm.clientUri" />
      </el-form-item>
      <el-form-item label="Logo URL" prop="logoUri">
        <span slot="label">
          <el-tooltip content="Application logo that will be seen on consent screens. These protocols rely upon TLS in production" placement="top">
            <i class="el-icon-question" />
          </el-tooltip>
          Logo URL
        </span>
        <el-input v-model="ruleForm.logoUri" />
      </el-form-item>
      <el-form-item label="Require Consent" prop="requireConsent">
        <span slot="label">
          <el-tooltip content="Specifies if a consent screen is required" placement="top">
            <i class="el-icon-question" />
          </el-tooltip>
          Require Consent
        </span>
        <el-switch v-model="ruleForm.requireConsent" />
      </el-form-item>
      <el-form-item label="Remember Consent" prop="allowRememberConsent">
        <span slot="label">
          <el-tooltip content="Specifies if the consent screen is remembered after consent is given" placement="top">
            <i class="el-icon-question" />
          </el-tooltip>
          Remember Consent
        </span>
        <el-switch v-model="ruleForm.allowRememberConsent" :disabled="!ruleForm.requireConsent" />
      </el-form-item>
      <el-form-item label="Consent Token Lifetime" prop="consentLifetime">
        <span slot="label">
          <el-tooltip content="Lifetime in seconds" placement="top">
            <i class="el-icon-question" />
          </el-tooltip>
          Consent Token Lifetime
        </span>
        <el-input v-model="ruleForm.consentLifetime" type="number" :disabled="!ruleForm.requireConsent" />
      </el-form-item>

    </el-form>
  </div>
</template>

<script>
export default {
  name: 'ClientConsent',
  data() {
    return {
      ruleForm: {
        clientUri: undefined,
        logoUri: undefined,
        requireConsent: false,
        allowRememberConsent: true,
        consentLifetime: undefined
      },
      rules: {

      }
    }
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

