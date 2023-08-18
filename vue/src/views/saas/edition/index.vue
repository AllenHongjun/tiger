<template>
  <div class="app-container">
    <el-row :gutter="0">
      <el-col :span="24">
        <el-row style="margin-bottom: 20px">
          <el-input v-model="listQuery.filter" :placeholder="$t('AbpUi[\'PagerSearch\']')" style="width: 150px" class="filter-item" />
          <el-button class="filter-item" type="primary" icon="el-icon-search" @click="handleFilter">搜索</el-button>
          <el-button type="primary" icon="el-icon-edit" @click="handleCreate">创建</el-button>
          <el-button class="filter-item" style="margin-left: 10px;" icon="el-icon-refresh" @click="handleRefresh">{{ $t("AbpIdentity['Refresh']") }}</el-button>
        </el-row>
        <el-table :key="tableKey" v-loading="listLoading" :data="list" element-loading-text="Loading" border fit highlight-current-row @sort-change="sortChange">

          <el-table-column type="selection" width="55" />
          <el-table-column type="index" width="55" />
          <el-table-column align="left" :label="$t('AbpSaas[\'DisplayName:EditionName\']')" prop="displayName">
            <template slot-scope="{ row }">{{ row.displayName }}</template>
          </el-table-column>

          <el-table-column align="left" :label="$t('AbpIdentity[\'Actions\']')" width="320">
            <template slot-scope="{row,$index}">
              <el-button type="primary" @click="handleUpdate(row)">
                {{ $t("AbpUi['Edit']") }}
              </el-button>
              <el-button type="danger" @click="deleteData(row,$index)">
                {{ $t("AbpUi['Delete']") }}
              </el-button>
            </template>
          </el-table-column>
        </el-table>

        <pagination v-show="total > 0" :total="total" :page.sync="listQuery.page" :limit.sync="listQuery.limit" @pagination="fetchData" />

        <el-dialog :title="dialogStatus == 'create'? '创建': $t('AbpUi[\'Edit\']')" :visible.sync="dialogFormVisible">
          <el-form ref="dataForm" :rules="rules" :model="temp" label-width="120px" label-position="right">
            <el-form-item :label="$t('AbpSaas[\'DisplayName:EditionName\']')" prop="displayName">
              <el-input v-model="temp.displayName" />
            </el-form-item>

          </el-form>
          <div style="text-align: right">
            <el-button type="danger" @click="dialogFormVisible = false">{{ $t("AbpUi['Cancel']") }}</el-button>
            <el-button type="primary" @click="dialogStatus === 'create' ? createData() : updateData()">{{ $t("AbpUi['Save']") }}</el-button>
          </div>
        </el-dialog>
      </el-col>
    </el-row>
  </div>
</template>

<script>
import baseListQuery from '@/utils/abp'
import {
  getEditions,
  deleteEdition,
  createEdition,
  updateEdition
} from '@/api/sass/edition'

import {
  getFeatures,
  updateFeatures
} from '@/api/sass/features'
import {
  checkPermission
} from '@/utils/abp'

import Pagination from '@/components/Pagination' // Secondary package based on el-pagination

export default {
  name: 'Edition',
  components: {
    Pagination
  },
  data() {
    return {
      tableKey: 0,
      list: null,
      listLoading: true,
      total: 0,
      listQuery: Object.assign({
      }, baseListQuery),

      dialogStatus: '',
      dialogFormVisible: false,
      temp: {
        id: undefined,
        displayName: undefined

      },
      rules: {
        displayName: [
          {
            required: true,
            message: '请输入版本名称',
            trigger: 'blur'
          },
          {
            max: 256,
            message: '版本名称不能超过256个字符',
            trigger: 'blur'
          }
        ]
      }
    }
  },

  created() {
    this.fetchData()
  },
  methods: {
    checkPermission,
    fetchData() {
      this.listLoading = true
      getEditions(this.listQuery).then(response => {
        this.list = response.items
        this.total = response.totalCount
        this.listLoading = false
      })
    },
    sortChange(data) {
      const {
        prop,
        order
      } = data
      this.listQuery.sort = order ? `${prop} ${order}` : undefined
      this.handleFilter()
    },
    handleFilter() {
      this.listQuery.page = 1
      this.fetchData()
    },
    handleRefresh() {
      this.handleFilter()
    },
    resetTemp() {
      this.temp = {
        id: undefined,
        displayName: undefined
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
      this.$refs['dataForm'].validate((valid) => {
        if (valid) {
          // 获取不到this.temp
          this.temp.id = parseInt(Math.random() * 100) + 1024 // mock a id
          createEdition(this.temp).then(() => {
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
    handleUpdate(row) {
      this.temp = Object.assign({}, row) // copy obj
      this.dialogStatus = 'update'
      this.dialogFormVisible = true
      this.$nextTick(() => {
        this.$refs['dataForm'].clearValidate()
      })
    },
    updateData() {
      this.$refs['dataForm'].validate((valid) => {
        if (valid) {
          const tempData = Object.assign({}, this.temp)
          updateEdition(tempData.id, tempData).then(() => {
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
    deleteData(row) {
      this.$confirm('此操作将永久删除数据, 是否继续?', '提示', {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        type: 'warning'
      }).then(() => {
        deleteEdition(row.id)
          .then((response) => {
            const index = this.list.findIndex((v) => v.id === row.id)
            this.list.splice(index, 1)
            this.$message({
              title: this.$i18n.t("TigerUi['Success']"),
              message: this.$i18n.t("TigerUi['SuccessMessage']"),
              type: 'success',
              duration: 2000
            })
          })
          .catch((err) => {
            console.log(err)
          })
      }).catch(() => {
        this.$message({
          type: 'info',
          message: '已取消删除'
        })
      })
    }

  }
}
</script>

<style lang="scss" scoped>
.permissionTree {
    height: 450px;
    overflow-y: scroll;
}
</style>
