<template>
<div class="app-container">
    <!-- 组织选择关联多个角色 -->
    <el-dialog :title="$t('AbpIdentity[\'OrganizationUnit:SelectRoles\']')" :visible.sync="dialogFormVisible">
        <!--@select-all="handleSelectionChange" @selection-change="handleSelectionChange" :row-key="getRowKeys"  -->
        <el-table ref="roleSelectTable" :key="tableKey" v-loading="listLoading" :data="list" border fit highlight-current-row style="width: 100%;" @sort-change="sortChange">
            <el-table-column type="selection" width="55" :reserve-selection="true">
            </el-table-column>
            <el-table-column :label="$t('AbpIdentity[\'RoleName\']')" prop="name" sortable align="left">
                <template slot-scope="{ row }">
                    <span>{{ row.name }}</span>
                </template>
            </el-table-column>
        </el-table>
        <pagination v-show="total > 0" :total="total" :page.sync="listQuery.page" :limit.sync="listQuery.limit" @pagination="getOrgRoleList" :page-sizes="pageSizesSelect" />
        <div slot="footer" class="dialog-footer">
            <el-button @click="dialogFormVisible = false">
                {{ $t("AbpIdentity['Cancel']") }}
            </el-button>
            <el-button type="primary" @click="addRoles()">
                {{ $t("AbpIdentity['Save']") }}
            </el-button>
        </div>
    </el-dialog>

</div>
</template>

<script>
import Pagination from '@/components/Pagination'
import {
    getOrgRoles,
    AddRoles
} from '@/api/system-manage/identity/organization-unit'

var row;

export default {
    name: 'AddRolesDialog',
    components: {
        Pagination,
    },
    props: {
        // 定义props属性
        // dialogRoleFormVisible: {
        //     type:Boolean,
        //     default:false
        // }
        // 是否显示dialog + 用户选择的table
        ouId: {
            type: Boolean,
        },
        refreshParentRoles: {
            type: Function,
            default: null,
        },
        // 当前已经选中的用户
        curSelectedRoles: {
            type: Array,
            default: null,
        }
    },
    computed: {
        // 根据当前的multipleSelection得到对应选中的pkId
        curSelectedRowIds() {
            let result = [];
            if (this.multipleSelection && this.multipleSelection.length > 0) {
                result = this.multipleSelection.map((role) => role.pkId);
            }
            return result;
        }
    },
    // 监听table的显示或者隐藏，初始化multipleSelection的值
    watch: {
        showDialog: {
            handler(newValue) {
                if (newValue) {
                    this.multipleSelection = this.$_.cloneDeep(this.curSelectedUsers);
                    this.rowMultipleChecked();
                } else {
                    this.clearSelection();
                }
            },
            immediate: true,
            deep: true,
        },
    },
    created() {
        this.getOrgRoleList()
    },
    data() {
        return {
            activeName: 'roles',
            tableKey: 0,
            list: null,
            total: 0,
            multipleSelection: [], // // 用来保存当前的选中
            pageSizesSelect: [5, 20, 30, 40, 50, 100],
            listLoading: true,
            listQuery: {
                page: 1,
                limit: 10,
                sort: undefined,
                filter: undefined,
                ouId: undefined
            },

            orgData: undefined,

            dialogFormVisible: false,
            dialogStatus: '',
        }
    },
    methods: {
        getOrgRoleList() {
            this.listLoading = true
            getOrgRoles(this.listQuery).then(response => {
                this.list = response.items
                this.total = response.totalCount
                this.listLoading = false
            })
        },
        sortChange(data) {
            const {
                prop,
                order
            } = data
            this.listQuery.sort = order ? `${prop} ${order}` : undefined
            this.handleFilter()
        },
        handleFilter(firstPage = true) {
            if (firstPage) this.listQuery.page = 1
            this.getOrgRoleList()
        },
        handleRefresh() {
            this.listQuery.ouId = undefined
            this.$refs.roleOrgTree.$refs.orgTree.setCurrentKey(null)
            this.orgData = null
            this.handleFilter()
        },
        handleAddRoles(orgData) {
            this.dialogFormVisible = true;
            this.orgData = orgData;
        },
        addRoles() {
            var roleIds = [];
            this.$refs.roleSelectTable.selection.forEach((value, index) => {
                roleIds.push(value.id);
            })
            AddRoles(this.orgData.id, {
                "roleIds": roleIds
            }).then(() => {
                // 调用父类刷新的方法 通过父组件传入 prop方法
                this.refreshParentRoles()
                this.dialogFormVisible = false
                // 清空选中
                this.clearSelection();
                this.$notify({
                    title: this.$i18n.t("TigerUi['Success']"),
                    message: this.$i18n.t("TigerUi['SuccessMessage']"),
                    type: 'success',
                    duration: 2000
                })
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
            debugger;
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
                this.list.forEach((row) => {
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
                this.list &&
                this.list.length > 0
            ) {
                this.$nextTick(() => {
                    // 触发一下选中
                    this.list.forEach((row) => {
                        if (this.curSelectedRowIds.indexOf(row.pkId) > -1) {
                            this.$refs["roleSelectTable"].toggleRowSelection(row, true);
                        }
                    });
                });
            }
        },
        // clearSelection的实现
        clearSelection() {
            if (this.$refs["roleSelectTable"]) {
                this.$refs["roleSelectTable"].clearSelection();
            }
        }
    }
}
</script>

<style scoped>
.line {
    text-align: center;
}
</style>
