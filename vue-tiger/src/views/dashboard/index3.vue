<template>
  <div class="dashboard-container">
    <el-row>
      <el-col :span="4">
        <div class="grid-content bg-purple">
          <h3>支付订单金额</h3>
          <p>13625.23</p>
          <p>昨日:262.23</p>
          <p>同比上周:625.23%</p>
        </div>
      </el-col>
      <el-col :span="4"><div class="grid-content bg-purple-light">2</div></el-col>
      <el-col :span="4"><div class="grid-content bg-purple">3</div></el-col>
      <el-col :span="4"><div class="grid-content bg-purple-light">4</div></el-col>
      <el-col :span="4"><div class="grid-content bg-purple">5</div></el-col>
      <el-col :span="4"><div class="grid-content bg-purple-light">6</div></el-col>
    </el-row>
  </div>

</template>

<script>
import { mapGetters } from 'vuex'
// import BoxCard from './admin/components/BoxCard'

export default {
  name: 'Dashboard',
  components: {

  },
  data() {
    return {
      input: '',
      active: 0,
      form: {
        name: '',
        region: '',
        date1: '',
        date2: '',
        delivery: false,
        type: [],
        resource: '',
        desc: ''
      }
    }
  },
  computed: {
    ...mapGetters([
      'name'
    ])
  },
  methods: {
    next() {
      console.log('next')
      console.log(this.active)
      if (this.active++ > 2) this.active = 0
    },
    onSubmit() {
      console.log('submit')
    }
  }
}
</script>

<style lang="scss" scoped>
.dashboard {
  &-container {
    margin: 30px;
  }
  &-text {
    font-size: 30px;
    line-height: 46px;
  }

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
