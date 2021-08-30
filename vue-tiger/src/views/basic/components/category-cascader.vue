<template>
  <div class="app-container" style="padding:0 0px;display:inline-block;">

    <el-cascader
      ref="categoryCascader"
      v-model="value"
      clearable
      :options="categoryTreeData"
      :props="categoryTreeProps"
      @change="checkChange"
    />
  </div>
</template>

<script>
import {
  getCategoryTree
} from '@/api/basic/category'
export default {
  name: 'CategoryCascader',

  props: {
    // categoryCascaderNodeClick: {
    //   type: Function,
    //   default: () => {}
    // },
    parentId: {
      type: String,
      default: ''
    }
  },

  data() {
    return {
      value: null,
      categoryTreeData: null,
      categoryTreeProps: {
        label: 'name',
        value: 'id',
        children: 'children',
        disabled: '',
        leaf: 'isLeaf',
        checkStrictly: true
      },
      treeQuery: {
        sort: 'code',
        filter: undefined
      },
      filterText: ''

    }
  },

  created() {
    this.getCategories()
    // console.log('parentId', this.parentId)
    // this.value = this.parentId
  },
  methods: {
    getCategories() {
      getCategoryTree(this.treeQuery).then(response => {
        this.categoryTreeData = response
        // console.log('categoryTreeData', this.categoryTreeData)
      })
    },
    handleChange(value) {
      console.log(value)
    },
    handleCategoryClick(value) {
      // console.log('node-value', value)
      // console.log('parentId', this.parentId)
      this.categoryCascaderNodeClick(value)
    },
    checkChange() {
      // console.log('this.parentId', this.props.parentId)

      var parentId = null
      if (this.$refs.categoryCascader.getCheckedNodes()[0]) {
        parentId = this.$refs.categoryCascader.getCheckedNodes()[0].value
      }
      console.log('parentId', parentId)
      this.$emit('handleCheckChange', parentId)
    }

  }
}
</script>
