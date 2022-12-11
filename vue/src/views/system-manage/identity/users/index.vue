<template>
<div class="app-container">
    <div class="filter-container" style="margin-bottom: 20px">
        <el-input v-model="listQuery.Filter" placeholder="关键词" style="width: 150px" class="filter-item" />

        <el-button class="filter-item" type="primary" icon="el-icon-search" @click="handleFilter">
            搜索
        </el-button>

        <el-button type="primary" icon="el-icon-plus" @click="handleCreate">
            添加
        </el-button>
    </div>

    <el-table v-loading="listLoading" :data="list" element-loading-text="Loading" border fit highlight-current-row>
        
        <el-table-column label="用户名" align="center" width="120">
            <template slot-scope="scope">
                {{ scope.row.userName }}
            </template>
        </el-table-column>
        <el-table-column label="手机号" align="center" width="200">
            <template slot-scope="scope">
                {{ scope.row.phoneNumber }}
            </template>
        </el-table-column>
        <el-table-column label="邮箱" align="center">
            <template slot-scope="scope">
                {{ scope.row.email }}
            </template>
        </el-table-column>
        <el-table-column label="创建时间" align="center" width="200">
            <template slot-scope="scope">
                {{ scope.row.creationTime | formatDate }}
            </template>
        </el-table-column>
        <el-table-column label="最后修改时间" align="center" width="200">
            <template slot-scope="scope">
                {{ scope.row.lastModificationTime | formatDate }}
            </template>
        </el-table-column>
        <el-table-column label="是否删除" align="center">
            <template slot-scope="scope">
                {{ scope.row.isDeleted == true ? '是' : '否' }}
            </template>
        </el-table-column>
        <el-table-column label="能否锁定" align="center">
            <template slot-scope="scope">
                {{ scope.row.lockoutEnabled == true ? '是' : '否' }}
            </template>
        </el-table-column>
        <el-table-column label="锁定结束时间" align="center" width="200">
            <template slot-scope="scope">
                {{ scope.row.lockoutEnd == null ? '' : (scope.row.lockoutEnd | formatDate) }}
            </template>
        </el-table-column>

        <el-table-column align="right" label="操作" width="220">
            <template slot-scope="scope">
                <el-dropdown trigger="click" @command="handleCommand">
                    <el-button type="primary" size="small">
                        操作<i class="el-icon-arrow-down el-icon--right" />
                    </el-button>
                    <el-dropdown-menu slot="dropdown">
                        <el-dropdown-item :command="beforeHandleCommand(scope, 'edit')">编辑</el-dropdown-item>
                        <el-dropdown-item>锁定</el-dropdown-item>
                        <el-dropdown-item :command="beforeHandleCommand(scope, 'updatePermission')">
                            授权
                        </el-dropdown-item>
                        <el-dropdown-item>设置密码</el-dropdown-item>
                        <el-dropdown-item :command="beforeHandleCommand(scope, 'delete')">删除</el-dropdown-item>
                    </el-dropdown-menu>
                </el-dropdown>
            </template>
        </el-table-column>
    </el-table>
    <pagination v-show="total > 0" :total="total" :page.sync="listQuery.page" :limit.sync="listQuery.limit" @pagination="fetchData" />

    <el-dialog :title="textMap[dialogStatus]" :visible.sync="dialogFormVisible">
        <el-form ref="dataForm" :rules="rules" :model="temp" label-width="180px" label-position="left">
            <el-tabs v-model="activeName">
                <el-tab-pane label="用户信息" name="first">
                    <el-form-item label="用户名" prop="userName">
                        <el-input v-model="temp.userName" />
                    </el-form-item>
                    <el-form-item v-if="dialogStatus === 'create'" label="密码" prop="password">
                        <el-input v-model="temp.password" />
                    </el-form-item>
                    <el-form-item label="邮箱" prop="email">
                        <el-input v-model="temp.email" />
                    </el-form-item>
                    <el-form-item label="手机号" prop="phoneNumber">
                        <el-input v-model="temp.phoneNumber" />
                    </el-form-item>
                </el-tab-pane>
                <el-tab-pane label="角色" name="second">
                    <template>
                        <el-checkbox v-model="checkAll" :indeterminate="isIndeterminate" @change="handleCheckAllChange">全选</el-checkbox>
                        <div style="margin: 15px 0" />
                        <el-checkbox-group v-model="checkedRoles" @change="handleCheckedRolesChange">
                            <el-checkbox v-for="role in roles" :key="role" span="4" :label="role">{{ role }}</el-checkbox>
                        </el-checkbox-group>
                    </template>
                </el-tab-pane>
                <el-tab-pane label="组织机构" name="third">
                    <org-tree ref="dialogOrgTree" :show-checkbox="true" :support-single-checked="singleChecked" @handleCheckChange="handleCheckChange" />
                    <el-form-item />
                </el-tab-pane>
            </el-tabs>

            <!-- <el-form-item label="使用共享数据库" prop="title">
          <el-checkbox v-model="checked"></el-checkbox>
        </el-form-item> -->
        </el-form>
        <div style="text-align: right">
            <el-button type="danger" @click="dialogFormVisible = false">取消</el-button>
            <el-button type="primary" @click="dialogStatus === 'create' ? createData() : updateData()">确认</el-button>
        </div>
    </el-dialog>

    <permission-dialog ref="permissionDialog" provider-name="U" />
