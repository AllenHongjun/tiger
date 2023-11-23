<template>
  <div class="model-container">
    <el-dialog :title=" dialogStatus == 'create'? $t('AppExam[\'Exam:AddNew\']'): $t('AbpUi[\'Edit\']')" :visible.sync="dialogFormVisible" top="6vh">
      <el-form ref="dataForm" :rules="rules" :model="temp" label-position="right" label-width="150px">
        <el-form-item prop="isEnable">
          <span slot="label">
            启用
          </span>
          <el-switch v-model="temp.isEnable" />
        </el-form-item>

        <el-form-item prop="maxExamCount">
          <span slot="label">
            课程完成条件
          </span>
          <el-radio-group v-model="temp.isShowScore">
            <el-radio :label="true">无完成条件：学员自由学习(不限学员学习时间)</el-radio>
            <el-radio :label="false">达到课程学时：学员达到学习时长</el-radio>
            <el-radio :label="false">通过课程考试：学员达到学习时长,并通过课程考试</el-radio>
          </el-radio-group>
        </el-form-item>

        <el-form-item prop="maxExamCount">
          <span slot="label">
            学时设置
          </span>
          <el-radio-group v-model="temp.isShowScore">
            <el-radio :label="true">达到课程总学时：学员学习总时长须超过60分钟</el-radio>
            <el-radio :label="false">达到每章节学时：学员须学完所有章节并达到每章设定学时</el-radio>
          </el-radio-group>
        </el-form-item>

        <el-form-item prop="maxExamCount">
          <span slot="label">
            学习监控
          </span>
          <el-checkbox-group v-model="temp.examSetting">
            <el-checkbox label="禁止拖动视频进度条播放" />
            <el-checkbox label="禁止复制图文课件内容" />
            <el-checkbox label="学习时，超过 10 分钟没有动作，将弹出提示确认，否则停止计时" />
          </el-checkbox-group>
        </el-form-item>

        <el-form-item prop="maxExamCount">
          <span slot="label">
            学习终端
          </span>
          <el-radio-group v-model="temp.isShowScore">
            <el-radio :label="true">不限制(所有终端可学习)</el-radio>
            <el-radio :label="false">仅限电脑端学习</el-radio>
          </el-radio-group>
        </el-form-item>

        <el-form-item prop="maxExamCount">
          <span slot="label">
            讨论答疑
          </span>
          <el-checkbox-group v-model="temp.questionSetting">
            <el-checkbox label="允许学员进行课程评论回复" />
            <el-checkbox label="课程评论及回复需要后台人工审核后显示" />
            <el-checkbox label="启用课程答疑（学员可向老师提问）" />
          </el-checkbox-group>
        </el-form-item>

        <el-form-item prop="isEnable">
          <span slot="label">
            课程学分
          </span>
          <el-input v-model="temp.questionSetting" type="number" />
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
  name: 'CourseSetting',
  data() {
    return {
      examTimeRange: [],
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
        interval: 0,
        examSetting: [],
        questionSetting: []
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

    // 更新按钮点击
    handleUpdate(row) {
      this.dialogStatus = 'update'
      this.dialogFormVisible = true
      // getExam(row.id).then(response => {
      //   this.temp = response
      // })

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

<style lang="scss" scoped>
.line{
  text-align: center;
}

::v-deep .el-radio{
  display: block;
  margin:10px 0;
}

//element ui el-checkbox竖排列 https://blog.csdn.net/weixin_43173924/article/details/100161380
.el-checkbox{
  display:block;
}
</style>

