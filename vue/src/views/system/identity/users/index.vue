<template>
  <div class="app-container">

    <div class="filter-container" style="margin-bottom:10px;">
      <el-form ref="userQueryForm" label-position="left" label-width="100px" :model="listQuery">
        <!-- 增加没一列之间的间隔 -->
        <el-row :gutter="20">
          <el-col :span="4">
            <el-form-item prop="filter" label="查询">
              <el-input v-model="listQuery.filter" placeholder="请输入用户名/手机号/邮箱" />
            </el-form-item>
          </el-col>
          <el-col :span="4">
            <el-form-item prop="roleId" :label="$t('AbpIdentity.Roles')">
              <el-select v-model="listQuery.roleId" class="filter-item" :placeholder="$t('AbpIdentity.Roles')" clearable>
                <el-option v-for="item in roleOptions" :key="item.id" :label="item.name" :value="item.id" />
              </el-select>
            </el-form-item>
          </el-col>
          <el-col :span="4">
            <el-form-item prop="organizationUnitId" label="组织">
              <el-select v-model="listQuery.organizationUnitId" class="filter-item" placeholder="请选择组织" clearable>
                <el-option v-for="item in orgOptions" :key="item.id" :label="item.displayName" :value="item.id" />
              </el-select>
            </el-form-item>
          </el-col>
          <el-col :span="4">
            <el-form-item prop="emailConfirmed" label="邮箱确认">
              <el-select v-model="listQuery.emailConfirmed" class="filter-item" placeholder="请选择" clearable>
                <el-option label="是" :value="true" />
                <el-option label="否" :value="false" />
              </el-select>
            </el-form-item>
          </el-col>
          <el-col :span="4">
            <el-form-item prop="isExternal" label="外部用户">
              <el-select v-model="listQuery.isExternal" class="filter-item" placeholder="请选择" clearable>
                <el-option label="是" :value="true" />
                <el-option label="否" :value="false" />
              </el-select>
            </el-form-item>
          </el-col>
          <!-- <el-col :span="4">
            <el-form-item prop="userName" label="用户名">
              <el-input v-model="listQuery.userName" placeholder="用户名" />
            </el-form-item>
          </el-col>
          <el-col :span="4">
            <el-form-item prop="phoneNumber" label="手机号">
              <el-input v-model="listQuery.phoneNumber" placeholder="手机号" />
            </el-form-item>
          </el-col> -->

          <el-col :span="4">
            <el-button-group>
              <el-button type="primary" icon="el-icon-search" @click="handleFilter">
                {{ $t('AbpIdentity.Search') }}
              </el-button>
              <el-button type="reset" icon="el-icon-remove-outline" @click="resetQueryForm">
                {{ $t('AbpIdentity.Reset') }}
              </el-button>
              <el-link type="info" :underline="false" style="margin-left: 8px;line-height: 28px;" @click="toggleAdvanced">
                {{ advanced ? '收起' : '展开' }}
                <i :class="advanced ? 'el-icon-arrow-up' : 'el-icon-arrow-down'" />
              </el-link>
            </el-button-group>
          </el-col>
        </el-row>

        <el-collapse-transition>
          <div v-show="advanced">
            <el-row :gutter="10">
              <el-col :span="8">
                <el-form-item prop="queryCreationTime" label="创建时间">
                  <el-date-picker v-model="queryCreationTime" type="datetimerange" value-format="yyyy-MM-dd HH:mm:ss" align="left" unlink-panels :picker-options="pickerOptions" :start-placeholder="$t('AbpIdentity[\'StartTime\']')" :end-placeholder="$t('AbpIdentity[\'EndTime\']')" :default-time="['00:00:00', '23:59:59']" @change="datePickerChange" />
                </el-form-item>
              </el-col>
              <el-col :span="8">
                <el-form-item prop="queryModifitionTime" label="修改时间">
                  <el-date-picker v-model="queryModifitionTime" type="datetimerange" value-format="yyyy-MM-dd HH:mm:ss" align="left" unlink-panels :picker-options="pickerOptions" :start-placeholder="$t('AbpIdentity[\'StartTime\']')" :end-placeholder="$t('AbpIdentity[\'EndTime\']')" :default-time="['00:00:00', '23:59:59']" @change="datePickerChange" />
                </el-form-item>
              </el-col>
              <el-col :span="4">
                <el-form-item prop="isLockedOut" label="锁定">
                  <el-select v-model="listQuery.isLockedOut" class="filter-item" placeholder="请选择" clearable>
                    <el-option label="是" :value="true" />
                    <el-option label="否" :value="false" />
                  </el-select>
                </el-form-item>
              </el-col>
              <!-- <el-col :span="4">
                <el-form-item prop="notActive" label="启用">
                  <el-select v-model="listQuery.notActive" class="filter-item" placeholder="请选择" clearable>
                    <el-option label="是" :value="true" />
                    <el-option label="否" :value="false" />
                  </el-select>
                </el-form-item>
              </el-col> -->

              <!-- <el-col :span="4">
                <el-form-item prop="minModifitionTime" label="修改时间">
                  <el-input v-model="listQuery.minModifitionTime" placeholder="修改时间" />
                </el-form-item>
              </el-col> -->

            </el-row>

          </div>
        </el-collapse-transition>

        <el-row>
          <el-col :span="24">
            <el-button-group style="float:left">
              <el-button type="primary" icon="el-icon-plus" @click="handleCreate">
                {{ $t("AbpIdentity['NewUser']") }}
              </el-button>
              <el-button type="primary" icon="el-icon-refresh" @click="handleRefresh">
                {{ $t("AbpIdentity['Refresh']") }}
              </el-button>
              <el-button type="primary" icon="el-icon-import">
                导入
              </el-button>
              <el-button type="primary" icon="el-icon-download" :loading="downloadLoading" @click="handleDownload">
                导出
              </el-button>
            </el-button-group>

          </el-col>
        </el-row>
      </el-form>
    </div>

    <el-table v-loading="listLoading" :data="list" element-loading-text="Loading" border fit highlight-current-row stripe :default-sort="{prop:'creationTime', order: 'descending'}" @sort-change="sortChange">
      <el-table-column type="selection" width="55" align="center" />
      <el-table-column type="index" width="50" align="center" />
      <el-table-column :label="$t('AbpIdentity[\'UserName\']')" align="center" width="120" prop="userName" sortable="custom">
        <template slot-scope="scope">
          {{ scope.row.userName }}
          <el-tag v-if="scope.row.lockoutEnd && (new Date(scope.row.lockoutEnd)) >= new Date()" type="danger" class="el-icon-lock" title="此用户已被锁定. 要解锁,请单击“操作”,然后单击“解锁”." />
        </template>
      </el-table-column>
      <el-table-column :label="$t('AbpIdentity[\'EmailAddress\']')" align="center" width="180" prop="email" sortable="custom">
        <template slot-scope="scope">
          {{ scope.row.email }}
        </template>
      </el-table-column>
      <el-table-column :label="$t('AbpIdentity[\'PhoneNumber\']')" align="center" width="200" prop="phoneNumber" sortable="custom">
        <template slot-scope="scope">
          {{ scope.row.phoneNumber }}
        </template>
      </el-table-column>

      <el-table-column :label="$t('AbpIdentity[\'DisplayName:Surname\']') + $t('AbpIdentity[\'DisplayName:Name\']')" align="center" width="100">
        <template slot-scope="scope">
          {{ scope.row.surname }}{{ scope.row.name }}
        </template>
      </el-table-column>

      <!-- <el-table-column label="是否删除" align="center">
            <template slot-scope="scope">
                <el-tag :type="( scope.row.isDeleted ? 'danger' : 'success')" :class="[scope.row.isDeleted ?  'el-icon-close' : 'el-icon-check']"></el-tag>
            </template>
        </el-table-column> -->
      <el-table-column :label="$t('AbpIdentity[\'DisplayName:LockoutEnabled\']')" align="center">
        <template slot-scope="scope">
          <el-tag :type="( scope.row.lockoutEnabled ? 'success' : 'danger')">
            {{ scope.row.lockoutEnabled ? '启用' : '禁用' }}
          </el-tag>
        </template>
      </el-table-column>
      <el-table-column label="邮箱已经确认" align="center">
        <template slot-scope="scope">
          <el-tag :type="( scope.row.emailConfirmed ? 'success' : 'danger')" :class="[scope.row.emailConfirmed ? 'el-icon-check':'el-icon-close' ]" />
        </template>
      </el-table-column>
      <el-table-column label="锁定结束时间" align="center" width="200">
        <template slot-scope="scope">
          <!-- 过滤器用括号会不识别。把全局过滤器当作方法一样使用  -->
          {{ scope.row.lockoutEnd | moment }}
        </template>
      </el-table-column>

      <el-table-column label="创建时间" align="center" width="200" prop="creationTime" sortable="custom">
        <template slot-scope="scope">
          {{ scope.row.creationTime | moment }}

        </template>
      </el-table-column>
      <el-table-column label="最后修改时间" align="center" width="200" prop="creationTime" sortable="custom">
        <template slot-scope="scope">
          {{ scope.row.lastModificationTime | moment }}
        </template>
      </el-table-column>

      <el-table-column align="left" label="操作" width="220">
        <template slot-scope="scope">
          <el-dropdown trigger="click" @command="handleCommand">
            <el-button type="primary" size="small" icon="el-icon-setting">
              {{ $t("AbpIdentity['Actions']") }}<i class="el-icon-arrow-down el-icon--right" />
            </el-button>
            <el-dropdown-menu slot="dropdown">
              <el-dropdown-item :command="beforeHandleCommand(scope, 'edit')">{{ $t("AbpIdentity['Edit']") }}</el-dropdown-item>
              <el-dropdown-item :command="beforeHandleCommand(scope, 'manageClaims')">{{ $t("AbpIdentity['ManageClaim']") }}</el-dropdown-item>
              <el-dropdown-item :command="beforeHandleCommand(scope, 'updatePermission')">{{ $t("AbpIdentity['Permissions']") }}</el-dropdown-item>
              <el-dropdown-item :command="beforeHandleCommand(scope, 'changePassword')">设置密码</el-dropdown-item>
              <el-dropdown-item v-if="scope.row.lockoutEnd && (new Date(scope.row.lockoutEnd)) >= new Date()" :command="beforeHandleCommand(scope, 'unlock')">解锁</el-dropdown-item>
              <el-dropdown-item v-else :command="beforeHandleCommand(scope, 'lock')">锁定</el-dropdown-item>
              <el-dropdown-item :command="beforeHandleCommand(scope, 'delete')">{{ $t("AbpIdentity['Delete']") }}</el-dropdown-item>
            </el-dropdown-menu>
          </el-dropdown>
        </template>
      </el-table-column>
    </el-table>
    <pagination v-show="total > 0" :total="total" :page.sync="listQuery.page" :limit.sync="listQuery.limit" @pagination="fetchData" />

    <!-- 创建 修改用户对话框 -->
    <el-dialog :title="dialogStatus == 'create'? $t('AbpIdentity[\'NewUser\']'): $t('AbpIdentity[\'Edit\']')" :visible.sync="dialogFormVisible">
      <el-form ref="dataForm" :rules="rules" :model="temp" label-width="120px" label-position="right">
        <el-tabs v-model="activeName">
          <el-tab-pane :label="$t('AbpIdentity[\'UserInformations\']')" name="first">
            <el-form-item :label="$t('AbpIdentity[\'UserName\']')" prop="userName">
              <el-input v-model="temp.userName" />
            </el-form-item>
            <el-form-item :label="$t('AbpIdentity[\'DisplayName:Surname\']')" prop="surname">
              <el-input v-model="temp.surname" />
            </el-form-item>
            <el-form-item :label="$t('AbpIdentity[\'DisplayName:Name\']')" prop="name">
              <el-input v-model="temp.name" />
            </el-form-item>
            <el-form-item v-if="dialogStatus === 'create'" :label="$t('AbpIdentity[\'Password\']')" prop="password" :class="{ 'is-required': !temp.id }">
              <el-input v-model="temp.password" type="text" auto-complete="off" class="no-autofill-pwd" />
            </el-form-item>
            <el-form-item :label="$t('AbpIdentity[\'EmailAddress\']')" prop="email">
              <el-input v-model="temp.email" />
            </el-form-item>
            <el-form-item :label="$t('AbpIdentity[\'PhoneNumber\']')" prop="phoneNumber">
              <el-input v-model="temp.phoneNumber" />
            </el-form-item>
            <el-form-item prop="lockoutEnabled">
              <el-checkbox v-model="temp.lockoutEnabled" :label="$t('AbpIdentity[\'DisplayName:LockoutEnabled\']')" />
            </el-form-item>
            <el-form-item prop="twoFactorEnabled">
              <el-checkbox v-model="temp.twoFactorEnabled" :label="$t('AbpIdentity[\'DisplayName:TwoFactorEnabled\']')" />
            </el-form-item>
          </el-tab-pane>
          <el-tab-pane :label="$t('AbpIdentity[\'Roles\']')" name="second">
            <template>
              <el-checkbox v-model="checkAll" :indeterminate="isIndeterminate" @change="handleCheckAllChange">全选</el-checkbox>
              <div style="margin: 15px 0" />
              <el-checkbox-group v-model="checkedRoles" class="check-roles-group" @change="handleCheckedRolesChange">
                <el-checkbox v-for="role in roles" :key="role" span="8" :label="role">{{ role }}</el-checkbox>
              </el-checkbox-group>
            </template>
          </el-tab-pane>
          <el-tab-pane :label="$t('AbpIdentity[\'OrganizationUnit:Tree\']')" name="third">
            <org-tree ref="dialogOrgTree" :show-checkbox="true" :support-single-checked="singleChecked" @handleCheckChange="handleCheckChange" />
            <el-form-item />
          </el-tab-pane>
        </el-tabs>

      </el-form>
      <div style="text-align: right">
        <el-button type="danger" @click="dialogFormVisible = false">{{ $t("AbpIdentity['Cancel']") }}</el-button>
        <el-button type="primary" @click="dialogStatus === 'create' ? createData() : updateData()">{{ $t("AbpIdentity['Save']") }}</el-button>
      </div>
    </el-dialog>

    <user-claim ref="claimTypeDialog" />

    <!-- 重置密码对话框 -->
    <el-dialog :title="$t('AbpIdentity[\'Users:ChangePassword\']')" :visible.sync="dialogChangePasswordFormVisible" width="30%">
      <el-form ref="changePasswordForm" :rules="rules" :model="changePasswordForm" label-width="80px" label-position="right">
        <el-form-item :label="$t('AbpIdentity[\'Password\']')" :inline="true">
          <el-input v-model="changePasswordForm.password" prop="password" type="text" auto-complete="off" style="width:90%" />
          <el-button type="primary" icon="el-icon-refresh" @click="generatePassword( 8 )" />
        </el-form-item>
      </el-form>
      <div style="text-align: right">
        <el-button type="danger" @click="dialogChangePasswordFormVisible = false">{{ $t("AbpIdentity['Cancel']") }}</el-button>
        <el-button type="primary" @click="changePassword()">{{ $t("AbpIdentity['Save']") }}</el-button>
      </div>
    </el-dialog>

    <!-- 锁定用户对话框 -->
    <el-dialog title="锁定" :visible.sync="dialogLockFormVisible" width="30%">
      <el-form :model="lockForm">
        <el-form-item label="锁定终止时间" label-width="100px">
          <el-date-picker v-model="lockForm.lockoutEnd" type="datetime" placeholder="选择日期" />
        </el-form-item>

      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button @click="dialogLockFormVisible = false">{{ $t("AbpIdentity['Cancel']") }}</el-button>
        <el-button type="primary" @click="lock()">{{ $t("AbpIdentity['Save']") }}</el-button>
      </div>
    </el-dialog>
    <permission-dialog ref="permissionDialog" provider-name="U" />
  </div>
