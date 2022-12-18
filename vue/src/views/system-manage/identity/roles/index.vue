<template>
<div class="app-container">
    <el-row :gutter="0">
        <!-- <el-col :span="6">
            <org-tree ref="roleOrgTree" :org-tree-node-click="handleOrgTreeNodeClick" />
        </el-col> -->
        <el-col :span="24">
            <el-row style="margin-bottom: 20px">
                <el-input v-model="listQuery.Filter" placeholder="关键词" style="width: 150px" class="filter-item" />

                <el-button class="filter-item" type="primary" icon="el-icon-search" @click="handleFilter" />

                <el-button type="primary" icon="el-icon-edit" @click="handleCreate">
                    {{ $t("AbpIdentity['NewRole']") }}
                </el-button>
                <el-button class="filter-item" style="margin-left: 10px;" icon="el-icon-refresh" @click="handleRefresh">
                    {{ $t("AbpIdentity['Refresh']") }}
                </el-button>
            </el-row>
            <el-table v-loading="listLoading" :data="list" element-loading-text="Loading" border fit highlight-current-row>
                <el-table-column align="center" label="ID" width="95">
                    <template slot-scope="scope">
                        {{ scope.$index }}
                    </template>
                </el-table-column>
                <el-table-column label="名称" align="left">
                    <template slot-scope="scope">
                        
                        <el-tag v-if="scope.row.isPublic">
                            {{ $t('AbpIdentity[\'DisplayName:IsPublic\']')}}
                        </el-tag>

                        <el-tag v-if="scope.row.isDefault" type="warning">
                            {{ $t('AbpIdentity[\'DisplayName:IsDefault\']')}}
                        </el-tag>
                        <el-tag v-if="scope.row.isStatic" type="success">
                            {{ $t('AbpIdentity[\'DisplayName:IsStatic\']')}}
                        </el-tag>
                        {{ scope.row.name }}
                    </template>
                </el-table-column>

                <el-table-column align="center" :label="$t('AbpIdentity[\'Actions\']')" width="400">
                    <template slot-scope="scope">
                        <el-button v-if="checkPermission('AbpIdentity.Roles.Update')" type="primary" icon="el-icon-edit" @click="handleUpdate(scope.row)">
                            {{ $t("AbpIdentity['Edit']") }}
                        </el-button>
                        &nbsp;&nbsp;
                        <el-button v-if="checkPermission('AbpIdentity.Roles.ManagePermissions')" type="primary" @click="handleUpdatePermission(scope.row)">
                            {{ $t("AbpIdentity['Permissions']") }}
                        </el-button>
                        <el-button v-if="!scope.row.isStatic && checkPermission('AbpIdentity.Roles.Delete')" type="danger" icon="el-icon-delete" @click="deleteData(scope.row.id)">
                            {{ $t("AbpIdentity['Delete']") }}
                        </el-button>
                    </template>
                </el-table-column>
            </el-table>
            <pagination v-show="total > 0" :total="total" :page.sync="listQuery.page" :limit.sync="listQuery.limit" @pagination="fetchData" />

            <el-dialog :title="dialogStatus == 'create'? $t('AbpIdentity[\'NewRole\']'): $t('AbpIdentity[\'Edit\']')" :visible.sync="dialogRoleFormVisible">
                <el-form ref="dataForm" :rules="rules" :model="temp" label-width="180px" label-position="left">
                    <el-form-item label="组织" prop="orgName">
                        <el-input v-model="orgName" disabled />
                    </el-form-item>
                    <el-form-item :label="$t('AbpIdentity[\'RoleName\']')" prop="name">
                        <el-input v-model="temp.name" />
                    </el-form-item>
                    <el-form-item :label="$t('AbpIdentity[\'DisplayName:IsDefault\']')" prop="title">
                        <el-checkbox v-model="temp.isDefault" />
                    </el-form-item>
                    <el-form-item :label="$t('AbpIdentity[\'DisplayName:IsPublic\']')" prop="title">
                        <el-checkbox v-model="temp.isPublic" />
                    </el-form-item>
                </el-form>
                <div style="text-align: right">
                    <el-button type="danger" @click="dialogRoleFormVisible = false">{{ $t("AbpIdentity['Cancel']") }}</el-button>
                    <el-button type="primary" @click="dialogStatus === 'create' ? createData() : updateData()">{{ $t("AbpIdentity['Save']") }}</el-button>
                </div>
            </el-dialog>

            <permission-dialog ref="permissionDialog" provider-name="R" />
        </el-col>
    </el-row>
</div>
</template>

