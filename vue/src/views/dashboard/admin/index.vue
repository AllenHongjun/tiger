<template>
  <div class="dashboard-editor-container">

    <github-corner class="github-corner" />

    <el-row>
      <el-form label-width="200px;" label-position="right" style="margin:0 auto; width:500px;">
        <el-form-item :label="$t('AbpIdentity[\'SelectDateTime\']')">
          <el-date-picker
            v-model="queryDateTime"
            type="datetimerange"
            value-format="yyyy-MM-dd HH:mm:ss"
            align="left"
            unlink-panels
            :picker-options="pickerOptions"
            :start-placeholder="$t('AbpIdentity[\'StartTime\']')"
            :end-placeholder="$t('AbpIdentity[\'EndTime\']')"
            :default-time="['00:00:00', '23:59:59']"
          />
        </el-form-item>
      </el-form>

      <!-- <el-button type="primary" style="margin-left:10px;" @click="handleRefresh()">{{ $t('AbpUi.Refresh') }}</el-button> -->
    </el-row>

    <panel-group @handleSetLineChartData="handleSetLineChartData" />

    <el-row style="padding:16px 0px 0;margin-bottom:32px;" :gutter="32">
      <el-col :xs="24" :sm="24" :lg="16">
        <div class="chart-wrapper">
          <line-chart :chart-data="lineChartData" />
        </div>
      </el-col>
      <el-col :xs="24" :sm="24" :lg="8">
        <div class="chart-wrapper">
          <tenant-table />
        </div>
      </el-col>

    </el-row>

    <el-row :gutter="32">
      <el-col :xs="24" :sm="24" :lg="8">
        <div class="chart-wrapper">
          <!-- <pie-chart :chart-data="lineChartData" /> -->
          <pie-chart-error-rate :query-date-time="queryDateTime" />
        </div>
      </el-col>
      <el-col :xs="24" :sm="24" :lg="8">
        <div class="chart-wrapper">
          <!-- <bar-chart /> -->
          <bar-chart-avg-exec-duration :query-date-time="queryDateTime" />
        </div>
      </el-col>
      <el-col :xs="24" :sm="24" :lg="8">
        <div class="chart-wrapper">
          <!-- <raddar-chart /> -->
          <pie-chart-edition-usage-stat />
        </div>
      </el-col>
    </el-row>

    <!-- <el-row :gutter="8">
      <el-col :xs="{span: 24}" :sm="{span: 24}" :md="{span: 24}" :lg="{span: 12}" :xl="{span: 12}" style="padding-right:8px;margin-bottom:30px;">
        <transaction-table />
      </el-col>
      <el-col :xs="{span: 24}" :sm="{span: 12}" :md="{span: 12}" :lg="{span: 6}" :xl="{span: 6}" style="margin-bottom:30px;">
        <todo-list />
      </el-col>
      <el-col :xs="{span: 24}" :sm="{span: 12}" :md="{span: 12}" :lg="{span: 6}" :xl="{span: 6}" style="margin-bottom:30px;">
        <box-card />
      </el-col>
    </el-row> -->

  </div>
</template>

<script>
import GithubCorner from '@/components/GithubCorner'
import PanelGroup from './components/PanelGroup'
import LineChart from './components/LineChart'
import RaddarChart from './components/RaddarChart'
import PieChart from './components/PieChart' // BarChartAvgExecDuration
import PieChartErrorRate from './components/PieChartErrorRate.vue' // BarChartAvgExecDuration
import BarChart from './components/BarChart'
import BarChartAvgExecDuration from './components/BarChartAvgExecDuration.vue'
import PieChartEditionUsageStat from './components/PieChartEditionUsageStat.vue'
import TenantTable from './components/TenantTable.vue'
import TransactionTable from './components/TransactionTable'
import TodoList from './components/TodoList'
import BoxCard from './components/BoxCard'

import { pickerRangeWithHotKey } from '@/utils/picker'
import moment from 'moment'

// 有这个图表想要的数据
const lineChartData = {
  newVisitis: {
    expectedData: [100, 120, 161, 134, 105, 160, 165],
    actualData: [120, 82, 91, 154, 162, 140, 145]
  },
  messages: {
    expectedData: [200, 192, 120, 144, 160, 130, 140],
    actualData: [180, 160, 151, 106, 145, 150, 130]
  },
  purchases: {
    expectedData: [80, 100, 121, 104, 105, 90, 100],
    actualData: [120, 90, 100, 138, 142, 130, 130]
  },
  shoppings: {
    expectedData: [130, 140, 141, 142, 145, 150, 160],
    actualData: [120, 82, 91, 154, 162, 140, 130]
  }
}

export default {
  name: 'DashboardAdmin',
  components: {
    GithubCorner,
    PanelGroup,
    LineChart,
    RaddarChart,
    PieChart,
    BarChart,
    PieChartErrorRate,
    BarChartAvgExecDuration,
    PieChartEditionUsageStat,
    TenantTable,
    TransactionTable,
    TodoList,
    BoxCard
  },
  data() {
    return {
      pickerOptions: pickerRangeWithHotKey,
      // momentjs获取零点时间 https://www.cnblogs.com/ybixian/p/13213670.html
      queryDateTime: [moment().startOf('day').add(-1, 'M').format('YYYY-MM-DD HH:mm:ss'), moment().endOf('day').format('YYYY-MM-DD HH:mm:ss')],
      // queryForm: {
      //   startTime: moment().add(-30, 'day').format('YYYY-MM-DD HH:mm:ss'),
      //   endTime: moment().format('YYYY-MM-DD HH:mm:ss')
      // },
      lineChartData: lineChartData.newVisitis
    }
  },
  methods: {
    // TODO: 优化增加刷新按钮，点击按钮刷新属性，当时间为空的时候，显示空的图表
    // datePickerChange(value) {
    //   if (!value) {
    //     // 日期选择器改变事件 ~ 解决日期选择器清空 值不清空的问题
    //     // 日期选择器改变事件 ~ 解决日期选择器清空 值不清空的问题
    //     this.queryForm.startTime = undefined
    //     this.queryForm.endTime = undefined
    //   }
    // },
    handleSetLineChartData(type) {
      this.lineChartData = lineChartData[type]
    }
  }
}
</script>

<style lang="scss" scoped>
.dashboard-editor-container {
  padding: 32px;
  background-color: rgb(240, 242, 245);
  position: relative;

  .github-corner {
    position: absolute;
    top: 0px;
    border: 0;
    right: 0;
  }

  .chart-wrapper {
    background: #fff;
    padding: 16px 16px 0;
    margin-bottom: 32px;
  }
}

@media (max-width:1024px) {
  .chart-wrapper {
    padding: 8px;
  }
}
</style>
