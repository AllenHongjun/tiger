<template>
  <div class="app-container">
    <el-row style="margin-bottom:20px">
      <!-- <FilenameOption v-model="filename" />
      <AutoWidthOption v-model="autoWidth" />
      <BookTypeOption v-model="bookType" /> -->
      <router-link :to="'/setting/role/create'">
        <el-button type="primary" size="small" icon="el-icon-edit">
          添加
        </el-button>
      </router-link>
    </el-row>
    <el-table
      v-loading="listLoading"
      :data="list"
      element-loading-text="Loading"
      border
      fit
      highlight-current-row
    >
      <el-table-column align="center" label="ID" width="95">
        <template slot-scope="scope">
          {{ scope.$index }}
        </template>
      </el-table-column>
      <el-table-column label="名称" align="center" width="150">
        <template slot-scope="scope">
          {{ scope.row.name }}
        </template>
      </el-table-column>
      <el-table-column label="是否默认" align="center" width="95">
        <template slot-scope="scope">
          {{ scope.row.isDefault == true? '是':'否' }}
        </template>
      </el-table-column>
      <el-table-column label="是否发布" align="center" width="95">
        <template slot-scope="scope">
          {{ scope.row.isPublish == true? '是':'否' }}
        </template>
      </el-table-column>
      <el-table-column label="是否静态" align="center" width="95">
        <template slot-scope="scope">
          {{ scope.row.isStatic == true? '是':'否' }}
        </template>
      </el-table-column>

      <el-table-column align="center" label="操作">
        <template slot-scope="scope">
          <router-link :to="'/setting/role/edit/'+scope.row.id">
            <el-button type="info" size="small"  icon="el-icon-edit" plain>
            </el-button>
          </router-link>
          <el-button type="danger" size="small"  icon="el-icon-delete" plain>
          </el-button>
        </template>
      </el-table-column>
    </el-table>
  </div>
</template>

<script>
import { getRoleList } from '@/api/user'

export default {
  data() {
    return {
      list: null,
      listLoading: true,
      total: 0,
      listQuery: {
        // page: 1,
        // limit: 20
        SkipCount: 0,
        // MaxResultCount: 1,
        Sorting: 'name desc'
      }
    }
  },
  created() {
    this.fetchData()
  },
  methods: {
    fetchData() {
      this.listLoading = true
      getRoleList(this.listQuery).then(response => {
        this.list = response.items
        this.total = response.totalCount
        this.listLoading = false
      })
    }
  }
}
</script>
