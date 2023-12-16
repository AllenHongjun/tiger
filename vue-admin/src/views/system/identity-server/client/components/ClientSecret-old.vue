<template>
  <div class="app-container">
    <el-form ref="ruleForm" :model="ruleForm" :rules="rules" label-width="140px" class="demo-ruleForm">
      <el-form-item label="Require Secret" prop="requireClientSecret">
        <span slot="label">
          <el-tooltip content="Specifies if a secret is required" placement="top">
            <i class="el-icon-question" />
          </el-tooltip>
          Require Secret
        </span>
        <el-switch v-model="ruleForm.requireClientSecret" />
      </el-form-item>

      <el-form-item label="Type" prop="type" clearable>
        <el-select v-model="ruleForm.type" placeholder="请选择Type">
          <el-option label="Shared Secret" value="Shared Secret" />
          <el-option label="X509 Certificate Base64" value="X509 Certificate Base64" />
          <el-option label="X509 Name" value="X509 Name" />
          <el-option label="X509 Thumbprint" value="X509 Thumbprint" />
        </el-select>
      </el-form-item>

      <el-form-item label="Value" prop="value">
        <!-- 优化：直接显示能看到提示 -->
        <span slot="label">
          <el-tooltip content="WARNING: you will not be able to access the value of secrets after creating them" placement="top">
            <i class="el-icon-question" />
          </el-tooltip>
          Value
        </span>
        <el-input v-model="ruleForm.value">
          <el-button slot="append" icon="el-icon-refresh-right">生成</el-button>
        </el-input>
      </el-form-item>

      <el-form-item label="Expiration" prop="expiration">
        <el-switch v-model="ruleForm.hasExpiration" />

      </el-form-item>

      <el-form-item prop="expiration">
        <el-date-picker
          v-if="ruleForm.hasExpiration"
          v-model="ruleForm.expiration"
          type="datetime"
          placeholder="选择过期时间"
          default-time="00:00:00"
        />
      </el-form-item>

      <el-form-item label="Description" prop="description">
        <el-input v-model="ruleForm.description" type="textarea" :autosize="{ minRows: 4, maxRows: 6}" />
      </el-form-item>

      <el-form-item>
        <el-button type="primary" @click="submitForm('ruleForm')">立即创建</el-button>
        <el-button @click="resetForm('ruleForm')">重置</el-button>
      </el-form-item>
    </el-form>
  </div>
</template>

<script>
export default {
  name: 'ClientSecret',
  props: {
    providerName: {
      type: String,
      required: false,
      default: ''
    }
  },
  data() {
    return {
      blank: {

      },
      ruleForm: {
        requireClientSecret: undefined,
        type: undefined,
        value: undefined,
        description: undefined,
        hasExpiration: false,
        expiration: undefined

      },
      rules: {
        value: [
          { min: 0, max: 15, message: '长度在 3 到 5 个字符', trigger: 'blur' }
        ],

        expiration: [
          { type: 'date', message: '请选择日期', trigger: 'change' }
        ]

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

