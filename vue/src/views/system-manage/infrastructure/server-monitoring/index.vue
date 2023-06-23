<template>
  <div class="app-container">

    <el-row :gutter="10">
      <el-col :span="12">
        <div class="grid-content bg-purple">
          <el-card class="box-card">
            <div slot="header" class="clearfix">
              <span>服务器信息</span>
            </div>
            <el-table v-loading="listLoading" :data="systemData" row-key="id" border fit highlight-current-row style="width: 100%">
              <el-table-column min-width="300px" label="名称" width="180">
                <template slot-scope="{row}">
                  <span>{{ row.name }}</span>
                </template>
              </el-table-column>

              <el-table-column align="left" label="信息">
                <template slot-scope="{row}">
                  <span>{{ row.value }}</span>
                </template>
              </el-table-column>
            </el-table>
          </el-card>
        </div>
      </el-col>

      <el-col :span="12">
        <div class="grid-content bg-purple">
          <el-card class="box-card">
            <div slot="header" class="clearfix">
              <span>CLR信息</span>
            </div>
            <el-table :data="CLRData" style="width: 100%">
              <el-table-column prop="name" label="名称" width="180" />
              <el-table-column prop="value" label="信息" />
            </el-table>
          </el-card>
        </div>
      </el-col>
    </el-row>
    <el-row :gutter="10">
      <el-col :span="8">
        <div class="grid-content bg-purple-light">
          <el-card class="box-card">
            <div slot="header" class="clearfix">
              <span>系统使用信息</span>
            </div>
            <el-table :data="systemUsedData" style="width: 100%">
              <el-table-column prop="name" label="名称" width="180" />
              <el-table-column prop="value" label="信息" />
            </el-table>
          </el-card>
        </div>
      </el-col>

      <el-col :span="16">
        <div class="grid-content bg-purple-light">
          <el-card class="box-card">
            <div slot="header" class="clearfix">
              <span>使用率</span>
            </div>
            <el-row :gutter="32">
              <el-col :xs="24" :sm="24" :lg="8">
                <div class="chart-wrapper">
                  <pie-chart :chart-data="cpuChartData" />
                </div>
              </el-col>
              <el-col :xs="24" :sm="24" :lg="8">
                <div class="chart-wrapper">
                  <pie-chart :chart-data="ramChartData" />
                </div>
              </el-col>

              <el-col :xs="24" :sm="24" :lg="8">
                <div class="chart-wrapper">
                  <pie-chart :chart-data="diskChartData" />
                </div>
              </el-col>
            </el-row>
          </el-card>
        </div>
      </el-col>

    </el-row>

    <el-row>
      <el-card class="box-card">
        <div slot="header" class="clearfix">
          <span>磁盘信息</span>
        </div>
        <el-table :data="diskData" style="width: 100%">
          <el-table-column prop="dirName" label="盘符路径" width="180" />
          <el-table-column prop="sysTypeName" label="文件系统" />
          <el-table-column prop="typeName" label="盘符类型" />
          <el-table-column prop="total" label="总大小" />
          <el-table-column prop="free" label="可用大小" />
          <el-table-column prop="used" label="已用大小" />
          <el-table-column prop="usage" label="已用百分比" />
        </el-table>
      </el-card>
    </el-row>
  </div>
</template>

<script>
import { getServerInfo, getCLRInfo, getSystemUsedInfo, getDiskInfo, getServerUsedRate } from '@/api/system-manage/monitor/server'
import PieChart from './components/PieChart'

export default {
  name: 'ServerMonitor',
  components: {
    PieChart
  },
  data() {
    return {
      blank: {

      },
      // 系统信息 必须是数组类型不然会报错
      systemData: [],
      // 系统使用信息
      systemUsedData: [],
      // dotnet运行时
      CLRData: [{
        name: '.NET名称',
        value: 'Java HotSpot(TM) 64-Bit Server VM'
      },
      {
        name: '.NET版本',
        value: '1.8.0_111'
      },
      {
        name: '启动时间',
        value: '2023-04-23 16:12:19'
      },
      {
        name: '运行时长',
        value: '60天0小时40分钟'
      },
      {
        name: '安装路径',
        value: '/usr/java/jdk1.8.0_111/jre'
      },
      {
        name: '项目路径',
        value: '/home/ruoyi/projects/ruoyi-vue'
      },
      {
        name: '运行参数',
        value: '[-Dname=target/ruoyi-vue.jar, -Duser.timezone=Asia/Shanghai, -Xms512m, -Xmx1024m, -XX:MetaspaceSize=128m, -XX:MaxMetaspaceSize=512m, -XX:+HeapDumpOnOutOfMemoryError, -XX:+PrintGCDateStamps, -XX:+PrintGCDetails, -XX:NewRatio=1, -XX:SurvivorRatio=30, -XX:+UseParallelGC, -XX:+UseParallelOldGC]'
      }
      ],
      // 磁盘信息
      diskData: [],
      cpuChartData: {},
      ramChartData: {},
      diskChartData: {},
      listLoading: true
    }
  },
  created() {
    this.fetchData()
    this.handleSetPieChartData()
  },
  methods: {
    async fetchData() {
      this.listLoading = true
      this.systemData = await getServerInfo()
      this.systemUsedData = await getSystemUsedInfo()
      this.CLRData = await getCLRInfo()
      this.diskData = await getDiskInfo()
      this.listLoading = false
    },
    handleSetPieChartData() {
      getServerUsedRate().then(response => {
        this.cpuChartData = {
          name: 'CPU使用率',
          rate: response.cpuRate,
          pieData: [response.cpuRate, 100]
        }

        this.ramChartData = {
          name: '内存使用率',
          rate: response.ramRate,
          pieData: [response.freeRam, response.totalRam]
        }

        this.diskChartData = {
          name: '磁盘使用率',
          rate: response.diskRate,
          pieData: [response.freeDisk, response.totalDisk]
        }
        // console.log('this.cpuChartData', this.cpuChartData)
        // console.log('this.ramChartData', this.ramChartData)
      })
    }

  }
}
</script>

<style lang="scss" scoped>
// 卡片
.text {
    font-size: 14px;
}

.item {
    margin-bottom: 18px;
}

.clearfix:before,
.clearfix:after {
    display: table;
    content: "";
}

.clearfix:after {
    clear: both
}

.box-card {
    width: 100%;
}

.line {
    text-align: center;
}

// 布局
.el-row {
    margin-bottom: 20px;

    &:last-child {
        margin-bottom: 0;
    }
}

.el-col {
    border-radius: 4px;
}

.bg-purple-dark {
    background: #99a9bf;
}

.bg-purple {
    background: #d3dce6;
}

.bg-purple-light {
    background: #e5e9f2;
}

.grid-content {
    border-radius: 4px;
    min-height: 36px;
}

.row-bg {
    padding: 10px 0;
    background-color: #f9fafc;
}
</style>
