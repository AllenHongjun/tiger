<template>
<div class="app-container">
    <div class="filter-container" style="margin-bottom: 20px">

        <el-date-picker v-model="queryDateTime" value-format="yyyy-MM-dd hh:mm:ss" type="datetimerange" :picker-options="pickerOptions" range-separator="至" start-placeholder="开始日期" end-placeholder="结束日期" align="right" size="mini" />

        <el-input v-model="listQuery.userName" placeholder="用户名" style="width: 150px" class="filter-item" size="mini" />
        <el-input v-model="listQuery.url" placeholder="URL过滤" style="width: 150px" class="filter-item" size="mini" />
        <el-select v-model="listQuery.httpMethod" placeholder="HTTP方法" clearable style="width: 110px" class="filter-item" size="mini">
            <el-option v-for="item in httpMethodOptions" :key="item" :label="item" :value="item" />
        </el-select>

        <el-select v-model="listQuery.hasException" placeholder="是否存在异常" clearable style="width: 110px" class="filter-item" size="mini">
            <el-option v-for="item in hasExceptionOptions" :key="item.value" :label="item.label" :value="item.value" />
        </el-select>
        <el-button class="filter-item" type="primary" icon="el-icon-search" @click="handleFilter">
            搜索
        </el-button>
        <el-button :loading="downloadLoading" class="filter-item" type="primary" icon="el-icon-download" @click="handleDownload">
            导出
        </el-button>

    </div>
    <el-table v-loading="listLoading" :data="list" element-loading-text="Loading" border fit highlight-current-row :default-sort="{prop: 'date', order: 'descending'}" @sort-change="sortChange">
        <el-table-column align="left" label="HTTP请求">
            <template slot-scope="scope">
                <el-tag :type="scope.row.httpStatusCode | httpCodeFilter">
                    {{ scope.row.httpStatusCode }}
                </el-tag>
                <el-tag :type="scope.row.httpStatusCode | httpCodeFilter">
                    {{ scope.row.httpMethod }}
                </el-tag>
                {{ scope.row.url }}
            </template>
        </el-table-column>
        <el-table-column label="用户" align="center" width="80">
            <template slot-scope="scope">
                {{ scope.row.userName }}
            </template>
        </el-table-column>
        <el-table-column label="IP地址" align="center" width="100">
            <template slot-scope="scope">
                {{ scope.row.clientIpAddress }}
            </template>
        </el-table-column>
        <el-table-column label="时间" prop="executionTime" align="center" width="140" sortable="custom">
            <template slot-scope="scope">
                {{ scope.row.executionTime | formatDate('YYYY-MM-DD HH:mm:ss') }}
            </template>
        </el-table-column>
        <el-table-column label="持续时间(s)" align="center" width="100">
            <template slot-scope="scope">
                {{ scope.row.executionDuration }}
            </template>
        </el-table-column>
        <el-table-column label="应用名称" align="center" width="120">
            <template slot-scope="scope">
                {{ scope.row.clientId }}
            </template>
        </el-table-column>

        <el-table-column align="center" label="操作" width="240">
            <template slot-scope="scope">
                <el-button type="primary" size="mini" icon="el-icon-edit" @click="handleUpdate(scope.row)">
                    详情
                </el-button>
                &nbsp;&nbsp;

            </template>
        </el-table-column>
    </el-table>
    <pagination v-show="total > 0" :total="total" :page.sync="listQuery.page" :limit.sync="listQuery.limit" @pagination="fetchData" />

    <el-dialog title="日志明细" :visible.sync="dialogRoleFormVisible">
        <el-form ref="dataForm" :model="temp" label-width="180px" label-position="left">

            <el-form-item label="浏览器信息" prop="browserInfo">
                <div>{{ temp.browserInfo }}</div>
            </el-form-item>

            <el-form-item v-if="temp.exceptions != ''" label="异常信息" prop="exceptions">
                <el-input v-model="temp.exceptions" type="textarea" :rows="8" />
            </el-form-item>

            <el-form-item label="id" prop="id">
                <div>{{ temp.id }}</div>
            </el-form-item>
            <el-form-item label="url地址" prop="url">
                <div>{{ temp.url }}</div>
            </el-form-item>
            <el-form-item label="用户id" prop="userId">
                <div>{{ temp.userId }}</div>
            </el-form-item>
            <el-form-item label="用户名" prop="userName">
                <div>{{ temp.userName }}</div>
            </el-form-item>

        </el-form>
        <div style="text-align: right">

            <el-button type="primary" @click="dialogRoleFormVisible = false">关闭</el-button>
        </div>
    </el-dialog>

</div>
</template>

<script>
import {
    getAuditLogs
} from '@/api/auditlogging/auditlog'
import Pagination from '@/components/Pagination' // Secondary package based on el-pagination
import baseListQuery from '@/utils/abp'
import {
    parseTime
} from '@/utils'

