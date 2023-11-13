<template>
  <div class="app-container">
    <question-cateogry-table ref="questionCateogryTable" @handleCreate="handleCreate" @handleUpdate="handleUpdate" />
    <question-cateogy-model ref="questionCateogryModel" @handleFilter="handleFilter" />
  </div>
</template>

<script>

import QuestionCateogryTable from './components/QuestionCateogryTable.vue'
import QuestionCateogyModel from './components/QuestionCateogyModel.vue'

export default {
  name: 'QuestionCategorys',
  components: {
    QuestionCateogryTable,
    QuestionCateogyModel
  },
  data() {
    return {

    }
  },
  created() {

  },
  methods: {
    handleFilter(firstPage = true) {
      this.$refs['questionCateogryTable'].handleFilter(firstPage)
    },
    handleCreate() {
      this.$refs['questionCateogryModel'].handleCreate()
    },
    handleUpdate(row) {
      this.$refs['questionCateogryModel'].handleUpdate(row)
    }
  }
}
</script>
