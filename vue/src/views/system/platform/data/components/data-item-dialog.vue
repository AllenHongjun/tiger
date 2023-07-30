<template>
  <div>
    <el-dialog :visible.sync="dialogTableVisible" class="data-item-dialog">
      <div class="filter-container">
        <el-button v-if="checkPermission('Platform.DataDictionary.Create')" class="filter-item" style="margin-left: 10px;" type="primary" icon="el-icon-plus" @click="handleCreate">
          {{ $t("AppPlatform['Data:AddNew']") }}
        </el-button>
      </div>
      <el-table :key="tableKey" v-loading="listLoading" :data="list" border fit highlight-current-row style="width: 100%;" @sort-change="sortChange">
        <el-table-column :label="$t('AppPlatform[\'DisplayName:Name\']')" prop="name" align="left">
          <template slot-scope="{ row }">
            <span>{{ row.name }}</span>
          </template>
        </el-table-column>
        <el-table-column :label="$t('AppPlatform[\'DisplayName:DisplayName\']')" prop="displayName" align="left">
          <template slot-scope="{ row }">
            <span>{{ row.displayName }}</span>
          </template>
        </el-table-column>
        <el-table-column :label="$t('AppPlatform[\'DisplayName:DefaultValue\']')" prop="defaultValue" align="left">
          <template slot-scope="{ row }">
            <span>{{ row.defaultValue }}</span>
          </template>
        </el-table-column>
        <el-table-column :label="$t('AppPlatform[\'DisplayName:Description\']')" prop="description" align="left">
          <template slot-scope="{ row }">
            <span>{{ row.description }}</span>
          </template>
        </el-table-column>
        <el-table-column :label="$t('AppPlatform[\'DisplayName:AllowBeNull\']')" prop="allowBeNull" align="left">
          <template slot-scope="{ row }">
            <span>{{ row.allowBeNull }}</span>
          </template>
        </el-table-column>
        <el-table-column :label="$t('AppPlatform[\'DisplayName:ValueType\']')" prop="valueType" align="left">
          <template slot-scope="{ row }">
            <el-tag>{{ row.valueType | typeFilter }}</el-tag>
          </template>
        </el-table-column>

        <el-table-column :label="$t('AbpUi[\'Actions\']')" align="left" width="240" class-name="small-padding fixed-width">
          <template slot-scope="{ row, $index }">
            <el-button v-if="checkPermission('Platform.DataDictionary.Update')" type="primary" size="mini" @click="handleUpdate(row)">
              {{ $t("AppPlatform['Data:Edit']") }}
            </el-button>
            <el-button v-if="checkPermission('Platform.DataDictionary.Delete')" size="mini" type="danger" @click="handleDelete(row, $index)">
              {{ $t("AppPlatform['Data:Delete']") }}
            </el-button>
          </template>
        </el-table-column>
      </el-table>
    </el-dialog>

    <el-dialog :title="dialogStatus == 'create'? $t('AppPlatform[\'Data:AddNew\']'): $t('AppPlatform[\'Data:Edit\']')" :visible.sync="dialogFormVisible">
      <el-form ref="dataForm" :rules="rules" :model="temp" label-position="right" label-width="180px">

        <el-form-item :label="$t('AppPlatform[\'DisplayName:Name\']')" prop="name">
          <el-input v-model="temp.name" />
        </el-form-item>
        <el-form-item :label="$t('AppPlatform[\'DisplayName:DisplayName\']')" prop="displayName">
          <el-input v-model="temp.displayName" />
        </el-form-item>
        <el-form-item :label="$t('AppPlatform[\'DisplayName:DefaultValue\']')" prop="defaultValue">
          <el-input v-model="temp.defaultValue" />
        </el-form-item>
        <el-form-item :label="$t('AppPlatform[\'DisplayName:Description\']')" prop="description">
          <el-input v-model="temp.description" />
        </el-form-item>
        <el-form-item prop="lockoutEnabled">
          <el-checkbox v-model="temp.allowBeNull" :label="$t('AppPlatform[\'DisplayName:AllowBeNull\']')" prop="allowBeNull" />
        </el-form-item>

        <el-form-item :label="$t('AppPlatform[\'DisplayName:ValueType\']')" prop="valueType">
          <el-select v-model="temp.valueType" class="filter-item" :placeholder="$t('AppPlatform[\'DisplayName:ValueType\']')">
            <el-option v-for="item in valueTypeOptions" :key="item.key" :label="item.display_name" :value="item.key" />
          </el-select>
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

const valueTypeOptions = [{
  key: 0,
  display_name: 'String'
},
{
  key: 1,
  display_name: 'Numeic'
},
{
  key: 2,
  display_name: 'Boolean'
},
{
  key: 3,
  display_name: 'Date'
},
{
  key: 4,
  display_name: 'DateTime'
},
{
  key: 5,
  display_name: 'Array'
},
{
  key: 6,
  display_name: 'Object'
}
]

// arr to obj, such as { CN : "China", US : "USA" }
const valueTypeKeyValue = valueTypeOptions.reduce((acc, cur) => {
  acc[cur.key] = cur.display_name
  return acc
}, {})

import {
  getDataByName,
  createDataItem,
  updateDataItem,
  deleteDataItem

} from '@/api/system-manage/platform/data'

import baseListQuery, {
  checkPermission
} from '@/utils/abp'

