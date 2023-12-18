<template>
  <div class="table-container">
    <div class="filter-container">
      <el-row style="margin-bottom: 20px">
        <el-input v-model="listQuery.filter" :placeholder="$t('AbpUi[\'PlaceholderInput\']')" style="width: 200px;" clearable class="filter-item" @keyup.enter.native="handleFilter" />
        <el-cascader
          ref="categoryCascader"
          v-model="listQuery.parentId"
          :options="options"
          :props="{ checkStrictly: true, value:'id', label:'name',children:'children',emitPath:false}"
          clearable
          class="filter-item"
          filterable
          :placeholder="$t('AppQuestionBank[\'DisplayName:QuestionCateogryName\']')"
          style="width: 360px;"
          @change="cascaderChange"
        />
        <el-select v-model="listQuery.enable" :placeholder="$t('AppQuestionBank[\'DisplayName:Enable\']')" class="filter-item" clearable="">
          <el-option :label="$t('AbpUi[\'Yes\']')" :value="true" />
          <el-option :label="$t('AbpUi[\'No\']')" :value="false" />
        </el-select>
        <el-button type="primary" class="filter-item" icon="el-icon-search" @click="handleFilter">
          {{ $t('AbpUi.Search') }}
        </el-button>
        <el-button v-if="checkPermission('QuestionBank.QuestionCategory.Create')" class="filter-item" style="margin-left: 10px;" type="primary" icon="el-icon-plus" @click="handleCreate">
          {{ $t("AppQuestionBank['Permission:Create']") }}
        </el-button>
        <el-button type="primary" plain class="filter-item" icon="el-icon-refresh" @click="handleRefresh">{{ $t("AbpUi['Refresh']") }}</el-button>
        <el-button v-if="checkPermission('QuestionBank.QuestionCategory.Export')" class="filter-item" type="primary" icon="el-icon-download" :loading="downloadLoading" @click="handleDownload">{{ $t('AbpUi.Export') }}</el-button>
        <el-button v-if="checkPermission('QuestionBank.QuestionCategory.Import')" class="filter-item" type="primary" icon="el-icon-upload" @click="handleImport">{{ $t('AbpUi.Import') }}</el-button>
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
          <span>
            <el-image style="width: 100px; height: 40px" :src="Url.photoPrefix + row.cover" fit="contain" :preview-src-list="[Url.photoPrefix + row.cover]">
              <div slot="error" class="image-slot">
                <i class="el-icon-picture-outline" />
              </div>
            </el-image>
          </span>
        </template>
      </el-table-column>
      <el-table-column :label="$t('AppQuestionBank[\'DisplayName:Code\']')" align="left" width="220">
        <template slot-scope="{ row }">
          <span>{{ row.code }}</span>
        </template>
      </el-table-column>
      <el-table-column :label="$t('AppQuestionBank[\'DisplayName:Enable\']')" align="left" width="120">
        <template slot-scope="{ row }">
          <el-switch v-model="row.enable" @change="handleToggleEnable(row)" />
        </template>
      </el-table-column>
      <el-table-column :label="$t('AppQuestionBank[\'DisplayName:IsPublic\']')" align="left" width="120">
        <template slot-scope="{ row }">
          <el-tag :type="( row.isPublic ? 'success' : 'danger')" :class="[ row.isPublic ? 'el-icon-check':'el-icon-close' ]" />
        </template>
      </el-table-column>
      <el-table-column :label="$t('AppQuestionBank[\'DisplayName:Sorting\']')" align="left" width="220">
        <template slot-scope="{ row }">
          <el-input-number v-model="row.sorting" :min="0" @change="handleUpdateSort(row)" />
        </template>
      </el-table-column>

      <el-table-column :label="$t('AbpUi[\'Actions\']')" align="left" width="200">
        <template slot-scope="{ row, $index }">
          <el-button v-if="checkPermission('QuestionBank.QuestionCategory.Update')" type="primary" class="el-icon-edit" :title="$t('AbpUi[\'Edit\']')" @click="handleUpdate(row)" />
          <el-button v-if="checkPermission('QuestionBank.QuestionCategory.Delete')" type="danger" class="el-icon-delete" :title="$t('AbpUi[\'Delete\']')" @click="handleDelete(row, $index)" />
        </template>
      </el-table-column>
    </el-table>

    <upload-single-excel ref="ImportExcelDialog" file-name="question-category" :import-from-xlsx="ImportQuestionCategoryFromXlsx" :import-xlsx-template="ExportQuestionCategoryXlsxTemplate" @call-filter="handleFilter" />
  </div>
</template>

<script>
import {
  getQuestionCategories,
  deleteQuestionCategory,
  getAllQuestionCategory,
  ExportQuestionCategoryXlsxTemplate,
  ImportQuestionCategoryFromXlsx,
  ExportQuestionCategoryToXlsx,
  toggleEnable,
  updateSort
} from '@/api/question-bank/question-category'
import UploadSingleExcel from '@/components/UploadSingleExcel/index.vue'
import { listToTree, recurtionToTree } from '@/utils/helpers/tree-helper'
import baseListQuery, { Url, checkPermission } from '@/utils/abp'
import { downloadByData } from '@/utils/download'

export default {
  name: 'QuestionCateogryTable',
  components: {
    UploadSingleExcel
  },
  data() {
    return {
      Url,
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
      downloadLoading: false

    }
  },
  created() {
    this.fetchOptions()
    this.getList()
  },
  methods: {
    checkPermission, // 检查权限
    listToTree,
    ExportQuestionCategoryXlsxTemplate,
    ImportQuestionCategoryFromXlsx,
    fetchOptions() {
      getAllQuestionCategory(baseListQuery).then(response => {
        this.options = recurtionToTree(response.items, null)
      })
    },
    cascaderChange(val) {
      if (!this.$refs.categoryCascader.checkedValue) {
        // 此处不清空，接口会传递后端字符串null导致无法解析
        this.listQuery.parentId = undefined
      }
    },
    // 获取列表数据
    getList() {
      this.listLoading = true
      getAllQuestionCategory(this.listQuery).then(response => {
        this.list = listToTree(response.items)
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
    handleRefresh() {
      this.handleFilter()
    },
    // 导出 所有
    handleDownload() {
      this.$confirm('是否导出全部查询结果?', this.$i18n.t("AbpUi['AreYouSure']"), {
        confirmButtonText: this.$i18n.t("AbpUi['Yes']"),
        cancelButtonText: this.$i18n.t("AbpUi['Cancel']"),
        type: 'warning'
      }).then(response => {
        this.downloadLoading = true
        ExportQuestionCategoryToXlsx(this.listQuery).then(response => {
          downloadByData(response, 'question-category.xlsx')
          this.downloadLoading = false
        }).catch(err => {
          console.log(err)
          this.downloadLoading = false
          this.$message.warning(err)
        })
      })
    },
    handleImport(row) {
      this.$refs['ImportExcelDialog'].handleUploadExcel()
    },
    handleCreate() {
      this.$emit('handleCreate')
    },
    handleUpdate(row) {
      this.$emit('handleUpdate', row)
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
    },
    handleEnable(row) {

    },
    handleToggleEnable(row) {
      toggleEnable(row.id).then(() => {
        this.$emit('handleFilter', false)
      })
    },
    handleUpdateSort(row) {
      updateSort(row.id, row.sorting).then(() => {
        this.$emit('handleFilter', false)
      })
    }
  }
}
</script>

<style scoped>
.line{
  text-align: center;
}
</style>

