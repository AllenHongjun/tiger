<template>
  <div class="page">
    <router-view />
  </div>
</template>

<style scoped>
.page {
  /*50 = navbar  */
  min-height: calc(100vh - 85px);
  width: 100%;
  position: relative;
  overflow: hidden;
}
/* .fixed-header+.app-main {
  padding-top: 50px;
} */
</style>
