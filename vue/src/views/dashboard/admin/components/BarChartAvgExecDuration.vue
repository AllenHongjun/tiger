<template>
  <div :class="className" :style="{height:height,width:width}" />
</template>

<script>
import echarts from 'echarts'
require('echarts/theme/macarons') // echarts theme
import resize from './mixins/resize'
import {
  getAuditLogAverageExecutionDurationPerDay
} from '@/api/system-manage/auditing/auditlog'
import moment from 'moment'

const animationDuration = 6000

export default {
  name: 'BarChartAvgExecDuration',
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
      getAuditLogAverageExecutionDurationPerDay(req).then(response => {
        const arrX = []
        const arrY = []
        for (const key in response) {
          arrX.push(moment(key).format('YYYY-MM-DD')) // ------属性
          arrY.push(Math.floor(response[key] * 100) / 100) // ------属性值
        }
        this.initChart(arrX, arrY)
      })
    },
    initChart(arrX, arrY) {
      this.chart = echarts.init(this.$el, 'macarons')

      this.chart.setOption({
        tooltip: {
          trigger: 'axis',
          axisPointer: { // 坐标轴指示器，坐标轴触发有效
            type: 'shadow' // 默认为直线，可选为：'line' | 'shadow'
          }
        },
        grid: {
          top: 10,
          left: '2%',
          right: '2%',
          bottom: '3%',
          containLabel: true
        },
        xAxis: [{
          type: 'category',
          data: arrX,
          axisTick: {
            alignWithLabel: true
          }
        }],
        yAxis: [{
          type: 'value',
          axisTick: {
            show: false
          }
        }],
        series: [
          {
            name: '平均处理时间(ms)',
            type: 'bar',
            stack: 'vistors',
            barWidth: '60%',
            data: arrY,
            animationDuration
          }

        ]
      })
    }
  }
}
</script>
