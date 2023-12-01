<template>
  <div class="app-container">
    <el-dialog :title=" dialogStatus == 'create'? $t('AppQuestionBank[\'Permission:Create\']'): $t('AbpUi[\'Edit\']')" :visible.sync="dialogFormVisible" top="4vh" width="70%">
      <el-form ref="dataForm" :rules="rules" :model="temp" label-position="right" label-width="150px">
        <el-form-item :label="$t('AppQuestionBank[\'DisplayName:Name\']')" prop="name">
          <el-input v-model="temp.source" type="text" />
        </el-form-item>
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
          <el-checkbox v-model="useEditor">使用富文本编辑器</el-checkbox>
          <tinymce v-if="useEditor" v-model="temp.content" :height="300" />
          <el-input v-else v-model="temp.content" type="textarea" :autosize="{ minRows: 4, maxRows: 6}" />
          <div v-if="temp.type === QuestionType.Completion" style="margin-top: 10px;">
            <span>请在填空位置</span>   <el-button type="primary" @click="insertBlankFill()">插入填空符</el-button>
          </div>
        </el-form-item>

        <el-form-item v-if="temp.type === QuestionType.TrueOrFalse" label="选项" prop="name">
          <el-radio v-model="temp.answer" label="true">A 正确</el-radio>
          <el-radio v-model="temp.answer" label="false">B 错误</el-radio>
        </el-form-item>

        <!-- 单选题 -->
        <div v-if="temp.type === QuestionType.SingleChoice" class="single-option-container">
          <el-row style="" class="option-header">
            <el-col :span="4">勾选设置答案</el-col>
            <el-col :span="20">选项内容</el-col>
          </el-row>
          <el-form-item v-for="(item,index) in temp.optionContentArr" :key="item.key" label="选项">
            <el-radio v-model="temp.answer" :label="alphas[index]" />
            <el-input v-model="item.value" />
          </el-form-item>
          <el-form-item>
            <el-button class="el-icon-plus" type="primary" plain @click="addQuestionOption()"> 添加选项</el-button>
            <el-button v-if="temp.optionContentArr.length > 4" class="el-icon-minus" type="danger" @click="minusQuestionOption()"> 删除选项</el-button>
          </el-form-item>
        </div>

        <!-- 多选题 -->
        <div v-if="temp.type === QuestionType.MultipleChoice" class="multi-contariner">
          <el-row style="" class="option-header">
            <el-col :span="4">勾选设置答案</el-col>
            <el-col :span="20">选项内容</el-col>
          </el-row>
          <input v-model="temp.answer" type="hidden" name="multipleChoiceAnswer">
          <el-checkbox-group v-model="temp.answerArr">
            <el-form-item v-for="(item, index) in temp.optionContentArr" :key="item.key" label="选项">
              <el-checkbox :label="alphas[index]" />
              <el-input v-model="item.value" />
            </el-form-item>
          </el-checkbox-group>
          <el-form-item>
            <el-button class="el-icon-plus" type="primary" plain @click="addQuestionOption()"> 添加选项</el-button>
            <el-button v-if="temp.optionContentArr.length > 4" class="el-icon-minus" type="danger" @click="minusQuestionOption()"> 删除选项</el-button>
          </el-form-item>
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
          <el-input v-model="temp.analysis" type="textarea" :autosize="{ minRows: 2, maxRows: 4}" />
          <!-- <tinymce v-model="temp.analysis" :height="150" /> -->
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
      alphas: ['A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'],
      radio: 'A',
      temp: {
        id: undefined,
        questionCategoryId: undefined,
        practicalTrainingId: undefined,
        type: 2,
        name: '123',
        content: undefined,
        optionContent: undefined,
        optionContentArr: ['', '', '', ''],
        optionSize: undefined,
        answer: undefined,
        answerArr: [],
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
      useEditor: false,
      // 表单验证规则
      rules: {
        name: [
          {
            required: false,
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
      this.temp.optionContentArr.push({ value: '' })
    },

    minusQuestionOption() {
      if (this.temp.optionContentArr.length <= 4) {
        this.$message.error('至少保留4个选项！')
        return
      }
      this.temp.optionContentArr.pop({ value: '' })
    },

    insertBlankFill() {

    },

    // 重置表单
    resetTemp() {
      this.temp = {
        id: undefined,
        questionCategoryId: undefined,
        practicalTrainingId: undefined,
        type: 2,
        name: '123',
        content: undefined,
        optionContent: undefined,
        optionContentArr: [{ value: '' }, { value: '' }, { value: '' }, { value: '' }],
        optionSize: undefined,
        answer: undefined,
        answerArr: [],
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
          var arr = []
          this.temp.optionContentArr.forEach(e => {
            arr.push(e.value)
          })
          this.temp.optionContent = arr.join('\r\n')
          debugger
          console.log('this.temp.QuestionType', this.temp.QuestionType)
          if (this.temp.QuestionType === QuestionType.MultipleChoice) {
            var answer1 = this.temp.answerArr.join('')
            this.temp.answer = answer1
          }
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
        // TODO:重构代码，简化，变量重命名 题目选项拆分处理
        var tempOptionConentArr = response.optionContent.split(/[(\r\n)\r\n]+/) // 根据换行或者回车进行识别
        var resultArr = []
        if (tempOptionConentArr.length < 4) {
          resultArr = [{ value: '' }, { value: '' }, { value: '' }, { value: '' }]
        } else {
          tempOptionConentArr.forEach(element => {
            resultArr.push({ value: element })
          })
        }
        response.optionContentArr = resultArr

        if (this.temp.type === QuestionType.MultipleChoice) {
          this.temp.answerArr = response.answer.split('')
        }
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
          var arr = []
          this.temp.optionContentArr.forEach(e => {
            arr.push(e.value)
          })
          this.temp.optionContent = arr.join('\r\n')
          if (this.temp.type === QuestionType.MultipleChoice) {
            var answer1 = this.temp.answerArr.join('')
            this.temp.answer = answer1
          }
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

