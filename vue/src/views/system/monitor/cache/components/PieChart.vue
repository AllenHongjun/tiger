<template>
  <div :class="className" :style="{height:height,width:width}" />
</template>

<script>
import echarts from 'echarts'
require('echarts/theme/macarons') // echarts theme
import resize from '@/components/Charts/mixins/resize'

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
    },
    redisInfo: {
      type: Object,
      default: undefined
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
  created() {
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

      this.chart.setOption({
        tooltip: {
          trigger: 'item',
          formatter: '{a} <br/>{b} : {c} ({d}%)'
        },
        legend: {
          left: 'center',
          bottom: '10',
          data: ['成功', '失败']
        },
        series: [
          {
            name: '查找到key的成功率',
            type: 'pie',
            // roseType: 'radius',
            radius: [15, 95],
            center: ['50%', '42%'],
            data: [
              { value: this.redisInfo.keyspace_hits, name: '成功' },
              { value: this.redisInfo.keyspace_misses, name: '失败' }
            ],
            animationEasing: 'cubicInOut',
            animationDuration: 1600
          }
        ]
      })
    }
  }
}
</script>
