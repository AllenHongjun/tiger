<template>
  <div id="components-demo">
    <!-- 定义和使用一个组件 -->
    <button-counter />
    <button-counter />
    <button-counter />

    <!-- 为每一篇博客渲染一个组件 -->
    <!-- 父级组件可以像处理 native DOM 事件一样通过 v-on 监听子组件实例的任意事件 -->
    <!-- <blog-post v-for="post in posts" v-bind:key="post.id" v-bind:post="post" v-on:enlarge-text="postFontSize += $event"></blog-post> -->
    <!-- <div id="blog-posts-events-demo">
        <div :style="{ fontSize: postFontSize + 'em' }">

            <blog-post v-for="post in posts" v-bind:key="post.id" v-bind:post="post" v-on:enlarge-text="onEnlargeText"></blog-post>
        </div>
    </div> -->

    <div id="blog-post-props-example">
      <blog-post title="My journey with Vue" />

      <!-- 动态赋予一个变量的值 -->
      <blog-post :title="post.title" />

      <!-- 动态赋予一个复杂表达式的值 -->
      <blog-post :title="post.title + ' by ' + post.author.name" />

      <!-- 即便 `42` 是静态的，我们仍然需要 `v-bind` 来告诉 Vue -->
      <!-- 这是一个 JavaScript 表达式而不是一个字符串。-->
      <blog-post :likes="42" />

      <!-- 用一个变量进行动态赋值。-->
      <blog-post :likes="post.likes" />

      <!-- 包含该 prop 没有值的情况在内，都意味着 `true`。-->
      <blog-post is-published />

      <!-- 即便 `false` 是静态的，我们仍然需要 `v-bind` 来告诉 Vue -->
      <!-- 这是一个 JavaScript 表达式而不是一个字符串。-->
      <blog-post :is-published="false" />

      <!-- 用一个变量进行动态赋值。-->
      <blog-post :is-published="post.isPublished" />

      <!-- 即便对象是静态的，我们仍然需要 `v-bind` 来告诉 Vue -->
      <!-- 这是一个 JavaScript 表达式而不是一个字符串。-->
      <blog-post
        :author="{
          name: 'Veronica',
          company: 'Veridian Dynamics'
        }"
      />

      <!-- 用一个变量进行动态赋值。-->
      <blog-post :author="post.author" />
    </div>
  </div>
</template>

<script>
import ButtonCounter from './component/button-counter'
// import BlogPost from './component/blog-post.vue'
import BlogPost from './component/blog-post2.vue'

export default {
  // 定义一个 button-counter的组件
  name: 'ComponentBegin',
  components: {
    ButtonCounter,
    BlogPost
  },
  data() {
    return {
      count: 0,
      post: {
        id: 2,
        title: 'Blogging with Vue',
        likes: 43,
        author: {
          name: 'Jack&rolling'
        }
      },
      posts: [{
        id: 1,
        title: 'My journey with Vue'
      },
      {
        id: 2,
        title: 'Blogging with Vue'
      },
      {
        id: 3,
        title: 'Why Vue is so fun'
      }
      ],
      postFontSize: 1 // 控制放大博文的字号
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
    },

    // 给事件绑定一个方法
    onEnlargeText: function(enlargeAmount) {
      this.postFontSize += enlargeAmount
    }
  }
}
</script>

<style scoped>
.line {
    text-align: center;
}
</style>
