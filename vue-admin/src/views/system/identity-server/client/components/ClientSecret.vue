<template>
  <div class="app-container">
    <div class="filter-container">
      <el-row>
        <el-button v-if="checkPermission('Platform.Layout.Create')" class="filter-item" type="primary" icon="el-icon-plus" @click="handleCreate">
          {{ $t("AbpIdentityServer['Permissions:Create']") }}
        </el-button>
      </el-row>
    </div>

    <el-table :key="tableKey" v-loading="listLoading" :data="list" border fit highlight-current-row :stripe="true" style="width: 100%;" @sort-change="sortChange">
      <el-table-column type="index" width="80" />
      <el-table-column :label="$t('AbpIdentityServer[\'Secret:Type\']')" prop="type" align="left" width="160">
        <template slot-scope="{ row }">
          <span>{{ row.type }}</span>
        </template>
      </el-table-column>
      <el-table-column :label="$t('AbpIdentityServer[\'Secret:Value\']')" prop="value" align="left" :show-overflow-tooltip="true">
        <template slot-scope="{ row }">
          <span>{{ row.value }}</span>
        </template>
      </el-table-column>
      <el-table-column :label="$t('AbpIdentityServer[\'Description\']')" prop="description" align="left" :show-overflow-tooltip="true">
        <template slot-scope="{ row }">
          <span>{{ row.description }}</span>
        </template>
      </el-table-column>
      <el-table-column :label="$t('AbpIdentityServer[\'Expiration\']')" prop="expiration" align="left" width="160">
        <template slot-scope="{ row }">
          <span>{{ row.expiration | moment }}</span>
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

    <el-dialog :title=" dialogStatus == 'create'? $t('AbpIdentityServer[\'Permissions:Create\']'): $t('AbpUi[\'Edit\']')" :visible.sync="dialogFormVisible" append-to-body>

      <el-form ref="dataForm" :model="temp" :rules="rules" label-width="140px" class="demo-ruleForm">
        <el-form-item label="Require Secret" prop="requireClientSecret">
          <span slot="label">
            <el-tooltip content="Specifies if a secret is required" placement="top">
              <i class="el-icon-question" />
            </el-tooltip>
            Require Secret
          </span>
          <el-switch v-model="temp.requireClientSecret" />
        </el-form-item>

        <el-form-item label="Type" prop="type" clearable>
          <el-select v-model="temp.type" placeholder="请选择Type">
            <el-option label="Shared Secret" value="Shared Secret" />
            <el-option label="X509 Certificate Base64" value="X509 Certificate Base64" />
            <el-option label="X509 Name" value="X509 Name" />
            <el-option label="X509 Thumbprint" value="X509 Thumbprint" />
          </el-select>
        </el-form-item>

        <el-form-item label="Value" prop="value">
          <!-- 优化：直接显示能看到提示 -->
          <span slot="label">
            <el-tooltip content="WARNING: you will not be able to access the value of secrets after creating them" placement="top">
              <i class="el-icon-question" />
            </el-tooltip>
            Value
          </span>
          <el-input v-model="temp.value">
            <el-button slot="append" icon="el-icon-refresh-right">生成</el-button>
          </el-input>
        </el-form-item>

        <el-form-item label="Expiration" prop="expiration">
          <el-switch v-model="temp.hasExpiration" />

        </el-form-item>

        <el-form-item prop="expiration">
          <el-date-picker
            v-if="temp.hasExpiration"
            v-model="temp.expiration"
            type="datetime"
            placeholder="选择过期时间"
            default-time="00:00:00"
          />
        </el-form-item>

        <el-form-item label="Description" prop="description">
          <el-input v-model="temp.description" type="textarea" :autosize="{ minRows: 4, maxRows: 6}" />
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
  name: 'ClientSecret',
  components: {
  },
  props: {
    clientSecrets: {
      type: Array,
      require: false,
      // 对象或数组默认值必须从一个工厂函数获取
      default: function() {
        return []
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
      dialogFormVisible: false,
      dialogStatus: '',

      temp: {
        requireClientSecret: undefined,
        type: undefined,
        value: undefined,
        description: undefined,
        hasExpiration: false,
        expiration: undefined

      },
      rules: {

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
      console.log('this.clientSecrets', this.clientSecrets)
      this.list = this.clientSecrets
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
        id: undefined,
        name: '',
        grantType: ''

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
          this.$emit('set-client-secret', this.list)
          this.$notify({
            title: this.$i18n.t("TigerUi['Success']"),
            message: this.$i18n.t("TigerUi['SuccessMessage']"),
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
          const index = this.list.findIndex((v) => v.id === this.temp.id)
          this.list.splice(index, 1, this.temp)
          // 通过自定义事件给父组件赋值
          this.$emit('set-client-secret', this.list)

          this.dialogFormVisible = false
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
        const index = this.list.findIndex((v) => v.id === row.id)
        this.list.splice(index, 1)
        this.$emit('set-client-secret', this.list)

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
