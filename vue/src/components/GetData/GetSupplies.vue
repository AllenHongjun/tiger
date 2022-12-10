<template>
  <div class="board-column">

    <!-- 选择供应商 -->
    <el-dialog title="选择供应商" :visible.sync="visible" width="1150px" height="800px" style="padding:0px;" fit :before-close="handleClose">
      <div class="filter-container">
        <el-input v-model="listQuery.Filter" placeholder="请输入名称" style="width: 200px;" class="filter-item" @keyup.enter.native="handleFilter" />

        <el-button-group>
          <el-button v-waves class="filter-item" size="mini" type="primary" icon="el-icon-search" @click="handleFilter">
            搜索
          </el-button>
        <!-- <el-button size="mini" type="primary" icon="el-icon-arrow-down" @click="handleSearch" /> -->
        </el-button-group>

      </div>

      <el-table ref="supplyTable" v-loading="listLoading" :data="list" highlight-current-row style="width: 100%" @sort-change="sortChange" @current-change="handleCurrentChange">
        <!-- <el-table-column align="center" type="selection" width="55" /> -->

        <el-table-column min-width="300px" label="名称">
          <template slot-scope="{row}">
            <span>{{ row.name }}</span>
          </template>
        </el-table-column>

        <el-table-column width="120px" align="center" label="联系人">
          <template slot-scope="scope">
            <span>{{ scope.row.attentionTo }}</span>
          </template>
        </el-table-column>

        <el-table-column width="320px" align="center" label="地址">
          <template slot-scope="scope">
            <span>{{ scope.row.province }}-{{ scope.row.city }}-{{ scope.row.reginon }}-{{ scope.row.address }}</span>
          </template>
        </el-table-column>

        <el-table-column width="120px" align="center" label="联系人手机">
          <template slot-scope="scope">
            <span>{{ scope.row.phone }}</span>
          </template>
        </el-table-column>

        <el-table-column width="180px" align="center" label="创建时间" sortable>
          <template slot-scope="scope">
            <span>{{ scope.row.creationTime | formatDate }}</span>
          </template>
        </el-table-column>

      </el-table>

      <pagination v-show="total>0" layout="prev, pager, next" :total="total" :page-size="10" primary :page.sync="listQuery.page" :limit.sync="listQuery.limit" @pagination="getList" />

      <div slot="footer" class="dialog-footer">
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

import { getSupplies } from '@/api/basic/supply'
import Pagination from '@/components/Pagination' // secondary package based on el-pagination
import waves from '@/directive/waves' // waves directive

export default {
  name: 'GetSupplies',
  components: {
    Pagination
  },
  directives: { waves },
  props: {
    headerText: {
      type: String,
      default: 'Header'
    },
    supply: {
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
      currentRow: null
    }
  },
  created() {
    this.getList()
  },
  methods: {
    getList() {
      this.listLoading = true
      getSupplies(this.listQuery).then(response => {
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
      this.$emit('closeGetSupplies') // 通知父组件改变。
    },
    close() {
      // this.visible = false
      this.$emit('closeGetSupplies') // 通知父组件改变。
    },
    handleSelect() {
      // console.log('this.currentRow', this.currentRow)
      this.$emit('selectSupply', this.currentRow)
      this.$emit('closeGetSupplies') // 通知父组件改变。
    },
    // 选中当前
    setCurrent(row) {
      console.log('row', row)
      this.$nextTick(() => {
        // this.$refs.multipleTable.toggleRowSelection(row, true)
        console.log(this.$refs)
        this.$refs.supplyTable.setCurrentRow(row)
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

