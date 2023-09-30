<template>
  <div class="app-container">
    <el-form ref="ruleForm" :model="ruleForm" :rules="rules" label-width="210px" class="demo-ruleForm">
      <el-form-item label="Allow Offline Access" prop="allowOfflineAccess">
        <span slot="label">
          <el-tooltip content="Enables refresh tokens" placement="top">
            <i class="el-icon-question" />
          </el-tooltip>
          {{ $t('AbpIdentityServer[\'Client:AllowedOfflineAccess\']') }}
        </span>
        <el-switch v-model="ruleForm.allowOfflineAccess" />
      </el-form-item>
      <el-form-item label="Update Access Token Claims on Refresh" prop="updateAccessTokenClaimsOnRefresh">
        <span slot="label">
          <el-tooltip content="Update access tokens and its claims when using refresh tokens" placement="top">
            <i class="el-icon-question" />
          </el-tooltip>
          {{ $t('AbpIdentityServer[\'Client:UpdateAccessTokenClaimsOnRefresh\']') }}
        </span>
        <el-switch v-model="ruleForm.updateAccessTokenClaimsOnRefresh" :disabled="!ruleForm.allowOfflineAccess" />
      </el-form-item>
      <el-form-item label="Refresh Token Expiration" prop="refreshTokenExpiration">
        <span slot="label">
          <el-tooltip content="Refresh Token Expiration" placement="top">
            <i class="el-icon-question" />
          </el-tooltip>
          {{ $t('AbpIdentityServer[\'Client:RefreshTokenExpiration\']') }}
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
          {{ $t('AbpIdentityServer[\'Client:AbsoluteRefreshTokenLifetime\']') }}
        </span>
        <el-input v-model="ruleForm.absoluteRefreshTokenLifetime" type="number" :disabled="!ruleForm.allowOfflineAccess || ruleForm.refreshTokenExpiration != 0" style="width:300px;" />
        <el-button-group style="margin-left:10px;vertical-align: top;">
          <el-button type="primary" plain @click="setAbsoluteRefreshTokenLifetime(15)">15分钟</el-button>
          <el-button type="primary" plain @click="setAbsoluteRefreshTokenLifetime(30)">30分钟</el-button>
          <el-button type="primary" plain @click="setAbsoluteRefreshTokenLifetime(60)">1小时</el-button>
          <el-button type="primary" plain @click="setAbsoluteRefreshTokenLifetime(300)">5小时</el-button>
        </el-button-group>
      </el-form-item>
      <el-form-item label="Sliding Refresh Token Lifetim" prop="slidingRefreshTokenLifetime">
        <span slot="label">
          <el-tooltip content="Lifetime in seconds" placement="top">
            <i class="el-icon-question" />
          </el-tooltip>
          {{ $t('AbpIdentityServer[\'Client:SlidingRefreshTokenLifetime\']') }}
        </span>
        <el-input v-model="ruleForm.slidingRefreshTokenLifetime" type="number" :disabled="!ruleForm.allowOfflineAccess || ruleForm.refreshTokenExpiration != 1" style="width:300px;" />
        <el-button-group style="margin-left:10px;vertical-align: top;">
          <el-button type="primary" plain @click="setSlidingRefreshTokenLifetime(15)">15分钟</el-button>
          <el-button type="primary" plain @click="setSlidingRefreshTokenLifetime(30)">30分钟</el-button>
          <el-button type="primary" plain @click="setSlidingRefreshTokenLifetime(60)">1小时</el-button>
          <el-button type="primary" plain @click="setSlidingRefreshTokenLifetime(300)">5小时</el-button>
        </el-button-group>
      </el-form-item>
      <el-form-item label="Refresh Token Usage" prop="refreshTokenExpiration">
        <span slot="label">
          <el-tooltip content="One time only is recommended for security best practice" placement="top">
            <i class="el-icon-question" />
          </el-tooltip>
          {{ $t('AbpIdentityServer[\'Client:RefreshTokenUsage\']') }}
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
  props: {
    ruleForm: {
      type: Object,
      require: false,
      // 对象或数组默认值必须从一个工厂函数获取
      default: function() {
        return {
          allowOfflineAccess: false,
          updateAccessTokenClaimsOnRefresh: false,
          refreshTokenType: 'Absolute',
          refreshTokenExpiration: 0,
          absoluteRefreshTokenLifetime: 0,
          slidingRefreshTokenLifetime: 0,
          refreshTokenUsage: 1
        }
      }
    }
  },
  data() {
    return {
      rules: {
      }
    }
  },
  created() {
    console.log('ruleForm.refreshTokenExpiration', this.ruleForm.refreshTokenExpiration)
  },
  methods: {
    setAbsoluteRefreshTokenLifetime(minitues) {
      this.ruleForm.absoluteRefreshTokenLifetime = minitues * 60
    },
    setSlidingRefreshTokenLifetime(minitues) {
      this.ruleForm.slidingRefreshTokenLifetime = minitues * 60
    },
    refreshTokenExpirationChange(e) {
      console.log(e)
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

