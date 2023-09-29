<template>
  <div :class="className" :style="{height:height,width:width}" />
</template>

<script>
import echarts from 'echarts'
require('echarts/theme/macarons') // echarts theme
import resize from './mixins/resize'
import {
  getAuditLogErrorRate
} from '@/api/system-manage/auditing/auditlog'

export default {
  name: 'PieChartErrorRate',
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
      var req = {
        startDate: '2021-09-22',
        endDate: '2023-09-30'
      }
      getAuditLogErrorRate(req).then(response => {
        // const data = []
        // response.forEach(item => {
        //   if (item.tenantCount !== 0)data.push({ name: item.displayName, value: item.tenantCount }) // ------属性
        // })

        this.initChart(response)
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
            name: '日志中的错误率',
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
