<template>
  <div class="app-container">
    <el-form
      ref="ruleForm"
      :model="ruleForm"
      :rules="rules"
      label-width="100px"
      class="demo-ruleForm"
    >
      <el-row>
        <el-col :span="8">
          <div class="grid-content bg-purple">
            <el-form-item label="名称" prop="name">
              <el-input v-model="ruleForm.name" />
            </el-form-item></div>
          </el-col>
      </el-row>
      <el-form-item label="是否默认" prop="resource">
        <el-radio-group v-model="ruleForm.isDefault">
          <el-radio :label="true">是</el-radio>
          <el-radio :label="false">否</el-radio>
        </el-radio-group>
      </el-form-item>
      <el-form-item label="是否公共" prop="resource">
        <el-radio-group v-model="ruleForm.isPublic">
          <el-radio :label="true">是</el-radio>
          <el-radio :label="false">否</el-radio>
        </el-radio-group>
      </el-form-item>
      <el-form-item label="是否静态" prop="resource">
        <el-radio-group v-model="ruleForm.isStatic">
          <el-radio :label="true">是</el-radio>
          <el-radio :label="false">否</el-radio>
        </el-radio-group>
      </el-form-item>

      <el-form-item>
        <el-button
          type="primary"
          @click="submitForm('ruleForm')"
        >提交</el-button>
        <el-button @click="resetForm('ruleForm')">重置</el-button>
      </el-form-item>
    </el-form>
  </div>
</template>
<script>
import { getRole, createRole } from '@/api/user'

export default {
  name: 'role_detail',
  props: {
    isEdit: {
      type: Boolean,
      default: false
    }
  },
  data() {
    return {
      ruleForm: {
        name: '',
        isDefault: false,
        isStatic: false,
        isPublic: false,
        id: '',
        roleNames: ['admin']
      },
      rules: {
        name: [
          { required: true, message: '请输入姓名', trigger: 'blur' },
          { min: 2, max: 8, message: '长度在 2 到 8 个字符', trigger: 'blur' }
        ]
      }
    }
  },
  created() {
    if (this.isEdit) {
      const id = this.$route.params && this.$route.params.id
      this.fetchData(id)
    }
    // Why need to make a copy of this.$route here?
    // Because if you enter this page and quickly switch tag, may be in the execution of the setTagsViewTitle function, this.$route is no longer pointing to the current page
    // https://github.com/PanJiaChen/vue-element-admin/issues/1221
    this.tempRoute = Object.assign({}, this.$route)
  },
  methods: {
    fetchData(id) {
      console.log(id)
      getRole(id).then(response => {
        this.ruleForm.name = response.name
        this.ruleForm.isDefault = response.isDefault
        this.ruleForm.isPublic = response.isPublic
        this.ruleForm.isStatic = response.isStatic

      }).catch(err => {
        console.log(err)
      })
    },
    submitForm(formName) {
      this.$refs[formName].validate((valid) => {
        if (valid) {
          createRole(this.ruleForm).then((response) => {
            console.log(response)
            this.$message({
              message: '添加成功',
              type: 'success'
            });
          })

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
