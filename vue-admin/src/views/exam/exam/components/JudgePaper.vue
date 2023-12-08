<template>
  <div class="paper-container">
    <el-dialog class="large-dialog" title="阅卷" :visible.sync="dialogFormVisible" top="1vh" width="99%" append-to-body>
      <el-container>
        <!-- <el-header>Header</el-header> -->
        <el-container>
          <el-aside style="width: 450px;">
            <el-card class="box-card">
              <div slot="header" class="clearfix">
                <h3>答题卡</h3>
              </div>
              <div class="mini-paper-section">
                <el-row class="mini-paper-section-header">
                  <el-col :span="24">
                    <span>第1大题 </span><span class="question-stat-box">(共 <b>14</b> 题 <b>90.0</b>  分)</span>
                  </el-col>
                </el-row>
                <el-row class="mini-paper-section-body">
                  <el-button type="info" plain>1</el-button>
                  <el-button type="info" plain>2</el-button>
                  <el-button type="primary" plain>3</el-button>
                  <el-button type="primary" plain>4</el-button>
                  <el-button type="success" plain>5</el-button>
                  <el-button type="info" plain>6</el-button>
                  <el-button type="info" plain>7</el-button>
                  <el-button type="info" plain>8</el-button>
                  <el-button type="info" plain>9</el-button>
                  <el-button type="info" plain>10</el-button>
                  <el-button type="info" plain>11</el-button>
                  <el-button type="info" plain>12</el-button>
                  <el-button type="info" plain>13</el-button>
                  <el-button type="info" plain>14</el-button>
                  <el-button type="info" plain>15</el-button>
                  <el-button type="info" plain>16</el-button>
                  <el-button type="info" plain>17</el-button>
                  <el-button type="info" plain>18</el-button>
                  <el-button type="info" plain>19</el-button>
                  <el-button type="info" plain>20</el-button>
                </el-row>
              </div>

              <el-divider />
              <div class="answer-tips">
                <el-button plain /> <span>未作答</span>
                <el-button type="primary" plain>&nbsp;&nbsp;</el-button> <span>已作答</span>
                <el-button type="success">&nbsp;&nbsp;</el-button> 正确
                <el-button type="success" plain>&nbsp;&nbsp;</el-button> 部分正确
                <el-button type="danger">&nbsp;&nbsp;</el-button> 错误
              </div>
            </el-card>

          </el-aside>
          <el-main>
            <el-card class="box-card">
              <el-row style="margin-bottom: 20px;">
                <el-table
                  :key="tableKey"
                  :data="tableData"
                  border
                  fit
                  highlight-current-row
                  style="width: 100%;"
                >
                  <el-table-column label="试卷大题统计" align="center">
                    <el-table-column label="大题名称" align="center">
                      <template slot-scope="{row}">
                        <span>第一大题</span>
                      </template>
                    </el-table-column>
                    <el-table-column label="总分" width="110px" align="center">
                      <template slot-scope="{row}">
                        <span>90</span>
                      </template>
                    </el-table-column>
                    <el-table-column label="得分" width="110px" align="center">
                      <template slot-scope="{row}">
                        <el-tag type="danger">60</el-tag>
                      </template>
                    </el-table-column>
                    <el-table-column label="得分率" width="110px" align="center">
                      <template slot-scope="{row}">
                        <el-tag type="danger">60%</el-tag>
                      </template>
                    </el-table-column>
                  </el-table-column>
                </el-table>
              </el-row>
              <div class="section">
                <el-row class="section-header">
                  <el-col :span="24">
                    <span>第1大题 </span><span>(共14题90.0分)</span>
                  </el-col>
                </el-row>
                <el-divider />
                <el-row class="section-body">
                  <div class="question">
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
                  </div>
                  <div class="answer" style="background-color: rgb(253, 226, 226);padding:20px;font-size:14px;">
                    <p>得分：<span style="color: red;">0</span> 分</p>
                    <p>标准答案：<span style="color: green;">B</span> </p>
                    <p>我的答案：<b style="color: red;">Aㄨ</b>  </p>
                    <el-divider content-position="center">试题解析</el-divider>
                    <p>使用一达通，提交产品预审环节，由客户提出。</p>
                  </div>

                </el-row>
              </div>
            </el-card>

          </el-main>
          <el-aside style="width: 200px;">
            <el-card class="right-box-card">
              <h4>剩余时间</h4>
              <p style="color: red;">46分53秒</p>
              <el-divider />
              <h4>答题进度</h4>
              <p>0/22</p>
            </el-card>

            <el-button type="primary" class="btn-submit-paper" @click="submitPaper()">交  卷</el-button>
          </el-aside>
        </el-container>
      </el-container>

      <div slot="footer" class="dialog-footer">
        <el-button type="primary" @click="dialogFormVisible = false">
          {{ $t("AbpUi['Close']") }}
        </el-button>
      </div>
    </el-dialog>

  </div>
</template>

<script>
export default {
  name: 'JudgePaper',
  data() {
    return {
      dialogFormVisible: true,
      dialogStatus: '',
      tableData: [
        {
          date: '2016-05-02',
          name: '王小虎',
          address: '上海市普陀区金沙江路 1518 弄'
        }, {
          date: '2016-05-04',
          name: '王小虎',
          address: '上海市普陀区金沙江路 1517 弄'
        }, {
          date: '2016-05-01',
          name: '王小虎',
          address: '上海市普陀区金沙江路 1519 弄'
        }, {
          date: '2016-05-03',
          name: '王小虎',
          address: '上海市普陀区金沙江路 1516 弄'
        }
      ],
      radio: 3,
      tableKey: 0,
      list: null,
      total: 0,
      listLoading: true,
      listQuery: {
        page: 1,
        limit: 10,
        importance: undefined,
        title: undefined,
        type: undefined,
        sort: '+id'
      },
      sortOptions: [{ label: 'ID Ascending', key: '+id' }, { label: 'ID Descending', key: '-id' }]
    }
  },
  methods: {
    // 更新按钮点击
    getAnswerSheet(row) {
      this.dialogFormVisible = true
    },
    submitPaper() {
      this.$router.push('/exam/result')
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

<style lang="scss" scoped>
.el-header, .el-footer {
  color: #333;
  text-align: left;
  padding: 0;
  margin: 0;
}

.app-container {
  background: #eef1f6;
}

body  .el-container {
  background: #eef1f6;
  margin-bottom: 40px;
  .el-aside {
    color: #333;
    text-align: left;

    .box-card{
      min-height:900px;
      h3 {
        text-align: center;
      }

    }
    .mini-paper-section{
      margin-top: 15px;
      .mini-paper-section-header {
        text-align: left;
        span{
          margin-left: 10px;
        }
        .question-stat-box{
          font-size: 12px;
          color: #333;
        }
      }

    }

    .el-button:nth-child(1){
      margin-left: 10px;
    }
    .mini-paper-section-body .el-button{
      min-width: 55px;
      margin-top: 15px;;
    }
    .answer-tips{
      vertical-align: middle;
      font-size: 12px;
      height: 20px;
      line-height: 20px;
    }
  }

  .el-main {
    color: #333;
    text-align: left;
    padding: 8px 20px;

    .el-radio{
      display: block;
      margin:10px 0;
    }
  }

  .right-box-card{
    text-align: center;
    h4{
      font-weight: normal;
      margin: 0;
    }

  }
  .btn-submit-paper{
    width: 100%;
    font-size: 18px;
    margin-top: 18px;
  }
}

.clearfix:before,
.clearfix:after {
  display: table;
  content: "";
}
.clearfix:after {
  clear: both
}

</style>

