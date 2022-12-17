<template>
<div class="app-container" style="padding:0 20px;">
    <div class="filter-container">
        <el-input v-model="filterText" placeholder="查询组织" />
    </div>
    <el-tree ref="orgTree" :data="orgTreeData" :props="orgTreeProps" :filter-node-method="filterOrg" :expand-on-click-node="false" :show-checkbox="showCheckbox" :check-strictly="checkStrictly" node-key="id" highlight-current :default-expand-all="false" @node-click="handleOrgClick" @check-change="checkChange">
        <span class="custom-tree-node" slot-scope="{ node, data }">
            <span>{{ node.label }}</span>
            <span>
                <el-button type="text" @click="() => handleUpdate(data)">
                    编辑
                </el-button>
                <el-button type="text" @click="() => handleCreate(data)">
                    创建子组织
                </el-button>
                <el-button type="text" @click="() => remove(node, data)">
                    删除
                </el-button>
            </span>
        </span>
    </el-tree>

    <el-dialog :title="
        dialogStatus == 'create'? $t('AbpIdentity[\'AddClaim\']'): $t('AbpUi[\'Edit\']')" :visible.sync="dialogFormVisible">
        <el-form ref="dataForm" :rules="rules" :model="temp" label-position="right" label-width="150px">
            <el-form-item :label="$t('AbpIdentity[\'OrganizationUnit:DisplayName\']')" prop="DisplayName">
                <el-input v-model="temp.displayName" />
            </el-form-item>

        </el-form>
        <div slot="footer" class="dialog-footer">
            <el-button @click="dialogFormVisible = false">
                {{ $t("AbpUi['Cancel']") }}
            </el-button>
            <el-button type="primary" @click="dialogStatus === 'create' ? createData() : updateData()">
                {{ $t("AbpUi['Save']") }}
            </el-button>
        </div>
    </el-dialog>
</div>
</template>

