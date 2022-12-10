<template>
<div class="app-container">
    <div class="filter-container" style="margin-bottom:10px;">
        <el-form ref="logQueryForm" label-position="left" label-width="100px" :model="queryForm">
            <!-- 增加没一列之间的间隔 -->
            <el-row :gutter="20">
                <el-col :span="4">
                    <el-form-item prop="url" :label="$t('AbpAuditLogging[\'Url\']')">
                        <el-input v-model="queryForm.url" :placeholder="$t('AbpAuditLogging[\'PlaceholderInput\']')" />
                    </el-form-item>
                </el-col>
                <el-col :span="4">
                    <el-form-item prop="httpMethod" :label="$t('AbpAuditLogging[\'HttpMethod\']')">
                        <el-select v-model="queryForm.httpMethod" clearable style="width:100%" @clear="queryForm.httpMethod=undefined">
                            <el-option label="获取(GET)" value="GET" />
                            <el-option label="修改(PUT)" value="PUT" />
                            <el-option label="提交(POST)" value="POST" />
                            <el-option label="删除(DELETE)" value="DELETE" />
                        </el-select>
                    </el-form-item>
                </el-col>
                <el-col :span="4">
                    <el-form-item prop="userName" :label="$t('AbpAuditLogging[\'UserName\']')">
                        <el-input v-model="queryForm.userName" :placeholder="$t('AbpAuditLogging[\'PlaceholderInput\']')" />
                    </el-form-item>
                </el-col>
                <el-col :span="4">
                    <el-form-item prop="clientIpAddress" :label="$t('AbpAuditLogging[\'ClientIpAddress\']')">
                        <el-input v-model="queryForm.clientIpAddress" :placeholder="$t('AbpAuditLogging[\'PlaceholderInput\']')" />
                    </el-form-item>
                </el-col>
                <el-col :span="4">
                    <el-form-item prop="ttpStatusCode" :label="$t('AbpAuditLogging[\'HttpStatusCode\']')">
                        <el-select v-model="queryForm.httpStatusCode" clearable style="width:100%" @clear="queryForm.httpStatusCode=undefined">
                            <el-option label="成功(200)" value="200" />
                            <el-option label="未登录(401)" value="401" />
                            <el-option label="未授权(403)" value="403" />
                            <el-option label="未找到资源(404)" value="404" />
                            <el-option label="异常(500)" value="500" />
                        </el-select>
                    </el-form-item>
                </el-col>
                <el-col :span="4">
                    <!-- <el-form-item prop="tenantName" :label="$t('AbpAuditLogging[\'TenantName\']')">
                        <el-input v-model="queryForm.tenantName" :placeholder="$t('AbpAuditLogging[\'PlaceholderInput\']')" />
                    </el-form-item> -->
                    <el-button-group>
                        <el-button type="primary" icon="el-icon-search" @click="getList">
                            {{ $t('AbpAuditLogging.Search') }}
                        </el-button>
                        <el-button type="reset" icon="el-icon-remove-outline" @click="resetQueryForm">
                            {{ $t('AbpAuditLogging.Reset') }}
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
                            <el-form-item prop="executionDuration" :label="$t('AbpAuditLogging[\'ExecutionDuration\']')">
                                <el-input v-model="queryForm.executionDuration" :placeholder="$t('AbpAuditLogging[\'PlaceholderInput\']')" />
                            </el-form-item>
                        </el-col>
                        <el-col :span="4">
                            <el-form-item prop="applicationName" :label="$t('AbpAuditLogging[\'ApplicationName\']')">
                                <el-input v-model="queryForm.applicationName" :placeholder="$t('AbpAuditLogging[\'PlaceholderInput\']')" />
                            </el-form-item>
                        </el-col>
                        <el-col :span="9">
                            <el-form-item label="日期">
                                <el-date-picker v-model="queryDateTime" type="datetimerange" align="right" unlink-panels :picker-options="pickerOptions" :range-separator="$t('AbpAuditLogging[\'RangeSeparator\']')" :start-placeholder="$t('AbpAuditLogging[\'StartPlaceholder\']')" :end-placeholder="$t('AbpAuditLogging[\'EndPlaceholder\']')" />
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
                        <el-button type="reset" icon="el-icon-download" @click="handleDownload">
                            导出
                        </el-button>
                    </el-button-group>

                </el-col>
            </el-row>

        </el-form>
    </div>

    <div class="table-container">
        <el-table :key="tableKey" v-loading="listLoading" :data="list" border fit highlight-current-row style="width: 100%;" :default-sort="{prop: 'date', order: 'descending'}" @sort-change="sortChange">
            <el-table-column :label="$t('AbpAuditLogging[\'RequestInfo\']')" align="left" width="" prop="httpStatusCode" sortable="custom">
                <template slot-scope="{ row }">
                    <el-tag :type="row.httpStatusCode | requestStatusCode">
                        {{ row.httpStatusCode }}
                    </el-tag>
                    <el-tag :type="row.httpMethod | requestMethodFilter">
                        {{ row.httpMethod }}
                    </el-tag>
                    <el-tag effect="dark" :type="row.executionDuration | requestDurationFilter">
                        {{ row.executionDuration }} <b>ms</b>
                    </el-tag>
                    <span class="api-block" :class="row.httpMethod | requestMethodFilter">
                        {{ row.url }}
                    </span>
                </template>
            </el-table-column>
            
            <el-table-column :label="$t('AbpAuditLogging[\'TenantName\']')" prop="tenantName" align="center" width="120" sortable="custom">
                <template slot-scope="{ row }">
                    <span>{{ row.tenantName | empty }}</span>
                </template>
            </el-table-column>
            <el-table-column :label="$t('AbpAuditLogging[\'UserName\']')" prop="userName" align="center" width="120" sortable="custom">
                <template slot-scope="{ row }">
                    <span>{{ row.userName | empty }}</span>
                </template>
            </el-table-column>
            <el-table-column :label="$t('AbpAuditLogging[\'ApplicationName\']')" prop="applicationName" align="center" width="120" sortable="custom">
                <template slot-scope="{ row }">
                    <span>{{ row.applicationName | empty }}</span>
                </template>
            </el-table-column>
            <el-table-column :label="$t('AbpAuditLogging[\'ExecutionTime\']')" prop="executionTime" align="center" width="180" sortable="custom">
                <template slot-scope="{ row }">
                    <span>{{ row.executionTime | moment }}</span>
                </template>
            </el-table-column>
            <el-table-column :label="$t('AbpAuditLogging[\'ClientIpAddress\']')" prop="clientIpAddress" align="center" width="120" sortable="custom">
                <template slot-scope="{ row }">
                    <span>{{ row.clientIpAddress | empty }}</span>
                </template>
            </el-table-column>
            <el-table-column :label="$t('AbpAuditLogging[\'Action\']')" prop="action" align="center" width="120">
                <template slot-scope="{ row }">
                    <el-button type="primary" @click="handleDetail(row)">
                        {{ $t('AbpAuditLogging.Detail') }}
                    </el-button>
                </template>
            </el-table-column>
        </el-table>
        <pagination v-show="total > 0" :total="total" :page.sync="queryForm.page" :limit.sync="queryForm.limit" @pagination="getList" />

        <!-- 审计日志明细 -->
        <audit-log-details ref="auditLogDetailsDialog" />
    </div>
