<template>
  <div class="app-container">
    <el-dialog :title=" dialogStatus == 'create'? $t('AppQuestionBank[\'Permission:Create\']'): $t('AbpUi[\'Edit\']')" :visible.sync="dialogFormVisible" top="4vh" width="70%">
      <el-form ref="dataForm" :rules="rules" :model="temp" label-position="right" label-width="150px">
        <el-form-item :label="$t('AppQuestionBank[\'DisplayName:Name\']')" prop="name">
          <el-input v-model="temp.name" />
        </el-form-item>
        <el-form-item prop="type" :label="$t('AppQuestionBank[\'DisplayName:Type\']')">
          <el-select v-model="temp.type" :placeholder="$t('AbpUi[\'PlaceholderInput\']')" filterable clearable>
            <el-option
              v-for="item in QuestionTypeMap"
              :key="item"
              :label="item"
              :value="item"
            />
          </el-select>
        </el-form-item>
        <el-form-item :label="$t('AppQuestionBank[\'DisplayName:QuestionCateogryName\']')" prop="questionCategoryId">
          <el-cascader v-model="temp.questionCategoryId" :options="questionCategoryOptions" :props="{ checkStrictly: true, value:'id', label:'name',children:'children',emitPath:false}" clearable class="filter-item" filterable />
        </el-form-item>
        <el-form-item :label="$t('AppQuestionBank[\'DisplayName:Content\']')" prop="content">
          <el-input v-model="temp.content" type="textarea" :autosize="{ minRows: 4, maxRows: 6}" />
        </el-form-item>

        <el-form-item :label="$t('AppQuestionBank[\'DisplayName:Degree\']')" prop="degree">
          <el-select v-model="temp.degree" :placeholder="$t('AbpUi[\'PlaceholderInput\']')" filterable clearable>
            <el-option
              v-for="item in QuestionDegreeMap"
              :key="item"
              :label="item"
              :value="item"
            />
          </el-select>
        </el-form-item>

        <el-form-item :label="$t('AppQuestionBank[\'DisplayName:Source\']')" prop="source">
          <el-input v-model="temp.source" type="textarea" :autosize="{ minRows: 4, maxRows: 6}" />
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
  getQuestion,
  createQuestion,
  updateQuestion
} from '@/api/question-bank/question'
import {
  getAllQuestionCategory
} from '@/api/question-bank/question-category'
import baseListQuery, { checkPermission } from '@/utils/abp'
import { QuestionType, QuestionTypeMap, QuestionDegree, QuestionDegreeMap } from '../datas/typing'
import { listToTree } from '@/utils/helpers/tree-helper'
export default {
  name: 'QuestionModel',
  data() {
    return {
      QuestionTypeMap,
      QuestionDegreeMap,
      questionCategoryOptions: [],
      temp: {
        id: undefined,
        questionCategoryId: undefined,
        practicalTrainingId: undefined,
        type: 1,
        name: undefined,
        content: undefined,
        optionA: undefined,
        optionB: undefined,
        optionC: undefined,
        optionD: undefined,
        optionE: undefined,
        degree: 1,
        analysis: undefined,
        source: undefined,
        helpMessage: undefined,
        helpVideo: undefined,
        fileUrl: undefined,
        fileName: undefined,
        isShow: true,
        enable: true,
        isShowTextButton: true,
        isShowImageButton: true,
        isShowLinkButton: true
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
        ]

      }
    }
  },
  created() {
    console.log('QuestionTypeMap', QuestionTypeMap)
  },
  methods: {
    checkPermission,
    fetchQuestionCategoryOptions() {
      getAllQuestionCategory(baseListQuery).then(response => {
        this.questionCategoryOptions = listToTree(response.items)
      })
    },
    // 重置表单
    resetTemp() {
      this.temp = {
        id: undefined,
        name: '',
        displayName: '',
        description: '',
        path: '',
        redirect: '',
        dataId: undefined,
        freamwork: ''
      }
    },

    // 点击创建按钮
    handleCreate() {
      this.fetchQuestionCategoryOptions()
      this.resetTemp()
      this.dialogStatus = 'create'
      this.dialogFormVisible = true
      this.$nextTick(() => {
        this.$refs['dataForm'].clearValidate()
      })
    },

    // 创建数据
    createData() {
      this.$refs['dataForm'].validate(valid => {
        if (valid) {
          createQuestion(this.temp).then(() => {
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
      this.fetchQuestionCategoryOptions()
      this.dialogStatus = 'update'
      this.dialogFormVisible = true
      getQuestion(row.id).then(response => {
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
          updateQuestion(this.temp.id, this.temp).then(() => {
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
.line{
  text-align: center;
}
</style>

