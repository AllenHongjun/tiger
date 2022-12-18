<template>
<div class="app-container">
    <el-row :gutter="20">
        <el-col :span="10">
            <div class="grid-content">

                <el-row>
                    <el-col :span="20">
                        <div class="grid-content">组织机构树</div>
                    </el-col>
                    <el-col :span="4">
                        <div class="grid-content">
                            <el-button type="primary">添加根机构</el-button>
                        </div>
                    </el-col>
                </el-row>

                <el-row>
                    <div class="block">
                        <org-tree ref="roleOrgTree" :org-tree-node-click="handleOrgTreeNodeClick" />
                    </div>
                </el-row>

            </div>
        </el-col>
        <el-col :span="14">
            <div class="grid-content">

                <el-tabs type="border-card" v-model="activeName">
                    <el-tab-pane :label="$t('AbpIdentity[\'OrganizationUnit:Members\']')" name="users">
                        <el-row>
                            <el-col :span="5" :offset="19">
                                <el-button v-if="checkPermission('AbpIdentity.Roles.Create')" class="filter-item" style="margin-left: 10px;" type="primary" icon="el-icon-edit" @click="handleCreate">
                                    {{ $t("AbpIdentity['OrganizationUnit:AddMember']")}}
                                </el-button>
                                <!-- <el-button class="filter-item" style="margin-left: 10px;" icon="el-icon-refresh" @click="handleRefresh">
                                    {{ $t("AbpIdentity['Refresh']") }}
                                </el-button> -->
                            </el-col>
                        </el-row>
                        <div class="filter-container">

                        </div>

                        <el-table :key="tableKey" v-loading="listLoading" :data="orgUserList" border fit highlight-current-row style="width: 100%;" @sort-change="sortChange">
                            <el-table-column :label="$t('AbpIdentity[\'RoleName\']')" prop="name" sortable align="center">
                                <template slot-scope="{ row }">
                                    <span>{{ row.userName }}</span>
                                </template>
                            </el-table-column>
                            <el-table-column :label="$t('AbpIdentity[\'Actions\']')" align="center" width="300" class-name="small-padding fixed-width">
                                <template slot-scope="{ row, $index }">

                                    <el-button v-if="!row.isStatic && checkPermission('AbpIdentity.Roles.Delete')" size="mini" type="danger" @click="handleDelete(row, $index)">
                                        {{ $t("AbpIdentity['Delete']") }}
                                    </el-button>
                                </template>
                            </el-table-column>
                        </el-table>

                        <pagination v-show="orgUserTotal > 0" :total="orgUserTotal" :page.sync="listQuery.page" :limit.sync="listQuery.limit" @pagination="getOrgUserList" />

                        <el-dialog :title="dialogStatus == 'create'? $t('AbpIdentity[\'NewRole\']'): $t('AbpIdentity[\'Edit\']')" :visible.sync="dialogFormVisible">
                            <el-form ref="dataForm" :rules="rules" :model="temp" label-position="right" label-width="120px">

                            </el-form>
                            <div slot="footer" class="dialog-footer">
                                <el-button @click="dialogFormVisible = false">
                                    {{ $t("AbpIdentity['Cancel']") }}
                                </el-button>
                                <el-button type="primary" @click="dialogStatus === 'create' ? createData() : updateData()">
                                    {{ $t("AbpIdentity['Save']") }}
                                </el-button>
                            </div>
                        </el-dialog>
                    </el-tab-pane>

                    <el-tab-pane :label="$t('AbpIdentity[\'OrganizationUnit:Roles\']')" name="roles">
                        <div v-if="this.orgData">
                            <el-row>
                                <el-col :span="5" :offset="19">
                                    <el-button v-if="checkPermission('AbpIdentity.Roles.Create')" class="filter-item" style="margin-left: 10px;" type="primary" icon="el-icon-edit" @click="handleAddRoles">
                                        {{ $t("AbpIdentity['OrganizationUnit:AddRole']")}}
                                    </el-button>
                                    <!-- <el-button class="filter-item" style="margin-left: 10px;" icon="el-icon-refresh" @click="handleRefresh">
                                        {{ $t("AbpIdentity['Refresh']") }}
                                    </el-button> -->
                                </el-col>
                            </el-row>

                            <el-table :key="tableKey" v-loading="listLoading" :data="orgRoleList" border fit highlight-current-row style="width: 100%;" @sort-change="sortChange">
                                <el-table-column :label="$t('AbpIdentity[\'RoleName\']')" prop="name" sortable align="center">
                                    <template slot-scope="{ row }">
                                        <span>{{ row.name }}</span>
                                    </template>
                                </el-table-column>
                                <el-table-column :label="$t('AbpIdentity[\'Actions\']')" align="center" width="300" class-name="small-padding fixed-width">
                                    <template slot-scope="{ row, $index }">
                                        <el-button v-if="!row.isStatic && checkPermission('AbpIdentity.Roles.Delete')" size="mini" type="danger" @click="handleRemoveRole(row, $index)">
                                            {{ $t("AbpIdentity['Delete']") }}
                                        </el-button>
                                    </template>
                                </el-table-column>
                            </el-table>

                            <pagination v-show="orgRoleTotal > 0" :total="orgRoleTotal" :page.sync="listQuery.page" :limit.sync="listQuery.limit" @pagination="getOrgRoleList" />

                            <!-- 组织选择关联多个角色 -->
                            <add-roles-dialog ref="addRolesDialog" :refreshParentRoles="handleRefreshRoles" />
                        </div>
                        <el-row v-else>
                            <p style="color:#606266;">选择一个组织机构来查看角色</p>
                        </el-row>

                    </el-tab-pane>

                </el-tabs>
            </div>
        </el-col>
    </el-row>
