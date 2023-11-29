<!-- demo 上传单个图片-->
<template>
  <div class="file-upload-container">
    <el-upload
      class="avatar-uploader"
      action="#"
      list-type="picture-card"
      :on-preview="handlePictureCardPreview"
      :on-remove="handleRemove"
      :http-request="httpRequest"
      :class="{'demo-httpRequestImg':httpRequestImg}"
    >
      <img v-if="imageUrl" :src="imageUrl" class="avatar" style="width: 100px;height:100px;">
      <i v-else class="el-icon-plus" />
    </el-upload>
    <el-dialog :visible.sync="dialogVisibleImg" append-to-body class="ImgClass">
      <img width="100%" :src="dialogImageUrl" alt="">
    </el-dialog>
  </div>
</template>

<script>
import { createObject } from '@/api/system-manage/oss/object'
export default {
  props: {
    value: {
      type: String,
      default: ''
    }
  },
  data() {
    return {
      dialogImageUrl: '', // 预览url
      dialogVisibleImg: false,
      httpRequestImg: false // 展示单个图片
    }
  },
  computed: {
    imageUrl() {
      return process.env.VUE_APP_IMG_URL + this.value
    }
  },
  methods: {
    httpRequest(data) { // 上传成功
      this.httpRequestImg = true

      // this.imageUrl = URL.createObjectURL(data.file.raw)// 赋值图片的url，用于图片回显功能

      const formData = new FormData()
      formData.append('Path', 'upload')
      formData.append('FileName', data.file.name)
      formData.append('File', data.file)

      createObject(formData).then(resData => {
        // bug: 文件名如果有空格无法显示
        this.value = resData.path + resData.name
        // 给父组件传值
        this.$emit('input', resData.path + resData.name)
        this.$notify({
          title: '成功',
          message: '操作成功',
          type: 'success',
          duration: 2000
        })
      })
    },
    handlePictureCardPreview(file) { // 预览
      this.dialogImageUrl = file.url
      this.dialogVisibleImg = true
    },
    handleRemove(file, fileList) { // 删除
      this.httpRequestImg = false
      console.log(file, fileList)
    }
  }
}
</script>

<style lang="scss" scoped>
  .demo-httpRequestImg{
    ::v-deep .el-upload--picture-card{
        display: none;
    }
  }
</style>
