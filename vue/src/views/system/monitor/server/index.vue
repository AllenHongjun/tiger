<template>
  <div class="app-container">

    <el-row :gutter="10">
      <el-col :span="12">
        <div class="grid-content bg-purple">
          <el-card class="box-card">
            <div slot="header" class="clearfix">
              <h4>系统信息</h4>
            </div>
            <el-row>
              <el-col :span="8"><label>主机名称:</label></el-col>
              <el-col :span="16"><span>{{ systemData.hostName }}</span></el-col>
            </el-row>
            <el-row>
              <el-col :span="8"><label>操作系统：</label></el-col>
              <el-col :span="16"><span>{{ systemData.systemOs }}</span></el-col>
            </el-row>
            <el-row>
              <el-col :span="8"><label>系统架构：</label></el-col>
              <el-col :span="16"><el-tag type="info">{{ systemData.osArchitecture }}</el-tag></el-col>
            </el-row>
            <el-row>
              <el-col :span="8"><label>CPU核数：</label></el-col>
              <el-col :span="16"><el-tag>{{ systemData.processorCount }}</el-tag></el-col>
            </el-row>

            <el-row>
              <el-col :span="8"><label>运行时长:</label></el-col>
              <el-col :span="16"><span>{{ systemData.sysRunTime | moment }}</span></el-col>
            </el-row>

            <el-row>
              <el-col :span="8"><label>外网地址：</label></el-col>
              <!-- {{ systemData.remoteIp }} -->
              <el-col :span="16"><span>您的iP地址是：[101.70.210.114 ] 来自：中国浙江金华婺城 联通 </span></el-col>
            </el-row>
            <el-row>
              <el-col :span="8"><label>内网地址：</label></el-col>
              <!-- {{ systemData.localIp }} -->
              <el-col :span="16"><el-tag>127.0.0.1</el-tag></el-col>
            </el-row>
            <el-row>
              <el-col :span="8"><label>运行框架：</label></el-col>
              <el-col :span="16"><span>{{ systemData.frameworkDescription }}</span></el-col>
            </el-row>

            <el-row>
              <el-col :span="8"><label>系统时间:</label></el-col>
              <el-col :span="16"><span>{{ systemData.sysRunTime | moment }}</span></el-col>
            </el-row>

          </el-card>
        </div>
      </el-col>
      <el-col :span="12">
        <div class="grid-content bg-purple-light">
          <el-card class="box-card">
            <div slot="header" class="clearfix">
              <h4>使用信息</h4>
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
            <el-row>
              <el-col :span="12"><label>启动时间：</label></el-col>
              <el-col :span="12"><span>{{ systemUsedData.startTime }}</span></el-col>
            </el-row>
            <el-row>
              <el-col :span="12"><label>开发环境：</label></el-col>
              <el-col :span="12"><span>{{ systemData.environment }}</span></el-col>
            </el-row>
            <el-row>
              <el-col :span="12"><label>环境变量：</label></el-col>
              <el-col :span="12"><span>{{ systemData.stage }}</span></el-col>
            </el-row>

          </el-card>
        </div>
      </el-col>
    </el-row>

    <el-row>
      <el-card class="box-card">
        <div slot="header" class="clearfix">
          <h4>磁盘信息</h4>
        </div>
        <el-table :data="diskData" style="width: 100%">
          <el-table-column prop="diskName" label="盘符路径" />
          <el-table-column prop="typeName" label="盘符类型" width="180" />
          <el-table-column prop="totalSize" label="总大小(mb)" width="180" />
          <el-table-column prop="availableFreeSpace" label="可用大小(mb)" width="180" />
          <el-table-column prop="used" label="已用大小(mb)" width="180" />
          <el-table-column prop="availablePercent" label="已用百分比(%)" width="180" />
        </el-table>
      </el-card>
    </el-row>
  </div>
</template>

<script>
import { getServerInfo, getCLRInfo, getSystemUsedInfo, getDiskInfo } from '@/api/system-manage/monitor/server'
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
      systemData: {},
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
  },
  methods: {
    async fetchData() {
      this.listLoading = true
      this.systemData = await getServerInfo()

      this.systemUsedData = await getSystemUsedInfo()
      this.diskData = await getDiskInfo()

      this.cpuChartData = {
        name: 'CPU使用率',
        rate: this.systemUsedData.cpuRate,
        pieData: [parseFloat(this.systemUsedData.cpuRate), 100] //  前面带数字,后面非数字,可以直接用parseFloat()函数
      }
      this.ramChartData = {
        name: '内存使用率',
        rate: this.systemUsedData.ramRate,
        pieData: [parseFloat(this.systemUsedData.usedRam), parseFloat(this.systemUsedData.freeRam)]
      }
      // console.log('this.diskData', this.diskData)
      // TODO:新增接口返回总的磁盘使用数据
      this.diskChartData = {
        name: 'C:盘使用率',
        rate: this.diskData[0].availablePercent + '%',
        pieData: [parseFloat(this.diskData[0].availableFreeSpace), this.diskData[0].availableFreeSpace]
      }

      this.listLoading = false
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

</style>
