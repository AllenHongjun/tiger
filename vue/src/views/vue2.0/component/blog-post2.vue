<template>
<div class="blog-post">
    <h3>{{ title }}</h3>
    <p>{{ content }}</p>
    <p>{{ likes }}</p>
    <p>{{ isPublished }}</p>
    <p>{{ commentIds}}</p>
    <p>{{ author }}</p>
    <p> {{ contactsPromise }}</p>

    <el-divider></el-divider>

</div>
</template>

<script>
export default {
    // 定义一个 BlogPost的组件
    name: 'BlogPost',

    // Prop 是你可以在组件上注册的一些自定义 attribute。当一个值传递给一个 prop attribute 的时候，它就变成了那个组件实例的一个 property
    // 由父组件向子组件传值
    // camelCase (驼峰命名法) 的 prop 名需要使用其等价的 kebab-case (短
    // props: ['post'],

    // 希望每个 prop 都有指定的值类型。这时，你可以以对象形式列出 prop，这些 property 的名称和值分别是 prop 各自的名称和类型
    props: {
        title: String,
        content: String,
        likes: Number,
        isPublished: Boolean,
        commentIds: Array,
        author: Object,
        callback: Function,
        contactsPromise: Promise // or any other constructor
    },

    // props: ['title', 'likes', 'isPublished', 'commentIds', 'author'],

    // 一个组件的 data 选项必须是一个函数
    data() {
        return {
            count: 0
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
.line {
    text-align: center;
}
</style>
