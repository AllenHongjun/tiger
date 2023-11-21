<template>
  <el-card class="box-card">
    <div slot="header" class="clearfix">
      <span>第1大题 </span><span>(共14题90.0分)</span>
      <div style="float: right;">
        <el-button plain type="primary" class="el-icon-plus">添加大题描述</el-button>
        <el-button plain>选项乱序</el-button>
      </div>
    </div>
    <div>
      <!--拖拽table使用 https://panjiachen.github.io/vue-element-admin/#/table/drag-table -->
      <el-table :data="tableData" style="width: 100%">
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
      <el-dropdown split-button type="primary" style="margin-top: 15px;" @command="handleCommand">
        从题库中选择
        <el-dropdown-menu slot="dropdown">
          <el-dropdown-item>手工选题</el-dropdown-item>
          <el-dropdown-item :command="beforeHandleCommand('handleRandomSelentQuestions')">随机选题</el-dropdown-item>
        </el-dropdown-menu>
      </el-dropdown>
    </el-row>

    <el-dialog
      title="随机选题"
      :visible.sync="dialogRandomSelentQuestionVisible"
      width="30%"
      append-to-body
      :before-close="handleClose"
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
  </el-card>

</template>

<script>
import { Type } from '@/views/question-bank/question/datas/typing'
import { getAllQuestionCategory } from '@/api/question-bank/question-category'
import { listToTree } from '@/utils/helpers/tree-helper'

export default {
  name: 'RandomPaperSection', // 随机试卷大题
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
        },
        {
          date: '2016-05-04',
          name: '王小虎',
          address: '上海市普陀区金沙江路 1517 弄'
        },
        {
          date: '2016-05-01',
          name: '王小虎',
          address: '上海市普陀区金沙江路 1519 弄'
        },
        {
          date: '2016-05-03',
          name: '王小虎',
          address: '上海市普陀区金沙江路 1516 弄'
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
      }
    }
  },
  created() {
    this.fetchOptions()
  },
  methods: {
    fetchOptions() {
      getAllQuestionCategory().then(response => {
        this.questionCategoryOptions = listToTree(response.items)
      })
    },

    beforeHandleCommand(command) {
      return {
        command: command
      }
    },
    handleCommand(param) {
      switch (param.command) {
        case 'handleRandomSelentQuestions':
          this.handleRandomSelentQuestions()
          break
        default:
          break
      }
    },
    handleRandomSelentQuestions() {
      this.dialogRandomSelentQuestionVisible = true
    }
  }
}
</script>

<style scoped>
.line{
  text-align: center;
}
.box-card{
  margin-bottom: 8px;
}
.question-count{
  width: 65%;
}
.number-of-questions .el-input{
 width: 50%;
}
</style>