</div>
</template>

<script>
import Pagination from '@/components/Pagination'
import OrgTree from '../components/org-tree'
import AddRolesDialog from './components/add-roles-dialog'

import {
    getOrganizationsRoot,
    getOrganizationSingleWithDetails,
    getOrganizationSingle,
    getOrganizationsAll,
    getOrganizations,
    getOrganizationChildren,
    getOrgUsers,
    getOrgRoles,
    removeRole,

    createOrganization,
    updateOrganization,
    deleteOrganization,

    getOrganizationsAllWithDetails
} from '@/api/system-manage/identity/organization-unit'

import {
    createRoleToOrg,
    getRoleById,
    updateRole,
    deleteRole
} from '@/api/system-manage/identity/role'

import baseListQuery, {
    checkPermission
} from '@/utils/abp'

export default {
    name: 'OrganizationUnits',
    components: {
        Pagination,
        OrgTree,
        AddRolesDialog
    },
    data() {
        return {
            activeName: 'roles',
            tableKey: 0,
            orgRoleList: null,
            orgRoleTotal: 0,
            pageSizesRoleSelect: [10, 20, 30, 40, 50, 100],

            listLoading: true,
            listQuery: {
                page: 1,
                limit: 50,
                sort: undefined,
                filter: undefined,
                ouId: undefined
            },

            dialogRoleFormVisible: false,

            orgUserList: null,
            orgUserTotal: 0,

            orgData: null,
            temp: {
                orgId: null,
                name: '',
                isDefault: false,
                isPublic: false
            },
            dialogFormVisible: false,
            dialogStatus: '',
            rules: {
                name: [{
                        required: true,
                        message: this.$i18n.t("AbpIdentity['The {0} field is required.']", [
                            this.$i18n.t("AbpIdentity['RoleName']")
                        ]),
                        trigger: 'blur'
                    },
                    {
                        max: 256,
                        message: this.$i18n.t(
                            "AbpIdentity['The field {0} must be a string with a maximum length of {1}.']",
                            [this.$i18n.t("AbpIdentity['RoleName']"), '256']
                        ),
                        trigger: 'blur'
                    }
                ]
            }
        }
    },
    computed: {
        orgName() {
            if (this.orgData === null) {
                return ''
            }
            return `${this.orgData.displayName}(${this.orgData.code})`
        },

    },
    created() {

    },
    methods: {
        checkPermission,
        getOrgUserList() {
            this.listLoading = true
            getOrgUsers(this.listQuery).then(response => {
                this.orgUserList = response.items
                this.orgUserTotal = response.totalCount
                this.listLoading = false
            })
        },

        getOrgRoleList() {
            this.listLoading = true
            getOrgRoles(this.listQuery).then(response => {
                this.orgRoleList = response.items
                this.orgRoleTotal = response.totalCount
                this.listLoading = false
            })
        },

        handleFilter(firstPage = true) {
            if (firstPage) this.listQuery.page = 1
            this.getOrgRoleList()
            this.getOrgUserList()
        },
        handleRefresh() {
            this.listQuery.ouId = undefined
            this.$refs.roleOrgTree.$refs.orgTree.setCurrentKey(null)
            this.orgData = null
            this.handleFilter()
        },
        sortChange(data) {
            const {
                prop,
                order
            } = data
            this.listQuery.sort = order ? `${prop} ${order}` : undefined
            this.handleFilter()
        },
        resetTemp() {
            this.temp = {
                orgId: null,
                name: '',
                isDefault: false,
                isPublic: false
            }
        },

        handleAddRoles() {
            this.$refs['addRolesDialog'].handleAddRoles(this.orgData)
        },
        handleRemoveRole(row, index) {
            this.$confirm(
                this.$i18n.t("AbpIdentity['OrganizationUnit:AreYouSureRemoveRole']", [
                    row.name
                ]),
                this.$i18n.t("AbpIdentity['AreYouSure']"), {
                    confirmButtonText: this.$i18n.t("AbpIdentity['Yes']"),
                    cancelButtonText: this.$i18n.t("AbpIdentity['Cancel']"),
                    type: 'warning'
                }
            ).then(async () => {
                removeRole(this.orgData.id, {
                    roleId: row.id
                }).then(() => {
                    this.handleFilter()
                    this.$notify({
                        title: this.$i18n.t("TigerUi['Success']"),
                        message: this.$i18n.t("TigerUi['SuccessMessage']"),
                        type: 'success',
                        duration: 2000
                    })
                })
            })
        },
        handleRefreshRoles(firstPage = true) {
            if (firstPage) this.listQuery.page = 1
            this.getOrgRoleList()
        },

        handleCreate() {
            this.resetTemp()
            this.dialogStatus = 'create'
            this.dialogFormVisible = true
            this.$nextTick(() => {
                this.$refs['dataForm'].clearValidate()
            })
        },
        createData() {
            this.$refs['dataForm'].validate(valid => {
                if (valid) {
                    if (this.orgData !== null) {
                        this.temp.orgId = this.orgData.id
                    }
                    createRoleToOrg(this.temp).then(() => {
                        this.handleFilter()
                        this.dialogFormVisible = false
                        this.$notify({
                            title: this.$i18n.t("HelloAbp['Success']"),
                            message: this.$i18n.t("HelloAbp['SuccessMessage']"),
                            type: 'success',
                            duration: 2000
                        })
                    })
                }
            })
        },
        handleUpdate(row) {
            this.temp = Object.assign({}, row) // copy obj
            this.dialogStatus = 'update'
            this.dialogFormVisible = true

            getRoleById(row.id).then(response => {
                this.temp = response
            })

            this.$nextTick(() => {
                this.$refs['dataForm'].clearValidate()
            })
        },
        updateData() {
            this.$refs['dataForm'].validate(valid => {
                if (valid) {
                    updateRole(this.temp).then(() => {
                        this.handleFilter(false)
                        this.dialogFormVisible = false
                        this.$notify({
                            title: this.$i18n.t("HelloAbp['Success']"),
                            message: this.$i18n.t("HelloAbp['SuccessMessage']"),
                            type: 'success',
                            duration: 2000
                        })
                    })
                }
            })
        },
        handleDelete(row, index) {
            this.$confirm(
                this.$i18n.t("AbpIdentity['RoleDeletionConfirmationMessage']", [
                    row.name
                ]),
                this.$i18n.t("AbpIdentity['AreYouSure']"), {
                    confirmButtonText: this.$i18n.t("AbpIdentity['Yes']"),
                    cancelButtonText: this.$i18n.t("AbpIdentity['Cancel']"),
                    type: 'warning'
                }
            ).then(async () => {
                deleteRole(row.id).then(() => {
                    this.handleFilter()
                    this.$notify({
                        title: this.$i18n.t("HelloAbp['Success']"),
                        message: this.$i18n.t("HelloAbp['SuccessMessage']"),
                        type: 'success',
                        duration: 2000
                    })
                })
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
        },
        onSubmit() {
            this.$message('submit!')
        },
        onCancel() {
            this.$message({
                message: 'cancel!',
                type: 'warning'
            })
        },

        fetchData() {
            this.listLoading = true
            getOrgRoles(this.listQuery).then((response) => {
                this.orgRoleList = response.items
                this.total = response.totalCount
                this.listLoading = false
            })
        },
        handleRefresh() {
            this.listQuery.ouId = undefined
            this.$refs.roleOrgTree.$refs.orgTree.setCurrentKey(null)
            this.orgData = null
            this.handleFilter()
        },
        handleFilter() {
            this.listQuery.page = 1
            this.fetchData()
        },
        handleOrgTreeNodeClick(data) {
            if (data.id) {
                this.listQuery.ouId = data.id
                this.orgData = data
                this.handleFilter()
            }
        },

        // table
        tableRowClassName({
            row,
            rowIndex
        }) {
            if (rowIndex === 1) {
                return 'warning-row';
            } else if (rowIndex === 3) {
                return 'success-row';
            }
            return '';
        }
    }
}
</script>

<style lang="scss" scoped>
.el-row {
    margin-bottom: 20px;

    &:last-child {
        margin-bottom: 0;
    }
}

.el-col {
    border-radius: 4px;
}

.bg-purple-dark {
    background: #99a9bf;
}

.bg-purple {
    background: #d3dce6;
}

.bg-purple-light {
    background: #e5e9f2;
}

.grid-content {
    border-radius: 4px;
    min-height: 36px;
}

.row-bg {
    padding: 10px 0;
    background-color: #f9fafc;
}

.el-table .warning-row {
    background: oldlace;
}

.el-table .success-row {
    background: #f0f9eb;
}

.el-tab-pane .h3 {
    margin: 0;
}
</style>