<script>
import {
    getRoleList,
    deleteRole,
    createRole,
    createRoleToOrg,
    updateRole
} from '@/api/system-manage/identity/role'

import {
    baseListQuery,
    checkPermission
} from '@/utils/abp'

import {
    getOrgRoles
} from '@/api/system-manage/identity/organization-unit'
import Pagination from '@/components/Pagination' // Secondary package based on el-pagination
import OrgTree from '../components/org-tree'
import PermissionDialog from '../components/permission-dialog'

export default {
    name: 'Role',
    components: {
        Pagination,
        OrgTree,
        PermissionDialog
    },
    data() {
        return {
            list: null,
            listLoading: true,
            total: 0,
            listQuery: {
                page: 1,
                limit: 20,
                SkipCount: 0,
                ouId: '',
                Filter: '',
                Sorting: 'name desc'
            },
            dialogStatus: '',
            dialogRoleFormVisible: false,
            orgData: '',
            temp: {
                id: '',
                name: '',
                orgId: '',
                isDefault: false,
                isPublic: false,
                isStatic: false
            },
            textMap: {
                update: '编辑',
                create: '添加'
            },
            dialogVisible: false,
            permissionData: {
                groups: []
            },
            treeDefaultProps: {
                children: 'children',
                label: 'label'
            },

            rules: {
                name: [{
                    required: true,
                    message: '请输入名称',
                    trigger: 'blur'
                }]
            }
        }
    },
    computed: {
        orgName() {
            if (this.orgData === null) {
                return ''
            }
            return `${this.orgData.displayName}(${this.orgData.code})`
        }
    },
    created() {
        this.fetchData()
    },
    methods: {
        checkPermission,
        fetchData() {
            this.listLoading = true
            getOrgRoles(this.listQuery).then((response) => {
                this.list = response.items
                this.total = response.totalCount
                this.listLoading = false
            })
        },
        handleRefresh() {
            this.listQuery.ouId = undefined
            // this.$refs.roleOrgTree.$refs.orgTree.setCurrentKey(null)
            this.orgData = null
            this.handleFilter()
        },
        handleFilter() {
            this.listQuery.page = 1
            this.fetchData()
        },
        resetTemp() {
            this.temp = {
                name: '',
                isDefault: false,
                isPublic: false
            }
        },
        handleCreate() {
            this.resetTemp()
            this.dialogStatus = 'create'
            this.dialogRoleFormVisible = true
            this.$nextTick(() => {
                this.$refs['dataForm'].clearValidate()
            })
        },
        createData() {
            this.$refs['dataForm'].validate((valid) => {
                if (valid) {
                    if (this.orgData !== null) {
                        this.temp.orgId = this.orgData.id
                    }
                    createRoleToOrg(this.temp).then(() => {
                        this.list.unshift(this.temp)
                        this.dialogRoleFormVisible = false
                        this.$notify({
                            title: '成功',
                            message: '操作成功',
                            type: 'success',
                            duration: 2000
                        })
                    })
                }
            })
        },
        handleUpdate(row) {
            this.temp = Object.assign({}, row) // copy obj
            // this.temp.timestamp = new Date(this.temp.timestamp)
            this.dialogStatus = 'update'
            this.dialogRoleFormVisible = true
            this.$nextTick(() => {
                this.$refs['dataForm'].clearValidate()
            })
        },
        updateData() {
            this.$refs['dataForm'].validate((valid) => {
                if (valid) {
                    const tempData = Object.assign({}, this.temp)
                    // tempData.timestamp = +new Date(tempData.timestamp) // change Thu Nov 30 2017 16:41:05 GMT+0800 (CST) to 1512031311464
                    updateRole(tempData).then(() => {
                        const index = this.list.findIndex((v) => v.id === this.temp.id)
                        this.list.splice(index, 1, this.temp)
                        this.dialogRoleFormVisible = false
                        this.$notify({
                            title: '成功',
                            message: '操作成功',
                            type: 'success',
                            duration: 2000
                        })
                    })
                }
            })
        },
        deleteData(id) {
            deleteRole(id)
                .then((response) => {
                    this.$message({
                        message: '删除成功',
                        type: 'success'
                    })
                })
                .catch((err) => {
                    console.log(err)
                })
        },
        handleUpdatePermission(row) {
            this.$refs['permissionDialog'].handleUpdatePermission(row)
        },

        handleOrgTreeNodeClick(data) {
            if (data.id) {
                this.listQuery.ouId = data.id
                this.orgData = data
                this.handleFilter()
            }
        }
    }
}
</script>

<style lang="scss" scoped>
.permissionTree {
    height: 450px;
    overflow-y: scroll;
}
</style>
