<template>
  <div class="table-container">
    <el-dialog :title=" dialogStatus == 'create'? $t('AppExam[\'Exam:AddNew\']'): $t('AbpUi[\'Edit\']')" :visible.sync="dialogFormVisible" top="1vh" width="95%" height="95%">

      <el-tabs v-model="activeName" type="border-card">
        <el-tab-pane label="学习统计" name="first">
          <learn-analysis />
        </el-tab-pane>

      </el-tabs>

      <div slot="footer" class="dialog-footer">
        <el-button type="primary" @click="dialogFormVisible = false">
          {{ $t("AbpUi['Close']") }}
        </el-button>
      </div>
    </el-dialog>
  </div>
</template>

<script>
import LearnAnalysis from './LearnAnalysis.vue'
export default {
  name: 'ExamAnalysis',
  components: {
    LearnAnalysis
  },
  data() {
    return {
      blank: {

      },
      activeName: 'first',
      dialogFormVisible: false,
      dialogStatus: ''
    }
  },
  methods: {
    // 查看课程分析
    handleViewCourseAnalysis(row, activeName) {
      this.dialogStatus = 'update'
      this.dialogFormVisible = true
      this.activeName = activeName
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
::v-deep .el-dialog {
  margin: 0 auto 0px;
}
::v-deep .el-dialog__body{
  height: calc(80vh - 50px);
  overflow: auto;
}
</style>

