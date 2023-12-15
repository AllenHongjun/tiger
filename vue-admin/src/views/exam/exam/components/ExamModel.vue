<template>
  <div class="model-container">
    <el-dialog class="large-dialog" :title=" dialogStatus == 'create'? $t('AppExam[\'Permission:Create\']'): $t('AbpUi[\'Edit\']')" :visible.sync="dialogFormVisible" top="1vh" width="99%">
      <el-tabs v-model="activeName">
        <el-tab-pane label="基本信息" name="first">
          <el-form ref="dataForm" :rules="rules" :model="temp" label-position="right" label-width="150px">
            <el-form-item :label="$t('AppExam[\'DisplayName:TestPaperName\']')" prop="testPaperId">
              <el-select v-model="temp.testPaperId" placeholder="请选择" filterable clearable>
                <el-option
                  v-for="item in testPaperOptions"
                  :key="item.id"
                  :label="item.name"
                  :value="item.id"
                />
              </el-select>
            </el-form-item>
            <el-form-item :label="$t('AppExam[\'DisplayName:Name\']')" prop="name">
              <el-input v-model="temp.name" />
            </el-form-item>
            <el-form-item :label="$t('AppExam[\'DisplayName:Number\']')" prop="number">
              <el-input v-model="temp.number" />
            </el-form-item>

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
            <el-row>
              <el-col :span="12">
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
              </el-col>
              <el-col :span="12">
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
              </el-col>
            </el-row>
            <el-row>
              <el-col :span="12">
                <el-form-item prop="maxExamCount">
                  <span slot="label">
                    启用
                  </span>
                  <el-switch v-model="temp.isEnable" />
                </el-form-item>
              </el-col>
              <el-col :span="12">
                <el-form-item prop="maxExamCount">
                  <span slot="label">
                    成绩设置
                  </span>
                  <el-radio v-model="temp.isShowScore" :label="true">交卷后显示成绩</el-radio>
                  <el-radio v-model="temp.isShowScore" :label="false">交卷后不显示成绩</el-radio>
                </el-form-item>
              </el-col>
            </el-row>
            <el-form-item :label="$t('AppExam[\'DisplayName:CoverUrl\']')" prop="coverUrl">
              <single-image-upload v-model="temp.coverUrl" @input="input" />
            </el-form-item>

          </el-form>
        </el-tab-pane>
        <el-tab-pane label="指定考生(15)" name="second">
          <!-- <examinee-table ref="ExamineeTable" /> -->
          <examinee-select-table />
        </el-tab-pane>

      </el-tabs>

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

import {
  getAllTestPaper
} from '@/api/exam/test-paper'
import SingleImageUpload from '@/components/Upload/SingleImage.vue'
import ExamineeTable from './ExamineeTable.vue'
import ExamineeSelectTable from './ExamineeSelectTable.vue'

export default {
  name: 'ExamModel',
  components: {
    SingleImageUpload,
    ExamineeTable,
    ExamineeSelectTable
  },
  data() {
    return {
      activeName: 'second',
      testPaperOptions: [],
      examTimeRange: [],
      temp: {
        id: undefined,
        courseId: undefined,
        testPaperId: undefined,
        name: undefined,
        coverUrl: undefined,
        number: undefined,
        examType: 1,
        startDate: undefined,
        endDate: undefined,
        examDuration: 0,
        sorting: 0,
        isEnable: true,
        passingScore: 60,
        maxExamCount: 0,

        isDifferent: true,
        isDifferentOrder: true,
        isShowScore: true,
        isShowError: true,
        isExamAnyTime: true,
        isShowWindowOnblur: true,
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
  created() {
    this.fetchTestPaperOptions()
  },
  methods: {
    fetchTestPaperOptions() {
      getAllTestPaper().then(response => {
        this.testPaperOptions = response.items
      })
    },
    // 重置表单
    resetTemp() {
      this.temp = {
        id: undefined,
        courseId: undefined,
        testPaperId: undefined,
        name: undefined,
        coverUrl: undefined,
        number: undefined,
        examType: 1,
        startDate: undefined,
        endDate: undefined,
        examDuration: 60,
        sorting: 0,
        isEnable: true,
        passingScore: 60,
        maxExamCount: 1,
        isShowScore: true,

        isDifferent: true,
        isDifferentOrder: true,
        isShowError: true,
        isExamAnyTime: true,
        isShowWindowOnblur: true,
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

    input(url) {
      this.temp.coverUrl = url
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
      if (this.examTimeRange) {
        this.temp.startDate = this.examTimeRange[0]
        this.temp.endDate = this.examTimeRange[1]
      }

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
        this.examTimeRange = [this.temp.startDate, this.temp.endDate]
      })

      this.$nextTick(() => {
        this.$refs['dataForm'].clearValidate()
      })
    },

    // 更新数据
    updateData() {
      if (this.examTimeRange) {
        this.temp.startDate = this.examTimeRange[0]
        this.temp.endDate = this.examTimeRange[1]
      }

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

