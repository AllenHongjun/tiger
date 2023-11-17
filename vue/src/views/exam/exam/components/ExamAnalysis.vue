<template>
  <div class="table-container">
    <el-dialog :title=" dialogStatus == 'create'? $t('AppExam[\'Exam:AddNew\']'): $t('AbpUi[\'Edit\']')" :visible.sync="dialogFormVisible" top="1vh" width="95%" height="95%">

      <el-tabs v-model="activeName" type="border-card">
        <el-tab-pane label="考试成绩" name="first">
          <exam-score />
        </el-tab-pane>
        <el-tab-pane label="人工评卷" name="second">
          <exam-judge />
        </el-tab-pane>
        <el-tab-pane label="成绩统计" name="third">
          <exam-score-analysis />
        </el-tab-pane>
        <el-tab-pane label="答题统计" name="fourth">
          <question-analysis />
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
import ExamScore from './ExamScore.vue'
import ExamJudge from './ExamJudge.vue'
import ExamScoreAnalysis from './ExamScoreAnalysis.vue'
import QuestionAnalysis from './QuestionAnalysis.vue'
export default {
  name: 'ExamAnalysis',
  components: {
    ExamScore,
    ExamJudge,
    ExamScoreAnalysis,
    QuestionAnalysis
  },
  data() {
    return {
      blank: {

      },
      activeName: 'first',
      dialogFormVisible: true,
      dialogStatus: ''
    }
  },
  methods: {
    // 更新按钮点击
    handleViewExamAnalysis(row, activeName) {
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
.line{
  text-align: center;
}
</style>

