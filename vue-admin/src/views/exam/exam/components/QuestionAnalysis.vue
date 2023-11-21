<template>
  <div class="table-container">

    <div class="filter-container" style="margin-bottom:10px;">
      <el-form ref="logQueryForm" label-position="left" label-width="80px" :model="listQuery">
        <el-row :gutter="20">
          <el-col :span="4">
            <el-form-item prop="degree" :label="$t('AppQuestionBank[\'DisplayName:Degree\']')">
              <el-select v-model="listQuery.degree" placeholder="-" filterable clearable>
                <el-option
                  v-for="item in degreeOptions"
                  :key="item.key"
                  :label="item.lable"
                  :value="item.value"
                />
              </el-select>
            </el-form-item>
          </el-col>

          <el-col :span="4">
            <el-form-item :label="$t('AppQuestionBank[\'DisplayName:Type\']')" prop="jobType">
              <el-select v-model="listQuery.type" placeholder="-" filterable clearable>
                <el-option
                  v-for="item in typeOptions"
                  :key="item.key"
                  :label="item.lable"
                  :value="item.value"
                />
              </el-select>
            </el-form-item>
          </el-col>

          <el-col :span="5">
            <el-form-item prop="questionCategoryId" :label="$t('AppQuestionBank[\'DisplayName:QuestionCateogryName\']')">
              <el-cascader
                v-model="listQuery.questionCategoryId"
                :options="questionCategoryOptions"
                :props="{ checkStrictly: true, value:'id', label:'name',children:'children',emitPath:false}"
                placeholder="-"
                style="width:230px;"
                clearable
                filterable
              />
            </el-form-item>
          </el-col>

          <el-col :span="4">
            <el-button-group>
              <el-button type="primary" class="filter-item" icon="el-icon-search" @click="handleFilter">
                {{ $t('AbpUi.Search') }}
              </el-button>
              <el-button type="reset" icon="el-icon-remove-outline" @click="resetQueryForm">
                {{ $t('AbpAuditLogging.Reset') }}
              </el-button>
            </el-button-group>
          </el-col>
        </el-row>

        <el-collapse-transition>
          <div v-show="advanced">
            <el-row :gutter="20">
              <el-col :span="4" />
            </el-row>
          </div>
        </el-collapse-transition>
      </el-form>

      <!-- 操作按钮 -->
      <el-row>
        <el-col>
          <el-button-group style="float:left">
            <el-button icon="el-icon-download" @click="handleDownload">
              导出
            </el-button>
          </el-button-group>

        </el-col>
      </el-row>
    </div>

    <el-table :key="tableKey" v-loading="listLoading" :data="list" border fit highlight-current-row :stripe="true" style="width: 100%;" @sort-change="sortChange">
      <el-table-column type="selection" width="55" center />
      <el-table-column type="index" width="50" />
      <el-table-column label="试题内容" prop="name" align="left" width="380" :show-overflow-tooltip="true">
        <template slot-scope="{ row }">
          <span>三国时期是指那三国？</span>
        </template>
      </el-table-column>

      <el-table-column label="题型" prop="description" align="left">
        <template slot-scope="{ row }">
          <span>单选题</span>
        </template>
      </el-table-column>
      <el-table-column label="试题分类" prop="path" align="left">
        <template slot-scope="{ row }">
          <span>实操题/中级</span>
        </template>
      </el-table-column>
      <el-table-column label="难度" prop="path" align="left">
        <template slot-scope="{ row }">
          <span>容易的</span>
        </template>
      </el-table-column>
      <el-table-column label="答案" prop="path" align="left">
        <template slot-scope="{ row }">
          <span>魏蜀吴</span>
        </template>
      </el-table-column>
      <el-table-column label="答题次数" prop="path" align="left">
        <template slot-scope="{ row }">
          <span>12</span>
        </template>
      </el-table-column>
      <el-table-column label="答错次数" prop="path" align="left">
        <template slot-scope="{ row }">
          <span>6</span>
        </template>
      </el-table-column>
      <el-table-column label="错误率" prop="path" align="left">
        <template slot-scope="{ row }">
          <span>6%</span>
        </template>
      </el-table-column>
      <el-table-column label="答对次数" prop="path" align="left">
        <template slot-scope="{ row }">
          <span>16</span>
        </template>
      </el-table-column>
      <el-table-column label="正确率" prop="path" align="left">
        <template slot-scope="{ row }">
          <span>76%</span>
        </template>
      </el-table-column>

      <el-table-column label="得分率" prop="path" align="left">
        <template slot-scope="{ row }">
          <span>23%</span>
        </template>
      </el-table-column>
    </el-table>

    <pagination v-show="total > 0" :total="total" :page.sync="listQuery.page" :limit.sync="listQuery.limit" @pagination="getList" />

  </div>
