<template>
  <div class="app-container">
    <el-dialog :lock-scroll="false" draggable width="400px" :visible.sync="dialogUploadVisible">
      <template #header>
        <div style="color: #fff">
          <el-icon size="16" style="margin-right: 3px; display: inline; vertical-align: middle"> <ele-UploadFilled /> </el-icon>
          <span> 导入Excel </span>
        </div>
      </template>
      <div>
        <el-upload ref="uploadRef" drag :auto-upload="false" :limit="1" :file-list="fileList" action="" :on-change="handleChange" accept=".xlsx,.xls">
          <el-icon class="el-icon--upload">
            <ele-UploadFilled />
          </el-icon>
          <div class="el-upload__text">将文件拖到此处，或<em>点击上传</em></div>
          <template #tip>
            <div class="el-upload__tip">请上传大小不超过 10MB 的文件</div>
            <div class="el-upload__tip">只允许上传.xlsx,.xls类型的文件<el-link type="primary" @click="downloadTemplate()">下载模板</el-link></div>
          </template>
        </el-upload>
      </div>
      <template #footer>
        <span class="dialog-footer">
          <el-button @click="dialogUploadVisible = false">取消</el-button>
          <el-button type="primary" @click="uploadFile()">确定</el-button>
        </span>
      </template>
    </el-dialog>
  </div>
</template>

<script>
import {
  ImportRoleXlsxTemplate,
  ImportRoleFromXlsx
} from '@/api/system-manage/identity/role'
import { downloadByData } from '@/utils/download'

export default {
  name: 'ImportExcel',
  data() {
    return {
      dialogUploadVisible: false,
      fileList: []
    }
  },
  methods: {
    // 获取导入模板
    downloadTemplate() {
      ImportRoleXlsxTemplate().then(response => {
        downloadByData(response, 'role-template.xlsx')
        this.downloadLoading = false
      })
    },
    uploadFile() {
      console.log('this.fileList', this.fileList)
      debugger
      if (this.fileList.length < 1) return
      const formData = new FormData()
      // 上传数据是我自己设置的数据,可自行添加数据到formData(使用键值对方式存储)
      formData.append('FileName', this.fileList[0].name)
      formData.append('importexcelfile', this.fileList[0].raw)// 拿到存在fileList的文件存放到formData中

      ImportRoleFromXlsx(formData).then(() => {
        this.dialogUploadVisible = false
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

