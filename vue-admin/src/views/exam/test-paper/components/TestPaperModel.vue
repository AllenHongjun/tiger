<template>
  <div class="model-container">
    <el-dialog class="test-paper-dialog" :title=" dialogStatus == 'create'? $t('AppExam[\'Permission:Create\']'): $t('AbpUi[\'Edit\']')" :visible.sync="dialogFormVisible" top="1vh" width="99%">
      <el-row>
        <el-steps :active="active" simple>
          <el-step title="基本信息" icon="el-icon-edit" @click.native="active = 0">1</el-step>
          <el-step title="设计试卷" icon="el-icon-upload" @click.native="active = 1">2</el-step>
        </el-steps>
      </el-row>

      <el-card v-if="active == 0">
        <el-form ref="dataForm" label-position="right" label-width="280px" :model="temp" :rules="rules">
          <el-form-item label="名称" prop="name">
            <el-input v-model="temp.name" />
          </el-form-item>

          <el-form-item label="编号" prop="number">
            <el-input v-model="temp.number" />
          </el-form-item>

        </el-form>
      </el-card>

      <el-row v-if="active == 1" :gutter="20">
        <el-col :span="6">
          <mini-paper-section ref="miniPaperSection" :test-paper="temp" @getTestPaperSections="handleGetAllTestPaperSections" />
        </el-col>
        <el-col v-if="testPaperSections.length > 0" :span="18">
          <div v-for="(testPaperSection, index) in testPaperSections" :key="index">
            <el-card class="section-box">
              <div slot="header" class="clearfix">
                <h3 class="section-title"> {{ testPaperSection.name }} <span>(共 <b>{{ testPaperSection.questionCount }}</b>  题 <b>{{ testPaperSection.totalScore }}</b>  分)</span></h3>
                <div style="float: right;">
                  <el-button plain type="primary" class="el-icon-plus" @click="handleUpdateSectionDescription()">添加大题描述</el-button>
                  <el-button plain>选项乱序</el-button>
                </div>
                <fixed-paper-section v-if="testPaperSection.type == 1" ref="fixedPaperSection" />
                <random-paper-section v-else-if="testPaperSection.type == 2" ref="randomPaperSection" :test-paper-section-id="testPaperSection.id" :test-paper-id="testPaperId" @update-test-paper-strategy="handelUpdateTestPaperStrategy" />
              </div>
            </el-card>

          </div>
        </el-col>
        <el-col v-else :span="18">
          <el-card>
            <h1>请添加试卷大题</h1>
          </el-card>
        </el-col>
      </el-row>

      <div slot="footer" class="dialog-footer">
        <el-button @click="dialogFormVisible = false">
          {{ $t("AbpUi['Cancel']") }}
        </el-button>
        <el-button type="primary" @click="dialogStatus === 'create' ? createData() : updateData()">
          {{ $t("AbpUi['Save']") }}
        </el-button>
      </div>
    </el-dialog>

    <el-dialog title="大题描述" :visible.sync="dialogTestPaperSectionDescriptionFormVisible" append-to-body>
      <el-form :model="testPaperSectionDescriptionForm">
        <el-form-item label="大题描述" label-width="200" props="description">
          <tinymce v-model="testPaperSectionDescriptionForm.description" :height="300" />
        </el-form-item>
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button @click="dialogTestPaperSectionDescriptionFormVisible = false">取 消</el-button>
        <el-button type="primary" @click="dialogTestPaperSectionDescriptionFormVisible = false">确 定</el-button>
      </div>
    </el-dialog>
  </div>
</template>

<script>
import Tinymce from '@/components/Tinymce'
import baseListQuery, { checkPermission } from '@/utils/abp'
import {
  getTestPaper,
  createTestPaper,
  updateTestPaper
} from '@/api/exam/test-paper'
import {
  getAllTestPaperSections
} from '@/api/exam/test-paper-section'

import MiniPaperSection from './MiniPaperSection.vue'
import RandomPaperSection from './RandomPaperSection.vue'
import FixedPaperSection from './FixedPaperSection.vue'

export default {
  name: 'TestPaperModel',
  components: {
    Tinymce,
    MiniPaperSection,
    RandomPaperSection,
    FixedPaperSection
  },
  data() {
    return {
      testPaperId: undefined,
      activeIndex2: 1,
      active: 1,
      dialogFormVisible: false,
      dialogStatus: '',
      temp: {
        id: undefined,
        testPaperMainId: undefined,
        number: undefined,
        name: undefined,
        type: 1,
        isComposing: true,
        enable: true,
        isIncludeAllSchoolTeachers: true,
        isLimitJudgeTime: true,
        judgeStartTime: undefined,
        judgeEndTime: undefined,
        testPaperSections: [
        ]
      },
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
        ]

      },
      testPaperSections: [],
      testPaperSectionDescriptionForm: {
        id: undefined,
        description: undefined
      },
      dialogTestPaperSectionDescriptionFormVisible: false
    }
  },
  methods: {
    checkPermission,
    next() {
      if (this.active++ > 2) this.active = 0
    },
    // 重置表单
    resetTemp() {
      this.temp = {
        testPaperMainId: undefined,
        number: undefined,
        name: undefined,
        type: 1,
        isComposing: true,
        enable: true,
        isIncludeAllSchoolTeachers: true,
        isLimitJudgeTime: true,
        judgeStartTime: undefined,
        judgeEndTime: undefined
      }
    },

    // 点击创建按钮
    handleCreate() {
      this.resetTemp()
      this.dialogStatus = 'create'
      this.dialogFormVisible = true
      // this.$nextTick(() => {
      //   this.$refs['dataForm'].clearValidate()
      // })
    },

    // 创建数据
    createData() {
      this.$refs['dataForm'].validate(valid => {
        if (valid) {
          createTestPaper(this.temp).then(() => {
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

    // 获取试卷详情
    handleDetail(id) {
      getTestPaper(id).then(response => {
        this.temp = response
      })
    },

    // 更新按钮点击
    handleUpdate(row) {
      this.testPaperId = row.id

      this.dialogStatus = 'update'
      this.dialogFormVisible = true
      getTestPaper(row.id).then(response => {
        this.temp = response
        this.$refs['miniPaperSection'].getAllList(row.id)
      })
      this.handleGetAllTestPaperSections(row.id)
    },

    // 更新数据
    updateData() {
      this.$refs['dataForm'].validate(valid => {
        if (valid) {
          updateTestPaper(this.temp.id, this.temp).then(() => {
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
    handleUpdateSectionDescription() {
      this.dialogTestPaperSectionDescriptionFormVisible = true
    },

    // 获取试卷大题列表数据
    handleGetAllTestPaperSections(testPaperId) {
      var listQuery = Object.assign({
        testPaperId: testPaperId // 父组件传入参数方法优化
      }, baseListQuery)
      getAllTestPaperSections(listQuery).then(response => {
        this.testPaperSections = response.items
      })
    },

    // 处理更新试卷抽题规则事件
    handelUpdateTestPaperStrategy() {
      this.$refs['miniPaperSection'].getAllList(this.testPaperId)
      this.handleGetAllTestPaperSections(this.testPaperId)
    }

  }
}
</script>

<style lang="scss" scoped>
.test-paper-dialog{
  ::v-deep .el-dialog {
    margin: 0 auto 0px;
  }
  ::v-deep .el-dialog__body{
    height: calc(90vh - 50px);
    overflow: auto;
  }
}

.section-title{
  font-size: 14px;
  font-weight: normal;
  display: inline-block;
  span{
    font-size: 12px;
  }
}

.section-box{
  margin-top: 15px;
}

