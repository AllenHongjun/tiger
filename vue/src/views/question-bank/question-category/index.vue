<template>
  <div class="app-container">
    <div class="filter-container">
      <el-row style="margin-bottom: 20px">
        <el-input v-model="listQuery.filter" :placeholder="$t('AbpUi[\'PagerSearch\']')" style="width: 200px;" clearable class="filter-item" @keyup.enter.native="handleFilter" />
        <el-cascader v-model="listQuery.parentId" :options="options" :props="{ checkStrictly: true, value:'id', label:'name',children:'children',emitPath:false}" clearable class="filter-item" filterable />
        <el-button type="primary" class="filter-item" icon="el-icon-search" @click="handleFilter">
          {{ $t('AbpUi.Search') }}
        </el-button>
        <el-button v-if="checkPermission('QuestionBank.QuestionCategory.Create')" class="filter-item" style="margin-left: 10px;" type="primary" icon="el-icon-plus" @click="handleCreate">
          {{ $t("AppQuestionBank['Permission:Create']") }}
        </el-button>
      </el-row>
    </div>

    <el-table
      ref="dataTreeList"
      v-loading="listLoading"
      :data="list"
      row-key="id"
      :default-expand-all="false"
      :tree-props="{children: 'children', hasChildren: 'hasChildren'}"
      style="width: 100%;"
    >
      <el-table-column type="selection" width="55" center />
      <el-table-column type="index" width="80" />
      <el-table-column :label="$t('AppQuestionBank[\'DisplayName:Name\']')" prop="name" align="left">
        <template slot-scope="{ row }">
          <span>{{ row.name }}</span>
        </template>
      </el-table-column>
      <el-table-column :label="$t('AppQuestionBank[\'DisplayName:Cover\']')" align="center" width="220">
        <template slot-scope="{ row }">
          <span><el-image style="width: 100px; height: 40px" :src="row.cover" fit="contain" /></span>
        </template>
      </el-table-column>
      <el-table-column :label="$t('AppQuestionBank[\'DisplayName:Code\']')" align="left" width="220">
        <template slot-scope="{ row }">
          <span>{{ row.code }}</span>
        </template>
      </el-table-column>
      <el-table-column :label="$t('AppQuestionBank[\'DisplayName:Enable\']')" align="left" width="120">
        <template slot-scope="{ row }">
          <el-tag :type="( row.enable ? 'success' : 'danger')" :class="[ row.enable ? 'el-icon-check':'el-icon-close' ]" />
        </template>
      </el-table-column>
      <el-table-column :label="$t('AppQuestionBank[\'DisplayName:IsPublic\']')" align="left" width="120">
        <template slot-scope="{ row }">
          <el-tag :type="( row.isPublic ? 'success' : 'danger')" :class="[ row.isPublic ? 'el-icon-check':'el-icon-close' ]" />
        </template>
      </el-table-column>
      <el-table-column :label="$t('AppQuestionBank[\'DisplayName:Sorting\']')" align="left" width="120">
        <template slot-scope="{ row }">
          <span>{{ row.sorting }}</span>
        </template>
      </el-table-column>

      <el-table-column :label="$t('AbpUi[\'Actions\']')" align="left" width="200" class-name="small-padding fixed-width">
        <template slot-scope="{ row, $index }">
          <el-button v-if="checkPermission('QuestionBank.QuestionCategory.Update')" type="primary" @click="handleUpdate(row)">
            {{ $t("AbpUi['Edit']") }}
          </el-button>
          <el-button v-if="checkPermission('QuestionBank.QuestionCategory.Delete')" type="danger" @click="handleDelete(row, $index)">
            {{ $t("AbpUi['Delete']") }}
          </el-button>
        </template>
      </el-table-column>
    </el-table>

    <!-- <pagination v-show="total > 0" :total="total" :page.sync="listQuery.page" :limit.sync="listQuery.limit" @pagination="getList" /> -->

    <el-dialog :title=" dialogStatus == 'create'? $t('AppQuestionBank[\'Permission:Create\']'): $t('AbpUi[\'Edit\']')" :visible.sync="dialogFormVisible">
      <el-form ref="dataForm" :rules="rules" :model="temp" label-position="right" label-width="150px">
        <el-form-item :label="$t('AppQuestionBank[\'DisplayName:Name\']')" prop="name">
          <el-cascader v-model="temp.parentId" :options="options" :props="{ checkStrictly: true, value:'id', label:'name',children:'children',emitPath:false}" clearable class="filter-item" filterable />
        </el-form-item>
        <el-form-item :label="$t('AppQuestionBank[\'DisplayName:Name\']')" prop="name">
          <el-input v-model="temp.name" />
        </el-form-item>
        <el-form-item :label="$t('AppQuestionBank[\'DisplayName:Cover\']')" prop="cover">
          <el-input v-model="temp.cover" />
        </el-form-item>
        <el-form-item :label="$t('AppQuestionBank[\'DisplayName:Code\']')" prop="code">
          <el-input v-model="temp.code" />
        </el-form-item>
        <el-form-item :label="$t('AppQuestionBank[\'DisplayName:Enable\']')" prop="enable">
          <el-switch v-model="temp.enable" />
        </el-form-item>
        <el-form-item :label="$t('AppQuestionBank[\'DisplayName:IsPublic\']')" prop="isPublic">
          <el-switch v-model="temp.isPublic" />
        </el-form-item>
        <el-form-item :label="$t('AppQuestionBank[\'DisplayName:Sorting\']')" prop="sorting">
          <el-input v-model="temp.sorting" type="number" />
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
  getQuestionCategories,
  getQuestionCategory,
  createQuestionCategory,
  updateQuestionCategory,
  deleteQuestionCategory,
  getAllQuestionCategory
} from '@/api/question-bank/question-category'
import baseListQuery, { checkPermission } from '@/utils/abp'
import { listToTree } from '@/utils/helpers/tree-helper'

