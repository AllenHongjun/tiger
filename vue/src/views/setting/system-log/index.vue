<template>
<div class="app-container">
    <div>
        <!-- <FilenameOption v-model="filename" />
      <AutoWidthOption v-model="autoWidth" />
      <BookTypeOption v-model="bookType" /> -->
        <router-link :to="'/user/create'">
            <el-button type="primary" size="small" icon="el-icon-edit">
                添加
            </el-button>
        </router-link>

    </div>
    <el-table v-loading="listLoading" :data="list" element-loading-text="Loading" border fit highlight-current-row>
        <el-table-column align="center" label="ID" width="95">
            <template slot-scope="scope">
                {{ scope.$index }}
            </template>
        </el-table-column>
        <el-table-column label="名称">
            <template slot-scope="scope">
                {{ scope.row.name }}
            </template>
        </el-table-column>
        <el-table-column label="用户名">
            <template slot-scope="scope">
                {{ scope.row.userName }}
            </template>
        </el-table-column>
        <el-table-column label="手机号" width="120">
            <template slot-scope="scope">
                {{ scope.row.phoneNumber }}
            </template>
        </el-table-column>
        <el-table-column label="邮箱" width="180" align="center">
            <template slot-scope="scope">
                {{ scope.row.email }}
            </template>
        </el-table-column>
        <!-- <el-table-column class-name="status-col" label="是否删除" width="110" align="center">
        <template slot-scope="scope">
          <el-tag :type="scope.row.status | statusFilter">{{ scope.row.status }}</el-tag>
        </template>
      </el-table-column> -->
        <el-table-column align="center" prop="created_at" label="创建日期" width="200">
            <template slot-scope="scope">
                <i class="el-icon-time" />
                <span>{{ scope.row.creationTime }}</span>
            </template>
        </el-table-column>
        <el-table-column align="center" prop="created_at" label="修改日期" width="200">
            <template slot-scope="scope">
                <i class="el-icon-time" />
                <span>{{ scope.row.lastModificationTime }}</span>
            </template>
        </el-table-column>
        <el-table-column align="center" label="操作" width="300">
            <template slot-scope="scope">
                <router-link :to="'/user/edit/'+scope.row.id">
                    <el-button type="info" size="small" icon="el-icon-edit" plain>
                    </el-button>
                </router-link>

                <el-button type="danger" size="small" icon="el-icon-delete" plain>

                </el-button>
            </template>
        </el-table-column>
    </el-table>
</div>
</template>

<script>
import {
    getList
} from '@/api/user'
// import { parseTime } from '@/utils'

export default {
    filters: {
        statusFilter(status) {
            const statusMap = {
                published: 'success',
                draft: 'gray',
                deleted: 'danger'
            }
            return statusMap[status]
        }
    },
    data() {
        return {
            list: null,
            listLoading: true
        }
    },
    created() {
        this.fetchData()
    },
    methods: {
        fetchData() {
            this.listLoading = true
            getList().then(response => {
                console.log(response)
                this.list = response.items
                this.listLoading = false
            })
        }
    }
}
</script>
