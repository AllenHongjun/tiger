<template>
  <div class="app-container">
    <!-- <el-form-item label="Allowed Identity Token Signing Algorithms" prop="allowedIdentityTokenSigningAlgorithms" clearable>
      <el-select v-model="ruleForm.allowedIdentityTokenSigningAlgorithms" placeholder="请选择" multiple>
        <el-option label="ES256" value="ES256" />
        <el-option label="ES384" value="ES384" />
        <el-option label="ES512" value="ES512" />
        <el-option label="PS256" value="PS256" />
        <el-option label="PS384" value="PS384" />
        <el-option label="PS512" value="PS512" />
        <el-option label="RS256" value="RS256" />
        <el-option label="RS384" value="RS384" />
        <el-option label="RS512" value="RS512" />
      </el-select>
    </el-form-item> -->

    <el-form-item label="Access Token Lifetime" prop="accessTokenLifetime">
      <!-- 优化：直接显示能看到提示 -->
      <span slot="label">
        <el-tooltip content="Lifetime in seconds" placement="top">
          <i class="el-icon-question" />
        </el-tooltip>
        Access Token Lifetime
      </span>
      <el-input v-model="ruleForm.accessTokenLifetime" type="number">
        <el-button slot="append" type="primary">15 分钟</el-button>
        <el-button slot="append" type="success">30 分钟</el-button>
        <el-button slot="append" type="primary">1 小时</el-button>
        <el-button slot="append" type="primary">5 小时</el-button>
      </el-input>

    </el-form-item>

    <el-form-item label="Access Token Type" prop="AccessTokenType">
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
        Include JWT ID
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

