<template>
  <div class="app-container">
    <el-row>
      <el-col :span="24">
        <div class="grid-content">

          <el-descriptions class="margin-top" title=" 缓存基本信息" :column="3" :size="size" border>
            <!-- <template slot="extra">
              <el-button type="primary" size="small">操作</el-button>
            </template> -->
            <el-descriptions-item>
              <template slot="label">
                <i class="el-icon-user" />
                Redis版本
              </template>
              {{ redisInfo.redis_version }}
            </el-descriptions-item>
            <el-descriptions-item>
              <template slot="label">
                <i class="el-icon-mobile-phone" />
                运行模式
              </template>
              {{ redisInfo.redis_mode }}
            </el-descriptions-item>
            <el-descriptions-item>
              <template slot="label">
                <i class="el-icon-location-outline" />
                TCP/IP 监听端口
              </template>
              {{ redisInfo.tcp_port }}
            </el-descriptions-item>
            <el-descriptions-item>
              <template slot="label">
                <i class="el-icon-tickets" />
                已连接客户端的数量
              </template>
              <el-tag size="small">{{ redisInfo.connected_clients }}</el-tag>
            </el-descriptions-item>
            <el-descriptions-item>
              <template slot="label">
                <i class="el-icon-office-building" />
                运行时间(天)
              </template>
              {{ redisInfo.uptime_in_days }}
            </el-descriptions-item>
            <el-descriptions-item>
              <template slot="label">
                <i class="el-icon-mobile-phone" />
                正在等待阻塞命令的客户端的数量
              </template>
              {{ redisInfo.blocked_clients }}
            </el-descriptions-item>
            <el-descriptions-item>
              <template slot="label">
                <i class="el-icon-mobile-phone" />
                Redis 分配的内存总量
              </template>
              {{ redisInfo.used_memory_human }}
            </el-descriptions-item>
            <el-descriptions-item>
              <template slot="label">
                <i class="el-icon-mobile-phone" />
                使用CPU
              </template>
              {{ redisInfo.used_cpu_sys }}
            </el-descriptions-item>
            <el-descriptions-item>
              <template slot="label">
                <i class="el-icon-mobile-phone" />
                Redis主机具有的内存总量
              </template>
              {{ redisInfo.total_system_memory_human }}
            </el-descriptions-item>
            <el-descriptions-item>
              <template slot="label">
                <i class="el-icon-mobile-phone" />
                AOF是否开启
              </template>
              {{ redisInfo.aof_enabled }}
            </el-descriptions-item>
            <el-descriptions-item>
              <template slot="label">
                <i class="el-icon-mobile-phone" />
                过期的key数量
              </template>
              {{ redisInfo.expired_keys }}
            </el-descriptions-item>
            <el-descriptions-item>
              <template slot="label">
                <i class="el-icon-mobile-phone" />
                服务器处理的命令总数
              </template>
              {{ redisInfo.total_commands_processed }}
            </el-descriptions-item>
            <el-descriptions-item>
              <template slot="label">
                <i class="el-icon-mobile-phone" />
                服务器接受的连接总数
              </template>
              {{ redisInfo.total_connections_received }}
            </el-descriptions-item>
            <el-descriptions-item>
              <template slot="label">
                <i class="el-icon-mobile-phone" />
                操作系统
              </template>
              {{ redisInfo.os }}
            </el-descriptions-item>
          </el-descriptions>
        </div>
      </el-col>
    </el-row>
    <el-row :gutter="10">
      <el-col :span="12">
        <div class="grid-content bg-purple">
          <el-card class="box-card">
            <div slot="header" class="clearfix">
              <span>命令统计</span>
            </div>
            <!-- 通过属性给组件传值 传给组件的数据要有值图标才会显示-->
            <pie-chart v-if="redisInfo" :chart-data="chartData" :redis-info="redisInfo" />
          </el-card>

        </div>
      </el-col>
      <el-col :span="12">
        <div class="grid-content bg-purple-light">
          <el-card class="box-card">
            <div slot="header" class="clearfix">
              <span>内存信息</span>
            </div>
            <!-- 通过属性给组件传值 -->
            <gauge-chart :chart-data="chartData" />
          </el-card>
        </div>
      </el-col>
    </el-row>
  </div>
</template>

<script>
// 引入组件不能添加括号，需要单个引入
import PieChart from './components/PieChart'
import GaugeChart from './components/GaugeChart'

import { getRedisInfo } from '@/api/system-manage/monitor/cache'

export default {
  name: 'CacheMonitor',
  components: {
    PieChart,
    GaugeChart
  },
  data() {
    return {
      redisInfo: undefined,
      blank: {

      },
      size: '',
      memoryRate: undefined,
      chartData: {}
    }
  },
  created() {
    this.fetchRedisInfo()
  },
  methods: {
    // 获取redis基本信息
    fetchRedisInfo() {
      getRedisInfo().then(response => {
        this.redisInfo = response
        console.log('response', response.used_memory, response.used_memory_rss)
        // this.memoryRate = response.used_memory / response.total_system_memory
        this.chartData.memoryRate = (response.used_memory_rss / response.used_memory).toFixed(2) * 100
      })
    },
    onSubmit() {
      this.$message('submit!')
    },
    onCancel() {
      this.$message({
        message: 'cancel!',
        type: 'warning'
      })
    }
  }
}
</script>

<style lang="scss" scoped>
.line {
    text-align: center;
}

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
