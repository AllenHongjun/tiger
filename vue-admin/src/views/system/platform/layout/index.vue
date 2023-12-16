<template>
  <div class="app-container">
    <layout-table ref="layoutTable" @handleCreate="handleCreate" @handleUpdate="handleUpdate" />
    <layout-model ref="layoutModel" @handleFilter="handleFilter" />
      </div>
    </el-dialog>
  </div>
</template>

<script>
import {
  getLayout,
  createLayout,
  updateLayout
} from '@/api/system-manage/platform/layout'

import LayoutTable from './components/LayoutTable.vue'
import LayoutModel from './components/LayoutModel.vue'

export default {
  name: 'Layouts',
  components: {
    LayoutTable,
    LayoutModel
      },
      dialogFormVisible: false,
      dialogStatus: '',

      // 表单验证规则
      rules: {
        name: [
          {
            required: true,
            message: this.$i18n.t("AbpValidation['The {0} field is required.']", [
              this.$i18n.t("AppPlatform['DisplayName:Name']")
            ]),
            trigger: 'blur'
          },
          {
            max: 64,
            message: this.$i18n.t(
              "AbpValidation['The field {0} must be a string with a maximum length of {1}.']",
              [this.$i18n.t("AppPlatform['DisplayName:Name']"), '64']
            ),
            trigger: 'blur'
          }
        ],
        displayName: [
          {
            required: true,
            message: this.$i18n.t("AbpValidation['The {0} field is required.']", [
              this.$i18n.t("AppPlatform['DisplayName:DisplayName']")
            ]),
            trigger: 'blur'
          },
          {
            max: 256,
            message: this.$i18n.t(
              "AbpValidation['The field {0} must be a string with a maximum length of {1}.']",
              [this.$i18n.t("AppPlatform['DisplayName:Name']"), '256']
            ),
            trigger: 'blur'
          }
        ],
        description: [
          {
            max: 256,
            message: this.$i18n.t(
              "AbpValidation['The field {0} must be a string with a maximum length of {1}.']",
              [this.$i18n.t("AppPlatform['DisplayName:Description']"), '256']
            ),
            trigger: 'blur'
          }
        ],
        path: [
          {
            required: true,
            message: this.$i18n.t("AbpValidation['The {0} field is required.']", [
              this.$i18n.t("AppPlatform['DisplayName:Path']")
            ]),
            trigger: 'blur'
          },
          {
            max: 256,
            message: this.$i18n.t(
              "AbpValidation['The field {0} must be a string with a maximum length of {1}.']",
              [this.$i18n.t("AppPlatform['DisplayName:Path']"), '256']
            ),
            trigger: 'blur'
          }
        ],
        redirect: [
          {
            max: 255,
            message: this.$i18n.t(
              "AbpValidation['The field {0} must be a string with a maximum length of {1}.']",
              [this.$i18n.t("AppPlatform['DisplayName:Redirect']"), '255']
            ),
            trigger: 'blur'
          }
        ],
        freamwork: [
          {
            required: true,
            message: this.$i18n.t("AbpValidation['The {0} field is required.']", [
              this.$i18n.t("AppPlatform['DisplayName:Freamwork']")
            ]),
            trigger: 'blur'
          },
          {
            max: 255,
            message: this.$i18n.t(
              "AbpValidation['The field {0} must be a string with a maximum length of {1}.']",
              [this.$i18n.t("AppPlatform['DisplayName:Freamwork']"), '255']
            ),
            trigger: 'blur'
          }
        ]
      }
    }
  },
  methods: {
    handleFilter(firstPage = true) {
      this.$refs['layoutTable'].handleFilter(firstPage)
    },

    // 点击创建按钮
    handleCreate() {
      this.$refs['layoutModel'].handleCreate()
    },

    // 更新按钮点击
    handleUpdate(row) {
      this.$refs['layoutModel'].handleUpdate(row)
    }
  }
}
</script>

<style scoped>
.line{
  text-align: center;
}
</style>

