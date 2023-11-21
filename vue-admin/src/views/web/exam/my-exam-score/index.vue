<template>
  <div class="app-container">
    我的考试成绩(多评委成绩列表) 这是一个故意的空白页面
  </div>
</template>

<script>
export default {
  name: 'CN',
  data() {
    return {
      blank: {

      }
    }
  },
  methods: {
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

