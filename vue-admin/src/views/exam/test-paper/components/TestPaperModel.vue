<template>
  <div class="model-container">
    <el-dialog :title=" dialogStatus == 'create'? $t('AppExam[\'Permission:Create\']'): $t('AbpUi[\'Edit\']')" :visible.sync="dialogFormVisible" top="1vh" width="99%">
      <el-row>
        <el-steps :active="active" simple>
          <el-step title="基本信息" icon="el-icon-edit" @click.native="active = 0">1</el-step>
          <el-step title="设计试卷" icon="el-icon-upload" @click.native="active = 1">2</el-step>
        </el-steps>
      </el-row>

      <el-card v-if="active == 0">
        <el-form label-position="right" label-width="280px" :model="temp">
          <el-form-item label="名称">
            <el-input v-model="temp.name" />
          </el-form-item>
          <el-form-item label="活动区域">
            <el-input v-model="temp.displayName" />
          </el-form-item>
          <el-form-item label="活动形式">
            <el-input v-model="temp.description" />
          </el-form-item>
        </el-form>
      </el-card>

      <el-row v-if="active == 1" :gutter="20">
        <el-col :span="6">
          <el-card class="box-card">
            <div slot="header" class="clearfix" style="background-color: #1890ff;">
              <el-dropdown split-button type="primary" style="width: 100%;" @click="handleClick">
                添加试卷大题
                <el-dropdown-menu slot="dropdown">
                  <el-dropdown-item><i class="el-icon-phone" />添加固定试卷大题</el-dropdown-item>
                  <el-dropdown-item><i class="el-icon-menu" />添加随机试卷大题</el-dropdown-item>
                </el-dropdown-menu>
              </el-dropdown>
              <!-- <el-menu
                :default-active="activeIndex2"
                class="el-menu-demo"
                mode="horizontal"
                background-color="#1890ff"
                text-color="#fff"
                active-text-color="#fff"
                style="float: right;height:100%;"
                @select="handleSelect"
              >
                <el-submenu index="2">
                  <template slot="title">添加试卷大题</template>
                  <el-menu-item index="2-1">
                    <i class="el-icon-phone" />添加固定试卷大题
                  </el-menu-item>
                  <el-menu-item index="2-2">
                    <i class="el-icon-menu" />添加随机试卷大题
                  </el-menu-item>
                </el-submenu>
              </el-menu> -->
            </div>
            <span>共 <b>25</b>   题 <b>95</b> 分</span>
            <el-divider />
            <div class="mini-paper-section">
              <el-row class="mini-paper-section-header">
                <el-col :span="12">
                  <span>第1大题 </span><span>(共14题90.0分)</span>
                </el-col>
                <el-col :span="8" :offset="3">
                  <el-button-group style="">
                    <el-button type="info" icon="el-icon-bottom" title="下移" />
                    <el-button type="info" icon="el-icon-edit" title="批量修改分数" />
                    <el-button type="info" icon="el-icon-delete" title="删除大题" />
                  </el-button-group>
                </el-col>
              </el-row>
              <el-row class="mini-paper-section-body">
                <el-button type="info" plain style="margin-left: 10px;">1</el-button>
                <el-button type="info" plain>2</el-button>
                <el-button type="info" plain>3</el-button>
                <el-button type="info" plain>4</el-button>
                <el-button type="info" plain>5</el-button>
                <el-button type="info" plain>6</el-button>
                <el-button type="info" plain>7</el-button>
                <el-button type="info" plain>8</el-button>
                <el-button type="info" plain>9</el-button>
                <el-button type="info" plain>10</el-button>
                <el-button type="info" plain>11</el-button>
                <el-button type="info" plain>12</el-button>
                <el-button type="info" plain>13</el-button>
                <el-button type="info" plain>14</el-button>
                <el-button type="info" plain>15</el-button>
                <el-button type="info" plain>16</el-button>
                <el-button type="info" plain>17</el-button>
                <el-button type="info" plain>18</el-button>
                <el-button type="info" plain>19</el-button>
                <el-button type="info" plain>20</el-button>
              </el-row>
            </div>
            <div class="mini-paper-section">
              <el-row class="mini-paper-section-header">
                <el-col :span="12">
                  <span>第2大题 </span><span>(共 56 题 88.0 分)</span>
                </el-col>
                <el-col :span="8" :offset="3">
                  <el-button-group style="">
                    <el-button type="info" icon="el-icon-bottom" title="下移" />
                    <el-button type="info" icon="el-icon-edit" title="批量修改分数" />
                    <el-button type="info" icon="el-icon-delete" title="删除大题" />
                  </el-button-group>
                </el-col>
              </el-row>
              <el-row class="mini-paper-section-body">
                <el-button type="info" plain style="margin-left: 10px;">1</el-button>
                <el-button type="info" plain>2</el-button>
                <el-button type="info" plain>3</el-button>
                <el-button type="info" plain>4</el-button>
                <el-button type="info" plain>5</el-button>
                <el-button type="info" plain>6</el-button>
                <el-button type="info" plain>7</el-button>
                <el-button type="info" plain>8</el-button>
                <el-button type="info" plain>9</el-button>
                <el-button type="info" plain>10</el-button>
                <el-button type="info" plain>11</el-button>
                <el-button type="info" plain>12</el-button>
                <el-button type="info" plain>13</el-button>
                <el-button type="info" plain>14</el-button>
                <el-button type="info" plain>15</el-button>
                <el-button type="info" plain>16</el-button>
                <el-button type="info" plain>17</el-button>
                <el-button type="info" plain>18</el-button>
                <el-button type="info" plain>19</el-button>
                <el-button type="info" plain>20</el-button>
              </el-row>
            </div>
          </el-card>
        </el-col>
        <el-col :span="18">
          <random-paper-section />
          <fixed-paper-section />
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
  </div>
</template>

<script>
import {
  getTestPaper,
  createTestPaper,
  updateTestPaper
} from '@/api/exam/test-paper'
import RandomPaperSection from './RandomPaperSection.vue'
import FixedPaperSection from './FixedPaperSection.vue'

export default {
  name: 'TestPaperModel',
  components: {
    RandomPaperSection,
    FixedPaperSection
  },
  data() {
    return {

      activeIndex2: 1,
      active: 1,
      temp: {
        id: undefined,
        name: '',
        displayName: '',
        description: '',
        path: '',
        redirect: '',
        dataId: undefined,
        freamwork: ''
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
      this.$nextTick(() => {
        this.$refs['dataForm'].clearValidate()
      })
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

    // 更新按钮点击
    handleUpdate(row) {
      this.dialogStatus = 'update'
      this.dialogFormVisible = true
      getTestPaper(row.id).then(response => {
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
    }
  }
}
</script>

<style lang="scss" scoped>
::v-deep .el-dialog__body{
  height: 82vh;
  overflow: auto;
}

.mini-paper-section{
  margin-top: 15px;
}

.mini-paper-section-body .el-button:first{
  margin-left: 10px !important;
}
.mini-paper-section-body .el-button{
  min-width: 55px;
  margin-top: 5px;;
}
.line{
  text-align: center;
}

.text {
  font-size: 14px;
}

.item {
  margin-bottom: 18px;
}

.clearfix:before,
.clearfix:after {
  display: table;
  content: "";
}
.clearfix:after {
  clear: both
}

.box-card {
  width: 100%;
}
</style>

