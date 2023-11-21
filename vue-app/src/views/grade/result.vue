<template>
  <div class="app-container">

    <el-main>
      <div class="grade-result" style="">
        <el-row>
          <h4>honjunyYU</h4>
          <h1>98.0</h1>
          <!-- <button type="warning" disabled>第2名</button> -->
          <p>恭喜您，成功通过本次考试！</p>
        </el-row>
      </div>

      <div class="grade-content">
        <div class="grade-analysis">
          <span>成绩: 12.0</span>
          <el-divider direction="vertical" />
          <span>用时: 0分17秒</span>
          <el-divider direction="vertical" />
          <span>交卷时间: 11-20 09:16</span>
          <el-divider direction="vertical" />
          <span>总分/及格分: 226.0/3.0</span>
        </div>

        <!-- <el-row :gutter="20">
          <el-col :span="6"><el-link href="https://element.eleme.io" target="_blank">查看答案解析</el-link></el-col>
          <el-col :span="6"><el-link type="primary">查看历史成绩</el-link></el-col>
          <el-col :span="6"><el-link type="success">问题反馈</el-link></el-col>
        </el-row> -->

        <el-button type="primary" class="btn-back">返回</el-button>
      </div>
    </el-main>

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

<style lang="scss" scoped>
.app-container{
  background-color: #eef1f6;
}
.el-main {
  color: #333;
  text-align: center;
  width: 1100px;
  min-height: 900px;
  margin:0 auto;

  .grade-result{
    background-color: #409EFF;
    color: #fff;
    height: 260px;

    h4{
      margin-top: 70px;
    }
  }

  .grade-content{
    background-color: #fff;

    .grade-analysis {
      height: 190px;
      padding-top: 20px;
      line-height: 190px;
      font-size: 14px;
    }
  }

  .el-row{
    text-align: center;
  }

  .btn-back{
    width: 220px;
    font-size: 18px;
    margin-bottom: 60px;
  }
}

body > .el-container {
  margin-bottom: 40px;
}

</style>

