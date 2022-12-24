<template>
<div class="app-container documentation-container">
    <el-row>
        <a class="document-btn" target="_blank" href="https://learn.microsoft.com/zh-cn/aspnet/core/getting-started/?view=aspnetcore-3.1&tabs=windows">asp.net core</a>
        <a class="document-btn" target="_blank" href="https://learn.microsoft.com/zh-cn/ef/core/">ef core</a>
        <a class="document-btn" target="_blank" href="https://identityserver4docs.readthedocs.io/zh_CN/latest/intro/big_picture.html">identity server</a>
        <a class="document-btn" target="_blank" href="https://docs.abp.io/zh-Hans/abp/latest/">abp</a>
        <a class="document-btn" target="_blank" href="https://github.com/EasyAbp/awesome-abp">awesome-abp</a>
        <a class="document-btn" target="_blank" href="https://github.com/EasyAbp/EasyAbpGuide">EasyAbpGuide</a>
        <a class="document-btn" target="_blank" href="https://github.com/abpframework/abp-samples">abp-samples</a>
        <a class="document-btn" target="_blank" href="https://github.com/EasyAbp/AbpHelper.GUI">AbpHelper.GUI</a>
    </el-row>

    <el-row>
        <a class="document-btn" target="_blank" href="https://v2.cn.vuejs.org/">vue</a>
        <a class="document-btn" target="_blank" href="https://v3.vuex.vuejs.org/zh/">vuex</a>
        <a class="document-btn" target="_blank" href="https://element.eleme.cn/#/zh-CN/component/installation">element-ui</a>
        <a class="document-btn" target="_blank" href="https://axios-http.com/zh/docs/intro">axios</a>
        <a class="document-btn" target="_blank" href="https://panjiachen.github.io/vue-element-admin-site/">element-admin-site</a>
        <a class="document-btn" target="_blank" href="https://panjiachen.github.io/vue-element-admin/#/dashboard">element-admin</a>
        <a class="document-btn" target="_blank" href="https://panjiachen.gitee.io/vue-element-admin-site/zh/">国内文档</a>
        <dropdown-menu class="document-btn" :items="articleList" title="系列文章" />
    </el-row>
    <el-row>
    </el-row>

</div>
</template>

<script>
import DropdownMenu from '@/components/Share/DropdownMenu'

export default {
    name: 'Documentation',
    components: {
        DropdownMenu
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
