<template>
  <div class="fixed-paper-section-container">
    <div>
      <!--拖拽table使用 https://panjiachen.github.io/vue-element-admin/#/table/drag-table -->
      <el-table
        :key="tableKey"
        v-loading="listLoading"
        :data="list"
        style="width: 100%"
        border
        fit
        highlight-current-row
        :stripe="true"
        @sort-change="sortChange"
      >
        <!-- <el-table-column type="selection" width="55" center /> -->
        <el-table-column type="index" label="序号" width="80" />
        <el-table-column prop="questionType" label="试题类型" width="180">
          <span>单选题</span>
        </el-table-column>
        <el-table-column prop="questionCategory" label="试题内容" show-overflow-tooltip>
          <span>试题内容</span>
        </el-table-column>
        <el-table-column prop="questionCount" label="标准答案" width="110">
          <b>ABC</b>
        </el-table-column>
        <el-table-column prop="scorePerQuestion" label="每题分数" width="110">
          <el-input value="15" />
        </el-table-column>
        <el-table-column label="操作" width="80" align="center">
          <el-button type="text" class="el-icon-delete" :title="$t('AbpUi[\'Delete\']')" />
        </el-table-column>
      </el-table>
    </div>
    <el-row>
      <el-dropdown type="primary" @command="handleCommand">
        <el-button type="primary" style="margin-top: 15px;">
          从题库中选择<i class="el-icon-arrow-down el-icon--right" />
        </el-button>
        <el-dropdown-menu slot="dropdown">
          <el-dropdown-item command="handleManualSelentQuestions">
            <i class="el-icon-circle-plus" />手工选题
          </el-dropdown-item>
          <el-dropdown-item command="handleRandomSelentQuestions">
            <i class="el-icon-refresh" /> 随机选题
          </el-dropdown-item>
        </el-dropdown-menu>
      </el-dropdown>
    </el-row>

    <el-dialog
      title="随机选题"
      :visible.sync="dialogRandomSelentQuestionVisible"
      width="30%"
      append-to-body
    >
      <el-form label-width="80px" :model="randomSelentQuestionForm">
        <el-form-item label="试题分类">
          <el-cascader
            v-model="randomSelentQuestionForm.questionCategoryId"
            :options="questionCategoryOptions"
            :props="{ checkStrictly: true, value:'id', label:'name',children:'children',emitPath:false}"
            placeholder="-"
            style="width:230px;"
            clearable
            filterable
          />
        </el-form-item>
        <el-form-item label="试题类型">
          <el-select v-model="randomSelentQuestionForm.questionTypeId" placeholder="-" filterable clearable>
            <el-option
              v-for="item in typeOptions"
              :key="item.key"
              :label="item.lable"
              :value="item.value"
            />
          </el-select>
        </el-form-item>
        <el-form-item label="抽题数量" class="number-of-questions">
          <el-input v-model="randomSelentQuestionForm.unlimitedDifficultyCount" placeholder="不限">
            <template slot="append">20</template>
          </el-input>
          <el-input v-model="randomSelentQuestionForm.easyCount" placeholder="简单">
            <template slot="append">20</template>
          </el-input>
          <el-input v-model="randomSelentQuestionForm.ordinaryCount" placeholder="普通">
            <template slot="append">20</template>
          </el-input>
          <el-input v-model="randomSelentQuestionForm.difficultCount" placeholder="困难">
            <template slot="append">20</template>
          </el-input>
        </el-form-item>
        <el-form-item label="每题分数">
          <el-input v-model="randomSelentQuestionForm.scorePerQuestion" />
        </el-form-item>
      </el-form>
      <span slot="footer" class="dialog-footer">
        <el-button @click="dialogRandomSelentQuestionVisible = false">取 消</el-button>
        <el-button type="primary" @click="dialogRandomSelentQuestionVisible = false">确 定</el-button>
      </span>
    </el-dialog>

    <question-select ref="QuestionSelect" :test-paper-id="testPaperId" :test-paper-section-id="testPaperSectionId" />
  </div>

</template>

<script>
import baseListQuery, { checkPermission } from '@/utils/abp'
import {
  getTestPaperQuestions,
  getAllTestPaperQuestion,
  deleteTestPaperQuestion
} from '@/api/exam/test-paper-question'
import { Type } from '@/views/question-bank/question/datas/typing'
import { getAllQuestionCategory } from '@/api/question-bank/question-category'
import { listToTree } from '@/utils/helpers/tree-helper'
import QuestionSelect from './QuestionSelect.vue'

export default {
  name: 'RandomPaperSection', // 随机试卷大题
  components: {
    QuestionSelect
  },
  props: {
    testPaperId: {
      type: String,
      require: true,
      default: undefined
    },
    testPaperSectionId: {
      type: String,
      require: true,
      default: undefined
    }
  },
  data() {
    return {
      typeOptions: Type,
      questionCategoryOptions: [],
      questionCategoryId: undefined,
      dialogRandomSelentQuestionVisible: false,
      tableData: [
        {
          date: '2016-05-02',
          name: '王小虎',
          address: '上海市普陀区金沙江路 1518 弄'
        }

      ],
      randomSelentQuestionForm: {
        questionCategoryId: undefined,
        questionTypeId: undefined,
        unlimitedDifficultyCount: undefined,
        easyCount: undefined,
        ordinaryCount: undefined,
        difficultCount: undefined,
        scorePerQuestion: undefined
      },
      tableKey: 0,
      list: null,
      total: 0,
      listLoading: true,
      listQuery: Object.assign({
        degree: undefined,
        questionCategoryId: undefined,
        createStartTime: undefined,
        createEndTime: undefined,
        type: undefined,
        enable: undefined
      }, baseListQuery)
    }
  },
  created() {
    this.fetchOptions()
    this.getListByTestPaperSectionId()
  },
  methods: {
    checkPermission,
    fetchOptions() {
      getAllQuestionCategory().then(response => {
        this.questionCategoryOptions = listToTree(response.items)
      })
    },
    // 获取列表数据
    getList() {
      this.listLoading = true
      this.listQuery.limit = 1000
      getTestPaperQuestions(this.listQuery).then(response => {
        this.list = response.items
        this.total = response.totalCount
        this.listLoading = false
      })
    },
    handleFilter(firstPage = true) {
      if (firstPage) this.listQuery.page = 1
      this.getList()
    },
    sortChange(data) {
      const {
        prop,
        order
      } = data
      this.listQuery.sort = order ? `${prop} ${order}` : undefined
      this.handleFilter()
    },
    getListByTestPaperSectionId() {
      this.listLoading = true
      getAllTestPaperQuestion(this.testPaperSectionId).then(response => {
        this.list = response.items
        this.listLoading = false
      })
    },
    handleCommand(command) {
      switch (command) {
        case 'handleRandomSelentQuestions':
          this.handleRandomSelentQuestions()
          break
        case 'handleManualSelentQuestions':
          this.handleManualSelentQuestions()
          break
        default:
          break
      }
    },
    handleManualSelentQuestions() {
      this.$refs['QuestionSelect'].getList()
    },
    handleRandomSelentQuestions() {
      this.dialogRandomSelentQuestionVisible = true
    }
  }
}
</script>

<style lang="scss" scoped>

.question-count{
  width: 65%;
}
.number-of-questions .el-input{
 width: 50%;
}
</style>
