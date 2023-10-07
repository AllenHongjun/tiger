<template>
  <div class="app-container">
    <div class="filter-container">
      <el-row style="margin-bottom: 20px">
        <el-input v-model="listQuery.filter" :placeholder="$t('AbpUi[\'PagerSearch\']')" style="width: 200px;" clearable class="filter-item" @keyup.enter.native="handleFilter" />
        <el-button type="primary" class="filter-item" icon="el-icon-search" @click="handleFilter">
          {{ $t('AbpUi.Search') }}
        </el-button>
        <el-button v-if="checkPermission('LocalizationManagement.Languages.Create')" class="filter-item" style="margin-left: 10px;" type="primary" icon="el-icon-plus" @click="handleCreate">
          {{ $t("LocalizationManagement['Permission:Create']") }}
        </el-button>
      </el-row>
    </div>

    <el-table :key="tableKey" v-loading="listLoading" :data="list" border fit highlight-current-row :stripe="true" style="width: 100%;" @sort-change="sortChange">
      <el-table-column type="selection" width="55" center />
      <el-table-column type="index" width="80" />
      <el-table-column :label="$t('LocalizationManagement[\'DisplayName:DisplayName\']')" align="left">
        <template slot-scope="{ row }">
          <span>{{ row.displayName }}</span>
          <el-tag v-if="row.isDefault">{{ $t('LocalizationManagement[\'DefaultLanguage\']') }}</el-tag>
        </template>
      </el-table-column>
      <el-table-column :label="$t('LocalizationManagement[\'DisplayName:CultureName\']')" align="left" width="220">
        <template slot-scope="{ row }">
          <span>{{ row.cultureName }}</span>
        </template>
      </el-table-column>
      <el-table-column :label="$t('LocalizationManagement[\'DisplayName:UiCultureName\']')" align="left" width="220">
        <template slot-scope="{ row }">
          <span>{{ row.uiCultureName }}</span>
        </template>
      </el-table-column>
      <el-table-column :label="$t('LocalizationManagement[\'DisplayName:Enable\']')" align="left" width="220">
        <template slot-scope="{ row }">
          <el-tag :type="( row.enable ? 'success' : 'danger')" :class="[row.enable ? 'el-icon-check':'el-icon-close' ]" />
        </template>
      </el-table-column>

      <el-table-column :label="$t('AbpUi[\'Actions\']')" align="left" width="280" class-name="small-padding fixed-width">
        <template slot-scope="{ row, $index }">

          <el-button v-if="checkPermission('LocalizationManagement.Languages.Update')" type="primary" @click="handleUpdate(row)">
            {{ $t("AbpUi['Edit']") }}
          </el-button>
          <el-button v-if="checkPermission('LocalizationManagement.Languages.ChangeDefault')" type="primary" plain @click="handleSetDefaultLanguage(row)">
            {{ $t("LocalizationManagement['DefaultLanguage']") }}
          </el-button>
          <el-button v-if="checkPermission('LocalizationManagement.Languages.Delete')" type="danger" @click="handleDelete(row, $index)">
            {{ $t("AbpUi['Delete']") }}
          </el-button>
        </template>
      </el-table-column>
    </el-table>

    <pagination v-show="total > 0" :total="total" :page.sync="listQuery.page" :limit.sync="listQuery.limit" @pagination="getList" />

    <el-dialog :title=" dialogStatus == 'create'? $t('LocalizationManagement[\'Permission:Create\']'): $t('AbpUi[\'Edit\']')" :visible.sync="dialogFormVisible">
      <el-form ref="dataForm" :rules="rules" :model="temp" label-position="right" label-width="150px">
        <el-form-item v-if="dialogStatus == 'create'" :label="$t('LocalizationManagement[\'DisplayName:CultureName\']')" prop="cultureName">
          <el-select v-model="temp.cultureName" placeholder="-" filterable>
            <el-option
              v-for="item in culturelistOptions"
              :key="item.name"
              :label="item.displayName"
              :value="item.name"
            />
          </el-select>
        </el-form-item>
        <el-form-item v-if="dialogStatus == 'create'" :label="$t('LocalizationManagement[\'DisplayName:UiCultureName\']')" prop="uiCultureName">
          <el-select v-model="temp.uiCultureName" placeholder="-" filterable>
            <el-option
              v-for="item in culturelistOptions"
              :key="item.name"
              :label="item.displayName"
              :value="item.name"
            />
          </el-select>
        </el-form-item>
        <el-form-item :label="$t('LocalizationManagement[\'DisplayName:DisplayName\']')" prop="displayName">
          <el-input v-model="temp.displayName" />
        </el-form-item>
        <el-form-item :label="$t('LocalizationManagement[\'DisplayName:Enable\']')" prop="enable">
          <el-switch v-model="temp.enable" />
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
  getLanguages,
  getLanguage,
  createLanguage,
  updateLanguage,
  deleteLanguage,
  setDefaultLanguage
} from '@/api/system-manage/localization/language'
import Pagination from '@/components/Pagination' // secondary package based on el-pagination
import baseListQuery, {
  checkPermission
} from '@/utils/abp'
import culturelist from '../datas/cultures.js'

