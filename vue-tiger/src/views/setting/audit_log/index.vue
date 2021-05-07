<template>
  <div class="app-container">
    <div class="filter-container">
      <el-date-picker
      v-model="value2"
      type="datetimerange"
      :picker-options="pickerOptions"
      range-separator="至"
      start-placeholder="开始日期"
      end-placeholder="结束日期"
      align="right">
    </el-date-picker>
      <!-- <el-input v-model="listQuery.title" placeholder="Title" style="width: 200px;" class="filter-item" @keyup.enter.native="handleFilter" />
      <el-select v-model="listQuery.importance" placeholder="Imp" clearable style="width: 90px" class="filter-item">
        <el-option v-for="item in importanceOptions" :key="item" :label="item" :value="item" />
      </el-select>
      <el-select v-model="listQuery.type" placeholder="Type" clearable class="filter-item" style="width: 130px">
        <el-option v-for="item in calendarTypeOptions" :key="item.key" :label="item.display_name+'('+item.key+')'" :value="item.key" />
      </el-select>
      <el-select v-model="listQuery.sort" style="width: 140px" class="filter-item" @change="handleFilter">
        <el-option v-for="item in sortOptions" :key="item.key" :label="item.label" :value="item.key" />
      </el-select> -->
      <!-- <el-button v-waves class="filter-item" type="primary" icon="el-icon-search" @click="handleFilter">
        Search
      </el-button>
      <el-button class="filter-item" style="margin-left: 10px;" type="primary" icon="el-icon-edit" @click="handleCreate">
        Add
      </el-button>
      <el-button v-waves :loading="downloadLoading" class="filter-item" type="primary" icon="el-icon-download" @click="handleDownload">
        Export
      </el-button>
      <el-checkbox v-model="showReviewer" class="filter-item" style="margin-left:15px;" @change="tableKey=tableKey+1">
        reviewer
      </el-checkbox> -->
    </div>
    <el-table
      v-loading="listLoading"
      :data="list"
      element-loading-text="Loading"
      border
      fit
      highlight-current-row
    >
      <el-table-column align="center" label="HTTP请求" width="300">
        <template slot-scope="scope">
          {{ scope.row.httpStatusCode + scope.row.httpMethod + scope.row.url }}
        </template>
      </el-table-column>
      <el-table-column label="用户" align="center" width="80">
        <template slot-scope="scope">
          {{ scope.row.userName }}
        </template>
      </el-table-column>
      <el-table-column label="IP地址" align="center" width="100">
        <template slot-scope="scope">
          {{ scope.row.clientIpAddress }}
        </template>
      </el-table-column>
      <el-table-column label="时间" align="center" width="150">
        <template slot-scope="scope">
          {{ scope.row.executionTime }}
        </template>
      </el-table-column>
      <el-table-column label="持续时间" align="center" width="100">
        <template slot-scope="scope">
          {{ scope.row.executionDuration }}
        </template>
      </el-table-column>
      <el-table-column label="应用名称" align="center" width="150">
        <template slot-scope="scope">
          {{ scope.row.clientId }}
        </template>
      </el-table-column>
      <el-table-column label="浏览器信息" align="center" >
        <template slot-scope="scope">
          {{ scope.row.browserInfo }}
        </template>
      </el-table-column>
      <!-- <el-table-column label="是否默认" align="center" width="95">
        <template slot-scope="scope">
          {{ scope.row.isDefault == true ? "是" : "否" }}
        </template>
      </el-table-column>
      <el-table-column label="是否发布" align="center" width="95">
        <template slot-scope="scope">
          {{ scope.row.isPublish == true ? "是" : "否" }}
        </template>
      </el-table-column>
      <el-table-column label="是否静态" align="center" width="95">
        <template slot-scope="scope">
          {{ scope.row.isStatic == true ? "是" : "否" }}
        </template>
      </el-table-column> -->

      <!-- <el-table-column align="center" label="操作">
        <template slot-scope="scope">
          <router-link :to="'/setting/role/edit/' + scope.row.id">
            <el-button type="info" size="small" icon="el-icon-edit" plain>
            </el-button>
          </router-link>
          <el-button type="danger" size="small"  icon="el-icon-delete" @click="deleteData(scope.row.id)" plain>
          </el-button>
        </template>
      </el-table-column> -->
    </el-table>

    <pagination
      v-show="total > 0"
      :total="total"
      :page.sync="listQuery.page"
      :limit.sync="listQuery.limit"
      @pagination="fetchData"
    />
  </div>
</template>

<script>
import { getAuditLogList } from "@/api/user";
import Pagination from "@/components/Pagination"; // Secondary package based on el-pagination

export default {
  name: "audit_log_list",
  components: { Pagination },
  data() {
    return {
      list: null,
      listLoading: true,
      total: 0,
      listQuery: {
        page: 1,
        limit: 10,
        // SkipCount: 0,
        // MaxResultCount: 10,
        Sorting: "",
      },
      pickerOptions: {
          shortcuts: [{
            text: '最近一周',
            onClick(picker) {
              const end = new Date();
              const start = new Date();
              start.setTime(start.getTime() - 3600 * 1000 * 24 * 7);
              picker.$emit('pick', [start, end]);
            }
          }, {
            text: '最近一个月',
            onClick(picker) {
              const end = new Date();
              const start = new Date();
              start.setTime(start.getTime() - 3600 * 1000 * 24 * 30);
              picker.$emit('pick', [start, end]);
            }
          }, {
            text: '最近三个月',
            onClick(picker) {
              const end = new Date();
              const start = new Date();
              start.setTime(start.getTime() - 3600 * 1000 * 24 * 90);
              picker.$emit('pick', [start, end]);
            }
          }]
        },
        value1: [new Date(2000, 10, 10, 10, 10), new Date(2000, 10, 11, 10, 10)],
        value2: ''
    };
  },
  created() {
    this.fetchData();
  },
  methods: {
    handleSizeChange(val) {
      console.log(`每页 ${val} 条`);
      this.listQuery.SkipCount =
        (this.listQuery.page - 1) * this.listQuery.MaxResultCount;
    },
    fetchData() {
      this.listLoading = true;
      console.log(this.listQuery);
      getAuditLogList(this.listQuery).then((response) => {
        this.list = response.items;
        this.total = response.totalCount;
        this.listLoading = false;
      });
    },
    deleteData(id) {
      console.log("delete");
      deleteRole(id)
        .then((response) => {
          this.$message({
            message: "删除成功",
            type: "success",
          });
        })
        .catch((err) => {
          console.log(err);
        });
    },
  },
};
</script>
