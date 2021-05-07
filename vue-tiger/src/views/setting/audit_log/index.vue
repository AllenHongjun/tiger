<template>
  <div class="app-container">
    <el-row style="margin-bottom: 20px">
      <!-- <FilenameOption v-model="filename" />
      <AutoWidthOption v-model="autoWidth" />
      <BookTypeOption v-model="bookType" /> -->
      <router-link :to="'/setting/role/create'">
        <el-button type="primary" size="small" icon="el-icon-edit">
          搜索
        </el-button>
      </router-link>
    </el-row>
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
