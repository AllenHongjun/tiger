<template>
  <div class="app-container">
    <el-input v-model="filterText" placeholder="输入关键字进行过滤" clearable="" />
    <el-tree
      ref="RegionTree"
      :props="props"
      :load="loadNode"
      lazy
      :filter-node-method="filterNode"
      @check-change="handleCheckChange"
      @node-click="handleNodeClick"
    />
  </div>
</template>

<script>
import {
  getRegionsByParentCode
} from '@/api/system-manage/area/region'

export default {
  name: 'RegionTree',
  props: {
    // 父级获取列表方法
    getList: {
      type: Function,
      require: true,
      default: null
    }
  },
  data() {
    return {
      props: {
        label: 'name',
        children: 'zones'

      },
      count: 1,
      filterText: '',
      parentCode: 0
    }
  },
  watch: {
    filterText(val) {
      this.$refs.RegionTree.filter(val)
    }
  },
  methods: {
    filterNode(value, data) {
      if (!value) return true
      return data.name.indexOf(value) !== -1
    },
    handleCheckChange(data, checked, indeterminate) {
      console.log(data, checked, indeterminate)
    },
    handleNodeClick(data) {
      console.log(data)
      // 异步请求的数据需要放到$nextTick方法里面调用，不然渲染不出来
      this.$nextTick(() => {
        // this.getTreeNode(this.$refs['RegionTree'].getNode(data.name))
        // 调用父级获取列表方法
        this.getList(true, data.areaCode)
      })
    },
    loadNode(node, resolve) {
      // tree懒加载数据使用 https://blog.csdn.net/qq_35073135/article/details/123924685
      if (node.level > 2) return resolve([])

      console.log(node, 'node')
      if (node.level === 0) { // node其实是需要展开树节点，但是第一次的node是个无用的数据，可以认为这个node是element给我们创建的，判断的话，就是level等于0
        this.getTreeData(0, resolve)
      } else {
        this.getTreeData(node.data.areaCode, resolve)
      }
    },
    getTreeData(areaCode, resolve) {
      getRegionsByParentCode(areaCode).then(response => {
        var data = response.items
        resolve(data)
      })
    },
    onSubmit() {
      this.$message('submit!')
    },
    onCancel() {
      this.$message({
        message: 'cancel!',
        type: 'warning'
      })
    }
  }
}
</script>

<style scoped>
.line{
  text-align: center;
}
.app-container{
  height: 850px;

  display: block;

  overflow-y: scroll;
}
</style>

