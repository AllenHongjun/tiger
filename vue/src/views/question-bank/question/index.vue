<template>
  <div class="app-container">
    <div class="filter-container">
      <el-row style="margin-bottom: 20px">
        <el-input v-model="listQuery.filter" :placeholder="$t('AbpUi[\'PagerSearch\']')" style="width: 200px;" clearable class="filter-item" @keyup.enter.native="handleFilter" />
        <el-button type="primary" class="filter-item" icon="el-icon-search" @click="handleFilter">
          {{ $t('AbpUi.Search') }}
        </el-button>
        <el-button v-if="checkPermission('QuestionBank.Question.Create')" class="filter-item" style="margin-left: 10px;" type="primary" icon="el-icon-plus" @click="handleCreate">
          {{ $t("AppQuestionBank['Permission:Create']") }}
        </el-button>
      </el-row>
    </div>

    <el-table :key="tableKey" v-loading="listLoading" :data="list" border fit highlight-current-row :stripe="true" style="width: 100%;" @sort-change="sortChange">
      <el-table-column type="selection" width="55" center />
      <el-table-column type="index" width="60" />
      <!-- <el-table-column :label="$t('AppQuestionBank[\'DisplayName:Name\']')" prop="name" sortable align="left" width="180">
        <template slot-scope="{ row }">
          <span>{{ row.name }}</span>
          学校
        </template>
      </el-table-column> -->
      <el-table-column :label="$t('AppQuestionBank[\'DisplayName:QuestionCateogryName\']')" prop="questionCateogryName" align="left" width="180">
        <template slot-scope="{ row }">
          <span>{{ row.questionCateogryName }}</span>
        </template>
      </el-table-column>
      <el-table-column :label="$t('AppQuestionBank[\'DisplayName:Content\']')" prop="Content" align="left">
        <template slot-scope="{ row }">
          <span>{{ row.content }}</span>
        </template>
      </el-table-column>
      <!-- <el-table-column :label="$t('AppQuestionBank[\'DisplayName:CreatorId\']')" prop="creatorId" sortable align="left" width="180">
        <template slot-scope="{ row }">
          <span>{{ row.creatorId }}</span>
        </template>
      </el-table-column> -->
      <el-table-column :label="$t('AppQuestionBank[\'DisplayName:Type\']')" prop="Type" align="left" width="180">
        <template slot-scope="{ row }">
          <el-tag v-if="QuestionTypeMap[row.type]" type="primary">{{ QuestionTypeMap[row.type] }}</el-tag>
        </template>
      </el-table-column>
      <el-table-column :label="$t('AppQuestionBank[\'DisplayName:Degree\']')" prop="Degree" align="left" width="180">
        <template slot-scope="{ row }">
          <el-tag v-if="QuestionDegreeMap[row.degree]" type="primary">{{ QuestionDegreeMap[row.degree] }}</el-tag>
        </template>
      </el-table-column>
      <el-table-column :label="$t('AppQuestionBank[\'DisplayName:Enable\']')" prop="Enable" align="left" width="180">
        <template slot-scope="{ row }">
          <el-tag :type="( row.enable ? 'success' : 'danger')" :class="[ row.enable ? 'el-icon-check':'el-icon-close' ]" />
        </template>
      </el-table-column>
      <el-table-column :label="$t('AbpUi[\'DisplayName:CreationTime\']')" prop="creationTime" align="left" width="180">
        <template slot-scope="{ row }">
          <span>{{ row.creationTime | moment }}</span>
        </template>
      </el-table-column>

      <el-table-column :label="$t('AbpUi[\'Actions\']')" align="left" width="280">
        <template slot-scope="{ row, $index }">
          <el-button v-if="checkPermission('QuestionBank.Question.Update')" type="primary" class="el-icon-edit" :title="$t('AbpUi[\'Edit\']')" @click="handleUpdate(row)" />
          <el-button v-if="checkPermission('QuestionBank.Question.Delete')" type="danger" class="el-icon-delete" :title="$t('AbpUi[\'Delete\']')" @click="handleDelete(row, $index)" />
        </template>
      </el-table-column>
    </el-table>

    <pagination v-show="total > 0" :total="total" :page.sync="listQuery.page" :limit.sync="listQuery.limit" @pagination="getList" />
    <question-model ref="questionModel" @handleFilter="handleFilter" />
  </div>
</template>

<script>
import {
  getQuestions,
  deleteQuestion

} from '@/api/question-bank/question'
import Pagination from '@/components/Pagination' // secondary package based on el-pagination
import QuestionModel from './components/QuestionModel.vue'
import baseListQuery, { checkPermission } from '@/utils/abp'

import { QuestionType, QuestionTypeMap, QuestionDegree, QuestionDegreeMap } from './datas/typing'

export default {
  name: 'Questions',
  components: {
    Pagination,
    QuestionModel
  },
  data() {
    return {
      QuestionType,
      QuestionTypeMap,
      QuestionDegree,
      QuestionDegreeMap,
      tableKey: 0,
      list: null,
      total: 0,
      listLoading: true,
      listQuery: baseListQuery

    }
  },
  created() {
    this.getList()
  },
  methods: {
    checkPermission, // 检查权限

    // 获取列表数据
    getList() {
      this.listLoading = true
      getQuestions(this.listQuery).then(response => {
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
      this.$refs['questionModel'].handleCreate()
    },
    handleUpdate(row) {
      this.$refs['questionModel'].handleUpdate(row)
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
        deleteQuestion(row.id).then(() => {
          this.handleFilter(false)
          this.$notify({
            title: this.$i18n.t("TigerUi['Success']"),
            message: this.$i18n.t("TigerUi['SuccessMessage']"),
            type: 'success',
            duration: 2000
          })
        })
      })
    }

  }
}
</script>
