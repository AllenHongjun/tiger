<template>
  <div class="app-container">
    <school-table ref="schoolTable" @handleCreate="handleCreate" @handleUpdate="handleUpdate" />
    <school-model ref="schoolModel" @handleFilter="handleFilter" />
  </div>
</template>

<script>

import SchoolTable from './components/SchoolTable.vue'
import SchoolModel from './components/SchoolModel.vue'

export default {
  name: 'Schools',
  components: {
    SchoolTable,
    SchoolModel
  },

  methods: {
    handleFilter(firstPage = true) {
      this.$refs['schoolTable'].handleFilter(firstPage)
    },
    handleCreate() {
      this.$refs['schoolModel'].handleCreate()
    },
    handleUpdate(row) {
      this.$refs['schoolModel'].handleUpdate(row)
    }
  }
}
</script>
