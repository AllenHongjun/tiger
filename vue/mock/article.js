/**
 * {
    'Boolean': '@boolean', // 随机生成布尔类型
    'Natural': '@natural(1, 100)', // 随机生成1到100之间自然数
    'Integer': '@integer(1, 100)', // 生成1到100之间的整数
    'Float': '@float(0, 100, 0, 5)', // 生成0到100之间的浮点数,小数点后尾数为0到5位
    'Character': '@character("aeiou")', // 在aeiou中，生成随机字符，不传参表示生成随机字符
    'String': '@string( 2, 10)', // 生成2到10个字符之间的字符串
    'Range': '@range(0, 10, 2)', // 生成一个数组，数组元素从0开始到10结束，间隔为2
    'Date': '@date("yyyy yy y MM M dd d")', // 生成一个随机日期,可加参数定义日期格式，默认yyyy-mm-dd
    'Color1': '@color', // 生成一个颜色16进制随机值
    'Color2': '@rgb',   //生成一个颜色rgb随机值
    'Paragraph':'@paragraph(2, 5)', //生成2至5个句子的文本
    'Sentence':'@sentence(3, 5)',   //生成3至5个单词组成的一个句子
    'World':'@word(3, 5)',          //生成3-5个字母组成的单词
    'title':'@title(3,5)',          //生成3-5个单词组成的标题
    'cParagraph':'@cparagraph(2, 5)', //生成2至5个句子的中文文本
    'cSentence':'@csentence(3, 5)',   //生成3至5个词语组成的一个中文句子
    'cWorld':'@cword(3, 5)',          //生成3-5个字组成的中文词语
    'ctitle':'@ctitle(3,5)',          //生成3-5个词语组成的中文标题
    'Name': '@name', // 生成姓名
    'cName': '@cname', // 生成中文姓名
    'Url': '@url', // 生成url地址
    'Email':'@email',//生成邮箱
    'Address': '@county(true)'， // 生成省 市 县组成的地址
    'Guid':'@guid()',         //生成Guid值
}

 *
 *
 */

const Mock = require('mockjs')

const List = []
const count = 100
// console.log('mock1')
const baseContent = '<p>I am testing data, I am testing data.</p><p><img src="https://wpimg.wallstcn.com/4c69009c-0fd4-4153-b112-6cb53d1cf943"></p>'
const image_uri = 'https://wpimg.wallstcn.com/e4558086-631c-425c-9430-56ffb46e70b3'

for (let i = 0; i < count; i++) {
  List.push(Mock.mock({
    id: '@increment',
    timestamp: +Mock.Random.date('T'),
    author: '@first',
    reviewer: '@first',
    title: '@title(5, 10)',
    content_short: 'mock data',
    content: baseContent,
    forecast: '@float(0, 100, 2, 2)',
    importance: '@integer(1, 3)',
    'type|1': ['CN', 'US', 'JP', 'EU'],
    'status|1': ['published', 'draft'],
    display_time: '@datetime',
    comment_disabled: true,
    pageviews: '@integer(300, 5000)',
    image_uri,
    platforms: ['a-platform']
  }))
}

module.exports = [
  {
    url: '/feature-management/features',
    type: 'get',
    response: config => {
      // console.log('mock')
      // console.log('mock1')
      const { importance, type, title, page = 1, limit = 20, sort } = config.query

      let mockList = List.filter(item => {
        if (importance && item.importance !== +importance) return false
        if (type && item.type !== type) return false
        if (title && item.title.indexOf(title) < 0) return false
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
  },
  {
    url: '/article/list',
    type: 'get',
    response: config => {
      // console.log('mock')
      // console.log('mock1')
      const { importance, type, title, page = 1, limit = 20, sort } = config.query

      let mockList = List.filter(item => {
        if (importance && item.importance !== +importance) return false
        if (type && item.type !== type) return false
        if (title && item.title.indexOf(title) < 0) return false
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
  },

  {
    url: '/vue-element-template/article/detail',
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

  {
    url: '/vue-element-template/article/create',
    type: 'post',
    response: _ => {
      return {
        code: 20000,
        data: 'success'
      }
    }
  },

  {
    url: '/vue-element-template/article/update',
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

