<template>
  <div class="app-container">
    <el-row :gutter="20">
      <el-col :span="12">
        <div class="grid-content bg-purple">
          <el-form ref="ruleform" :rules="rules" :model="form" label-width="120px">
            <el-form-item label="活动名称" prop="name">
              <el-input v-model="form.name" />
            </el-form-item>
            <el-form-item label="活动区域" prop="region">
              <el-select v-model="form.region" placeholder="please select your zone">
                <el-option label="上海" value="shanghai" />
                <el-option label="北京" value="beijing" />
                <el-option label="广州" value="guangzhou" />
                <el-option label="深圳" value="shengzhen" />
              </el-select>
            </el-form-item>
            <el-form-item label="活动时间" prop="date1">
              <el-col :span="11">
                <el-date-picker v-model="form.date1" type="date" placeholder="日期" style="width: 100%;" />
              </el-col>
              <el-col :span="2" class="line">-</el-col>
              <el-col :span="11">
                <el-time-picker v-model="form.date2" type="fixed-time" placeholder="时间" style="width: 100%;" />
              </el-col>
            </el-form-item>
            <el-form-item label="即时交货" prop="delivery">
              <el-switch v-model="form.delivery" />
            </el-form-item>
            <el-form-item label="活动类型" prop="type">
              <el-checkbox-group v-model="form.type">
                <el-checkbox label="线上" name="type" />
                <el-checkbox label="推广活动" name="type" />
                <el-checkbox label="线下" name="type" />
                <el-checkbox label="简单" name="type" />
              </el-checkbox-group>
            </el-form-item>
            <el-form-item label="资源" prop="resource">
              <el-radio-group v-model="form.resource">
                <el-radio label="赞助" />
                <el-radio label="场地" />
              </el-radio-group>
            </el-form-item>
            <el-form-item label="活动表单" prop="desc">
              <el-input v-model="form.desc" type="textarea" />
            </el-form-item>
            <el-form-item>
              <el-button type="primary" @click="onSubmit('ruleform')">提交</el-button>
              <el-button @click="onCancel">取消</el-button>
            </el-form-item>
          </el-form>
        </div>
      </el-col>
      <el-col :span="12"><div class="grid-content bg-purple-light">
        <el-form ref="ruleUserForm" :rules="userFormRules" :model="userForm" label-width="120px">
          <el-form-item label="用户名" prop="username">
            <el-input v-model="userForm.username" />
          </el-form-item>
          <el-form-item label="手机号" prop="phone">
            <el-input v-model="userForm.phone" />
          </el-form-item>
          <el-form-item label="邮箱" prop="email">
            <el-input v-model="userForm.email" />
          </el-form-item>
          <el-form-item label="旧密码" prop="oldPassword">
            <el-input v-model="userForm.oldPassword" show-password />
          </el-form-item>
          <el-form-item label="新密码" prop="password">
            <el-input v-model="userForm.password" show-password />
          </el-form-item>
          <el-form-item label="确认密码" prop="comfirmPwd">
            <el-input v-model="userForm.comfirmPwd" show-password />
          </el-form-item>
          <el-form-item label="地址" prop="address">
            <el-input v-model="userForm.address" />
          </el-form-item>
          <el-form-item label="生日" prop="birthday">
            <el-col :span="11">
              <el-date-picker v-model="userForm.birthday" type="date" placeholder="日期" style="width: 100%;" />
            </el-col>
          </el-form-item>
          <el-form-item label="性别" prop="sex">
            <el-radio-group v-model="userForm.sex">
              <el-radio label="男" />
              <el-radio label="女" />
            </el-radio-group>
          </el-form-item>
          <el-form-item label="城市" prop="city">
            <el-input v-model="userForm.city" />
          </el-form-item>
          <el-form-item label="年龄" prop="age">
            <el-input v-model.number="userForm.age" />
          </el-form-item>

          <el-form-item>
            <el-button type="primary" @click="onSubmit('ruleUserForm')">提交</el-button>
            <el-button @click="onCancel">取消</el-button>
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
    const reg = /(?!^(\d+|[a-zA-Z]+|[~!@#$%^&*?]+)$)^[\w~!@#$%^&*?]{6,22}$/
    var validateNewPwd = (rule, value, callback) => {
      if (!reg.test(value)) {
        callback(new Error('密码应是6-22位数字、字母或字符！'))
      } else if (this.userForm.oldPassword === value) {
        callback(new Error('新密码与旧密码不可一致！'))
      } else {
        callback()
      }
    }
    var validateComfirmPwd = (rule, value, callback) => {
      console.log('this.userForm.comfirmPwd', this.userForm.comfirmPwd, 'value', value)
      if (!reg.test(value)) {
        callback(new Error('密码应是6-22位数字、字母或字符！'))
      } else if (this.userForm.password !== value) {
        callback(new Error('确认密码与新密码不一致！'))
      } else {
        callback()
      }
    }
    return {
      form: {
        name: '',
        region: '',
        date1: '',
        date2: '',
        delivery: false,
        type: [],
        resource: '',
        desc: ''
      },
      /**
       *
       * Form 组件提供了表单验证的功能，只需要通过 rules 属性传入约定的验证规则，
       * 并将 Form-Item 的 prop 属性设置为需校验的字段名即可。
       * 校验规则参见 async-validator  https://github.com/yiminghe/async-validator
       */
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
      },

      userForm: {
        username: '',
        phone: '',
        oldPassword: '',
        password: '',
        comfirmPwd: '',
        address: '',
        email: '',
        birthday: '',
        sex: '',
        age: '',
        city: ''
      },
      userFormRules: {
        username: [
          { required: true, message: '请输入用户名', trigger: 'blur' },
          { min: 3, max: 15, message: '长度在 3 到 15 个字符', trigger: 'blur' }
        ],
        phone: [{
          required: true,
          pattern: /^1[3-9]\d{9}$/, // 可以写正则表达式呦呦呦
          message: '目前只支持中国大陆的手机号码',
          trigger: 'blur'
        }],
        oldPassword: [
          { required: true, message: '请输入旧密码', trigger: 'blur' },
          { min: 3, max: 15, message: '长度在 3 到 22 个字符', trigger: 'blur' }
        ],
        password: [
          { required: true, message: '请输入新密码', trigger: 'blur' },
          { validator: validateNewPwd, trigger: 'blur' }
        ],
        comfirmPwd: [
          { required: true, message: '请输入确认密码', trigger: 'blur' },
          { validator: validateComfirmPwd, trigger: 'blur' }
        ],
        address: [
          { required: true, message: '请输入地址', trigger: 'change' },
          { min: 2, max: 250, message: '长度在 2 到 250 个字符', trigger: 'blur' }
        ],
        email: [
          {
            required: true, // 是否必填
            message: '请输入正确的邮箱地址', // 错误提示信息
            trigger: 'blur', // 检验方式（blur为鼠标点击其他地方，）
            type: 'email' // 要检验的类型（number，email，date等）
          }
        ],
        birthday: [
          { type: 'date', required: true, message: '请选择日期', trigger: 'change' }
        ],
        sex: [
          { required: true, message: '请选择性别', trigger: 'change' }
        ],
        age: [
          {
            required: true, // 是否必填
            message: '请输入正确的年龄' // 错误提示信息
            // trigger: 'blur' // 检验方式（blur为鼠标点击其他地方，）
            // type: 'number' // 要检验的类型（number，email，date等）
          },
          {
            type: 'number',
            message: '年龄必须为数字值'
            // trigger: 'blur' // 检验方式（blur为鼠标点击其他地方，）
          }
        ]

      }
    }
  },
  methods: {
    onSubmit(formName) {
      // 提交的验证指定的表单数据。
      this.$refs[formName].validate((valid) => {
        if (valid) {
          alert('验证成功提交表单')
        } else {
          console.log('error submit!!')
          return false
        }
      })
      // this.$message('submit!')
    },
    onCancel() {
      this.$message({
        message: '取消了!',
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

