<template>
  <div class="model-container">
    <el-dialog :title=" dialogStatus == 'create'? $t('AppPlatform[\'Layout:AddNew\']'): $t('AbpUi[\'Edit\']')" :visible.sync="dialogFormVisible" top="5vh" width="80%" append-to-body>
      <el-form ref="dataForm" :rules="rules" :model="temp" label-position="right" label-width="150px">

        <el-form-item label="章节类型" prop="type">
          <el-select v-model="temp.type" placeholder="请选择">
            <el-option
              v-for="item in typeOptions"
              :key="item.value"
              :label="item.label"
              :value="item.value"
            />
          </el-select>
        </el-form-item>

        <el-form-item label="章节名称" prop="name">
          <el-input v-model="temp.name" />
        </el-form-item>

        <el-form-item label="章节学时" prop="duration">
          <el-input v-model="temp.duration">
            <el-button slot="append">分钟</el-button>
          </el-input>
        </el-form-item>

        <el-form-item label="章节内容" prop="content">
          <!-- <el-input v-model="temp.duration" type="textarea" :autosize="{ minRows: 4, maxRows: 6}" /> -->
          <!-- <tinymce id="tinymce" v-model="temp.content" :height="500" /> -->
        </el-form-item>
        <!-- <tinymce id="tinymce" v-model="temp.content" :height="500" /> -->

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
  getLayout,
  createLayout,
  updateLayout
} from '@/api/system-manage/platform/layout'

export default {
  name: 'SubChapterModel',
  components: {
  },
  data() {
    return {
      typeOptions: [
        {
          value: '1',
          label: '图文'
        }, {
          value: '2',
          label: '视频'
        }, {
          value: '3',
          label: '文档'
        }
      ],
      value: '',
      temp: {
        id: undefined,
        name: '',
        displayName: '',
        description: '',
        duration: undefined, // 学时
        content: `<h1 style="text-align: center;">Welcome to the TinyMCE demo!</h1><p style="text-align: center; font-size: 15px;"><img title="TinyMCE Logo" src="//www.tinymce.com/images/glyph-tinymce@2x.png" alt="TinyMCE Logo" width="110" height="97" /><ul>
        <li>Our <a href="//www.tinymce.com/docs/">documentation</a> is a great resource for learning how to configure TinyMCE.</li><li>Have a specific question? Visit the <a href="https://community.tinymce.com/forum/">Community Forum</a>.</li><li>We also offer enterprise grade support as part of <a href="https://tinymce.com/pricing">TinyMCE premium subscriptions</a>.</li>
      </ul>`, // 章节内容
        type: 1, // 1 图文 2 视频 3 文档附件
        path: '',
        freamwork: ''
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
        ]

      }

    }
  },
  methods: {
    // 重置表单
    resetTemp() {
      this.temp = {
        id: undefined,
        name: '',
        displayName: '',
        description: '',
        path: '',
        redirect: '',
        dataId: undefined,
        freamwork: ''
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
          createLayout(this.temp).then(() => {
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
      getLayout(row.id).then(response => {
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
          updateLayout(this.temp.id, this.temp).then(() => {
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
::v-deep .el-dialog {
  margin: 0 auto 0px;
}
::v-deep .el-dialog__body{
  height: calc(80vh - 50px);
  overflow: auto;
}
</style>

