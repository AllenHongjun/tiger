<template>
  <div class="app-container">
    <el-row :gutter="20">
      <el-col :span="8">
        <div class="grid-content">
          <el-card class="box-card">
            <el-row>
              <el-col :span="16">
                <h3 class="grid-content">{{ $t('AbpIdentity["OrganizationUnit:Tree"]') }}</h3>
              </el-col>
            </el-row>
            <el-row>
              <div class="block">
                <org-tree ref="roleOrgTree" :org-tree-node-click="handleOrgTreeNodeClick" />
              </div>
            </el-row>
          </el-card>

        </div>
      </el-col>
      <el-col :span="16">
        <div class="grid-content">
          <el-tabs v-model="activeName" type="border-card">
            <el-tab-pane v-if="checkPermission('AbpIdentity.OrganizationUnits.ManageUsers')" :label="$t('AbpIdentity[\'OrganizationUnit:Members\']')" name="users">
              <div v-if="orgData">
                <el-row>
                  <el-col :span="18">
                    <h3 type="info">{{ orgData.displayName }}</h3>
                  </el-col>
                  <el-col :span="6">
                    <el-button v-if="checkPermission('AbpIdentity.Users.Create')" type="primary" icon="el-icon-edit" @click="handleAddUsers">
                      {{ $t("AbpIdentity['OrganizationUnit:AddMember']") }}
                    </el-button>
                    <el-button icon="el-icon-refresh" @click="handleRefreshRoles">
                      {{ $t("AbpIdentity['Refresh']") }}
                    </el-button>
                  </el-col>
                </el-row>

                <el-table :key="tableKey" v-loading="listLoading" :data="orgUserList" border fit highlight-current-row style="width: 100%" max-height="600" @sort-change="sortChangeUsers">
                  <el-table-column :label="$t('AbpIdentity[\'UserName\']')" prop="name" sortable align="left">
                    <template slot-scope="{ row }">
                      <span>{{ row.userName }}</span>
                      <el-tag v-if="row.name">{{ row.surname + row.name }}</el-tag>
                    </template>
                  </el-table-column>
                  <el-table-column :label="$t('AbpIdentity[\'EmailAddress\']')" prop="email" sortable align="center">
                    <template slot-scope="{ row }">
                      <el-tag type="info">{{ row.email }}</el-tag>
                    </template>
                  </el-table-column>
                  <el-table-column :label="$t('AbpIdentity[\'Actions\']')" align="center" width="300" class-name="small-padding fixed-width">
                    <template slot-scope="{ row, $index }">
                      <el-button type="danger" @click="handleRemoveUser(row, $index)">
                        {{ $t("AbpIdentity['Delete']") }}
                      </el-button>
                    </template>
                  </el-table-column>
                </el-table>

                <pagination v-show="orgUserTotal > 0" :total="orgUserTotal" :page.sync="listQueryUsers.page" :limit.sync="listQueryUsers.limit" @pagination="getOrgUserList" />

                <!-- 组织选择关联多个用户 -->
                <add-users ref="addUsersDialog" :refresh-parent-users="handleRefreshUsers" :ou-id="orgData.id" />
              </div>
              <el-row v-else>
                <p style="color: #606266">选择一个组织机构来查看用户</p>
              </el-row>
            </el-tab-pane>
            <el-tab-pane v-if="checkPermission('AbpIdentity.OrganizationUnits.ManageRoles')" :label="$t('AbpIdentity[\'OrganizationUnit:Roles\']')" name="roles">
              <div v-if="orgData">
                <el-row>
                  <el-col :span="5" :offset="19">
                    <el-button v-if="checkPermission('AbpIdentity.Roles.Create')" class="filter-item" style="margin-left: 10px" type="primary" icon="el-icon-edit" @click="handleAddRoles">
                      {{ $t("AbpIdentity['OrganizationUnit:AddRole']") }}
                    </el-button>
                    <el-button class="filter-item" style="margin-left: 10px;" icon="el-icon-refresh" @click="handleRefreshRoles">
                      {{ $t("AbpIdentity['Refresh']") }}
                    </el-button>
                  </el-col>
                </el-row>

                <el-table :key="tableKey" v-loading="listLoading" :data="orgRoleList" border fit highlight-current-row style="width: 100%" @sort-change="sortChangeRoles">
                  <el-table-column :label="$t('AbpIdentity[\'RoleName\']')" prop="name" sortable align="center">
                    <template slot-scope="{ row }">
                      <span>{{ row.name }}</span>
                    </template>
                  </el-table-column>
                  <el-table-column :label="$t('AbpIdentity[\'Actions\']')" align="center" width="300" class-name="small-padding fixed-width">
                    <template slot-scope="{ row, $index }">
                      <el-button v-if="checkPermission('AbpIdentity.Roles.Delete')" type="danger" @click="handleRemoveRole(row, $index)">
                        {{ $t("AbpIdentity['Delete']") }}
                      </el-button>
                    </template>
                  </el-table-column>
                </el-table>

                <pagination v-show="orgRoleTotal > 0" :total="orgRoleTotal" :page.sync="listQueryRoles.page" :limit.sync="listQueryRoles.limit" @pagination="getOrgRoleList" />

                <!-- 组织选择关联多个角色 -->
                <add-roles ref="addRolesDialog" :refresh-parent-roles="handleRefreshRoles" :ou-id="orgData.id" />
              </div>
              <el-row v-else>
                <p style="color: #606266">选择一个组织机构来查看角色</p>
              </el-row>
            </el-tab-pane>
          </el-tabs>
        </div>
      </el-col>
    </el-row>
  </div>
