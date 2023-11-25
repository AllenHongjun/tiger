<template>
  <el-card class="random-paper-section-box">
    <div slot="header" class="clearfix">
      <h3 class="section-title">第1大题 <span>(共 <b>0</b>  题 <b>0</b>  分)</span></h3>
      <div style="float: right;">
        <el-button plain type="primary" class="el-icon-plus">添加大题描述</el-button>
        <el-button plain>选项乱序</el-button>
      </div>
    </div>
    <div>
      <el-table :data="tableData" style="width: 100%">
        <!-- <el-table-column type="selection" width="55" center /> -->
        <el-table-column type="index" label="序号" width="80" />
        <el-table-column prop="questionType" label="试题类型" width="180">
          <el-select placeholder="-" filterable clearable>
            <el-option
              v-for="item in typeOptions"
              :key="item.key"
              :label="item.lable"
              :value="item.value"
            />
          </el-select>
        </el-table-column>
        <el-table-column prop="questionCategory" label="试题分类">
          <el-cascader
            v-model="questionCategoryId"
            :options="questionCategoryOptions"
            :props="{ checkStrictly: true, value:'id', label:'name',children:'children',emitPath:false}"
            placeholder="-"
            style="width:230px;"
            clearable
            filterable
          />
        </el-table-column>
        <el-table-column label="抽题数量设置" width="110" align="center">
          <el-table-column prop="unlimitedDifficultyCount" label="不限难度" width="110">
            <el-input placeholder="0" class="question-count" /> / <span>0</span>
          </el-table-column>
          <el-table-column prop="easyCount" label="容易" width="110">
            <el-input placeholder="0" class="question-count" /> / <span>0</span>
          </el-table-column>
          <el-table-column prop="ordinaryCount" label="中等" width="110">
            <el-input placeholder="0" class="question-count" /> / <span>0</span>
          </el-table-column>
          <el-table-column prop="difficultCount" label="困难" width="110">
            <el-input placeholder="0" class="question-count" /> / <span>0</span>
          </el-table-column>
        </el-table-column>
        <el-table-column prop="questionCount" label="抽题数" width="110">
          <b>0</b>
        </el-table-column>
        <el-table-column prop="scorePerQuestion" label="每题分数" width="110">3
        </el-table-column>
        <el-table-column label="操作" width="80" align="center">
          <el-button type="text" class="el-icon-delete" :title="$t('AbpUi[\'Delete\']')" />
        </el-table-column>
      </el-table>
    </div>
    <el-button type="primary" style="margin-top: 15px;">添加随机规则</el-button>
  </el-card>

</template>

<script>
import {
  getTestPaperStrategies,
  getTestPaperStrategy,
  createTestPaperStrategy,
  updateTestPaperStrategy,
  deleteTestPaperStrategy
} from '@/api/exam/test-paper'
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
        }

      ]
    }
  },
  created() {
    // this.fetchOptions()
  },
  methods: {
    fetchOptions() {
      getAllQuestionCategory().then(response => {
        this.questionCategoryOptions = listToTree(response.items)
      })
    }
  }
}
</script>

<style lang="scss" scoped>
.random-paper-section-box{
  margin-bottom: 15px;
}
.section-title{
  font-size: 14px;
  font-weight: normal;
  display: inline-block;
}
.question-count{
  width: 65%;
}
</style>
