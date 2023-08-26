<template>
  <div class="app-container">
    <div class="filter-container">
      <el-row style="margin-bottom: 20px">
        <el-input v-model="listQuery.filter" :placeholder="$t('AbpUi[\'PagerSearch\']')" style="width: 200px;" class="filter-item" clearable="" @keyup.enter.native="handleFilter" />
        <el-button type="primary" class="filter-item" icon="el-icon-search" @click="handleFilter">
          {{ $t('AbpUi.Search') }}
        </el-button>
      </el-row>
    </div>

    <el-table :key="tableKey" v-loading="listLoading" :data="list" border fit highlight-current-row :stripe="true" style="width: 100%;" max-height="720" @sort-change="sortChange">
      <el-table-column type="index" width="80" />
      <el-table-column :label="$t('CachingManagement[\'DisplayName:Key\']')" prop="key" sortable align="left">
        <template slot-scope="{ row }">
          <span>{{ row }}</span>
        </template>
      </el-table-column>

      <el-table-column :label="$t('AbpUi[\'Actions\']')" align="center" width="200" class-name="small-padding fixed-width">
        <template slot-scope="{ row, $index }">
          <el-button v-if="checkPermission('AbpCachingManagement.Cache.Refresh')" type="primary" @click="handleUpdate(row)">
            {{ "查看" }}
          </el-button>
          <el-button v-if="checkPermission('AbpCachingManagement.Cache.Delete')" type="danger" @click="handleDelete(row, $index)">
            {{ $t("AbpUi['Delete']") }}
          </el-button>
        </template>
      </el-table-column>
    </el-table>

    <pagination :total="99999" :page.sync="listQuery.page" layout="prev, next" :limit.sync="listQuery.limit" prev-text="< 上一页" next-text="下一页 >" @pagination="getList" />

    <el-dialog :title=" dialogStatus == 'create'? $t('CachingManagement[\'Cache:AddNew\']'): $t('AbpUi[\'Edit\']')" :visible.sync="dialogFormVisible">
      <el-form ref="dataForm" :rules="rules" :model="temp" label-position="right" label-width="150px">
        <el-form-item :label="$t('CachingManagement[\'DisplayName:Type\']')" prop="type">
          <el-input v-model="temp.type" />
        </el-form-item>
        <el-form-item :label="$t('CachingManagement[\'DisplayName:Size\']')" prop="size">
          <el-input v-model="temp.size" />
        </el-form-item>
        <el-form-item :label="$t('CachingManagement[\'DisplayName:Expiration\']')" prop="expiration">
          <!-- <el-input v-model="temp.values.data" type="textarea" /> -->
          <span readonly>{{ temp.expiration | moment }}</span>
        </el-form-item>
        <el-form-item :label="$t('CachingManagement[\'DisplayName:Values\']')" prop="type">
          <json-viewer :value="temp.values.data" :expand-depth="10" boxed sort :expanded="false" />
        </el-form-item>

      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button @click="dialogFormVisible = false">
          {{ $t("AbpUi['Cancel']") }}
        </el-button>
        <!-- <el-button type="success" @click="dialogStatus === 'create' ? createData() : handleRefresh()">
          {{ $t("CachingManagement['Permission:Refresh']") }}
        </el-button> -->
      </div>
    </el-dialog>
  </div>
</template>

<script>
import {
  getCaches,
  getCache,
  refreshCache,
  updateCache,
  deleteCache
} from '@/api/system-manage/monitor/cache'
import Pagination from '@/components/Pagination' // secondary package based on el-pagination
import baseListQuery, {
  checkPermission
} from '@/utils/abp'
import moment from 'moment'
import JsonViewer from 'vue-json-viewer'

export default {
  name: 'Caches',
  components: {
    Pagination,
    JsonViewer
  },
  data() {
    return {
      tableKey: 0,
      list: null,
      total: 0,
      listLoading: true,
      listQuery: Object.assign({
        Prefix: undefined,
        Marker: 0
      }, baseListQuery),
      temp: {
        type: undefined,
        size: undefined,
        expiration: undefined,
        values: {
          absexp: undefined,
          sldexp: undefined,
          data: undefined
        }
      },
      dialogFormVisible: false,
      dialogStatus: '',

      // 表单验证规则
      rules: {
        name: [
          {
            required: true,
            message: this.$i18n.t("AbpValidation['The {0} field is required.']", [
              this.$i18n.t("CachingManagement['DisplayName:Key']")
            ]),
            trigger: 'blur'
          },
          {
            max: 64,
            message: this.$i18n.t(
              "AbpValidation['The field {0} must be a string with a maximum length of {1}.']",
              [this.$i18n.t("CachingManagement['DisplayName:Key']"), '64']
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
      getCaches(this.listQuery).then(response => {
        this.list = response.keys
        this.listQuery.Marker = response.nextMarker
        this.total = 500

        this.listLoading = false
      })
    },
    handleFilter(firstPage = true) {
      if (firstPage) this.listQuery.Marker = 0
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
        type: undefined,
        size: undefined,
        expiration: undefined,
        values: {
          absexp: undefined,
          sldexp: undefined,
          data: undefined
        }
      }
    },

    // 更新按钮点击
    handleUpdate(row) {
      console.log(row)
      this.dialogStatus = 'update'
      this.dialogFormVisible = true
      getCache({ 'key': row }).then(response => {
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
          updateCache(this.temp.id, this.temp).then(() => {
            const index = this.list.findIndex((v) => v.id === this.temp.id)
            this.list.splice(index, 1, this.temp)
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
        this.$i18n.t("AbpUi['ItemWillBeDeletedMessage']", [
          row.name
        ]),
        // title
        this.$i18n.t("AbpUi['ItemWillBeDeletedMessage']"), {
          confirmButtonText: this.$i18n.t("AbpUi['Yes']"), // 确认按钮
          cancelButtonText: this.$i18n.t("AbpUi['Cancel']"), // 取消按钮
          type: 'warning' // 弹框类型
        }
      ).then(async() => {
        // 回调函数
        deleteCache({ 'key': row }).then(() => {
          const index = this.list.findIndex((v) => v === row)
          this.list.splice(index, 1)
          this.$notify({
            title: this.$i18n.t("TigerUi['Success']"),
            message: this.$i18n.t("TigerUi['SuccessMessage']"),
            type: 'success',
            duration: 2000
          })
        })
      })
    },

    // 要根据重新计算缓存过期的时间，待开发
    handleRefresh(key) {
      var input = {
        key: key,
        absoluteExpiration: '2023-08-26T03:19:52.986Z',
        slidingExpiration: '2023-08-26T03:19:52.986Z'
      }
      console.log(this.temp)
      this.$message('开发中', this.temp)
      refreshCache(input).then(() => {
        this.this.handleFilter(false)
        this.dialogFormVisible = false
        this.$notify({
          title: this.$i18n.t("TigerUi['Success']"),
          message: this.$i18n.t("TigerUi['SuccessMessage']"),
          type: 'success',
          duration: 2000
        })
      })
      return
    }
  }
}
</script>
