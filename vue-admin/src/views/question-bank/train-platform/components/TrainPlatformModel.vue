<template>
  <div class="model-container">
    <el-dialog :title=" dialogStatus == 'create'? $t('AppQuestionBank[\'Permission:Create\']'): $t('AbpUi[\'Edit\']')" :visible.sync="dialogFormVisible">
      <el-form ref="dataForm" :rules="rules" :model="temp" label-position="right" label-width="150px">
        <el-form-item :label="$t('AppQuestionBank[\'DisplayName:Name\']')" prop="name">
          <el-input v-model="temp.name" />
        </el-form-item>
        <el-form-item :label="$t('AppQuestionBank[\'DisplayName:Description\']')" prop="description">
          <el-input v-model="temp.description" />
        </el-form-item>
        <el-form-item :label="$t('AppQuestionBank[\'DisplayName:Icon\']')" prop="icon" style="width: 200px;">
          <single-image-upload v-model="temp.icon" @input="input" />
        </el-form-item>
        <el-form-item :label="$t('AppQuestionBank[\'DisplayName:Url\']')" prop="url">
          <el-input v-model="temp.url" />
        </el-form-item>
        <el-form-item :label="$t('AppQuestionBank[\'DisplayName:CheckCode\']')" prop="checkCode">
          <el-input v-model="temp.checkCode" />
        </el-form-item>
        <el-form-item :label="$t('AppQuestionBank[\'DisplayName:TokenType\']')" prop="tokenType">
          <el-select v-model="temp.tokenType" placeholder="请选择">
            <el-option label="旧版Cookie" :value="0" />
            <el-option label="旧版Url" :value="1" />
            <el-option label="新版Url" :value="2" />
          </el-select>
        </el-form-item>
        <el-form-item :label="$t('AppQuestionBank[\'DisplayName:Sorting\']')" prop="sorting">
          <el-input v-model="temp.sorting" type="number" />
        </el-form-item>
        <el-form-item :label="$t('AppQuestionBank[\'DisplayName:Enable\']')" prop="enable">
          <el-switch
            v-model="temp.enable"
            active-color="#13ce66"
            inactive-color="#ff4949"
          />
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
  getTrainPlatform,
  createTrainPlatform,
  updateTrainPlatform
} from '@/api/question-bank/train-platform'
import SingleImageUpload from '@/components/Upload/SingleImage.vue'

export default {
  name: 'TrainPlatformModel',
  components: {
    SingleImageUpload
  },
  data() {
    return {
      temp: {
        id: undefined,
        name: undefined,
        description: undefined,
        icon: undefined,
        url: undefined,
        checkCode: undefined,
        tokenType: 2,
        sorting: 1,
        enable: true
      },
      dialogFormVisible: false,
      dialogStatus: '',

      // 表单验证规则
      rules: {
        name: [
          {
            required: true,
            message: this.$i18n.t("AbpValidation['The {0} field is required.']", [
              this.$i18n.t("AppQuestionBank['DisplayName:Name']")
            ]),
            trigger: 'blur'
          },
          {
            max: 64,
            message: this.$i18n.t(
              "AbpValidation['The field {0} must be a string with a maximum length of {1}.']",
              [this.$i18n.t("AppQuestionBank['DisplayName:Name']"), '512']
            ),
            trigger: 'blur'
          }
        ],
        description: [
          {
            max: 64,
            message: this.$i18n.t(
              "AbpValidation['The field {0} must be a string with a maximum length of {1}.']",
              [this.$i18n.t("AppQuestionBank['DisplayName:Description']"), '512']
            ),
            trigger: 'blur'
          }
        ],
        icon: [
          {
            required: true,
            message: this.$i18n.t("AbpValidation['The {0} field is required.']", [
              this.$i18n.t("AppQuestionBank['DisplayName:Icon']")
            ]),
            trigger: 'blur'
          },
          {
            max: 64,
            message: this.$i18n.t(
              "AbpValidation['The field {0} must be a string with a maximum length of {1}.']",
              [this.$i18n.t("AppQuestionBank['DisplayName:Icon']"), '1024']
            ),
            trigger: 'blur'
          }
        ],
        url: [
          {
            required: true,
            message: this.$i18n.t("AbpValidation['The {0} field is required.']", [
              this.$i18n.t("AppQuestionBank['DisplayName:Url']")
            ]),
            trigger: 'blur'
          },
          {
            max: 64,
            message: this.$i18n.t(
              "AbpValidation['The field {0} must be a string with a maximum length of {1}.']",
              [this.$i18n.t("AppQuestionBank['DisplayName:Url']"), '1024']
            ),
            trigger: 'blur'
          },
          {
            message: this.$i18n.t(
              "AbpValidation['The {0} field is not a valid fully-qualified http, https, or ftp URL.']",
              [this.$i18n.t("AppQuestionBank['DisplayName:Url']")]),
            trigger: 'blur', pattern: /^(http|https):\/\/[a-zA-Z0-9\-\.]+\.[a-zA-Z]{2,}(\/\S*)?$/
          }
        ],
        checkCode: [
          {
            required: true,
            message: this.$i18n.t("AbpValidation['The {0} field is required.']", [
              this.$i18n.t("AppQuestionBank['DisplayName:CheckCode']")
            ]),
            trigger: 'blur'
          },
          {
            max: 64,
            message: this.$i18n.t(
              "AbpValidation['The field {0} must be a string with a maximum length of {1}.']",
              [this.$i18n.t("AppQuestionBank['DisplayName:CheckCode']"), '64']
            ),
            trigger: 'blur'
          }
        ]

      }
    }
  },
  methods: {
    input(url) {
      this.temp.icon = url
    },
    // 重置表单
    resetTemp() {
      this.temp = {
        id: undefined,
        name: undefined,
        description: undefined,
        icon: undefined,
        url: undefined,
        checkCode: undefined,
        tokenType: 2,
        sorting: 1,
        enable: true
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
          createTrainPlatform(this.temp).then(() => {
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
      getTrainPlatform(row.id).then(response => {
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
          updateTrainPlatform(this.temp.id, this.temp).then(() => {
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

