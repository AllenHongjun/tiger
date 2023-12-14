<template>
  <div class="app-container">
    <train-platform-table ref="trainplatformTable" @handleCreate="handleCreate" @handleUpdate="handleUpdate" />
    <train-platform-model ref="trainplatformModel" @handleFilter="handleFilter" />
  </div>
</template>

<script>

import TrainPlatformTable from './components/TrainPlatformTable.vue'
import TrainPlatformModel from './components/TrainPlatformModel.vue'

export default {
  name: 'TrainPlatforms',
  components: {
    TrainPlatformTable,
    TrainPlatformModel
  },

  methods: {
    handleFilter(firstPage = true) {
      this.$refs['trainplatformTable'].handleFilter(firstPage)
    },
    handleCreate() {
      this.$refs['trainplatformModel'].handleCreate()
    },
    handleUpdate(row) {
      this.$refs['trainplatformModel'].handleUpdate(row)
    }
  }
}
</script>
