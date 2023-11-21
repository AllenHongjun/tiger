<template>
  <div class="app-container">
    <!-- 组织选择关联多个角色 -->
    <el-dialog :title="$t('AbpIdentity[\'OrganizationUnit:SelectUsers\']')" :visible.sync="dialogFormVisible" top="7vh">
      <el-row style="margin-bottom: 20px">
        <el-input v-model="listQuery.filter" :placeholder="$t('AbpUi[\'PagerSearch\']')" clearable style="width: 150px" class="filter-item" />
        <el-button class="filter-item" type="primary" icon="el-icon-search" @click="handleFilter" />
      </el-row>
      <el-table
        ref="userSelectTable"
        :key="tableKey"
        v-loading="listLoading"
        :data="list"
        border
        fit
        highlight-current-row
        style="width: 100%;"
        :row-key="getRowKeys"
        @sort-change="sortChange"
      >
        <el-table-column type="selection" width="55" :reserve-selection="true" />
        <el-table-column :label="$t('AbpIdentity[\'UserName\']')" prop="name" sortable align="left">
          <template slot-scope="{ row }">
            <span>{{ row.userName }}</span>
            <el-tag>{{ row.surname + row.name }}</el-tag>
          </template>
        </el-table-column>

        <el-table-column :label="$t('AbpIdentity[\'EmailAddress\']')" prop="name" sortable align="left">
          <template slot-scope="{ row }">
            <span>{{ row.email }}</span>
          </template>
        </el-table-column>
      </el-table>
      <pagination v-show="total > 0" :total="total" :page.sync="listQuery.page" :limit.sync="listQuery.limit" @pagination="getOrgUserList" />
      <div slot="footer" class="dialog-footer">
        <el-button @click="dialogFormVisible = false">
          {{ $t("AbpIdentity['Cancel']") }}
        </el-button>
        <el-button type="primary" @click="addUsers()">
          {{ $t("AbpIdentity['Save']") }}
        </el-button>
      </div>
    </el-dialog>

  </div>
</template>

<script>
import Pagination from '@/components/Pagination'
import {
  getOrgUsers,
  getUnAddedUsers,
  AddUsers
} from '@/api/system-manage/identity/organization-unit'

var row

export default {
  name: 'AddUsers',
  components: {
    Pagination
  },
  props: {
    ouId: {
      type: String,
      default: ''
    },
    refreshParentUsers: {
      type: Function,
      default: null
    },
    // 当前已经选中的用户
    curSelectedUsers: {
      type: Array,
      default: null
    }
  },
  data() {
    return {
      tableKey: 0,
      orgData: undefined,
      list: null,
      total: 0,
      multipleSelection: [], // // 用来保存当前的选中
      listLoading: true,
      listQuery: {
        page: 1,
        limit: 10,
        sort: undefined,
        filter: undefined,
        ouId: undefined
      },
      dialogFormVisible: false,
      dialogStatus: ''
    }
  },
  computed: {
    // 根据当前的multipleSelection得到对应选中的id
    curSelectedRowIds() {
      let result = []
      if (this.multipleSelection && this.multipleSelection.length > 0) {
        result = this.multipleSelection.map((user) => user.id)
      }
      return result
    }
  },
  // 监听table的显示或者隐藏，初始化multipleSelection的值
  watch: {
    showDialog: {
      handler(newValue) {
        if (newValue) {
          this.multipleSelection = this.$_.cloneDeep(this.curSelectedUsers)
          this.rowMultipleChecked()
        } else {
          this.clearSelection()
        }
      },
      immediate: true,
      deep: true
    }
  },
  created() {
    this.getOrgUserList()
  },
  methods: {
    getOrgUserList() {
      this.listLoading = true
      getUnAddedUsers(this.ouId, this.listQuery).then(response => {
        this.list = response.items
        this.total = response.totalCount
        this.listLoading = false
      })
    },
    sortChange(data) {
      const {
        prop,
        order
      } = data
      this.listQuery.sort = order ? `${prop} ${order}` : undefined
      this.handleFilter()
    },
    handleFilter(firstPage = true) {
      if (firstPage) this.listQuery.page = 1
      this.getOrgUserList()
    },
    handleRefresh() {
      this.listQuery.ouId = undefined
      this.$refs.userOrgTree.$refs.orgTree.setCurrentKey(null)
      this.orgData = null
      this.handleFilter()
    },
    handleAddUsers(orgData) {
      // 每次重新打开都要请求新的组织接口数据
      this.dialogFormVisible = true
      this.orgData = orgData
      this.getOrgUserList()
    },
    addUsers() {
      var userIds = []
      this.$refs.userSelectTable.selection.forEach((value, index) => {
        userIds.push(value.id)
      })
      AddUsers(this.orgData.id, {
        'userIds': userIds
      }).then(() => {
        // 调用父类刷新的方法 通过父组件传入 prop方法
        this.refreshParentUsers()
        this.dialogFormVisible = false
        // 清空选中
        this.clearSelection()
        this.$notify({
          title: this.$i18n.t("TigerUi['Success']"),
          message: this.$i18n.t("TigerUi['SuccessMessage']"),
          type: 'success',
          duration: 2000
        })
      })
    },
    getRowKeys(row) {
      // 在使用 reserve-selection 功能与显示树形数据时，该属性是必填的。类型为 String 时，支持多层访问：user.info.id，但不支持 user.info[0].id，此种情况请使用 Function。
      return row.id
    },
    // 监听selection-change获得跨页选中的行的数据
    /**
     * @param selection 选中的rows
     * @param changedRow 变化的row
     */
    handleSelectionChange(selection, changedRow) {
      debugger
      // 检查有没有新增的，有新增的就push
      if (selection && selection.length > 0) {
        selection.forEach((row) => {
          if (this.curSelectedRowIds.indexOf(row.id) < 0) {
            this.multipleSelection.push(row)
          }
        })
      }
      // 如果当前的selection没有changedRow，表示changedRow被cancel了，
      // 如果this.multipleSelection有这一条，需要splice掉
      if (row && selection.indexOf(changedRow) < 0) {
        if (this.curSelectedRowIds.indexOf(changedRow.id) > -1) {
          for (let index = 0; index < this.multipleSelection.length; index++) {
            if (row.id === this.multipleSelection[index].id) {
              this.multipleSelection.splice(index, 1)
              break
            }
          }
        }
      }
      // 如果当前一条都没有选中，表示都没有选中，则需要把当前页面的rows都遍历一下，splice掉没选中的
      if (selection.length === 0) {
        this.list.forEach((row) => {
          const index = this.curSelectedRowIds.indexOf(row.id)
          if (index > -1) {
            this.multipleSelection.splice(index, 1)
          }
        })
      }
    },
    // 每次分页查询的时候触发 来把已经选中的check一下
    rowMultipleChecked() {
      if (
        this.curSelectedRowIds &&
                this.curSelectedRowIds.length > 0 &&
                this.list &&
                this.list.length > 0
      ) {
        this.$nextTick(() => {
          // 触发一下选中
          this.list.forEach((row) => {
            if (this.curSelectedRowIds.indexOf(row.id) > -1) {
              this.$refs['userSelectTable'].toggleRowSelection(row, true)
            }
          })
        })
      }
    },
    // clearSelection的实现
    clearSelection() {
      if (this.$refs['userSelectTable']) {
        this.$refs['userSelectTable'].clearSelection()
      }
    }
  }
}
</script>

<style lang="scss" scoped>
.line {
    text-align: center;
}

.el-tag{
  margin-left: 8px;
}

.pagination-container{
  padding: 5 0;
}
</style>
