<template>
  <div class="singleImageUpload2 upload-container" style="width: 220px;">
    <el-upload
      :data="dataObj"
      :multiple="false"
      :show-file-list="false"
      :on-success="handleImageSuccess"
      class="image-uploader"
      drag
      action="#"
      :http-request="httpRequest"
    >
      <i class="el-icon-upload" />
      <div class="el-upload__text">
        Drag或<em>点击上传</em>
      </div>
    </el-upload>
    <div v-show="imageUrl.length > Url.photoPrefix.length" class="image-preview">
      <div class="image-preview-wrapper">
        <img :src="imageUrl">
        <div class="image-preview-action">
          <i class="el-icon-delete" @click="rmImage" />
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { getToken } from '@/api/qiniu'
import { Url } from '@/utils/abp'
import { createObject } from '@/api/system-manage/oss/object'

export default {
  name: 'SingleImageUpload',
  props: {
    value: {
      type: String,
      default: ''
    }
  },
  data() {
    return {
      tempUrl: '',
      dataObj: { token: '', key: '' },
      Url
    }
  },
  computed: {
    imageUrl() {
      return process.env.VUE_APP_IMG_URL + this.value
    }
  },
  methods: {
    rmImage() {
      this.emitInput('')
    },
    emitInput(val) {
      this.$emit('input', val)
    },
    handleImageSuccess(file) {
      console.log(this.tempUrl)
      this.emitInput(file.blobName)
      // this.emitInput(this.tempUrl)
    },
    beforeUpload() {
      return
    },
    httpRequest(data) { // 上传成功
      this.httpRequestImg = true

      const formData = new FormData()
      formData.append('Path', 'upload')
      formData.append('FileName', data.file.name)
      formData.append('File', data.file)

      createObject(formData).then(resData => {
        // bug: 文件名如果有空格无法显示
        this.value = resData.path + resData.name
        // 给父组件传值
        this.$emit('input', resData.path + resData.name)

        this.tempUrl = process.env.VUE_APP_IMG_URL + resData.path + resData.name
        this.$notify({
          title: '成功',
          message: '操作成功',
          type: 'success',
          duration: 2000
        })
      })
    }
  }
}
</script>

<style lang="scss" scoped>
.upload-container {
  width: 100%;
  height: 100%;
  position: relative;
  .image-uploader {
    height: 100%;
  }
  .image-preview {
    width: 100%;
    height: 100%;
    position: absolute;
    left: 0px;
    top: 0px;
    border: 1px dashed #d9d9d9;
    .image-preview-wrapper {
      position: relative;
      width: 100%;
      height: 100%;
      img {
        width: 100%;
        height: 100%;
      }
    }
    .image-preview-action {
      position: absolute;
      width: 100%;
      height: 100%;
      left: 0;
      top: 0;
      cursor: default;
      text-align: center;
      color: #fff;
      opacity: 0;
      font-size: 20px;
      background-color: rgba(0, 0, 0, .5);
      transition: opacity .3s;
      cursor: pointer;
      text-align: center;
      line-height: 200px;
      .el-icon-delete {
        font-size: 36px;
      }
    }
    &:hover {
      .image-preview-action {
        opacity: 1;
      }
    }
  }
}
</style>
