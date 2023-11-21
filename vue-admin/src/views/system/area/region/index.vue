<template>
  <div class="app-container">
    <el-row>
      <el-col :span="6">
        <div class="grid-content bg-purple">
          <region-tree :get-list="handleFilter" />
        </div>
      </el-col>
      <el-col :span="18">
        <div class="grid-content bg-purple-light">
          <div class="filter-container">
            <el-row style="margin-bottom: 20px">
              <el-input v-model="listQuery.filter" :placeholder="$t('AbpUi[\'PagerSearch\']')" style="width: 200px;" clearable class="filter-item" @keyup.enter.native="handleFilter" />
              <el-button type="primary" class="filter-item" icon="el-icon-search" @click="handleFilter">
                {{ $t('AbpUi.Search') }}
              </el-button>
              <el-button v-if="checkPermission('Platform.Layout.Create')" class="filter-item" style="margin-left: 10px;" type="primary" icon="el-icon-plus" @click="handleCreate">
                {{ $t("AppPlatform['Permission:Create']") }}
              </el-button>
            </el-row>
          </div>

          <el-table :key="tableKey" v-loading="listLoading" :data="list" border fit highlight-current-row :stripe="true" style="width: 100%;" @sort-change="sortChange">
            <el-table-column type="selection" width="55" center />
            <el-table-column type="index" width="80" />

            <el-table-column :label="$t('AppArea[\'DisplayName:ParentCode\']')" align="left">
              <template slot-scope="{ row }">
                <span>{{ row.parentCode }}</span>
              </template>
            </el-table-column>
            <el-table-column :label="$t('AppArea[\'DisplayName:AreaCode\']')" prop="areaCode" sortable align="left">
              <template slot-scope="{ row }">
                <span>{{ row.areaCode }}</span>
              </template>
            </el-table-column>
            <el-table-column :label="$t('AppArea[\'DisplayName:Name\']')" prop="name" sortable align="left" width="160">
              <template slot-scope="{ row }">
                <span>{{ row.name }}</span>
              </template>
            </el-table-column>
            <el-table-column :label="$t('AppArea[\'DisplayName:ZipCode\']')" prop="zipCode" sortable align="left">
              <template slot-scope="{ row }">
                <span>{{ row.zipCode }}</span>
              </template>
            </el-table-column>
            <el-table-column :label="$t('AppArea[\'DisplayName:CityCode\']')" prop="cityCode" align="left">
              <template slot-scope="{ row }">
                <span>{{ row.cityCode }}</span>
              </template>
            </el-table-column>
            <el-table-column :label="$t('AppArea[\'DisplayName:MergerName\']')" prop="mergerName" align="left">
              <template slot-scope="{ row }">
                <span>{{ row.mergerName }}</span>
              </template>
            </el-table-column>
            <el-table-column :label="$t('AppArea[\'DisplayName:Lng\']')" prop="lng" align="left" width="80">
              <template slot-scope="{ row }">
                <span>{{ row.lng }}</span>
              </template>
            </el-table-column>
            <el-table-column :label="$t('AppArea[\'DisplayName:Lat\']')" prop="lat" align="left" width="80">
              <template slot-scope="{ row }">
                <span>{{ row.lat }}</span>
              </template>
            </el-table-column>

            <el-table-column :label="$t('AbpUi[\'Actions\']')" align="left" width="200" class-name="small-padding fixed-width">
              <template slot-scope="{ row, $index }">
                <el-button v-if="checkPermission('Platform.Layout.Update')" type="primary" @click="handleUpdate(row)">
                  {{ $t("AbpUi['Edit']") }}
                </el-button>
                <el-button v-if="checkPermission('Platform.Layout.Delete')" type="danger" @click="handleDelete(row, $index)">
                  {{ $t("AbpUi['Delete']") }}
                </el-button>
              </template>
            </el-table-column>
          </el-table>

          <pagination v-show="total > 0" :total="total" :page.sync="listQuery.page" :limit.sync="listQuery.limit" @pagination="getList" />

          <el-dialog :title=" dialogStatus == 'create'? $t('AppPlatform[\'Layout:AddNew\']'): $t('AbpUi[\'Edit\']')" :visible.sync="dialogFormVisible">
            <el-form ref="dataForm" :rules="rules" :model="temp" label-position="right" label-width="150px">
              <el-form-item :label="$t('AppArea[\'DisplayName:Name\']')" prop="name">
                <el-input v-model="temp.name" />
              </el-form-item>
              <el-form-item :label="$t('AppArea[\'DisplayName:ParentCode\']')" prop="parentCode">
                <el-cascader
                  v-model="temp.parentCode"
                  style="width:400px;"
                  filterable
                  :options="regionOptions"
                  :props="{ checkStrictly: true , label: 'name', value:'areaCode' }"
                  clearable
                />
              </el-form-item>
              <el-form-item :label="$t('AppArea[\'DisplayName:AreaCode\']')" prop="areaCode">
                <el-input v-model="temp.areaCode" />
              </el-form-item>
              <el-form-item :label="$t('AppArea[\'DisplayName:ZipCode\']')" prop="zipCode">
                <el-input v-model="temp.zipCode" />
              </el-form-item>
              <el-form-item :label="$t('AppArea[\'DisplayName:CityCode\']')" prop="cityCode">
                <el-input v-model="temp.cityCode" />
              </el-form-item>
              <el-form-item :label="$t('AppArea[\'DisplayName:Lng\']')" prop="lng">
                <el-input v-model="temp.lng" />
              </el-form-item>
              <el-form-item :label="$t('AppArea[\'DisplayName:Lat\']')" prop="lat">
                <el-input v-model="temp.lat" />
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
      </el-col>
    </el-row>

  </div>
