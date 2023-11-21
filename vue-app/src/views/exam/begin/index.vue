<template>
  <div class="content-container">
    <el-card class="bodbox content-box">
      <div class="header">
        <h1>这里是考试标题</h1>
        <h4>考试时间：从11-13 21:51至11-20 21:51</h4>
      </div>
      <div class="body">
        <h3>考试须知</h3>
        <section>
          为了您能顺利地完成本次考试，请阅读并注意以下事项：
          <h5>一、考试环境</h5>
          <p>
            1、本考试限电脑端参加，请使用谷歌（Chrome）或Edge浏览器；
            2、考试前，请检查所有硬件设备、网络是否正常，确保硬件性能和网络稳定；
            3、考试中因断电、关机等退出考试界面，请务必用上次参加时使用的软件（如谷歌浏览器、微信）打开链接或扫码，点击【继续考试】继续之前的作答；
            4、请尽量选择一处相对安静、不易被打扰的环境；
          </p>
          <h5>二、考试须知</h5>
          <p>
            1、考试有效时间从 2023-11-13 21:51 至 2023-11-20 21:51，请务必在有效时间内参加；
            2、考试卷面满分为35.0分，及格分数线：3.0分；
            3、允许考生参加1次，答题限时为60分钟，系统采用倒计时方式；
          </p>
          <h5>三、考场纪律</h5>
          <p>
            考生应知悉，在整个考试过程上，考生应承诺自觉遵守考试纪律，并知悉以下行为将会被认定违反考试纪律或作弊行为。违纪考生将视为违纪并取消成绩；
            1、不允许出现伪造资料、身份信息等，替代他人或委托 他人代为参加考试的行为；
            2、考试全程不允许出现翻看书籍、资料或使用手机、计算器、平板电脑等与考试无关的行为；
            3、考试全程不允许以任意方式(包括但不限于抄录、截图摄屏、视频记录等行为)记录或传播考试过程及试题；
            4、考试全程不允许出现与考试内容相关的讨论、对话等声音；
          </p>

          本次考试试题属于机密内容，不得对外泄露。如发现有通过摄屏、拍照并在互联网传播等渠道泄露试题的行为，将取消考生本次考试成绩并依法追究相关法律责任
        </section>
      </div>
      <div class="footer">
        <el-checkbox>我已阅读考试须知</el-checkbox>
        <el-button type="primary" @click="enterExam">进入考试</el-button>
      </div>
    </el-card>
  </div>
</template>

<script>
export default {
  name: 'TakeExam',
  data() {
    return {
    }
  },
  methods: {
    enterExam() {
      this.$router.push('/exam/exam-peper')
    }
  }
}
</script>

<style lang="scss" scoped>
.line{
  text-align: center;
}

.content-box{
  width: 900px;
  min-height: 990px;
  margin: 0 auto;
  .header{
    text-align: center;
    font-size: 14px;
    h4{
      font-weight: normal;
    }
  }

  .body{
    width: 75%;
    margin: 15px auto 100px;
  }

  .footer{
    text-align: center;
    .el-checkbox{
      display: block;
      margin: 12px auto;
    }
    .el-button{
      width: 80%;
      font-size: 22px;
    }
  }
}
</style>

