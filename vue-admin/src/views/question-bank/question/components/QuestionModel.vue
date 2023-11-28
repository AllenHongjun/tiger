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
        <!-- <el-form-item :label="$t('AppQuestionBank[\'DisplayName:Name\']')" prop="name">
          <el-input v-model="temp.name" />
        </el-form-item> -->

        <el-form-item :label="$t('AppQuestionBank[\'DisplayName:Content\']')" prop="content">
          <!-- <el-input v-model="temp.content" type="textarea" :autosize="{ minRows: 4, maxRows: 6}" /> -->
          <tinymce v-model="temp.content" :height="300" />
          <div v-if="temp.type === QuestionType.Completion" style="margin-top: 10px;">
            <span>请在填空位置</span>   <el-button type="primary" @click="insertBlankFill()">插入填空符</el-button>
          </div>
        </el-form-item>

        <el-form-item v-if="temp.type === QuestionType.TrueOrFalse" label="选项" prop="name">
          <el-radio v-model="temp.answer" label="true">A 正确</el-radio>
          <el-radio v-model="temp.answer" label="false">B 错误</el-radio>
        </el-form-item>

        <div v-if="temp.type === QuestionType.SingleChoice" class="single-option-container">
          <el-row style="" class="option-header">
            <el-col :span="4">勾选设置答案</el-col>
            <el-col :span="20">选项内容</el-col>
          </el-row>
          <el-form-item label="选项">
            <el-radio v-model="temp.answer" label="A" />
            <el-input v-model="temp.optionContent" />
          </el-form-item>
          <el-form-item label="选项">
            <el-radio v-model="temp.answer" label="B" />
            <el-input v-model="temp.optionContent" />
          </el-form-item>
          <el-form-item label="选项">
            <el-radio v-model="temp.answer" label="C" />
            <el-input v-model="temp.optionContent" />
          </el-form-item>
          <el-form-item label="选项">
            <el-radio v-model="temp.answer" label="D" />
            <el-input v-model="temp.optionContent" />
          </el-form-item>
          <!-- <el-form-item>
            <el-button class="el-icon-plus" @click="addQuestionOption()">添加选项</el-button>
            <el-button class="el-icon-minus" type="danger" @click="minusQuestionOption()">删除选项</el-button>
          </el-form-item> -->
        </div>
        <div v-if="temp.type === QuestionType.MultipleChoice" class="multi-contariner">
          <el-row style="" class="option-header">
            <el-col :span="4">勾选设置答案</el-col>
            <el-col :span="20">选项内容</el-col>
          </el-row>
          <el-checkbox-group v-model="temp.answer">
            <el-form-item label="选项">
              <el-checkbox label="A">A</el-checkbox>
              <el-input v-model="temp.optionContent" />
            </el-form-item>
            <el-form-item label="选项">
              <el-checkbox label="B">B</el-checkbox>
              <el-input v-model="temp.optionContent" style="" />
            </el-form-item>
            <el-form-item label="选项">
              <el-checkbox label="C">C</el-checkbox>
              <el-input v-model="temp.optionContent" />
            </el-form-item>
            <el-form-item label="选项">
              <el-checkbox label="D">D</el-checkbox>
              <el-input v-model="temp.optionContent" />
            </el-form-item>
          </el-checkbox-group>

          <!-- <el-form-item>
            <el-button class="el-icon-plus" @click="addQuestionOption()">添加选项</el-button>
            <el-button class="el-icon-minus" type="danger" @click="minusQuestionOption()">删除选项</el-button>
          </el-form-item> -->
        </div>

        <el-form-item v-if="temp.type === QuestionType.Completion || temp.type === QuestionType.QA || temp.type === QuestionType.ShortAnswer" :label="$t('AppQuestionBank[\'DisplayName:Answer\']')" prop="score" class="answer">
          <div v-if="temp.type === QuestionType.Completion">
            <el-input v-model="temp.answer" placeholder="请输入内容">
              <template slot="prepend">填空1答案</template>
              <el-button v-if="false" slot="append" icon="el-icon-close" />
            </el-input>
            <el-input v-model="temp.answer" placeholder="请输入内容">
              <template slot="prepend">填空2答案</template>
              <el-button slot="append" icon="el-icon-close" />
            </el-input>
            <el-row>
              <el-button type="primary" style="margin-right:10px;">增加填空题答案</el-button>
              <el-checkbox v-model="checked">判分时不区分答案先后顺序</el-checkbox>
              <el-checkbox v-model="checked">只要匹配答案的部分关键字就可得分</el-checkbox>
            </el-row>
          </div>
          <div v-if="temp.type === QuestionType.QA || temp.type === QuestionType.ShortAnswer">
            <el-input v-model="temp.answer" type="textarea" :autosize="{ minRows: 2, maxRows: 4}" />
          </div>
        </el-form-item>

        <el-row>
          <el-col :span="12">
            <el-form-item :label="$t('AppQuestionBank[\'DisplayName:Score\']')" prop="score">
              <el-input v-model="temp.score" type="number" />
            </el-form-item>
          </el-col>
          <el-col :span="12">
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
          </el-col>
        </el-row>

        <el-form-item :label="$t('AppQuestionBank[\'DisplayName:Analysis\']')" prop="analysis">
          <!-- <el-input v-model="temp.analysis" type="textarea" :autosize="{ minRows: 2, maxRows: 4}" /> -->
          <tinymce v-model="temp.analysis" :height="150" />
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
import Tinymce from '@/components/Tinymce'
export default {
  name: 'QuestionModel',
  components: {
    Tinymce
  },
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
        type: 2,
        name: undefined,
        content: undefined,
        optionContent: undefined,
        optionSize: undefined,
        answer: undefined,
        score: undefined,
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

    // 添加题目选项
    addQuestionOption() {
      // 操作数据 https://blog.csdn.net/weixin_42282414/article/details/115765510
      this.$alert('添加成功')
    },

    minusQuestionOption() {
      this.$alert('删除选项！')
    },

    insertBlankFill() {

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

<style lang="scss" scoped>

::v-deep .el-dialog__body{
  height: calc(80vh - 50px);
  overflow: auto;
}
.option-header{
  background-color: #eff2f5;
  text-align:center;
  font-size: 12px;
  height: 34px;
  line-height: 34px;
  margin-bottom: 5px;
}
.single-option-container{
  .el-input{
    width: 90%;
  }
}

.multi-contariner{
  ::v-deep .el-input {
    width: 80%;
    margin-left:20px;
  }
}
.answer{
  .el-input{
    margin-bottom: 5px;
  }
}
</style>