</template>

<script>
import {
  getRegions,
  getRegion,
  createRegion,
  updateRegion,
  deleteRegion,
  getAllRegion
} from '@/api/system-manage/area/region'
import Pagination from '@/components/Pagination/index.vue' // secondary package based on el-pagination
import RegionTree from '../components/RegionTree.vue'
import baseListQuery, { checkPermission } from '@/utils/abp'
import { listToTree } from '@/utils/helpers/tree-helper'

export default {
  name: 'Layouts',
  components: {
    Pagination,
    RegionTree
  },
  data() {
    return {
      regionOptions: [],
      tableKey: 0,
      list: null,
      total: 0,
      listLoading: true,
      listQuery: Object.assign({

        parentCode: 0
      }, baseListQuery),
      temp: {
        id: 0,
        level: 0,
        parentCode: 0,
        areaCode: 0,
        zipCode: '',
        cityCode: '',
        name: '',
        shortName: '',
        mergerName: '',
        pinyin: '',
        lng: 0,
        lat: 0
      },

      dialogFormVisible: false,
      dialogStatus: '',

      // 表单验证规则
      rules: {
        name: [
          {
            required: true,
            message: this.$i18n.t("AbpValidation['The {0} field is required.']", [
              this.$i18n.t("AppPlatform['DisplayName:Name']")
            ]),
            trigger: 'blur'
          },
          {
            max: 64,
            message: this.$i18n.t(
              "AbpValidation['The field {0} must be a string with a maximum length of {1}.']",
              [this.$i18n.t("AppPlatform['DisplayName:Name']"), '64']
            ),
            trigger: 'blur'
          }
        ]

      }
    }
  },
  created() {
    this.getList()
    this.fetchRegionOptions()
  },
  methods: {
    checkPermission, // 检查权限
    listToTree,
    fetchRegionOptions() {
      getAllRegion().then(response => {
        // 级联选择器使用 https://blog.csdn.net/weixin_38547641/article/details/108275580
        const DEFAULT_CONFIG = {
          id: 'areaCode',
          children: 'children',
          pid: 'parentCode'
        }
        var o = this.listToTree(response.items, DEFAULT_CONFIG)
        this.regionOptions = o
      })
    },
    // 获取列表数据
    getList() {
      this.listLoading = true

      // 回显的数据格式需要一个数组 https://blog.csdn.net/li222248/article/details/118359028
      getRegions(this.listQuery).then(response => {
        this.list = response.items
        this.total = response.totalCount
        this.listLoading = false
      })
    },
    handleFilter(firstPage = true, parentCode = 0) {
      if (firstPage) this.listQuery.page = 1
      this.listQuery.parentCode = parentCode
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
        id: 0,
        level: 0,
        parentCode: 0,
        areaCode: 0,
        zipCode: '',
        cityCode: '',
        name: '',
        shortName: '',
        mergerName: '',
        pinyin: '',
        lng: 0,
        lat: 0
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
          createRegion(this.temp).then(() => {
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
      getRegion(row.id).then(response => {
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
          updateRegion(this.temp.id, this.temp).then(() => {
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
        deleteRegion(row.id).then(() => {
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