<script>
import {
    getOrganizationsRoot,
    getOrganizationSingleWithDetails,
    getOrganizationSingle,
    getOrganizationsAll,
    getOrganizations,
    getOrganizationChildren,
    getOrgUsers,
    getOrgRoles,
    createOrganization,
    updateOrganization,
    deleteOrganization,

    getOrganizationsAllTree,

    getOrganizationsAllWithDetails
} from '@/api/system-manage/identity/organization-unit'
import {
    Tree
} from 'element-ui'
export default {
    name: 'OrgTree',
    mixins: [Tree],
    props: {
        supportSingleChecked: {
            type: Boolean,
            default: false
        },
        orgTreeNodeClick: {
            type: Function,
            default: () => {}
        }
    },
    data() {
        return {
            orgTreeData: null,
            orgTreeProps: {
                label: 'displayName',
                children: 'children',
                disabled: '',
                isLeaf: 'isLeaf'
            },
            treeQuery: {
                sort: 'code',
                filter: undefined
            },
            filterText: '',
            temp: {
                id: undefined,
                displayName: '',
            },
            dialogFormVisible: false,
            dialogStatus: '',

            // 表单验证规则
            rules: {
                name: [{
                        required: true,
                        message: this.$i18n.t("AbpIdentity['The {0} field is required.']", [
                            this.$i18n.t("AbpIdentity['IdentityClaim:Name']")
                        ]),
                        trigger: 'blur'
                    },
                    {
                        // 在abp表单验证模块里面自带的配置规则
                        max: 256,
                        message: this.$i18n.t(
                            "AbpIdentity['The field {0} must be a string with a maximum length of {1}.']",
                            [this.$i18n.t("AbpIdentity['IdentityClaim:Name']"), '256']
                        ),
                        trigger: 'blur'
                    }
                ]
            }
        }
    },
    watch: {
        filterText(val) {
            this.treeQuery.filter = val
            this.$refs.orgTree.filter(val)
        }
    },
    created() {
        this.getOrgs()
    },
    methods: {
        getOrgs() {
            getOrganizationsAllTree(this.treeQuery).then(response => {
                // console.log('orgTreeData', response.items)
                this.orgTreeData = response.items
            })
        },

        // 重置表单
        resetTemp() {
            this.temp = {
                id: undefined,
                name: '',
                required: false,
                isStatic: false,
                regex: '',
                regexDescription: '',
                description: '',
                valueType: 0
            }
        },

        // 点击创建按钮

        handleCreate(data) {
            this.resetTemp()
            this.dialogStatus = 'create'
            this.dialogFormVisible = true
            this.$nextTick(() => {
                this.$refs['dataForm'].clearValidate()
            })

            return;
        },

        // 创建数据
        createData() {
            this.$refs['dataForm'].validate(valid => {
                if (valid) {
                    createOrganization(this.temp).then((res) => {
                        // TODO: bug添加更新组织 页面不会刷新
                        const newChild = {
                            id: res.id,
                            label: res.displayName,
                            children: res.children
                        };
                        if (!this.orgTreeData.children) {
                            this.$set(this.orgTreeData, 'children', []);
                        }
                        this.orgTreeData.push(newChild);
                        this.dialogFormVisible = false
                        this.$notify({
                            title: this.$i18n.t("TigerUi['Success']"),
                            message: this.$i18n.t("TigerUi['SuccessMessage']"),
                            type: 'success',
                            duration: 2000
                        })
                    })
                }
            })
        },

        handleUpdate(row) {
            const {
                id,
                displayName,
                concurrencyStamp
            } = row
            this.currentId = id
            this.temp.displayName = displayName
            this.temp.concurrencyStamp = concurrencyStamp

            this.dialogStatus = 'update'
            this.dialogFormVisible = true

            this.$nextTick(() => {
                this.$refs['dataForm'].clearValidate()
            })

        },

        updateData() {
            this.$refs['dataForm'].validate(valid => {
                if (valid) {
                    updateOrganization(this.currentId, this.temp).then((res) => {
                        
                        // 更新不能用这个
                        // const newChild = {
                        //     id: res.id,
                        //     label: res.displayName,
                        //     children: res.children
                        // };
                        // if (!this.orgTreeData.children) {
                        //     this.$set(this.orgTreeData, 'children', []);
                        // }
                        // this.orgTreeData.children.push(newChild);

                        this.dialogFormVisible = false
                        this.$notify({
                            title: this.$i18n.t("TigerUi['Success']"),
                            message: this.$i18n.t("TigerUi['SuccessMessage']"),
                            type: 'success',
                            duration: 2000
                        })
                    })
                }
            })
        },

        remove(node, data) {
            this.$confirm(
                this.$i18n.t("AbpIdentity['OUDeletionConfirmationMessage']", [
                    node.displayName
                ]),
                this.$i18n.t("AbpIdentity['AreYouSure']"), {
                    confirmButtonText: this.$i18n.t("AbpIdentity['Yes']"),
                    cancelButtonText: this.$i18n.t("AbpIdentity['Cancel']"),
                    type: 'warning'
                }
            ).then(async () => {
                deleteOrganization(node.key).then(() => {
                    // tree组件中找到这个节点并且移除
                    const parent = node.parent;
                    const children = parent.data.children || parent.data;
                    const index = children.findIndex(d => d.id === data.id);
                    children.splice(index, 1);
                    this.$notify({
                        title: this.$i18n.t("TigerUi['Success']"),
                        message: this.$i18n.t("TigerUi['SuccessMessage']"),
                        type: 'success',
                        duration: 2000
                    })
                })
            })
        },

        handleOrgClick(data) {
            this.orgTreeNodeClick(data)
        },
        filterOrg(value, data) {
            if (!value) return true
            return data.displayName.indexOf(value) !== -1
        },
        checkChange(data, checked, indeterminate) {
            // 单个
            let keys = ''
            if (this.supportSingleChecked) {
                if (checked) {
                    console.log(data, checked, indeterminate)
                    keys = this.$refs.orgTree.getCheckedKeys()
                    if (keys.length > 1) {
                        this.$refs.orgTree.setCheckedKeys([])
                        this.$refs.orgTree.setChecked(data, true)
                        keys = this.$refs.orgTree.getCheckedKeys()
                    }
                    console.log('单个-keys:', keys)
                    this.$emit('handleCheckChange', data, keys)
                }
            } else {
                keys = this.$refs.orgTree.getCheckedKeys()
                console.log('多个-keys:', keys)
                this.$emit('handleCheckChange', data, keys)
            }
        },

        // renderContent(h, { node, data, store }) {
        //   return (
        //     <span class="custom-tree-node">
        //       <span>{node.label}</span>
        //       <span>
        //         <el-button size="mini" type="text" on-click={ () => this.append(data) }>Append</el-button>
        //         <el-button size="mini" type="text" on-click={ () => this.remove(node, data) }>Delete</el-button>
        //       </span>
        //     </span>);
        //}
        handleDragStart(node, ev) {
            console.log('drag start', node);
        },
        handleDragEnter(draggingNode, dropNode, ev) {
            console.log('tree drag enter: ', dropNode.label);
        },
        handleDragLeave(draggingNode, dropNode, ev) {
            console.log('tree drag leave: ', dropNode.label);
        },
        handleDragOver(draggingNode, dropNode, ev) {
            console.log('tree drag over: ', dropNode.label);
        },
        handleDragEnd(draggingNode, dropNode, dropType, ev) {
            console.log('tree drag end: ', dropNode && dropNode.label, dropType);
        },
        handleDrop(draggingNode, dropNode, dropType, ev) {
            console.log('tree drop: ', dropNode.label, dropType);
        },
        // allowDrop(draggingNode, dropNode, type) {
        //     if (dropNode.data.label === '二级 3-1') {
        //         return type !== 'inner';
        //     } else {
        //         return true;
        //     }
        // },
        // allowDrag(draggingNode) {
        //     return draggingNode.data.label.indexOf('三级 3-2-2') === -1;
        // },
    }
}
</script>

<style lang="scss" scoped>
.custom-tree-node {
    flex: 1;
    display: flex;
    align-items: center;
    justify-content: space-between;
    font-size: 14px;
    padding-right: 8px;
}
</style>
