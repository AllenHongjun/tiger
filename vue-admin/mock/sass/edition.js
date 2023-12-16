const Mock = require('mockjs')

const List = []
const count = 100
// console.log('mock1')

for (let i = 0; i < count; i++) {
  List.push(Mock.mock({
    id: '@increment',
    displayName: '@cWord(4,8)',
    planId: null,
    planName: null,
    concurrencyStamp: '@String(31,32)',
    extraProperties: {}
  }))
}

module.exports = [
  // get List
  {
    url: '/api/sass/edtions',
    type: 'get',
    response: config => {
      console.log('mock')
      const { filter, page = 1, limit = 10, sort } = config.query
      // 这是node server 的内容 会在服务器端答应内容
      let mockList = List.filter(item => {
        if (filter && item.displayName && item.displayName.indexOf(filter) < 0) return false
        return true
      })

      if (sort === '-id') {
        mockList = mockList.reverse()
      }

      const pageList = mockList.filter((item, index) => index < limit * page && index >= limit * (page - 1))

      return {
        totalCount: mockList.length,
        items: pageList
      }
    }
  },
  // get detail
  {
  // url 必须能匹配你的接口路由
  // 比如 fetchComments 对应的路由可能是 /item/1/comments 或者 /item/2/comments
  // 所以你需要通过正则来进行匹配
    url: '/api/sass/edtions/[A-Za-z0-9]',
    type: 'get',
    response: config => {
    // 返回的结果
    // req and res detail see
    // https://expressjs.com/zh-cn/api.html#req
      const { id } = config.query
      for (const item of List) {
        if (item.id === +id) {
          return {
            code: 20000,
            data: item
          }
        }
      }
    }
  },

  // 创建
  {
    url: '/api/sass/edtions',
    type: 'post',
    response: _ => {
      return {
        code: 20000,
        data: 'success'
      }
    }
  },
  // 更新
  {
    url: '/api/sass/edtions/[A-Za-z0-9]',
    type: 'put',
    response: _ => {
      return {
        code: 20000,
        data: 'success'
      }
    }
  },

  // 删除
  {
    url: '/api/sass/edtions/[A-Za-z0-9]',
    type: 'delete',
    response: _ => {
      return {
        code: 20000,
        data: 'success'
      }
    }
  }

]

