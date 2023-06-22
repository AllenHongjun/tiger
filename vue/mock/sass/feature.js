
const Mock = require('mockjs')

const data = Mock.mock({
  'items|30': [{
    id: '@id',
    title: '@sentence(10, 20)',
    'status|1': ['published', 'draft', 'deleted'],
    author: 'name',
    display_time: '@datetime',
    pageviews: '@integer(300, 5000)'
  }]
})

module.exports = [
  {
    // url 必须能匹配你的接口路由
  // 比如 fetchComments 对应的路由可能是 /article/1/comments 或者 /article/2/comments
  // 所以你需要通过正则来进行匹配
    url: '/dev-api/feature-management/features',
    type: 'get', // 必须和你接口定义的类型一样
    response: config => {
      // 返回的结果
    // req and res detail see
    // https://expressjs.com/zh-cn/api.html#req
      const items = data.items
      return {
        code: 20000,
        data: {
          totalCount: items.length,
          items: items
        }
      }
    }
  }
]
