<template>
  <div class="model-container">
    <el-dialog :title=" dialogStatus == 'create'? $t('AppExam[\'Exam:AddNew\']'): $t('AbpUi[\'Edit\']')" :visible.sync="dialogFormVisible">
      <el-form ref="dataForm" :rules="rules" :model="temp" label-position="right" label-width="150px">
        <el-form-item :label="$t('AppExam[\'DisplayName:Name\']')" prop="name">
          <el-input v-model="temp.name" />
        </el-form-item>
        <el-form-item :label="$t('AppExam[\'DisplayName:Number\']')" prop="number">
          <el-input v-model="temp.number" />
        </el-form-item>
        <el-form-item :label="$t('AppExam[\'DisplayName:CoverUrl\']')" prop="coverUrl">
          <el-input v-model="temp.coverUrl" />
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
  getExam,
  createExam,
  updateExam
} from '@/api/exam/exam'

export default {
  name: 'ExamModel',
  data() {
    return {
      temp: {
        id: undefined,
        courseId: undefined,
        testPaperId: '8B62B2F7-21BC-D83E-768F-3A0EAD76BDF4',
        questionCategoryId: undefined,
        name: undefined,
        coverUrl: undefined,
        number: undefined,
        examType: 1,
        startDate: undefined,
        endDate: undefined,
        examDuration: 0,
        isDifferent: true,
        isDifferentOrder: true,
        isShowScore: true,
        isShowError: true,
        isEnable: true,
        isExamAnyTime: true,
        isShowWindowOnblur: true,
        maxExamCount: 0,
        sorting: 0,
        onlyExamDayVisible: true,
        isStartSync: true,
        isShowHelp: true,
        halftimeFlag: true,
        halftimeStart: undefined,
        halftimeEnd: undefined,
        deductionAmounnt: 0,
        deductionInterval: 0,
        interval: 0
      },
      dialogFormVisible: false,
      dialogStatus: '',

      // 表单验证规则
      rules: {
        name: [
          {
            required: true,
            message: this.$i18n.t("AbpValidation['The {0} field is required.']", [
              this.$i18n.t("AppExam['DisplayName:Name']")
            ]),
            trigger: 'blur'
          },
          {
            max: 64,
            message: this.$i18n.t(
              "AbpValidation['The field {0} must be a string with a maximum length of {1}.']",
              [this.$i18n.t("AppExam['DisplayName:Name']"), '64']
            ),
            trigger: 'blur'
          }
        ],
        displayName: [
          {
            required: true,
            message: this.$i18n.t("AbpValidation['The {0} field is required.']", [
              this.$i18n.t("AppExam['DisplayName:DisplayName']")
            ]),
            trigger: 'blur'
          },
          {
            max: 256,
            message: this.$i18n.t(
              "AbpValidation['The field {0} must be a string with a maximum length of {1}.']",
              [this.$i18n.t("AppExam['DisplayName:Name']"), '256']
            ),
            trigger: 'blur'
          }
        ]

      }
    }
  },
  methods: {
    // 重置表单
    resetTemp() {
      this.temp = {
        id: undefined,
        courseId: undefined,
        testPaperId: '8B62B2F7-21BC-D83E-768F-3A0EAD76BDF4',
        questionCategoryId: undefined,
        name: undefined,
        coverUrl: undefined,
        number: undefined,
        examType: 1,
        startDate: undefined,
        endDate: undefined,
        examDuration: 0,
        isDifferent: true,
        isDifferentOrder: true,
        isShowScore: true,
        isShowError: true,
        isEnable: true,
        isExamAnyTime: true,
        isShowWindowOnblur: true,
        maxExamCount: 0,
        sorting: 0,
        onlyExamDayVisible: true,
        isStartSync: true,
        isShowHelp: true,
        halftimeFlag: true,
        halftimeStart: undefined,
        halftimeEnd: undefined,
        deductionAmounnt: 0,
        deductionInterval: 0,
        interval: 0
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
          createExam(this.temp).then(() => {
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
    },

    // 更新按钮点击
    handleUpdate(row) {
      this.dialogStatus = 'update'
      this.dialogFormVisible = true
      getExam(row.id).then(response => {
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
          updateExam(this.temp.id, this.temp).then(() => {
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

<style scoped>
.line{
  text-align: center;
}
</style>

