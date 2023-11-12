<template>
  <div class="app-container">
    <exam-table ref="examTable" @handleCreate="handleCreate" @handleUpdate="handleUpdate" />
    <exam-model ref="examModel" @handleFilter="handleFilter" />
  </div>
</template>

<script>

import ExamTable from './components/ExamTable.vue'
import ExamModel from './components/ExamModel.vue'

export default {
  name: 'Exams',
  components: {
    ExamTable,
    ExamModel
  },

  methods: {
    handleFilter(firstPage = true) {
      this.$refs['examTable'].handleFilter(firstPage)
    },
    handleCreate() {
      this.$refs['examModel'].handleCreate()
    },
    handleUpdate(row) {
      this.$refs['examModel'].handleUpdate(row)
    }
  }
}
</script>
