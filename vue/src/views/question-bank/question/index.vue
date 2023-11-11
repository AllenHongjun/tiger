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

    <el-dialog :title=" dialogStatus == 'create'? $t('AppQuestionBank[\'Permission:Create\']'): $t('AbpUi[\'Edit\']')" :visible.sync="dialogFormVisible">
      <el-form ref="dataForm" :rules="rules" :model="temp" label-position="right" label-width="150px">
        <el-form-item :label="$t('AppQuestionBank[\'DisplayName:Name\']')" prop="name">
          <el-input v-model="temp.name" />
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
  getQuestions,
  getQuestion,
  createQuestion,
  updateQuestion,
  deleteQuestion
} from '@/api/question-bank/question'
import Pagination from '@/components/Pagination' // secondary package based on el-pagination
import baseListQuery, { checkPermission } from '@/utils/abp'

import { QuestionType, QuestionTypeMap, QuestionDegree, QuestionDegreeMap } from './datas/typing'

export default {
  name: 'Questions',
  components: {
    Pagination
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
      listQuery: baseListQuery,
      temp: {
        id: undefined,
        name: '',
        displayName: '',
        description: '',
        path: '',
        redirect: '',
        dataId: undefined,
        freamwork: ''
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
            this.handleFilter(false)
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
            this.handleFilter(false)
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
