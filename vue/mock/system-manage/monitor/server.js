const { config } = require('@vue/test-utils')
const Mock = require('mockjs')

const List = []
const diskList = []
const count = 100

for (let i = 0; i < count; i++) {
  List.push(Mock.mock({
    id: '@increment',
    name: '@cWord(4)',
    value: '@sentence(3, 6)'
  }))
}

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
  // 获取磁盘使用信息
  {
    url: '/api/monitor/disk-info',
    type: 'get',
    response: config => {
      return diskList
    }
  }
]

