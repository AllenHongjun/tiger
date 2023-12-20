<template>
  <div class="table-container">
    <div class="filter-container" style="margin-bottom:10px;">
      <el-form ref="logQueryForm" label-position="left" label-width="100px" :model="listQuery">
        <el-row :gutter="20">
          <el-col :span="4">
            <el-form-item prop="filter" :label="$t('AbpUi[\'Search\']')">
              <el-input v-model="listQuery.filter" :placeholder="$t('AbpUi[\'PlaceholderInput\']')" clearable />
            </el-form-item>
          </el-col>

          <el-col :span="4">
            <el-form-item prop="filter" :label="$t('AppQuestionBank[\'DisplayName:Enable\']')">
              <el-select v-model="listQuery.enable" placeholder="-" class="filter-item" clearable="">
                <el-option :label="$t('AbpUi[\'Yes\']')" :value="true" />
                <el-option :label="$t('AbpUi[\'No\']')" :value="false" />
              </el-select>
            </el-form-item>
          </el-col>

          <el-col :span="8">
            <el-form-item :label="$t('AbpUi[\'DisplayName:CreationTime\']')">
              <el-date-picker
                v-model="queryCreateDateTime"
                value-format="yyyy-MM-dd HH:mm:ss"
                :default-time="['00:00:00', '23:59:59']"
                type="datetimerange"
                align="right"
                unlink-panels
                :picker-options="pickerOptions"
                range-separator="-"
                :start-placeholder="$t('AbpUi[\'StartTime\']')"
                :end-placeholder="$t('AbpUi[\'EndTime\']')"
                @change="datePickerChange"
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
      </el-form>

      <!-- 操作按钮 -->
      <el-row>
        <el-col>
          <el-button-group>
            <el-button v-if="checkPermission('QuestionBank.TrainPlatform.Create')" class="filter-item" type="primary" icon="el-icon-plus" @click="handleCreate">
              {{ $t("AppQuestionBank['Permission:Create']") }}
            </el-button>
          </el-button-group>
        </el-col>
      </el-row>
    </div>

    <el-table :key="tableKey" v-loading="listLoading" :data="list" border fit highlight-current-row :stripe="true" style="width: 100%;" @sort-change="sortChange">
      <el-table-column type="selection" width="55" center />
      <el-table-column type="index" width="80" />
      <el-table-column :label="$t('AppQuestionBank[\'DisplayName:Name\']')" prop="name" sortable align="left">
        <template slot-scope="{ row }">
          <span>{{ row.name }}</span>
        </template>
      </el-table-column>

      <el-table-column :label="$t('AppQuestionBank[\'DisplayName:Icon\']')" prop="icon" align="left" width="220">
        <template slot-scope="{ row }">
          <el-image style="width: 100px; height: 40px" :src="Url.photoPrefix + row.icon" fit="contain" :preview-src-list="[Url.photoPrefix + row.icon]">
            <div slot="error" class="image-slot">
              <i class="el-icon-picture-outline" />
            </div>
          </el-image>
        </template>
      </el-table-column>
      <!-- <el-table-column :label="$t('AppQuestionBank[\'DisplayName:TrainPlatformUrl\']')" prop="url" align="left" width="280">
        <template slot-scope="{ row }">
          <el-link :href="row.url" target="_blank" type="primary">{{ row.url }}</el-link>
        </template>
      </el-table-column> -->
      <el-table-column :label="$t('AppQuestionBank[\'DisplayName:CheckCode\']')" prop="checkCode" sortable align="left" width="140">
        <template slot-scope="{ row }">
          <span>{{ row.checkCode }}</span>
        </template>
      </el-table-column>
      <el-table-column :label="$t('AppQuestionBank[\'DisplayName:TokenType\']')" prop="tokenType" sortable align="left" width="160">
        <template slot-scope="{ row }">
          <el-tag>{{ TokenTypeMap[row.tokenType] }}</el-tag>
        </template>
      </el-table-column>
      <el-table-column :label="$t('AppQuestionBank[\'DisplayName:Sorting\']')" prop="sorting" sortable align="left" width="120">
        <template slot-scope="{ row }">
          <span>{{ row.sorting }}</span>
        </template>
      </el-table-column>
      <el-table-column :label="$t('AppQuestionBank[\'DisplayName:Enable\']')" prop="enable" sortable align="left" width="120">
        <template slot-scope="{ row }">
          <el-tag :type="( row.enable ? 'success' : 'danger')" :class="[ row.enable ? 'el-icon-check':'el-icon-close' ]" />
        </template>
      </el-table-column>
      <el-table-column :label="$t('AbpUi[\'DisplayName:CreationTime\']')" prop="creationTime" align="left" width="180">
        <template slot-scope="{ row }">
          <span>{{ row.creationTime | moment }}</span>
        </template>
      </el-table-column>

      <el-table-column :label="$t('AbpUi[\'Actions\']')" align="left" width="180">
        <template slot-scope="{ row, $index }">
          <el-button v-if="checkPermission('QuestionBank.TrainPlatform.Update')" type="primary" class="el-icon-edit" :title="$t('AbpUi[\'Edit\']')" @click="handleUpdate(row)" />
          <el-button v-if="checkPermission('QuestionBank.TrainPlatform.Delete')" type="danger" class="el-icon-delete" :title="$t('AbpUi[\'Delete\']')" @click="handleDelete(row, $index)" />
        </template>
      </el-table-column>
    </el-table>

    <pagination v-show="total > 0" :total="total" :page.sync="listQuery.page" :limit.sync="listQuery.limit" @pagination="getList" />
  </div>
</template>

<script>
import baseListQuery, { Url, checkPermission } from '@/utils/abp'
import {
  getTrainPlatforms,
  deleteTrainPlatform
} from '@/api/question-bank/train-platform'
import { pickerRangeWithHotKey } from '@/utils/picker'
import Pagination from '@/components/Pagination' // secondary package based on el-pagination
import { TokenTypeMap } from '../datas/typing'

export default {
  name: 'TrainPlatformTable',
  components: {
    Pagination
  },
  data() {
    return {
      TokenTypeMap,
      queryCreateDateTime: undefined,
      advanced: false,
      pickerOptions: pickerRangeWithHotKey,
      tableKey: 0,
      list: null,
      total: 0,
      listLoading: true,
      listQuery: Object.assign({
        createStartTime: undefined,
        createEndTime: undefined
      }, baseListQuery),
      Url
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
      if (this.queryCreateDateTime) {
        this.listQuery.createStartTime = this.queryCreateDateTime[0]
        this.listQuery.createEndTime = this.queryCreateDateTime[1]
      }
      getTrainPlatforms(this.listQuery).then(response => {
        this.list = response.items
        this.total = response.totalCount
        this.listLoading = false
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
    // 重置查询参数
    resetQueryForm() {
      this.queryCreateDateTime = undefined
      this.listQuery = Object.assign({
        createStartTime: undefined,
        createEndTime: undefined,
        enable: undefined
      }, baseListQuery)
    },
    // 刷新页面
    handleRefresh() {
      this.handleFilter()
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
        deleteTrainPlatform(row.id).then(() => {
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

<style scoped>
.line{
  text-align: center;
}
</style>

