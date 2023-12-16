<template>
  <div :class="className" :style="{height:height,width:width}" />
</template>

<script>
import echarts from 'echarts'
require('echarts/theme/macarons') // echarts theme
import resize from './mixins/resize'
import {
  getEditionUsageStat
} from '@/api/sass/edition'
import moment from 'moment'

export default {
  name: 'PieChartEditionUsageStat',
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
      this.fetchData()
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
    fetchData() {
      getEditionUsageStat().then(response => {
        const data = []
        response.forEach(item => {
          if (item.tenantCount !== 0)data.push({ name: item.displayName, value: item.tenantCount }) // ------属性
        })

        this.initChart(data)
      })
    },
    initChart(data) {
      this.chart = echarts.init(this.$el, 'macarons')

      this.chart.setOption({
        tooltip: {
          trigger: 'item',
          formatter: '{a} <br/>{b} : {c} ({d}%)'
        },
        legend: {
          left: 'center',
          bottom: '10',
          data: ['成功', '故障']
        },
        series: [
          {
            name: '版本使用情况统计',
            type: 'pie',
            // roseType: 'radius',
            radius: [15, 95],
            center: ['50%', '42%'],
            data: data,
            animationEasing: 'cubicInOut',
            animationDuration: 1600
          }
        ]
      })
    }
  }
}
</script>