const DEFAULT_CONFIG = {
  id: 'id',
  children: 'children',
  pid: 'parentId'
}

export default {
  name: 'QuestionCategorys',
  components: {
  },
  data() {
    return {
      options: undefined,
      tableKey: 0,
      list: [], // 注意：树型格式数据默认不能为null
      total: 0,
      listLoading: true,
      listQuery: Object.assign({
        parentId: undefined,
        name: '',
        enable: undefined,
        isPublic: undefined
      }, baseListQuery),
      temp: {
        id: undefined,
        parentId: undefined,
        name: undefined,
        cover: undefined,
        code: undefined,
        enable: true,
        sorting: 0,
        isPublic: true
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
        ],
        displayName: [
          {
            required: true,
            message: this.$i18n.t("AbpValidation['The {0} field is required.']", [
              this.$i18n.t("AppQuestionBank['DisplayName:DisplayName']")
            ]),
            trigger: 'blur'
          },
          {
            max: 256,
            message: this.$i18n.t(
              "AbpValidation['The field {0} must be a string with a maximum length of {1}.']",
              [this.$i18n.t("AppQuestionBank['DisplayName:Name']"), '256']
            ),
            trigger: 'blur'
          }
        ]

      }
    }
  },
  created() {
    this.fetchOptions()
    this.getList()
  },
  methods: {
    checkPermission, // 检查权限
    listToTree,
    fetchOptions() {
      getAllQuestionCategory(baseListQuery).then(response => {
        this.options = listToTree(response.items, DEFAULT_CONFIG)
      })
    },
    // 获取列表数据
    getList() {
      this.listLoading = true
      getAllQuestionCategory(this.listQuery).then(response => {
        this.list = listToTree(response.items, DEFAULT_CONFIG)
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
        parentId: undefined,
        name: undefined,
        cover: undefined,
        code: undefined,
        enable: true,
        sorting: 0,
        isPublic: true
      }
    },

    // 点击创建按钮
    handleCreate() {
      this.resetTemp()
      this.dialogStatus = 'create'
      this.dialogFormVisible = true
      this.fetchOptions()
      this.$nextTick(() => {
        this.$refs['dataForm'].clearValidate()
      })
    },

    // 创建数据
    createData() {
      this.$refs['dataForm'].validate(valid => {
        if (valid) {
          createQuestionCategory(this.temp).then(() => {
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

      // TODO:更新数据回显，异步请求调整为请求完成之后绑定数据
      this.fetchOptions()
      getQuestionCategory(row.id).then(response => {
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
          updateQuestionCategory(this.temp.id, this.temp).then(() => {
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
        deleteQuestionCategory(row.id).then(() => {
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
