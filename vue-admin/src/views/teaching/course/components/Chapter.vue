<template>
  <div class="chapter-container">

    <el-card class="sub-chapter-box">
      <div slot="header" class="clearfix">
        <span>第一章</span>

        <el-button style="float: right; padding: 3px 5px" type="text" class="el-icon-arrow-down">下移</el-button>
        <el-button style="float: right; padding: 3px 5px" type="text" class="el-icon-delete">删除章节</el-button>
      </div>
      <div class="subChapterTable">
        <el-table :key="tableKey" :data="tableData" style="width: 100%" border fit highlight-current-row :stripe="true">
          <el-table-column prop="date" label="类型" width="180">
            <template slot-scope="{ row }">
              <span>12小时</span>
            </template>
          </el-table-column>
          <el-table-column prop="name" label="章节名称">
            <template slot-scope="{ row }">
              <span>这是一个课程的子章节</span>
            </template>
          </el-table-column>
          <el-table-column prop="address" label="章节学时" width="180">
            <template slot-scope="{ row }">
              <span>60(分钟)</span>
            </template>
          </el-table-column>
          <el-table-column :label="$t('AbpUi[\'Actions\']')" align="center" width="80">
            <template slot-scope="{ row, $index }">
              <el-button type="text" class="el-icon-edit" :title="$t('AbpUi[\'Edit\']')" @click="handleUpdate(row)" />
              <el-button type="text" class="el-icon-delete" :title="$t('AbpUi[\'Delete\']')" @click="handleDelete(row, $index)" />

            </template>
          </el-table-column>
        </el-table>
      </div>
      <el-button type="primary" class="el-icon-plus" plain @click="handleCreate()">添加子章节</el-button>
    </el-card>

    <el-button type="primary" class="el-icon-plus">创建课程章节</el-button>

    <sub-chapter-model ref="subChapterModel" />
  </div>
</template>

<script>
import SubChapterModel from './SubChapterModel.vue'
export default {
  name: 'Chapter',
  components: {
    SubChapterModel
  },
  data() {
    return {
      tableKey: undefined,
      tableData: [
        {
          date: '2016-05-02',
          name: '王小虎',
          address: '上海市普陀区金沙江路 1518 弄'
        }, {
          date: '2016-05-04',
          name: '王小虎',
          address: '上海市普陀区金沙江路 1518 弄'
        }, {
          date: '2016-05-01',
          name: '王小虎',
          address: '上海市普陀区金沙江路 1518 弄'
        }, {
          date: '2016-05-03',
          name: '王小虎',
          address: '上海市普陀区金沙江路 1518 弄'
        }
      ]
    }
  },
  methods: {
    handleCreate() {
      this.$refs['subChapterModel'].handleCreate()
    },
    handleUpdate(row) {
      this.$refs['subChapterModel'].handleUpdate()
    },
    // 删除
    handleDelete(row, index) {
      this.$confirm(
        // 消息
        this.$i18n.t("AbpUi['ItemWillBeDeletedMessageWithFormat']", [
          row.name
        ]),
        // title
        this.$i18n.t("AbpUi['AreYouSure']"), {
          confirmButtonText: this.$i18n.t("AbpUi['Yes']"), // 确认按钮
          cancelButtonText: this.$i18n.t("AbpUi['Cancel']"), // 取消按钮
          type: 'warning' // 弹框类型
        }
      ).then(async() => {
        // 回调函数
        // deleteLayout(row.id).then(() => {
        //   this.handleFilter(false)
        //   this.$notify({
        //     title: this.$i18n.t("TigerUi['Success']"),
        //     message: this.$i18n.t("TigerUi['SuccessMessage']"),
        //     type: 'success',
        //     duration: 2000
        //   })
        // })
      })
    }
  }
}
</script>

<style lang="scss" scoped>

.sub-chapter-box{
  margin: 20px auto;
}
.subChapterTable{
  margin: 20px auto;
}
.line{
  text-align: center;
}
</style>

