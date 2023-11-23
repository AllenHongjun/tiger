<template>
  <div class="app-container documentation-container">
    <el-row>
      <tinymce />
    </el-row>
    <el-row>
      <a class="document-btn" target="_blank" href="https://learn.microsoft.com/zh-cn/dotnet/?WT.mc_id=dotnet-35129-website">.NET 文档</a>
      <a class="document-btn" target="_blank" href="https://learn.microsoft.com/zh-cn/aspnet/core/?view=aspnetcore-3.1">asp.net-core</a>
      <a class="document-btn" target="_blank" href="https://learn.microsoft.com/zh-cn/ef/">ef-core</a>
      <a class="document-btn" target="_blank" href="https://identityserver4docs.readthedocs.io/zh_CN/latest/intro/big_picture.html">identity-server</a>
      <a class="document-btn" target="_blank" href="https://docs.abp.io/zh-Hans/abp/3.2">abp</a>
      <a class="document-btn" target="_blank" href="https://learn.microsoft.com/zh-cn/sql/sql-server/sql-docs-navigation-guide?view=sql-server-2016">sql-server</a>
      <a class="document-btn" target="_blank" href="https://github.com/jasonhua95/awesome-dotnet-core">awesome-dotnet-core</a>
      <a class="document-btn" target="_blank" href="https://github.com/EasyAbp/awesome-abp">awesome-abp</a>
      <a class="document-btn" target="_blank" href="https://easyabp.io/">easyabp</a>
      <a class="document-btn" target="_blank" href="https://www.aliyun.com/resources?spm=5176.28055625.J_4VYgf18xNlTAyFFbOuOQe.11.e939154aSYsv0S">aliyun</a>
    </el-row>
    <el-row>
      <dropdown-menu class="document-btn" :items="articleList" title="系列文章" />
      <a class="document-btn" target="_blank" href="https://v2.cn.vuejs.org/">vue</a>

      <a class="document-btn" target="_blank" href="https://panjiachen.gitee.io/vue-element-admin-site/zh/">element-admin-doc</a>
      <a class="document-btn" target="_blank" href="https://v3.vuex.vuejs.org/zh/">vuex</a>
      <a class="document-btn" target="_blank" href="https://cli.vuejs.org/zh/">vue cli</a>
      <a class="document-btn" target="_blank" href="https://es6.ruanyifeng.com//">es2015+</a>
      <a class="document-btn" target="_blank" href="https://element.eleme.cn/#/zh-CN/component/installation">element-ui</a>
      <a class="document-btn" target="_blank" href="https://panjiachen.gitee.io/vue-element-admin-site/zh/">国内文档</a>
      <a class="document-btn" target="_blank" href="https://axios-http.com/zh/docs/intro">axios</a>

    </el-row>

    <el-row>
      <a class="document-btn" target="_blank" href="https://panjiachen.github.io/vue-element-admin/#/dashboard">element-admin</a>
      <a class="document-btn" target="_blank" href="https://commercial-demo.abp.io/">abp-demo</a>
      <a class="document-btn" target="_blank" href="http://dashboard.yudao.iocoder.cn/index">yudao</a>
      <a class="document-btn" target="_blank" href="https://demo.devqd.com/#/dashboard/home">admin.net</a>
      <a class="document-btn" target="_blank" href="https://github.com/colinin/abp-next-admin">abp-next-admin</a>
      <a class="document-btn" target="_blank" href="https://github.com/WilliamXu96/ABP-MicroService">abp-microservice</a>
      <a class="document-btn" target="_blank" href="http://116.205.177.85/dashboard/analysis">abp-pro</a>
      <a class="document-btn" target="_blank" href="http://v2.volcore.xyz/home">vol.vue</a>
      <a class="document-btn" target="_blank" href="http://vue.ruoyi.vip/index">ruoyi.vue</a>
    </el-row>
    <el-row />
  </div>
</template>

<script>
import DropdownMenu from '@/components/Share/DropdownMenu'
import tinymce from '../tinymce.vue'

export default {
  name: 'Documentation',
  components: {
    DropdownMenu,
    tinymce
  },
  data() {
    return {
      articleList: [{
        title: '基础篇',
        href: 'https://juejin.im/post/59097cd7a22b9d0065fb61d2'
      },
      {
        title: '登录权限篇',
        href: 'https://juejin.im/post/591aa14f570c35006961acac'
      },
      {
        title: '实战篇',
        href: 'https://juejin.im/post/593121aa0ce4630057f70d35'
      },
      {
        title: 'vue-admin-template 篇',
        href: 'https://juejin.im/post/595b4d776fb9a06bbe7dba56'
      },
      {
        title: 'v4.0 篇',
        href: 'https://juejin.im/post/5c92ff94f265da6128275a85'
      },
      {
        title: '自行封装 component',
        href: 'https://segmentfault.com/a/1190000009090836'
      },
      {
        title: '优雅的使用 icon',
        href: 'https://juejin.im/post/59bb864b5188257e7a427c09'
      },
      {
        title: 'webpack4（上）',
        href: 'https://juejin.im/post/59bb864b5188257e7a427c09'
      },
      {
        title: 'webpack4（下）',
        href: 'https://juejin.im/post/5b5d6d6f6fb9a04fea58aabc'
      }
      ]
    }
  }
}
</script>

<style lang="scss" scoped>
.documentation-container {
    margin: 50px;
    display: flex;
    flex-wrap: wrap;
    justify-content: space-evenly;

    .document-btn {
        flex-shrink: 0;
        display: block;
        cursor: pointer;
        background: black;
        color: white;
        height: 60px;
        width: 200px;
        margin-bottom: 16px;
        line-height: 60px;
        font-size: 20px;
        text-align: center;
    }
}
</style>
