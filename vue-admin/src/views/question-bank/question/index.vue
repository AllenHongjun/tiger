<template>
  <div class="app-container">
    <question-table ref="questionTable" @handleCreate="handleCreate" @handleUpdate="handleUpdate" />
    <question-model ref="questionModel" @handleFilter="handleFilter" />
  </div>
</template>

<script>

import QuestionTable from './components/QuestionTable.vue'
import QuestionModel from './components/QuestionModel.vue'

export default {
  name: 'Question',
  components: {
    QuestionTable,
    QuestionModel
  },
  data() {
    return {

    }
  },
  created() {

  },
  methods: {
    handleFilter(firstPage = true) {
      this.$refs['questionTable'].handleFilter(firstPage)
    },
    handleCreate() {
      this.$refs['questionModel'].handleCreate()
    },
    handleUpdate(row) {
      this.$refs['questionModel'].handleUpdate(row)
    }
  }
}
</script>
