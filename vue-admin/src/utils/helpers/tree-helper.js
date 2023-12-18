const DEFAULT_CONFIG = {
  id: 'id',
  children: 'children',
  pid: 'parentId'
}

const getConfig = config => Object.assign({}, DEFAULT_CONFIG, config)

/**
 * list转为树
 * https://www.zhihu.com/question/588733122#:~:text=function%20buildTree%20%28list%2C%20parentId%20%3D%20null%29%20%7B%20const,return%20tree%3B%20%7D%20%E8%BF%99%E4%B8%AA%E5%87%BD%E6%95%B0%E6%8E%A5%E5%8F%97%E4%B8%80%E4%B8%AA%E5%88%97%E8%A1%A8%20list%20%E5%92%8C%E4%B8%80%E4%B8%AA%E5%8F%AF%E9%80%89%E7%9A%84%20parentId%20%E5%8F%82%E6%95%B0%EF%BC%8C%E8%BF%94%E5%9B%9E%E4%B8%80%E4%B8%AA%E6%A0%91%E7%8A%B6%E7%BB%93%E6%9E%84%E3%80%82
 * @param {*} list
 * @param {*} config
 * @returns
 */
export function recurtionToTree(list, parentId = null) {
  const tree = []
  for (const item of list) {
    if (item.parentId === parentId) {
      const children = recurtionToTree(list, item.id)
      if (children.length) {
        item.children = children
      } else {
        item.children = undefined
      }
      tree.push(item)
    }
  }
  return tree
}

// tree to list
export function listToTree(list, config = {}) {
  const conf = getConfig(config)
  const nodeMap = new Map()
  const result = []
  const { id, children, pid } = conf

  for (const node of list) {
    node[children] = node[children] || []
    nodeMap.set(node[id], node)
  }
  for (const node of list) {
    const parent = nodeMap.get(node[pid])
    ;(parent ? parent[children] : result).push(node)
  }
  return result
}

export function treeToList(tree, config = {}) {
  config = getConfig(config)
  const { children } = config
  const result = [...tree]
  for (let i = 0; i < result.length; i++) {
    if (!result[i][children]) continue
    result.splice(i + 1, 0, ...result[i][children])
  }
  return result
}

export function findNode(tree, func, config = {}) {
  config = getConfig(config)
  const { children } = config
  const list = [...tree]
  for (const node of list) {
    if (func(node)) return node
    node[children] && list.push(...node[children])
  }
  return null
}

export function findNodeAll(tree, func, config = {}) {
  config = getConfig(config)
  const { children } = config
  const list = [...tree]
  const result = []
  for (const node of list) {
    func(node) && result.push(node)
    node[children] && list.push(...node[children])
  }
  return result
}

export function findPath(tree, func, config = {}) {
  config = getConfig(config)
  const path = []
  const list = [...tree]
  const visitedSet = new Set()
  const { children } = config
  while (list.length) {
    const node = list[0]
    if (visitedSet.has(node)) {
      path.pop()
      list.shift()
    } else {
      visitedSet.add(node)
      node[children] && list.unshift(...node[children])
      path.push(node)
      if (func(node)) {
        return path
      }
    }
  }
  return null
}

export function findPathAll(tree, func, config = {}) {
  config = getConfig(config)
  const path = []
  const list = [...tree]
  const result = []
  const visitedSet = new Set()
  const { children } = config
  while (list.length) {
    const node = list[0]
    if (visitedSet.has(node)) {
      path.pop()
      list.shift()
    } else {
      visitedSet.add(node)
      node[children] && list.unshift(...node[children])
      path.push(node)
      func(node) && result.push([...path])
    }
  }
  return result
}

export function filter(tree, func, config = {}) {
  config = getConfig(config)
  const children = config.children
  function listFilter(list) {
    return list
      .map(node => ({ ...node }))
      .filter(node => {
        node[children] = node[children] && listFilter(node[children])
        return func(node) || (node[children] && node[children].length)
      })
  }
  return listFilter(tree)
}

export function forEach(tree, func, config = {}) {
  config = getConfig(config)
  const list = [...tree]
  const { children } = config
  for (let i = 0; i < list.length; i++) {
    // func 返回true就终止遍历，避免大量节点场景下无意义循环，引起浏览器卡顿
    if (func(list[i])) {
      return
    }
    children && list[i][children] && list.splice(i + 1, 0, ...list[i][children])
  }
}

/**
 * @description: Extract tree specified structure
 */
export function treeMap(treeData, opt) {
  return treeData.map(item => treeMapEach(item, opt))
}

/**
 * @description: Extract tree specified structure
 */
export function treeMapEach(data, { children = 'children', conversion }) {
  const haveChildren =
    Array.isArray(data[children]) && data[children].length > 0
  const conversionData = conversion(data) || {}
  if (haveChildren) {
    return {
      ...conversionData,
      [children]: data[children].map(i =>
        treeMapEach(i, {
          children,
          conversion
        })
      )
    }
  } else {
    return {
      ...conversionData
    }
  }
}

/**
 * 递归遍历树结构
 * @param treeDatas 树
 * @param callBack 回调
 * @param parentNode 父节点
 */
export function eachTree(treeDatas, callBack, parentNode = {}) {
  treeDatas.forEach(element => {
    const newNode = callBack(element, parentNode) || element
    if (element.children) {
      eachTree(element.children, callBack, newNode)
    }
  })
}
