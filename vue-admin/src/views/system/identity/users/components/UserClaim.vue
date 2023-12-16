<template>
  <div class="app-container">
    <el-dialog :title="$t('AbpIdentity[\'ClaimTypes\']')" :visible.sync="dialogFormVisible">
      <el-card class="box-card">
        <el-form ref="dataForm" :model="form" :rules="rules" :inline="true" label-position="left" label-width="120px">
          <el-form-item :label="$t('AbpIdentity[\'DisplayName:ClaimType\']')" :label-width="formLabelWidth" prop="claimType">
            <el-select v-model="form.claimType" placeholder="请选择">
              <el-option
                v-for="item in claimTypeOptions"
                :key="item.id"
                :label="item.name"
                :value="item.name"
              />
            </el-select>
          </el-form-item>
          <el-form-item :label="$t('AbpIdentity[\'DisplayName:ClaimValue\']')" :label-width="formLabelWidth" prop="claimValue">
            <el-input v-model="form.claimValue" />
          </el-form-item>

          <el-form-item>
            <el-button type="primary" @click="onSubmit">{{ $t('AbpIdentity[\'IdentityClaim:New\']') }}</el-button>
          </el-form-item>
        </el-form>

        <el-table v-loading="listLoading" :data="claimTypeList" element-loading-text="Loading" border fit highlight-current-row>
          <el-table-column type="selection" width="55" center />
          <el-table-column align="center" label="ID" width="95">
            <template slot-scope="scope">{{ scope.$index }}</template>
          </el-table-column>
          <el-table-column :label="$t('AbpIdentity[\'DisplayName:ClaimType\']')" align="center">
            <template slot-scope="scope">{{ scope.row.claimType }}</template>
          </el-table-column>
          <el-table-column :label="$t('AbpIdentity[\'DisplayName:ClaimValue\']')" align="center">
            <template slot-scope="scope">{{ scope.row.claimValue }}</template>
          </el-table-column>

          <el-table-column :label="$t('AbpIdentity[\'Actions\']')" width="150">
            <template slot-scope="scope">
              <el-button type="danger" @click="deleteData(scope.row)">
                {{ $t("AbpIdentity['Delete']") }}
              </el-button>
            </template>
          </el-table-column>
        </el-table>
      </el-card>

      <div slot="footer" class="dialog-footer">
        <el-button @click="dialogFormVisible = false">{{ $t("AbpIdentity['Cancel']") }}</el-button>
        <el-button type="primary" @click="dialogFormVisible = false">{{ $t("AbpIdentity['Save']") }}</el-button>
      </div>
    </el-dialog>
  </div>
</template>

<script>

import {
  getUserClaims,
  createUserClaim,
  deleteUserClaim
} from '@/api/system-manage/identity/user'

import {
  getAllClaimTypes
} from '@/api/system-manage/identity/claim-type'

export default {
  name: 'UserClaim',
  props: {
    // dialogFormVisible: {
    //   type: Boolean,
    //   required: false,
    //   default: false
    // }
  },
  data() {
    return {
      claimTypeList: [

      ],
      userId: undefined,
      form: {
        claimType: '',
        claimValue: ''
      },
      listLoading: true,
      dialogFormVisible: false,
      claimTypeOptions: [],
      formLabelWidth: '120px',
      rules: {
        claimType: [
          {
            required: true,
            message: this.$i18n.t("AbpIdentity['The {0} field is required.']", [
              this.$i18n.t("AbpIdentity['DisplayName:ClaimType']")
            ]),
            trigger: 'blur'
          },
          {
            max: 256,
            message: this.$i18n.t(
              "AbpIdentity['The field {0} must be a string with a maximum length of {1}.']",
              [this.$i18n.t("AbpIdentity['DisplayName:ClaimType']"), '256']
            ),
            trigger: 'blur'
          }
        ],
        claimValue: [
          {
            required: true,
            message: this.$i18n.t("AbpIdentity['The {0} field is required.']", [
              this.$i18n.t("AbpIdentity['DisplayName:ClaimValue']")
            ]),
            trigger: 'blur'
          },
          {
            max: 256,
            message: this.$i18n.t(
              "AbpIdentity['The field {0} must be a string with a maximum length of {1}.']",
              [this.$i18n.t("AbpIdentity['DisplayName:ClaimValue']"), '256']
            ),
            trigger: 'blur'
          }
        ]
      }
    }
  },
  created() {

  },
  methods: {
    resetTemp() {
      this.form = {
        claimType: '',
        claimValue: ''
      }
    },
    fetchClaimTypes() {
      getAllClaimTypes().then(response => {
        this.claimTypeOptions = response
      })
    },
    fetchUserClaimTypes(userId) {
      this.listLoading = true
      getUserClaims(userId).then(response => {
        this.claimTypeList = response.items
        this.listLoading = false
      })
    },
    handleManageClaims(row) {
      this.dialogFormVisible = true
      this.userId = row.id
      this.fetchClaimTypes()
      this.fetchUserClaimTypes(row.id)
    },
    onSubmit() {
      this.$refs['dataForm'].validate((valid) => {
        if (valid) {
          createUserClaim(this.userId, this.form).then(() => {
            this.fetchUserClaimTypes(this.userId)
            this.resetTemp()
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
    // 删除UserClaim
    deleteData(row) {
      this.$confirm(
        this.$i18n.t("AbpUi['ItemWillBeDeletedMessageWithFormat']", [
          row.claimType
        ]),
        this.$i18n.t("AbpUi['AreYouSure']"), {
          confirmButtonText: this.$i18n.t("AbpUi['Yes']"), // 确认按钮
          cancelButtonText: this.$i18n.t("AbpUi['Cancel']"), // 取消按钮
          type: 'warning' // 弹框类型
        }
      ).then(() => {
        var request = { claimType: row.claimType, claimValue: row.claimValue }
        deleteUserClaim(this.userId, request)
          .then((response) => {
            this.fetchUserClaimTypes(this.userId)
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
      }).catch(() => {
        this.$message({
          type: 'info',
          message: this.$i18n.t("AbpUi['Cancel']")
        })
      })
    }
  }
}
</script>

<style scoped>
.line{
  text-align: center;
}
</style>

