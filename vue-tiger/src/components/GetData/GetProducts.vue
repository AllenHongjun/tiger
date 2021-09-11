<template>
  <div class="board-column">

    <!-- 选择商品 -->
    <el-dialog title="选择商品" :visible.sync="visible" width="1150px" height="800px" fit :before-close="handleClose">
      <div class="filter-container">
        <el-input v-model="listQuery.Filter" placeholder="请输入名称" style="width: 200px;" class="filter-item" @keyup.enter.native="handleFilter" />

        <el-button v-waves class="filter-item" type="primary" icon="el-icon-search" @click="handleFilter">
          搜索
        </el-button>

      </div>
      <el-divider />

      <el-table ref="productTable" v-loading="listLoading" :data="list" highlight-current-row style="width: 100%" @sort-change="sortChange" @current-change="handleCurrentChange">
        <el-table-column align="center" type="selection" width="55" />

        <el-table-column width="120px" align="center" label="图片">
          <template slot-scope="scope">
            <span><img :src="Url.photoPrefix + scope.row.picture" width="40px"></span>
          </template>
        </el-table-column>

        <el-table-column width="80px" label="分类名称">
          <template slot-scope="{row}">
            <span>{{ row.categoryName }}</span>
          </template>
        </el-table-column>

        <el-table-column min-width="220px" label="商品名称">
          <template slot-scope="{row}">
            <router-link :to="'/basic/product/edit/'+row.id" class="link-type">
              <span>{{ row.name }}</span>
            </router-link>
          </template>
        </el-table-column>

        <el-table-column min-width="80px" label="商品货号">
          <template slot-scope="{row}">
            <span>{{ row.productSn }}</span>
          </template>
        </el-table-column>

        <el-table-column min-width="80px" label="商品原价">
          <template slot-scope="{row}">
            <span>{{ row.oriPrice }}</span>
          </template>
        </el-table-column>

        <el-table-column min-width="80px" label="商品售价">
          <template slot-scope="{row}">
            <span>{{ row.price }}</span>
          </template>
        </el-table-column>

        <el-table-column min-width="80px" label="商品采购价">
          <template slot-scope="{row}">
            <span>{{ row.purchasePrice }}</span>
          </template>
        </el-table-column>

        <!-- <el-table-column min-width="100px" label="销量">
          <template slot-scope="{row}">
            <span>{{ row.sale }}</span>
          </template>
        </el-table-column> -->

        <el-table-column min-width="100px" label="库存">
          <template slot-scope="{row}">
            <span>{{ row.stock }}</span>
          </template>
        </el-table-column>

        <!-- <el-table-column min-width="100px" label="排序">
          <template slot-scope="{row}">
            <span>{{ row.sort }}</span>
          </template>
        </el-table-column> -->

        <el-table-column width="180px" align="center" label="时间">
          <template slot-scope="scope">
            <span>{{ scope.row.creationTime | formatDate }}</span>
          </template>
        </el-table-column>
      </el-table>

      <pagination v-show="total>0" layout="prev, pager, next" :total="total" :page-size="10" primary :page.sync="listQuery.page" :limit.sync="listQuery.limit" @pagination="getList" />

      <div slot="footer" class="dialog-footer">
        <el-button @click="toggleSelection([list[1], list[2]])">切换第二、第三行的选中状态</el-button>
        <el-button type="primary" @click="close">
          关闭
        </el-button>
        <el-button type="success" @click="handleSelect">
          选中
        </el-button>
      </div>
    </el-dialog>
  </div>

</template>

<script>

import { getProducts } from '@/api/basic/product'
import Pagination from '@/components/Pagination' // secondary package based on el-pagination
import waves from '@/directive/waves' // waves directive
import { Url } from '@/utils/abp'

export default {
  name: 'GetProducts',
  components: {
    Pagination, Url
  },
  directives: { waves },
  props: {
    headerText: {
      type: String,
      default: 'Header'
    },
    product: {
      type: Object,
      default() {
        return {}
      }
    },

    visible: {
      type: Boolean,
      default: true
    }
  },
  data() {
    return {
      list: null,
      total: 0,
      listLoading: true,
      listQuery: {
        page: 1,
        limit: 10,
        Filter: ''
        // Sorting: 'name desc',
      },
      currentRow: null,
      Url
    }
  },
  created() {
    this.getList()
  },
  methods: {
    getList() {
      this.listLoading = true
      getProducts(this.listQuery).then(response => {
        this.list = response.items
        this.total = response.totalCount
        this.listLoading = false
        // 默认选中首行
        // this.setCurrent(this.list[0])
      })
    },
    handleFilter() {
      // this.searchDivVisibilty = false
      this.listQuery.page = 1
      this.getList()
    },
    // 排序
    sortChange(data) {
      const { prop, order } = data
      if (prop === 'id') {
        this.sortByID(order)
      }
    },
    sortByID(order) {
      if (order === 'ascending') {
        this.listQuery.sort = '+id'
      } else {
        this.listQuery.sort = '-id'
      }
      this.handleFilter()
    },
    handleClose(done) {
      this.$emit('closeGetProducts') // 通知父组件改变。
    },
    close() {
      // this.visible = false
      this.$emit('closeGetProducts') // 通知父组件改变。
    },
    toggleSelection(rows) {
      if (rows) {
        rows.forEach(row => {
          this.$refs.productTable.toggleRowSelection(row)
        })
      } else {
        this.$refs.productTable.clearSelection()
      }
    },
    handleSelect() {
      // console.log('this.$refs.productTable', this.$refs.productTable.selection)
      this.$emit('selectProduct', this.$refs.productTable.selection)
      this.$emit('closeGetProducts') // 通知父组件改变。
    },
    // 选中当前
    setCurrent(row) {
      console.log('row', row)
      this.$nextTick(() => {
        // this.$refs.productTable.toggleRowSelection(row, true)
        console.log(this.$refs)
        this.$refs.productTable.setCurrentRow(row)
      })
    },
    handleCurrentChange(val) {
      this.currentRow = val
    }
  }
}
</script>
<style lang="scss" scoped>
::v-deep .el-dialog__body{
  padding:5px 2px;
}
</style>

