<template>
  <div class="app-container">
    <el-dialog :title=" dialogStatus == 'create'? $t('AppQuestionBank[\'Permission:Create\']'): $t('AbpUi[\'Edit\']')" :visible.sync="dialogFormVisible" top="4vh" width="70%">
      <el-form ref="dataForm" :rules="rules" :model="temp" label-position="right" label-width="150px">
        <el-row>
          <el-col :span="12">
            <el-form-item prop="type" :label="$t('AppQuestionBank[\'DisplayName:Type\']')">
              <el-select v-model="temp.type" placeholder="-" filterable clearable>
                <el-option
                  v-for="item in questionTypeOptions"
                  :key="item.key"
                  :label="item.lable"
                  :value="item.value"
                />
              </el-select>
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item :label="$t('AppQuestionBank[\'DisplayName:QuestionCateogryName\']')" prop="questionCategoryId">
              <el-cascader v-model="temp.questionCategoryId" :options="questionCategoryOptions" :props="{ checkStrictly: true, value:'id', label:'name',children:'children',emitPath:false}" clearable class="filter-item" filterable />
            </el-form-item>
          </el-col>
        </el-row>
        <el-form-item :label="$t('AppQuestionBank[\'DisplayName:Name\']')" prop="name">
          <el-input v-model="temp.name" />
        </el-form-item>

        <el-form-item :label="$t('AppQuestionBank[\'DisplayName:Content\']')" prop="content">
          <el-input v-model="temp.content" type="textarea" :autosize="{ minRows: 4, maxRows: 6}" />
          <div v-if="temp.type === QuestionType.Completion">
            请在填空位置<el-button type="primary">插入填空符</el-button>
          </div>
        </el-form-item>

        <el-form-item v-if="temp.type === QuestionType.QA" :label="$t('AppQuestionBank[\'DisplayName:Answer\']')" prop="answer">
          <el-input v-model="temp.answer" type="textarea" :autosize="{ minRows: 3, maxRows: 5}" />
        </el-form-item>

        <el-form-item v-if="temp.type === QuestionType.TrueOrFalse" label="选项" prop="name">
          <el-radio v-model="radio" label="A">A. 正确</el-radio>
          <el-radio v-model="radio" label="B">B. 错误</el-radio>
        </el-form-item>

        <div v-if="temp.type === QuestionType.SingleChoice" class="single-option-container">
          <el-form-item label="A">
            <el-radio v-model="temp.answer" lable="A">A</el-radio>
            <el-input v-model="temp.name" style="width: 80%;" />
          </el-form-item>
          <el-form-item label="B">
            <el-radio v-model="temp.answer" lable="B">B</el-radio>
            <el-input v-model="temp.name" style="width: 80%;" />
          </el-form-item>
          <el-form-item label="C">
            <el-radio v-model="temp.answer" lable="C">C</el-radio>
            <el-input v-model="temp.name" style="width: 80%;" />
          </el-form-item>
          <el-form-item>
            <el-button class="el-icon-plus">添加选项</el-button>
            <el-button class="el-icon-minus" type="danger">删除选项</el-button>
          </el-form-item>
        </div>

        <el-row v-if="temp.type === QuestionType.MultipleChoice" class="multi-contariner">
          <el-form-item label="选项">
            <el-checkbox label="复选框 A">A</el-checkbox>
            <el-input v-model="temp.name" style="width: 80%;margin-left:20px;" />
          </el-form-item>
          <el-form-item label="选项">
            <el-checkbox label="复选框 A">B</el-checkbox>
            <el-input v-model="temp.name" style="width: 80%;margin-left:20px;" />
          </el-form-item>
          <el-form-item label="选项">
            <el-checkbox label="复选框 A">C</el-checkbox>
            <el-input v-model="temp.name" style="width: 80%;margin-left:20px;" />
          </el-form-item>
          <el-form-item label="选项">
            <el-checkbox label="复选框 A">D</el-checkbox>
            <el-input v-model="temp.name" style="width: 80%;margin-left:20px;" />
          </el-form-item>
        </el-row>

        <el-form-item v-if="false">
          <el-button>删减选项</el-button>
          <el-button type="primary">增加选项</el-button>
        </el-form-item>

        <el-form-item :label="$t('AppQuestionBank[\'DisplayName:Degree\']')" prop="degree">
          <el-select v-model="temp.degree" placeholder="-" filterable clearable>
            <el-option
              v-for="item in questionDegreeOptions"
              :key="item.key"
              :label="item.lable"
              :value="item.value"
            />
          </el-select>
        </el-form-item>

        <el-form-item :label="$t('AppQuestionBank[\'DisplayName:Analysis\']')" prop="analysis">
          <el-input v-model="temp.analysis" type="textarea" :autosize="{ minRows: 2, maxRows: 4}" />
        </el-form-item>

        <el-form-item :label="$t('AppQuestionBank[\'DisplayName:Source\']')" prop="source">
          <el-input v-model="temp.source" type="textarea" :autosize="{ minRows: 1, maxRows: 2}" />
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
import { QuestionType, QuestionTypeMap, QuestionDegree, QuestionDegreeMap, Type, Degree } from '../datas/typing'
import { listToTree } from '@/utils/helpers/tree-helper'
export default {
  name: 'QuestionModel',
  data() {
    return {
      QuestionType,
      QuestionTypeMap,
      QuestionDegreeMap,
      questionCategoryOptions: [],
      questionTypeOptions: Type,
      questionDegreeOptions: Degree,
      radio: 'A',
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
        answer: 'B',
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
      dialogFormVisible: true,
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

