<template>
  <div :class="className" :style="{height:height,width:width}" />
</template>

<script>
import echarts from 'echarts'
require('echarts/theme/macarons') // echarts theme
import resize from './mixins/resize'

const animationDuration = 6000

export default {
  mixins: [resize],
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
    }
  },
  data() {
    return {
      chart: null
    }
  },
  mounted() {
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

      this.chart.setOption(
        {
          xAxis: {
            type: 'category',
            data: ['星期一', '星期二', '星期三', '星期四', '星期五', '星期六', '星期日']
          },
          yAxis: {
            type: 'value'
          },
          series: [{
            data: [820, 20, 901, 934, 3000, 1330, 1320],
            type: 'line',
            smooth: false
          }]
        }

      )
    }
  }
}
</script>
