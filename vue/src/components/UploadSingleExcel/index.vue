<template>
  <div class="app-container">
    <el-dialog title="导入Excel" :lock-scroll="false" draggable width="400px" :visible.sync="dialogUploadVisible">
      <div>
        <el-upload ref="uploadRef" drag :auto-upload="false" :limit="1" :file-list="fileList" action="" :on-change="handleChange" accept=".xlsx,.xls">
          <i class="el-icon-upload" />
          <div class="el-upload__text">将文件拖到此处，或<em>点击上传</em></div>
          <template #tip>
            <div class="el-upload__tip">请上传大小不超过 10MB 的文件</div>

            <div class="el-upload__tip">
              <el-tooltip content="导入需要大量内存资源。 这就是为什么不建议一次导入超过 500 - 1,000 条记录。 如果您的记录较多，最好将它们拆分为多个 Excel 文件并分别导入。" placement="top">
                <i class="el-icon-warning" />
              </el-tooltip>
              只允许上传.xlsx,.xls类型的文件
              <el-link type="primary" @click="downloadTemplate()">下载模板</el-link>
            </div>
          </template>
        </el-upload>
      </div>
      <template #footer>
        <span class="dialog-footer">
          <el-button @click="dialogUploadVisible = false">取消</el-button>
          <el-button type="primary" :loading="uploadLoading" @click="uploadFile()">确定</el-button>
        </span>
      </template>
    </el-dialog>
  </div>
</template>

<script>

import { downloadByData } from '@/utils/download'

export default {
  name: 'UploadSingleExcel',
  props: {
    importFromXlsx: {
      type: Function,
      default: function() { }
    },
    importXlsxTemplate: {
      type: Function,
      default: function() { }
    }
  },
  data() {
    return {
      dialogUploadVisible: false,
      uploadLoading: false,
      fileList: []
    }
  },
  methods: {
    // 获取导入模板
    downloadTemplate() {
      this.importXlsxTemplate().then(response => {
        downloadByData(response, 'role-template.xlsx')
        this.downloadLoading = false
      })
    },
    uploadFile() {
      if (this.fileList.length < 1) return
      const formData = new FormData()
      // 上传数据是我自己设置的数据,可自行添加数据到formData(使用键值对方式存储)
      formData.append('FileName', this.fileList[0].name)
      formData.append('importexcelfile', this.fileList[0].raw)// 拿到存在fileList的文件存放到formData中
      this.uploadLoading = true
      this.importFromXlsx(formData).then(() => {
        this.dialogUploadVisible = false
        this.uploadLoading = false

        // 调用父组件hanldeFilter方法  https://juejin.cn/post/6934317147749367815
        this.$emit('call-filter')
        this.$notify({
          title: this.$i18n.t("TigerUi['Success']"),
          message: this.$i18n.t("TigerUi['SuccessMessage']"),
          type: 'success',
          duration: 2000
        })
      })
    },
    handleChange(file, fileList) {
      this.fileList = fileList
    }
  }
}
</script>

<style scoped>
.line{
  text-align: center;
}
</style>

