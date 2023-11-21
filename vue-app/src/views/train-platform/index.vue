<template>
  <div class="app-container">
    <el-row>
      <el-col v-for="(o) in 10" :key="o" :span="8" :offset="2">
        <el-card :body-style="{ padding: '0px' }">
          <div class="image-box">
            <img src="https://ali.srzx.com/img/sxpt.png" class="image">
          </div>

          <!-- <el-image src="https://ali.srzx.com/img/sxpt.png" :fit="scale-down" /> -->
          <div style="padding: 14px;">
            <b>Alibaba国际站实训平台</b>
            <div class="bottom clearfix">
              <el-button type="primary" circle class="el-icon-right" />
            </div>
          </div>
        </el-card>
      </el-col>
    </el-row>
  </div>
</template>

<script>
export default {
  name: 'CN',
  data() {
    return {
      currentDate: new Date(),
      blank: {

      }
    }
  },
  methods: {
  }
}
</script>

<style lang="scss" scoped>

.el-card{
  height: 380px;
  margin-bottom: 30px;
  text-align: center;
  color: #606266;
  border-radius: 5%;
  .image-box {
    background-color: #eee;
    width: 140px;
    height: 140px;
    line-height: 140px;
    border-radius: 50%;
    margin: 40px auto 20px;
    display: block;
    position: relative;

    img{
      position: absolute;
      top: 50%;
      left: 50%;
      transform: translate(-50%,-50%);
    }
  }

  .bottom{
    margin-top: 40px;
    line-height: 12px;
  }
}

.el-card:hover{
  background-color: #409EFF;
  color: azure;
}

.el-card:hover .el-button{
  background-color: #eee;
  color: #409EFF;
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

