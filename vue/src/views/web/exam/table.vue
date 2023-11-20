<template>
  <div class="app-container">
    <el-header class="navbar" style="height:80px;">
      <el-menu
        :default-active="activeIndex2"
        class="left-menu"
        mode="horizontal"
        background-color="#409EFF"
        text-color="#fff"
        active-text-color="#ffd"
        @select="handleSelect"
      >
        <el-menu-item index="1">练习</el-menu-item>
        <el-menu-item index="2">考试</el-menu-item>
        <el-menu-item index="3">课程</el-menu-item>
        <el-menu-item index="4">成绩</el-menu-item>
        <el-menu-item index="5">积分</el-menu-item>
        <el-menu-item index="6">实训平台</el-menu-item>
        <el-menu-item index="7"><a href="#" target="_blank">学习资料</a></el-menu-item>
        <div class="right-menu">
          <el-dropdown class="avatar-container" trigger="click">
            <div class="avatar-wrapper">
              <img src="https://th.bing.com/th/id/OIP.oYETPWlwurA42fNBfjENPwHaEo?w=381&h=197&c=7&r=0&o=5&pid=1.7" class="user-avatar">
              <i class="el-icon-caret-bottom" />
            </div>
            <el-dropdown-menu slot="dropdown" class="user-dropdown">
              <router-link to="/profile/index">
                <el-dropdown-item>
                  我的账号
                </el-dropdown-item>
              </router-link>
              <el-dropdown-item divided>
                <span style="display:block;">注 销</span>
              </el-dropdown-item>
            </el-dropdown-menu>
          </el-dropdown>
        </div>
      </el-menu>

    </el-header>
    <el-main>
      <div class="filter-container" style="margin-bottom:10px;">
        <el-form ref="logQueryForm" label-position="left" label-width="80px" :model="listQuery">
          <el-row :gutter="20">
            <el-col :span="4">
              <el-form-item prop="filter" :label="$t('AbpUi[\'Search\']')">
                <el-input v-model="listQuery.filter" :placeholder="$t('AbpUi[\'PlaceholderInput\']')" clearable />
              </el-form-item>
            </el-col>

            <el-col :span="8">
              <el-form-item :label="$t('AbpUi[\'DisplayName:CreationTime\']')">
                <el-date-picker
                  v-model="queryCreateDateTime"
                  value-format="yyyy-MM-dd HH:mm:ss"
                  :default-time="['00:00:00', '23:59:59']"
                  type="datetimerange"
                  align="right"
                  unlink-panels
                  :picker-options="pickerOptions"
                  range-separator="-"
                  :start-placeholder="$t('AbpUi[\'StartTime\']')"
                  :end-placeholder="$t('AbpUi[\'EndTime\']')"
                  @change="datePickerChange"
                />
              </el-form-item>
            </el-col>

            <el-col :span="4">
              <el-button-group>
                <el-button type="primary" class="filter-item" icon="el-icon-search" @click="handleFilter">
                  {{ $t('AbpUi.Search') }}
                </el-button>
                <el-button type="reset" icon="el-icon-remove-outline" @click="resetQueryForm">
                  {{ $t('AbpAuditLogging.Reset') }}
                </el-button>

              </el-button-group>
            </el-col>
          </el-row>

        </el-form>
      </div>
    </el-main>

    <div class="card-container">
      <el-card v-for="o in 4" :key="o" class="box-card">
        <el-row>
          <el-col :span="22">
            <h3>考试名称</h3>
            <div>
              <el-tag type="success">进行中</el-tag>
              <span> 考试时间：11-13 21:51 至 11-20 21:51</span> |
              <span> 答题时间：60分钟 </span> |
              <span> 限考 <el-tag type="info">12</el-tag> 次 </span>
            </div>
          </el-col>
          <el-col :span="2">
            <el-button type="primary" class="take-exam">参加考试</el-button>
          </el-col>
        </el-row>
        <div class="text item" />
      </el-card>

      <pagination v-show="total > 0" :total="total" :page.sync="listQuery.page" :limit.sync="listQuery.limit" />
    </div>

  </div>
</template>

