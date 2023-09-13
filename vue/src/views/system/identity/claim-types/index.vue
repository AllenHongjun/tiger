<template>
  <div class="app-container">
    <div class="filter-container">
      <el-input v-model="listQuery.filter" :placeholder="$t('AbpUi[\'PagerSearch\']')" clearable style="width: 200px;" class="filter-item" @keyup.enter.native="handleFilter" />
      <el-button type="primary" icon="el-icon-search" class="filter-item" @click="handleFilter">
        {{ $t('AbpIdentity.Search') }}
      </el-button>

      <el-button v-if="checkPermission('AbpIdentity.IdentityClaimTypes.Create')" class="filter-item" style="margin-left: 10px;" type="primary" icon="el-icon-edit" @click="handleCreate">
        {{ $t("AbpIdentity['AddClaim']") }}
      </el-button>
    </div>

    <el-table :key="tableKey" v-loading="listLoading" :data="list" border fit highlight-current-row style="width: 100%;" @sort-change="sortChange">
      <el-table-column type="selection" width="55" align="center" />
      <el-table-column align="center" label="ID" width="95">
        <template slot-scope="scope">{{ scope.$index }}</template>
      </el-table-column>
      <el-table-column :label="$t('AbpIdentity[\'IdentityClaim:Name\']')" prop="name" sortable align="center">
        <template slot-scope="{ row }">
          <span>{{ row.name }}</span>
        </template>
      </el-table-column>
      <el-table-column :label="$t('AbpIdentity[\'IdentityClaim:ValueType\']')" align="center">
        <template slot-scope="{ row }">
          <span>{{ row.valueTypeAsString }}</span>
        </template>
      </el-table-column>
      <el-table-column :label="$t('AbpIdentity[\'IdentityClaim:Description\']')" prop="description" sortable align="center">
        <template slot-scope="{ row }">
          <span>{{ row.description }}</span>
        </template>
      </el-table-column>
      <el-table-column :label="$t('AbpIdentity[\'IdentityClaim:Regex\']')" prop="regex" sortable align="center">
        <template slot-scope="{ row }">
          <span>{{ row.regex }}</span>
        </template>
      </el-table-column>

      <el-table-column :label="$t('AbpIdentity[\'IdentityClaim:Required\']')" prop="required" sortable align="center">
        <template slot-scope="{ row }">
          <el-tag :type="( row.required ? 'success' : 'danger')" :class="[row.required ? 'el-icon-check' : 'el-icon-close']" />
        </template>
      </el-table-column>
      <el-table-column :label="$t('AbpIdentity[\'IdentityClaim:IsStatic\']')" prop="static" sortable align="center">
        <template slot-scope="{ row }">
          <el-tag :type="( row.isStatic ? 'success' : 'danger')" :class="[row.isStatic ? 'el-icon-check' : 'el-icon-close']" />
        </template>
      </el-table-column>

      <el-table-column :label="$t('AbpUi[\'Actions\']')" align="center" width="200" class-name="small-padding fixed-width">
        <template slot-scope="{ row, $index }">
          <el-button v-if="!row.isStatic && checkPermission('AbpIdentity.IdentityClaimTypes.Update')" type="primary" @click="handleUpdate(row)">
            {{ $t("AbpUi['Edit']") }}
          </el-button>
          <el-button v-if="!row.isStatic && checkPermission('AbpIdentity.IdentityClaimTypes.Delete')" type="danger" @click="handleDelete(row, $index)">
            {{ $t("AbpUi['Delete']") }}
          </el-button>
        </template>
      </el-table-column>
    </el-table>

    <pagination v-show="total > 0" :total="total" :page.sync="listQuery.page" :limit.sync="listQuery.limit" @pagination="getList" />

    <el-dialog
      :title="
        dialogStatus == 'create'? $t('AbpIdentity[\'AddClaim\']'): $t('AbpUi[\'Edit\']')"
      :visible.sync="dialogFormVisible"
    >
      <el-form ref="dataForm" :rules="rules" :model="temp" label-position="right" label-width="150px">
        <el-form-item :label="$t('AbpIdentity[\'IdentityClaim:Name\']')" prop="name">
          <el-input v-model="temp.name" />
        </el-form-item>
        <el-form-item :label="$t('AbpIdentity[\'IdentityClaim:ValueType\']')">
          <el-select v-model="temp.valueType">
            <el-option :value="0" label="String" />
            <el-option :value="1" label="Int" />
            <el-option :value="2" label="Boolean" />
            <el-option :value="3" label="DateTime" />
          </el-select>
        </el-form-item>
        <el-form-item>
          <el-checkbox v-model="temp.required" :label="$t('AbpIdentity[\'IdentityClaim:Required\']')" />
        </el-form-item>
        <el-form-item :label="$t('AbpIdentity[\'IdentityClaim:Regex\']')" prop="regex">
          <el-input v-model="temp.regex" />
        </el-form-item>
        <el-form-item :label="$t('AbpIdentity[\'IdentityClaim:RegexDescription\']')" prop="regexDescription">
          <el-input v-model="temp.regexDescription" />
        </el-form-item>
        <el-form-item :label="$t('AbpIdentity[\'IdentityClaim:Description\']')" prop="description">
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
import {
  getClaimTypes,
  getClaimType,
  // getClaimTypesAll,
  createClaimType,
  updateClaimType,
  deleteClaimType
} from '@/api/system-manage/identity/claim-type'
import Pagination from '@/components/Pagination' // secondary package based on el-pagination
import baseListQuery, {
  checkPermission
} from '@/utils/abp'

