<template>
  <div class="app-container">

    <el-container>
      <!-- <el-aside width="200px">Aside</el-aside> -->
      <el-container>
        <el-header>
          <el-menu
            :default-active="activeIndex2"
            class="el-menu-demo"
            mode="horizontal"
            background-color="#545c64"
            text-color="#fff"
            active-text-color="#ffd04b"
            @select="handleSelect"
          >
            <el-menu-item index="1">练习任务</el-menu-item>
            <!-- <el-submenu index="2">
              <template slot="title">我的工作台</template>
              <el-menu-item index="2-1">选项1</el-menu-item>
              <el-menu-item index="2-2">选项2</el-menu-item>
              <el-menu-item index="2-3">选项3</el-menu-item>
              <el-submenu index="2-4">
                <template slot="title">选项4</template>
                <el-menu-item index="2-4-1">选项1</el-menu-item>
                <el-menu-item index="2-4-2">选项2</el-menu-item>
                <el-menu-item index="2-4-3">选项3</el-menu-item>
              </el-submenu>
            </el-submenu> -->
            <el-menu-item index="1">考试任务</el-menu-item>
            <el-menu-item index="3" disabled>实训平台</el-menu-item>
            <el-menu-item index="4"><a href="https://www.ele.me" target="_blank">学习资料</a></el-menu-item>
          </el-menu>
        </el-header>

        <el-main>
          <el-row>
            <el-col :span="20">
              <el-tabs v-model="activeName" @tab-click="handleClick">
                <el-tab-pane label="全部练习" name="first">
                  <div class="">
                    <div class="block">
                      <el-image :src="src" />
                      <el-image :src="src" />
                      <el-image :src="src" />
                      <el-image :src="src" />
                      <el-image :src="src" />
                      <el-image :src="src" />
                      <el-image :src="src" />
                      <el-image :src="src" />
                      <el-image :src="src" />
                      <el-image :src="src" />
                      <el-image :src="src" />
                      <el-image :src="src" />
                      <el-image :src="src" />
                      <el-image :src="src" />
                      <el-image :src="src" />
                      <el-image :src="src" />
                      <el-image :src="src" />
                      <el-image :src="src" />
                      <el-image :src="src" />
                      <el-image :src="src" />
                    </div>
                    <el-pagination
                      :current-page="currentPage4"
                      :page-sizes="[100, 200, 300, 400]"
                      :page-size="100"
                      layout="total, sizes, prev, pager, next, jumper"
                      :total="400"
                      @size-change="handleSizeChange"
                      @current-change="handleCurrentChange"
                    />
                  </div>
                </el-tab-pane>
                <el-tab-pane label="理论练习" name="second">理论练习</el-tab-pane>
                <el-tab-pane label="实操练习" name="third">实操练习
                </el-tab-pane>
                <el-tab-pane label="模拟考试练习" name="fourth">模拟考试练习</el-tab-pane>
              </el-tabs>
            </el-col>
            <el-col :span="4">
              <el-button type="primary" round>查看已练习试卷</el-button>
            </el-col>

          </el-row>
        </el-main>
        <el-footer>
          <p>E-Mail：service@srzx.com 浙ICP备18007420号-2   浙公网安备 33070202100263号</p>
        </el-footer>
      </el-container>
    </el-container>
  </div>
</template>

<script>
export default {
  name: 'CN',
  data() {
    return {
      blank: {

      },
      activeIndex2: '1',
      activeName: 'first',
      src: 'https://cube.elemecdn.com/6/94/4d3ea53c084bad6931a56d5158a48jpeg.jpeg',
      currentPage1: 5,
      currentPage2: 5,
      currentPage3: 5,
      currentPage4: 4

    }
  },
  methods: {
    handleSelect(key, keyPath) {
      console.log(key, keyPath)
    },
    handleClick(tab, event) {
      console.log(tab, event)
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

<style scoped>

.el-header, .el-footer {
  background-color: #B3C0D1;
  color: #333;
  text-align: center;
  line-height: 60px;
}

.el-aside {
  background-color: #D3DCE6;
  color: #333;
  text-align: center;
  line-height: 200px;
}

.el-main {
  background-color: #E9EEF3;
  color: #333;
  text-align: center;
}

body > .el-container {
  margin-bottom: 40px;
}

.el-container:nth-child(5) .el-aside,
.el-container:nth-child(6) .el-aside {
  line-height: 260px;
}

.el-container:nth-child(7) .el-aside {
  line-height: 320px;
}

.el-image {
  width: 256px;
  margin-right: 15px;
}
</style>

