<template>
<div class="app-container">
    <div class="filter-container" style="margin-bottom:10px;">
        <el-form ref="logQueryForm" label-position="left" label-width="100px" :model="queryForm">
            <!-- 增加没一列之间的间隔 -->
            <el-row :gutter="20">
                <el-col :span="8">
                    <el-form-item :label="$t('AbpIdentity[\'SelectDateTime\']')">
                        <el-date-picker v-model="queryDateTime" type="datetimerange" align="left" unlink-panels :picker-options="pickerOptions" :start-placeholder="$t('AbpIdentity[\'StartTime\']')" :end-placeholder="$t('AbpIdentity[\'EndTime\']')" />
                    </el-form-item>
                </el-col>
                <el-col :span="4">
                    <el-form-item prop="applicationName" :label="$t('AbpIdentity[\'ApplicationName\']')">
                        <el-input v-model="queryForm.applicationName" :placeholder="$t('AbpIdentity[\'ApplicationName\']')" />
                    </el-form-item>
                </el-col>
                <el-col :span="4">
                    <el-form-item prop="identity" :label="$t('AbpIdentity[\'Identity\']')">
                        <el-input v-model="queryForm.identity" :placeholder="$t('AbpIdentity[\'Identity\']')" />
                    </el-form-item>
                </el-col>
                <el-col :span="4">
                    <el-form-item prop="userName" :label="$t('AbpIdentity[\'UserName\']')">
                        <el-input v-model="queryForm.userName" :placeholder="$t('AbpIdentity[\'UserName\']')" />
                    </el-form-item>
                </el-col>

                <el-col :span="4">
                    <el-button-group>
                        <el-button type="primary" icon="el-icon-search" @click="getList">
                            {{ $t('AbpIdentity.Search') }}
                        </el-button>
                        <el-button type="reset" icon="el-icon-remove-outline" @click="resetQueryForm">
                            {{ $t('AbpIdentity.Reset') }}
                        </el-button>
                        <el-link type="info" :underline="false" style="margin-left: 8px;line-height: 28px;" @click="toggleAdvanced">
                            {{ advanced ? '收起' :  '展开'}}
                            <i :class="advanced ? 'el-icon-arrow-up' : 'el-icon-arrow-down'" />
                        </el-link>
                    </el-button-group>
                </el-col>
            </el-row>

            <el-collapse-transition>
                <div v-show="advanced">
                    <el-row :gutter="20">
                        <el-col :span="4">
                            <el-form-item prop="action" :label="$t('AbpIdentity[\'ActionName\']')">
                                <el-input v-model="queryForm.action" :placeholder="$t('AbpIdentity[\'ActionName\']')" />
                            </el-form-item>
                        </el-col>
                        <el-col :span="4">
                            <el-form-item prop="clientId" :label="$t('AbpIdentity[\'ClientId\']')">
                                <el-input v-model="queryForm.clientId" :placeholder="$t('AbpIdentity[\'ClientId\']')" />
                            </el-form-item>
                        </el-col>
                        <el-col :span="4">
                            <el-form-item prop="CorrelationId" :label="$t('AbpIdentity[\'CorrelationId\']')">
                                <el-input v-model="queryForm.correlationId" :placeholder="$t('AbpIdentity[\'CorrelationId\']')" />
                            </el-form-item>
                        </el-col>
                    </el-row>

                </div>
            </el-collapse-transition>

            <el-row>
                <el-col :span="4">
                    <el-button-group style="float:left">
                        <el-button type="primary" icon="el-icon-refresh" @click="handleRefresh">
                            刷新
                        </el-button>
                        <!-- <el-button type="reset" icon="el-icon-download" @click="handleDownload">
                            导出
                        </el-button> -->
                    </el-button-group>

                </el-col>
            </el-row>
        </el-form>
    </div>

    <div class="table-container">
        <el-table :key="tableKey" v-loading="listLoading" :data="list" border fit highlight-current-row style="width: 100%;" :default-sort="{prop: 'date', order: 'descending'}" @sort-change="sortChange">

            <el-table-column :label="$t('AbpIdentity[\'CreationTime\']')" prop="creationTime" align="center" width="150" sortable="custom">
                <template slot-scope="{ row }">
                    <span>{{ row.creationTime | moment }}</span>
                </template>
            </el-table-column>
            <el-table-column :label="$t('AbpIdentity[\'ActionName\']')" prop="action" align="center" width="120" sortable="custom">
                <template slot-scope="{ row }">
                    <span>{{ row.action | empty }}</span>
                </template>
            </el-table-column>
            <el-table-column :label="$t('AbpIdentity[\'ClientIpAddress\']')" prop="clientIpAddress" align="center" width="120">
                <template slot-scope="{ row }">
                    <span>{{ row.clientIpAddress | empty }}</span>
                </template>
            </el-table-column>
            <el-table-column :label="$t('AbpIdentity[\'BrowserInfo\']')" prop="browserInfo" align="center" :show-overflow-tooltip="true">
                <template slot-scope="{ row }">
                    <span>{{ row.browserInfo | empty }}</span>
                </template>
            </el-table-column>
            <el-table-column :label="$t('AbpIdentity[\'ApplicationName\']')" prop="applicationName" align="center" width="140" sortable="custom">
                <template slot-scope="{ row }">
                    <span>{{ row.applicationName | empty }}</span>
                </template>
            </el-table-column>
            <el-table-column :label="$t('AbpIdentity[\'Identity\']')" prop="identity" align="center" width="120" sortable="custom">
                <template slot-scope="{ row }">
                    <span>{{ row.identity | empty }}</span>
                </template>
            </el-table-column>
            <el-table-column :label="$t('AbpIdentity[\'TenantName\']')" prop="tenantName" align="center" width="180" sortable="custom">
                <template slot-scope="{ row }">
                    <span>{{ row.tenantName | empty }}</span>
                </template>
            </el-table-column>
            <el-table-column :label="$t('AbpIdentity[\'UserName\']')" prop="userName" align="center" width="180" sortable="custom">
                <template slot-scope="{ row }">
                    <span>{{ row.userName | empty }}</span>
                </template>
            </el-table-column>
            <el-table-column :label="$t('AbpIdentity[\'ClientId\']')" prop="clientId" align="center" width="120" sortable="custom">
                <template slot-scope="{ row }">
                    <span>{{ row.clientId | empty }}</span>
                </template>
            </el-table-column>
            <el-table-column :label="$t('AbpIdentity[\'CorrelationId\']')" prop="correlationId" align="center" width="120" sortable="custom" :show-overflow-tooltip="true">
                <template slot-scope="{ row }">
                    <span>{{ row.correlationId | empty }}</span>
                </template>
            </el-table-column>
           
        </el-table>
        <pagination v-show="total > 0" :total="total" :page.sync="queryForm.page" :limit.sync="queryForm.limit" @pagination="getList" />

        
    </div>
