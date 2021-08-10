<template>
  <div class="app-container">
    <el-row :gutter="20">
      <el-col :span="12"><div class="grid-content bg-purple">
        <el-form ref="form" :rules="rules" :model="form" label-width="120px">
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
            <el-button type="primary" @click="onSubmit('form')">提交</el-button>
            <el-button @click="onCancel">取消</el-button>
          </el-form-item>
        </el-form></div></el-col>
      <el-col :span="12"><div class="grid-content bg-purple-light">2</div></el-col>
    </el-row>

  </div>
</template>

<script>
export default {
  data() {
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
      }
    }
  },
  methods: {
    onSubmit(formName) {
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

