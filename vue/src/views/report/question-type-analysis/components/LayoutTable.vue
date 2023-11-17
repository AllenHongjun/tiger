<template>
  <div class="table-container">
    <div class="filter-container" style="margin-bottom:10px;">
      <el-form ref="logQueryForm" label-position="left" label-width="80px" :model="listQuery">
        <el-row :gutter="20">
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
    </div>

    <!-- TODO:参考首页放置两个饼图 统计试题每个类型占用的百分比  试题每个难度的百分比统计 -->
    <el-table :key="tableKey" v-loading="listLoading" :data="list" border fit highlight-current-row :stripe="true" style="width: 100%;" @sort-change="sortChange">
      <el-table-column label="试题类型" align="left" width="240">
        <template slot-scope="{ row }">
          <span>单选题</span>
        </template>
      </el-table-column>
      <el-table-column label="试题总数" prop="description" align="left">
        <template slot-scope="{ row }">
          <span>27</span>
        </template>
      </el-table-column>
      <el-table-column label="难度不限" prop="path" align="left">
        <template slot-scope="{ row }">
          <span>2</span>
        </template>
      </el-table-column>
      <el-table-column label="容易" prop="path" align="left">
        <template slot-scope="{ row }">
          <span>1</span>
        </template>
      </el-table-column>
      <el-table-column label="中等" prop="path" align="left">
        <template slot-scope="{ row }">
          <span>2</span>
        </template>
      </el-table-column>
      <el-table-column label="困难" prop="path" align="left">
        <template slot-scope="{ row }">
          <span>2</span>
        </template>
      </el-table-column>
    </el-table>
  </div>
</template>

<script>
import baseListQuery, { checkPermission } from '@/utils/abp'
import { pickerRangeWithHotKey } from '@/utils/picker'
import {
  getLayouts,
  deleteLayout
} from '@/api/system-manage/platform/layout'
import { getAllQuestionCategory } from '@/api/question-bank/question-category'
import { listToTree } from '@/utils/helpers/tree-helper'
import Pagination from '@/components/Pagination' // secondary package based on el-pagination

export default {
  name: 'LayoutTable',
  components: {
    Pagination
  },
  data() {
    return {
      questionCategoryOptions: [],
      queryCreateDateTime: undefined,
      advanced: false,
      pickerOptions: pickerRangeWithHotKey,
      tableKey: 0,
      list: null,
      total: 0,
      listLoading: true,
      listQuery: Object.assign({
        questionCategoryId: undefined,
        createStartTime: undefined,
        createEndTime: undefined
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