</template>

<script>
import {
  getUserList,
  ExportUserToXlsx,
  createUserToOrg,
  updateUserToOrg,
  ChangePassword,
  Lock,
  UnLock,
  deleteUser,
  getOrganizationsByUserId,
  getAssignableRoles,
  getRolesByUserId
} from '@/api/system-manage/identity/user'
import {
  getRoleList
} from '@/api/system-manage/identity/role'
import {
  getOrganizations
} from '@/api/system-manage/identity/organization-unit'

import baseListQuery from '@/utils/abp'
import { downloadByData } from '@/utils/download'
import Pagination from '@/components/Pagination' // Secondary package based on el-pagination
import UserClaim from './components/UserClaim.vue'
import PermissionDialog from '../components/permission-dialog'
import OrgTree from '../components/org-tree'

import {
  pickerRangeWithHotKey
} from '@/utils/picker'

import {
  validEmail,
  validPhone
} from '@/utils/validate'

export default {
  name: 'User',
  components: {
    Pagination,
    UserClaim,
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
        // 默认设置手机号可以为空
        return callback()
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

    const passwordValidator = (rule, value, callback) => {
      if (this.temp.id && !value) {
        callback()
        return
      }

      if (!value) {
        callback(
          new Error(
            this.$i18n.t("AbpIdentity['The {0} field is required.']", [
              this.$i18n.t("AbpIdentity['Password']")
            ])
          )
        )
        return
      }
      if (value.length < this.requiredLength) {
        callback(
          new Error(
            this.$i18n.t("AbpIdentity['Identity.PasswordTooShort']", [`${this.requiredLength}`])
          )
        )
        return
      }
      if (value.length > 128) {
        callback(
          new Error(
            this.$i18n.t(
              "AbpIdentity['The field {0} must be a string with a maximum length of {1}.']",
              [this.$i18n.t("AbpIdentity['Password']"), '128']
            )
          )
        )
        return
      }
      let reg = /\d+/
      if (this.requireDigit && !reg.test(value)) {
        callback(
          new Error(
            this.$i18n.t("AbpIdentity['Identity.PasswordRequiresDigit']")
          )
        )
        return
      }
      reg = /[a-z]+/
      if (this.requireLowercase && !reg.test(value)) {
        callback(
          new Error(
            this.$i18n.t("AbpIdentity['Identity.PasswordRequiresLower']")
          )
        )
        return
      }
      reg = /[A-Z]+/
      if (this.requireUppercase && !reg.test(value)) {
        callback(
          new Error(
            this.$i18n.t("AbpIdentity['Identity.PasswordRequiresUpper']")
          )
        )
        return
      }
      reg = /\W+/
      if (this.requireNonAlphanumeric && !reg.test(value)) {
        callback(
          new Error(
            this.$i18n.t(
              "AbpIdentity['Identity.PasswordRequiresNonAlphanumeric']"
            )
          )
        )
        return
      }

      callback()
    }

    return {
      downloadLoading: false,
      list: null,
      listLoading: true,
      advanced: false, // 判断搜索栏展开/收起
      total: 0,
      roleOptions: [],
      orgOptions: [],
      queryCreationTime: undefined, // 时间范围过滤条件
      queryModifitionTime: undefined,
      pickerOptions: pickerRangeWithHotKey,
      listQuery: Object.assign({
        roleId: undefined,
        organizationUnitId: undefined,
        userName: undefined,
        phoneNumber: undefined,
        name: undefined,
        isLockedOut: undefined,
        notActive: undefined,
        emailConfirmed: undefined,
        isExternal: undefined,
        minCreationTime: undefined,
        maxCreationTime: undefined,
        minModifitionTime: undefined,
        maxModifitionTime: undefined
      }, baseListQuery),
      checkAll: false,
      checkedRoles: [],
      roles: [],
      isIndeterminate: true,
      activeName: 'first',
      singleChecked: false,
      temp: {
        id: '',
        userName: '',
        password: '1q2w3E*', // 修改用户的时候是重置密码，如果密码为空就不修改密码
        name: '',
        surname: '',
        email: '',
        lockoutEnabled: '',
        lockoutEnd: '',
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
      dialogChangePasswordFormVisible: false,
      changePasswordForm: {
        password: '' // 重置后的密码
      },
      lockForm: {
        lockoutEnd: undefined
      },
      dialogLockFormVisible: false,
      rules: {
        userName: [{
          required: true,
          message: this.$i18n.t("AbpIdentity['The {0} field is required.']", [
            this.$i18n.t("AbpIdentity['UserName']")
          ]),
          trigger: 'blur'
        },
        {
          max: 256,
          message: this.$i18n.t(
            "AbpIdentity['The field {0} must be a string with a maximum length of {1}.']",
            [this.$i18n.t("AbpIdentity['UserName']"), '256']
          ),
          trigger: 'blur'
        }
        ],
        email: [{
          required: true,
          message: this.$i18n.t("AbpIdentity['The {0} field is required.']", [
            this.$i18n.t("AbpIdentity['EmailAddress']")
          ]),
          trigger: 'blur'
        },
        {
          type: 'email',
          message: this.$i18n.t(
            "AbpIdentity['The {0} field is not a valid e-mail address.']",
            [this.$i18n.t("AbpIdentity['EmailAddress']")]
          ),
          trigger: ['blur', 'change']
        },
        {
          max: 256,
          message: this.$i18n.t(
            "AbpIdentity['The field {0} must be a string with a maximum length of {1}.']",
            [this.$i18n.t("AbpIdentity['EmailAddress']"), '256']
          ),
          trigger: 'blur'
        }
        ],
        name: [{
          max: 64,
          message: this.$i18n.t(
            "AbpIdentity['The field {0} must be a string with a maximum length of {1}.']",
            [this.$i18n.t("AbpIdentity['DisplayName:Name']"), '64']
          ),
          trigger: 'blur'
        }],
        surname: [{
          max: 64,
          message: this.$i18n.t(
            "AbpIdentity['The field {0} must be a string with a maximum length of {1}.']",
            [this.$i18n.t("AbpIdentity['DisplayName:Surname']"), '64']
          ),
          trigger: 'blur'
        }],
        phoneNumber: [
          // {
          //     required: true,
          //     message: this.$i18n.t("AbpIdentity['The {0} field is required.']", [
          //         this.$i18n.t("AbpIdentity['PhoneNumber']")
          //     ]),
          //     trigger: 'blur'
          // },
          {
            validator: checkPhone,
            message: this.$i18n.t("AbpIdentity['The {0} field is not a valid phone number.']", [
              this.$i18n.t("AbpIdentity['PhoneNumber']")
            ]),
            trigger: 'blur'
          },
          {
            max: 16,
            message: this.$i18n.t(
              "AbpIdentity['The field {0} must be a string with a maximum length of {1}.']",
              [this.$i18n.t("AbpIdentity['PhoneNumber']"), '16']
            ),
            trigger: 'blur'
          }
        ],
        password: [{
          validator: passwordValidator,
          trigger: ['blur', 'change']
        }]
      }
    }
  },
  created() {
    this.fetchData()
    this.fetchRoleOptions()
    this.fetchOrgOptions()
  },
  methods: {
    fetchRoleOptions() {
      getRoleList({
        page: 1,
        limit: 1000
      }).then((response) => {
        this.roleOptions = response.items
      })
    },
    fetchOrgOptions() {
      getOrganizations({
        page: 1,
        limit: 1000
      }).then((response) => {
        this.orgOptions = response.items
      })
    },
    datePickerChange(value) {
      if (!value) {
        // 日期选择器改变事件 ~ 解决日期选择器清空 值不清空的问题
        this.listQuery.minCreationTime = undefined
        this.listQuery.maxCreationTime = undefined

        this.listQuery.minModifitionTime = undefined
        this.listQuery.maxModifitionTime = undefined
      }
    },
    fetchData() {
      this.listLoading = true
      if (this.queryCreationTime) {
        this.listQuery.minCreationTime = this.queryCreationTime[0]
        this.listQuery.maxCreationTime = this.queryCreationTime[1]
      }
      if (this.queryModifitionTime) {
        this.listQuery.minModifitionTime = this.queryModifitionTime[0]
        this.listQuery.maxModifitionTime = this.queryModifitionTime[1]
      }
      getUserList(this.listQuery).then((response) => {
        this.list = response.items
        this.total = response.totalCount
        this.listLoading = false
      })
    },
    // 重置查询参数
    resetQueryForm() {
      this.listQuery = Object.assign({
        roleNames: '',
        organiztion: '',
        username: '',
        phone: '',
        emailAddress: '',
        lockoutEnd: '',
        isLockOut: '',
        isActive: '' // 是否启用
      }, baseListQuery)
    },
    toggleAdvanced() {
      this.advanced = !this.advanced
    },
    handleFilter() {
      this.listQuery.page = 1
      this.fetchData()
    },
    sortChange(data) {
      const {
        prop,
        order
      } = data
      this.listQuery.sort = order ? `${prop} ${order}` : undefined
      this.handleFilter()
    },
    handleRefresh() {
      this.listQuery.filter = undefined
      this.fetchData()
    },
    // 导出 所有
    handleDownload() {
      this.$confirm('是否确认导出全部查询结果?', this.$i18n.t("AbpUi['AreYouSure']"), {
        confirmButtonText: this.$i18n.t("AbpUi['Yes']"),
        cancelButtonText: this.$i18n.t("AbpUi['Cancel']"),
        type: 'warning'
      }).then(response => {
        this.downloadLoading = true
        ExportUserToXlsx(this.listQuery).then(response => {
          downloadByData(response, 'user.xlsx')
          this.downloadLoading = false
        }).catch(err => {
          console.log(err)
          this.downloadLoading = false
          this.$message.warning(err)
        })
      })
    },

    handleCommand(param) {
      switch (param.command) {
        case 'edit':
          this.handleUpdate(param.scope.row)
          break
        case 'lock':
          this.handleLock(param.scope.row)
          break
        case 'unlock':
          this.unLock(param.scope.row)
          break
        case 'manageClaims':
          this.handleManageClaims(param.scope.row)
          break
        case 'updatePermission':
          this.handleUpdatePermission(param.scope.row)
          break
        case 'changePassword':
          this.handelChangePassword(param.scope.row)
          break
        case 'delete':
          this.deleteData(param.scope.row)
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
        id: '',
        userName: '',
        password: '',
        name: '',
        surname: '',
        email: '',
        lockoutEnabled: '',
        lockoutEnd: '',
        phoneNumber: '',
        roleNames: [],
        orgIds: []
      }
      this.checkedRoles = []
    },
    handleCreate() {
      this.resetTemp()
      this.dialogStatus = 'create'
      this.dialogFormVisible = true
      this.singleChecked = false
      this.fetchRoles()
      this.$nextTick(() => {
        this.$refs['dataForm'].clearValidate()
      })
    },
    createData() {
      this.$refs['dataForm'].validate((valid, object) => {
        if (valid) {
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
        } else {
          // 错误消息弹框的形式出现
          const errorMessage = []
          for (const key in object) {
            errorMessage.push(object[key][0].message)
          }
          this.$message.error(errorMessage.join(','))
        }
      })
    },
    fetchRoles() {
      getAssignableRoles().then((response) => {
        var obj = response.items
        var tempArr = []
        var arr = Object.keys(obj).forEach(function(key) {
          tempArr.push(obj[key].name)
        })
        this.roles = tempArr
      })
    },
    fetchUserRoles(id) {
      getRolesByUserId(id).then((response) => {
        var obj = response.items
        var tempArr = []
        if (obj.length > 0) {
          var arr = Object.keys(obj).forEach(function(key) {
            tempArr.push(obj[key].name)
          })
        }

        this.checkedRoles = tempArr
      })
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
    handleUpdate(row) {
      this.temp = Object.assign({}, row) // copy obj
      this.dialogStatus = 'update'
      this.dialogFormVisible = true
      // update can check many
      this.singleChecked = false
      this.fetchRoles()
      this.fetchUserRoles(row.id)

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
      this.$refs['dataForm'].validate((valid, object) => {
        if (valid) {
          this.temp.roleNames = this.checkedRoles
          const tempData = Object.assign({}, this.temp)
          updateUserToOrg(tempData).then(() => {
            const index = this.list.findIndex((v) => v.id === this.temp.id)
            this.list.splice(index, 1, this.temp)
            this.dialogFormVisible = false

            this.$notify({
              title: this.$i18n.t("TigerUi['Success']"),
              message: this.$i18n.t("TigerUi['SuccessMessage']"),
              type: 'success',
              duration: 2000
            })
          })
        } else {
          const errorMessage = []
          for (const key in object) {
            errorMessage.push(obj[key][0].message)
          }
          this.$message.error(errorMessage.join(','))
        }
      })
    },
    deleteData(row) {
      this.$confirm(this.$i18n.t("AbpUi['ItemWillBeDeletedMessageWithFormat']", [
        row.userName
      ]), this.$i18n.t("AbpUi['AreYouSure']"), {
        confirmButtonText: this.$i18n.t("AbpUi['Yes']"),
        cancelButtonText: this.$i18n.t("AbpUi['Cancel']"),
        type: 'warning'
      })
        .then(() => {
          deleteUser(row.id)
            .then((response) => {
              const index = this.list.findIndex((v) => v.id === row.id)
              this.list.splice(index, 1)
              this.$message({
                title: this.$i18n.t("TigerUi['Success']"),
                message: this.$i18n.t("TigerUi['SuccessMessage']"),
                type: 'success',
                duration: 2000
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
    handleManageClaims(row) {
      this.$refs['claimTypeDialog'].handleManageClaims(row)
    },
    handleUpdatePermission(row) {
      // 用户授权
      this.$refs['permissionDialog'].handleUpdatePermission(row)
    },
    handleLock(row) {
      this.dialogLockFormVisible = true
      this.lockForm.lockoutEnd = undefined
      this.temp = Object.assign({}, row) // copy obj
    },
    lock() {
      var dateEnd = new Date(this.lockForm.lockoutEnd)
      var now = new Date()
      var seconds = parseInt((dateEnd.getTime() - now.getTime()) / 1000)
      this.temp.lockoutEnd = this.lockForm.lockoutEnd
      Lock(this.temp.id, {
        seconds: seconds
      }).then(() => {
        const index = this.list.findIndex((v) => v.id === this.temp.id)
        this.list.splice(index, 1, this.temp)
        this.dialogLockFormVisible = false
        this.$notify({
          title: this.$i18n.t("TigerUi['Success']"),
          message: this.$i18n.t("TigerUi['SuccessMessage']"),
          type: 'success',
          duration: 2000
        })
      })
    },
    unLock(row) {
      this.temp = Object.assign({}, row) // copy obj
      this.temp.lockoutEnd = undefined
      UnLock(this.temp.id).then(() => {
        const index = this.list.findIndex((v) => v.id === this.temp.id)
        this.list.splice(index, 1, this.temp)
        this.$notify({
          title: this.$i18n.t("TigerUi['Success']"),
          message: this.$i18n.t("TigerUi['SuccessMessage']"),
          type: 'success',
          duration: 2000
        })
      })
    },
    handelChangePassword(row) {
      this.temp = Object.assign({}, row) // copy obj
      this.changePasswordForm.password = ''
      this.dialogChangePasswordFormVisible = true
    },
    // Random user password
    generatePassword(length = 8) {
      length = Number(length)
      // Limit length
      if (length < 6) {
        length = 6
      } else if (length > 16) {
        length = 16
      }
      const passwordArray = ['ABCDEFGHIJKLMNOPQRSTUVWXYZ', 'abcdefghijklmnopqrstuvwxyz', '1234567890', '!@#$%&*()']
      var password = []
      let n = 0
      for (let i = 0; i < length; i++) {
        // If password length less than 9, all value random
        if (password.length < (length - 4)) {
          // Get random passwordArray index
          const arrayRandom = Math.floor(Math.random() * 4)
          // Get password array value
          const passwordItem = passwordArray[arrayRandom]
          // Get password array value random index
          // Get random real value
          const item = passwordItem[Math.floor(Math.random() * passwordItem.length)]
          password.push(item)
        } else {
          // If password large then 9, lastest 4 password will push in according to the random password index
          // Get the array values sequentially
          const newItem = passwordArray[n]
          const lastItem = newItem[Math.floor(Math.random() * newItem.length)]
          // Get array splice index
          const spliceIndex = Math.floor(Math.random() * password.length)
          password.splice(spliceIndex, 0, lastItem)
          n++
        }
      }
      this.changePasswordForm.password = password.join('')
    },
    changePassword() {
      // TODO:表单验证没有生效
      this.$refs['changePasswordForm'].validate((valid) => {
        if (valid) {
          ChangePassword(this.temp.id, {
            password: this.changePasswordForm.password
          }).then(() => {
            this.dialogChangePasswordFormVisible = false
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
.check-roles-group .el-checkbox {
    width: 120px;
}

// 粗暴解决浏览器密码表单自动填充问题 https://www.jianshu.com/p/c92d99d27360
.no-autofill-pwd {

    .el-input__inner {

        -webkit-text-security: disc !important;

    }

}
</style>
