<template>
  <div class="app-container">
    <el-dialog :title=" dialogStatus == 'create'? $t('AppQuestionBank[\'Permission:Create\']'): $t('AbpUi[\'Edit\']')" :visible.sync="dialogFormVisible" top="4vh" width="70%">
      <el-form ref="dataForm" :rules="rules" :model="temp" label-position="right" label-width="150px">
        <el-row>
          <el-col :span="12">
            <el-form-item :label="$t('AppQuestionBank[\'DisplayName:QuestionCateogryName\']')" prop="questionCategoryId">
              <el-cascader v-model="temp.questionCategoryId" :options="questionCategoryOptions" :props="{ checkStrictly: true, value:'id', label:'name',children:'children',emitPath:false}" clearable class="filter-item" filterable style="width:100%;" />
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item prop="type" :label="$t('AppQuestionBank[\'DisplayName:Type\']')">
              <el-select v-model="temp.type" placeholder="-" filterable>
                <el-option
                  v-for="item in questionTypeOptions"
                  :key="item.key"
                  :label="item.lable"
                  :value="item.value"
                />
              </el-select>
            </el-form-item>
          </el-col>
        </el-row>

        <el-form-item :label="$t('AppQuestionBank[\'DisplayName:Content\']')" prop="content">
          <el-checkbox v-model="useEditor">使用富文本编辑器</el-checkbox>
          <!-- bugfix:tinymce每次查看详情内容不更新问题  https://blog.csdn.net/qq_39367226/article/details/103186116 -->
          <tinymce v-if="useEditor && dialogFormVisible" ref="editor" v-model="temp.content" :height="300" @blur="onInputBlur" />
          <el-input v-else v-model="temp.content" type="textarea" :autosize="{ minRows: 4, maxRows: 6}" @blur="onInputBlur" />
          <div v-if="temp.type === QuestionType.Completion" style="margin-top: 10px;">
            <span>请在填空位置</span>   <el-button type="primary" @click="insertBlankFill()">插入填空符</el-button>
          </div>
        </el-form-item>

        <el-form-item v-if="temp.type === QuestionType.TrueOrFalse" label="选项" prop="name">
          <el-radio v-model="temp.answer" label="true"> A.  正确</el-radio>
          <el-radio v-model="temp.answer" label="false">B.  错误</el-radio>
        </el-form-item>

        <!-- 单选题 -->
        <div v-if="temp.type === QuestionType.SingleChoice" class="single-option-container">
          <el-row style="" class="option-header">
            <el-col :span="4">勾选设置答案</el-col>
            <el-col :span="20">选项内容</el-col>
          </el-row>
          <el-form-item v-for="(item,index) in optionContentArr" :key="item.key" label="选项">
            <el-radio v-model="temp.answer" :label="alphas[index]" />
            <el-input v-model="item.value" />
          </el-form-item>
          <el-form-item>
            <el-button class="el-icon-plus" type="primary" plain @click="addQuestionOption()"> 添加选项</el-button>
            <el-button v-if="optionContentArr.length > 4" class="el-icon-minus" type="danger" @click="minusQuestionOption()"> 删除选项</el-button>
          </el-form-item>
        </div>

        <!-- 多选题 -->
        <div v-if="temp.type === QuestionType.MultipleChoice" class="multi-contariner">
          <el-row style="" class="option-header">
            <el-col :span="4">勾选设置答案</el-col>
            <el-col :span="20">选项内容</el-col>
          </el-row>
          <input v-model="temp.answer" type="hidden" name="multipleChoiceAnswer">
          <el-checkbox-group v-model="answerArr">
            <el-form-item v-for="(item, index) in optionContentArr" :key="item.key" label="选项">
              <el-checkbox :label="alphas[index]" />
              <el-input v-model="item.value" />
            </el-form-item>
          </el-checkbox-group>
          <el-form-item>
            <el-button class="el-icon-plus" type="primary" plain @click="addQuestionOption()"> 添加选项</el-button>
            <el-button v-if="optionContentArr.length > 4" class="el-icon-minus" type="danger" @click="minusQuestionOption()"> 删除选项</el-button>
          </el-form-item>
        </div>

        <!-- 填空题 -->
        <el-form-item v-if="temp.type === QuestionType.Completion || temp.type === QuestionType.QA || temp.type === QuestionType.ShortAnswer" :label="$t('AppQuestionBank[\'DisplayName:Answer\']')" prop="answer" class="answer">
          <div v-if="temp.type === QuestionType.Completion">
            <!-- 限制填空题答案输入框中不允许| 这个是答案拆分的分隔符 -->
            <el-input v-for="(item, index) in completionAnswerArr" :key="item.key" v-model="item.value" placeholder="请输入内容">
              <template slot="prepend">填空{{ index + 1 }}答案</template>
              <el-button slot="append" icon="el-icon-close" @click="completionAnswerArr.splice(index,1)" />
            </el-input>
            <el-row>
              <el-button type="primary" style="margin-right:10px;" @click="addCompletionAnswer()">增加填空题答案</el-button>
              <el-checkbox>判分时不区分答案先后顺序</el-checkbox>
              <el-checkbox>只要匹配答案的部分关键字就可得分</el-checkbox>
            </el-row>
          </div>
          <div v-if="temp.type === QuestionType.QA || temp.type === QuestionType.ShortAnswer">
            <el-input v-model="temp.answer" type="textarea" :autosize="{ minRows: 2, maxRows: 4}" />
          </div>
        </el-form-item>
        <el-form-item v-if="temp.type === QuestionType.Completion || temp.type === QuestionType.QA || temp.type === QuestionType.PracticalTraining" :label="$t('AppQuestionBank[\'DisplayName:TrainPlatformUrl\']')">
          <el-select v-model="temp.trainPlatformId" placeholder="-" filterable clearable>
            <el-option
              v-for="item in trainPlatformOptions"
              :key="item.id"
              :label="item.name"
              :value="item.id"
            />
          </el-select>
          <el-input v-model="temp.trainPlatformPath" type="text" style="width: 60%;" placeholder="请输入path：路径，由零或多个‘/’隔开的字符串，一般用来表示主机上的一个目录或者文件地址。" />
        </el-form-item>

        <el-row>
          <el-col :span="12">
            <el-form-item :label="$t('AppQuestionBank[\'DisplayName:Score\']')" prop="score">
              <el-input-number v-model="temp.score" :step="5" :min="0" :max="100" :precision="2" label="题目分数" />
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
import { getAllQuestionCategory } from '@/api/question-bank/question-category'
import { getAllTrainPlatform } from '@/api/question-bank/train-platform'
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
      trainPlatformOptions: [],
      questionTypeOptions: Type,
      questionDegreeOptions: Degree,
      alphas: ['A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'],
      optionContentArr: ['', '', '', ''], // 选项数组
      answerArr: [], // 多选题答案数组
      completionAnswerArr: [], // 填空题答案数组
      cursorIndex: '', // 光标位置
      radio: 'A',
      temp: {
        id: undefined,
        questionCategoryId: undefined,
        practicalTrainingId: undefined,
        type: 1,
        name: '123',
        content: undefined,
        optionContent: undefined,
        optionSize: undefined,
        answer: undefined,
        trainPlatformId: undefined,
        trainPlatformPath: undefined,
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
      useEditor: true,
      // 表单验证规则
      rules: {
        questionCategoryId: [
          {
            required: true,
            message: this.$i18n.t("AbpValidation['The {0} field is required.']", [this.$i18n.t("AppQuestionBank['DisplayName:QuestionCateogryName']")]),
            trigger: 'blur'
          }
        ],
        type: [
          {
            required: true,
            message: this.$i18n.t("AbpValidation['The {0} field is required.']", [this.$i18n.t("AppQuestionBank['DisplayName:Type']")]),
            trigger: 'blur'
          }
        ],
        content: [
          {
            required: true,
            message: this.$i18n.t("AbpValidation['The {0} field is required.']", [this.$i18n.t("AppQuestionBank['DisplayName:Content']")]),
            trigger: 'blur'
          },
          {
            max: 2048,
            message: this.$i18n.t("AbpValidation['The field {0} must be a string with a maximum length of {1}.']", [this.$i18n.t("AppQuestionBank['DisplayName:Content']"), '2048']),
            trigger: 'blur'
          }
        ],
        analysis: [
          {
            max: 512,
            message: this.$i18n.t("AbpValidation['The field {0} must be a string with a maximum length of {1}.']", [this.$i18n.t("AppQuestionBank['DisplayName:Analysis']"), '512']),
            trigger: 'blur'
          }
        ],
        source: [
          {
            max: 256,
            message: this.$i18n.t("AbpValidation['The field {0} must be a string with a maximum length of {1}.']", [this.$i18n.t("AppQuestionBank['DisplayName:Source']"), '256']),
            trigger: 'blur'
          }
        ]
      }
    }
  },
  created() {
  },
  methods: {
    checkPermission,
    fetchQuestionCategoryOptions() {
      getAllQuestionCategory(baseListQuery).then(response => {
        this.questionCategoryOptions = listToTree(response.items)
      })

      getAllTrainPlatform().then(response => {
        this.trainPlatformOptions = response.items
      })
    },

    // 添加题目选项
    addQuestionOption() {
      // debugger
      this.optionContentArr.push({ value: '' })
    },
    // 移除题目选项
    minusQuestionOption() {
      if (this.optionContentArr.length <= 4) {
        this.$message.error('至少保留4个选项！')
        return
      }
      this.optionContentArr.pop({ value: '' })
    },
    addCompletionAnswer() {
      this.completionAnswerArr.push({ value: '' })
    },
    // 获取文本框的光标位置
    onInputBlur(e) {
      this.cursorIndex = e.srcElement.selectionStart
    },

    /**
     * 点击按钮插入填空符
     * 参考：element ui - el-input 实现点击插入关键词功能 https://blog.csdn.net/qq_38374286/article/details/132909372
     * */
    insertBlankFill(item = ' ___ ') {
      // 将文本内容在光标位置进行拆分
      const txt = this.temp.content ?? ''
      const start = txt.substring(0, this.cursorIndex)
      const end = txt.substring(this.cursorIndex, txt.length)

      // 插入关键词
      this.temp.content = start + `${item}` + end
      this.completionAnswerArr.push({ value: '' })

      // 获取文本框，设置焦点，处理光标位置
      if (this.$refs[this.targetInputName]) {
        this.$refs[this.targetInputName].focus()
        this.$nextTick(() => {
          const input = this.$refs[this.targetInputName].$el.firstElementChild
          input.focus()
          const addIndex = item.length
          input.selectionStart = this.cursorIndex + addIndex
          input.selectionEnd = this.cursorIndex + addIndex
        })
      }
    },

    // 重置表单
    resetTemp() {
      this.temp = {
        id: undefined,
        questionCategoryId: undefined,
        practicalTrainingId: undefined,
        type: 1,
        content: undefined,
        optionContent: undefined,
        optionSize: undefined,
        trainPlatformId: undefined,
        trainPlatformPath: undefined,
        answer: undefined,
        score: 10,
        degree: 0,
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

      this.optionContentArr = ['', '', '', '']
      this.answerArr = []
      this.completionAnswerArr = [] // 填空题答案数组
    },

    // 点击创建按钮
    handleCreate() {
      debugger
      this.fetchQuestionCategoryOptions()
      this.resetTemp()
      this.dialogStatus = 'create'
      this.dialogFormVisible = true
      this.$nextTick(() => {
        this.$refs['dataForm'].clearValidate()
      })
    },
    // 模型数据转换
    transferModel() {
      var arr = []
      this.optionContentArr.forEach(e => {
        arr.push(e.value)
      })
      this.temp.optionContent = arr.join('\r\n')

      if (this.temp.type === QuestionType.MultipleChoice) {
        this.temp.answer = this.answerArr.join('')
      }

      if (this.temp.type === QuestionType.Completion) {
        this.temp.answer = this.answerArr.join('|')
      }
    },
    // 创建数据
    createData() {
      this.$refs['dataForm'].validate(valid => {
        if (valid) {
          this.transferModel()
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
        debugger
        // 拆分显示选项
        var tempOptionConentArr = response.optionContent.split(/[(\r\n)\r\n]+/) // 根据换行或者回车进行识别
        var resultArr = []
        if (tempOptionConentArr.length < 4) {
          resultArr = [{ value: '' }, { value: '' }, { value: '' }, { value: '' }]
        } else {
          tempOptionConentArr.forEach(element => {
            resultArr.push({ value: element })
          })
        }
        this.optionContentArr = resultArr

        // 多选题答案显示
        if (this.temp.type === QuestionType.MultipleChoice) {
          if (response.answer == null) {
            response.answer = 'A'
          }
          var checkList = response.answer.split('')
          this.answerArr = checkList
        }

        // 填空题答案显示
        if (this.temp.type === QuestionType.Completion) {
          var completionAnswerArr = response.answer.split('|')
          completionAnswerArr.forEach(e => {
            this.completionAnswerArr.push({ value: e })
          })
        }
      })

      this.$nextTick(() => {
        this.$refs['dataForm'].clearValidate()
      })
    },
    // 更新数据
    updateData() {
      this.$refs['dataForm'].validate(valid => {
        if (valid) {
          this.transferModel()
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

