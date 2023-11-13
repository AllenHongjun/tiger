<template>
  <div class="app-container">
    <el-dialog :title=" dialogStatus == 'create'? $t('AppQuestionBank[\'Permission:Create\']'): $t('AbpUi[\'Edit\']')" :visible.sync="dialogFormVisible">
      <el-form ref="dataForm" :rules="rules" :model="temp" label-position="right" label-width="150px">
        <el-form-item :label="$t('AppQuestionBank[\'DisplayName:Name\']')" prop="name">
          <el-cascader v-model="temp.parentId" :options="options" :props="{ checkStrictly: true, value:'id', label:'name',children:'children',emitPath:false}" clearable class="filter-item" filterable />
        </el-form-item>
        <el-form-item :label="$t('AppQuestionBank[\'DisplayName:Name\']')" prop="name">
          <el-input v-model="temp.name" />
        </el-form-item>
        <el-form-item :label="$t('AppQuestionBank[\'DisplayName:Cover\']')" prop="cover">
          <el-input v-model="temp.cover" />
        </el-form-item>
        <el-form-item :label="$t('AppQuestionBank[\'DisplayName:Code\']')" prop="code">
          <el-input v-model="temp.code" />
        </el-form-item>
        <el-form-item :label="$t('AppQuestionBank[\'DisplayName:Enable\']')" prop="enable">
          <el-switch v-model="temp.enable" />
        </el-form-item>
        <el-form-item :label="$t('AppQuestionBank[\'DisplayName:IsPublic\']')" prop="isPublic">
          <el-switch v-model="temp.isPublic" />
        </el-form-item>
        <el-form-item :label="$t('AppQuestionBank[\'DisplayName:Sorting\']')" prop="sorting">
          <el-input v-model="temp.sorting" type="number" />
        </el-form-item>

      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button @click="dialogFormVisible = false">
          {{ $t("AbpUi['Cancel']") }}
        </el-button>
        <el-button type="primary" @click="dialogStatus === 'create' ? createData() : updateData()">
          {{ $t("AbpUi['Save']") }}
        </el-button>
      </div>
    </el-dialog>
  </div>
</template>

<script>
import {
  getAllQuestionCategory,
  getQuestionCategory,
  createQuestionCategory,
  updateQuestionCategory
} from '@/api/question-bank/question-category'
import baseListQuery, { checkPermission } from '@/utils/abp'
import { listToTree } from '@/utils/helpers/tree-helper'
export default {
  name: 'QuestionCateogyModel',
  data() {
    return {
      options: undefined,
      temp: {
        id: undefined,
        parentId: undefined,
        name: undefined,
        cover: undefined,
        code: undefined,
        enable: true,
        sorting: 0,
        isPublic: true
      },
      dialogFormVisible: false,
      dialogStatus: '',

      // 表单验证规则
      rules: {
        name: [
          {
            required: true,
            message: this.$i18n.t("AbpValidation['The {0} field is required.']", [
              this.$i18n.t("AppQuestionBank['DisplayName:Name']")
            ]),
            trigger: 'blur'
          },
          {
            max: 64,
            message: this.$i18n.t(
              "AbpValidation['The field {0} must be a string with a maximum length of {1}.']",
              [this.$i18n.t("AppQuestionBank['DisplayName:Name']"), '64']
            ),
            trigger: 'blur'
          }
        ],
        displayName: [
          {
            required: true,
            message: this.$i18n.t("AbpValidation['The {0} field is required.']", [
              this.$i18n.t("AppQuestionBank['DisplayName:DisplayName']")
            ]),
            trigger: 'blur'
          },
          {
            max: 256,
            message: this.$i18n.t(
              "AbpValidation['The field {0} must be a string with a maximum length of {1}.']",
              [this.$i18n.t("AppQuestionBank['DisplayName:Name']"), '256']
            ),
            trigger: 'blur'
          }
        ]

      }
    }
  },
  methods: {
    fetchOptions() {
      getAllQuestionCategory(baseListQuery).then(response => {
        this.options = listToTree(response.items)
      })
    },
    // 重置表单
    resetTemp() {
      this.temp = {
        id: undefined,
        parentId: undefined,
        name: undefined,
        cover: undefined,
        code: undefined,
        enable: true,
        sorting: 0,
        isPublic: true
      }
    },
    // 点击创建按钮
    handleCreate() {
      this.resetTemp()
      this.dialogStatus = 'create'
      this.dialogFormVisible = true
      this.fetchOptions()
      this.$nextTick(() => {
        this.$refs['dataForm'].clearValidate()
      })
    },

    // 创建数据
    createData() {
      this.$refs['dataForm'].validate(valid => {
        if (valid) {
          createQuestionCategory(this.temp).then(() => {
            this.$emit('handleFilter', false)
            this.dialogFormVisible = false
            this.$notify({
              title: this.$i18n.t("TigerUi['Success']"),
              message: this.$i18n.t("TigerUi['SuccessMessage']"),
              type: 'success',
              duration: 2000
            })
          })
        }
      })
    },

    // 更新按钮点击
    handleUpdate(row) {
      this.dialogStatus = 'update'
      this.dialogFormVisible = true

      // TODO:更新数据回显，异步请求调整为请求完成之后绑定数据
      this.fetchOptions()
      getQuestionCategory(row.id).then(response => {
        this.temp = response
      })

      this.$nextTick(() => {
        this.$refs['dataForm'].clearValidate()
      })
    },

    // 更新数据
    updateData() {
      this.$refs['dataForm'].validate(valid => {
        if (valid) {
          updateQuestionCategory(this.temp.id, this.temp).then(() => {
            this.$emit('handleFilter', false)
            this.dialogFormVisible = false
            this.$notify({
              title: this.$i18n.t("TigerUi['Success']"),
              message: this.$i18n.t("TigerUi['SuccessMessage']"),
              type: 'success',
              duration: 2000
            })
          })
        }
      })
    }
  }
}
</script>

<style scoped>
.line {
  text-align: center;
}
</style>
