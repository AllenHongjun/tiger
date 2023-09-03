<template>
  <div class="app-container">
    <el-form ref="ruleForm" :model="ruleForm" :rules="rules" label-width="210px" class="demo-ruleForm">
      <el-form-item label="Allow Offline Access" prop="allowOfflineAccess">
        <span slot="label">
          <el-tooltip content="Enables refresh tokens" placement="top">
            <i class="el-icon-question" />
          </el-tooltip>
          Allow Offline Access
        </span>
        <el-switch v-model="ruleForm.allowOfflineAccess" />
      </el-form-item>
      <el-form-item label="Update Access Token Claims on Refresh" prop="updateAccessTokenClaimsOnRefresh">
        <span slot="label">
          <el-tooltip content="Update access tokens and its claims when using refresh tokens" placement="top">
            <i class="el-icon-question" />
          </el-tooltip>
          Update Access Token Claims on Refresh
        </span>
        <el-switch v-model="ruleForm.updateAccessTokenClaimsOnRefresh" :disabled="!ruleForm.allowOfflineAccess" />
      </el-form-item>
      <el-form-item label="Refresh Token Expiration" prop="refreshTokenExpiration">
        <span slot="label">
          <el-tooltip content="Refresh Token Expiration" placement="top">
            <i class="el-icon-question" />
          </el-tooltip>
          Refresh Token Expiration
        </span>
        <el-radio-group v-model="ruleForm.refreshTokenExpiration" :disabled="!ruleForm.allowOfflineAccess" @change="refreshTokenExpirationChange">
          <el-radio :label="0">Absolute</el-radio>
          <el-radio :label="1">Sliding</el-radio>
        </el-radio-group>
      </el-form-item>
      <el-form-item label="Absolute Refresh Token Lifetime" prop="absoluteRefreshTokenLifetime">
        <span slot="label">
          <el-tooltip content="Lifetime in seconds" placement="top">
            <i class="el-icon-question" />
          </el-tooltip>
          Absolute Refresh Token Lifetime
        </span>
        <el-input v-model="ruleForm.absoluteRefreshTokenLifetime" type="number" :disabled="!ruleForm.allowOfflineAccess || ruleForm.refreshTokenExpiration != 0" />
      </el-form-item>
      <el-form-item label="Sliding Refresh Token Lifetim" prop="slidingRefreshTokenLifetime">
        <span slot="label">
          <el-tooltip content="Lifetime in seconds" placement="top">
            <i class="el-icon-question" />
          </el-tooltip>
          Sliding Refresh Token Lifetim
        </span>
        <el-input v-model="ruleForm.slidingRefreshTokenLifetime" type="number" :disabled="!ruleForm.allowOfflineAccess || ruleForm.refreshTokenExpiration != 1" />
      </el-form-item>
      <el-form-item label="Refresh Token Usage" prop="refreshTokenExpiration">
        <span slot="label">
          <el-tooltip content="One time only is recommended for security best practice" placement="top">
            <i class="el-icon-question" />
          </el-tooltip>
          Refresh Token Usage
        </span>
        <el-radio-group v-model="ruleForm.refreshTokenUsage" :disabled="!ruleForm.allowOfflineAccess">
          <el-radio :label="0">One Time Only</el-radio>
          <el-radio :label="1">Reuse</el-radio>
        </el-radio-group>
      </el-form-item>

    </el-form>
  </div>
</template>

<script>
export default {
  name: 'ClientRefreshToken',
  data() {
    return {
      ruleForm: {

        allowOfflineAccess: false,
        updateAccessTokenClaimsOnRefresh: false,
        refreshTokenType: 'Absolute',
        refreshTokenExpiration: 0,
        absoluteRefreshTokenLifetime: 0,
        slidingRefreshTokenLifetime: 0,
        refreshTokenUsage: 1

      },
      rules: {

      }
    }
  },
  created() {
    console.log('ruleForm.refreshTokenExpiration', this.ruleForm.refreshTokenExpiration)
  },
  methods: {
    refreshTokenExpirationChange(value) {
      // console.log('refreshTokenExpirationChange', value)
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
    }
  }
}
</script>

<style scoped>
.line{
  text-align: center;
}
</style>

