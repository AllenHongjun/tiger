<template>
  <div class="app-container" style="padding:0 20px;">
    <!-- <div class="filter-container">
      <el-input
        v-model="filterText"
        placeholder="查询组织"
      />
    </div> -->
    <el-tree
      ref="categoryTree"
      :data="categoryTreeData"
      :props="categoryTreeProps"
      :filter-node-method="filterOrg"
      :expand-on-click-node="false"
      :show-checkbox="showCheckbox"
      :check-strictly="checkStrictly"
      node-key="id"
      highlight-current
      default-expand-all
      @node-click="handleOrgClick"
      @check-change="checkChange"
    />
  </div>
</template>

<script>
import {
  getCategoryTree
} from '@/api/basic/category'
import { Tree } from 'element-ui'
export default {
  name: 'CategoryTree',
  mixins: [Tree],
  props: {
    supportSingleChecked: {
      type: Boolean,
      default: false
    },
    categoryTreeNodeClick: {
      type: Function,
      default: () => {}
    }
  },
  data() {
    return {
      categoryTreeData: null,
      categoryTreeProps: {
        label: 'name',
        children: 'children',
        disabled: '',
        isLeaf: 'isLeaf'
      },
      treeQuery: {
        sort: 'code',
        filter: undefined
      },
      filterText: ''
    }
  },
  watch: {
    filterText(val) {
      this.treeQuery.filter = val
      this.$refs.categoryTree.filter(val)
    }
  },
  created() {
    this.getCategories()
  },
  methods: {
    getCategories() {
      getCategoryTree(this.treeQuery).then(response => {
        this.categoryTreeData = response
        console.log('categoryTreeData', this.categoryTreeData)
      })
    },
    handleOrgClick(data) {
      this.categoryTreeNodeClick(data)
    },
    filterOrg(value, data) {
      if (!value) return true
      return data.displayName.indexOf(value) !== -1
    },
    checkChange(data, checked, indeterminate) {
      // 单个
      let keys = ''
      if (this.supportSingleChecked) {
        if (checked) {
          console.log(data, checked, indeterminate)
          keys = this.$refs.categoryTree.getCheckedKeys()
          if (keys.length > 1) {
            this.$refs.categoryTree.setCheckedKeys([])
            this.$refs.categoryTree.setChecked(data, true)
            keys = this.$refs.categoryTree.getCheckedKeys()
          }
          console.log('单个-keys:', keys)
          this.$emit('handleCheckChange', data, keys)
        }
      } else {
        keys = this.$refs.categoryTree.getCheckedKeys()
        console.log('多个-keys:', keys)
        this.$emit('handleCheckChange', data, keys)
      }
    }
  }
}
</script>
