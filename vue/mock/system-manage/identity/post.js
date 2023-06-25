const Mock = require('mockjs')

const List = []
const count = 100
// console.log('mock1')
const baseContent = '<p>I am testing data, I am testing data.</p><p><img src="https://wpimg.wallstcn.com/4c69009c-0fd4-4153-b112-6cb53d1cf943"></p>'
const image_uri = 'https://wpimg.wallstcn.com/e4558086-631c-425c-9430-56ffb46e70b3'

/**
 *
 * {
            "createBy": "admin",
            "creationTime": "2023-04-23 16:11:40",
            "updateBy": null,
            "lastModificationTime": null,
            "remark": "",
            "postId": 2,
            "postCode": "se",
            "postName": "项目经理",
            "postSort": 2,
            "status": "0",
            "flag": false
        },
 *
 */

for (let i = 0; i < count; i++) {
  List.push(Mock.mock({
    id: '@increment',
    createBy: '@cWord(2,3)',
    creationTime: '@datetime',
    updateBy: '@cWord(2,3)',
    lastModificationTime: '@datetime',
    remark: '@cParagraph(1,3)',
    postId: '@Integer(1,10000)',
    postCode: '@String(2,6)',
    postName: '@cWord(3,4)',
    postSort: '@Integer(1,100)',
    'status|1': ['0', '1'],
    flag: false

    // timestamp: +Mock.Random.date('T'),
    // author: '@first',
    // reviewer: '@first',
    // title: '@cWord(5, 10)',
    // content_short: 'mock data',
    // content: baseContent,
    // forecast: '@float(0, 100, 2, 2)',
    // importance: '@integer(1, 3)',
    // 'type|1': ['CN', 'US', 'JP', 'EU'],
    // 'status|1': ['published', 'draft'],
    // display_time: '@datetime',
    // comment_disabled: true,
    // pageviews: '@integer(300, 5000)',
    // image_uri,
    // platforms: ['a-platform']
  }))
}

module.exports = [
  // get List
  {
    url: '/api/identity/post/search',
    type: 'get',
    response: config => {
      console.log('mock')
      // console.log('config', config)
      const { importance, type, filter, page = 1, limit = 10, sort } = config.query

      let mockList = List.filter(item => {
        // if (importance && item.importance !== +importance) return false
        // if (type && item.type !== type) return false
        if (filter && item.postName.indexOf(filter) < 0) return false
        return true
      })

      if (sort === '-id') {
        mockList = mockList.reverse()
      }

      const pageList = mockList.filter((item, index) => index < limit * page && index >= limit * (page - 1))

      return {
        code: 20000,
        data: {
          totalCount: mockList.length,
          items: pageList
        }
      }
    }
  },
  // get detail
  {
    url: '/api/identity/post/[A-Za-z0-9]',
    type: 'get',
    response: config => {
      const { id } = config.query
      for (const article of List) {
        if (article.id === +id) {
          return {
            code: 20000,
            data: article
          }
        }
      }
    }
  },

  {
    url: '/vue-element-template/article/pv',
    type: 'get',
    response: _ => {
      return {
        code: 20000,
        data: {
          pvData: [
            { key: 'PC', pv: 1024 },
            { key: 'mobile', pv: 1024 },
            { key: 'ios', pv: 1024 },
            { key: 'android', pv: 1024 }
          ]
        }
      }
    }
  },
  // 创建
  {
    url: '/api/identity/post',
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
    url: `/api/identity/post/[A-Za-z0-9]`,
    type: 'post',
    response: _ => {
      return {
        code: 20000,
        data: 'success'
      }
    }
  },

  // fetchComments 的 mock
  {
  // url 必须能匹配你的接口路由
  // 比如 fetchComments 对应的路由可能是 /article/1/comments 或者 /article/2/comments
  // 所以你需要通过正则来进行匹配
    url: '/article/[A-Za-z0-9]/comments',
    type: 'get', // 必须和你接口定义的类型一样
    response: (req, res) => {
    // 返回的结果
    // req and res detail see
    // https://expressjs.com/zh-cn/api.html#req
      return {
        code: 20000,
        data: {
          status: 'success'
        }
      }
    }
  }
]

