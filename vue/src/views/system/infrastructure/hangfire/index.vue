<template>
  <div class="app-container">
    <embed type="text/x-scriptlet" src="https://localhost:44306/hangfire/" class="inline-page" width="100%" style="min-height: 860px;">
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

