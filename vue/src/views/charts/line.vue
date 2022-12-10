<template>
  <div class="chart-container">
    <el-row :gutter="32">
      <el-col :xs="24" :sm="24" :lg="12">
        <div class="chart-wrapper">
          <pie-chart />
        </div>
      </el-col>
      <el-col :xs="24" :sm="24" :lg="12">
        <div class="chart-wrapper">
          <bar-chart />
        </div>
      </el-col>
    </el-row>
    <!-- <chart height="50%" width="50%" /> -->
    <el-divider />

    <el-row :gutter="32">
      <el-col :xs="24" :sm="24" :lg="12">
        <div class="chart-wrapper">
          <line-simpe-chart />
        </div>
      </el-col>
      <el-col :xs="24" :sm="24" :lg="12">
        <div class="chart-wrapper">
          <line-stack-chart />
        </div>
      </el-col>

    </el-row>

    <el-row :gutter="32">

      <el-col :xs="24" :sm="24" :lg="12">
        <div class="chart-wrapper">
          <!-- <line-stack-chart /> -->
          <pie-legend-chart />
        </div>
      </el-col>

    </el-row>
  </div>
</template>

<script>
// import Chart from '@/components/Charts/Keyboard'
import Chart from '@/components/Charts/LineMarker'
import PieChart from './components/PieChart'
import BarChart from './components/BarChart'
import AChart from './components/AChart'

import LineSimpeChart from './components/LineSimpeChart'
import LineStackChart from './components/LineStackChart'
import DataSetSimpleChart from './components/DataSetSimpleChart'
import PieLegendChart from './components/PieLegendChart'

export default {
  name: 'LineChart',
  components: { Chart, PieChart, BarChart, AChart, LineSimpeChart, LineStackChart,
    DataSetSimpleChart, PieLegendChart }
}
</script>

<style scoped>
.chart-container{
  position: relative;
  width: 100%;
  height: calc(100vh - 84px);
}
</style>

