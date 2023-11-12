<template>
  <div class="app-container">
    <layout-table ref="layoutTable" @handleCreate="handleCreate" @handleUpdate="handleUpdate" />
    <layout-model ref="layoutModel" @handleFilter="handleFilter" />
  </div>
</template>

<script>

import LayoutTable from './components/LayoutTable.vue'
import LayoutModel from './components/LayoutModel.vue'

export default {
  name: 'Layouts',
  components: {
    LayoutTable,
    LayoutModel
  },

  methods: {
    handleFilter(firstPage = true) {
      this.$refs['layoutTable'].handleFilter(firstPage)
    },
    handleCreate() {
      this.$refs['layoutModel'].handleCreate()
    },
    handleUpdate(row) {
      this.$refs['layoutModel'].handleUpdate(row)
    }
  }
}
</script>
