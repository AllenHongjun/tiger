<template>
  <div class="app-container">
    <el-row>
      <el-col :span="24">
        <div class="grid-content">
          <el-radio-group v-model="size">
            <el-radio label="">默认</el-radio>
            <el-radio label="medium">中等</el-radio>
            <el-radio label="small">小型</el-radio>
            <el-radio label="mini">超小</el-radio>
          </el-radio-group>
          <el-divider />

          <el-descriptions class="margin-top" title=" cache基本信息" :column="3" :size="size" border>
            <!-- <template slot="extra">
              <el-button type="primary" size="small">操作</el-button>
            </template> -->
            <el-descriptions-item>
              <template slot="label">
                <i class="el-icon-user" />
                Redis版本
              </template>
              3.0.0
            </el-descriptions-item>
            <el-descriptions-item>
              <template slot="label">
                <i class="el-icon-mobile-phone" />
                运行模式
              </template>
              单机
            </el-descriptions-item>
            <el-descriptions-item>
              <template slot="label">
                <i class="el-icon-location-outline" />
                端口
              </template>
              6379
            </el-descriptions-item>
            <el-descriptions-item>
              <template slot="label">
                <i class="el-icon-tickets" />
                客户端数
              </template>
              <el-tag size="small">1</el-tag>
            </el-descriptions-item>
            <el-descriptions-item>
              <template slot="label">
                <i class="el-icon-office-building" />
                运行时间(天)
              </template>
              139
            </el-descriptions-item>
            <el-descriptions-item>
              <template slot="label">
                <i class="el-icon-mobile-phone" />
                运行模式
              </template>
              单机
            </el-descriptions-item>
            <el-descriptions-item>
              <template slot="label">
                <i class="el-icon-mobile-phone" />
                使用内存
              </template>
              882.29K
            </el-descriptions-item>
            <el-descriptions-item>
              <template slot="label">
                <i class="el-icon-mobile-phone" />
                使用CPU
              </template>
              36.09
            </el-descriptions-item>
            <el-descriptions-item>
              <template slot="label">
                <i class="el-icon-mobile-phone" />
                内存配置
              </template>

            </el-descriptions-item>
            <el-descriptions-item>
              <template slot="label">
                <i class="el-icon-mobile-phone" />
                AOF是否开启
              </template>
              否
            </el-descriptions-item>
            <el-descriptions-item>
              <template slot="label">
                <i class="el-icon-mobile-phone" />
                RDB是否成功
              </template>
              否
            </el-descriptions-item>
            <el-descriptions-item>
              <template slot="label">
                <i class="el-icon-mobile-phone" />
                AOF是否开启
              </template>
              ok
            </el-descriptions-item>
            <el-descriptions-item>
              <template slot="label">
                <i class="el-icon-mobile-phone" />
                Key数量
              </template>
              66
            </el-descriptions-item>
            <el-descriptions-item>
              <template slot="label">
                <i class="el-icon-mobile-phone" />
                网络入口/出口
              </template>
              0.17kps/0.02kps
            </el-descriptions-item>
          </el-descriptions>
        </div>
      </el-col>
    </el-row>
    <el-row :gutter="10">
      <el-col :span="12"><div class="grid-content bg-purple">2</div></el-col>
      <el-col :span="12"><div class="grid-content bg-purple-light">3</div></el-col>
    </el-row>
  </div>
</template>

<script>
export default {
  data() {
    return {
      blank: {

      },
      size: ''
    }
  },
  methods: {
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

<style lang="scss" scoped>
.line{
  text-align: center;
}

.el-row {
  margin-bottom: 20px;
  &:last-child {
    margin-bottom: 0;
  }
}
.el-col {
  border-radius: 4px;
}
.bg-purple-dark {
  background: #99a9bf;
}
.bg-purple {
  background: #d3dce6;
}
.bg-purple-light {
  background: #e5e9f2;
}
.grid-content {
  border-radius: 4px;
  min-height: 36px;
}
.row-bg {
  padding: 10px 0;
  background-color: #f9fafc;
}
</style>

