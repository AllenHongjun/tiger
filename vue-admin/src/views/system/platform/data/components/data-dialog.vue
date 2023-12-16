<template>
  <el-dialog :title="dialogStatus == 'create'? $t('AppPlatform[\'Data:AddNew\']'): $t('AppPlatform[\'Data:Edit\']')" :visible.sync="dialogFormVisible">
    <el-form ref="dataForm" :rules="rules" :model="temp" label-position="right" label-width="180px">
      <!-- <el-form-item :label="$t('AppPlatform[\'DisplayName:ParentId\']')" prop="parentId">
        <el-input v-model="temp.parentId" />
      </el-form-item> -->
      <el-form-item :label="$t('AppPlatform[\'DisplayName:Name\']')" prop="name">
        <el-input v-model="temp.name" />
      </el-form-item>
      <el-form-item :label="$t('AppPlatform[\'DisplayName:DisplayName\']')" prop="displayName">
        <el-input v-model="temp.displayName" />
      </el-form-item>
      <el-form-item :label="$t('AppPlatform[\'DisplayName:Description\']')" prop="description">
        <el-input v-model="temp.description" />
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
</template>

<script>

import {
  createData,
  updateData,
  getData
} from '@/api/system-manage/platform/data'

export default {
  name: 'DataDialog',
  data() {
    return {
      temp: {
        id: undefined,
        parentId: undefined,
        name: '',
        displayName: '',
        description: ''
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
        ]
      },
      dialogStatus: '',
      dialogFormVisible: false
    }
  },
  methods: {
    resetTemp() {
      this.temp = {
        id: undefined,
        parentId: undefined,
        name: '',
        displayName: '',
        description: ''
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
          createData(this.temp).then(() => {
            this.$emit('handleFilter')
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

      getData(row.id).then(response => {
        this.temp = response
      })

      this.$nextTick(() => {
        this.$refs['dataForm'].clearValidate()
      })
    },
    updateData() {
      this.$refs['dataForm'].validate(valid => {
        if (valid) {
          updateData(this.temp.id, this.temp).then(() => {
            this.$emit('handleFilter', false)
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
    }
  }
}
</script>
