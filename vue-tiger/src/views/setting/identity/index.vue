<template>
  <div class="identity">
    <el-alert :closable="false" >
      <router-view />
    </el-alert>
  </div>
</template>

<style scoped>
.identity {
  /*50 = navbar  */
  min-height: calc(100vh - 50px);
  width: 100%;
  position: relative;
  overflow: hidden;
}
/* .fixed-header+.app-main {
  padding-top: 50px;
} */
</style>


