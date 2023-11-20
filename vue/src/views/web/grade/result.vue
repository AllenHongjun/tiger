<template>
  <div class="app-container">
    <el-header style="height: 260px;">
      <el-row>
        <h4>honjunyYU</h4>
        <h1>98.0</h1>
        <!-- <button type="warning" disabled>第2名</button> -->
        <p>恭喜您，成功通过本次考试！</p>
      </el-row>

    </el-header>
    <el-main>
      <div>
        <span>成绩: 12.0</span>
        <el-divider direction="vertical" />
        <span>用时: 0分17秒</span>
        <el-divider direction="vertical" />
        <span>交卷时间: 11-20 09:16</span>
        <el-divider direction="vertical" />
        <span>总分/及格分: 226.0/3.0</span>

      </div>
      <el-row :gutter="20">
        <el-col :span="6"><el-link href="https://element.eleme.io" target="_blank">查看答案解析</el-link></el-col>
        <el-col :span="6"><el-link type="primary">查看历史成绩</el-link></el-col>
        <el-col :span="6"><el-link type="success">问题反馈</el-link></el-col>
      </el-row>

      <el-button type="primary">返回</el-button>
    </el-main>
    <el-footer>基础支持</el-footer>

  </div>
</template>

<script>
export default {
  name: 'CN',
  data() {
    return {
      blank: {

      }
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

<style scoped>
.el-header{
  height: 280px;
}
.el-header, .el-footer {
  background-color: #B3C0D1;
  color: #333;
  text-align: center;
}

.el-aside {
  background-color: #D3DCE6;
  color: #333;
  text-align: center;
}

.el-main {
  background-color: #E9EEF3;
  color: #333;
  text-align: center;
}

body > .el-container {
  margin-bottom: 40px;
}

.el-container:nth-child(5) .el-aside,
.el-container:nth-child(6) .el-aside {
}

.el-container:nth-child(7) .el-aside {
}
</style>

