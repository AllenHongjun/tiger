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
                    <h4 style="display: inline-block;">第1大题 </h4><span class="question-stat-box">    (共 <b>14</b> 题 <b>90.0</b>  分)</span>
                  </el-col>
                </el-row>
                <el-row class="mini-paper-section-body">
                  <el-button type="success">1</el-button>
                  <el-button type="success">2</el-button>
                  <el-button type="success">3</el-button>
                  <el-button type="danger">4</el-button>
                  <el-button type="success" plain>5</el-button>
                  <el-button type="danger">6</el-button>
                  <el-button type="danger">7</el-button>
                </el-row>
              </div>

              <el-divider />
              <div class="answer-tips">
                <el-button type="success" /> 正确
                <el-button type="danger" /> 错误
                <el-button type="success" plain /> 部分正确
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
                    <h4 style="display: inline-block;">第1大题 </h4><span>    (共 <b>14</b> 题 <b>90.0</b> 分)</span>
                  </el-col>
                </el-row>
                <el-divider />
                <p>
                  这里是大题描述
                </p>
                <el-divider />
                <!-- 判断题 -->
                <el-row class="question-box">
                  <div class="question">
                    <div class="question-content">
                      15.  出口方式为不通过一达通出口的信用保障订单，走阿里物流会有（ ）的交易手续费
                      <el-tag type="info" lable="">判断题 5.0分</el-tag>
                    </div>
                    <div class="question-option">
                      <el-radio-group v-model="radio" :fill="(radio === 'A' ? '#67C23A':'#F56C6C' )">
                        <el-radio-button label="A">A
                          <span class="option-content">正确</span>
                        </el-radio-button>
                        <el-radio-button label="B">B
                          <span class="option-content">错误</span>
                        </el-radio-button>
                      </el-radio-group>
                    </div>
                  </div>
                  <div class="answer" style="background-color:#fbeaea;">
                    <p>得分：<span style="color: red;">0</span> 分</p>
                    <p>标准答案：<span style="color: green;">B</span> </p>
                    <p>考生答案：<b style="color: red;">A</b> <b>ㄨ</b>  </p>
                    <div>
                      评判结果:
                      <el-radio-group v-model="radio1" :fill="(radio1 ? '#67C23A':'#F56C6C' )">
                        <el-radio-button :label="true" border>
                          <i class="el-icon-check" />  正确
                        </el-radio-button>
                        <el-radio-button :label="false" border>
                          <i class="el-icon-close" />  错误
                        </el-radio-button>
                      </el-radio-group>
                      <span>
                        判分
                        <el-input
                          v-model="score"
                          class="manual-score-input"
                          type="text"
                        >5分
                        </el-input>
                        共 <b>10.0</b> 分
                      </span>
                    </div>
                    <div>
                      <el-divider content-position="center">试题解析</el-divider>
                      <p>使用一达通，提交产品预审环节，由客户提出。</p>
                    </div>

                  </div>
                </el-row>
                <!-- 单选题 -->
                <el-row class="question-box">
                  <div class="question">
                    <div class="question-content">
                      1.  出口方式为不通过一达通出口的信用保障订单，走阿里物流会有（ ）的交易手续费
                      <el-tag type="info" lable="">单选题 5.0分</el-tag>
                    </div>
                    <div class="question-option">
                      <el-radio-group v-model="radio" :fill="(radio === 'A' ? '#67C23A':'#F56C6C' )">
                        <el-radio-button label="A">A
                          <span class="option-content">只需要把el-radio元素换成el-radio-button元素即可，此外，Element 还提供了size属性。
                          </span>
                        </el-radio-button>
                        <el-radio-button label="B">B<span class="option-content">通常情况下，应当处理input事件，并更新组件的绑定值或使用v-model</span>
                        </el-radio-button>
                        <el-radio-button label="C">C
                          <span class="option-content">置 autosize 属性可以使得文本域的高度能够根据文本内容自动进</span>
                        </el-radio-button>
                        <el-radio-button label="D">D
                          <span class="option-content">选项内容这里是题目的选项，选项内容这里是题目的选项，选项内容这里是题目的选项</span>
                        </el-radio-button>

                      </el-radio-group>
                    </div>
                  </div>
                  <div class="answer" style="background-color:#fbeaea;">
                    <p>得分：<span style="color: red;">0</span> 分</p>
                    <p>标准答案：<span style="color: green;">B</span> </p>
                    <p>考生答案：<b style="color: red;">A</b> <b>ㄨ</b>  </p>
                    <div>
                      评判结果:
                      <el-radio-group v-model="radio1" :fill="(radio1 ? '#67C23A':'#F56C6C' )">
                        <el-radio-button :label="true" border>
                          <i class="el-icon-check" />  正确
                        </el-radio-button>
                        <el-radio-button :label="false" border>
                          <i class="el-icon-close" />  错误
                        </el-radio-button>
                      </el-radio-group>
                      <span>
                        判分
                        <el-input
                          v-model="score"
                          class="manual-score-input"
                          type="text"
                        >5分
                        </el-input>
                        共 <b>10.0</b> 分
                      </span>
                    </div>
                    <div>
                      <el-divider content-position="center">试题解析</el-divider>
                      <p>使用一达通，提交产品预审环节，由客户提出。</p>
                    </div>

                  </div>
                </el-row>
                <!-- 多选题 -->
                <el-row class="question-box">
                  <div class="question">
                    <div class="question-content">
                      2.  出口方式为不通过一达通出口的信用保障订单，走阿里物流会有（ ）的交易手续费
                      <el-tag type="info" lable="">多选题 5.0分</el-tag>
                    </div>
                    <div class="question-option">
                      <el-checkbox-group v-model="checkboxGroup1" :fill="(checkboxGroup1.join() == ['B'].join() ? '#67C23A':'#F56C6C' )">
                        <el-checkbox-button v-for="questionOption in questionOptions" :key="questionOption" :label="questionOption" style="display:block;">
                          {{ questionOption }}
                          <span class="option-content">只需要把el-radio元素换成el-radio-button元素即可，此外，Element 还提供了size属性。
                          </span>
                        </el-checkbox-button>
                      </el-checkbox-group>
                    </div>
                  </div>
                  <div class="answer" style="">
                    <p>得分：<span style="color: red;">0</span> 分</p>
                    <p>标准答案：<span style="color: green;">B</span> </p>
                    <p>考生答案：<b style="color: red;">A</b> <b>ㄨ</b>  </p>
                    <div>
                      评判结果:
                      <el-radio-group v-model="radio1" :fill="(radio1 ? '#67C23A':'#F56C6C' )">
                        <el-radio-button :label="true" border>
                          <i class="el-icon-check" />  正确
                        </el-radio-button>
                        <el-radio-button :label="false" border>
                          <i class="el-icon-close" />  错误
                        </el-radio-button>
                      </el-radio-group>
                      <span>
                        判分
                        <el-input
                          v-model="score"
                          class="manual-score-input"
                          type="text"
                        >5分
                        </el-input>
                        共 <b>10.0</b> 分
                      </span>
                    </div>
                    <div>
                      <el-divider content-position="center">试题解析</el-divider>
                      <p>使用一达通，提交产品预审环节，由客户提出。</p>
                    </div>

                  </div>
                </el-row>
                <!-- 填空题 -->
                <el-row class="question-box">
                  <div class="question">
                    <div class="question-content">
                      4.  出口方式为不通过一达
                      <el-tag type="info">  答案1</el-tag>
                      通出口的信用保障订
                      <el-tag type="info">答案2</el-tag>
                      单，走阿里物流会有的交易手续费
                      <el-tag type="info" lable="">填空题 5.0分</el-tag>
                    </div>
                  </div>
                  <div class="answer" style="">
                    <p>得分：<span style="color: red;">0</span> 分</p>
                    <p>标准答案：<span style="color: green;">B</span> </p>
                    <p>考生答案：<b style="color: red;">A</b> <b>ㄨ</b>  </p>
                    <div>
                      评判结果:
                      <el-radio-group v-model="radio1" :fill="(radio1 ? '#67C23A':'#F56C6C' )">
                        <el-radio-button :label="true" border>
                          <i class="el-icon-check" />  正确
                        </el-radio-button>
                        <el-radio-button :label="false" border>
                          <i class="el-icon-close" />  错误
                        </el-radio-button>
                      </el-radio-group>
                      <span>
                        判分
                        <el-input
                          v-model="score"
                          class="manual-score-input"
                          type="text"
                        >5分
                        </el-input>
                        共 <b>10.0</b> 分
                      </span>
                    </div>
                    <div>
                      <el-divider content-position="center">试题解析</el-divider>
                      <p>使用一达通，提交产品预审环节，由客户提出。</p>
                    </div>

                  </div>
                </el-row>
                <!-- 问答题 -->
                <el-row class="question-box">
                  <div class="question">
                    <div class="question-content">
                      5.  说一说，刘备是一个怎么样的人?
                      <el-tag type="info" lable="">问答题 20.0分</el-tag>
                    </div>
                    <div>
                      <el-input type="textarea" :autosize="{ minRows: 16, maxRows: 20}" placeholder="这是显示考生答案" :disabled="true" />
                    </div>
                  </div>
                  <div class="answer" style="">
                    <p>得分：<span style="color: red;">0</span> 分</p>
                    <p>标准答案：<span style="color: green;">B</span> </p>
                    <p>考生答案：<b style="color: red;">A</b> <b>ㄨ</b>  </p>
                    <div>
                      评判结果:
                      <el-radio-group v-model="radio1" :fill="(radio1 ? '#67C23A':'#F56C6C' )">
                        <el-radio-button :label="true" border>
                          <i class="el-icon-check" />  正确
                        </el-radio-button>
                        <el-radio-button :label="false" border>
                          <i class="el-icon-close" />  错误
                        </el-radio-button>
                      </el-radio-group>
                      <span>
                        判分
                        <el-input
                          v-model="score"
                          class="manual-score-input"
                          type="text"
                        >5分
                        </el-input>
                        共 <b>10.0</b> 分
                      </span>
                    </div>
                    <div>
                      <el-divider content-position="center">试题解析</el-divider>
                      <p>使用一达通，提交产品预审环节，由客户提出。</p>
                    </div>

                  </div>
                </el-row>
              </div>
            </el-card>

          </el-main>
          <el-aside style="width: 200px;">
            <el-card class="right-box-card">
              <h4>用户名</h4>
              <p>Hongjunyu</p>
              <el-divider />
              <h4>成绩</h4>
              <p style="color:red;">99.0</p>
              <el-divider />
              <h4>状态</h4>
              <p>
                <el-tag type="success">通过</el-tag>
              </p>
            </el-card>

            <el-button type="danger" class="btn-submit-paper" @click="submitPaper()">提交阅卷</el-button>
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
const questionOptions = ['A', 'B', 'C', 'D']
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
      radio1: false,
      score: 15,
      checkboxGroup1: ['B'],
      questionOptions: questionOptions,
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
p {
  margin: 0;
}
.el-header, .el-footer {
  color: #333;
  text-align: left;
  padding: 0;
  margin: 0;
}

