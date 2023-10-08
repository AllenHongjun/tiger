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
    },
    queryDateTime: {
      type: Array,
      // 对象或数组默认值必须从一个工厂函数获取
      default: function() {
        return []
      }
    }
  },
  data() {
    return {
      chart: null
    }
  },
  watch: {
    // 此处监听variable变量，当期有变化时执行
    queryDateTime(oldValue, newValue) {
      console.log('oldValue', oldValue, 'newValue', newValue)
      this.$nextTick(() => {
        this.fetchData()
      })
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
        startDate: this.queryDateTime[0],
        endDate: this.queryDateTime[1]
      }
      getAuditLogErrorRate(req).then(response => {
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
