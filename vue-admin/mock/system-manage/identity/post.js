const Mock = require('mockjs')

const List = []
const count = 100
// console.log('mock1')

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
      const { status, filter, page = 1, limit = 10, sort } = config.query

      let mockList = List.filter(item => {
        if (status && item.status !== status) return false
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
  // url 必须能匹配你的接口路由
  // 比如 fetchComments 对应的路由可能是 /article/1/comments 或者 /article/2/comments
  // 所以你需要通过正则来进行匹配
    url: '/api/identity/post/[A-Za-z0-9]',
    type: 'get',
    response: config => {
    // 返回的结果
    // req and res detail see
    // https://expressjs.com/zh-cn/api.html#req
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
    url: '/api/identity/post/[A-Za-z0-9]',
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
    url: '/api/identity/post/[A-Za-z0-9]',
    type: 'delete',
    response: _ => {
      return {
        code: 20000,
        data: 'success'
      }
    }
  }

]

