<template>
<div class="app-container">
    <div class="filter-container">
        <!-- <el-input
        v-model="listQuery.filter"
        :placeholder="$t('AbpUi[\'PagerSearch\']')"
        style="width: 200px;"
        class="filter-item"
        @keyup.enter.native="handleRefresh"
      /> -->
        <el-button v-if="checkPermission('AbpIdentity.OrganizationUnits.Create')" class="filter-item" style="margin-left: 10px;" type="primary" icon="el-icon-plus" @click="handleCreate">
            {{ $t("AbpIdentity['OrganizationUnit:New']") }}
        </el-button>
    </div>
    <el-table :data="list" :load="loadChildren" :tree-props="{children: 'children', hasChildren: 'hasChildren'}" style="width: 100%" row-key="id" border fit highlight-current-row lazy>
        <el-table-column :label="$t('AbpIdentity[\'OrganizationUnit:Code\']')" prop="code">
            <template slot-scope="{ row }">
                <span>{{ row.code }}</span>
            </template>
        </el-table-column>
        <el-table-column :label="$t('AbpIdentity[\'OrganizationUnit:DisplayName\']')" prop="displayName">
            <template slot-scope="{ row }">
                <span>{{ row.displayName }}</span>
            </template>
        </el-table-column>
        <el-table-column :label="$t('AbpIdentity[\'Actions\']')" align="center" width="380" class-name="small-padding fixed-width">
            <template slot-scope="{ row }">
                <el-button v-if="checkPermission('AbpIdentity.OrganizationUnits.Create')"  type="success"  @click="handleCreate(row)">
                    {{ $t("AbpIdentity['OrganizationUnit:AddChildren']") }}
                </el-button>
                <el-button v-if="checkPermission('AbpIdentity.OrganizationUnits.Update')"  type="primary" icon="el-icon-edit" @click="handleUpdate(row)">
                    {{ $t("AbpIdentity['Edit']") }}
                </el-button>
                <el-button v-if="checkPermission('AbpIdentity.OrganizationUnits.Delete')"  type="danger" icon="el-icon-delete" @click="handleDelete(row)">
                    {{ $t("AbpIdentity['Delete']") }}
                </el-button>
            </template>
        </el-table-column>
    </el-table>

    <el-dialog :title="dialogStatus==='create'?
        $t('AbpIdentity[\'OrganizationUnit:New\']') :
        dialogStatus==='createChild'?
          $t('AbpIdentity[\'OrganizationUnit:AddChildren\']') :
          $t('AbpIdentity[\'Edit\']')" :visible.sync="dialogFormVisible">
        <el-form ref="dataForm" :model="temp" label-position="right" label-width="120px">
            <el-form-item v-if="currentParentName!==''" :label="$t('AbpIdentity[\'OrganizationUnit:ParentDisplayName\']')">
                <el-input v-model="currentParentName" disabled />
            </el-form-item>
            <el-form-item prop="displayName" :label="$t('AbpIdentity[\'OrganizationUnit:DisplayName\']')">
                <el-input v-model="temp.displayName" />
            </el-form-item>
        </el-form>
        <div slot="footer" class="dialog-footer">
            <el-button @click="dialogFormVisible = false">
                {{ $t("AbpIdentity['Cancel']") }}
            </el-button>
            <el-button type="primary" @click="dialogStatus === 'create' ||
            dialogStatus === 'createChild' ?
            createData() : updateData()">
                {{ $t("AbpIdentity['Save']") }}
            </el-button>
        </div>
    </el-dialog>

</div>
</template>

<script>
import {
    // getOrganizations,
    getOrganizationsRoot,
    getOrganizationChildren,
    createOrganization,
    updateOrganization,
    deleteOrganization
} from '@/api/system-manage/identity/organization-unit'
import {
    checkPermission
} from '@/utils/abp'
export default {
    name: 'Organizations',
    data() {
        return {
            tableKey: 0,
            list: null,
            listLoading: true,
            currentId: '',
            currentParentName: '',
            temp: {
                parentId: null,
                displayName: ''
            },
            dialogFormVisible: false,
            dialogStatus: ''
        }
    },
    created() {
        this.getList()
    },
    methods: {
        checkPermission,
        getList() {
            this.listLoading = true
            // 这里会出现感觉是重新刷新了(给人感觉不好的感觉),后期考虑下通过level进行控制返回层架数据,
            this.list = []
            getOrganizationsRoot().then(response => {
                this.list = this.processingChildrenLeaf(response.items)
                this.listLoading = false
            })
        },
        loadChildren(row, treeNode, resolve) {
            getOrganizationChildren(row.id).then(response => {
                resolve(this.processingChildrenLeaf(response))
            })
        },
        handleCreate(row) {
            this.resetTemp()
            if (!row.id) {
                this.dialogStatus = 'create'
                this.currentParentName = ''
            } else {
                this.dialogStatus = 'createChild'
                this.temp.parentId = row.id
                this.currentParentName = `${row.displayName}(${row.code})`
            }
            this.dialogFormVisible = true
            this.$nextTick(() => {
                this.$refs['dataForm'].clearValidate()
            })
        },
        createData() {
            this.$refs['dataForm'].validate(valid => {
                if (valid) {
                    createOrganization(this.temp).then(() => {
                        this.handleRefresh()
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
                    updateOrganization(this.currentId, this.temp).then(() => {
                        this.handleRefresh(false)
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
        handleDelete(row, index) {
            this.$confirm(
                this.$i18n.t("AbpIdentity['OUDeletionConfirmationMessage']", [
                    row.displayName
                ]),
                this.$i18n.t("AbpIdentity['AreYouSure']"), {
                    confirmButtonText: this.$i18n.t("AbpIdentity['Yes']"),
                    cancelButtonText: this.$i18n.t("AbpIdentity['Cancel']"),
                    type: 'warning'
                }
            ).then(async () => {
                deleteOrganization(row.id).then(() => {
                    this.handleRefresh()
                    this.$notify({
                        title: this.$i18n.t("TigerUi['Success']"),
                        message: this.$i18n.t("TigerUi['SuccessMessage']"),
                        type: 'success',
                        duration: 2000
                    })
                })
            })
        },
        handleRefresh(firstPage = true) {
            // if (firstPage) this.listQuery.page = 1
            this.getList()
        },
        resetTemp() {
            this.currentId = ''
            this.currentParentName = ''
            this.temp = {
                parentId: null,
                displayName: ''
            }
        },
        processingChildrenLeaf(response) {
            // 统一处理返回值
            return response.map(item => {
                if (item.children) {
                    item.children = []
                }
                item.hasChildren = !item.isLeaf
                return item
            })
        }
    }
}
</script>