export default {
  name: 'DataItemDialog',
  filters: {
    statusFilter(status) {
      const statusMap = {
        published: 'success',
        draft: 'info',
        deleted: 'danger'
      }
      return statusMap[status]
    },
    typeFilter(type) {
      return valueTypeKeyValue[type]
    }
  },
  data() {
    return {
      tableKey: 0,
      list: null,
      total: 0,
      listLoading: true,
      listQuery: baseListQuery,
      dialogTableVisible: false,
      dataId: undefined,
      dataName: '',
      valueTypeOptions,
      temp: {
        id: undefined,
        name: '',
        displayName: '',
        defaultValue: '',
        description: '',
        allowBeNull: true,
        valueType: ''
      },
      rules: {
        name: [
          {
            required: true,
            // 表单验证可以 扩展 AbpValidation 这个基类的资源，添加需要的验证
            message: this.$i18n.t("AbpValidation['The {0} field is required.']", [
              this.$i18n.t("AppPlatform['DisplayName:Name']")
            ]),
            trigger: 'blur'
          },
          {
            max: 30,
            message: this.$i18n.t(
              "AbpValidation['The field {0} must be a string with a maximum length of {1}.']",
              [this.$i18n.t("AppPlatform['DisplayName:Name']"), '30']
            ),
            trigger: 'blur'
          }
        ],
        displayName: [
          {
            required: true,
            // 表单验证可以 扩展 AbpValidation 这个基类的资源，添加需要的验证
            message: this.$i18n.t("AbpValidation['The {0} field is required.']", [
              this.$i18n.t("AppPlatform['DisplayName:DisplayName']")
            ]),
            trigger: 'blur'
          },
          {
            max: 128,
            message: this.$i18n.t(
              "AbpValidation['The field {0} must be a string with a maximum length of {1}.']",
              [this.$i18n.t("AppPlatform['DisplayName:DisplayName']"), '128']
            ),
            trigger: 'blur'
          }
        ],
        description: [
          {
            max: 1024,
            message: this.$i18n.t(
              "AbpValidation['The field {0} must be a string with a maximum length of {1}.']",
              [this.$i18n.t("AppPlatform['DisplayName:DisplayName']"), '1024']
            ),
            trigger: 'blur'
          }
        ],
        DefaultValue: [
          {
            max: 128,
            message: this.$i18n.t(
              "AbpValidation['The field {0} must be a string with a maximum length of {1}.']",
              [this.$i18n.t("AppPlatform['DisplayName:DisplayName']"), '1024']
            ),
            trigger: 'blur'
          }
        ]
      },
      dialogStatus: '',
      dialogFormVisible: false
    }
  },
  methods: {
    checkPermission,
    getList(row) {
      // this.temp = Object.assign({}, row) // copy obj
      this.dataId = row.id
      this.dataName = row.name
      this.dialogTableVisible = true

      this.listLoading = true
      getDataByName(row.name).then(response => {
        this.list = response.items
        this.total = response.totalCount
        this.listLoading = false
      })
    },
    handleFilter(firstPage = true) {
      this.dialogTableVisible = true
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
    resetTemp() {
      this.temp = {
        id: undefined,
        name: '',
        displayName: '',
        defaultValue: '',
        description: '',
        allowBeNull: undefined, // 类型必须正确，不然会报错
        valueType: ''
      }
    },
    handleCreate() {
      this.resetTemp()
      this.dialogStatus = 'create'
      this.dialogFormVisible = true

      this.$nextTick(() => {
        this.$refs['dataForm'].clearValidate()
      })
    },
    createData() {
      this.$refs['dataForm'].validate(valid => {
        if (valid) {
          createDataItem(this.dataId, this.temp).then(() => {
            this.getList({ id: this.dataId, name: this.dataName })
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
    handleUpdate(row) {
      this.temp = Object.assign({}, row) // copy obj
      this.dialogStatus = 'update'
      this.dialogFormVisible = true

      // TODO:需要一个接口重新获取字典项目数据
      // getData(row.id).then(response => {
      //   this.temp = response
      // })

      this.$nextTick(() => {
        this.$refs['dataForm'].clearValidate()
      })
    },
    updateData() {
      this.$refs['dataForm'].validate(valid => {
        if (valid) {
          updateDataItem(this.dataId, this.temp.name, this.temp).then(() => {
            this.getList({ id: this.dataId, name: this.dataName })
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
    handleDelete(row, index) {
      var id = this.dataId
      var that = this
      this.$confirm(
        this.$i18n.t(
          "AppPlatform['TenantDeletionConfirmationMessage']",
          [row.name]
        ),
        this.$i18n.t("AppPlatform['AreYouSure']"), {
          confirmButtonText: this.$i18n.t("AbpUi['Yes']"),
          cancelButtonText: this.$i18n.t("AbpUi['Cancel']"),
          type: 'warning'
        }
      ).then(async() => {
        deleteDataItem(id, row.name).then(() => {
          that.getList({ id: id, name: that.dataName })
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

<style lang="scss" scoped>
.data-item-dialog
{
    overflow: hidden;

    ::v-deep .el-dialog {
        height: 70%;
        overflow: hidden;

        .el-dialog__body {
            position: absolute;
            left: 0;
            top: 54px;
            bottom: 0;
            right: 0;
            padding: 0;
            z-index: 1;
            overflow: hidden;
            overflow-y: auto;
            // 下边设置字体，我的需求是黑底白字
            color: #ffffff;
            line-height: 30px;
            padding: 0 15px 30px;
        }
    }
}
</style>
