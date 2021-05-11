<template>
  <div class="app-container">
    <div class="filter-container" style="margin-bottom: 20px">
      <el-input
        v-model="listQuery.userName"
        placeholder="用户名"
        style="width: 150px"
        class="filter-item"
      />

      <el-button
        size="small"
        class="filter-item"
        type="primary"
        icon="el-icon-search"
        @click="handleFilter"
      >
        搜索
      </el-button>

      <el-button
        type="primary"
        size="small"
        icon="el-icon-plus"
        @click="handleCreate"
      >
        添加
      </el-button>
    </div>

    <el-table
      v-loading="listLoading"
      :data="list"
      element-loading-text="Loading"
      border
      fit
      highlight-current-row
    >
      <!-- <el-table-column align="center" label="ID" width="95">
        <template slot-scope="scope">
          {{ scope.$index }}
        </template>
      </el-table-column> -->
      <!-- <el-table-column label="名称">
        <template slot-scope="scope">
          {{ scope.row.name }}
        </template>
      </el-table-column> -->
      <el-table-column label="用户名">
        <template slot-scope="scope">
          {{ scope.row.userName }}
        </template>
      </el-table-column>
      <el-table-column label="手机号">
        <template slot-scope="scope">
          {{ scope.row.phoneNumber }}
        </template>
      </el-table-column>
      <el-table-column label="邮箱" align="center">
        <template slot-scope="scope">
          {{ scope.row.email }}
        </template>
      </el-table-column>

      <el-table-column align="right" label="操作" width="300">
        <template slot-scope="scope">
          <el-dropdown size="small" split-button type="primary">
            操作
            <el-dropdown-menu slot="dropdown">
              <el-dropdown-item @click="handleUpdate(scope.row)"
                >编辑</el-dropdown-item
              >
              <el-dropdown-item>锁定</el-dropdown-item>
              <el-dropdown-item>权限</el-dropdown-item>
              <el-dropdown-item>设置密码</el-dropdown-item>
              <el-dropdown-item @click="deleteData(scope.row.id)"
                >删除</el-dropdown-item
              >
            </el-dropdown-menu>
          </el-dropdown>
        </template>
      </el-table-column>
    </el-table>
    <pagination
      v-show="total > 0"
      :total="total"
      :page.sync="listQuery.page"
      :limit.sync="listQuery.limit"
      @pagination="fetchData"
    />

    <el-dialog :title="textMap[dialogStatus]" :visible.sync="dialogFormVisible">
      <el-form
        ref="dataForm"
        :rules="rules"
        :model="temp"
        label-width="180px"
        label-position="left"
      >
        <el-tabs v-model="activeName">
          <el-tab-pane label="用户信息" name="first">
            <el-form-item label="用户名" prop="userName">
              <el-input v-model="temp.userName" />
            </el-form-item>
            <el-form-item
              v-if="dialogStatus === 'create'"
              label="密码"
              prop="concurrencyStamp"
            >
              <el-input v-model="temp.concurrencyStamp" />
            </el-form-item>
            <el-form-item label="邮箱" prop="email">
              <el-input v-model="temp.email" />
            </el-form-item>
            <el-form-item label="手机号" prop="phoneNumber">
              <el-input v-model="temp.phoneNumber" />
            </el-form-item>
          </el-tab-pane>
          <el-tab-pane label="角色" name="second">角色</el-tab-pane>
          <el-tab-pane label="组织机构" name="third">组织机构</el-tab-pane>
        </el-tabs>

        <!-- <el-form-item label="使用共享数据库" prop="title">
          <el-checkbox v-model="checked"></el-checkbox>
        </el-form-item> -->
      </el-form>
      <div style="text-align: right">
        <el-button type="danger" @click="dialogFormVisible = false"
          >取消</el-button
        >
        <el-button
          type="primary"
          @click="dialogStatus === 'create' ? createData() : updateData()"
          >确认</el-button
        >
      </div>
    </el-dialog>
  </div>
</template>