.paper-container {
  background: #eef1f6;
}

body  .el-container {
  background: #eef1f6;
  margin-bottom: 40px;
  .el-aside {
    color: #333;
    text-align: left;

    .box-card{
      min-height:800px;
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
      .el-button{
        vertical-align: middle;
      }
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

    .question-box{
      margin-bottom: 60px;
      .question{
        margin-bottom: 20px;
      }
      .question-option {
        .el-radio-button{
          display: block;
          margin:20px 0;
        }
        ::v-deep .el-radio-button__inner{
          border-radius: 25px;
          border-left: 1px solid #DCDFE6;
        }

        .el-checkbox-button{
          margin:20px 0;
        }
        ::v-deep .el-checkbox-button.is-focus .el-checkbox-button__inner{
          border-color: #DCDFE6;
        }

        /*需要加上深度选择器才会生效*/
        ::v-deep .el-checkbox-button__inner{
          border-left: 1px solid #DCDFE6;
          border-radius: 0 0 0 0;
        }

        .option-content{
          position: absolute;
          top: -5px;
          left: -25px;
          color: black;
          width: 800px;
          height: 36px;
          line-height: 36px;
          font-size: 14px;
          font-weight: 100;
          text-align: left;
          text-indent: 6em;
          color: #606266;
        }
      }
      /*题目*/
      .answer{
        background-color: #dcf8ce /*#fbeaea*/;
        padding:20px;
        font-size:16px;
        color: #606266;
        p{
          margin-bottom: 8px;
        }
        .manual-score-input{
          width: 80px;font-weight: bold;color: red;
        }
      }
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

</style>

