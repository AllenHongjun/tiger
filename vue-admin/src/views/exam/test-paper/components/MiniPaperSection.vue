<template>
  <div class="mini-paper-serction-container">
    <el-card class="mini-paper-serction-card">
      <div slot="header" class="clearfix">
        <el-dropdown type="primary" @command="handleCommand">
          <el-button type="primary">
            添加试卷大题<i class="el-icon-arrow-down el-icon--right" />
          </el-button>
          <el-dropdown-menu slot="dropdown">
            <el-dropdown-item command="createFixedTestPaperSection"><i class="el-icon-circle-plus" />添加固定试卷大题</el-dropdown-item>
            <el-dropdown-item command="createRandomTestPaperSection"><i class="el-icon-refresh" />添加随机试卷大题</el-dropdown-item>
          </el-dropdown-menu>
          <span style="margin-left:10px;">(共 <b>0</b>   题 <b>0</b> 分)</span>
        </el-dropdown>
      </div>
      <div v-for="(testPaperSection, index) in testPaper.testPaperSections" :key="index" class="mini-paper-section">
        <el-row class="mini-paper-section-header">
          <el-col :span="12">
            <h3 class="section-title">第1大题 <span>(共 <b>0</b>  题 <b>0</b>  分)</span></h3>
          </el-col>
          <el-col :span="12" :offset="0">
            <el-button-group style="float:right;margin-top:10px;">
              <el-button type="info" icon="el-icon-bottom" title="下移" />
              <el-button type="info" icon="el-icon-edit" title="批量修改分数" />
              <el-button type="info" icon="el-icon-delete" title="删除大题" />
            </el-button-group>
          </el-col>
        </el-row>
        <el-row class="mini-paper-section-body">
          <el-button v-for="(question, index) in testPaperSection.questionCount" :key="index" type="info" plain>{{ index + 1 }}</el-button>
        </el-row>
      </div>

    </el-card>
  </div>
</template>

<script>
import {
  getTestPaper,
  createTestPaper,
  updateTestPaper

} from '@/api/exam/test-paper'

import {
  getTestPaperSection,
  createTestPaperSection,
  updateTestPaperSection
} from '@/api/exam/test-paper-section'
import { switchCase } from '@babel/types'

export default {
  name: 'MiniPaperSection',
  props: {
    testPaper: {
      type: Object,
      require: false,
      // 对象或数组默认值必须从一个工厂函数获取
      default: function() {
        return {}
      }
    }
  },
  data() {
    return {
      testPaperId: '6E25C8D0-07A4-EB6C-DE2B-3A0ED6499A28',
      testPaperSectionModel: {
        testPaperId: undefined,
        name: undefined,
        description: undefined,
        questionCount: 0,
        totalScore: 0,
        sort: 0
      },
      testPaperSections: [
        {
          testPaperId: undefined,
          name: undefined,
          description: undefined,
          questionCount: 20,
          totalScore: 0,
          sort: 0
        }

      ]
    }
  },
  methods: {
    handleCommand(command) {
      this.$message('click on item ' + command)
      switch (command) {
        case 'createFixedTestPaperSection':
          this.createData(1)
          break
        case 'createRandomTestPaperSection':
          this.createData(2)
          break
        default:
          break
      }
    },
    // 添加大题
    createData(type) {
      this.testPaperSectionModel.testPaperId = this.testPaper.id
      this.testPaperSectionModel.name = '第一大题'
      this.testPaperSectionModel.type = type
      this.testPaperSectionModel.questionCount = 10
      this.testPaperSectionModel.totalScore = 0
      this.testPaperSectionModel.sort = 1

      // getTestPaper(this.testPaperId).then(response => {
      //   this.testPaperSections = response.testPaperSection
      // })

      createTestPaperSection(this.testPaperSectionModel).then(() => {
        this.$nextTick(() => {
          getTestPaper(this.testPaper.id).then(response => {
            this.testPaper.testPaperSections = response.testPaperSections
          })
        })
      })
    }
  }
}
</script>

<style lang="scss" scoped>
.mini-paper-serction-card{
  min-height: 600px;
}

.section-title{
  font-size: 14px;
  font-weight: normal;
  display: inline-block;
  span{
    font-size: 12px;
  }
}

.mini-paper-section{
  margin-top: 15px;
}

.mini-paper-section-body .el-button:first-child{
  margin-left: 10px;
}
.mini-paper-section-body .el-button{
  min-width: 50px;
  margin-top: 5px;;
}
</style>