<script>
import { getUserList } from "@/api/user";
// import { parseTime } from '@/utils'
import Pagination from "@/components/Pagination"; // Secondary package based on el-pagination
import {validEmail, validPhone} from "@/utils/validate";
export default {
  name: "User",
  components: { Pagination },
  filters: {
    statusFilter(status) {
      const statusMap = {
        published: "success",
        draft: "gray",
        deleted: "danger",
      };
      return statusMap[status];
    },
  },
  data() {
    var checkPhone = (rule, value, callback) =>  {
          if (!value) {
            return callback(new Error('电话号码不能为空'))
          }
          setTimeout(() => {
            // Number.isInteger是es6验证数字是否为整数的方法,但是我实际用的时候输入的数字总是识别成字符串
            // 所以我就在前面加了一个+实现隐式转换
            if (!Number.isInteger(+value)) {
              callback(new Error('请输入数字值'))
            } else {
              if (validPhone(value)) {
                callback()
              } else {
                callback(new Error('电话号码格式/长度不正确'))
              }
            }
          }, 100)
      };
    var checkEmail = (rule, value, callback) => {
      if (!value) {
        return callback(new Error('邮箱不能为空'))
      }
      setTimeout(() => {
        if (validEmail(value)) {
          callback()
        } else {
          callback(new Error('请输入正确的邮箱格式'))
        }
      }, 100)
    };

    return {
      list: null,
      listLoading: true,
      total: 0,
      listQuery: {
        userName: "",
        page: 1,
        limit: 10,
        Sorting: "",
      },
      activeName:"first",
      temp: {
        id: "",
        userName: "",
        concurrencyStamp:"",
        name:"",
        surname:"",
        email: "",
        lockoutEnabled: "",
        phoneNumber:"",
      },
      dialogFormVisible: false,
      dialogStatus: "",
      textMap: {
        update: "编辑",
        create: "添加",
      },
      rules: {
        userName: [
          { required: true, message: "请输入名称", trigger: "blur" },
          { min: 3, max: 15, message: '长度在 3 到 15 个字符', trigger: 'blur' }
          ],
        email: [
          { required: true,  trigger: "blur" ,validator: checkEmail},
        ],
        phoneNumber: [
          { required: true,  trigger: "blur" ,validator: checkPhone},
        ],
        concurrencyStamp: [
          { required: false, message: "请输入密码", trigger: "blur" },
        ],
      },
    };
  },
  created() {
    this.fetchData();
  },
  methods: {

    fetchData() {
      this.listLoading = true;
      getUserList(this.listQuery).then((response) => {
        this.list = response.items;
        this.total = response.totalCount;
        this.listLoading = false;
      });
    },
    handleFilter() {},
    resetTemp() {
      this.temp = {
        name: "",
        adminEmailAddress: "",
        adminPassword: "",
      };
    },
    handleCreate() {
      this.resetTemp();
      this.dialogStatus = "create";
      this.dialogFormVisible = true;
      this.$nextTick(() => {
        this.$refs["dataForm"].clearValidate();
      });
    },
    createData() {
      this.$refs["dataForm"].validate((valid) => {
        if (valid) {
          createTenant(this.temp).then(() => {
            this.list.unshift(this.temp);
            this.dialogFormVisible = false;
            this.$notify({
              title: "成功",
              message: "租户添加成功",
              type: "success",
              duration: 2000,
            });
          });
        }
      });
    },
    handleUpdate(row) {
      this.temp = Object.assign({}, row); // copy obj
      // this.temp.timestamp = new Date(this.temp.timestamp)
      this.dialogStatus = "update";
      this.dialogFormVisible = true;
      this.$nextTick(() => {
        this.$refs["dataForm"].clearValidate();
      });
    },
    updateData() {
      this.$refs["dataForm"].validate((valid) => {
        if (valid) {
          const tempData = Object.assign({}, this.temp);
          // console.log(tempData)
          // tempData.timestamp = +new Date(tempData.timestamp) // change Thu Nov 30 2017 16:41:05 GMT+0800 (CST) to 1512031311464
          updateTenant(tempData.id, tempData).then(() => {
            const index = this.list.findIndex((v) => v.id === this.temp.id);
            this.list.splice(index, 1, this.temp);
            this.dialogFormVisible = false;
            this.$notify({
              title: "成功",
              message: "修改成功",
              type: "success",
              duration: 2000,
            });
          });
        }
      });
    },
    deleteData(id) {
      console.log("delete");
      this.$confirm("此操作将永久删除数据, 是否继续?", "提示", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning",
      })
        .then(() => {
          deleteTenant(id)
            .then((response) => {
              const index = this.list.findIndex((v) => v.id === id);
              this.list.splice(index, 1);
              this.$message({
                message: "删除成功",
                type: "success",
              });
            })
            .catch((err) => {
              console.log(err);
            });
        })
        .catch(() => {
          this.$message({
            type: "info",
            message: "已取消删除",
          });
        });
    }
  },
};
</script>