export default {
    name: 'AuditLogList',
    components: {
        Pagination
    },
    filters: {
        httpCodeFilter(code) {
            code = parseInt(code)
            var result = ''
            if (code <= 100) {
                result = 'info'
            } else if (code <= 200) {
                result = 'success'
            } else if (code <= 300) {
                result = 'warning'
            } else {
                result = 'danger'
            }
            return result
        },
        typeFilter(type) {
            return calendarTypeKeyValue[type]
        }
    },
    data() {
        return {
            list: null,
            listLoading: true,
            downloadLoading: false,
            filename: 'auto-log',
            autoWidth: true,
            bookType: 'xlsx', // 'csv' ''
            total: 0,
            queryDateTime: undefined,
            listQuery: Object.assign({
                startTime: undefined,
                endTime: undefined,
                httpMethod: undefined,
                url: undefined,
                userName: undefined,
                tenantName: undefined,
                applicationName: undefined,
                hasException: '',
                httpStatusCode: undefined,
                Sorting: ''
            }, baseListQuery),
            httpMethodOptions: [
                'GET',
                'POST',
                'DELETE',
                'PUT',
                'HEAD',
                'CONNECT',
                'OPTIONS',
                'TRACE'
            ],
            hasExceptionOptions: [{
                    value: '',
                    label: '全部异常'
                },
                {
                    value: 'False',
                    label: '否'
                }, {
                    value: 'True',
                    label: '是'
                }
            ],
            pickerOptions: {
                shortcuts: [{
                        text: '最近一周',
                        onClick(picker) {
                            const end = new Date()
                            const start = new Date()
                            start.setTime(start.getTime() - 3600 * 1000 * 24 * 7)
                            picker.$emit('pick', [start, end])
                        }
                    },
                    {
                        text: '最近一个月',
                        onClick(picker) {
                            const end = new Date()
                            const start = new Date()
                            start.setTime(start.getTime() - 3600 * 1000 * 24 * 30)
                            picker.$emit('pick', [start, end])
                        }
                    },
                    {
                        text: '最近三个月',
                        onClick(picker) {
                            const end = new Date()
                            const start = new Date()
                            start.setTime(start.getTime() - 3600 * 1000 * 24 * 90)
                            picker.$emit('pick', [start, end])
                        }
                    }
                ]
            },
            value1: [new Date(2000, 10, 10, 10, 10), new Date(2000, 10, 11, 10, 10)],
            value2: '',
            dialogStatus: '',
            dialogRoleFormVisible: false,
            temp: {
                httpStatusCode: 204,
                comments: '',
                exceptions: '',
                url: '/api/permission-management/permissions',
                httpMethod: 'PUT',
                browserInfo: 'Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36',
                correlationId: '3dd03595582048c1ae603ef669b756d6',
                clientId: 'TigerAdmin_App',
                clientName: null,
                clientIpAddress: '::1',
                executionDuration: 65,
                executionTime: '2020-10-24T22:27:54.9287546',
                impersonatorTenantId: null,
                impersonatorUserId: null,
                tenantName: null,
                tenantId: null,
                userName: 'admin',
                userId: '1847197c-bfc8-aa9c-bafd-39f8621f66ad',
                applicationName: null,
                id: '67a91900-a8b1-8766-f751-39f86d2f15f1',
                extraProperties: {}
            }
        }
    },
    created() {
        this.fetchData()
    },
    methods: {
        handleSizeChange(val) {
            console.log(`每页 ${val} 条`)
            this.listQuery.SkipCount =
                (this.listQuery.page - 1) * this.listQuery.MaxResultCount
        },
        fetchData() {
            this.listLoading = true
            // console.log(this.listQuery);
            if (this.queryDateTime) {
                this.listQuery.startTime = this.queryDateTime[0]
                this.listQuery.endTime = this.queryDateTime[1]
            }
            getAuditLogs(this.listQuery).then((response) => {
                this.list = response.items
                this.total = response.totalCount
                this.listLoading = false
            })
        },
        handleFilter() {
            this.listQuery.page = 1
            this.fetchData()
        },
        sortChange(data) {
            const {
                prop,
                order
            } = data
            if (prop === 'executionTime') {
                this.sortExecutionTime(order)
            }
        },
        sortExecutionTime(order) {
            if (order === 'ascending') {
                this.listQuery.Sorting = 'executionTime ASC'
            } else {
                this.listQuery.Sorting = 'executionTime DESC'
            }
            this.handleFilter()
        },
        deleteData(id) {
            console.log('delete')
        },
        handleUpdate(row) {
            this.temp = Object.assign({}, row) // copy obj
            console.log(this.temp)
            this.dialogStatus = 'update'
            this.dialogRoleFormVisible = true
        },
        handleDownload() {
            this.downloadLoading = true
            import('@/vendor/Export2Excel').then(excel => {
                const tHeader = ['browserInfo', 'clientId', 'clientIpAddress', 'clientName', 'correlationId', 'exceptions', 'executionDuration', 'executionTime', 'httpMethod', 'httpStatusCode', 'url', 'userId', 'userName']
                const filterVal = ['browserInfo', 'clientId', 'clientIpAddress', 'clientName', 'correlationId', 'exceptions', 'executionDuration', 'executionTime', 'httpMethod', 'httpStatusCode', 'url', 'userId', 'userName']
                const list = this.list
                const data = this.formatJson(filterVal, list)
                excel.export_json_to_excel({
                    header: tHeader,
                    data,
                    filename: this.filename,
                    autoWidth: this.autoWidth,
                    bookType: this.bookType
                })
                this.downloadLoading = false
            })
        },
        formatJson(filterVal, jsonData) {
            return jsonData.map(v => filterVal.map(j => {
                if (j === 'executionTime') {
                    return parseTime(v[j])
                } else {
                    return v[j]
                }
            }))
        }
    }
}
</script>

<style scoped>
.filter-container {
    margin-bottom: 20px;
}
</style>