export default {
  name: 'ClaimTypes',
  components: {
    Pagination
  },
  data() {
    return {
      tableKey: 0,
      list: null,
      total: 0,
      listLoading: true,
      listQuery: baseListQuery,
      temp: {
        id: undefined,
        name: '',
        required: false,
        isStatic: false,
        regex: '',
        regexDescription: '',
        description: '',
        valueType: 0
      },
      dialogFormVisible: false,
      dialogStatus: '',

      // 表单验证规则
      rules: {
        name: [{
          required: true,
          message: this.$i18n.t("AbpIdentity['The {0} field is required.']", [
            this.$i18n.t("AbpIdentity['IdentityClaim:Name']")
          ]),
          trigger: 'blur'
        },
        {
          // 在abp表单验证模块里面自带的配置规则
          max: 256,
          message: this.$i18n.t(
            "AbpIdentity['The field {0} must be a string with a maximum length of {1}.']",
            [this.$i18n.t("AbpIdentity['IdentityClaim:Name']"), '256']
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
      getClaimTypes(this.listQuery).then(response => {
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
        name: '',
        required: false,
        isStatic: false,
        regex: '',
        regexDescription: '',
        description: '',
        valueType: 0
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
          createClaimType(this.temp).then(() => {
            this.list.unshift(this.temp)
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
      debugger
      this.temp = Object.assign({}, row) // copy obj
      this.dialogStatus = 'update'
      this.dialogFormVisible = true

      getClaimType(row.id).then(response => {
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
          updateClaimType(this.temp).then(() => {
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
        this.$i18n.t("AbpIdentity['WillDeleteClaim']", [
          row.name
        ]),
        // title
        this.$i18n.t("AbpIdentity['AreYouSure']"), {
          confirmButtonText: this.$i18n.t("AbpIdentity['Yes']"), // 确认按钮
          cancelButtonText: this.$i18n.t("AbpIdentity['Cancel']"), // 取消按钮
          type: 'warning' // 弹框类型
        }
      ).then(async() => {
        // 回调函数
        deleteClaimType(row.id).then(() => {
          const index = this.list.findIndex((v) => v.id === row.id)
          this.list.splice(index, 1)
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