</div>
</template>

<script>
import {
    getSecurityLogs
} from '@/api/system-manage/identity/security-log'

import {
    pickerRangeWithHotKey
} from '@/utils/picker'
import {
    parseTime
} from '@/utils'

import Pagination from '@/components/Pagination'
import baseListQuery from '@/utils/abp'

export default {
    name: 'SecurityLog',
    components: {
        Pagination
    },
    filters: {
    },
    data() {
        return {
            tableKey: 0,
            advanced: false, // 判断搜索栏展开/收起
            list: null,
            total: 0,
            listLoading: true,
            queryDateTime: undefined, // 时间范围过滤条件
            queryForm: Object.assign({
                startTime: undefined,
                endTime: undefined,
                applicationName: undefined,
                userName: undefined,
                identity: undefined,
                action: undefined,
                clientId: undefined,
                correlationId: undefined,
            }, baseListQuery),
            pickerOptions: pickerRangeWithHotKey,
            downloadLoading: false // 下载控制

        }
    },
    created() {
        this.getList()
    },
    methods: {

        // 获取列表
        getList() {
            this.listLoading = true
            if (this.queryDateTime) {
                this.queryForm.startTime = this.queryDateTime[0]
                this.queryForm.endTime = this.queryDateTime[1]
            }
            getSecurityLogs(this.queryForm).then(response => {
                this.list = response.items
                this.total = response.totalCount
                this.listLoading = false
            })
        },
        // 重置查询参数
        resetQueryForm() {
            this.queryForm = Object.assign({
                startTime: undefined,
                endTime: undefined,
                applicationName: undefined,
                userName: undefined,
                identity: undefined,
                action: undefined,
                clientId: undefined,
                correlationId: undefined,
            }, baseListQuery)
        },
        // 搜索展开切换
        toggleAdvanced() {
            this.advanced = !this.advanced
        },
        // 设置排序字段重新查询
        sortChange(data) {
            const {
                prop,
                order
            } = data
            if (order === 'ascending') {
                this.queryForm.sort = prop + ' ASC'
            } else {
                this.queryForm.sort = prop + ' DESC'
            }
            this.queryForm.page = 1
            this.getList()
        },
        //刷新页面
        handleRefresh() {
            //  在history记录中前进或者后退val步，当val为0时刷新当前页面。
            this.$router.go(0);
        },
        //查看详情
        handleDetail(row) {
            this.$refs['auditLogDetailsDialog'].createLogInfo(row)
        },
        //下载数据
        handleDownload() {
            this.downloadLoading = true
            import('@/vendor/Export2Excel').then(excel => {
                const tHeader = ['browserInfo', 'clientId', 'clientIpAddress', 'clientName', 'correlationId', 'exceptions', 'executionDuration', 'executionTime', 'httpMethod', 'httpStatusCode', 'url', 'userId', 'userName']
                const filterVal = ['browserInfo', 'clientId', 'clientIpAddress', 'clientName', 'correlationId', 'exceptions', 'executionDuration', 'executionTime', 'httpMethod', 'httpStatusCode', 'url', 'userId', 'userName']

                // TODO: 修改为当前查询条件下所有页数的数据
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

<style lang="scss" scoped>
.app-container {
    .api-block {
        height: auto;
        border: none;
        padding: 4px 0;
        margin: 4px 0;
    }
    
}
</style>
