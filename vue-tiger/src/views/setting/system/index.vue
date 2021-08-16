<template>
  <div class="app-container">
    <el-row :gutter="20">
      <el-col :span="6">
        <el-menu
          default-active="2"
          class="el-menu-vertical-demo"
          @open="handleOpen"
          @close="handleClose"
        >
          <el-submenu index="1">
            <template slot="title">
              <i class="el-icon-location" />
              <span>身份认证管理</span>
            </template>

          </el-submenu>
          <el-menu-item index="2">
            <i class="el-icon-menu" />
            <span slot="title">导航二</span>
          </el-menu-item>
          <el-menu-item index="3" disabled>
            <i class="el-icon-document" />
            <span slot="title">导航三</span>
          </el-menu-item>
          <el-menu-item index="4">
            <i class="el-icon-setting" />
            <span slot="title">导航四</span>
          </el-menu-item>
        </el-menu>
      </el-col>
      <el-col :span="18">
        <div class="grid-content bg-purple-light">
          <h3>身份认证管理</h3>
          <el-divider />
          <el-form ref="ruleForm" :model="ruleForm" :label-position="labelPosition" :rules="rules" label-width="180px" class="demo-ruleForm">
            <h5>密码设置</h5>

            <el-form-item label="密码长度" prop="name">
              <el-input v-model="ruleForm.password.requiredLength" type="number" />
              <span class="tips">密码的最小长度</span>
            </el-form-item>

            <el-form-item label="要求唯一字符数量" prop="name">
              <el-input v-model="ruleForm.password.requiredUniqueChars" type="number" />
              <span class="tips">密码必须包含唯一字符的数量</span>
            </el-form-item>

            <el-form-item label="要求非字母数字" prop="requireNonAlphanumeric">
              <el-switch v-model="ruleForm.password.requireNonAlphanumeric" />
              <p class="tips">密码是否必须包含非字母数字.</p>
            </el-form-item>

            <el-form-item label="要求小写字母" prop="requireLowercase">
              <el-switch v-model="ruleForm.password.requireLowercase" />
              <p class="tips">密码是否必须包含大写字母.</p>
            </el-form-item>

            <el-form-item label="要求数字" prop="requireDigit">
              <el-switch v-model="ruleForm.password.requireDigit" />
              <p class="tips">密码是否必须包含数字.</p>
            </el-form-item>

            <el-form-item label="要求大写字母" prop="requireUppercase">
              <el-switch v-model="ruleForm.password.requireUppercase" />
              <p class="tips">密码是否必须包含小写字母.</p>
            </el-form-item>

            <el-divider />
            <h5>锁定设置</h5>
            <el-form-item label="允许新用户" prop="allowedForNewUsers">
              <el-switch v-model="ruleForm.lockout.allowedForNewUsers" />
              <p class="tips">允许新用户被锁定.</p>
            </el-form-item>

            <el-form-item label="锁定时间(秒)" prop="lockoutDuration">
              <el-input v-model="ruleForm.lockout.lockoutDuration" type="number" />
              <span class="tips">当锁定发生时用户被的锁定的时间(秒).</span>
            </el-form-item>

            <el-form-item label="最大失败访问尝试次数" prop="maxFailedAccessAttempts">
              <el-input v-model="ruleForm.lockout.maxFailedAccessAttempts" type="number" />
              <span class="tips">如果启用锁定, 当用户被锁定前失败的访问尝试次数.</span>
            </el-form-item>

            <el-divider />
            <h5>登录设置</h5>

            <el-form-item label="要求验证的电子邮箱" prop="requireConfirmedEmail">
              <el-switch v-model="ruleForm.signIn.requireConfirmedEmail" />
              <p class="tips">登录时是否需要验证的电子邮箱.</p>
            </el-form-item>

            <el-form-item label="允许用户确认他们的电话号码" prop="enablePhoneNumberConfirmation">
              <el-switch v-model="ruleForm.signIn.enablePhoneNumberConfirmation" />
              <p class="tips">用户是否可以确认电话号码.</p>
            </el-form-item>

            <el-form-item label="要求验证的手机号码" prop="requireConfirmedPhoneNumber">
              <el-switch v-model="ruleForm.signIn.requireConfirmedPhoneNumber" />
              <p class="tips">是否允许用户更新电子邮箱.</p>
            </el-form-item>

            <el-divider />
            <h5>用户设置</h5>

            <el-form-item label="启用电子邮箱更新" prop="isEmailUpdateEnabled">
              <el-switch v-model="ruleForm.user.isEmailUpdateEnabled" />
              <p class="tips">是否允许用户更新电子邮箱.</p>
            </el-form-item>

            <el-form-item label="启用用户名更新" prop="isUserNameUpdateEnabled">
              <el-switch v-model="ruleForm.user.isUserNameUpdateEnabled" />
              <p class="tips">是否允许用户更新用户名.</p>
            </el-form-item>

            <el-divider />
            <el-form-item>
              <el-button type="primary" @click="submitForm('ruleForm')">立即创建</el-button>
              <el-button @click="resetForm('ruleForm')">重置</el-button>
            </el-form-item>
          </el-form>
        </div>
      </el-col>
    </el-row>

  </div>
</template>

<script>
export default {
  data() {
    return {
      labelPosition: 'left',
      ruleForm: {
        password: {
          requiredLength: 3,
          requiredUniqueChars: 1,
          requireNonAlphanumeric: false,
          requireLowercase: false,
          requireUppercase: false,
          requireDigit: false
        },
        lockout: {
          allowedForNewUsers: true,
          lockoutDuration: 300,
          maxFailedAccessAttempts: 5
        },
        signIn: {
          requireConfirmedEmail: false,
          enablePhoneNumberConfirmation: true,
          requireConfirmedPhoneNumber: false
        },
        user: {
          isUserNameUpdateEnabled: true,
          isEmailUpdateEnabled: true
        },

        name: '',
        region: '',
        date1: '',
        date2: '',
        delivery: false,
        type: [],
        resource: '',
        desc: ''
      },
      rules: {
        name: [
          { required: true, message: '请输入活动名称', trigger: 'blur' },
          { min: 3, max: 5, message: '长度在 3 到 5 个字符', trigger: 'blur' }
        ],
        region: [
          { required: true, message: '请选择活动区域', trigger: 'change' }
        ],
        date1: [
          { type: 'date', required: true, message: '请选择日期', trigger: 'change' }
        ],
        date2: [
          { type: 'date', required: true, message: '请选择时间', trigger: 'change' }
        ],
        type: [
          { type: 'array', required: true, message: '请至少选择一个活动性质', trigger: 'change' }
        ],
        resource: [
          { required: true, message: '请选择活动资源', trigger: 'change' }
        ],
        desc: [
          { required: true, message: '请填写活动形式', trigger: 'blur' }
        ]
      }
    }
  },
  methods: {
    handleOpen(key, keyPath) {
      console.log(key, keyPath)
    },
    handleClose(key, keyPath) {
      console.log(key, keyPath)
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
.tips{
  color:rgb(155, 157, 158);
}
</style>

