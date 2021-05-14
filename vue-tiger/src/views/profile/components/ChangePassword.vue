<template>
  <el-form ref="aForm" :model="aForm" :rules="aRules">
    <el-form-item label="原密码" prop="password">
      <el-input v-model.trim="aForm.password" type="password" show-password/>
    </el-form-item>
    <el-form-item label="新密码" prop="newPassword">
      <el-input v-model.trim="aForm.newPassword" type="password" show-password/>
    </el-form-item>
    <el-form-item label="确认新密码" prop="againPassword" >
      <el-input v-model.trim="aForm.againPassword" type="password" show-password/>
    </el-form-item>
    <el-form-item>
      <el-button type="primary" @click="submit">提交</el-button>
    </el-form-item>
  </el-form>
</template>

<script>
import { changePassword } from '@/api/user'
export default {
  data() {
    var avalidatePass = (rule, value, callback) => {
      if (value === '') {
        callback(new Error(
          '确认密码不能为空'
        ))
      } else if (value !== this.aForm.newPassword) {
        callback(new Error(
          '两次输入的密码不一致'
        ))
      }
      // else {
      //   callback()
      // }
    }
    return {
      aForm: {
        password: '',
        newPassword: '',
        againPassword: ''
      },
      aRules: {
        password: [{
          required: true,
          message: '原密码不能为空',
          trigger: ['blur', 'change']
        }
        ],
        newPassword: [{
          required: true,
          message: '新密码不能为空',
          trigger: ['blur', 'change']
        }
        ],
        againPassword: [{
          required: true,
          message: '确认密码不能为空',
          validator: avalidatePass,
          trigger: ['blur', 'change']
        }
        ]
      },
      loading: false
    }
  },
  methods: {
    submit() {
      this.$refs.aForm.validate(valid => {
        if (valid) {
          this.loading = true
          const dataJson = {
            currentPassword: this.aForm.password,
            newPassword: this.aForm.againPassword
          }
          changePassword(dataJson).then((res) => {
            this.loading = false
            this.$notify({
              title: '成功',
              message: '消息获取成功',
              type: 'success',
              duration: 2000
            })
          })
            .catch(() => {
              this.loading = false
            })
        } else {
          return false
        }
      })
    }
  }
}
</script>
