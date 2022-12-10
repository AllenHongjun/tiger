// 进行多语言支持配置
import Vue from 'vue'
import VueI18n from 'vue-i18n'  // 引入国际化的插件包
import Cookies from 'js-cookie' // 引入 Cookies 保存当前默认语言选项
import elementEnLocale from 'element-ui/lib/locale/lang/en' // element-ui lang element-ui 英文包
import elementZhLocale from 'element-ui/lib/locale/lang/zh-CN' // element-ui lang element-ui 中文包

// 自定义的中英文配置
import helloAbpEnLocale from './en'
import helloAbpZhLocale from './zh'

Vue.use(VueI18n)  // 全局注册国际化包

const messages = {
  en: {
    ...elementEnLocale,
    ...helloAbpEnLocale
  },
  'zh-Hans': {
    ...elementZhLocale,
    ...helloAbpZhLocale
  }
}

export function getLanguage() {
  const chooseLanguage = Cookies.get('language')
  if (chooseLanguage) return chooseLanguage

  // if has not choose language
  const language = (
    navigator.language || navigator.browserLanguage
  ).toLowerCase()
  const locales = Object.keys(messages)
  for (const locale of locales) {
    if (language.indexOf(locale) > -1) {
      return locale
    }
  }
  return 'zh-Hans'
}
export function setLocale(language, values) {
  i18n.mergeLocaleMessage(language, values)
  i18n.locale = language
}

// 创建国际化插件的实例
const i18n = new VueI18n({
  // set locale
  // options: en | zh | es
  // 指定语言类型 zh表示中文  en表示英文 set locale 设置默认初始化的语言 i18n
  locale: getLanguage(),
  // 将将elementUI语言包 和自定义语言包 加入到插件语言数据里 set locale messages
  messages
})

export default i18n