</div>
</template>

<script>
import {
    getAuditLogs
} from '@/api/auditlogging/auditlog'
import {
    pickerRangeWithHotKey
} from '@/utils/picker'
import {
    parseTime
} from '@/utils'

import Pagination from '@/components/Pagination'
import baseListQuery from '@/utils/abp'
import AuditLogDetails from './details'


export default {
    name: 'AuditLog',
    components: {
        Pagination,
        AuditLogDetails
    },
    filters: {
        requestDurationFilter(duration) {
            let type = 'success'
            if (duration > 2 * 1000) {
                type = 'warning'
            } else if (duration > 5 * 1000) {
                type = 'error'
            }
            return type
        },
        requestStatusCode(code) {
            let type = 'success'
            switch (code) {
                case 401:
                case 403:
                case 404:
                    type = 'warning'
                    break
                case 500:
                    type = 'danger'
                    break
            }
            return type
        },
        requestMethodFilter(method) {
            let type = 'success'
            switch (method.toUpperCase()) {
                case 'GET':
                    type = ''
                    break
                case 'PUT':
                    type = 'warning'
                    break
                case 'POST':
                    type = 'success'
                    break
                case 'DELETE':
                    type = 'danger'
                    break
                default:
                    type = 'Info'
            }
            return type
        }
    },
    data() {
        return {
            tableKey: 0,
            advanced: false, // 判断搜索栏展开/收起
            list: null,
            total: 0,
            listLoading: true,
            queryDateTime: undefined,
            queryForm: Object.assign({
                startTime: undefined,
                endTime: undefined,
                httpMethod: undefined,
                url: undefined,
                userName: undefined,
                tenantName: undefined,
                applicationName: undefined,
                hasException: false,
                httpStatusCode: undefined
            }, baseListQuery),
            pickerOptions: pickerRangeWithHotKey,
            downloadLoading:false // 下载控制

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
            getAuditLogs(this.queryForm).then(response => {
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
                httpMethod: undefined,
                url: undefined,
                userName: undefined,
                tenantName: undefined,
                applicationName: undefined,
                hasException: false,
                httpStatusCode: undefined
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
        handleRefresh(){
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

    .el-tag {
        color: #ffffff;
        font-weight: 700;
        background: #61affe;
    }

    .el-tag--warning {
        background: #fca130;
    }

    .el-tag--danger {
        background: #f93e3e;
    }

    .el-tag--success {
        background: #49cc90;
    }

    .info {
        border-color: #61affe;
        background: rgba(97, 175, 254, .1);
    }

    .success {
        border-color: #49cc90;
        background: rgba(73, 204, 144, .1);
    }

    .danger {
        border-color: #f93e3e;
        background: rgba(249, 62, 62, .1);
    }

    .warning {
        border-color: #fca130;
        background: rgba(252, 161, 48, .1);
    }
}
</style>
