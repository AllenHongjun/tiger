<template>
  <div class="app-container">
    <el-form ref="ruleForm" :model="ruleForm" :rules="rules" label-width="300px" class="demo-ruleForm">
      <el-form-item label="Front Channel Logout URI" prop="frontChannelLogoutUri">
        <span slot="label">
          <el-tooltip content="Endpoint IdentityServer will call in a browser iframe when single sign-out is triggered" placement="top">
            <i class="el-icon-question" />
          </el-tooltip>
          Front Channel Logout URI
        </span>
        <el-input v-model="ruleForm.frontChannelLogoutUri" />
      </el-form-item>
      <el-form-item label="Front Channel Logout Session Required" prop="frontChannelLogoutSessionRequired">
        <span slot="label">
          <el-tooltip content="Enable to send the session ID during single sign-out" placement="top">
            <i class="el-icon-question" />
          </el-tooltip>
          Front Channel Logout Session Required
        </span>
        <el-switch v-model="ruleForm.frontChannelLogoutSessionRequired" />
      </el-form-item>
      <el-form-item label="Back Channel Logout URI" prop="backChannelLogoutUri">
        <span slot="label">
          <el-tooltip content="Endpoint IdentityServer will call via HTTP when single sign-out is triggered" placement="top">
            <i class="el-icon-question" />
          </el-tooltip>
          Back Channel Logout URI
        </span>
        <el-input v-model="ruleForm.backChannelLogoutUri" />
      </el-form-item>
      <el-form-item label="Back Channel Logout Session Required" prop="backChannelLogoutSessionRequired">
        <span slot="label">
          <el-tooltip content="Enable to send the session ID during single sign-out" placement="top">
            <i class="el-icon-question" />
          </el-tooltip>
          Back Channel Logout Session Required
        </span>
        <el-switch v-model="ruleForm.backChannelLogoutSessionRequired" />
      </el-form-item>

    </el-form>
  </div>
</template>

<script>
export default {
  name: 'ClientSigninSignout',
  data() {
    return {
      ruleForm: {
        frontChannelLogoutUri: '',
        frontChannelLogoutSessionRequired: false,
        backChannelLogoutUri: '',
        backChannelLogoutSessionRequired: false
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
    }
  }
}
</script>

<style scoped>
.line{
  text-align: center;
}
</style>

