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
      <el-table
        :key="tableKey"
        v-loading="listLoading"
        :data="list"
        border
        fit
        highlight-current-row
        :stripe="true"
        style="width: 100%;"
        @sort-change="sortChange"
      >
        <!-- <el-table-column type="selection" width="55" center /> -->
        <el-table-column type="index" label="序号" width="80" />
        <el-table-column prop="questionType" label="试题类型" width="180">
          <template slot-scope="{ row }">
            <el-select v-model="row.questionType" placeholder="-" filterable clearable @change="updateData(row)">
              <el-option
                v-for="item in typeOptions"
                :key="item.key"
                :label="item.lable"
                :value="item.value"
              />
            </el-select>
          </template>
        </el-table-column>
        <el-table-column prop="questionCategory" label="试题分类">
          <template slot-scope="{ row }">
            <el-cascader
              v-model="row.questionCategoryId"
              :options="questionCategoryOptions"
              :props="{ checkStrictly: true, value:'id', label:'name',children:'children',emitPath:false}"
              placeholder="-"
              style="width:230px;"
              clearable
              filterable
              @change="updateData(row)"
            />
          </template>

        </el-table-column>
        <el-table-column label="抽题数量设置" width="110" align="center">
          <el-table-column prop="unlimitedDifficultyCount" label="不限难度" width="110">
            <template slot-scope="{ row }">
              <el-input v-model="row.unlimitedDifficultyCount" placeholder="0" class="question-count" @blur="updateData(row)" /> / <span>{{ row.unlimitedDifficultyCount }}</span>
            </template>
          </el-table-column>
          <el-table-column prop="easyCount" label="容易" width="110">
            <template slot-scope="{ row }">
              <el-input v-model="row.easyCount" placeholder="0" class="question-count" @blur="updateData(row)" /> / <span>{{ row.easyCount }}</span>
            </template>
          </el-table-column>
          <el-table-column prop="ordinaryCount" label="普通" width="110">
            <template slot-scope="{ row }">
              <el-input v-model="row.ordinaryCount" placeholder="0" class="question-count" @blur="updateData(row)" /> / <span>{{ row.ordinaryCount }}</span>
            </template>
          </el-table-column>
          <el-table-column prop="difficultCount" label="困难" width="110">
            <template slot-scope="{ row }">
              <el-input v-model="row.difficultCount" placeholder="0" class="question-count" @blur="updateData(row)" /> / <span>{{ row.difficultCount }}</span>
            </template>
          </el-table-column>
        </el-table-column>
        <el-table-column prop="questionCount" label="抽题数" width="110">
          <template slot-scope="{ row }">
            <b>{{ row.displayName }}</b>
          </template>
        </el-table-column>
        <el-table-column prop="scorePerQuestion" label="每题分数" width="110">
          <template slot-scope="{ row }">
            <el-input v-model="row.scorePerQuestion" placeholder="0" class="question-count" @blur="updateData(row)" />
          </template>
        </el-table-column>
        <el-table-column label="操作" width="80" align="center">
          <template slot-scope="{ row }">
            <el-button type="text" class="el-icon-delete" :title="$t('AbpUi[\'Delete\']')" @click="handleDelete(row)" />
          </template>
        </el-table-column>
      </el-table>
    </div>
    <el-button type="primary" style="margin-top: 15px;" @click="createData()">添加随机规则</el-button>
  </el-card>

</template>

<script>
import baseListQuery, { checkPermission } from '@/utils/abp'
import {
  getTestPaperStrategies,
  createTestPaperStrategy,
  updateTestPaperStrategy,
  deleteTestPaperStrategy
} from '@/api/exam/test-paper-strategy'
import { Type } from '@/views/question-bank/question/datas/typing'
import { getAllQuestionCategory } from '@/api/question-bank/question-category'
import { listToTree } from '@/utils/helpers/tree-helper'

export default {
  name: 'RandomPaperSection', // 随机试卷大题
  props: {
    testPaperId: {
      type: String,
      require: true,
      // 对象或数组默认值必须从一个工厂函数获取
      default: undefined
    },
    testPaperSectionId: {
      type: String,
      require: true,
      // 对象或数组默认值必须从一个工厂函数获取
      default: undefined
    }
  },
  data() {
    return {
      typeOptions: Type,
      questionCategoryOptions: [],
      questionCategoryId: undefined,
      tableKey: 0,
      list: null,
      total: 0,
      listLoading: true,
      listQuery: Object.assign({
        testPaperSectionId: undefined
      }, baseListQuery),
      temp: {
        testPaperId: undefined,
        testPaperSectionId: undefined,
        questionCategoryId: undefined,
        questionType: 1,
        unlimitedDifficultyCount: 0,
        easyCount: 0,
        ordinaryCount: 0,
        difficultCount: 0,
        scorePerQuestion: 0
      }
    }
  },
  created() {
    this.fetchQuestionCategoryOptions()
    this.getList()
  },
  methods: {
    checkPermission,
    fetchQuestionCategoryOptions() {
      getAllQuestionCategory().then(response => {
        this.questionCategoryOptions = listToTree(response.items)
      })
    },
    // 获取列表数据
    getList() {
      this.listLoading = true
      this.listQuery.testPaperSectionId = this.testPaperSectionId
      // 增加默认时间排序
      if (!this.listQuery.sort) this.listQuery.sort = 'creationTime asc'
      getTestPaperStrategies(this.listQuery).then(response => {
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
    // 重置表单
    resetTemp() {
      this.temp = {
        testPaperId: this.testPaperId,
        testPaperSectionId: this.testPaperSectionId,
        questionCategoryId: '0A754243-E155-112F-7640-3A0EC1EF97E0',
        questionType: 1,
        unlimitedDifficultyCount: 0,
        easyCount: 0,
        ordinaryCount: 0,
        difficultCount: 0,
        scorePerQuestion: 0
      }
    },

    // 点击创建按钮
    handleCreate() {
      this.resetTemp()
      this.$nextTick(() => {
        this.$refs['dataForm'].clearValidate()
      })
    },

    // 创建数据
    createData() {
      // TODO:获取当前题库一级分类的第一分类id提交，如果第一个题库分类没有，就提示先添加题库
      // this.temp.questionCategoryId = '0A754243-E155-112F-7640-3A0EC1EF97E0'
      this.resetTemp()
      createTestPaperStrategy(this.temp).then(() => {
        this.handleFilter()
      })
    },

    // 更新数据
    updateData(row) {
      updateTestPaperStrategy(row.id, row).then(() => {
        this.handleFilter(false)
      })
    },

    // 删除
    handleDelete(row, index) {
      this.$confirm(
        // 消息
        this.$i18n.t("AbpUi['ItemWillBeDeletedMessage']"),
        // title
        this.$i18n.t("AbpUi['AreYouSure']"), {
          confirmButtonText: this.$i18n.t("AbpUi['Yes']"), // 确认按钮
          cancelButtonText: this.$i18n.t("AbpUi['Cancel']"), // 取消按钮
          type: 'warning' // 弹框类型
        }
      ).then(async() => {
        // 回调函数
        deleteTestPaperStrategy(row.id).then(() => {
          this.handleFilter(false)
        })
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
