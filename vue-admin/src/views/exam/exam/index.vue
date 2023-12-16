<template>
  <div class="app-container">
    <exam-table ref="examTable" @handleCreate="handleCreate" @handleUpdate="handleUpdate" />
  </div>
</template>

<script>

import ExamTable from './components/ExamTable.vue'

export default {
  name: 'Exams',
  components: {
    ExamTable
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
