const { config } = require('@vue/test-utils')
const Mock = require('mockjs')

const List = []
const diskList = []
const count = 100

// 服务器信息
for (let i = 0; i < count; i++) {
  List.push(Mock.mock({
    id: '@increment',
    name: '@cWord(4)',
    value: '@sentence(3, 6)'
  }))
}

// 磁盘信息
for (let i = 0; i < 5; i++) {
  diskList.push(Mock.mock({
    dirName: '/',
    sysTypeName: 'ext4',
    typeName: '/',
    // 生成0到100之间的浮点数,小数点后尾数为0到2位
    total: '@float(0, 100, 2, 2) GB',
    free: '@float(0, 100, 2, 2) GB',
    used: '@float(0, 100, 2, 2) GB',
    usage: '@float(0, 100, 2, 2)'
  }))
}

module.exports = [
  // 服务器信息
  {
    url: '/api/monitor/server-info',
    type: 'get',
    response: config => {
      return List.slice(0, 10)
    }
  },
  // 获取clr信息
  {
    url: '/api/monitor/clr-info',
    type: 'get',
    response: config => {
      return List.slice(0, 10)
    }
  },
  // 获取系统使用信息
  {
    url: '/api/monitor/system-used-info',
    type: 'get',
    response: config => {
      return List.slice(0, 6)
    }
  },
  // 获取服务器使用率
  {
    url: '/api/monitor/server-used-rate',
    type: 'get',
    response: config => {
      return {
        freeRam: 1.96,
        usedRam: 0.62,
        totalRam: 3.84,
        ramRate: 17,
        cpuRate: 2,
        freeDisk: 50,
        totalDisk: 300,
        diskRate: 23,
        startTime: '2023-06-23 03:00:03',
        runTime: '00 天 17 小时 48 分 17 秒'
      }
    }
  },
  // 获取磁盘使用信息
  {
    url: '/api/monitor/disk-info',
    type: 'get',
    response: config => {
      return diskList
    }
  }
]

