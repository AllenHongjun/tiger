<template>
  <div :class="className" :style="{height:height,width:width}" />
</template>

<script>
import echarts from 'echarts'
require('echarts/theme/macarons') // echarts theme
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
      default: '210px'
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
        if (val) {
          this.setOptions(val)
        }
      },
      deep: true
    }
  },
  mounted() {
    // 因为 ECharts 初始化必须绑定 dom，所以我们只能在 vue 的 mounted 生命周期里进行初始化
    this.$nextTick(() => {
      this.initChart()
      // 一开始渲染，属性没有数据会报错需要判断
      if (this.chartData.rate) {
        this.setOptions(this.chartData)
      }
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
    },
    setOptions(data) {
      // console.log('echartData', data)
      // console.log('data.rate', data.rate)
      // console.log('data.pieData', data.pieData)
      this.chart.setOption({
        tooltip: {
          trigger: 'item',
          formatter: '{a} <br/>{b} : {c} ({d}%)'
        },
        color: ['#e10040', '#e1dbdd'],
        series: [
          {
            name: data.name,
            type: 'pie',
            // roseType: 'radius',
            // 饼图的半径 [内圆半径，外圆半径]
            radius: [50, 60],
            center: ['40%', '45%'],
            data: [
              { value: data.pieData[0], name: '已用' },
              { value: data.pieData[1], name: '剩余' }
            ],
            animationEasing: 'cubicInOut',
            animationDuration: 1600,
            // 配置圆环中间文字
            label: {
              normal: {
                show: true,
                position: 'center',
                color: '#4c4a4a',
                // formatter 指定 label 的格式。具体的，formatter 中可以使用的占位符
                formatter: '{total|' + data.rate + '}' + '\n\r' + '{active|' + data.name + '}',
                rich: {
                  total: {
                    fontSize: 28,
                    fontFamily: '微软雅黑',
                    color: '#454c5c'
                  },
                  active: {
                    fontFamily: '微软雅黑',
                    fontSize: 12,
                    color: '#6c7a89',
                    lineHeight: 30
                  }
                }
              }
            }
          }
        ]

      })
    }

  }
}
</script>
