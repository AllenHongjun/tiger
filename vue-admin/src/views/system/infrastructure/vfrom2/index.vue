<template>
  <div class="app-container">
    <v-form-designer style="width: 100%;margin-left:0;" />
  </div>
</template>

<script>
// import VForm from 'vform-builds' // 引入VForm库
// import countTo from 'vue-count-to'

// 需要全局引入组件 不然没有效果
export default {
  name: 'Vfrom',

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

<style lang="scss">
body {
  margin: 0;  /* 如果页面出现垂直滚动条，则加入此行CSS以消除之 */
}
</style>

