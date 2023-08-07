/**
 * 格式化字符串
 * @param formatted 需要处理的字符串
 * @param args 参数列表，可以是数组，也可以是对象
 * @returns 返回格式化的字符串
 * @example format('Hello, {0}!', ['World'])
 * @example format('Hello, {name}!', {name: 'World'})
 */
export function format(formatted, args) {
  if (Array.isArray(args)) {
    for (let i = 0; i < args.length; i++) {
      const regexp = new RegExp('\\{' + i + '\\}', 'gi')
      formatted = formatted.replace(regexp, args[i])
    }
  } else if (typeof args === 'object') {
    Object.keys(args).forEach((key) => {
      const regexp = new RegExp('\\{' + key + '\\}', 'gi')
      formatted = formatted.replace(regexp, args[key])
    })
  }
  return formatted
}
/**
 *
 * @param str 字符串是否为空或空格
 * @returns
 */
export function isNullOrWhiteSpace(str) {
  return str === undefined || str === null || str === '' || str === ' '
}

export function tryToJson(str) {
  if (!str || str.length === 0) return {}
  try {
    return JSON.parse(str)
  } catch {
    return str
  }
}

export function getUnique(val) {
  const arr = val.split('')
  const newArr = [...new Set(arr)]
  return newArr.join('')
}