export default {
  name: 'Languages',
  components: {
    Pagination
  },
  data() {
    return {
      culturelistOptions: culturelist,
      tableKey: 0,
      list: null,
      total: 0,
      listLoading: true,
      listQuery: baseListQuery,
      temp: {
        id: undefined,
        enable: true,
        cultureName: '',
        uiCultureName: '',
        displayName: '',
        flagIcon: '',
        isDefault: true
      },
      dialogFormVisible: false,
      dialogStatus: '',

      // 表单验证规则
      rules: {
        name: [
          {
            required: true,
            message: this.$i18n.t("AbpValidation['The {0} field is required.']", [
              this.$i18n.t("LocalizationManagement['DisplayName:Name']")
            ]),
            trigger: 'blur'
          },
          {
            max: 64,
            message: this.$i18n.t(
              "AbpValidation['The field {0} must be a string with a maximum length of {1}.']",
              [this.$i18n.t("LocalizationManagement['DisplayName:Name']"), '64']
            ),
            trigger: 'blur'
          }
        ],
        displayName: [
          {
            required: true,
            message: this.$i18n.t("AbpValidation['The {0} field is required.']", [
              this.$i18n.t("LocalizationManagement['DisplayName:DisplayName']")
            ]),
            trigger: 'blur'
          },
          {
            max: 256,
            message: this.$i18n.t(
              "AbpValidation['The field {0} must be a string with a maximum length of {1}.']",
              [this.$i18n.t("LocalizationManagement['DisplayName:Name']"), '256']
            ),
            trigger: 'blur'
          }
        ],
        description: [
          {
            max: 256,
            message: this.$i18n.t(
              "AbpValidation['The field {0} must be a string with a maximum length of {1}.']",
              [this.$i18n.t("LocalizationManagement['DisplayName:Description']"), '256']
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
      getLanguages(this.listQuery).then(response => {
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
        enable: true,
        cultureName: '',
        uiCultureName: '',
        displayName: '',
        flagIcon: '',
        isDefault: true
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
          createLanguage(this.temp).then(() => {
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
      getLanguage(row.id).then(response => {
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
          updateLanguage(this.temp.id, this.temp).then(() => {
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
          row.displayName
        ]),
        // title
        this.$i18n.t("AbpUi['AreYouSure']"), {
          confirmButtonText: this.$i18n.t("AbpUi['Yes']"), // 确认按钮
          cancelButtonText: this.$i18n.t("AbpUi['Cancel']"), // 取消按钮
          type: 'warning' // 弹框类型
        }
      ).then(async() => {
        // 回调函数
        deleteLanguage(row.id).then(() => {
          this.handleFilter(false)
          this.$notify({ title: this.$i18n.t("TigerUi['Success']"), message: this.$i18n.t("TigerUi['SuccessMessage']"), type: 'success', duration: 2000 })
        })
      })
    },
    // 设置默认语言
    handleSetDefaultLanguage(row) {
      setDefaultLanguage(row.id).then(response => {
        this.handleFilter(false)
        this.$notify({ title: this.$i18n.t("TigerUi['Success']"), message: this.$i18n.t("TigerUi['SuccessMessage']"), type: 'success', duration: 2000 })
      })
    }
  }
}
</script>

<style scoped>
.el-tag{
  margin-left:10px;
}
</style>
