<template>
  <div class="app-container">
    <div class="filter-container">
      <el-row>
        <el-button class="filter-item" style="margin-left: 10px;" type="primary" icon="el-icon-plus" @click="handleCreate">
          {{ $t("AbpIdentityServer['Propertites:New']") }}
        </el-button>
      </el-row>
    </div>

    <el-table :key="tableKey" v-loading="listLoading" :data="list" border fit highlight-current-row :stripe="true" style="width: 100%;" @sort-change="sortChange">
      <el-table-column type="index" width="80" />
      <el-table-column :label="$t('AbpIdentityServer[\'Propertites:Key\']')" prop="key" sortable align="left" width="180">
        <template slot-scope="{ row }">
          <span>{{ row.key }}</span>
        </template>
      </el-table-column>
      <el-table-column :label="$t('AbpIdentityServer[\'Propertites:Value\']')" align="left">
        <template slot-scope="{ row }">
          <span>{{ row.value }}</span>
        </template>
      </el-table-column>

      <el-table-column :label="$t('AbpUi[\'Actions\']')" align="left" width="200" class-name="small-padding fixed-width">
        <template slot-scope="{ row, $index }">
          <el-button type="primary" @click="handleUpdate(row)">
            {{ $t("AbpUi['Edit']") }}
          </el-button>
          <el-button type="danger" @click="handleDelete(row, $index)">
            {{ $t("AbpUi['Delete']") }}
          </el-button>
        </template>
      </el-table-column>
    </el-table>

    <el-dialog :title=" dialogStatus == 'create'? $t('AbpIdentityServer[\'Propertites:New\']'): $t('AbpUi[\'Edit\']')" :visible.sync="dialogFormVisible" append-to-body>
      <el-form ref="dataForm" :rules="rules" :model="temp" label-position="right" label-width="150px">
        <el-form-item :label="$t('AbpIdentityServer[\'Propertites:Key\']')" prop="key">
          <el-input v-model="temp.key" />
        </el-form-item>
        <el-form-item :label="$t('AbpIdentityServer[\'Propertites:Value\']')" prop="value">
          <el-input v-model="temp.value" />
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

import baseListQuery, {
  checkPermission
} from '@/utils/abp'

/*
1. 将propertie 的数组从父组件传递进入子组件中。
2. 在子组件中，添加更新修改删除数据。
3. 修改后的 propertie的数据传递给父组件
4. 在父组件中一起保存数据

*/

export default {
  name: 'Properties',
  components: {
  },
  props: {
    properties: {
      type: Object,
      require: false,
      // 对象或数组默认值必须从一个工厂函数获取
      default: function() {
        return {}
      }
    }
  },
  data() {
    return {
      tableKey: 0,
      list: null,
      total: 0,
      listLoading: true,
      listQuery: baseListQuery,
      temp: {
        key: '',
        value: ''
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
  },
  methods: {
    checkPermission, // 检查权限

    // 获取列表数据
    getList() {
      this.listLoading = true

      var data = []
      // console.log('this.properties', this.properties)
      var properties = this.properties
      // 将对象转为数组
      Object.keys(properties).forEach(function(key) {
        console.log('person', key, ':', properties[key])
        var item = {
          key: key,
          value: properties[key]
        }
        data.push(item)
      })
      // console.log('this.data', data)
      this.list = data
      this.listLoading = false
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
        key: '',
        value: ''
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
          this.list.unshift(this.temp)
          this.dialogFormVisible = false
          // 触发子组件设置userClaims的事件，然后父组件监听该事件
          console.log('this.list', this.list)
          this.$emit('set-properties', this.list)
          this.$notify({
            title: this.$i18n.t("TigerUi['Success']"),
            message: '请别忘记点击保存按钮哦',
            type: 'success',
            duration: 2000
          })
        }
      })
    },

    // 更新按钮点击
    handleUpdate(row) {
      this.temp = Object.assign({}, row) // copy obj
      this.dialogStatus = 'update'
      this.dialogFormVisible = true
      this.$nextTick(() => {
        this.$refs['dataForm'].clearValidate()
      })
    },

    // 更新数据
    updateData() {
      this.$refs['dataForm'].validate(valid => {
        if (valid) {
          const index = this.list.findIndex((v) => v.key === this.temp.key)
          this.list.splice(index, 1, this.temp)
          this.dialogFormVisible = false
          this.$emit('set-properties', this.list)
          this.$notify({
            title: this.$i18n.t("TigerUi['Success']"),
            message: this.$i18n.t("TigerUi['SuccessMessage']"),
            type: 'success',
            duration: 2000
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
        const index = this.list.findIndex((v) => v.key === row.key)
        this.list.splice(index, 1)
        this.$emit('set-properties', this.list)
        this.$notify({
          title: this.$i18n.t("TigerUi['Success']"),
          message: this.$i18n.t("TigerUi['SuccessMessage']"),
          type: 'success',
          duration: 2000
        })
      })
    }
  }
}
</script>
