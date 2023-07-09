<template>
  <el-dialog :title="dialogStatus == 'create'? $t('AbpTextTemplate[\'Permission:Create\']'): $t('AbpTextTemplate[\'Permission:Edit\']')" :visible.sync="dialogFormVisible">
    <el-form ref="dataForm" :rules="rules" :model="temp" label-position="right" label-width="180px">
      <el-form-item :label="$t('AbpTextTemplate[\'DisplayName:DisplayName\']')">
        <span> {{ temp.displayName }}</span>
      </el-form-item>
      <el-form-item :label="$t('AbpTextTemplate[\'DisplayName:Content\']')">

        <el-input v-model="temp.content" type="textarea" :autosize="{ minRows: 15, maxRows: 25}" placeholder="请输入内容" />
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
  getTextTemplateByName,
  getTextTemplateContentByName,
  updateTextTemplate,
  restoreToDefault
} from '@/api/system-manage/text-template'

export default {
  name: 'TextTemplateModel',
  data() {
    return {
      temp: {
        name: '',
        content: '',
        culture: '',
        displayName: ''
      },
      dialogStatus: '',
      dialogFormVisible: false

    }
  },
  methods: {
    resetTemp() {
      this.temp = {
        name: '',
        content: '',
        culture: '',
        displayName: ''
      }
    },
    handleUpdate(row) {
      this.temp = Object.assign({}, row) // copy obj
      this.dialogStatus = 'update'
      this.dialogFormVisible = true

      getTextTemplateContentByName(row.name).then(response => {
        this.temp = response
      })

      this.$nextTick(() => {
        this.$refs['dataForm'].clearValidate()
      })
    },
    updateData() {
      this.$refs['dataForm'].validate(valid => {
        if (valid) {
          updateTextTemplate(this.temp).then(() => {
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
