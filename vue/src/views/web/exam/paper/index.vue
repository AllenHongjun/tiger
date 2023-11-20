<template>
  <div class="app-container">
    <el-container>
      <el-header>Header</el-header>
      <el-container>
        <el-aside style="width:350px;">
          <el-card class="box-card">
            <div slot="header" class="clearfix">
              <h3>答题卡</h3>
            </div>
            <div class="mini-paper-section">
              <el-row class="mini-paper-section-header">
                <el-col :span="24">
                  <span>第1大题 </span><span>(共14题90.0分)</span>
                </el-col>
              </el-row>
              <el-row class="mini-paper-section-body">
                <el-button type="primary" plain style="margin-left: 10px;">1</el-button>
                <el-button type="primary" plain>2</el-button>
                <el-button type="primary" plain>3</el-button>
                <el-button type="primary" plain>4</el-button>
                <el-button type="primary" plain>5</el-button>
                <el-button type="primary" plain>6</el-button>
                <el-button type="primary" plain>7</el-button>
                <el-button type="primary" plain>8</el-button>
                <el-button type="primary" plain>9</el-button>
                <el-button type="primary" plain>10</el-button>
                <el-button type="primary" plain>11</el-button>
                <el-button type="primary" plain>12</el-button>
                <el-button type="primary" plain>13</el-button>
                <el-button type="primary" plain>14</el-button>
                <el-button type="primary" plain>15</el-button>
                <el-button type="primary" plain>16</el-button>
                <el-button type="primary" plain>17</el-button>
                <el-button type="primary" plain>18</el-button>
                <el-button type="primary" plain>19</el-button>
                <el-button type="primary" plain>20</el-button>
              </el-row>
            </div>
            <div class="mini-paper-section">
              <el-row class="mini-paper-section-header">
                <el-col :span="24">
                  <span>第2大题 </span><span>(共 56 题 88.0 分)</span>
                </el-col>
              </el-row>
              <el-row class="mini-paper-section-body">
                <el-button type="primary" plain style="margin-left: 10px;">1</el-button>
                <el-button type="primary" plain>2</el-button>
                <el-button type="primary" plain>3</el-button>
                <el-button type="primary" plain>4</el-button>
                <el-button type="primary" plain>5</el-button>
                <el-button type="primary" plain>6</el-button>
                <el-button type="primary" plain>7</el-button>
                <el-button type="primary" plain>8</el-button>
                <el-button type="primary" plain>9</el-button>
                <el-button type="primary" plain>10</el-button>
                <el-button type="primary" plain>11</el-button>
                <el-button type="primary" plain>12</el-button>
                <el-button type="primary" plain>13</el-button>
                <el-button type="primary" plain>14</el-button>
                <el-button type="primary" plain>15</el-button>
                <el-button type="primary" plain>16</el-button>
                <el-button type="primary" plain>17</el-button>
                <el-button type="primary" plain>18</el-button>
                <el-button type="primary" plain>19</el-button>
                <el-button type="primary" plain>20</el-button>
              </el-row>
            </div>
          </el-card>
          <el-divider />
          <el-row>
            <el-button plain>&nbsp;&nbsp;</el-button> 未作答
            <el-button type="primary" plain>&nbsp;&nbsp;</el-button> 已作答
            <el-button type="success" plain>&nbsp;&nbsp;</el-button> 标记
          </el-row>

        </el-aside>
        <el-main>
          <el-card class="box-card">
            <div class="section">
              <el-row class="section-header">
                <el-col :span="24">
                  <span>第1大题 </span><span>(共14题90.0分)</span>
                </el-col>
              </el-row>
              <el-divider />
              <el-row class="section-body">
                <div class="question-content">
                  1.出口方式为不通过一达通出口的信用保障订单，走阿里物流会有（ ）的交易手续费
                  <el-tag type="info" lable="">单选题 5.0分</el-tag>
                </div>
                <div class="question-option">
                  <el-radio-group v-model="radio">
                    <el-radio :label="3">
                      A
                      <span>选项内容</span>
                    </el-radio>
                    <el-radio :label="6">
                      B
                      <span>选项内容</span>
                    </el-radio>
                    <el-radio :label="9"> C   <span>选项内容</span></el-radio>
                    <el-radio :label="22"> D   <span>选项内容</span></el-radio>
                  </el-radio-group>
                </div>
              </el-row>
            </div>
          </el-card>

        </el-main>
        <el-aside width="200px">
          <el-card class="box-card">
            <h4>剩余时间</h4>
            <p style="color: red;">46分53秒</p>
            <el-divider />
            <h4>答题进度</h4>
            <p>0/22</p>
          </el-card>

          <el-button type="primary">交  卷</el-button>
        </el-aside>
      </el-container>
    </el-container>
  </div>
</template>

<script>
export default {
  name: 'CN',
  data() {
    return {
      radio: 3,
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

.el-header, .el-footer {
  background-color: #B3C0D1;
  color: #333;
  text-align: left;
}

.el-aside {
  background-color: #D3DCE6;
  color: #333;
  text-align: left;
}

.el-main {
  background-color: #E9EEF3;
  color: #333;
  text-align: left;
}

body > .el-container {
  margin-bottom: 40px;
}

.el-container:nth-child(5) .el-aside,
.el-container:nth-child(6) .el-aside {
}

.el-container:nth-child(7) .el-aside {
}

.mini-paper-section{
  margin-top: 15px;
}

.mini-paper-section-body .el-button:first{
  margin-left: 10px !important;
}
.mini-paper-section-body .el-button{
  min-width: 55px;
  margin-top: 5px;;
}
.line{
  text-align: center;
}

.text {
  font-size: 14px;
}

.item {
  margin-bottom: 18px;
}

.clearfix:before,
.clearfix:after {
  display: table;
  content: "";
}
.clearfix:after {
  clear: both
}

.box-card {
  width: 100%;
}

::v-deep .el-radio{
  display: block;
 margin:10px 0;
}
</style>