</template>

<script>
import Pagination from '@/components/Pagination/index.vue'
import OrgTree from '../components/OrgTree.vue'
import AddRoles from './components/AddRoles.vue'
import AddUsers from './components/AddUsers.vue'

import {
  getOrgUsers,
  removeUser,
  getOrgRoles,
  removeRole
} from '@/api/system-manage/identity/organization-unit'

import { checkPermission } from '@/utils/abp'

export default {
  name: 'OrganizationUnits',
  components: {
    Pagination,
    OrgTree,
    AddRoles,
    AddUsers
  },
  data() {
    return {
      activeName: 'users',
      tableKey: 0,
      listLoading: true,
      orgData: null,

      orgUserList: null,
      orgUserTotal: 0,
      listQueryUsers: {
        page: 1,
        limit: 50,
        sort: undefined,
        filter: undefined,
        ouId: undefined
      },
      dialogUserFormVisible: false,

      orgRoleList: null,
      orgRoleTotal: 0,
      listQueryRoles: {
        page: 1,
        limit: 50,
        sort: undefined,
        filter: undefined,
        ouId: undefined
      },
      dialogRoleFormVisible: false

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
  created() {},
  methods: {
    checkPermission,
    handleOrgTreeNodeClick(data) {
      if (data.id) {
        this.listQueryRoles.ouId = data.id
        this.listQueryUsers.ouId = data.id
        this.orgData = data
        this.handleFilterRoles()
        this.handleFilterUsers()
      }
    },
    getOrgUserList() {
      this.listLoading = true
      getOrgUsers(this.listQueryUsers).then((response) => {
        this.orgUserList = response.items
        this.orgUserTotal = response.totalCount
        this.listLoading = false
      })
    },
    handleFilterUsers() {
      this.listQueryUsers.page = 1
      this.getOrgUserList()
    },

    sortChangeUsers(data) {
      const {
        prop,
        order
      } = data
      this.listQueryUsers.sort = order ? `${prop} ${order}` : undefined
      this.handleFilterUsers()
    },

    handleRefreshUsers() {
      this.handleFilterUsers()
    },

    handleAddUsers() {
      this.$refs['addUsersDialog'].handleAddUsers(this.orgData)
    },
    handleRemoveUser(row, index) {
      this.$confirm(
        this.$i18n.t("AbpIdentity['OrganizationUnit:AreYouSureRemoveUser']", [
          row.name
        ]),
        this.$i18n.t("AbpIdentity['AreYouSure']"), {
          confirmButtonText: this.$i18n.t("AbpIdentity['Yes']"),
          cancelButtonText: this.$i18n.t("AbpIdentity['Cancel']"),
          type: 'warning'
        }
      ).then(async() => {
        removeUser(this.orgData.id, {
          userId: row.id
        }).then(() => {
          this.handleFilterUsers()
          this.$notify({
            title: this.$i18n.t("TigerUi['Success']"),
            message: this.$i18n.t("TigerUi['SuccessMessage']"),
            type: 'success',
            duration: 2000
          })
        })
      })
    },

    getOrgRoleList() {
      this.listLoading = true
      getOrgRoles(this.listQueryRoles).then((response) => {
        this.orgRoleList = response.items
        this.orgRoleTotal = response.totalCount
        this.listLoading = false
      })
    },
    handleFilterRoles() {
      this.listQueryRoles.page = 1
      this.getOrgRoleList()
    },

    sortChangeRoles(data) {
      const {
        prop,
        order
      } = data
      this.listQueryRoles.sort = order ? `${prop} ${order}` : undefined
      this.handleFilterRoles()
    },

    handleRefreshRoles() {
      this.handleFilterRoles()
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
      ).then(async() => {
        removeRole(this.orgData.id, {
          roleId: row.id
        }).then(() => {
          this.handleFilterRoles()
          this.$notify({
            title: this.$i18n.t("TigerUi['Success']"),
            message: this.$i18n.t("TigerUi['SuccessMessage']"),
            type: 'success',
            duration: 2000
          })
        })
      })
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

.el-table .warning-row {
    background: oldlace;
}

.el-table .success-row {
    background: #f0f9eb;
}

.el-tab-pane .h3 {
    margin: 0;
}

h3{
    margin: 0px;
}
.el-tag{
  margin-left: 8px;
}
</style>
