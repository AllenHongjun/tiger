<!-- 公告栏组件 -->
<template>
  <div id="box" ref="box">
    <div ref="marquee" class="marquee-box" @mouseover="menter" @mouseleave="mleave">
      <p id="pWidth" ref="cmdlist" class="el-icon-bell">
        系统缓存数据管理，请谨慎操作！
      </p>
    </div>
  </div>
</template>

<script>
export default {
  name: 'NoticeBar',
  props: {
    text: {
      type: String,
      default: ''
    },
    speed: {
      type: Number,
      default: 50
    },
    defaultWidth: {
      type: Number,
      default: 375
    }
  },
  data() {
    return {
      value: 0,
      timer: '', // 计时器
      pwidth: 0, // 公告文本的宽度
      windowWidth: document.documentElement.clientWidth // 设备屏幕的宽度
    }
  },

  watch: {
    // 可以通过改变元素的marginLeft值和计时器来实现滚动
    value(newValue, oldValue) {
      const allWidth = parseInt(this.windowWidth) + parseInt(this.pwidth[0])
      if (newValue <= -allWidth) {
        this.$refs.cmdlist.style.marginLeft = this.windowWidth + 'px'
        this.value = 0
      }
    }
  },
  created() {},
  mounted() {
    const element = this.$refs.cmdlist
    this.pwidth = document.defaultView.getComputedStyle(element, '').width.split('px')
    this.timer = setInterval(this.clickCommend, 20)
  },
  beforeDestroy() {
    clearInterval(this.timer)
  },
  methods: {
    clickCommend(e) {
      const _this = this
      this.$nextTick(() => {
        this.value -= 1
        this.$refs.cmdlist.style.marginLeft = _this.windowWidth + this.value + 'px'
      })
    },
    menter() {
      clearInterval(this.timer)
    },
    mleave() {
      this.timer = setInterval(this.clickCommend, 20)
    }
  }
}
</script>

<style lang="css" scoped>
#box {
  width: 100%;
  height: 50px;
  position: relative;
}
.marquee-box  {
  position: relative;
  width: 100%;
  height: 100%;
  overflow:auto;
  background-color: #f0d8d8;
}
#pWidth{
  width: 100%;
  height: 50px;
  padding: 0;
  margin: 0;
  line-height: 50px;
  display: block;
  word-break: keep-all;
  white-space: nowrap;
  overflow:hidden;
  font-family: 微软雅黑;
  font-size:18px;
  color:#f31d1d;
  background-color: #f0d8d8
}
::-webkit-scrollbar {
  width: 0 !important;
}
::-webkit-scrollbar {
  width: 0 !important;height: 0;
}
</style>
