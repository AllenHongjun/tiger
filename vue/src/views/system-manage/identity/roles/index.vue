<template>
<div class="app-container">
    <el-row :gutter="0">
        <el-col :span="6">
            <org-tree ref="roleOrgTree" :org-tree-node-click="handleOrgTreeNodeClick" />
        </el-col>
        <el-col :span="18">
            <el-row style="margin-bottom: 20px">
                <el-input v-model="listQuery.Filter" placeholder="关键词" style="width: 150px" class="filter-item" />

                <el-button class="filter-item" type="primary" icon="el-icon-search" @click="handleFilter" />

                <el-button type="primary" icon="el-icon-edit" @click="handleCreate">
                    添加
                </el-button>
                <el-button class="filter-item" style="margin-left: 10px;" icon="el-icon-refresh" @click="handleRefresh">
                    刷新
                </el-button>
            </el-row>
            <el-table v-loading="listLoading" :data="list" element-loading-text="Loading" border fit highlight-current-row>
                <el-table-column align="center" label="ID" width="95">
                    <template slot-scope="scope">
                        {{ scope.$index }}
                    </template>
                </el-table-column>
                <el-table-column label="名称" align="center">
                    <template slot-scope="scope">
                        {{ scope.row.name }}
                    </template>
                </el-table-column>
                <el-table-column label="是否默认" align="center" width="95">
                    <template slot-scope="scope">
                        {{ scope.row.isDefault == true ? "是" : "否" }}
                    </template>
                </el-table-column>
                <el-table-column label="是否发布" align="center" width="95">
                    <template slot-scope="scope">
                        {{ scope.row.isPublic == true ? "是" : "否" }}
                    </template>
                </el-table-column>
                <!-- <el-table-column label="是否静态" align="center" width="95">
                        <template slot-scope="scope">
                        {{ scope.row.isStatic == true? '是':'否' }}
                        </template>
                    </el-table-column> -->

                <el-table-column align="center" label="操作" width="400">
                    <template slot-scope="scope">
                        <el-button type="primary" icon="el-icon-edit" @click="handleUpdate(scope.row)">
                            编辑
                        </el-button>
                        &nbsp;&nbsp;
                        <el-button type="primary" @click="handlePermission(scope)">
                            授权
                        </el-button>
                        <el-button type="danger" icon="el-icon-delete" @click="deleteData(scope.row.id)">
                            删除
                        </el-button>
                    </template>
                </el-table-column>
            </el-table>
            <pagination v-show="total > 0" :total="total" :page.sync="listQuery.page" :limit.sync="listQuery.limit" @pagination="fetchData" />

            <el-dialog :visible.sync="dialogVisible" title="角色授权">
                <el-checkbox v-model="allPermissionChecked">授予所有权限</el-checkbox>
                <el-divider></el-divider>
                <el-form label-width="80px" label-position="left">
                    <el-tabs tab-position="left">
                        <el-tab-pane v-for="group in permissionData.groups" :key="group.name" :label="group.displayName">
                            <!-- <h3>{{group.displayName}}</h3>
                            <el-divider></el-divider> -->
                            <el-checkbox v-model="allPermissionChecked">全选</el-checkbox>
                            <el-divider></el-divider>
                            <el-form-item :label="group.displayName" class="permissionTree">
                                <el-tree ref="permissionTree" :data="transformPermissionTree(group.permissions)" :props="treeDefaultProps" show-checkbox :check-strictly="false" node-key="name" :default-expand-all="false" />
                            </el-form-item>
                        </el-tab-pane>
                    </el-tabs>
                </el-form>
                <div style="text-align: right">
                    <el-button type="danger" @click="dialogVisible = false">取消</el-button>
                    <el-button type="primary" @click="updatePermissionData()">确认</el-button>
                </div>
            </el-dialog>

            <el-dialog :title="textMap[dialogStatus]" :visible.sync="dialogRoleFormVisible">
                <el-form ref="dataForm" :rules="rules" :model="temp" label-width="180px" label-position="left">
                    <el-form-item label="组织" prop="orgName">
                        <el-input v-model="orgName" disabled />
                    </el-form-item>
                    <el-form-item label="名称" prop="name">
                        <el-input v-model="temp.name" />
                    </el-form-item>
                    <el-form-item label="是否默认" prop="title">
                        <el-checkbox v-model="temp.isDefault" />
                    </el-form-item>
                    <el-form-item label="是否公开" prop="title">
                        <el-checkbox v-model="temp.isPublic" />
                    </el-form-item>
                </el-form>
                <div style="text-align: right">
                    <el-button type="danger" @click="dialogRoleFormVisible = false">取消</el-button>
                    <el-button type="primary" @click="dialogStatus === 'create' ? createData() : updateData()">确认</el-button>
                </div>
            </el-dialog>
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
    getPermissions,
    updatePermissions
} from '@/api/system-manage/identity/permission'

