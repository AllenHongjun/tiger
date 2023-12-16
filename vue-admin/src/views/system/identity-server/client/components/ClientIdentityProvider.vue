<template>
  <div class="app-container">
    <el-form-item label="Enable Local Login" prop="enableLocalLogin">
      <span slot="label">
        <el-tooltip content="Allow IdentityServer local credentials (e.g. username and password)" placement="top">
          <i class="el-icon-question" />
        </el-tooltip>
        Enable Local Login
      </span>
      <el-switch v-model="ruleForm.enableLocalLogin" />
    </el-form-item>

    <el-divider />

    <div class="filter-container">
      <el-row>
        <el-button class="filter-item" type="primary" icon="el-icon-plus" @click="handleCreate">
          {{ $t("AbpIdentityServer['Permissions:Create']") }}
        </el-button>
      </el-row>
    </div>

    <el-table :key="tableKey" v-loading="listLoading" :data="list" border fit highlight-current-row :stripe="true" style="width: 100%;" @sort-change="sortChange">
      <el-table-column type="index" width="80" />
      <el-table-column :label="$t('AbpIdentityServer[\'Name\']')" prop="provider" align="left">
        <template slot-scope="{ row }">
          <span>{{ row.provider }}</span>
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

    <el-dialog :title=" dialogStatus == 'create'? $t('AbpIdentityServer[\'Permissions:Create\']'): $t('AbpUi[\'Edit\']')" :visible.sync="dialogFormVisible" append-to-body>
      <el-form ref="dataForm" :rules="rules" :model="temp" label-position="right" label-width="150px">
        <el-form-item :label="$t('AbpIdentityServer[\'Name\']')">
          <el-input v-model="temp.provider" />
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

export default {
  name: 'ClientIdentityProvider',
  components: {
  },
  props: {
    identityProviderRestrictions: {
      type: Array,
      require: false,
      // 对象或数组默认值必须从一个工厂函数获取
      default: function() {
        return []
      }
    },
    ruleForm: {
      type: Object,
      require: false,
      // 对象或数组默认值必须从一个工厂函数获取
      default: function() {
        return {
          enableLocalLogin: true
        }
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
        provider: ''
      },
      dialogFormVisible: false,
      dialogStatus: '',

      // 表单验证规则
      rules: {
        provider: [
          {
            required: true,
            message: this.$i18n.t("AbpValidation['The {0} field is required.']", [
              this.$i18n.t("AbpIdentityServer['Name']")
            ]),
            trigger: 'blur'
          },
          {
            max: 200,
            message: this.$i18n.t(
              "AbpValidation['The field {0} must be a string with a maximum length of {1}.']",
              [this.$i18n.t("AbpIdentityServer['Name']"), '200']
            ),
            trigger: 'blur'
          }
        ]
      }
    }
  },
  created() {
    // 将子组件的表单验证规则传递给父组件
    this.$emit('set-form-rules', this.rules)
    this.getList()
  },
  methods: {
    checkPermission, // 检查权限

    // 获取列表数据
    getList() {
      // console.log('this.identityProviderRestrictions', this.identityProviderRestrictions)
      this.list = this.identityProviderRestrictions
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
        provider: ''
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
          // this.$emit('set-identity-provider-restrictions', this.list)
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
          const index = this.list.findIndex((v) => v === this.temp)
          this.list.splice(index, 1, this.temp)
          this.dialogFormVisible = false
          // this.$emit('set-identity-provider-restrictions', this.list)
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
        const index = this.list.findIndex((v) => v === row)
        this.list.splice(index, 1)
        this.$emit('set-identity-provider-restrictions', this.list)
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
