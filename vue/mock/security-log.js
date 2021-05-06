
const Mock = require('mockjs')

const List = []
const count = 100


for (let i = 0; i < count; i++) {
  List.push(Mock.mock({
    id: '@increment',
    timestamp: +Mock.Random.date('T'),
    identity: Mock.Random.string('lower', 5, 6),
    action: Mock.Random.string(5, 9),
    userName: '@first',
    clientIpAddress: Mock.Random.ip(),
    browserInfo: '@title(5, 8)',
    creationTime: '@datetime'
    // author: '@first',
    // reviewer: '@first',
    // title: '@title(5, 10)',
    // content_short: 'mock data',

    //forecast: '@float(0, 100, 2, 2)',
    // importance: '@integer(1, 3)',
    // 'type|1': ['CN', 'US', 'JP', 'EU'],
    // 'status|1': ['published', 'draft'],
    //display_time: '@datetime',
    // comment_disabled: true,
    //pageviews: '@integer(300, 5000)',


  }))
}


module.exports = [
  {
    url: '/vue/security-log/list',
    type: 'get',
    response: config => {
      const { userName, daterange, page = 1, limit = 20, sort } = config.query

      let mockList = List.filter(item => {
        // if (importance && item.importance !== +importance) return false
        // if (type && item.type !== type) return false
         if (userName && item.userName.indexOf(userName) < 0) return false
        //  if (dateragne && item.creationTime)
        return true
      })

      if (sort === '-id') {
        mockList = mockList.reverse()
      }

      const pageList = mockList.filter((item, index) => index < limit * page && index >= limit * (page - 1))

      return {
        code: 20000,
        data: {
          total: mockList.length,
          items: pageList
        }
      }
    }
  }


]

