<template>
  <div class="layout">
    <router-view />
  </div>
  <!-- docker本地运行环境 docker run -it -p 3000:3000 --name allinone rocksolidknowledge/identityserver-dev-sso -->
</template>

<style scoped>
.layout {
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
