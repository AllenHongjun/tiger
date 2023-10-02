<template>
  <el-table :data="list" style="width: 100%;">
    <el-table-column :label="$t('AbpSaas[\'TenantName\']')" min-width="120">
      <template slot-scope="scope">
        {{ scope.row.name }}
      </template>
    </el-table-column>
    <el-table-column :label="$t('AbpSaas[\'DisplayName:EditionName\']')" width="200" align="center">
      <template slot-scope="scope">
        {{ scope.row.editionName }}
      </template>
    </el-table-column>
    <el-table-column :label="$t('TigerUi[\'DisplayName:CreationTime\']')" width="120" align="center">
      <template slot-scope="{row}">
        <el-tag type="primary ">
          {{ row.creationTime | moment('YYYY-MM-DD') }}
        </el-tag>
      </template>
    </el-table-column>
  </el-table>
</template>

<script>
import { getTenants } from '@/api/sass/tenant'
import baseListQuery from '@/utils/abp'

export default {
  name: 'TenantTable',
  filters: {
    statusFilter(status) {
      const statusMap = {
        success: 'success',
        pending: 'danger'
      }
      return statusMap[status]
    },
    orderNoFilter(str) {
      return str.substring(0, 30)
    }
  },
  data() {
    return {
      list: null,
      listQuery: baseListQuery
    }
  },
  created() {
    this.fetchData()
  },
  methods: {
    fetchData() {
      var req = {
        page: 1,
        limit: 10,
        sort: 'creationTime desc'
      }
      getTenants(req).then(response => {
        console.log('response', response)
        this.list = response.items.slice(0, 8)
      })
    }
  }
}
</script>
