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
                                <el-button class="filter-item" style="margin-left: 10px;" icon="el-icon-refresh" @click="handleRefresh">
                                    {{ $t("AbpIdentity['Refresh']") }}
                                </el-button>
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
                        <el-row>
                            <el-col :span="5" :offset="19">
                                <el-button v-if="checkPermission('AbpIdentity.Roles.Create')" class="filter-item" style="margin-left: 10px;" type="primary" icon="el-icon-edit" @click="handleCreate">
                                    {{ $t("AbpIdentity['OrganizationUnit:AddRole']")}}
                                </el-button>
                                <el-button class="filter-item" style="margin-left: 10px;" icon="el-icon-refresh" @click="handleRefresh">
                                    {{ $t("AbpIdentity['Refresh']") }}
                                </el-button>
                            </el-col>
                        </el-row>
                        <div class="filter-container">

                        </div>

                        <el-table :key="tableKey" v-loading="listLoading" :data="orgRoleList" border fit highlight-current-row style="width: 100%;" @sort-change="sortChange">
                            <el-table-column :label="$t('AbpIdentity[\'RoleName\']')" prop="name" sortable align="center">
                                <template slot-scope="{ row }">
                                    <span>{{ row.name }}</span>
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

                        <pagination v-show="orgRoleTotal > 0" :total="orgRoleTotal" :page.sync="listQuery.page" :limit.sync="listQuery.limit" @pagination="getOrgRoleList" />

                        <!-- 组织选择关联多个角色 -->
                        <el-dialog :title="$t('AbpIdentity[\'OrganizationUnit:SelectRoles\']')" :visible.sync="dialogFormVisible">
                            <el-table :key="tableKey" v-loading="listLoading" :data="orgRoleList" border fit highlight-current-row style="width: 100%;" @sort-change="sortChange" :row-key="getRowKeys" @select-all="handleSelectionChange" @selection-change="handleSelectionChange">
                                <el-table-column type="selection" width="55" :reserve-selection="true">
                                </el-table-column>
                                <el-table-column :label="$t('AbpIdentity[\'RoleName\']')" prop="name" sortable align="left">
                                    <template slot-scope="{ row }">
                                        <span>{{ row.name }}</span>
                                    </template>
                                </el-table-column>
                            </el-table>
                            <pagination v-show="orgRoleTotal > 0" :total="orgRoleTotal" :page.sync="listQuery.page" :limit.sync="listQuery.limit" @pagination="getOrgRoleList" :page-sizes="pageSizesRoleSelect" />
                            <div slot="footer" class="dialog-footer">
                                <el-button @click="dialogFormVisible = false">
                                    {{ $t("AbpIdentity['Cancel']") }}
                                </el-button>
                                <el-button type="primary" @click="addRoles()">
                                    {{ $t("AbpIdentity['Save']") }}
                                </el-button>
                            </div>
                        </el-dialog>
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

import {
    getOrganizationsRoot,
    getOrganizationSingleWithDetails,
    getOrganizationSingle,
    getOrganizationsAll,
    getOrganizations,
    getOrganizationChildren,
    getOrgUsers,
    getOrgRoles,
    AddRoles,
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
        OrgTree
    },
    data() {
        return {
            activeName: 'roles',
            tableKey: 0,
            orgRoleList: null,
            orgRoleTotal: 0,
            multipleSelectionRoles: [], // 角色多选
            multipleSelection: [], // // 用来保存当前的选中
            pageSizesRoleSelect: [1, 20, 30, 40, 50, 100],

            listLoading: true,
            listQuery: {
                page: 1,
                limit: 1,
                sort: undefined,
                filter: undefined,
                ouId: undefined
            },

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

        // 根据当前的multipleSelection得到对应选中的pkId
        curSelectedRowIds() {
            let result = [];
            if (this.multipleSelection && this.multipleSelection.length > 0) {
                result = this.multipleSelection.map((user) => user.pkId);
            }
            return result;
        }
    },
    created() {
        this.getOrgRoleList()
        this.getOrgUserList()
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
        getRowKeys(row) {
            // 在使用 reserve-selection 功能与显示树形数据时，该属性是必填的。类型为 String 时，支持多层访问：user.info.id，但不支持 user.info[0].id，此种情况请使用 Function。
            return row.pkId;
        },
        // 监听selection-change获得跨页选中的行的数据
        /**
         * @param selection 选中的rows
         * @param changedRow 变化的row
         */
        handleSelectionChange(selection, changedRow) {
            // 检查有没有新增的，有新增的就push
            if (selection && selection.length > 0) {
                selection.forEach((row) => {
                    if (this.curSelectedRowIds.indexOf(row.pkId) < 0) {
                        this.multipleSelection.push(row);
                    }
                });
            }
            // 如果当前的selection没有changedRow，表示changedRow被cancel了，
            // 如果this.multipleSelection有这一条，需要splice掉
            if (row && selection.indexOf(changedRow) < 0) {
                if (this.curSelectedRowIds.indexOf(changedRow.pkId) > -1) {
                    for (let index = 0; index < this.multipleSelection.length; index++) {
                        if (row.pkId === this.multipleSelection[index].pkId) {
                            this.multipleSelection.splice(index, 1);
                            break;
                        }
                    }
                }
            }
            // 如果当前一条都没有选中，表示都没有选中，则需要把当前页面的rows都遍历一下，splice掉没选中的
            if (selection.length === 0) {
                this.pageinfo.records.forEach((row) => {
                    let index = this.curSelectedRowIds.indexOf(row.pkId);
                    if (index > -1) {
                        this.multipleSelection.splice(index, 1);
                    }
                });
            }
        },
        // 每次分页查询的时候触发 来把已经选中的check一下
        rowMultipleChecked() {
            if (
                this.curSelectedRowIds &&
                this.curSelectedRowIds.length > 0 &&
                this.pageinfo &&
                this.pageinfo.records &&
                this.pageinfo.records.length > 0
            ) {
                this.$nextTick(() => {
                    // 触发一下选中
                    this.pageinfo.records.forEach((row) => {
                        if (this.curSelectedRowIds.indexOf(row.pkId) > -1) {
                            this.$refs["userSelectTable"].toggleRowSelection(row, true);
                        }
                    });
                });
            }
        },
        // clearSelection的实现
        clearSelection() {
            if (this.$refs["userSelectTable"]) {
                this.$refs["userSelectTable"].clearSelection();
            }
        },
        addRoles() {
            if (this.orgData !== null) {
                this.temp.orgId = this.orgData.id
            }
            this.$refs;
            debugger
            // 方法一，通过selection-change 事件获取
            console.log(this.dataonLineListSelections);
            // 方法二，通过 this.$refs 获取
            console.log(this.$refs.multipleTable.selection);

            return;

            // 切换选中状态
            if (rows) {
                rows.forEach(row => {
                    this.$refs.multipleTable.toggleRowSelection(row);
                });
            } else {
                this.$refs.multipleTable.clearSelection();
            }

            var roleIds = null;
            AddRoles(this.orgData.id, this.temp).then(() => {
                this.handleFilter()
                this.dialogFormVisible = false
                this.$notify({
                    title: this.$i18n.t("TigerUI['Success']"),
                    message: this.$i18n.t("TigerUI['SuccessMessage']"),
                    type: 'success',
                    duration: 2000
                })
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
