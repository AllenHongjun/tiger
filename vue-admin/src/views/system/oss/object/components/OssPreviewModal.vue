<template>
  <div class="app-container">

    <el-drawer title="图片预览" :visible.sync="drawer" :direction="direction">
      <el-card class="box-card">
        <div class="text item">
          <div class="img-container">

            <el-image :src="src" style="height: 500px;margin:0 auto;display:block;" fit="contain">
              <div v-if="form.isFolder" slot="error" class="image-slot">
                <i class="el-icon-error" />
                不支持预览
              </div>
            </el-image>
          </div>

          <el-form ref="form" :model="form" label-width="110px" label-position="left">
            <el-form-item :label="$t('AbpOssManagement[\'DisplayName:Name\']')">
              <span>{{ form.name }}</span>
            </el-form-item>

            <el-form-item :label="$t('AbpOssManagement[\'DisplayName:Size\']')">
              <span>{{ form.size }}</span>
            </el-form-item>

            <el-form-item :label="$t('AbpOssManagement[\'DisplayName:CreationDate\']')">
              <span>{{ form.creationDate | moment }}</span>
            </el-form-item>

            <el-form-item :label="$t('AbpOssManagement[\'DisplayName:LastModifiedDate\']')">
              <span>{{ form.lastModifiedDate | moment }}</span>
            </el-form-item>

            <el-form-item label="path">
              <span>{{ form.path }}</span>
            </el-form-item>

            <el-form-item label="MD5">
              <span>{{ form.mD5 }}</span>
            </el-form-item>

            <el-form-item label="文件夹">
              <span>{{ form.isFolder ? '是': '否' }}</span>
            </el-form-item>

          </el-form>
        </div>
      </el-card>
    </el-drawer>
  </div>
</template>

<script>
export default {
  name: 'OssPreview',
  props: {
    // 定义props属性
    ouId: {
      type: String,
      default: null
    }
  },
  data() {
    return {
      drawer: false,
      direction: 'rtl',
      src: 'https://cube.elemecdn.com/6/94/4d3ea53c084bad6931a56d5158a48jpeg.jpeg',
      form: {
        isFolder: false,
        path: null,
        name: undefined,
        size: undefined,
        mD5: undefined,
        creationDate: undefined,
        lastModifiedDate: undefined,
        metadata: {
          Id: undefined,
          DisplayName: undefined
        }
      }
    }
  },
  methods: {
    handleDrawer(row, index) {
      console.log(row, index)
      this.form = row
      this.src = 'https://tiger-blob.oss-cn-hangzhou.aliyuncs.com/host/' + row.name
      this.drawer = true
      this.isFolder = row.isFolder
    },
    handleClose(done) {
      this.$confirm('确认关闭？')
        .then(_ => {
          done()
        })
        .catch(_ => {})
    },
    onSubmit() {
      this.$message('submit!')
    },
    onCancel() {
      this.$message({
        message: 'cancel!',
        type: 'warning'
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

