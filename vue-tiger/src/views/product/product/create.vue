<template>
  <article-detail :is-edit="false" />
  <!-- <div class="app-container">
    <div>12313</div>
  </div> -->

</template>

<script>
import ArticleDetail from './components/ArticleDetail'

export default {
  name: 'CreateForm',
  components: { ArticleDetail }
}
</script>