import {
    getOrgRoles
} from '@/api/system-manage/identity/organization-unit'
import Pagination from '@/components/Pagination' // Secondary package based on el-pagination
import OrgTree from '../components/org-tree'

export default {
    name: 'Role',
    components: {
        Pagination,
        OrgTree
    },
    props: {
        providerName: {
            type: String,
            required: false
        }
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
                isPublic: false
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
            dialogPermissionFormVisible: false,
            allPermissionChecked: false,
            permissionsQuery: {
                providerKey: '',
                providerName: 'R'
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
        this.permissionsQuery.providerName = 'R'
    },
    methods: {
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
            this.$refs.roleOrgTree.$refs.orgTree.setCurrentKey(null)
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
        handlePermission(scope) {
            this.dialogVisible = true
            if (this.permissionsQuery.providerName === 'R') {
                this.permissionsQuery.providerKey = scope.row.name
            } else if (this.permissionsQuery.providerName === 'U') {
                this.permissionsQuery.providerKey = scope.row.id
            }
            getPermissions(this.permissionsQuery).then((response) => {
                this.permissionData = response

                for (const i in this.permissionData.groups) {
                    const keys = []
                    const group = this.permissionData.groups[i]
                    for (const j in group.permissions) {
                        if (group.permissions[j].isGranted) {
                            keys.push(group.permissions[j].name)
                        }
                    }

                    this.$nextTick(() => {

                        this.$refs.permissionTree[i].setCheckedKeys(keys)
                    })
                }
            })
        },
        transformPermissionTree(permissions, name = null) {
            const arr = []
            if (!permissions || !permissions.some((v) => v.parentName === name)) {
                return arr
            }

            const parents = permissions.filter((v) => v.parentName === name)
            for (const i in parents) {
                let label = ''
                if (this.permissionsQuery.providerName === 'R') {
                    label = parents[i].displayName
                } else if (this.permissionsQuery.providerName === 'U') {
                    label =
                        parents[i].displayName +
                        ' ' +
                        parents[i].grantedProviders.map((provider) => {
                            return `${provider.providerName}: ${provider.providerKey}`
                        })
                }
                arr.push({
                    name: parents[i].name,
                    label,
                    disabled: this.isGrantedByOtherProviderName(
                        parents[i].grantedProviders
                    ),
                    children: this.transformPermissionTree(permissions, parents[i].name)
                })
            }
            return arr
        },
        isGrantedByOtherProviderName(grantedProviders) {
            if (grantedProviders.length) {
                return (
                    grantedProviders.findIndex(
                        (p) => p.providerName !== this.permissionsQuery.providerName
                    ) > -1
                )
            }
            return false
        },
        // 更新权限
        updatePermissionData() {
            const tempData = []
            for (const i in this.permissionData.groups) {
                const keys = this.$refs.permissionTree[i].getCheckedKeys()
                const group = this.permissionData.groups[i]
                for (const j in group.permissions) {
                    if (
                        group.permissions[j].isGranted &&
                        !keys.some((v) => v === group.permissions[j].name)
                    ) {
                        tempData.push({
                            isGranted: false,
                            name: group.permissions[j].name
                        })
                    } else if (
                        !group.permissions[j].isGranted &&
                        keys.some((v) => v === group.permissions[j].name)
                    ) {
                        tempData.push({
                            isGranted: true,
                            name: group.permissions[j].name
                        })
                    }
                }
            }
            updatePermissions(this.permissionsQuery, {
                permissions: tempData
            }).then(
                () => {
                    this.dialogPermissionFormVisible = false
                    this.$message({
                        message: '权限添加成功',
                        type: 'success'
                    })
                    // fetchAppConfig(
                    //   this.permissionsQuery.providerKey,
                    //   this.permissionsQuery.providerName
                    // )
                }
            )
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
