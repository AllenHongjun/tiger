<template>
  <div :class="className" :style="{height:height,width:width}" />
</template>

<script>
import echarts from 'echarts'
require('echarts/theme/fruit') // echarts theme
import resize from './mixins/resize' // 重置图表形状防止变形

export default {
  mixins: [resize],
  // 通过属性给组件传值
  props: {
    className: {
      type: String,
      default: 'chart'
    },
    width: {
      type: String,
      default: '100%'
    },
    height: {
      type: String,
      default: '300px'
    },
    autoResize: {
      type: Boolean,
      default: true
    },
    chartData: {
      type: Object,
      required: true
    }
  },
  data() {
    return {
      chart: null
    }
  },
  // 第一种 watch options变化 利用vue的深度 watcher，options 一有变化就重新setOption
  watch: {
    chartData: {
      handler(val) {
        this.setOptions(val)
      },
      deep: true
    }
  },
  mounted() {
    // 因为 ECharts 初始化必须绑定 dom，所以我们只能在 vue 的 mounted 生命周期里进行初始化
    this.$nextTick(() => {
      this.initChart()
    })
  },

  beforeDestroy() {
    if (!this.chart) {
      return
    }
    this.chart.dispose()
    this.chart = null
  },
  methods: {
    initChart() {
      this.chart = echarts.init(this.$el, 'macarons')
      this.setOptions(this.chartData)
    },
    setOptions(data) {
      var option = null
      option = {
        tooltip: {
          formatter: '{a} <br/>{b} : {c}%'
        },
        toolbox: {
          feature: {
            restore: {},
            saveAsImage: {}
          }
        },
        series: [
          {
            name: '内存消耗',
            type: 'gauge',
            detail: { formatter: '{value}%' },
            data: [{ value: 75, name: '使用内存' }]
          }
        ]
      }
      this.chart.setOption(option, true)
      // 实时查询数据
      // setInterval(function() {
      //   option.series[0].data[0].value = (Math.random() * 100).toFixed(2) - 0
      //   this.chart.setOption(option, true)
      // }, 2000)
      // console.log('data', data)
    }

  }
}
</script>
