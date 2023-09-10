<template>
  <div class="app-container">

    <el-form-item prop="accessTokenLifetime">
      <!-- 优化：直接显示能看到提示 -->
      <span slot="label">
        <el-tooltip content="Lifetime in seconds" placement="top">
          <i class="el-icon-question" />
        </el-tooltip>
        {{ $t('AbpIdentityServer[\'Client:AccessTokenLifetime\']') }}
      </span>
      <el-input v-model="ruleForm.accessTokenLifetime" type="number">
        <el-button slot="append" type="primary">15 分钟</el-button>
        <el-button slot="append" type="success">30 分钟</el-button>
        <el-button slot="append" type="primary">1 小时</el-button>
        <el-button slot="append" type="primary">5 小时</el-button>
      </el-input>

    </el-form-item>

    <el-form-item :label="$t('AbpIdentityServer[\'Client:AccessTokenType\']')" prop="AccessTokenType">
      <el-radio-group v-model="ruleForm.AccessTokenType">
        <el-radio :label="0">JWT</el-radio>
        <el-radio :label="1">Reference</el-radio>
      </el-radio-group>
    </el-form-item>

    <el-form-item label="Include JWT ID" prop="includeJwtId">
      <span slot="label">
        <el-tooltip content="Add an ID to JWTs, which can be used to revoke JWTs" placement="top">
          <i class="el-icon-question" />
        </el-tooltip>
        {{ $t('AbpIdentityServer[\'Client:IncludeJwtId\']') }}
      </span>
      <el-switch v-model="ruleForm.includeJwtId" :disabled="ruleForm.AccessTokenType !== 0" />
    </el-form-item>
  </div>
</template>

<script>
export default {
  name: 'ClientToken',
  props: {
    ruleForm: {
      type: Object,
      require: false,
      // 对象或数组默认值必须从一个工厂函数获取
      default: function() {
        return {
          // allowedIdentityTokenSigningAlgorithms: '',
          accessTokenLifetime: 0,
          AccessTokenType: 0,
          includeJwtId: false
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

