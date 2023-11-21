<template>
  <div class="app-container">
    <test-paper-table ref="testpaperTable" @handleCreate="handleCreate" @handleUpdate="handleUpdate" />
    <test-paper-model ref="testpaperModel" @handleFilter="handleFilter" />
  </div>
</template>

<script>

import TestPaperTable from './components/TestPaperTable.vue'
import TestPaperModel from './components/TestPaperModel.vue'

export default {
  name: 'TestPapers',
  components: {
    TestPaperTable,
    TestPaperModel
  },

  methods: {
    handleFilter(firstPage = true) {
      this.$refs['testpaperTable'].handleFilter(firstPage)
    },
    handleCreate() {
      this.$refs['testpaperModel'].handleCreate()
    },
    handleUpdate(row) {
      this.$refs['testpaperModel'].handleUpdate(row)
    }
  }
}
</script>
