<template>
  <div class="model-container">
    <el-dialog :title=" dialogStatus == 'create'? $t('AppExam[\'Exam:AddNew\']'): $t('AbpUi[\'Edit\']')" :visible.sync="dialogFormVisible" top="8vh">
      <el-form ref="dataForm" :rules="rules" :model="temp" label-position="right" label-width="150px">
        <el-form-item prop="passingScore">
          <span slot="label">
            <el-tooltip content="考生通过考试的分数" placement="top">
              <i class="el-icon-question" />
            </el-tooltip>
            及格分数
          </span>
          <el-input v-model="temp.passingScore">
            <template slot="append">总分<span>35.0</span>分</template>
          </el-input>
        </el-form-item>

        <el-form-item prop="examDuration">
          <span slot="label">
            <el-tooltip content="允许考试进入考试或者练习的时间范围。比如设置12月1日到12月5日，考生只能在这个时间范围进入考试" placement="top">
              <i class="el-icon-question" />
            </el-tooltip>
            考试时间
          </span>
          <el-date-picker
            v-model="examTimeRange"
            type="datetimerange"
            range-separator="至"
            start-placeholder="开始日期"
            end-placeholder="结束日期"
          />
        </el-form-item>

        <el-form-item prop="examDuration">
          <span slot="label">
            <el-tooltip content="允许考试的答题时间,超过时间自动提交答卷" placement="top">
              <i class="el-icon-question" />
            </el-tooltip>
            答题时间
          </span>
          <el-input v-model="temp.examDuration" type="number">
            <template slot="append">分钟</template>
          </el-input>
        </el-form-item>

        <el-form-item prop="maxExamCount">
          <span slot="label">
            <el-tooltip content="允许每个考生参加的次数" placement="top">
              <i class="el-icon-question" />
            </el-tooltip>
            可考次数
          </span>
          <el-input v-model="temp.maxExamCount" type="number">
            <template slot="append">次/考生</template>
          </el-input>
        </el-form-item>

        <el-form-item prop="maxExamCount">
          <span slot="label">
            成绩设置
          </span>
          <el-radio v-model="temp.isShowScore" label="true">交卷后显示成绩</el-radio>
          <el-radio v-model="temp.isShowScore" label="false">交卷后不显示成绩</el-radio>
        </el-form-item>

        <el-form-item prop="maxExamCount">
          <span slot="label">
            启用
          </span>
          <el-switch v-model="temp.isEnable" />
        </el-form-item>

        <el-form-item prop="maxExamCount">
          <span slot="label">
            考试设置
          </span>
          <el-checkbox-group v-model="temp.examSetting">
            <el-checkbox label="交卷后显示考生名次" />
            <el-checkbox label="交卷后允许查看标准答案和试题解析" />
            <el-checkbox label="交卷后允许从成绩页面进入考生中心查看历史成绩" />
          </el-checkbox-group>
        </el-form-item>

        <el-form-item prop="maxExamCount">
          <span slot="label">
            试题设置
          </span>
          <el-checkbox-group v-model="temp.questionSetting">
            <el-checkbox label="必须答完所有试题才可提交试卷" />
            <el-checkbox label="多选题漏选可得分" />
            <el-checkbox label="不显示题型" />
            <el-checkbox label="不显示试题分数" />
            <el-checkbox label="启用试题水印 " />
          </el-checkbox-group>
        </el-form-item>

        <el-form-item prop="maxExamCount">
          <span slot="label">
            防作弊设置
          </span>
          <el-checkbox-group v-model="temp.examSetting">
            <el-checkbox label="只允许考生切换考试页面 5 次 ，否则强制交卷" />
            <el-checkbox label="答题时，超过300秒无操作，系统将强制交卷" />
            <el-checkbox label="禁止考生复制、粘贴、剪切" />
            <el-checkbox label="试题乱序" />
            <el-checkbox label="选项乱序" />
          </el-checkbox-group>
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
  name: 'ExamSetting',
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
      dialogFormVisible: true,
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

<style lang="scss" scoped>
.line{
  text-align: center;
}

//element ui el-checkbox竖排列 https://blog.csdn.net/weixin_43173924/article/details/100161380
.el-checkbox{
  display:block;
}
</style>