</template>

<script>
import baseListQuery, { checkPermission } from '@/utils/abp'
import { getAllQuestionCategory } from '@/api/question-bank/question-category'
import { listToTree } from '@/utils/helpers/tree-helper'
import { pickerRangeWithHotKey } from '@/utils/picker'
import {
  getLayouts,
  deleteLayout
} from '@/api/system-manage/platform/layout'
import Pagination from '@/components/Pagination' // secondary package based on el-pagination
import ExamScorePanel from './ExamScorePanel.vue'
import { QuestionType, QuestionTypeMap, QuestionDegree, QuestionDegreeMap, Degree, Type } from '@/views/question-bank/question/datas/typing'

export default {
  name: 'QuestionAnalysis',
  components: {
    Pagination,
    ExamScorePanel
  },
  data() {
    return {
      degreeOptions: Degree,
      typeOptions: Type,
      queryCreateDateTime: undefined,
      advanced: false,
      pickerOptions: pickerRangeWithHotKey,
      tableKey: 0,
      list: null,
      total: 0,
      listLoading: true,
      listQuery: Object.assign({
        degree: undefined,
        questionCategoryId: undefined,
        createStartTime: undefined,
        createEndTime: undefined,
        type: 1,
        enable: undefined
      }, baseListQuery)

    }
  },
  created() {
    this.getList()
  },
  methods: {
    checkPermission, // 检查权限
    fetchOptions() {
      getAllQuestionCategory().then(response => {
        this.questionCategoryOptions = listToTree(response.items)
      })
    },
    datePickerChange(value) {
      if (!value) {
        // 日期选择器改变事件 ~ 解决日期选择器清空 值不清空的问题
        this.listQuery.createStartTime = undefined
        this.listQuery.createEndTime = undefined
      }
    },
    // 搜索展开切换
    toggleAdvanced() {
      this.advanced = !this.advanced
    },
    // 刷新页面
    handleRefresh() {
      this.handleFilter()
    },
    // 重置查询参数
    resetQueryForm() {
      this.queryCreateDateTime = undefined
      this.listQuery = Object.assign({
        degree: undefined,
        questionCategoryId: undefined,
        createStartTime: undefined,
        createEndTime: undefined,
        type: undefined,
        enable: undefined
      }, baseListQuery)
    },
    // 获取列表数据
    getList() {
      this.listLoading = true
      getLayouts(this.listQuery).then(response => {
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
    handleCreate() {
      this.$emit('handleCreate')
    },

    // 删除
    handleDelete(row, index) {
      this.$confirm(
        // 消息
        this.$i18n.t("AbpUi['ItemWillBeDeletedMessageWithFormat']", [
          row.name
        ]),
        // title
        this.$i18n.t("AbpUi['AreYouSure']"), {
          confirmButtonText: this.$i18n.t("AbpUi['Yes']"), // 确认按钮
          cancelButtonText: this.$i18n.t("AbpUi['Cancel']"), // 取消按钮
          type: 'warning' // 弹框类型
        }
      ).then(async() => {
        // 回调函数
        deleteLayout(row.id).then(() => {
          this.handleFilter(false)
          this.$notify({
            title: this.$i18n.t("TigerUi['Success']"),
            message: this.$i18n.t("TigerUi['SuccessMessage']"),
            type: 'success',
            duration: 2000
          })
        })
      })
    },
    // 下载数据
    handleDownload() {
      this.$alert('开发中...')
      return
    }
  }
}
</script>

<style scoped>
.line{
  text-align: center;
}
</style>