</div>
</template>

<script>
import {
    getUserList,
    createUserToOrg,
    updateUserToOrg,
    deleteUser,
    getOrganizationsByUserId,
    getAssignableRoles,
    getRolesByUserId,
    getUserRoles
} from '@/api/system-manage/identity/user'

import {
    
} from '@/api/system-manage/identity/role'


// import { parseTime } from '@/utils'
import Pagination from '@/components/Pagination' // Secondary package based on el-pagination
import {
    validEmail,
    validPhone
} from '@/utils/validate'
import PermissionDialog from '../components/permission-dialog'
import OrgTree from '../components/org-tree'

export default {
    name: 'User',
    components: {
        Pagination,
        PermissionDialog,
        OrgTree
    },
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
        var checkPhone = (rule, value, callback) => {
            if (!value) {
                return callback(new Error('电话号码不能为空'))
            }
            setTimeout(() => {
                // Number.isInteger是es6验证数字是否为整数的方法,但是我实际用的时候输入的数字总是识别成字符串
                // 所以我就在前面加了一个+实现隐式转换
                if (!Number.isInteger(+value)) {
                    callback(new Error('请输入数字值'))
                } else {
                    if (validPhone(value)) {
                        callback()
                    } else {
                        callback(new Error('电话号码格式/长度不正确'))
                    }
                }
            }, 100)
        }
        var checkEmail = (rule, value, callback) => {
            if (!value) {
                return callback(new Error('邮箱不能为空'))
            }
            setTimeout(() => {
                if (validEmail(value)) {
                    callback()
                } else {
                    callback(new Error('请输入正确的邮箱格式'))
                }
            }, 100)
        }

        return {
            list: null,
            listLoading: true,
            total: 0,
            listQuery: {
                Filter: '',
                page: 1,
                limit: 10,
                Sorting: ''
            },
            checkAll: false,
            checkedRoles: [],
            roles: [],
            isIndeterminate: true,
            activeName: 'first',
            singleChecked: false,
            temp: {
                id: '',
                userName: '',
                password: '',
                name: '',
                surname: '',
                email: '',
                lockoutEnabled: '',
                phoneNumber: '',
                roleNames: [],
                orgIds: []
            },
            dialogFormVisible: false,
            dialogStatus: '',
            textMap: {
                update: '编辑',
                create: '添加'
            },
            rules: {
                userName: [{
                        required: true,
                        message: '请输入名称',
                        trigger: 'blur'
                    },
                    {
                        min: 3,
                        max: 15,
                        message: '长度在 3 到 15 个字符',
                        trigger: 'blur'
                    }
                ],
                email: [{
                    required: true,
                    trigger: 'blur',
                    validator: checkEmail
                }],
                phoneNumber: [{
                    required: true,
                    trigger: 'blur',
                    validator: checkPhone
                }],
                password: [{
                    required: true,
                    message: '请输入密码',
                    trigger: 'blur'
                }]
            }
        }
    },
    created() {
        this.fetchData()
    },
    methods: {
        fetchData() {
            this.listLoading = true
            getUserList(this.listQuery).then((response) => {
                this.list = response.items
                this.total = response.totalCount
                this.listLoading = false
            })
        },
        fetchRoles() {
            getAssignableRoles().then((response) => {
                var obj = response.items
                var tempArr = []
                var arr = Object.keys(obj).forEach(function (key) {
                    tempArr.push(obj[key].name)
                })
                this.roles = tempArr
            })
        },
        fetchGetUserRoles(id) {
            getRolesByUserId(id).then((response) => {
                var obj = response.items
                console.log('obj', obj)
                var tempArr = []
                if (obj.length > 0) {
                    var arr = Object.keys(obj).forEach(function (key) {
                        tempArr.push(obj[key].name)
                    })
                }

                this.checkedRoles = tempArr
            })
        },
        handleFilter() {
            this.listQuery.page = 1
            this.fetchData()
        },
        handleCheckAllChange(val) {
            this.checkedRoles = val ? this.roles : []
            this.isIndeterminate = false
        },
        handleCheckedRolesChange(value) {
            const checkedCount = value.length
            this.checkAll = checkedCount === this.roles.length
            this.isIndeterminate =
                checkedCount > 0 && checkedCount < this.roles.length
        },
        handleCommand(param) {
            switch (param.command) {
                case 'delete':
                    this.deleteData(param.scope.row.id)
                    break
                case 'edit':
                    this.handleUpdate(param.scope.row)
                    break
                case 'updatePermission':
                    this.handleUpdatePermission(param.scope.row)
                    break
                default:
                    // this.handlePasswd(command.scope.row)
                    break
            }
        },
        beforeHandleCommand(scope, command) {
            return {
                scope: scope,
                command: command
            }
        },
        resetTemp() {
            this.temp = {
                name: '',
                adminEmailAddress: '',
                adminPassword: ''
            }
        },
        handleCreate() {
            this.resetTemp()
            this.dialogStatus = 'create'
            this.dialogFormVisible = true

            // create org
            this.singleChecked = false
            this.temp.roleNames = []
            this.fetchRoles()
            this.$nextTick(() => {
                this.$refs['dataForm'].clearValidate()
            })
        },
        createData() {
            this.$refs['dataForm'].validate((valid) => {
                if (valid) {
                    console.log(this.temp.roleNames)
                    this.temp.roleNames = this.checkedRoles
                    createUserToOrg(this.temp).then(() => {
                        this.list.unshift(this.temp)
                        this.dialogFormVisible = false
                        this.$notify({
                            title: '成功',
                            message: '添加成功',
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
            // update can check many
            this.singleChecked = false
            this.fetchRoles()
            this.fetchGetUserRoles(row.id)

            // handle org
            getOrganizationsByUserId(row.id).then(response => {
                const ids = this.temp.orgIds = response.items.map(item => item.id)
                // 2.bind checked
                this.$refs.dialogOrgTree.$refs.orgTree.setCheckedKeys(ids)
            })

            this.$nextTick(() => {
                // 1.reset dialog tree
                this.$refs.dialogOrgTree.$refs.orgTree.setCheckedKeys([])
                this.$refs['dataForm'].clearValidate()
            })
        },
        updateData() {
            this.$refs['dataForm'].validate((valid) => {
                if (valid) {
                    this.temp.roleNames = this.checkedRoles
                    console.log(this.temp)
                    const tempData = Object.assign({}, this.temp)
                    updateUserToOrg(this.temp).then(() => {
                        const index = this.list.findIndex((v) => v.id === this.temp.id)
                        this.list.splice(index, 1, this.temp)
                        this.dialogFormVisible = false
                        this.$notify({
                            title: '成功',
                            message: '修改成功',
                            type: 'success',
                            duration: 2000
                        })
                    })
                }
            })
        },
        handleUpdatePermission(row) {
            console.log('row', row)
            // 用户授权
            this.$refs['permissionDialog'].handleUpdatePermission(row)
        },
        deleteData(id) {
            console.log('delete')
            this.$confirm('此操作将永久删除数据, 是否继续?', '提示', {
                    confirmButtonText: '确定',
                    cancelButtonText: '取消',
                    type: 'warning'
                })
                .then(() => {
                    deleteUser(id)
                        .then((response) => {
                            const index = this.list.findIndex((v) => v.id === id)
                            this.list.splice(index, 1)
                            this.$message({
                                message: '删除成功',
                                type: 'success'
                            })
                        })
                        .catch((err) => {
                            console.log(err)
                        })
                })
                .catch(() => {
                    this.$message({
                        type: 'info',
                        message: '已取消删除'
                    })
                })
        },
        handleCheckChange(data, orgIds) {
            // singleChecked
            if (orgIds) {
                this.temp.orgIds = orgIds
            }
        }
    }
}
</script>

<style lang="scss" scoped>
.el-checkbox {
    width: 60px;
}
</style>