<script>
import {
  mapGetters
} from 'vuex'
import Pagination from '@/components/Pagination' // secondary package based on el-pagination
import { pickerRangeWithHotKey } from '@/utils/picker'
import baseListQuery, { checkPermission } from '@/utils/abp'

export default {
  name: 'CN',
  components: {
    Pagination
  },
  data() {
    return {
      blank: {

      },
      activeIndex2: 1,
      queryCreateDateTime: undefined,
      currentPage4: 4,
      pickerOptions: pickerRangeWithHotKey,
      tableKey: 0,
      list: null,
      total: 150,
      listLoading: true,
      listQuery: Object.assign({
        createStartTime: undefined,
        createEndTime: undefined
      }, baseListQuery)
    }
  },
  computed: {
    ...mapGetters([
      'sidebar',
      'avatar',
      'device'
    ])
  },
  methods: {
    handleSelect(key, keyPath) {
      console.log(key, keyPath)
    },
    handleClick(tab, event) {
      console.log(tab, event)
    },
    datePickerChange(value) {
      if (!value) {
        // 日期选择器改变事件 ~ 解决日期选择器清空 值不清空的问题
        this.listQuery.createStartTime = undefined
        this.listQuery.createEndTime = undefined
      }
    },
    // 搜索展开切换
    toggleAdvanced() {
      this.advanced = !this.advanced
    },
    // 刷新页面
    handleRefresh() {
      this.handleFilter()
    },
    // 重置查询参数
    resetQueryForm() {
      this.queryCreateDateTime = undefined
      this.listQuery = Object.assign({
        degree: undefined,
        questionCategoryId: undefined,
        createStartTime: undefined,
        createEndTime: undefined,
        type: undefined,
        enable: undefined
      }, baseListQuery)
    },
    // 获取列表数据
    getList() {
      this.listLoading = true
      if (this.queryCreateDateTime) {
        this.listQuery.createStartTime = this.queryCreateDateTime[0]
        this.listQuery.createEndTime = this.queryCreateDateTime[1]
      }
    },
    handleFilter(firstPage = true) {
      if (firstPage) this.listQuery.page = 1
      this.getList()
    },
    sortChange(data) {
      const {
        prop,
        order
      } = data
      this.listQuery.sort = order ? `${prop} ${order}` : undefined
      this.handleFilter()
    },
    handleSizeChange(val) {
      console.log(`每页 ${val} 条`)
    },
    handleCurrentChange(val) {
      console.log(`当前页: ${val}`)
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

<style lang="scss" scoped>
.el-header, .el-footer {
  background-color: #FFFFFF;
  color: #333;
  text-align: center;
  padding: 0px;
}

.el-aside {
  background-color: #FFFFFF;
  color: #333;
  text-align: center;
  padding: 0px;
}

.el-main {
  background-color: #FFFFFF;
  color: #333;
  text-align: center;
}

body > .el-container {
  margin-bottom: 40px;
}

.navbar{
  height: 80px;
  position: relative;
  background: #fff;
  box-shadow: 0 1px 4px rgba(0,21,41,.08);

  .left-menu{
    width: 100%;
  }
  .right-menu {
    float: right;
    height: 100%;
    line-height: 50px;

    &:focus {
      outline: none;
    }

    .right-menu-item {
      display: inline-block;
      padding: 0 8px;
      height: 100%;
      font-size: 18px;
      color: #5a5e66;
      vertical-align: text-bottom;

      &.hover-effect {
        cursor: pointer;
        transition: background .3s;

        &:hover {
          background: rgba(0, 0, 0, .025)
        }
      }
    }

    .avatar-container {
      margin-right: 30px;

      .avatar-wrapper {
        margin-top: 5px;
        position: relative;

        .user-avatar {
          cursor: pointer;
          width: 40px;
          height: 40px;
          border-radius: 10px;
        }

        .el-icon-caret-bottom {
          cursor: pointer;
          position: absolute;
          right: -20px;
          top: 25px;
          font-size: 12px;
        }
      }
    }
  }
}

.el-image {
  width: 256px;
  line-height: 256px;
  margin-right: 15px;
}

.box-card {
  height: 120px;
  margin-bottom: 20px;
  font-size: 12px;
  .take-exam{
    width: 150px;
    height: 55px;
    font-size: 16px;
    vertical-align: middle;
  }
}
</style>

