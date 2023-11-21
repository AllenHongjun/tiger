// JS树结构数据处理工具  https://github.com/wintc23/js-tree-tool

const DEFAULT_CONFIG = {
  id: 'id',
  children: 'children',
  pid: 'pid'
}

const getConfig = config => Object.assign({}, DEFAULT_CONFIG, config)

const tools = {
  fromList(list, config = {}) {
    config = getConfig(config)
    const nodeMap = new Map(); const result = []; const { id, children, pid } = config
    for (const node of list) {
      node[children] = node[children] || []
      nodeMap.set(node[id], node)
    }
    for (const node of list) {
      const parent = nodeMap.get(node[pid])
      ;(parent ? parent.children : result).push(node)
    }
    return result
  },

  toList(tree, config = {}) {
    config = getConfig(config)
    const { children } = config; const result = [...tree]
    for (let i = 0; i < result.length; i++) {
      if (!result[i][children]) continue
      result.splice(i + 1, 0, ...result[i][children])
    }
    return result
  },

  findNode(tree, func, config = {}) {
    config = getConfig(config)
    const { children } = config; const list = [...tree]
    for (const node of list) {
      if (func(node)) return node
      node[children] && list.push(...node[children])
    }
    return null
  },

  findNodeAll(tree, func, config = {}) {
    config = getConfig(config)
    const { children } = config; const list = [...tree]; const result = []
    for (const node of list) {
      func(node) && result.push(node)
      node[children] && list.push(...node[children])
    }
    return result
  },

  findPath(tree, func, config = {}) {
    config = getConfig(config)
    const path = []; const list = [...tree]; const visitedSet = new Set(); const { children } = config
    while (list.length) {
      const node = list[0]
      if (visitedSet.has(node)) {
        path.pop()
        list.shift()
      } else {
        visitedSet.add(node)
        node[children] && list.unshift(...node[children])
        path.push(node)
        if (func(node)) return path
      }
    }
    return null
  },

  findPathAll(tree, func, config = {}) {
    config = getConfig(config)
    const path = []; const list = [...tree]; const result = []
    const visitedSet = new Set(); const { children } = config
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
  },

  filter(tree, func, config = {}) {
    config = getConfig(config)
    const { children } = config
    function listFilter(list) {
      return list.map(node => ({ ...node })).filter(node => {
        node[children] = node[children] && listFilter(node[children])
        return func(node) || (node[children] && node[children].length)
      })
    }
    return listFilter(tree)
  },

  forEach(tree, func, config = {}) {
    config = getConfig(config)
    const list = [...tree]; const { children } = config
    for (let i = 0; i < list.length; i++) {
      func(list[i])
      list[i][children] && list.splice(i + 1, 0, ...list[i][children])
    }
  },

  _insert(tree, node, targetNode, config, after) {
    config = getConfig(config)
    const { children } = config
    function insert(list) {
      const idx = list.indexOf(node)
      idx < 0 ? list.forEach(n => insert(n[children] || [])) : list.splice(idx + after, 0, targetNode)
    }
    insert(tree, node)
  },

  insertBefore(tree, newNode, oldNode, config = {}) {
    tools._insert(tree, oldNode, newNode, config, 0)
  },

  insertAfter(tree, oldNode, newNode, config = {}) {
    tools._insert(tree, oldNode, newNode, config, 1)
  },

  removeNode(tree, func, config = {}) {
    config = getConfig(config)
    const { children } = config; const list = [tree]
    while (list.length) {
      const nodeList = list.shift()
      const delList = nodeList.reduce((r, n, idx) => (func(n) && r.push(idx), r), [])
      delList.reverse()
      delList.forEach(idx => nodeList.splice(idx, 1))
      const childrenList = nodeList.map(n => n[children]).filter(l => l && l.length)
      list.push(...childrenList)
    }
  }

}

const makeHandlers = () => {
  const obj = {}
  for (const key in tools) {
    if (key.startsWith('_')) continue
    obj[key] = tools[key]
  }
  return obj
}

const handlers = makeHandlers()

const treeHandler = {
  ...handlers,
  createInstance(config) {
    const obj = {}
    for (const key in handlers) {
      const func = handlers[key]
      obj[key] = (...args) => func(...args, config)
    }
    return obj
  }
}

// if (typeof module === 'undefined' && typeof window !== 'undefined') {
//   window.treeTool = treeHandler
// } else {
//   module.exports = treeHandler
// }

export default treeHandler
